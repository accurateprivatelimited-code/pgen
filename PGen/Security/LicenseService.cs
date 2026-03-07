using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Data;

namespace PGen.Security;

internal static class LicenseService
{
    // NOTE: Replace this with your own secret and/or online handshake.
    // For offline use, this encrypts/decrypts a small JSON payload.
    private const string AppSecret = "PGen::OfflineLicense::ReplaceMeWithARealSecret";

    private static byte[] DeriveKey()
    {
        using var sha = SHA256.Create();
        return sha.ComputeHash(Encoding.UTF8.GetBytes(AppSecret));
    }

    public static LicenseFile? TryReadLicense(string licensePath)
    {
        if (!File.Exists(licensePath))
            return null;

        var blob = File.ReadAllBytes(licensePath);
        if (blob.Length < 12 + 16 + 1)
            return null;

        var nonce = blob.AsSpan(0, 12).ToArray();
        var tag = blob.AsSpan(12, 16).ToArray();
        var ciphertext = blob.AsSpan(28).ToArray();

        var key = DeriveKey();
        var plaintext = new byte[ciphertext.Length];

        try
        {
            using var aes = new AesGcm(key, 16);
            aes.Decrypt(nonce, ciphertext, tag, plaintext);
            var json = Encoding.UTF8.GetString(plaintext);
            return JsonSerializer.Deserialize<LicenseFile>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch
        {
            return null;
        }
    }

    public static void WriteLicense(string licensePath, LicenseFile license)
    {
        // persist file on disk first (unchanged behaviour)
        var json = JsonSerializer.Serialize(license, new JsonSerializerOptions { WriteIndented = true });
        var plaintext = Encoding.UTF8.GetBytes(json);

        var key = DeriveKey();
        var nonce = RandomNumberGenerator.GetBytes(12);
        var ciphertext = new byte[plaintext.Length];
        var tag = new byte[16];

        using (var aes = new AesGcm(key, 16))
        {
            aes.Encrypt(nonce, plaintext, ciphertext, tag);
        }

        var blob = new byte[nonce.Length + tag.Length + ciphertext.Length];
        Buffer.BlockCopy(nonce, 0, blob, 0, nonce.Length);
        Buffer.BlockCopy(tag, 0, blob, nonce.Length, tag.Length);
        Buffer.BlockCopy(ciphertext, 0, blob, nonce.Length + tag.Length, ciphertext.Length);

        Directory.CreateDirectory(Path.GetDirectoryName(licensePath)!);
        File.WriteAllBytes(licensePath, blob);

        // also record metadata in database
        try
        {
            using var conn = PGen.Data.Database.CreateConnection();
            using var cmd = new MySql.Data.MySqlClient.MySqlCommand("sp_CreateLicense", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("p_version", license.Version);
            cmd.Parameters.AddWithValue("p_generated_for", license.GeneratedFor);
            cmd.Parameters.AddWithValue("p_machine_id", license.AllowedMachineIds != null && license.AllowedMachineIds.Length>0 ? license.AllowedMachineIds[0] : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("p_expires_utc", license.ExpiresUtc.HasValue ? license.ExpiresUtc.Value : (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }
        catch
        {
            // swallow DB errors so that file write still succeeds
        }
    }
}

