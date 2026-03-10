namespace PGen;

partial class RoleManagementForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lstRoles = new ListBox();
        pnlRoleDetails = new Panel();
        grpRights = new GroupBox();
        grpFormRights = new GroupBox();
        chkUserManagement = new CheckBox();
        chkRoleManagement = new CheckBox();
        chkPasswordGeneration = new CheckBox();
        chkManageLicenses = new CheckBox();
        grpRoleRights = new GroupBox();
        chkDeleteRoles = new CheckBox();
        chkEditRoles = new CheckBox();
        chkCreateRoles = new CheckBox();
        chkViewRoles = new CheckBox();
        grpUserRights = new GroupBox();
        chkDeleteUsers = new CheckBox();
        chkEditUsers = new CheckBox();
        chkCreateUsers = new CheckBox();
        chkViewUsers = new CheckBox();
        lblDescription = new Label();
        txtDescription = new TextBox();
        lblRoleName = new Label();
        txtRoleName = new TextBox();
        lblRoleId = new Label();
        txtRoleId = new TextBox();
        btnUpdateRole = new Button();
        btnDeleteRole = new Button();
        btnNewRole = new Button();
        btnLogout = new Button();
        pnlNewRole = new Panel();
        lblNewRoleStatus = new Label();
        btnCancelNewRole = new Button();
        btnCreateRole = new Button();
        txtNewRoleDesc = new TextBox();
        lblNewRoleDesc = new Label();
        txtNewRoleName = new TextBox();
        lblNewRoleName = new Label();
        menuStrip = new MenuStrip();
        menuAdmin = new ToolStripMenuItem();
        menuCreateLicense = new ToolStripMenuItem();
        menuManageUsers = new ToolStripMenuItem();
        menuManageRoles = new ToolStripMenuItem();
        pnlRoleDetails.SuspendLayout();
        grpRights.SuspendLayout();
        grpFormRights.SuspendLayout();
        grpRoleRights.SuspendLayout();
        grpUserRights.SuspendLayout();
        pnlNewRole.SuspendLayout();
        menuStrip.SuspendLayout();
        SuspendLayout();
        // 
        // lstRoles
        // 
        lstRoles.Dock = DockStyle.Left;
        lstRoles.FormattingEnabled = true;
        lstRoles.Location = new Point(0, 0);
        lstRoles.Margin = new Padding(3, 4, 3, 4);
        lstRoles.Name = "lstRoles";
        lstRoles.Size = new Size(228, 807);
        lstRoles.TabIndex = 0;
        lstRoles.SelectedIndexChanged += lstRoles_SelectedIndexChanged;
        // 
        // pnlRoleDetails
        // 
        pnlRoleDetails.Controls.Add(grpRights);
        pnlRoleDetails.Controls.Add(lblDescription);
        pnlRoleDetails.Controls.Add(txtDescription);
        pnlRoleDetails.Controls.Add(lblRoleName);
        pnlRoleDetails.Controls.Add(txtRoleName);
        pnlRoleDetails.Controls.Add(lblRoleId);
        pnlRoleDetails.Controls.Add(txtRoleId);
        pnlRoleDetails.Controls.Add(btnUpdateRole);
        pnlRoleDetails.Controls.Add(btnDeleteRole);
        pnlRoleDetails.Controls.Add(btnNewRole);
        pnlRoleDetails.Dock = DockStyle.Fill;
        pnlRoleDetails.Location = new Point(228, 0);
        pnlRoleDetails.Margin = new Padding(3, 4, 3, 4);
        pnlRoleDetails.Name = "pnlRoleDetails";
        pnlRoleDetails.Padding = new Padding(11, 13, 11, 13);
        pnlRoleDetails.Size = new Size(686, 807);
        pnlRoleDetails.TabIndex = 1;
        // 
        // grpRights
        // 
        grpRights.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpRights.Controls.Add(grpFormRights);
        grpRights.Controls.Add(grpRoleRights);
        grpRights.Controls.Add(pnlNewRole);
        grpRights.Controls.Add(grpUserRights);
        grpRights.Location = new Point(15, 194);
        grpRights.Margin = new Padding(3, 4, 3, 4);
        grpRights.Name = "grpRights";
        grpRights.Padding = new Padding(6, 7, 6, 7);
        grpRights.Size = new Size(656, 499);
        grpRights.TabIndex = 7;
        grpRights.TabStop = false;
        grpRights.Text = "Assign Rights";
        // 
        // grpFormRights
        // 
        grpFormRights.Controls.Add(chkUserManagement);
        grpFormRights.Controls.Add(chkRoleManagement);
        grpFormRights.Controls.Add(chkPasswordGeneration);
        grpFormRights.Controls.Add(chkManageLicenses);
        grpFormRights.Location = new Point(6, 33);
        grpFormRights.Margin = new Padding(3, 4, 3, 4);
        grpFormRights.Name = "grpFormRights";
        grpFormRights.Padding = new Padding(3, 4, 3, 4);
        grpFormRights.Size = new Size(309, 160);
        grpFormRights.TabIndex = 5;
        grpFormRights.TabStop = false;
        grpFormRights.Text = "Forms";
        // 
        // chkUserManagement
        // 
        chkUserManagement.AutoSize = true;
        chkUserManagement.Location = new Point(7, 25);
        chkUserManagement.Margin = new Padding(3, 4, 3, 4);
        chkUserManagement.Name = "chkUserManagement";
        chkUserManagement.Size = new Size(152, 24);
        chkUserManagement.TabIndex = 0;
        chkUserManagement.Text = "User Management";
        chkUserManagement.UseVisualStyleBackColor = true;
        // 
        // chkRoleManagement
        // 
        chkRoleManagement.AutoSize = true;
        chkRoleManagement.Location = new Point(7, 51);
        chkRoleManagement.Margin = new Padding(3, 4, 3, 4);
        chkRoleManagement.Name = "chkRoleManagement";
        chkRoleManagement.Size = new Size(153, 24);
        chkRoleManagement.TabIndex = 1;
        chkRoleManagement.Text = "Role Management";
        chkRoleManagement.UseVisualStyleBackColor = true;
        // 
        // chkPasswordGeneration
        // 
        chkPasswordGeneration.AutoSize = true;
        chkPasswordGeneration.Location = new Point(7, 76);
        chkPasswordGeneration.Margin = new Padding(3, 4, 3, 4);
        chkPasswordGeneration.Name = "chkPasswordGeneration";
        chkPasswordGeneration.Size = new Size(169, 24);
        chkPasswordGeneration.TabIndex = 2;
        chkPasswordGeneration.Text = "Password Generation";
        chkPasswordGeneration.UseVisualStyleBackColor = true;
        // 
        // chkManageLicenses
        // 
        chkManageLicenses.AutoSize = true;
        chkManageLicenses.Location = new Point(7, 101);
        chkManageLicenses.Margin = new Padding(3, 4, 3, 4);
        chkManageLicenses.Name = "chkManageLicenses";
        chkManageLicenses.Size = new Size(143, 24);
        chkManageLicenses.TabIndex = 3;
        chkManageLicenses.Text = "Manage Licenses";
        chkManageLicenses.UseVisualStyleBackColor = true;
        // 
        // grpRoleRights
        // 
        grpRoleRights.Controls.Add(chkDeleteRoles);
        grpRoleRights.Controls.Add(chkEditRoles);
        grpRoleRights.Controls.Add(chkCreateRoles);
        grpRoleRights.Controls.Add(chkViewRoles);
        grpRoleRights.Location = new Point(320, 33);
        grpRoleRights.Margin = new Padding(3, 4, 3, 4);
        grpRoleRights.Name = "grpRoleRights";
        grpRoleRights.Padding = new Padding(3, 4, 3, 4);
        grpRoleRights.Size = new Size(309, 107);
        grpRoleRights.TabIndex = 1;
        grpRoleRights.TabStop = false;
        grpRoleRights.Text = "Role Management";
        grpRoleRights.Visible = false;
        // 
        // chkDeleteRoles
        // 
        chkDeleteRoles.AutoSize = true;
        chkDeleteRoles.Location = new Point(137, 51);
        chkDeleteRoles.Margin = new Padding(3, 4, 3, 4);
        chkDeleteRoles.Name = "chkDeleteRoles";
        chkDeleteRoles.Size = new Size(115, 24);
        chkDeleteRoles.TabIndex = 3;
        chkDeleteRoles.Text = "Delete Roles";
        chkDeleteRoles.UseVisualStyleBackColor = true;
        chkDeleteRoles.Visible = false;
        // 
        // chkEditRoles
        // 
        chkEditRoles.AutoSize = true;
        chkEditRoles.Location = new Point(137, 25);
        chkEditRoles.Margin = new Padding(3, 4, 3, 4);
        chkEditRoles.Name = "chkEditRoles";
        chkEditRoles.Size = new Size(97, 24);
        chkEditRoles.TabIndex = 2;
        chkEditRoles.Text = "Edit Roles";
        chkEditRoles.UseVisualStyleBackColor = true;
        chkEditRoles.Visible = false;
        // 
        // chkCreateRoles
        // 
        chkCreateRoles.AutoSize = true;
        chkCreateRoles.Location = new Point(7, 51);
        chkCreateRoles.Margin = new Padding(3, 4, 3, 4);
        chkCreateRoles.Name = "chkCreateRoles";
        chkCreateRoles.Size = new Size(114, 24);
        chkCreateRoles.TabIndex = 1;
        chkCreateRoles.Text = "Create Roles";
        chkCreateRoles.UseVisualStyleBackColor = true;
        chkCreateRoles.Visible = false;
        // 
        // chkViewRoles
        // 
        chkViewRoles.AutoSize = true;
        chkViewRoles.Location = new Point(7, 25);
        chkViewRoles.Margin = new Padding(3, 4, 3, 4);
        chkViewRoles.Name = "chkViewRoles";
        chkViewRoles.Size = new Size(103, 24);
        chkViewRoles.TabIndex = 0;
        chkViewRoles.Text = "View Roles";
        chkViewRoles.UseVisualStyleBackColor = true;
        chkViewRoles.Visible = false;
        // 
        // grpUserRights
        // 
        grpUserRights.Controls.Add(chkDeleteUsers);
        grpUserRights.Controls.Add(chkEditUsers);
        grpUserRights.Controls.Add(chkCreateUsers);
        grpUserRights.Controls.Add(chkViewUsers);
        grpUserRights.Location = new Point(6, 33);
        grpUserRights.Margin = new Padding(3, 4, 3, 4);
        grpUserRights.Name = "grpUserRights";
        grpUserRights.Padding = new Padding(3, 4, 3, 4);
        grpUserRights.Size = new Size(309, 107);
        grpUserRights.TabIndex = 0;
        grpUserRights.TabStop = false;
        grpUserRights.Text = "User Management";
        grpUserRights.Visible = false;
        // 
        // chkDeleteUsers
        // 
        chkDeleteUsers.AutoSize = true;
        chkDeleteUsers.Location = new Point(137, 51);
        chkDeleteUsers.Margin = new Padding(3, 4, 3, 4);
        chkDeleteUsers.Name = "chkDeleteUsers";
        chkDeleteUsers.Size = new Size(114, 24);
        chkDeleteUsers.TabIndex = 3;
        chkDeleteUsers.Text = "Delete Users";
        chkDeleteUsers.UseVisualStyleBackColor = true;
        chkDeleteUsers.Visible = false;
        // 
        // chkEditUsers
        // 
        chkEditUsers.AutoSize = true;
        chkEditUsers.Location = new Point(137, 25);
        chkEditUsers.Margin = new Padding(3, 4, 3, 4);
        chkEditUsers.Name = "chkEditUsers";
        chkEditUsers.Size = new Size(96, 24);
        chkEditUsers.TabIndex = 2;
        chkEditUsers.Text = "Edit Users";
        chkEditUsers.UseVisualStyleBackColor = true;
        chkEditUsers.Visible = false;
        // 
        // chkCreateUsers
        // 
        chkCreateUsers.AutoSize = true;
        chkCreateUsers.Location = new Point(7, 51);
        chkCreateUsers.Margin = new Padding(3, 4, 3, 4);
        chkCreateUsers.Name = "chkCreateUsers";
        chkCreateUsers.Size = new Size(113, 24);
        chkCreateUsers.TabIndex = 1;
        chkCreateUsers.Text = "Create Users";
        chkCreateUsers.UseVisualStyleBackColor = true;
        chkCreateUsers.Visible = false;
        // 
        // chkViewUsers
        // 
        chkViewUsers.AutoSize = true;
        chkViewUsers.Location = new Point(7, 25);
        chkViewUsers.Margin = new Padding(3, 4, 3, 4);
        chkViewUsers.Name = "chkViewUsers";
        chkViewUsers.Size = new Size(102, 24);
        chkViewUsers.TabIndex = 0;
        chkViewUsers.Text = "View Users";
        chkViewUsers.UseVisualStyleBackColor = true;
        chkViewUsers.Visible = false;
        // 
        // lblDescription
        // 
        lblDescription.AutoSize = true;
        lblDescription.Location = new Point(15, 146);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new Size(88, 20);
        lblDescription.TabIndex = 4;
        lblDescription.Text = "Description:";
        // 
        // txtDescription
        // 
        txtDescription.Location = new Point(109, 142);
        txtDescription.Margin = new Padding(3, 4, 3, 4);
        txtDescription.Multiline = true;
        txtDescription.Name = "txtDescription";
        txtDescription.Size = new Size(486, 44);
        txtDescription.TabIndex = 5;
        // 
        // lblRoleName
        // 
        lblRoleName.AutoSize = true;
        lblRoleName.Location = new Point(15, 106);
        lblRoleName.Name = "lblRoleName";
        lblRoleName.Size = new Size(86, 20);
        lblRoleName.TabIndex = 2;
        lblRoleName.Text = "Role Name:";
        // 
        // txtRoleName
        // 
        txtRoleName.Location = new Point(109, 102);
        txtRoleName.Margin = new Padding(3, 4, 3, 4);
        txtRoleName.Name = "txtRoleName";
        txtRoleName.Size = new Size(486, 27);
        txtRoleName.TabIndex = 3;
        // 
        // lblRoleId
        // 
        lblRoleId.AutoSize = true;
        lblRoleId.Location = new Point(15, 66);
        lblRoleId.Name = "lblRoleId";
        lblRoleId.Size = new Size(61, 20);
        lblRoleId.TabIndex = 0;
        lblRoleId.Text = "Role ID:";
        // 
        // txtRoleId
        // 
        txtRoleId.Enabled = false;
        txtRoleId.Location = new Point(109, 62);
        txtRoleId.Margin = new Padding(3, 4, 3, 4);
        txtRoleId.Name = "txtRoleId";
        txtRoleId.Size = new Size(486, 27);
        txtRoleId.TabIndex = 1;
        // 
        // btnUpdateRole
        // 
        btnUpdateRole.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnUpdateRole.Location = new Point(496, 767);
        btnUpdateRole.Margin = new Padding(3, 4, 3, 4);
        btnUpdateRole.Name = "btnUpdateRole";
        btnUpdateRole.Size = new Size(86, 31);
        btnUpdateRole.TabIndex = 8;
        btnUpdateRole.Text = "Update";
        btnUpdateRole.UseVisualStyleBackColor = true;
        btnUpdateRole.Click += btnUpdateRole_Click;
        // 
        // btnDeleteRole
        // 
        btnDeleteRole.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnDeleteRole.Location = new Point(589, 767);
        btnDeleteRole.Margin = new Padding(3, 4, 3, 4);
        btnDeleteRole.Name = "btnDeleteRole";
        btnDeleteRole.Size = new Size(86, 31);
        btnDeleteRole.TabIndex = 9;
        btnDeleteRole.Text = "Delete";
        btnDeleteRole.UseVisualStyleBackColor = true;
        btnDeleteRole.Click += btnDeleteRole_Click;
        // 
        // btnNewRole
        // 
        btnNewRole.Location = new Point(15, 700);
        btnNewRole.Margin = new Padding(3, 4, 3, 4);
        btnNewRole.Name = "btnNewRole";
        btnNewRole.Size = new Size(114, 31);
        btnNewRole.TabIndex = 7;
        btnNewRole.Text = "New Role";
        btnNewRole.UseVisualStyleBackColor = true;
        btnNewRole.Click += btnNewRole_Click;
        // 
        // btnLogout
        // 
        btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLogout.Location = new Point(829, 32);
        btnLogout.Margin = new Padding(3, 4, 3, 4);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(86, 31);
        btnLogout.TabIndex = 8;
        btnLogout.Text = "Logout";
        btnLogout.UseVisualStyleBackColor = true;
        btnLogout.Click += btnLogout_Click;
        // 
        // pnlNewRole
        // 
        pnlNewRole.BackColor = SystemColors.Window;
        pnlNewRole.BorderStyle = BorderStyle.FixedSingle;
        pnlNewRole.Controls.Add(lblNewRoleStatus);
        pnlNewRole.Controls.Add(btnCancelNewRole);
        pnlNewRole.Controls.Add(btnCreateRole);
        pnlNewRole.Controls.Add(txtNewRoleDesc);
        pnlNewRole.Controls.Add(lblNewRoleDesc);
        pnlNewRole.Controls.Add(txtNewRoleName);
        pnlNewRole.Controls.Add(lblNewRoleName);
        pnlNewRole.Location = new Point(107, 201);
        pnlNewRole.Margin = new Padding(3, 4, 3, 4);
        pnlNewRole.Name = "pnlNewRole";
        pnlNewRole.Padding = new Padding(11, 13, 11, 13);
        pnlNewRole.Size = new Size(400, 226);
        pnlNewRole.TabIndex = 10;
        pnlNewRole.Visible = false;
        // 
        // lblNewRoleStatus
        // 
        lblNewRoleStatus.AutoSize = true;
        lblNewRoleStatus.Location = new Point(11, 127);
        lblNewRoleStatus.Name = "lblNewRoleStatus";
        lblNewRoleStatus.Size = new Size(0, 20);
        lblNewRoleStatus.TabIndex = 4;
        // 
        // btnCancelNewRole
        // 
        btnCancelNewRole.Location = new Point(320, 127);
        btnCancelNewRole.Margin = new Padding(3, 4, 3, 4);
        btnCancelNewRole.Name = "btnCancelNewRole";
        btnCancelNewRole.Size = new Size(69, 31);
        btnCancelNewRole.TabIndex = 6;
        btnCancelNewRole.Text = "Cancel";
        btnCancelNewRole.UseVisualStyleBackColor = true;
        btnCancelNewRole.Click += btnCancelNewRole_Click;
        // 
        // btnCreateRole
        // 
        btnCreateRole.Location = new Point(246, 127);
        btnCreateRole.Margin = new Padding(3, 4, 3, 4);
        btnCreateRole.Name = "btnCreateRole";
        btnCreateRole.Size = new Size(69, 31);
        btnCreateRole.TabIndex = 5;
        btnCreateRole.Text = "Create";
        btnCreateRole.UseVisualStyleBackColor = true;
        btnCreateRole.Click += btnCreateRole_Click;
        // 
        // txtNewRoleDesc
        // 
        txtNewRoleDesc.Location = new Point(97, 53);
        txtNewRoleDesc.Margin = new Padding(3, 4, 3, 4);
        txtNewRoleDesc.Multiline = true;
        txtNewRoleDesc.Name = "txtNewRoleDesc";
        txtNewRoleDesc.Size = new Size(291, 65);
        txtNewRoleDesc.TabIndex = 3;
        // 
        // lblNewRoleDesc
        // 
        lblNewRoleDesc.AutoSize = true;
        lblNewRoleDesc.Location = new Point(11, 53);
        lblNewRoleDesc.Name = "lblNewRoleDesc";
        lblNewRoleDesc.Size = new Size(88, 20);
        lblNewRoleDesc.TabIndex = 2;
        lblNewRoleDesc.Text = "Description:";
        // 
        // txtNewRoleName
        // 
        txtNewRoleName.Location = new Point(97, 13);
        txtNewRoleName.Margin = new Padding(3, 4, 3, 4);
        txtNewRoleName.Name = "txtNewRoleName";
        txtNewRoleName.Size = new Size(291, 27);
        txtNewRoleName.TabIndex = 1;
        // 
        // lblNewRoleName
        // 
        lblNewRoleName.AutoSize = true;
        lblNewRoleName.Location = new Point(11, 13);
        lblNewRoleName.Name = "lblNewRoleName";
        lblNewRoleName.Size = new Size(86, 20);
        lblNewRoleName.TabIndex = 0;
        lblNewRoleName.Text = "Role Name:";
        // 
        // menuStrip
        // 
        menuStrip.ImageScalingSize = new Size(20, 20);
        menuStrip.Items.AddRange(new ToolStripItem[] { menuAdmin });
        menuStrip.Location = new Point(228, 0);
        menuStrip.Name = "menuStrip";
        menuStrip.Size = new Size(686, 28);
        menuStrip.TabIndex = 11;
        menuStrip.Text = "menuStrip1";
        // 
        // menuAdmin
        // 
        menuAdmin.DropDownItems.AddRange(new ToolStripItem[] { menuCreateLicense, menuManageUsers, menuManageRoles });
        menuAdmin.Name = "menuAdmin";
        menuAdmin.Size = new Size(67, 24);
        menuAdmin.Text = "Admin";
        // 
        // menuCreateLicense
        // 
        menuCreateLicense.Name = "menuCreateLicense";
        menuCreateLicense.Size = new Size(247, 26);
        menuCreateLicense.Text = "Create Machine License";
        menuCreateLicense.Click += menuCreateLicense_Click;
        // 
        // menuManageUsers
        // 
        menuManageUsers.Name = "menuManageUsers";
        menuManageUsers.Size = new Size(247, 26);
        menuManageUsers.Text = "Manage Users";
        menuManageUsers.Click += menuManageUsers_Click;
        // 
        // menuManageRoles
        // 
        menuManageRoles.Name = "menuManageRoles";
        menuManageRoles.Size = new Size(247, 26);
        menuManageRoles.Text = "Manage Roles";
        menuManageRoles.Click += menuManageRoles_Click;
        // 
        // RoleManagementForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(914, 807);
        Controls.Add(menuStrip);
        Controls.Add(btnLogout);
        Controls.Add(pnlRoleDetails);
        Controls.Add(lstRoles);
        Margin = new Padding(3, 4, 3, 4);
        Name = "RoleManagementForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Role Management";
        Load += RoleManagementForm_Load;
        pnlRoleDetails.ResumeLayout(false);
        pnlRoleDetails.PerformLayout();
        grpRights.ResumeLayout(false);
        grpFormRights.ResumeLayout(false);
        grpFormRights.PerformLayout();
        grpRoleRights.ResumeLayout(false);
        grpRoleRights.PerformLayout();
        grpUserRights.ResumeLayout(false);
        grpUserRights.PerformLayout();
        pnlNewRole.ResumeLayout(false);
        pnlNewRole.PerformLayout();
        menuStrip.ResumeLayout(false);
        menuStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private ListBox lstRoles;
    private Panel pnlRoleDetails;
    private GroupBox grpRights;
    private GroupBox grpUserRights;
    private CheckBox chkDeleteUsers;
    private CheckBox chkEditUsers;
    private CheckBox chkCreateUsers;
    private CheckBox chkViewUsers;
    private GroupBox grpFormRights;
    private CheckBox chkUserManagement;
    private CheckBox chkRoleManagement;
    private CheckBox chkPasswordGeneration;
    private CheckBox chkManageLicenses;

    private GroupBox grpRoleRights;
    private CheckBox chkDeleteRoles;
    private CheckBox chkEditRoles;
    private CheckBox chkCreateRoles;
    private CheckBox chkViewRoles;
    private Label lblDescription;
    private TextBox txtDescription;
    private Label lblRoleName;
    private TextBox txtRoleName;
    private Label lblRoleId;
    private TextBox txtRoleId;
    private Button btnUpdateRole;
    private Button btnDeleteRole;
    private Button btnNewRole;
    private Button btnLogout;
    private Panel pnlNewRole;
    private Label lblNewRoleStatus;
    private Button btnCancelNewRole;
    private Button btnCreateRole;
    private TextBox txtNewRoleDesc;
    private Label lblNewRoleDesc;
    private TextBox txtNewRoleName;
    private Label lblNewRoleName;
    private MenuStrip menuStrip;
    private ToolStripMenuItem menuAdmin;
    private ToolStripMenuItem menuCreateLicense;
    private ToolStripMenuItem menuManageUsers;
    private ToolStripMenuItem menuManageRoles;
}
