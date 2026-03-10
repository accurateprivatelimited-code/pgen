using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PGen.Data
{
    internal static class MeterTypeRepository
    {
        /// <summary>
        /// Returns all active meter types ordered by sort_order, then name.
        /// </summary>
        public static List<string> GetMeterTypes()
        {
            var list = new List<string>();
            using var conn = Database.CreateConnection();
            using var cmd = new MySqlCommand(
                "SELECT name FROM meter_types WHERE is_active = 1 ORDER BY sort_order, name",
                conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }
            return list;
        }

        /// <summary>
        /// Returns all active meter types ordered by sort_order, then name.
        /// </summary>
        public static async Task<List<string>> GetMeterTypesAsync()
        {
            var list = new List<string>();
            await using var conn = await Database.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(
                "SELECT name FROM meter_types WHERE is_active = 1 ORDER BY sort_order, name",
                conn);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(reader.GetString(0));
            }
            return list;
        }
    }
}
