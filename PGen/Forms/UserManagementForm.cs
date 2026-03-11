using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGen.Auth;

namespace PGen;

[SupportedOSPlatform("windows")]
public partial class UserManagementForm : Form
{
    private readonly UserAccount _currentUser;

    public UserManagementForm(UserAccount currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();

        // configure admin menu availability
        menuCreateLicense.Enabled = AuthService.HasRight(_currentUser, UserRight.ManageLicenses);
        menuManageUsers.Enabled = false; // already on user management
        menuManageRoles.Enabled = AuthService.HasRight(_currentUser, UserRight.EditRoles);
    }

    private void menuCreateLicense_Click(object sender, EventArgs e)
    {
        if (!menuCreateLicense.Enabled)
            return;

        using var dlg = new LicenseAdminForm(_currentUser);
        var result = dlg.ShowDialog(this);
        if (result == DialogResult.Retry)
        {
            DialogResult = DialogResult.Retry;
            Close();
        }
    }

    private void menuManageUsers_Click(object sender, EventArgs e)
    {
        // already on user management; nothing to do
    }

    private void menuManageRoles_Click(object sender, EventArgs e)
    {
        if (!menuManageRoles.Enabled)
            return;

        using var dlg = new RoleManagementForm(_currentUser);
        var result = dlg.ShowDialog(this);
        if (result == DialogResult.Retry)
        {
            DialogResult = DialogResult.Retry;
            Close();
        }
    }

    private async Task ReloadUsers()
    {
        dgvUsers.AutoGenerateColumns = false;
        var users = await AuthService.GetUsersAsync();
        
        // Enrich users with role names and active status
        var enrichedUsers = new List<object>();
        foreach (var u in users)
        {
            var role = await RoleService.GetRoleAsync(u.RoleId);
            enrichedUsers.Add(new
            {
                u.UserName,
                RoleName = role?.Name ?? "Unknown",
                u.RoleId,
                u.IsActive,
                StatusText = u.IsActive ? "Active" : "Inactive"
            });
        }
        
        dgvUsers.DataSource = enrichedUsers;
    }

    private async void btnRefresh_Click(object sender, EventArgs e)
    {
        await ReloadUsers();
    }

    private async void btnAdd_Click(object sender, EventArgs e)
    {

        using var dlg = new UserAddForm();
        var result = dlg.ShowDialog(this);
        if (result == DialogResult.Retry)
        {
            DialogResult = DialogResult.Retry;
            Close();
            return;
        }
        if (result != DialogResult.OK)
            return;

        try
        {
            await AuthService.AddUserAsync(dlg.UserNameValue, dlg.PasswordValue, dlg.RoleIdValue);
            await ReloadUsers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Add user failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnEdit_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedUserName();
        if (selected is null)
            return;

        var users = await AuthService.GetUsersAsync();
        var user = users.FirstOrDefault(u => string.Equals(u.UserName, selected, StringComparison.OrdinalIgnoreCase));
        if (user == null)
            return;

        using var dlg = new UserAddForm(user);
        var result = dlg.ShowDialog(this);
        if (result == DialogResult.Retry)
        {
            DialogResult = DialogResult.Retry;
            Close();
            return;
        }
        if (result != DialogResult.OK)
            return;

        try
        {
            if (dlg.RoleIdValue != user.RoleId)
            {
                await AuthService.UpdateUserRoleAsync(user.UserName, dlg.RoleIdValue);
            }
            await ReloadUsers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Edit user failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnResetPassword_Click(object sender, EventArgs e)
    {

        var selected = GetSelectedUserName();
        if (selected is null)
            return;

        using var dlg = new PasswordResetForm(selected);
        var result = dlg.ShowDialog(this);
        if (result == DialogResult.Retry)
        {
            DialogResult = DialogResult.Retry;
            Close();
            return;
        }
        if (result != DialogResult.OK)
            return;

        try
        {
            await AuthService.ResetPasswordAsync(selected, dlg.NewPassword);
            MessageBox.Show(this, "Password updated.", "Users", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await ReloadUsers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Reset failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedUserName();
        if (selected is null)
            return;

        var users = await AuthService.GetUsersAsync();
        var user = users.FirstOrDefault(u => string.Equals(u.UserName, selected, StringComparison.OrdinalIgnoreCase));
        if (user == null)
            return;

        var action = user.IsActive ? "deactivate" : "activate";
        var actionText = user.IsActive ? "Deactivate" : "Activate";
        
        if (MessageBox.Show(this, $"{actionText} user '{selected}'?", $"Confirm {action}", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        try
        {
            if (user.IsActive)
            {
                await AuthService.DeactivateUserAsync(selected);
                MessageBox.Show(this, $"User '{selected}' has been deactivated.", "User Deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                await AuthService.ActivateUserAsync(selected);
                MessageBox.Show(this, $"User '{selected}' has been activated.", "User Activated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            await ReloadUsers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, $"{actionText} failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private string? GetSelectedUserName()
    {
        if (dgvUsers.SelectedRows.Count == 0)
        {
            MessageBox.Show(this, "Select a user first.", "Users", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }

        var row = dgvUsers.SelectedRows[0];
        if (row.DataBoundItem is null)
            return null;
        
        dynamic item = row.DataBoundItem;
        return item.UserName;
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Retry;
        Close();
    }
}
