using MySql.Data.MySqlClient;
using PGen.Models;
using System.Data;

namespace PGen.Data
{
    internal static class MeterKeyRepository
    {
        /// <summary>
        /// Persists a set of <see cref="MeterKeyRow"/> items into the
        /// <c>meter_key_rows</c> table.  The operation is performed inside a
        /// single transaction; any failure will roll back the entire batch.
        /// </summary>
        /// <param name="rows">Rows to save.  May be empty.</param>
        /// <param name="sessionId">
        /// Optional session identifier, which can be used to group a single
        /// generation run.
        /// </param>
        public static void SaveRows(IEnumerable<MeterKeyRow> rows, string? sessionId = null)
        {
            if (rows is null) throw new ArgumentNullException(nameof(rows));

            // ensure the database has a unique key on the key columns:
            // ALTER TABLE meter_key_rows ADD UNIQUE ux_keys_unique(ak8,ek8,ak32,ek32);
            // the INSERT ... ON DUPLICATE KEY UPDATE statement below relies on that index.

            using var conn = Database.CreateConnection();
            using var tx = conn.BeginTransaction();
            using var cmd = new MySqlCommand(
                "INSERT INTO meter_key_rows (session_id, msn, meter_type, model, set_index, ak8, ek8, ak32, ek32) " +
                "VALUES (@session, @msn, @meterType, @model, @setIndex, @ak8, @ek8, @ak32, @ek32) " +
                "ON DUPLICATE KEY UPDATE session_id = VALUES(session_id), msn = VALUES(msn), meter_type = VALUES(meter_type), model = VALUES(model), set_index = VALUES(set_index)",
                conn, tx);

            cmd.Parameters.Add("@session", MySqlDbType.VarChar, 36);
            cmd.Parameters.Add("@msn", MySqlDbType.VarChar, 50);
            cmd.Parameters.Add("@meterType", MySqlDbType.VarChar, 50);
            cmd.Parameters.Add("@model", MySqlDbType.VarChar, 50);
            cmd.Parameters.Add("@setIndex", MySqlDbType.Int32);
            cmd.Parameters.Add("@ak8", MySqlDbType.VarChar, 16);
            cmd.Parameters.Add("@ek8", MySqlDbType.VarChar, 16);
            cmd.Parameters.Add("@ak32", MySqlDbType.VarChar, 64);
            cmd.Parameters.Add("@ek32", MySqlDbType.VarChar, 64);

            foreach (var r in rows)
            {
                cmd.Parameters["@session"].Value = (object?)sessionId ?? DBNull.Value;
                cmd.Parameters["@msn"].Value = r.Msn;
                cmd.Parameters["@meterType"].Value = r.MeterType;
                cmd.Parameters["@model"].Value = r.Model;
                cmd.Parameters["@setIndex"].Value = r.SetIndex;
                cmd.Parameters["@ak8"].Value = r.Ak8;
                cmd.Parameters["@ek8"].Value = r.Ek8;
                cmd.Parameters["@ak32"].Value = r.Ak32;
                cmd.Parameters["@ek32"].Value = r.Ek32;
                cmd.ExecuteNonQuery();
            }

            tx.Commit();
        }

