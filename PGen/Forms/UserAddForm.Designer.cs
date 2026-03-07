namespace PGen
{
    partial class UserAddForm
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
            var lblPassword = new System.Windows.Forms.Label();
            var lblRole = new System.Windows.Forms.Label();
            txtUser = new System.Windows.Forms.TextBox();
            txtPassword = new System.Windows.Forms.TextBox();
            cboRole = new System.Windows.Forms.ComboBox();
            btnOk = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            var btnLogout = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new System.Drawing.Point(20, 20);
            lblUser.Name = "lblUser";
            lblUser.Size = new System.Drawing.Size(63, 15);
            lblUser.TabIndex = 0;
            lblUser.Text = "User name";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new System.Drawing.Point(20, 60);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(57, 15);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Password";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Location = new System.Drawing.Point(20, 100);
            lblRole.Name = "lblRole";
            lblRole.Size = new System.Drawing.Size(30, 15);
            lblRole.TabIndex = 2;
            lblRole.Text = "Role";
            // 
            // txtUser
            // 
            txtUser.Location = new System.Drawing.Point(110, 17);
            txtUser.Name = "txtUser";
            txtUser.Size = new System.Drawing.Size(220, 23);
            txtUser.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(110, 57);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new System.Drawing.Size(220, 23);
            txtPassword.TabIndex = 4;
            // 
            // cboRole
            // 
            cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboRole.Location = new System.Drawing.Point(110, 97);
            cboRole.Name = "cboRole";
            cboRole.Size = new System.Drawing.Size(220, 23);
            cboRole.TabIndex = 5;
            // 
            // btnOk
            // 
            btnOk.Location = new System.Drawing.Point(155, 140);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(85, 27);
            btnOk.TabIndex = 6;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(245, 140);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(85, 27);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnLogout.Location = new System.Drawing.Point(335, 12);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new System.Drawing.Size(75, 23);
            btnLogout.TabIndex = 8;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // UserAddForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(420, 185);
            Controls.Add(btnLogout);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(cboRole);
            Controls.Add(txtPassword);
            Controls.Add(txtUser);
            Controls.Add(lblRole);
            Controls.Add(lblPassword);
            Controls.Add(lblUser);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserAddForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Add User";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLogout;
    }
}
