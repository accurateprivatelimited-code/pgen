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
            var lblUser = new System.Windows.Forms.Label();
            var lblMachine = new System.Windows.Forms.Label();
            var lblDays = new System.Windows.Forms.Label();
            cboUser = new System.Windows.Forms.ComboBox();
            txtMachineId = new System.Windows.Forms.TextBox();
            numDays = new System.Windows.Forms.NumericUpDown();
            btnGenerate = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numDays).BeginInit();
            SuspendLayout();
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new System.Drawing.Point(20, 20);
            lblUser.Name = "lblUser";
            lblUser.Size = new System.Drawing.Size(30, 15);
            lblUser.TabIndex = 0;
            lblUser.Text = "User";
            // 
            // cboUser
            // 
            cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboUser.Location = new System.Drawing.Point(110, 17);
            cboUser.Name = "cboUser";
            cboUser.Size = new System.Drawing.Size(220, 23);
            cboUser.TabIndex = 1;
            // 
            // lblMachine
            // 
            lblMachine.AutoSize = true;
            lblMachine.Location = new System.Drawing.Point(20, 60);
            lblMachine.Name = "lblMachine";
            lblMachine.Size = new System.Drawing.Size(70, 15);
            lblMachine.TabIndex = 2;
            lblMachine.Text = "Machine ID";
            // 
            // lblDays
            // 
            lblDays.AutoSize = true;
            lblDays.Location = new System.Drawing.Point(20, 100);
            lblDays.Name = "lblDays";
            lblDays.Size = new System.Drawing.Size(68, 15);
            lblDays.TabIndex = 3;
            lblDays.Text = "Valid (days)";
            // 
            // txtMachineId
            // 
            txtMachineId.Location = new System.Drawing.Point(110, 57);
            txtMachineId.Name = "txtMachineId";
            txtMachineId.Size = new System.Drawing.Size(420, 23);
            txtMachineId.TabIndex = 4;
            // 
            // numDays
            // 
            numDays.Location = new System.Drawing.Point(110, 97);
            numDays.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
            numDays.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numDays.Name = "numDays";
            numDays.Size = new System.Drawing.Size(100, 23);
            numDays.TabIndex = 3;
            numDays.Value = new decimal(new int[] { 365, 0, 0, 0 });
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new System.Drawing.Point(330, 100);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new System.Drawing.Size(95, 27);
            btnGenerate.TabIndex = 4;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new System.Drawing.Point(435, 100);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(95, 27);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // LicenseAdminForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(544, 143);
            Controls.Add(btnClose);
            Controls.Add(btnGenerate);
            Controls.Add(numDays);
            Controls.Add(txtMachineId);
            Controls.Add(cboUser);
            Controls.Add(lblDays);
            Controls.Add(lblMachine);
            Controls.Add(lblUser);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LicenseAdminForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Admin - Create License";
            ((System.ComponentModel.ISupportInitialize)numDays).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.TextBox txtMachineId;
        private System.Windows.Forms.NumericUpDown numDays;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnClose;
    }
}

