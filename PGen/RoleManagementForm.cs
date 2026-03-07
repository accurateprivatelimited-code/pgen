using System.Runtime.Versioning;
using System.Windows.Forms;
using PGen.Auth;

namespace PGen;

[SupportedOSPlatform("windows")]
public partial class RoleManagementForm : Form
{
    private readonly UserAccount _currentUser;
    private Role? _selectedRole;

    public RoleManagementForm(UserAccount currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();
        // ensure checkboxes for rights the current user does not possess are disabled
        var hasEditRoles = AuthService.HasRight(_currentUser, UserRight.EditRoles);
        // coarse form-level checkboxes should follow the same enabled state
        chkUserManagement.Enabled = hasEditRoles;
        chkRoleManagement.Enabled = hasEditRoles;
        // keep granular controls disabled as well (they are hidden)
        chkViewUsers.Enabled = hasEditRoles;
        chkCreateUsers.Enabled = hasEditRoles;
        chkEditUsers.Enabled = hasEditRoles;
        chkDeleteUsers.Enabled = hasEditRoles;
        chkViewRoles.Enabled = hasEditRoles;
        chkCreateRoles.Enabled = hasEditRoles;
        chkEditRoles.Enabled = hasEditRoles;
        chkDeleteRoles.Enabled = hasEditRoles;
        chkPasswordGeneration.Enabled = hasEditRoles;
        chkManageLicenses.Enabled = hasEditRoles;
    }

    private void RoleManagementForm_Load(object sender, EventArgs e)
    {
        RefreshRolesList();
    }

    private void RefreshRolesList()
    {
        lstRoles.Items.Clear();
        var roles = RoleService.GetRoles();
        foreach (var role in roles)
        {
            lstRoles.Items.Add(role);
        }
    }

