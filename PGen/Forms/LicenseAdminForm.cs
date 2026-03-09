using System.Runtime.Versioning;
using System.Windows.Forms;
using System.Linq;
using PGen.Auth;
using PGen.Security;

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

        using var sfd = new SaveFileDialog
        {
            Filter = "License file (license.bin)|license.bin|All files (*.*)|*.*",
            FileName = "license.bin",
            OverwritePrompt = true
        };

        if (sfd.ShowDialog(this) != DialogResult.OK)
            return;

        var license = new LicenseFile
        {
            Version = "1",
            AllowedMachineIds = new[] { machineId },
            ExpiresUtc = DateTime.UtcNow.AddDays((double)numDays.Value),
            GeneratedFor = cboUser.SelectedItem is UserAccount ua ? ua.UserName : null
        };

        LicenseService.WriteLicense(sfd.FileName, license);

        var userText = cboUser.SelectedItem is UserAccount ua2 ? ua2.UserName : "(none)";
        MessageBox.Show(
            this,
            $"License written:\r\n{sfd.FileName}\r\n\r\nUser:\r\n{userText}\r\n\r\nMachine ID:\r\n{machineId}\r\n\r\nExpires (UTC):\r\n{license.ExpiresUtc:O}",
            "License created",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        DialogResult = DialogResult.OK;
        Close();
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

