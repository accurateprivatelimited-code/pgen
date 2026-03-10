namespace PGen
{
    partial class UserManagementForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvUsers = new DataGridView();
            colUser = new DataGridViewTextBoxColumn();
            colRole = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnResetPassword = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            btnEdit = new Button();
            btnLogout = new Button();
            menuStrip = new MenuStrip();
            menuAdmin = new ToolStripMenuItem();
            menuCreateLicense = new ToolStripMenuItem();
            menuManageUsers = new ToolStripMenuItem();
            menuManageRoles = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Columns.AddRange(new DataGridViewColumn[] { colUser, colRole });
            dgvUsers.Location = new Point(14, 73);
            dgvUsers.Margin = new Padding(3, 4, 3, 4);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(526, 290);
            dgvUsers.TabIndex = 0;
            // 
            // colUser
            // 
            colUser.DataPropertyName = "UserName";
            colUser.HeaderText = "User";
            colUser.MinimumWidth = 6;
            colUser.Name = "colUser";
            colUser.ReadOnly = true;
            colUser.Width = 240;
            // 
            // colRole
            // 
            colRole.DataPropertyName = "RoleName";
            colRole.HeaderText = "Role";
            colRole.MinimumWidth = 6;
            colRole.Name = "colRole";
            colRole.ReadOnly = true;
            colRole.Width = 180;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.Location = new Point(14, 380);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(103, 36);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add user";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnResetPassword
            // 
            btnResetPassword.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnResetPassword.Location = new Point(145, 380);
            btnResetPassword.Margin = new Padding(3, 4, 3, 4);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(137, 36);
            btnResetPassword.TabIndex = 2;
            btnResetPassword.Text = "Reset password";
            btnResetPassword.UseVisualStyleBackColor = true;
            btnResetPassword.Click += btnResetPassword_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(217, 33);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(103, 36);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete user";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRefresh.Location = new Point(425, 380);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(103, 36);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEdit.Location = new Point(303, 380);
            btnEdit.Margin = new Padding(3, 4, 3, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(103, 36);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit user";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Location = new Point(465, 36);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(75, 30);
            btnLogout.TabIndex = 5;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { menuAdmin });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(553, 28);
            menuStrip.TabIndex = 6;
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
            // UserManagementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(553, 432);
            Controls.Add(menuStrip);
            Controls.Add(btnLogout);
            Controls.Add(btnRefresh);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnResetPassword);
            Controls.Add(btnAdd);
            Controls.Add(dgvUsers);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MainMenuStrip = menuStrip;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserManagementForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Admin - Manage Users";
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuAdmin;
        private System.Windows.Forms.ToolStripMenuItem menuCreateLicense;
        private System.Windows.Forms.ToolStripMenuItem menuManageUsers;
        private System.Windows.Forms.ToolStripMenuItem menuManageRoles;
        private DataGridViewTextBoxColumn colUser;
        private DataGridViewTextBoxColumn colRole;
    }
}

