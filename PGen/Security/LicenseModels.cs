namespace PGen.Security;

internal sealed class LicenseFile
{
    public required string Version { get; init; }
    public required string[] AllowedMachineIds { get; init; }
    public DateTime? ExpiresUtc { get; init; }

    // user for whom the license was generated (optional)
    public string? GeneratedFor { get; init; }
}

internal readonly record struct LicenseValidationResult(bool IsValid, string Message);

