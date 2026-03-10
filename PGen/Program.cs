using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using PGen.Auth;
using PGen.Security;
using PGen.Forms;

namespace PGen
{
    internal static class Program
    {
        [STAThread]
        [SupportedOSPlatform("windows")]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // initial user setup (creates admin account if needed)
            AuthService.EnsureDefaultUsersAsync().GetAwaiter().GetResult();

            // validate license before showing login form
            var licenseResult = LicenseValidator.ValidateOrExplain();
            if (!licenseResult.IsValid)
            {
                using var dialog = new LicenseErrorDialog(licenseResult.Message, MachineId.ComputeMachineIdHex());
                dialog.ShowDialog();
                return; // no license, exit application
            }

            while (true)
            {
                using var login = new LoginForm();
                if (login.ShowDialog() != DialogResult.OK || login.AuthenticatedUser is null)
                    return;

                var user = login.AuthenticatedUser;

                try
                {
                    using var main = new Form1(user);
                    var result = main.ShowDialog();
                    if (result != DialogResult.Retry)
                        break; // normal close
                    // Retry means logout → loop back to login
                }
                catch (ObjectDisposedException)
                {
                    // Form disposed during logout/close flow; return to login
                    continue;
                }
            }
        }
    }
}
