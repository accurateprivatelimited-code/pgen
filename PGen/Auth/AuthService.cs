using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;
using PGen.Data;

namespace PGen.Auth;

internal static class AuthService
{
    // migrate to database-backed storage, previous JSON implementation removed.

    public static void EnsureDefaultUsers()
    {
        RoleService.EnsureDefaultRoles();
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("sp_EnsureDefaultUsers", conn) { CommandType = CommandType.StoredProcedure };
        cmd.ExecuteNonQuery();
    }

    public static async Task EnsureDefaultUsersAsync()
    {
        await RoleService.EnsureDefaultRolesAsync();
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("sp_EnsureDefaultUsers", conn) { CommandType = CommandType.StoredProcedure };
        await cmd.ExecuteNonQueryAsync();
    }

    public static UserAccount? Authenticate(string username, string password)
    {
        var hash = HashPassword(password);
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("sp_AuthenticateUser", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_username", username);
        cmd.Parameters.AddWithValue("p_password_hash", hash);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new UserAccount
            {
                UserName = reader.GetString("username"),
                PasswordHash = hash,
                RoleId = reader.GetString("role_id")
            };
        }
        return null;
    }

    public static async Task<UserAccount?> AuthenticateAsync(string username, string password)
    {
        var hash = HashPassword(password);
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("sp_AuthenticateUser", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_username", username);
        cmd.Parameters.AddWithValue("p_password_hash", hash);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new UserAccount
            {
                UserName = reader.GetString("username"),
                PasswordHash = hash,
                RoleId = reader.GetString("role_id")
            };
        }
        return null;
    }

    public static IReadOnlyList<UserAccount> GetUsers()
    {
        var list = new List<UserAccount>();
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("SELECT username,password_hash,role_id FROM users ORDER BY role_id,username", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new UserAccount
            {
                UserName = reader.GetString("username"),
                PasswordHash = reader.GetString("password_hash"),
                RoleId = reader.GetString("role_id")
            });
        }
        return list;
    }

    public static async Task<IReadOnlyList<UserAccount>> GetUsersAsync()
    {
        var list = new List<UserAccount>();
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("SELECT username,password_hash,role_id FROM users ORDER BY role_id,username", conn);
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new UserAccount
            {
                UserName = reader.GetString("username"),
                PasswordHash = reader.GetString("password_hash"),
                RoleId = reader.GetString("role_id")
            });
        }
        return list;
    }

    public static void AddUser(string username, string password, string roleId)
    {
        username = (username ?? string.Empty).Trim();
        roleId = (roleId ?? string.Empty).Trim();

        if (username.Length == 0)
            throw new ArgumentException("User name is required.");
        if (password.Length == 0)
            throw new ArgumentException("Password is required.");
        if (roleId.Length == 0)
            throw new ArgumentException("Role is required.");

        var role = RoleService.GetRole(roleId);
        if (role is null)
            throw new InvalidOperationException("Selected role does not exist.");

        var hash = HashPassword(password);
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("sp_AddUser", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_username", username);
        cmd.Parameters.AddWithValue("p_password_hash", hash);
        cmd.Parameters.AddWithValue("p_role_id", roleId);
        cmd.ExecuteNonQuery();
    }

    public static async Task AddUserAsync(string username, string password, string roleId)
    {
        username = (username ?? string.Empty).Trim();
        roleId = (roleId ?? string.Empty).Trim();

        if (username.Length == 0)
            throw new ArgumentException("User name is required.");
        if (password.Length == 0)
            throw new ArgumentException("Password is required.");
        if (roleId.Length == 0)
            throw new ArgumentException("Role is required.");

        var role = await RoleService.GetRoleAsync(roleId);
        if (role is null)
            throw new InvalidOperationException("Selected role does not exist.");

        var hash = HashPassword(password);
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("sp_AddUser", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_username", username);
        cmd.Parameters.AddWithValue("p_password_hash", hash);
        cmd.Parameters.AddWithValue("p_role_id", roleId);
        await cmd.ExecuteNonQueryAsync();
    }

    public static void UpdateUserRole(string username, string newRoleId)
    {
        username = (username ?? string.Empty).Trim();
        newRoleId = (newRoleId ?? string.Empty).Trim();

        if (username.Length == 0)
            throw new ArgumentException("User name is required.");
        if (newRoleId.Length == 0)
            throw new ArgumentException("Role is required.");

        var role = RoleService.GetRole(newRoleId);
        if (role is null)
            throw new InvalidOperationException("Selected role does not exist.");

        if (string.Equals(username, "admin", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot change the role of the admin account.");

        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("sp_UpdateUserRole", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_username", username);
        cmd.Parameters.AddWithValue("p_new_role", newRoleId);
        cmd.ExecuteNonQuery();
    }

    public static async Task UpdateUserRoleAsync(string username, string newRoleId)
    {
        username = (username ?? string.Empty).Trim();
        newRoleId = (newRoleId ?? string.Empty).Trim();

        if (username.Length == 0)
            throw new ArgumentException("User name is required.");
        if (newRoleId.Length == 0)
            throw new ArgumentException("Role is required.");

        var role = await RoleService.GetRoleAsync(newRoleId);
        if (role is null)
            throw new InvalidOperationException("Selected role does not exist.");

        if (string.Equals(username, "admin", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot change the role of the admin account.");

        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("sp_UpdateUserRole", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_username", username);
        cmd.Parameters.AddWithValue("p_new_role", newRoleId);
        await cmd.ExecuteNonQueryAsync();
    }

    public static void ResetPassword(string username, string newPassword)
    {
        username = (username ?? string.Empty).Trim();
        if (username.Length == 0)
            throw new ArgumentException("User name is required.");
        if (newPassword.Length == 0)
            throw new ArgumentException("Password is required.");

        var hash = HashPassword(newPassword);
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("UPDATE users SET password_hash = @hash WHERE username = @user", conn);
        cmd.Parameters.AddWithValue("@hash", hash);
        cmd.Parameters.AddWithValue("@user", username);
        if (cmd.ExecuteNonQuery() == 0)
            throw new InvalidOperationException("User not found.");
    }

    public static async Task ResetPasswordAsync(string username, string newPassword)
    {
        username = (username ?? string.Empty).Trim();
        if (username.Length == 0)
            throw new ArgumentException("User name is required.");
        if (newPassword.Length == 0)
            throw new ArgumentException("Password is required.");

        var hash = HashPassword(newPassword);
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("UPDATE users SET password_hash = @hash WHERE username = @user", conn);
        cmd.Parameters.AddWithValue("@hash", hash);
        cmd.Parameters.AddWithValue("@user", username);
        if (await cmd.ExecuteNonQueryAsync() == 0)
            throw new InvalidOperationException("User not found.");
    }

    public static void DeleteUser(string username)
    {
        username = (username ?? string.Empty).Trim();
        if (username.Length == 0)
            throw new ArgumentException("User name is required.");
        if (string.Equals(username, "admin", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot delete the admin account.");

        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("sp_DeleteUser", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_username", username);
        cmd.ExecuteNonQuery();
    }

    public static async Task DeleteUserAsync(string username)
    {
        username = (username ?? string.Empty).Trim();
        if (username.Length == 0)
            throw new ArgumentException("User name is required.");
        if (string.Equals(username, "admin", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot delete the admin account.");

        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("sp_DeleteUser", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_username", username);
        await cmd.ExecuteNonQueryAsync();
    }

    public static bool HasRight(UserAccount user, UserRight right)
    {
        var role = RoleService.GetRole(user.RoleId);
        return role?.HasRight(right) ?? false;
    }

    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }
}

