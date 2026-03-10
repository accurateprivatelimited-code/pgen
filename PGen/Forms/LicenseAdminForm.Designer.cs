namespace PGen
{
    partial class LicenseAdminForm
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
            lblUser = new Label();
            lblMachine = new Label();
            lblDays = new Label();
            cboUser = new ComboBox();
            txtMachineId = new TextBox();
            numDays = new NumericUpDown();
            btnGenerate = new Button();
            btnClose = new Button();
            btnLogout = new Button();
            menuStrip = new MenuStrip();
            menuAdmin = new ToolStripMenuItem();
            menuCreateLicense = new ToolStripMenuItem();
            menuManageUsers = new ToolStripMenuItem();
            menuManageRoles = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)numDays).BeginInit();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(16, 55);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(38, 20);
            lblUser.TabIndex = 0;
            lblUser.Text = "User";
            // 
            // lblMachine
            // 
            lblMachine.AutoSize = true;
            lblMachine.Location = new Point(16, 108);
            lblMachine.Name = "lblMachine";
            lblMachine.Size = new Size(84, 20);
            lblMachine.TabIndex = 2;
            lblMachine.Text = "Machine ID";
            // 
            // lblDays
            // 
            lblDays.AutoSize = true;
            lblDays.Location = new Point(16, 161);
            lblDays.Name = "lblDays";
            lblDays.Size = new Size(86, 20);
            lblDays.TabIndex = 3;
            lblDays.Text = "Valid (days)";
            // 
            // cboUser
            // 
            cboUser.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUser.Location = new Point(119, 51);
            cboUser.Margin = new Padding(3, 4, 3, 4);
            cboUser.Name = "cboUser";
            cboUser.Size = new Size(251, 28);
            cboUser.TabIndex = 1;
            // 
            // txtMachineId
            // 
            txtMachineId.Location = new Point(119, 104);
            txtMachineId.Margin = new Padding(3, 4, 3, 4);
            txtMachineId.Name = "txtMachineId";
            txtMachineId.Size = new Size(479, 27);
            txtMachineId.TabIndex = 4;
            // 
            // numDays
            // 
            numDays.Location = new Point(119, 157);
            numDays.Margin = new Padding(3, 4, 3, 4);
            numDays.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
            numDays.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numDays.Name = "numDays";
            numDays.Size = new Size(114, 27);
            numDays.TabIndex = 3;
            numDays.Value = new decimal(new int[] { 365, 0, 0, 0 });
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(370, 161);
            btnGenerate.Margin = new Padding(3, 4, 3, 4);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(109, 36);
            btnGenerate.TabIndex = 4;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(490, 161);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(109, 36);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Location = new Point(524, 50);
            btnLogout.Margin = new Padding(3, 4, 3, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(86, 31);
            btnLogout.TabIndex = 6;
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
            menuStrip.Padding = new Padding(7, 3, 0, 3);
            menuStrip.Size = new Size(622, 30);
            menuStrip.TabIndex = 7;
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
            // LicenseAdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 246);
            Controls.Add(menuStrip);
            Controls.Add(btnLogout);
            Controls.Add(btnClose);
            Controls.Add(btnGenerate);
            Controls.Add(numDays);
            Controls.Add(txtMachineId);
            Controls.Add(cboUser);
            Controls.Add(lblDays);
            Controls.Add(lblMachine);
            Controls.Add(lblUser);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LicenseAdminForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Admin - Create License";
            ((System.ComponentModel.ISupportInitialize)numDays).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.TextBox txtMachineId;
        private System.Windows.Forms.NumericUpDown numDays;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuAdmin;
        private System.Windows.Forms.ToolStripMenuItem menuCreateLicense;
        private System.Windows.Forms.ToolStripMenuItem menuManageUsers;
        private System.Windows.Forms.ToolStripMenuItem menuManageRoles;
        private Label lblUser;
        private Label lblMachine;
        private Label lblDays;
    }
}

