using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace PGen.Forms
{
    [SupportedOSPlatform("windows")]
    public partial class LicenseErrorDialog : Form
    {
        private readonly string _machineId;

        public LicenseErrorDialog(string message, string machineId)
        {
            _machineId = machineId;
            InitializeComponent();
            lblMessage.Text = message;
            txtMachineId.Text = machineId;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "PGen - License Check";
            this.Size = new Size(500, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;

            // Message label
            lblMessage = new Label
            {
                Location = new Point(20, 20),
                Size = new Size(440, 60),
                Text = "",
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Machine ID label
            lblMachineIdLabel = new Label
            {
                Location = new Point(20, 90),
                Size = new Size(100, 23),
                Text = "Machine ID:",
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold)
            };

            // Machine ID text box
            txtMachineId = new TextBox
            {
                Location = new Point(20, 115),
                Size = new Size(360, 23),
                Text = "",
                ReadOnly = true,
                Font = new Font("Consolas", 8.25F)
            };

            // Copy button
            btnCopy = new Button
            {
                Location = new Point(390, 113),
                Size = new Size(70, 27),
                Text = "Copy",
                UseVisualStyleBackColor = true
            };
            btnCopy.Click += BtnCopy_Click;

            // OK button
            btnOK = new Button
            {
                Location = new Point(205, 160),
                Size = new Size(75, 27),
                Text = "OK",
                UseVisualStyleBackColor = true,
                DialogResult = DialogResult.OK
            };
            btnOK.Click += (s, e) => this.Close();

            // Add controls to form
            this.Controls.AddRange(new Control[] { lblMessage, lblMachineIdLabel, txtMachineId, btnCopy, btnOK });

            // Set default button
            this.AcceptButton = btnOK;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_machineId);
            MessageBox.Show("Machine ID copied to clipboard.", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private Label lblMessage;
        private Label lblMachineIdLabel;
        private TextBox txtMachineId;
        private Button btnCopy;
        private Button btnOK;
    }
}
