using System.Runtime.Versioning;
using System.Windows.Forms;
using System.Linq;
using MySql.Data.MySqlClient;
using PGen.Auth;
using PGen.Data;

namespace PGen;

[SupportedOSPlatform("windows")]
public partial class LicenseAdminForm : Form
{
    private readonly UserAccount _currentUser;

    public LicenseAdminForm(UserAccount currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();
        if (!AuthService.HasRight(_currentUser, UserRight.ManageLicenses))
        {
            MessageBox.Show("You do not have permission to manage licenses.", "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Close();
            return;
        }

        // configure admin menu availability
        menuCreateLicense.Enabled = false; // already on license form
        menuManageUsers.Enabled = AuthService.HasRight(_currentUser, UserRight.CreateUsers) ||
                                  AuthService.HasRight(_currentUser, UserRight.EditUsers) ||
                                  AuthService.HasRight(_currentUser, UserRight.DeleteUsers);
        menuManageRoles.Enabled = AuthService.HasRight(_currentUser, UserRight.EditRoles);

        // populate user dropdown
        var users = AuthService.GetUsers().OrderBy(u => u.UserName, StringComparer.OrdinalIgnoreCase).ToList();
        cboUser.DataSource = users;
        cboUser.DisplayMember = "UserName";
        cboUser.ValueMember = "UserName"; // just use username
        // try to default to the current user if it's in the list
        if (_currentUser != null)
        {
            var match = users.FirstOrDefault(u => u.UserName == _currentUser.UserName);
            if (match != null)
            {
                cboUser.SelectedItem = match;
            }
            else
            {
                cboUser.SelectedIndex = users.Count > 0 ? 0 : -1;
            }
        }
        else
        {
            cboUser.SelectedIndex = users.Count > 0 ? 0 : -1;
        }
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        var machineId = txtMachineId.Text.Trim();
        if (machineId.Length == 0)
        {
            MessageBox.Show(this, "Machine ID is required.", "License", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var expiresUtc = DateTime.UtcNow.AddDays((double)numDays.Value);
        var generatedFor = cboUser.SelectedItem is UserAccount ua ? ua.UserName : null;

        try
        {
            using var conn = Database.CreateConnection();
            using var cmd = new MySqlCommand(
                "INSERT INTO licenses (version, generated_for, machine_id, expires_utc) " +
                "VALUES (@version, @generated_for, @machine_id, @expires_utc)",
                conn);

            cmd.Parameters.AddWithValue("@version", "1");
            cmd.Parameters.AddWithValue("@generated_for", (object?)generatedFor ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@machine_id", machineId);
            cmd.Parameters.AddWithValue("@expires_utc", expiresUtc);

            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                "Failed to create license in database:\r\n\r\n" + ex.Message,
                "License error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        var userText = generatedFor ?? "(none)";
        MessageBox.Show(
            this,
            $"License created in database.\r\n\r\nUser:\r\n{userText}\r\n\r\nMachine ID:\r\n{machineId}\r\n\r\nExpires (UTC):\r\n{expiresUtc:O}",
            "License created",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        DialogResult = DialogResult.OK;
        Close();
    }

    private void menuCreateLicense_Click(object sender, EventArgs e)
    {
        // already on license screen; nothing to do
    }

    private void menuManageUsers_Click(object sender, EventArgs e)
    {
        if (!menuManageUsers.Enabled)
            return;

        using var dlg = new UserManagementForm(_currentUser);
        var result = dlg.ShowDialog(this);
        if (result == DialogResult.Retry)
        {
            DialogResult = DialogResult.Retry;
            Close();
        }
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

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Retry;
        Close();
    }
}

