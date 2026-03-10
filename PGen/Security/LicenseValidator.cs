using System;
using MySql.Data.MySqlClient;
using PGen.Data;

namespace PGen.Security;

internal static class LicenseValidator
{
    public static LicenseValidationResult ValidateOrExplain()
    {
        var machineId = MachineId.ComputeMachineIdHex();

        try
        {
            using var conn = Database.CreateConnection();
            using var cmd = new MySqlCommand(
                "SELECT version, generated_for, machine_id, expires_utc " +
                "FROM licenses " +
                "WHERE machine_id = @machineId " +
                "ORDER BY expires_utc DESC " +
                "LIMIT 1",
                conn);

            cmd.Parameters.AddWithValue("@machineId", machineId);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return new LicenseValidationResult(
                    IsValid: false,
                    Message:
                        "No license found for this machine.\r\n\r\n" +
                        $"Machine ID:\r\n{machineId}");
            }

            int expiresIndex = reader.GetOrdinal("expires_utc");
            if (!reader.IsDBNull(expiresIndex))
            {
                var expiresUtc = reader.GetDateTime(expiresIndex);
                if (DateTime.UtcNow > expiresUtc)
                {
                    return new LicenseValidationResult(false, "License is expired.");
                }
            }

            return new LicenseValidationResult(true, "OK");
        }
        catch (Exception ex)
        {
            return new LicenseValidationResult(
                IsValid: false,
                Message:
                    "Error while validating license from database.\r\n\r\n" +
                    ex.Message + "\r\n\r\n" +
                    $"Machine ID:\r\n{machineId}");
        }
    }
}

