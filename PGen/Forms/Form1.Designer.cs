namespace PGen
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblMsn = new Label();
            lblType = new Label();
            lblSets = new Label();
            grpInputs = new GroupBox();
            txtMsnOrRange = new TextBox();
            cboMeterType = new ComboBox();
            numSets = new NumericUpDown();
            btnGenerate = new Button();
            btnCancel = new Button();
            btnExport8 = new Button();
            btnExport32 = new Button();
            grpSecurity = new GroupBox();
            lblMachine = new Label();
            lblMachineId = new TextBox();
            grpSearch = new GroupBox();
            lblSearch = new Label();
            txtSearch = new TextBox();
            lblFilterField = new Label();
            cboFilterField = new ComboBox();
            btnClearSearch = new Button();
            dgvResults = new DataGridView();
            statusStrip = new StatusStrip();
            toolStatus = new ToolStripStatusLabel();
            toolUser = new ToolStripStatusLabel();
            progressBar = new ToolStripProgressBar();
            menuStrip = new MenuStrip();
            menuAdmin = new ToolStripMenuItem();
            menuCreateLicense = new ToolStripMenuItem();
            menuManageUsers = new ToolStripMenuItem();
            menuManageRoles = new ToolStripMenuItem();
            menuLogout = new ToolStripMenuItem();
            grpInputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSets).BeginInit();
            grpSecurity.SuspendLayout();
            grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            statusStrip.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // lblMsn
            // 
            lblMsn.AutoSize = true;
            lblMsn.Location = new Point(16, 40);
            lblMsn.Name = "lblMsn";
            lblMsn.Size = new Size(171, 20);
            lblMsn.TabIndex = 0;
            lblMsn.Text = "MSN or Range (8/10/12)";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(801, 41);
            lblType.Name = "lblType";
            lblType.Size = new Size(83, 20);
            lblType.TabIndex = 1;
            lblType.Text = "Meter Type";
            // 
            // lblSets
            // 
            lblSets.AutoSize = true;
            lblSets.Location = new Point(1097, 39);
            lblSets.Name = "lblSets";
            lblSets.Size = new Size(36, 20);
            lblSets.TabIndex = 3;
            lblSets.Text = "Sets";
            // 
            // grpInputs
            // 
            grpInputs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpInputs.Controls.Add(lblMsn);
            grpInputs.Controls.Add(txtMsnOrRange);
            grpInputs.Controls.Add(lblType);
            grpInputs.Controls.Add(cboMeterType);
            grpInputs.Controls.Add(lblSets);
            grpInputs.Controls.Add(numSets);
            grpInputs.Controls.Add(btnGenerate);
            grpInputs.Controls.Add(btnCancel);
            grpInputs.Location = new Point(14, 34);
            grpInputs.Margin = new Padding(3, 4, 3, 4);
            grpInputs.Name = "grpInputs";
            grpInputs.Padding = new Padding(3, 4, 3, 4);
            grpInputs.Size = new Size(1326, 145);
            grpInputs.TabIndex = 0;
            grpInputs.TabStop = false;
            grpInputs.Text = "Keys Generation";
            // 
            // txtMsnOrRange
            // 
            txtMsnOrRange.Location = new Point(194, 36);
            txtMsnOrRange.Margin = new Padding(3, 4, 3, 4);
            txtMsnOrRange.Name = "txtMsnOrRange";
            txtMsnOrRange.PlaceholderText = "Example: 00001234 or 00001234-00001300";
            txtMsnOrRange.Size = new Size(594, 27);
            txtMsnOrRange.TabIndex = 1;
            // 
            // cboMeterType
            // 
            cboMeterType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMeterType.FormattingEnabled = true;
            cboMeterType.Location = new Point(900, 35);
            cboMeterType.Margin = new Padding(3, 4, 3, 4);
            cboMeterType.Name = "cboMeterType";
            cboMeterType.Size = new Size(171, 28);
            cboMeterType.TabIndex = 2;
            // 
            // numSets
            // 
            numSets.Location = new Point(1143, 35);
            numSets.Margin = new Padding(3, 4, 3, 4);
            numSets.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numSets.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSets.Name = "numSets";
            numSets.Size = new Size(80, 27);
            numSets.TabIndex = 4;
            numSets.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(1039, 95);
            btnGenerate.Margin = new Padding(3, 4, 3, 4);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(126, 33);
            btnGenerate.TabIndex = 5;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // btnCancel
            // 
            btnCancel.Enabled = false;
            btnCancel.Location = new Point(1176, 95);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(126, 33);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnExport8
            // 
            btnExport8.Location = new Point(1024, 28);
            btnExport8.Margin = new Padding(3, 4, 3, 4);
            btnExport8.Name = "btnExport8";
            btnExport8.Size = new Size(126, 33);
            btnExport8.TabIndex = 8;
            btnExport8.Text = "Export Excel (8)";
            btnExport8.UseVisualStyleBackColor = true;
            btnExport8.Click += btnExport8_Click;
            // 
            // btnExport32
            // 
            btnExport32.Location = new Point(1172, 28);
            btnExport32.Margin = new Padding(3, 4, 3, 4);
            btnExport32.Name = "btnExport32";
            btnExport32.Size = new Size(138, 33);
            btnExport32.TabIndex = 9;
            btnExport32.Text = "Export Excel (32)";
            btnExport32.UseVisualStyleBackColor = true;
            btnExport32.Click += btnExport32_Click;
            // 
            // grpSecurity
            // 
            grpSecurity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpSecurity.Controls.Add(lblMachine);
            grpSecurity.Controls.Add(lblMachineId);
            grpSecurity.Location = new Point(14, 187);
            grpSecurity.Margin = new Padding(3, 4, 3, 4);
            grpSecurity.Name = "grpSecurity";
            grpSecurity.Padding = new Padding(3, 4, 3, 4);
            grpSecurity.Size = new Size(1326, 80);
            grpSecurity.TabIndex = 1;
            grpSecurity.TabStop = false;
            grpSecurity.Text = "Security";
            // 
            // lblMachine
            // 
            lblMachine.AutoSize = true;
            lblMachine.Location = new Point(16, 36);
            lblMachine.Name = "lblMachine";
            lblMachine.Size = new Size(84, 20);
            lblMachine.TabIndex = 0;
            lblMachine.Text = "Machine ID";
            // 
            // lblMachineId
            // 
            lblMachineId.Location = new Point(114, 32);
            lblMachineId.Margin = new Padding(3, 4, 3, 4);
            lblMachineId.Name = "lblMachineId";
            lblMachineId.ReadOnly = true;
            lblMachineId.Size = new Size(1188, 27);
            lblMachineId.TabIndex = 1;
            // 
            // grpSearch
            // 
            grpSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpSearch.Controls.Add(lblSearch);
            grpSearch.Controls.Add(txtSearch);
            grpSearch.Controls.Add(lblFilterField);
            grpSearch.Controls.Add(cboFilterField);
            grpSearch.Controls.Add(btnClearSearch);
            grpSearch.Controls.Add(btnExport8);
            grpSearch.Controls.Add(btnExport32);
            grpSearch.Location = new Point(14, 275);
            grpSearch.Margin = new Padding(3, 4, 3, 4);
            grpSearch.Name = "grpSearch";
            grpSearch.Padding = new Padding(3, 4, 3, 4);
            grpSearch.Size = new Size(1326, 80);
            grpSearch.TabIndex = 3;
            grpSearch.TabStop = false;
            grpSearch.Text = "Quick Search & Filter";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(16, 36);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(56, 20);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(80, 32);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(342, 27);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblFilterField
            // 
            lblFilterField.AutoSize = true;
            lblFilterField.Location = new Point(446, 36);
            lblFilterField.Name = "lblFilterField";
            lblFilterField.Size = new Size(81, 20);
            lblFilterField.TabIndex = 2;
            lblFilterField.Text = "Filter Field:";
            // 
            // cboFilterField
            // 
            cboFilterField.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterField.FormattingEnabled = true;
            cboFilterField.Items.AddRange(new object[] { "All Fields", "MSN", "Type", "AK8", "EK8", "AK32", "EK32" });
            cboFilterField.Location = new Point(543, 32);
            cboFilterField.Margin = new Padding(3, 4, 3, 4);
            cboFilterField.Name = "cboFilterField";
            cboFilterField.Size = new Size(171, 28);
            cboFilterField.TabIndex = 3;
            cboFilterField.SelectedIndexChanged += cboFilterField_SelectedIndexChanged;
            // 
            // btnClearSearch
            // 
            btnClearSearch.Location = new Point(731, 32);
            btnClearSearch.Margin = new Padding(3, 4, 3, 4);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Size = new Size(86, 31);
            btnClearSearch.TabIndex = 4;
            btnClearSearch.Text = "Clear";
            btnClearSearch.UseVisualStyleBackColor = true;
            btnClearSearch.Click += btnClearSearch_Click;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Location = new Point(14, 360);
            dgvResults.Margin = new Padding(3, 4, 3, 4);
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.RowHeadersWidth = 51;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.Size = new Size(1327, 469);
            dgvResults.TabIndex = 2;
            dgvResults.CellContentClick += dgvResults_CellContentClick;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStatus, toolUser, progressBar });
            statusStrip.Location = new Point(0, 854);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1353, 27);
            statusStrip.TabIndex = 3;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStatus
            // 
            toolStatus.Name = "toolStatus";
            toolStatus.Size = new Size(53, 21);
            toolStatus.Text = "Ready.";
            // 
            // toolUser
            // 
            toolUser.Name = "toolUser";
            toolUser.Size = new Size(0, 21);
            // 
            // progressBar
            // 
            progressBar.Alignment = ToolStripItemAlignment.Right;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(229, 19);
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { menuAdmin, menuLogout });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 3, 0, 3);
            menuStrip.Size = new Size(1353, 30);
            menuStrip.TabIndex = 4;
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
            // menuLogout
            // 
            menuLogout.Alignment = ToolStripItemAlignment.Right;
            menuLogout.Name = "menuLogout";
            menuLogout.Size = new Size(70, 24);
            menuLogout.Text = "Logout";
            menuLogout.Click += menuLogout_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1353, 881);
            Controls.Add(menuStrip);
            Controls.Add(statusStrip);
            Controls.Add(dgvResults);
            Controls.Add(grpSearch);
            Controls.Add(grpSecurity);
            Controls.Add(grpInputs);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1255, 918);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PGen - AK/EK Generator";
            grpInputs.ResumeLayout(false);
            grpInputs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSets).EndInit();
            grpSecurity.ResumeLayout(false);
            grpSecurity.PerformLayout();
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtMsnOrRange;
        private System.Windows.Forms.ComboBox cboMeterType;

        private System.Windows.Forms.NumericUpDown numSets;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExport8;
        private System.Windows.Forms.Button btnExport32;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboFilterField;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolUser;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.TextBox lblMachineId;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuAdmin;
        private System.Windows.Forms.ToolStripMenuItem menuCreateLicense;
        private System.Windows.Forms.ToolStripMenuItem menuManageUsers;
        private System.Windows.Forms.ToolStripMenuItem menuManageRoles;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
        private Label lblMsn;
        private Label lblType;

        private Label lblSets;
        private GroupBox grpInputs;
        private GroupBox grpSecurity;
        private Label lblMachine;
        private GroupBox grpSearch;
        private Label lblSearch;
        private Label lblFilterField;
    }
}

