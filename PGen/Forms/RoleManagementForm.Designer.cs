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
        // added form-level rights controls
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
        pnlRoleDetails.SuspendLayout();
        grpRights.SuspendLayout();
        grpRoleRights.SuspendLayout();
        grpUserRights.SuspendLayout();
        pnlNewRole.SuspendLayout();
        SuspendLayout();
        
        // lstRoles
        lstRoles.Dock = DockStyle.Left;
        lstRoles.FormattingEnabled = true;
        lstRoles.ItemHeight = 15;
        lstRoles.Location = new Point(0, 0);
        lstRoles.Name = "lstRoles";
        lstRoles.Size = new Size(200, 605);
        lstRoles.TabIndex = 0;
        lstRoles.SelectedIndexChanged += lstRoles_SelectedIndexChanged;
        
        // pnlRoleDetails
        pnlRoleDetails.Controls.Add(grpRights);
        pnlRoleDetails.Controls.Add(lblDescription);
        pnlRoleDetails.Controls.Add(txtDescription);
        pnlRoleDetails.Controls.Add(lblRoleName);
        pnlRoleDetails.Controls.Add(txtRoleName);
        pnlRoleDetails.Controls.Add(lblRoleId);
        pnlRoleDetails.Controls.Add(txtRoleId);
        pnlRoleDetails.Controls.Add(btnUpdateRole);
        pnlRoleDetails.Controls.Add(btnDeleteRole);
        pnlRoleDetails.Dock = DockStyle.Fill;
        pnlRoleDetails.Location = new Point(200, 0);
        pnlRoleDetails.Name = "pnlRoleDetails";
        pnlRoleDetails.Padding = new Padding(10);
        pnlRoleDetails.Size = new Size(600, 605);
        pnlRoleDetails.TabIndex = 1;
        
        // grpRights
        grpRights.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpRights.Controls.Add(grpFormRights);
        grpRights.Controls.Add(grpRoleRights);
        grpRights.Controls.Add(grpUserRights);
        grpRights.Location = new Point(13, 120);
        grpRights.Name = "grpRights";
        grpRights.Padding = new Padding(5);
        grpRights.Size = new Size(574, 400);
        grpRights.TabIndex = 7;
        grpRights.TabStop = false;
        grpRights.Text = "Assign Rights";
        // grpFormRights
        grpFormRights.Controls.Add(chkUserManagement);
        grpFormRights.Controls.Add(chkRoleManagement);
        grpFormRights.Controls.Add(chkPasswordGeneration);
        grpFormRights.Controls.Add(chkManageLicenses);
        grpFormRights.Location = new Point(5, 25);
        grpFormRights.Name = "grpFormRights";
        grpFormRights.Padding = new Padding(3);
        grpFormRights.Size = new Size(270, 120);
        grpFormRights.TabIndex = 5;
        grpFormRights.TabStop = false;
        grpFormRights.Text = "Forms";

        // chkUserManagement
        chkUserManagement.AutoSize = true;
        chkUserManagement.Location = new Point(6, 19);
        chkUserManagement.Name = "chkUserManagement";
        chkUserManagement.Size = new Size(112, 19);
        chkUserManagement.TabIndex = 0;
        chkUserManagement.Text = "User Management";
        chkUserManagement.UseVisualStyleBackColor = true;

        // chkRoleManagement
        chkRoleManagement.AutoSize = true;
        chkRoleManagement.Location = new Point(6, 38);
        chkRoleManagement.Name = "chkRoleManagement";
        chkRoleManagement.Size = new Size(112, 19);
        chkRoleManagement.TabIndex = 1;
        chkRoleManagement.Text = "Role Management";
        chkRoleManagement.UseVisualStyleBackColor = true;

        // chkPasswordGeneration
        chkPasswordGeneration.AutoSize = true;
        chkPasswordGeneration.Location = new Point(6, 57);
        chkPasswordGeneration.Name = "chkPasswordGeneration";
        chkPasswordGeneration.Size = new Size(130, 19);
        chkPasswordGeneration.TabIndex = 2;
        chkPasswordGeneration.Text = "Password Generation";
        chkPasswordGeneration.UseVisualStyleBackColor = true;

        // chkManageLicenses
        chkManageLicenses.AutoSize = true;
        chkManageLicenses.Location = new Point(6, 76);
        chkManageLicenses.Name = "chkManageLicenses";
        chkManageLicenses.Size = new Size(114, 19);
        chkManageLicenses.TabIndex = 3;
        chkManageLicenses.Text = "Manage Licenses";
        chkManageLicenses.UseVisualStyleBackColor = true;


        
        // grpUserRights
        // detailed user rights hidden by default
        grpUserRights.Controls.Add(chkDeleteUsers);
        grpUserRights.Controls.Add(chkEditUsers);
        grpUserRights.Controls.Add(chkCreateUsers);
        grpUserRights.Controls.Add(chkViewUsers);
        grpUserRights.Visible = false;
        grpUserRights.Location = new Point(5, 25);
        grpUserRights.Name = "grpUserRights";
        grpUserRights.Padding = new Padding(3);
        grpUserRights.Size = new Size(270, 80);
        grpUserRights.TabIndex = 0;
        grpUserRights.TabStop = false;
        grpUserRights.Text = "User Management";
        
        // placeholder controls, hidden
        chkViewUsers.AutoSize = true;
        chkViewUsers.Location = new Point(6, 19);
        chkViewUsers.Name = "chkViewUsers";
        chkViewUsers.Size = new Size(80, 19);
        chkViewUsers.TabIndex = 0;
        chkViewUsers.Text = "View Users";
        chkViewUsers.UseVisualStyleBackColor = true;
        chkViewUsers.Visible = false;
        
        chkCreateUsers.AutoSize = true;
        chkCreateUsers.Location = new Point(6, 38);
        chkCreateUsers.Name = "chkCreateUsers";
        chkCreateUsers.Size = new Size(93, 19);
        chkCreateUsers.TabIndex = 1;
        chkCreateUsers.Text = "Create Users";
        chkCreateUsers.UseVisualStyleBackColor = true;
        chkCreateUsers.Visible = false;
        
        chkEditUsers.AutoSize = true;
        chkEditUsers.Location = new Point(120, 19);
        chkEditUsers.Name = "chkEditUsers";
        chkEditUsers.Size = new Size(80, 19);
        chkEditUsers.TabIndex = 2;
        chkEditUsers.Text = "Edit Users";
        chkEditUsers.UseVisualStyleBackColor = true;
        chkEditUsers.Visible = false;
        
        chkDeleteUsers.AutoSize = true;
        chkDeleteUsers.Location = new Point(120, 38);
        chkDeleteUsers.Name = "chkDeleteUsers";
        chkDeleteUsers.Size = new Size(93, 19);
        chkDeleteUsers.TabIndex = 3;
        chkDeleteUsers.Text = "Delete Users";
        chkDeleteUsers.UseVisualStyleBackColor = true;
        chkDeleteUsers.Visible = false;
        
        // grpRoleRights (granular, hidden)
        grpRoleRights.Controls.Add(chkDeleteRoles);
        grpRoleRights.Controls.Add(chkEditRoles);
        grpRoleRights.Controls.Add(chkCreateRoles);
        grpRoleRights.Controls.Add(chkViewRoles);
        grpRoleRights.Location = new Point(280, 25);
        grpRoleRights.Visible = false;
        grpRoleRights.Name = "grpRoleRights";
        grpRoleRights.Padding = new Padding(3);
        grpRoleRights.Size = new Size(270, 80);
        grpRoleRights.TabIndex = 1;
        grpRoleRights.TabStop = false;
        grpRoleRights.Text = "Role Management";
        
        chkViewRoles.AutoSize = true;
        chkViewRoles.Location = new Point(6, 19);
        chkViewRoles.Name = "chkViewRoles";
        chkViewRoles.Size = new Size(83, 19);
        chkViewRoles.TabIndex = 0;
        chkViewRoles.Text = "View Roles";
        chkViewRoles.UseVisualStyleBackColor = true;
        chkViewRoles.Visible = false;
        
        chkCreateRoles.AutoSize = true;
        chkCreateRoles.Location = new Point(6, 38);
        chkCreateRoles.Name = "chkCreateRoles";
        chkCreateRoles.Size = new Size(96, 19);
        chkCreateRoles.TabIndex = 1;
        chkCreateRoles.Text = "Create Roles";
        chkCreateRoles.UseVisualStyleBackColor = true;
        chkCreateRoles.Visible = false;
        
        chkEditRoles.AutoSize = true;
        chkEditRoles.Location = new Point(120, 19);
        chkEditRoles.Name = "chkEditRoles";
        chkEditRoles.Size = new Size(83, 19);
        chkEditRoles.TabIndex = 2;
        chkEditRoles.Text = "Edit Roles";
        chkEditRoles.UseVisualStyleBackColor = true;
        chkEditRoles.Visible = false;
        
        chkDeleteRoles.AutoSize = true;
        chkDeleteRoles.Location = new Point(120, 38);
        chkDeleteRoles.Name = "chkDeleteRoles";
        chkDeleteRoles.Size = new Size(96, 19);
        chkDeleteRoles.TabIndex = 3;
        chkDeleteRoles.Text = "Delete Roles";
        chkDeleteRoles.UseVisualStyleBackColor = true;
        chkDeleteRoles.Visible = false;
        

        
        
        
        
        // lblRoleId
        lblRoleId.AutoSize = true;
        lblRoleId.Location = new Point(13, 15);
        lblRoleId.Name = "lblRoleId";
        lblRoleId.Size = new Size(49, 15);
        lblRoleId.TabIndex = 0;
        lblRoleId.Text = "Role ID:";
        
        txtRoleId.Enabled = false;
        txtRoleId.Location = new Point(70, 12);
        txtRoleId.Name = "txtRoleId";
        txtRoleId.Size = new Size(517, 23);
        txtRoleId.TabIndex = 1;
        
        // lblRoleName
        lblRoleName.AutoSize = true;
        lblRoleName.Location = new Point(13, 45);
        lblRoleName.Name = "lblRoleName";
        lblRoleName.Size = new Size(72, 15);
        lblRoleName.TabIndex = 2;
        lblRoleName.Text = "Role Name:";
        
        txtRoleName.Location = new Point(70, 42);
        txtRoleName.Name = "txtRoleName";
        txtRoleName.Size = new Size(517, 23);
        txtRoleName.TabIndex = 3;
        
        // lblDescription
        lblDescription.AutoSize = true;
        lblDescription.Location = new Point(13, 75);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new Size(70, 15);
        lblDescription.TabIndex = 4;
        lblDescription.Text = "Description:";
        
        txtDescription.Location = new Point(70, 72);
        txtDescription.Multiline = true;
        txtDescription.Name = "txtDescription";
        txtDescription.Size = new Size(517, 42);
        txtDescription.TabIndex = 5;
        
        // btnUpdateRole
        btnUpdateRole.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnUpdateRole.Location = new Point(434, 575);
        btnUpdateRole.Name = "btnUpdateRole";
        btnUpdateRole.Size = new Size(75, 23);
        btnUpdateRole.TabIndex = 8;
        btnUpdateRole.Text = "Update";
        btnUpdateRole.UseVisualStyleBackColor = true;
        btnUpdateRole.Click += btnUpdateRole_Click;
        
        // btnDeleteRole
        btnDeleteRole.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnDeleteRole.Location = new Point(515, 575);
        btnDeleteRole.Name = "btnDeleteRole";
        btnDeleteRole.Size = new Size(75, 23);
        btnDeleteRole.TabIndex = 9;
        btnDeleteRole.Text = "Delete";
        btnDeleteRole.UseVisualStyleBackColor = true;
        btnDeleteRole.Click += btnDeleteRole_Click;
        
        // pnlNewRole
        pnlNewRole.BackColor = SystemColors.Window;
        pnlNewRole.BorderStyle = BorderStyle.FixedSingle;
        pnlNewRole.Controls.Add(lblNewRoleStatus);
        pnlNewRole.Controls.Add(btnCancelNewRole);
        pnlNewRole.Controls.Add(btnCreateRole);
        pnlNewRole.Controls.Add(txtNewRoleDesc);
        pnlNewRole.Controls.Add(lblNewRoleDesc);
        pnlNewRole.Controls.Add(txtNewRoleName);
        pnlNewRole.Controls.Add(lblNewRoleName);
        pnlNewRole.Location = new Point(250, 250);
        pnlNewRole.Name = "pnlNewRole";
        pnlNewRole.Padding = new Padding(10);
        pnlNewRole.Size = new Size(350, 170);
        pnlNewRole.TabIndex = 10;
        pnlNewRole.Visible = false;
        
        // lblNewRoleName
        lblNewRoleName.AutoSize = true;
        lblNewRoleName.Location = new Point(10, 10);
        lblNewRoleName.Name = "lblNewRoleName";
        lblNewRoleName.Size = new Size(72, 15);
        lblNewRoleName.TabIndex = 0;
        lblNewRoleName.Text = "Role Name:";
        
        txtNewRoleName.Location = new Point(85, 10);
        txtNewRoleName.Name = "txtNewRoleName";
        txtNewRoleName.Size = new Size(255, 23);
        txtNewRoleName.TabIndex = 1;
        
        // lblNewRoleDesc
        lblNewRoleDesc.AutoSize = true;
        lblNewRoleDesc.Location = new Point(10, 40);
        lblNewRoleDesc.Name = "lblNewRoleDesc";
        lblNewRoleDesc.Size = new Size(70, 15);
        lblNewRoleDesc.TabIndex = 2;
        lblNewRoleDesc.Text = "Description:";
        
        txtNewRoleDesc.Location = new Point(85, 40);
        txtNewRoleDesc.Multiline = true;
        txtNewRoleDesc.Name = "txtNewRoleDesc";
        txtNewRoleDesc.Size = new Size(255, 50);
        txtNewRoleDesc.TabIndex = 3;
        
        // lblNewRoleStatus
        lblNewRoleStatus.AutoSize = true;
        lblNewRoleStatus.Location = new Point(10, 95);
        lblNewRoleStatus.Name = "lblNewRoleStatus";
        lblNewRoleStatus.Size = new Size(0, 15);
        lblNewRoleStatus.TabIndex = 4;
        
        // btnCreateRole
        btnCreateRole.Location = new Point(215, 95);
        btnCreateRole.Name = "btnCreateRole";
        btnCreateRole.Size = new Size(60, 23);
        btnCreateRole.TabIndex = 5;
        btnCreateRole.Text = "Create";
        btnCreateRole.UseVisualStyleBackColor = true;
        btnCreateRole.Click += btnCreateRole_Click;
        
        // btnCancelNewRole
        btnCancelNewRole.Location = new Point(280, 95);
        btnCancelNewRole.Name = "btnCancelNewRole";
        btnCancelNewRole.Size = new Size(60, 23);
        btnCancelNewRole.TabIndex = 6;
        btnCancelNewRole.Text = "Cancel";
        btnCancelNewRole.UseVisualStyleBackColor = true;
        btnCancelNewRole.Click += btnCancelNewRole_Click;
        
        // btnNewRole
        btnNewRole.Location = new Point(13, 525);
        btnNewRole.Name = "btnNewRole";
        btnNewRole.Size = new Size(100, 23);
        btnNewRole.TabIndex = 7;
        btnNewRole.Text = "New Role";
        btnNewRole.UseVisualStyleBackColor = true;
        btnNewRole.Click += btnNewRole_Click;
        pnlRoleDetails.Controls.Add(btnNewRole);
        
        // btnLogout
        btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLogout.Location = new Point(725, 12);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(75, 23);
        btnLogout.TabIndex = 8;
        btnLogout.Text = "Logout";
        btnLogout.UseVisualStyleBackColor = true;
        btnLogout.Click += btnLogout_Click;
        
        // RoleManagementForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 605);
        Controls.Add(btnLogout);
        Controls.Add(pnlNewRole);
        Controls.Add(pnlRoleDetails);
        Controls.Add(lstRoles);
        Name = "RoleManagementForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Role Management";
        Load += RoleManagementForm_Load;
        pnlRoleDetails.ResumeLayout(false);
        pnlRoleDetails.PerformLayout();
        grpRights.ResumeLayout(false);
        //grpSettingsRights.ResumeLayout(false);
        //grpSettingsRights.PerformLayout();

        grpRoleRights.ResumeLayout(false);
        grpRoleRights.PerformLayout();
        grpUserRights.ResumeLayout(false);
        grpUserRights.PerformLayout();
        pnlNewRole.ResumeLayout(false);
        pnlNewRole.PerformLayout();
        ResumeLayout(false);
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
}
