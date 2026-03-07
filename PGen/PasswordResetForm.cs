using System.Runtime.Versioning;
using System.Windows.Forms;

namespace PGen;

[SupportedOSPlatform("windows")]
public partial class PasswordResetForm : Form
{
    public string NewPassword => txtPassword.Text;

    public PasswordResetForm(string username)
    {
        InitializeComponent();
        lblUserValue.Text = username;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        if (NewPassword.Length == 0)
        {
            MessageBox.Show(this, "Password is required.", "Reset password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}

