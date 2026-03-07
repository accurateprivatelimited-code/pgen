using System.Configuration;
using MySql.Data.MySqlClient;

namespace PGen.Data
{
    internal static class Database
    {
        /// <summary>
        /// Connection string read from App.config.
        /// </summary>
        public static string ConnectionString
            => ConfigurationManager.ConnectionStrings["PGenDb"]?.ConnectionString
               ?? throw new InvalidOperationException("Connection string 'PGenDb' not found.");

        /// <summary>
        /// Returns an open MySqlConnection instance. Caller is responsible for disposal.
        /// </summary>
        public static async Task<MySqlConnection> CreateConnectionAsync()
        {
            var conn = new MySqlConnection(ConnectionString);
            await conn.OpenAsync();
            return conn;
        }

        /// <summary>
        /// Returns an open MySqlConnection instance. Caller is responsible for disposal.
        /// </summary>
        public static MySqlConnection CreateConnection()
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}