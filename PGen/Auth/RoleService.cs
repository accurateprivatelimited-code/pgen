using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;
using PGen.Data;

namespace PGen.Auth;

internal static class RoleService
{
    public static void EnsureDefaultRoles()
    {
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("sp_EnsureDefaultRoles", conn) { CommandType = CommandType.StoredProcedure };
        cmd.ExecuteNonQuery();
    }

    public static async Task EnsureDefaultRolesAsync()
    {
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("sp_EnsureDefaultRoles", conn) { CommandType = CommandType.StoredProcedure };
        await cmd.ExecuteNonQueryAsync();
    }

    public static IReadOnlyList<Role> GetRoles()
    {
        var list = new List<Role>();
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("SELECT id,name,rights,description,created_utc FROM roles ORDER BY name", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Role
            {
                Id = reader.GetString("id"),
                Name = reader.GetString("name"),
                Rights = (UserRight)reader.GetInt64("rights"),
                Description = reader.IsDBNull("description") ? null : reader.GetString("description"),
                CreatedUtc = reader.GetDateTime("created_utc")
            });
        }
        return list;
    }

    public static async Task<IReadOnlyList<Role>> GetRolesAsync()
    {
        var list = new List<Role>();
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("SELECT id,name,rights,description,created_utc FROM roles ORDER BY name", conn);
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new Role
            {
                Id = reader.GetString("id"),
                Name = reader.GetString("name"),
                Rights = (UserRight)reader.GetInt64("rights"),
                Description = await reader.IsDBNullAsync("description") ? null : reader.GetString("description"),
                CreatedUtc = reader.GetDateTime("created_utc")
            });
        }
        return list;
    }

    public static Role? GetRole(string roleId)
    {
        if (string.IsNullOrEmpty(roleId))
            return null;
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("SELECT id,name,rights,description,created_utc FROM roles WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("@id", roleId);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Role
            {
                Id = reader.GetString("id"),
                Name = reader.GetString("name"),
                Rights = (UserRight)reader.GetInt64("rights"),
                Description = reader.IsDBNull("description") ? null : reader.GetString("description"),
                CreatedUtc = reader.GetDateTime("created_utc")
            };
        }
        return null;
    }

    public static async Task<Role?> GetRoleAsync(string roleId)
    {
        if (string.IsNullOrEmpty(roleId))
            return null;
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("SELECT id,name,rights,description,created_utc FROM roles WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("@id", roleId);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Role
            {
                Id = reader.GetString("id"),
                Name = reader.GetString("name"),
                Rights = (UserRight)reader.GetInt64("rights"),
                Description = await reader.IsDBNullAsync("description") ? null : reader.GetString("description"),
                CreatedUtc = reader.GetDateTime("created_utc")
            };
        }
        return null;
    }

    public static void CreateRole(string name, UserRight rights, string? description = null)
    {
        name = (name ?? string.Empty).Trim();
        if (name.Length == 0)
            throw new ArgumentException("Role name is required.");
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("sp_CreateRole", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_name", name);
        cmd.Parameters.AddWithValue("p_rights", (long)rights);
        cmd.Parameters.AddWithValue("p_description", description);
        cmd.ExecuteNonQuery();
    }

    public static async Task CreateRoleAsync(string name, UserRight rights, string? description = null)
    {
        name = (name ?? string.Empty).Trim();
        if (name.Length == 0)
            throw new ArgumentException("Role name is required.");
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("sp_CreateRole", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_name", name);
        cmd.Parameters.AddWithValue("p_rights", (long)rights);
        cmd.Parameters.AddWithValue("p_description", description);
        await cmd.ExecuteNonQueryAsync();
    }

    public static void UpdateRole(string roleId, string name, UserRight rights, string? description = null)
    {
        roleId = (roleId ?? string.Empty).Trim();
        name = (name ?? string.Empty).Trim();
        if (roleId.Length == 0)
            throw new ArgumentException("Role ID is required.");
        if (name.Length == 0)
            throw new ArgumentException("Role name is required.");
        if (string.Equals(roleId, "admin", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(roleId, "operator", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot modify built-in roles.");
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("sp_UpdateRole", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_id", roleId);
        cmd.Parameters.AddWithValue("p_name", name);
        cmd.Parameters.AddWithValue("p_rights", (long)rights);
        cmd.Parameters.AddWithValue("p_description", description);
        cmd.ExecuteNonQuery();
    }

    public static async Task UpdateRoleAsync(string roleId, string name, UserRight rights, string? description = null)
    {
        roleId = (roleId ?? string.Empty).Trim();
        name = (name ?? string.Empty).Trim();
        if (roleId.Length == 0)
            throw new ArgumentException("Role ID is required.");
        if (name.Length == 0)
            throw new ArgumentException("Role name is required.");
        if (string.Equals(roleId, "admin", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(roleId, "operator", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot modify built-in roles.");
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("sp_UpdateRole", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_id", roleId);
        cmd.Parameters.AddWithValue("p_name", name);
        cmd.Parameters.AddWithValue("p_rights", (long)rights);
        cmd.Parameters.AddWithValue("p_description", description);
        await cmd.ExecuteNonQueryAsync();
    }

    public static void DeleteRole(string roleId)
    {
        roleId = (roleId ?? string.Empty).Trim();
        if (roleId.Length == 0)
            throw new ArgumentException("Role ID is required.");
        if (string.Equals(roleId, "admin", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(roleId, "operator", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot delete built-in roles.");
        using var conn = Database.CreateConnection();
        using var cmd = new MySqlCommand("sp_DeleteRole", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_id", roleId);
        cmd.ExecuteNonQuery();
    }

    public static async Task DeleteRoleAsync(string roleId)
    {
        roleId = (roleId ?? string.Empty).Trim();
        if (roleId.Length == 0)
            throw new ArgumentException("Role ID is required.");
        if (string.Equals(roleId, "admin", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(roleId, "operator", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Cannot delete built-in roles.");
        await using var conn = await Database.CreateConnectionAsync();
        await using var cmd = new MySqlCommand("sp_DeleteRole", conn) { CommandType = CommandType.StoredProcedure };
        cmd.Parameters.AddWithValue("p_id", roleId);
        await cmd.ExecuteNonQueryAsync();
    }
}