        /// <summary>
        /// Update an existing row in the database. Identifies the row by its keys (AK8,EK8,AK32,EK32).
        /// </summary>
        public static void UpdateRow(MeterKeyRow originalRow, MeterKeyRow updatedRow)
        {
            if (originalRow is null) throw new ArgumentNullException(nameof(originalRow));
            if (updatedRow is null) throw new ArgumentNullException(nameof(updatedRow));
            using var conn = Database.CreateConnection();
            using var cmd = new MySqlCommand(
                "UPDATE meter_key_rows SET msn=@msn, meter_type=@meterType, model=@model, set_index=@setIndex, ak8=@ak8, ek8=@ek8, ak32=@ak32, ek32=@ek32 " +
                "WHERE ak8=@oldAk8 AND ek8=@oldEk8 AND ak32=@oldAk32 AND ek32=@oldEk32", conn);
            cmd.Parameters.AddWithValue("@msn", updatedRow.Msn);
            cmd.Parameters.AddWithValue("@meterType", updatedRow.MeterType);
            cmd.Parameters.AddWithValue("@model", updatedRow.Model);
            cmd.Parameters.AddWithValue("@setIndex", updatedRow.SetIndex);
            cmd.Parameters.AddWithValue("@ak8", updatedRow.Ak8);
            cmd.Parameters.AddWithValue("@ek8", updatedRow.Ek8);
            cmd.Parameters.AddWithValue("@ak32", updatedRow.Ak32);
            cmd.Parameters.AddWithValue("@ek32", updatedRow.Ek32);
            cmd.Parameters.AddWithValue("@oldAk8", originalRow.Ak8);
            cmd.Parameters.AddWithValue("@oldEk8", originalRow.Ek8);
            cmd.Parameters.AddWithValue("@oldAk32", originalRow.Ak32);
            cmd.Parameters.AddWithValue("@oldEk32", originalRow.Ek32);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Delete a row from the database using its key fields (AK8,EK8,AK32,EK32).
        /// </summary>
        public static void DeleteRow(MeterKeyRow row)
        {
            if (row is null) throw new ArgumentNullException(nameof(row));
            using var conn = Database.CreateConnection();
            using var cmd = new MySqlCommand(
                "DELETE FROM meter_key_rows WHERE ak8=@ak8 AND ek8=@ek8 AND ak32=@ak32 AND ek32=@ek32", conn);
            cmd.Parameters.AddWithValue("@ak8", row.Ak8);
            cmd.Parameters.AddWithValue("@ek8", row.Ek8);
            cmd.Parameters.AddWithValue("@ak32", row.Ak32);
            cmd.Parameters.AddWithValue("@ek32", row.Ek32);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Perform a quick-search query against the stored meter rows.
        /// </summary>
        public static IReadOnlyList<MeterKeyRow> QueryRows(string filterField, string searchText)
        {
            var list = new List<MeterKeyRow>();
            if (string.IsNullOrWhiteSpace(searchText))
                return list;

            var pattern = "%" + searchText.Replace("%", "\\%") + "%";
            using var conn = Database.CreateConnection();
            using var cmd = new MySqlCommand();
            cmd.Connection = conn;

            string where;
            switch (filterField)
            {
                case "MSN":
                    where = "msn LIKE @pat";
                    break;
                case "Type":
                    where = "meter_type LIKE @pat";
                    break;
                case "AK8":
                    where = "ak8 LIKE @pat";
                    break;
                case "EK8":
                    where = "ek8 LIKE @pat";
                    break;
                case "AK32":
                    where = "ak32 LIKE @pat";
                    break;
                case "EK32":
                    where = "ek32 LIKE @pat";
                    break;
                default:
                    where = "(msn LIKE @pat OR meter_type LIKE @pat OR ak8 LIKE @pat OR ek8 LIKE @pat OR ak32 LIKE @pat OR ek32 LIKE @pat)";
                    break;
            }

            cmd.CommandText = "SELECT msn,meter_type,model,set_index,ak8,ek8,ak32,ek32 FROM meter_key_rows WHERE " + where + " ORDER BY created_utc DESC LIMIT 1000";
            cmd.Parameters.AddWithValue("@pat", pattern);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new MeterKeyRow
                {
                    Msn = reader.GetString("msn"),
                    MeterType = reader.GetString("meter_type"),
                    Model = reader.GetString("model"),
                    SetIndex = reader.GetInt32("set_index"),
                    Ak8 = reader.IsDBNull("ak8") ? string.Empty : reader.GetString("ak8"),
                    Ek8 = reader.IsDBNull("ek8") ? string.Empty : reader.GetString("ek8"),
                    Ak32 = reader.IsDBNull("ak32") ? string.Empty : reader.GetString("ak32"),
                    Ek32 = reader.IsDBNull("ek32") ? string.Empty : reader.GetString("ek32"),
                });
            }

            return list;
        }
    }
}