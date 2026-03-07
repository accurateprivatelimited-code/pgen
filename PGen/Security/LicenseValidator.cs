namespace PGen.Security;

internal static class LicenseValidator
{
    public const string LicenseFileName = "license.bin";

    public static LicenseValidationResult ValidateOrExplain()
    {
        var licensePath = Path.Combine(AppContext.BaseDirectory, LicenseFileName);
        var license = LicenseService.TryReadLicense(licensePath);
        if (license is null)
        {
            return new LicenseValidationResult(
                IsValid: false,
                Message:
                    "License file not found or invalid.\r\n\r\n" +
                    $"Machine ID:\r\n{MachineId.ComputeMachineIdHex()}\r\n\r\n" +
                    $"Place a valid '{LicenseFileName}' next to the EXE.");
        }

        if (license.AllowedMachineIds is null || license.AllowedMachineIds.Length == 0)
            return new LicenseValidationResult(false, "License has no allowed machines.");

        if (license.ExpiresUtc is not null && DateTime.UtcNow > license.ExpiresUtc.Value)
            return new LicenseValidationResult(false, "License is expired.");

        var machineId = MachineId.ComputeMachineIdHex();
        if (!license.AllowedMachineIds.Contains(machineId, StringComparer.OrdinalIgnoreCase))
            return new LicenseValidationResult(false, "This machine is not licensed.\r\n\r\nMachine ID:\r\n" + machineId);

        return new LicenseValidationResult(true, "OK");
    }
}

