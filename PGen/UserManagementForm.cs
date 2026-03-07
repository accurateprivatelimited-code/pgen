using System.Runtime.Versioning;
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

    private void ReloadUsers()
    {
        dgvUsers.AutoGenerateColumns = false;
        var users = AuthService.GetUsers().ToList();
        
        // Enrich users with role names
        var enrichedUsers = users.Select(u => new
        {
            u.UserName,
            RoleName = RoleService.GetRole(u.RoleId)?.Name ?? "Unknown",
            u.RoleId
        }).ToList();
        
        dgvUsers.DataSource = enrichedUsers;
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        ReloadUsers();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {

        using var dlg = new UserAddForm();
        if (dlg.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            AuthService.AddUser(dlg.UserNameValue, dlg.PasswordValue, dlg.RoleIdValue);
            ReloadUsers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Add user failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedUserName();
        if (selected is null)
            return;

        var user = AuthService.GetUsers()
            .FirstOrDefault(u => string.Equals(u.UserName, selected, StringComparison.OrdinalIgnoreCase));
        if (user == null)
            return;

        using var dlg = new UserAddForm(user);
        if (dlg.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            if (dlg.RoleIdValue != user.RoleId)
            {
                AuthService.UpdateUserRole(user.UserName, dlg.RoleIdValue);
            }
            ReloadUsers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Edit user failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnResetPassword_Click(object sender, EventArgs e)
    {

        var selected = GetSelectedUserName();
        if (selected is null)
            return;

        using var dlg = new PasswordResetForm(selected);
        if (dlg.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            AuthService.ResetPassword(selected, dlg.NewPassword);
            MessageBox.Show(this, "Password updated.", "Users", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadUsers();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Reset failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {

        var selected = GetSelectedUserName();
        if (selected is null)
            return;

        if (MessageBox.Show(this, $"Delete user '{selected}'?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        try
        {
            AuthService.DeleteUser(selected);
            ReloadUsers();
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
}
