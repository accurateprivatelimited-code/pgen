using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGen.Auth;

namespace PGen;

[SupportedOSPlatform("windows")]
public partial class LoginForm : Form
{
    public UserAccount? AuthenticatedUser { get; private set; }

    public LoginForm()
    {
        InitializeComponent();
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        var user = await AuthService.AuthenticateAsync(txtUser.Text.Trim(), txtPassword.Text);
        if (user is null)
        {
            MessageBox.Show(this, "Invalid user name or password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        AuthenticatedUser = user;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}

