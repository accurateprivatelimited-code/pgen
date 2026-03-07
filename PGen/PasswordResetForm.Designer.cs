namespace PGen
{
    partial class PasswordResetForm
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
            lblUserValue = new System.Windows.Forms.Label();
            var lblPassword = new System.Windows.Forms.Label();
            txtPassword = new System.Windows.Forms.TextBox();
            btnOk = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new System.Drawing.Point(20, 20);
            lblUser.Name = "lblUser";
            lblUser.Size = new System.Drawing.Size(33, 15);
            lblUser.TabIndex = 0;
            lblUser.Text = "User:";
            // 
            // lblUserValue
            // 
            lblUserValue.AutoSize = true;
            lblUserValue.Location = new System.Drawing.Point(70, 20);
            lblUserValue.Name = "lblUserValue";
            lblUserValue.Size = new System.Drawing.Size(0, 15);
            lblUserValue.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new System.Drawing.Point(20, 55);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(86, 15);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "New password";
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(120, 52);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new System.Drawing.Size(220, 23);
            txtPassword.TabIndex = 3;
            // 
            // btnOk
            // 
            btnOk.Location = new System.Drawing.Point(165, 95);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(85, 27);
            btnOk.TabIndex = 4;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(255, 95);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(85, 27);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // PasswordResetForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(364, 140);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(lblUserValue);
            Controls.Add(lblUser);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PasswordResetForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Reset Password";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblUserValue;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}

