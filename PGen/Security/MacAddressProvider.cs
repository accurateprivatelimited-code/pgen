using System.Net.NetworkInformation;

namespace PGen.Security;

internal static class MacAddressProvider
{
    /// <summary>
    /// Retrieves the list of MAC addresses that will be used to compute the machine ID.
    ///
    /// Prior to this change the code only included adapters that were in the <c>Up</c>
    /// operational state.  On machines with multiple network interfaces (for example
    /// wired + Wi‑Fi or VPN adapters) switching networks would toggle an adapter's
    /// status, which in turn mutated the set of MACs and produced a different
    /// machine‑ID hash.  The result was that the application claimed the machine
    /// was unlicensed whenever the user changed their connection.
    ///
    /// The new logic ignores the operational status entirely and instead filters out
    /// loopbacks, tunnels and known *virtual* adapters.  Physical hardware addresses
    /// tend to be stable even when the machine is offline, ensuring the computed ID
    /// does not change simply because the internet connection changed.
    /// </summary>
    public static IReadOnlyList<string> GetCandidateMacAddresses()
    {
        return NetworkInterface.GetAllNetworkInterfaces()
            .Where(nic =>
                nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                !IsVirtualAdapter(nic))
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(s => s, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static bool IsVirtualAdapter(NetworkInterface nic)
    {
        // crude but effective filter – any description mentioning "virtual" or the
        // common vendor strings for VMs/Hyper-V are excluded.  This keeps the hash
        // deterministic across network state changes while ignoring adapters that
        // are not really part of the host hardware.
        var desc = nic.Description?.ToLowerInvariant() ?? string.Empty;
        return desc.Contains("virtual") ||
               desc.Contains("vmware") ||
               desc.Contains("hyper-v") ||
               desc.Contains("microsoft") && desc.Contains("virtual");
    }
}

