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
    }

    private async Task ReloadUsers()
    {
        dgvUsers.AutoGenerateColumns = false;
        var users = await AuthService.GetUsersAsync();
        
        // Enrich users with role names
        var enrichedUsers = new List<object>();
        foreach (var u in users)
        {
            var role = await RoleService.GetRoleAsync(u.RoleId);
            enrichedUsers.Add(new
            {
                u.UserName,
                RoleName = role?.Name ?? "Unknown",
                u.RoleId
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
        if (dlg.ShowDialog(this) != DialogResult.OK)
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
        if (dlg.ShowDialog(this) != DialogResult.OK)
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
        if (dlg.ShowDialog(this) != DialogResult.OK)
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

        if (MessageBox.Show(this, $"Delete user '{selected}'?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        try
        {
            await AuthService.DeleteUserAsync(selected);
            await ReloadUsers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Delete failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
