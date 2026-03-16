using MySql.Data.MySqlClient;
using PGen.Models;
using System.Data;
using System.Threading.Tasks;

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
        public static async Task SaveRowsAsync(IEnumerable<MeterKeyRow> rows, string? sessionId = null)
        {
            if (rows is null) throw new ArgumentNullException(nameof(rows));

            var rowsList = rows.ToList();
            if (!rowsList.Any()) return;

            // ensure the database has a unique key on the key columns:
            // ALTER TABLE meter_key_rows ADD UNIQUE ux_keys_unique(ak8,ek8,ak32,ek32);
            // the INSERT ... ON DUPLICATE KEY UPDATE statement below relies on that index.

            await using var conn = await Database.CreateConnectionAsync();
            await using var tx = await conn.BeginTransactionAsync();
            await using var cmd = new MySqlCommand(
                "INSERT INTO meter_key_rows (session_id, msn, meter_type, model, set_index, ak8, ek8, ak32, ek32, po_number) " +
                "VALUES (@session, @msn, @meterType, @model, @setIndex, @ak8, @ek8, @ak32, @ek32, @poNumber) " +
                "ON DUPLICATE KEY UPDATE session_id = VALUES(session_id), msn = VALUES(msn), meter_type = VALUES(meter_type), model = VALUES(model), set_index = VALUES(set_index), po_number = VALUES(po_number)",
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
            cmd.Parameters.Add("@poNumber", MySqlDbType.VarChar, 50);

            // Use bulk insert for better performance
            foreach (var r in rowsList)
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
                cmd.Parameters["@poNumber"].Value = (object?)r.PoNumber ?? DBNull.Value;
                await cmd.ExecuteNonQueryAsync();
            }

            await tx.CommitAsync();
        }

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

            var rowsList = rows.ToList();
            if (!rowsList.Any()) return;

            // ensure the database has a unique key on the key columns:
            // ALTER TABLE meter_key_rows ADD UNIQUE ux_keys_unique(ak8,ek8,ak32,ek32);
            // the INSERT ... ON DUPLICATE KEY UPDATE statement below relies on that index.

            using var conn = Database.CreateConnection();
            using var tx = conn.BeginTransaction();
            using var cmd = new MySqlCommand(
                "INSERT INTO meter_key_rows (session_id, msn, meter_type, model, set_index, ak8, ek8, ak32, ek32, po_number) " +
                "VALUES (@session, @msn, @meterType, @model, @setIndex, @ak8, @ek8, @ak32, @ek32, @poNumber) " +
                "ON DUPLICATE KEY UPDATE session_id = VALUES(session_id), msn = VALUES(msn), meter_type = VALUES(meter_type), model = VALUES(model), set_index = VALUES(set_index), po_number = VALUES(po_number)",
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
            cmd.Parameters.Add("@poNumber", MySqlDbType.VarChar, 50);

            foreach (var r in rowsList)
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
                cmd.Parameters["@poNumber"].Value = (object?)r.PoNumber ?? DBNull.Value;
                cmd.ExecuteNonQuery();
            }

            tx.Commit();
        }

        /// <summary>
        /// Update an existing row in the database. Identifies the row by its keys (AK8,EK8,AK32,EK32).
        /// </summary>
        public static async Task UpdateRowAsync(MeterKeyRow originalRow, MeterKeyRow updatedRow)
        {
            if (originalRow is null) throw new ArgumentNullException(nameof(originalRow));
            if (updatedRow is null) throw new ArgumentNullException(nameof(updatedRow));
            
            await using var conn = await Database.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(
                "UPDATE meter_key_rows SET msn=@msn, meter_type=@meterType, model=@model, set_index=@setIndex, ak8=@ak8, ek8=@ek8, ak32=@ak32, ek32=@ek32, po_number=@poNumber " +
                "WHERE ak8=@oldAk8 AND ek8=@oldEk8 AND ak32=@oldAk32 AND ek32=@oldEk32", conn);
            
            cmd.Parameters.AddWithValue("@msn", updatedRow.Msn);
            cmd.Parameters.AddWithValue("@meterType", updatedRow.MeterType);
            cmd.Parameters.AddWithValue("@model", updatedRow.Model);
            cmd.Parameters.AddWithValue("@setIndex", updatedRow.SetIndex);
            cmd.Parameters.AddWithValue("@ak8", updatedRow.Ak8);
            cmd.Parameters.AddWithValue("@ek8", updatedRow.Ek8);
            cmd.Parameters.AddWithValue("@ak32", updatedRow.Ak32);
            cmd.Parameters.AddWithValue("@ek32", updatedRow.Ek32);
            cmd.Parameters.AddWithValue("@poNumber", (object?)updatedRow.PoNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@oldAk8", originalRow.Ak8);
            cmd.Parameters.AddWithValue("@oldEk8", originalRow.Ek8);
            cmd.Parameters.AddWithValue("@oldAk32", originalRow.Ak32);
            cmd.Parameters.AddWithValue("@oldEk32", originalRow.Ek32);
            
            await cmd.ExecuteNonQueryAsync();
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
                "UPDATE meter_key_rows SET msn=@msn, meter_type=@meterType, model=@model, set_index=@setIndex, ak8=@ak8, ek8=@ek8, ak32=@ak32, ek32=@ek32, po_number=@poNumber " +
                "WHERE ak8=@oldAk8 AND ek8=@oldEk8 AND ak32=@oldAk32 AND ek32=@oldEk32", conn);
            
            cmd.Parameters.AddWithValue("@msn", updatedRow.Msn);
            cmd.Parameters.AddWithValue("@meterType", updatedRow.MeterType);
            cmd.Parameters.AddWithValue("@model", updatedRow.Model);
            cmd.Parameters.AddWithValue("@setIndex", updatedRow.SetIndex);
            cmd.Parameters.AddWithValue("@ak8", updatedRow.Ak8);
            cmd.Parameters.AddWithValue("@ek8", updatedRow.Ek8);
            cmd.Parameters.AddWithValue("@ak32", updatedRow.Ak32);
            cmd.Parameters.AddWithValue("@ek32", updatedRow.Ek32);
            cmd.Parameters.AddWithValue("@poNumber", (object?)updatedRow.PoNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@oldAk8", originalRow.Ak8);
            cmd.Parameters.AddWithValue("@oldEk8", originalRow.Ek8);
            cmd.Parameters.AddWithValue("@oldAk32", originalRow.Ak32);
            cmd.Parameters.AddWithValue("@oldEk32", originalRow.Ek32);
            
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Delete a row from the database using its key fields (AK8,EK8,AK32,EK32).
        /// </summary>
        public static async Task DeleteRowAsync(MeterKeyRow row)
        {
            if (row is null) throw new ArgumentNullException(nameof(row));
            
            await using var conn = await Database.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(
                "DELETE FROM meter_key_rows WHERE ak8=@ak8 AND ek8=@ek8 AND ak32=@ak32 AND ek32=@ek32", conn);
            
            cmd.Parameters.AddWithValue("@ak8", row.Ak8);
            cmd.Parameters.AddWithValue("@ek8", row.Ek8);
            cmd.Parameters.AddWithValue("@ak32", row.Ak32);
            cmd.Parameters.AddWithValue("@ek32", row.Ek32);
            
            await cmd.ExecuteNonQueryAsync();
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
        /// Perform a bulk delete operation for multiple rows at once.
        /// </summary>
        public static async Task DeleteRowsAsync(IEnumerable<MeterKeyRow> rows)
        {
            if (rows is null) throw new ArgumentNullException(nameof(rows));
            
            var rowsList = rows.ToList();
            if (!rowsList.Any()) return;

            await using var conn = await Database.CreateConnectionAsync();
            await using var tx = await conn.BeginTransactionAsync();
            
            // Build a single DELETE statement with multiple OR conditions for better performance
            var conditions = rowsList.Select((r, index) => 
                $"(ak8=@ak8{index} AND ek8=@ek8{index} AND ak32=@ak32{index} AND ek32=@ek32{index})");
            
            await using var cmd = new MySqlCommand(
                $"DELETE FROM meter_key_rows WHERE {string.Join(" OR ", conditions)}", conn, tx);
            
            // Add parameters for each row
            for (int i = 0; i < rowsList.Count; i++)
            {
                var r = rowsList[i];
                cmd.Parameters.AddWithValue($"@ak8{i}", r.Ak8);
                cmd.Parameters.AddWithValue($"@ek8{i}", r.Ek8);
                cmd.Parameters.AddWithValue($"@ak32{i}", r.Ak32);
                cmd.Parameters.AddWithValue($"@ek32{i}", r.Ek32);
            }
            
            await cmd.ExecuteNonQueryAsync();
            await tx.CommitAsync();
        }

        /// <summary>
        /// Perform a bulk delete operation for multiple rows at once.
        /// </summary>
        public static void DeleteRows(IEnumerable<MeterKeyRow> rows)
        {
            if (rows is null) throw new ArgumentNullException(nameof(rows));
            
            var rowsList = rows.ToList();
            if (!rowsList.Any()) return;

            using var conn = Database.CreateConnection();
            using var tx = conn.BeginTransaction();
            
            // Build a single DELETE statement with multiple OR conditions for better performance
            var conditions = rowsList.Select((r, index) => 
                $"(ak8=@ak8{index} AND ek8=@ek8{index} AND ak32=@ak32{index} AND ek32=@ek32{index})");
            
            using var cmd = new MySqlCommand(
                $"DELETE FROM meter_key_rows WHERE {string.Join(" OR ", conditions)}", conn, tx);
            
            // Add parameters for each row
            for (int i = 0; i < rowsList.Count; i++)
            {
                var r = rowsList[i];
                cmd.Parameters.AddWithValue($"@ak8{i}", r.Ak8);
                cmd.Parameters.AddWithValue($"@ek8{i}", r.Ek8);
                cmd.Parameters.AddWithValue($"@ak32{i}", r.Ak32);
                cmd.Parameters.AddWithValue($"@ek32{i}", r.Ek32);
            }
            
            cmd.ExecuteNonQuery();
            tx.Commit();
        }

        /// <summary>
        /// Perform a quick-search query against the stored meter rows.
        /// </summary>
        public static async Task<IReadOnlyList<MeterKeyRow>> QueryRowsAsync(string filterField, string searchText)
        {
            var list = new List<MeterKeyRow>();
            if (string.IsNullOrWhiteSpace(searchText))
                return list;

            var pattern = "%" + searchText.Replace("%", "\\%") + "%";
            await using var conn = await Database.CreateConnectionAsync();
            await using var cmd = new MySqlCommand();
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
                case "PO Number":
                    where = "po_number LIKE @pat";
                    break;
                default:
                    where = "(msn LIKE @pat OR meter_type LIKE @pat OR ak8 LIKE @pat OR ek8 LIKE @pat OR ak32 LIKE @pat OR ek32 LIKE @pat OR po_number LIKE @pat)";
                    break;
            }

            cmd.CommandText = "SELECT msn,meter_type,model,set_index,ak8,ek8,ak32,ek32,po_number FROM meter_key_rows WHERE " + where + " ORDER BY created_utc DESC LIMIT 1000";
            cmd.Parameters.AddWithValue("@pat", pattern);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new MeterKeyRow
                {
                    Msn = reader.GetString("msn"),
                    MeterType = reader.GetString("meter_type"),
                    Model = reader.GetString("model"),
                    SetIndex = reader.GetInt32("set_index"),
                    Ak8 = await reader.IsDBNullAsync("ak8") ? string.Empty : reader.GetString("ak8"),
                    Ek8 = await reader.IsDBNullAsync("ek8") ? string.Empty : reader.GetString("ek8"),
                    Ak32 = await reader.IsDBNullAsync("ak32") ? string.Empty : reader.GetString("ak32"),
                    Ek32 = await reader.IsDBNullAsync("ek32") ? string.Empty : reader.GetString("ek32"),
                    PoNumber = await reader.IsDBNullAsync("po_number") ? null : reader.GetString("po_number"),
                });
            }

            return list;
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
                case "PO Number":
                    where = "po_number LIKE @pat";
                    break;
                default:
                    where = "(msn LIKE @pat OR meter_type LIKE @pat OR ak8 LIKE @pat OR ek8 LIKE @pat OR ak32 LIKE @pat OR ek32 LIKE @pat OR po_number LIKE @pat)";
                    break;
            }

            cmd.CommandText = "SELECT msn,meter_type,model,set_index,ak8,ek8,ak32,ek32,po_number FROM meter_key_rows WHERE " + where + " ORDER BY created_utc DESC LIMIT 1000";
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
                    PoNumber = reader.IsDBNull("po_number") ? null : reader.GetString("po_number"),
                });
            }

            return list;
        }
    }
}
