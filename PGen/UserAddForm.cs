using System.Runtime.Versioning;
using System.Windows.Forms;
using PGen.Auth;

namespace PGen;

[SupportedOSPlatform("windows")]
public partial class UserAddForm : Form
{
    private List<Role> _roles = new();
    private bool _isEdit;

    public string UserNameValue => txtUser.Text.Trim();
    public string PasswordValue => txtPassword.Text;
    public string RoleIdValue => (cboRole.SelectedItem as Role)?.Id ?? string.Empty;

    public UserAddForm(UserAccount? existing = null)
    {
        InitializeComponent();
        LoadRoles();
        if (existing != null)
        {
            _isEdit = true;
            Text = "Edit User";
            txtUser.Text = existing.UserName;
            txtUser.ReadOnly = true;
            // select current role
            var role = _roles.FirstOrDefault(r => r.Id == existing.RoleId);
            if (role != null)
                cboRole.SelectedItem = role;
            // hide password controls when editing
            txtPassword.Visible = false;
            foreach (Control c in Controls)
            {
                if (c is Label lbl && lbl.Text.Contains("Password"))
                    lbl.Visible = false;
            }
        }
    }

    private void LoadRoles()
    {
        _roles = RoleService.GetRoles().ToList();
        cboRole.DataSource = _roles;
        cboRole.DisplayMember = "Name";
        cboRole.ValueMember = "Id";
        
        // Select "Operator" role by default
        var operatorRole = _roles.FirstOrDefault(r => string.Equals(r.Name, "Operator", StringComparison.OrdinalIgnoreCase));
        if (operatorRole != null)
            cboRole.SelectedItem = operatorRole;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        if (UserNameValue.Length == 0)
        {
            MessageBox.Show(this, "User name is required.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!_isEdit && PasswordValue.Length == 0)
        {
            MessageBox.Show(this, "Password is required.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