    private void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstRoles.SelectedItem is Role role)
        {
            _selectedRole = role;
            DisplayRoleDetails(role);
        }
        else
        {
            _selectedRole = null;
            ClearRoleDetails();
        }
    }

    private void DisplayRoleDetails(Role role)
    {
        txtRoleId.Text = role.Id;
        txtRoleName.Text = role.Name;
        txtDescription.Text = role.Description ?? string.Empty;
        UpdateRightsCheckboxes(role.Rights);
    }

    private void ClearRoleDetails()
    {
        txtRoleId.Clear();
        txtRoleName.Clear();
        txtDescription.Clear();
        ClearRightsCheckboxes();
    }

    private void UpdateRightsCheckboxes(UserRight rights)
    {
        // form-level rights map to underlying granular flags
        // only consider create/edit/delete when deciding if a role grants form access
        chkUserManagement.Checked = (rights & (UserRight.CreateUsers | UserRight.EditUsers | UserRight.DeleteUsers)) != 0;
        chkRoleManagement.Checked = (rights & (UserRight.CreateRoles | UserRight.EditRoles | UserRight.DeleteRoles)) != 0;


        // keep underlying checkboxes in sync (they are hidden)
        chkViewUsers.Checked = (rights & UserRight.ViewUsers) == UserRight.ViewUsers;
        chkCreateUsers.Checked = (rights & UserRight.CreateUsers) == UserRight.CreateUsers;
        chkEditUsers.Checked = (rights & UserRight.EditUsers) == UserRight.EditUsers;
        chkDeleteUsers.Checked = (rights & UserRight.DeleteUsers) == UserRight.DeleteUsers;

        chkViewRoles.Checked = (rights & UserRight.ViewRoles) == UserRight.ViewRoles;
        chkCreateRoles.Checked = (rights & UserRight.CreateRoles) == UserRight.CreateRoles;
        chkEditRoles.Checked = (rights & UserRight.EditRoles) == UserRight.EditRoles;
        chkDeleteRoles.Checked = (rights & UserRight.DeleteRoles) == UserRight.DeleteRoles;

        chkPasswordGeneration.Checked = (rights & UserRight.GeneratePasswords) == UserRight.GeneratePasswords;
        chkManageLicenses.Checked = (rights & UserRight.ManageLicenses) == UserRight.ManageLicenses;
    }

    private void ClearRightsCheckboxes()
    {
        chkUserManagement.Checked = false;
        chkRoleManagement.Checked = false;
        chkPasswordGeneration.Checked = false;
        chkManageLicenses.Checked = false;

        // also clear underlying controls for completeness
        chkViewUsers.Checked = false;
        chkCreateUsers.Checked = false;
        chkEditUsers.Checked = false;
        chkDeleteUsers.Checked = false;

        chkViewRoles.Checked = false;
        chkCreateRoles.Checked = false;
        chkEditRoles.Checked = false;
        chkDeleteRoles.Checked = false;
    }

    private UserRight GetSelectedRights()
    {
        UserRight rights = UserRight.None;

        if (chkUserManagement.Checked)
            rights |= UserRight.ViewUsers | UserRight.CreateUsers | UserRight.EditUsers | UserRight.DeleteUsers;
        if (chkRoleManagement.Checked)
            rights |= UserRight.ViewRoles | UserRight.CreateRoles | UserRight.EditRoles | UserRight.DeleteRoles;
        if (chkPasswordGeneration.Checked)
            rights |= UserRight.GeneratePasswords;
        if (chkManageLicenses.Checked)
            rights |= UserRight.ManageLicenses;

        return rights;
    }

    private void btnNewRole_Click(object sender, EventArgs e)
    {

        lblNewRoleStatus.Text = string.Empty;
        txtNewRoleName.Clear();
        txtNewRoleDesc.Clear();
        // clear rights so new role starts with a clean slate (otherwise the
        // checkbox state might reflect the currently selected role)
        ClearRightsCheckboxes();
        pnlNewRole.Visible = true;
        txtNewRoleName.Focus();
    }

    private void btnCreateRole_Click(object sender, EventArgs e)
    {
        var roleName = txtNewRoleName.Text.Trim();
        if (roleName.Length == 0)
        {
            lblNewRoleStatus.Text = "Role name is required.";
            lblNewRoleStatus.ForeColor = Color.Red;
            return;
        }

        try
        {
            // when creating a new role we allow the current rights checkbox state
            // to serve as the initial rights set.  this makes it possible to define
            // a role and its permissions in one step rather than having to create it
            // first and then edit it.
            var rights = GetSelectedRights();
            RoleService.CreateRole(roleName, rights, txtNewRoleDesc.Text.Trim());
            lblNewRoleStatus.Text = "Role created successfully.";
            lblNewRoleStatus.ForeColor = Color.Green;
            RefreshRolesList();
            pnlNewRole.Visible = false;
        }
        catch (Exception ex)
        {
            lblNewRoleStatus.Text = $"Error: {ex.Message}";
            lblNewRoleStatus.ForeColor = Color.Red;
        }
    }

    private void btnCancelNewRole_Click(object sender, EventArgs e)
    {
        pnlNewRole.Visible = false;
    }

    private void btnUpdateRole_Click(object sender, EventArgs e)
    {

        if (_selectedRole is null)
        {
            MessageBox.Show(this, "Please select a role to update.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var rights = GetSelectedRights();
            RoleService.UpdateRole(_selectedRole.Id, txtRoleName.Text.Trim(), rights, txtDescription.Text.Trim());
            MessageBox.Show(this, "Role updated successfully.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshRolesList();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Error: {ex.Message}", "Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnDeleteRole_Click(object sender, EventArgs e)
    {

        if (_selectedRole is null)
        {
            MessageBox.Show(this, "Please select a role to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (MessageBox.Show(this, $"Are you sure you want to delete the role '{_selectedRole.Name}'?",
            "Delete Role", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            RoleService.DeleteRole(_selectedRole.Id);
            MessageBox.Show(this, "Role deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshRolesList();
            ClearRoleDetails();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Error: {ex.Message}", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
