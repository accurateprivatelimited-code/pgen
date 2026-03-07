using System.Security.Cryptography;
using System.Text;

namespace PGen.Security;

internal static class MachineId
{
    public static string ComputeMachineIdHex()
    {
        var macs = MacAddressProvider.GetCandidateMacAddresses();
        var input = macs.Count == 0 ? "NO_MAC" : string.Join("|", macs);

        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hash);
    }
}

