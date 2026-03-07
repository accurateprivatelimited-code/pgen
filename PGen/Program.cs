using System.Runtime.Versioning;
using System.Windows.Forms;
using PGen.Auth;
using PGen.Security;

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
            AuthService.EnsureDefaultUsers();

            while (true)
            {
                using var login = new LoginForm();
                if (login.ShowDialog() != DialogResult.OK || login.AuthenticatedUser is null)
                    return;

                var user = login.AuthenticatedUser;

                // perform license validation after successful login so that an
                // administrator can see the machine ID and, if necessary, issue a
                // license for the current or any other machine.
                var licenseResult = LicenseValidator.ValidateOrExplain();
                bool isAdmin = AuthService.HasRight(user, UserRight.ManageLicenses);
                if (!licenseResult.IsValid)
                {
                    MessageBox.Show(
                        licenseResult.Message,
                        "PGen - License Check",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    if (isAdmin)
                    {
                        var ask = MessageBox.Show(
                            "You are an administrator and may create a license for this or another machine.\r\n" +
                            "Do you want to open the license creation dialog?",
                            "Create License?",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (ask == DialogResult.Yes)
                        {
                            using var dlg = new LicenseAdminForm(user);
                            dlg.ShowDialog();
                            // re-check after potentially writing a license file
                            licenseResult = LicenseValidator.ValidateOrExplain();
                        }
                    }
                }

                if (!licenseResult.IsValid && !isAdmin)
                {
                    // non-admin on an unlicensed machine cannot proceed
                    return;
                }

                using var main = new Form1(user);
                var result = main.ShowDialog();
                if (result != DialogResult.Retry)
                    break; // normal close
                // Retry means logout → loop back to login
            }
        }
    }
}
