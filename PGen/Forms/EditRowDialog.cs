using System.ComponentModel;
using System.Text.RegularExpressions;
using PGen.Models;

namespace PGen
{
    internal partial class EditRowDialog : Form
    {
        private readonly MeterKeyRow _originalRow;
        public MeterKeyRow UpdatedRow { get; private set; }

        public EditRowDialog(MeterKeyRow row)
        {
            _originalRow = row;
            UpdatedRow = null;

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Edit Row";
            this.Size = new System.Drawing.Size(450, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;

            // Controls
            var lblMsn = new Label { Text = "MSN:", Location = new Point(20, 20), Size = new Size(80, 23) };
            txtMsn = new TextBox { Location = new Point(110, 20), Size = new Size(150, 23), ReadOnly = true };

            var lblType = new Label { Text = "Meter Type:", Location = new Point(20, 60), Size = new Size(80, 23) };
            txtMeterType = new TextBox { Location = new Point(110, 60), Size = new Size(150, 23) };


            var lblSet = new Label { Text = "Set:", Location = new Point(20, 140), Size = new Size(80, 23) };
            numSetIndex = new NumericUpDown { Location = new Point(110, 140), Size = new Size(100, 23), Minimum = 1, Maximum = 1000 };

            var lblAk8 = new Label { Text = "AK8:", Location = new Point(20, 180), Size = new Size(80, 23) };
            txtAk8 = new TextBox { Location = new Point(110, 180), Size = new Size(300, 23) };
            txtAk8.KeyPress += NumericTextBox_KeyPress;

            var lblEk8 = new Label { Text = "EK8:", Location = new Point(20, 220), Size = new Size(80, 23) };
            txtEk8 = new TextBox { Location = new Point(110, 220), Size = new Size(300, 23) };
            txtEk8.KeyPress += NumericTextBox_KeyPress;

            var lblAk32 = new Label { Text = "AK32:", Location = new Point(20, 260), Size = new Size(80, 23) };
            txtAk32 = new TextBox { Location = new Point(110, 260), Size = new Size(300, 23), Multiline = true, Height = 40 };
            txtAk32.KeyPress += NumericTextBox_KeyPress;

            var lblEk32 = new Label { Text = "EK32:", Location = new Point(20, 310), Size = new Size(80, 23) };
            txtEk32 = new TextBox { Location = new Point(110, 310), Size = new Size(300, 23), Multiline = true, Height = 40 };
            txtEk32.KeyPress += NumericTextBox_KeyPress;

            btnOK = new Button { Text = "OK", Location = new Point(260, 360), Size = new Size(75, 23), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Cancel", Location = new Point(345, 360), Size = new Size(75, 23), DialogResult = DialogResult.Cancel };
            btnLogout = new Button { Text = "Logout", Location = new Point(370, 12), Size = new Size(75, 23) };
            btnLogout.Click += BtnLogout_Click;

            // Add controls to form
            this.Controls.AddRange(new Control[] {
                lblMsn, txtMsn, lblType, txtMeterType,
                lblSet, numSetIndex, lblAk8, txtAk8, lblEk8, txtEk8,
                lblAk32, txtAk32, lblEk32, txtEk32, btnOK, btnCancel, btnLogout
            });

            btnOK.Click += BtnOK_Click;

            this.ResumeLayout(false);
        }

        private void LoadData()
        {
            txtMsn.Text = _originalRow.Msn;
            txtMeterType.Text = _originalRow.MeterType;

            numSetIndex.Value = _originalRow.SetIndex;

            // existing stored values may be in the previous hex format; convert if necessary so
            // the editor always shows a positive integer string.
            txtAk8.Text = NormalizeKey(_originalRow.Ak8, expectedLength: 8);
            txtEk8.Text = NormalizeKey(_originalRow.Ek8, expectedLength: 8);
            txtAk32.Text = NormalizeKey(_originalRow.Ak32, expectedLength: 32);
            txtEk32.Text = NormalizeKey(_originalRow.Ek32, expectedLength: 32);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                UpdatedRow = new MeterKeyRow
                {
                    Msn = txtMsn.Text,
                    MeterType = txtMeterType.Text,
                    Model = _originalRow.Model,
                    SetIndex = (int)numSetIndex.Value,
                    Ak8 = txtAk8.Text,
                    Ek8 = txtEk8.Text,
                    Ak32 = txtAk32.Text,
                    Ek32 = txtEk32.Text
                };
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMeterType.Text))
            {
                MessageBox.Show("Meter Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMeterType.Focus();
                return false;
            }


            if (string.IsNullOrWhiteSpace(txtAk8.Text))
            {
                MessageBox.Show("AK8 is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAk8.Focus();
                return false;
            }
            if (!IsPositiveInteger(txtAk8.Text))
            {
                MessageBox.Show("AK8 must be a positive integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAk8.Focus();
                return false;
            }
            if (txtAk8.Text.Length != 8)
            {
                MessageBox.Show("AK8 must be exactly 8 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAk8.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEk8.Text))
            {
                MessageBox.Show("EK8 is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEk8.Focus();
                return false;
            }
            if (!IsPositiveInteger(txtEk8.Text))
            {
                MessageBox.Show("EK8 must be a positive integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEk8.Focus();
                return false;
            }
            if (txtEk8.Text.Length != 8)
            {
                MessageBox.Show("EK8 must be exactly 8 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEk8.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAk32.Text))
            {
                MessageBox.Show("AK32 is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAk32.Focus();
                return false;
            }
            if (!IsPositiveInteger(txtAk32.Text))
            {
                MessageBox.Show("AK32 must be a positive integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAk32.Focus();
                return false;
            }
            if (txtAk32.Text.Length != 32)
            {
                MessageBox.Show("AK32 must be exactly 32 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAk32.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEk32.Text))
            {
                MessageBox.Show("EK32 is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEk32.Focus();
                return false;
            }
            if (!IsPositiveInteger(txtEk32.Text))
            {
                MessageBox.Show("EK32 must be a positive integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEk32.Focus();
                return false;
            }
            if (txtEk32.Text.Length != 32)
            {
                MessageBox.Show("EK32 must be exactly 32 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEk32.Focus();
                return false;
            }

            return true;
        }

        // Control declarations
        private TextBox txtMsn;
        private TextBox txtMeterType;
        private NumericUpDown numSetIndex;
        private TextBox txtAk8;
        private TextBox txtEk8;
        private TextBox txtAk32;
        private TextBox txtEk32;
        private Button btnOK;
        private Button btnCancel;
        private Button btnLogout;

        // utility helpers for numeric-only key fields
        private static bool IsPositiveInteger(string text)
        {
            // allow no leading plus sign, require at least one digit and no zeros at start unless the number is exactly "0" (we disallow zero anyway)
            return Regex.IsMatch(text, "^[1-9][0-9]*$");
        }

        private static string NormalizeKey(string raw, int expectedLength)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return raw ?? string.Empty;

            string normalized;
            // if the value already consists only of digits, assume it is a decimal representation
            if (Regex.IsMatch(raw, "^[0-9]+$"))
                normalized = raw;
            else
            {
                // otherwise try to treat it as hex and convert – keep digits only output
                try
                {
                    var value = System.Numerics.BigInteger.Parse(raw, System.Globalization.NumberStyles.HexNumber);
                    if (value.Sign < 0) value = System.Numerics.BigInteger.Negate(value);
                    normalized = value.ToString();
                }
                catch
                {
                    // if parsing fails just return original text (validation will catch it later)
                    normalized = raw;
                }
            }

            // enforce fixed width by padding/truncating
            if (expectedLength > 0)
            {
                if (normalized.Length > expectedLength)
                    normalized = normalized[^expectedLength..]; // rightmost digits
                else if (normalized.Length < expectedLength)
                    normalized = normalized.PadLeft(expectedLength, '0');
            }
            return normalized;
        }

        private void NumericTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // only digits and control characters (backspace, etc.) are permitted
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
            Close();
        }
    }
}
