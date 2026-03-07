using System.ComponentModel;
using System.Data;
using System.Runtime.Versioning;
using PGen.Export;
using PGen.Models;
using PGen.Security;
using PGen.Services;
using PGen.Auth;
using PGen.Data;

namespace PGen
{
    [SupportedOSPlatform("windows")]
    public partial class Form1 : Form
    {
        private readonly PasswordGeneratorService _generator = new();
        private readonly BindingList<MeterKeyRow> _rows = new();
        private List<MeterKeyGroupRow> _groupedRowsForActions = new();
        private const int MaxSetColumns = 50;
        private CancellationTokenSource? _cts;
        private readonly UserAccount _currentUser;

        public Form1(UserAccount currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();

            dgvResults.AutoGenerateColumns = false;

            cboMeterType.Items.AddRange(new object[] {
                "1P GPRS",
                "3P WC GPRS UNI",
                "3P WC GPRS BI",
                "3P WC NO AMR UNI",
                "3P WC NO AMR BI",
                "LT/HT GPRS UNI",
                "LT/HT GPRS BI",
                "LT NON AMR",
                "HT NON AMR",
                "SMCD",
                "P202",
                "1P NON AMR"
            });
            cboMeterType.SelectedIndex = 0;



            cboFilterField.Items.AddRange(new object[] { "All Fields", "MSN", "Type", "AK8", "EK8", "AK32", "EK32" });
            cboFilterField.SelectedIndex = 0;

            numSets.Value = 1;

            lblMachineId.Text = MachineId.ComputeMachineIdHex();
            var role = RoleService.GetRole(_currentUser.RoleId);
            toolUser.Text = $"User: {_currentUser.UserName} ({role?.Name ?? "Unknown"})";

            // menus (will also be affected by control tags below)
            // explicit checks retained for clarity but not strictly necessary.
            menuCreateLicense.Enabled = AuthService.HasRight(_currentUser, UserRight.ManageLicenses);
            menuManageUsers.Enabled = AuthService.HasRight(_currentUser, UserRight.CreateUsers) || AuthService.HasRight(_currentUser, UserRight.EditUsers) || AuthService.HasRight(_currentUser, UserRight.DeleteUsers);
            menuManageRoles.Enabled = AuthService.HasRight(_currentUser, UserRight.CreateRoles) ||
                                  AuthService.HasRight(_currentUser, UserRight.EditRoles) ||
                                  AuthService.HasRight(_currentUser, UserRight.DeleteRoles);

            // if the user only has one section available, jump there right away
            NavigateInitialForm();

            ApplyFilter();
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (_cts is not null)
                return;

            try
            {
                var msns = MsnRangeParser.Parse(txtMsnOrRange.Text);
                var meterType = (cboMeterType.SelectedItem?.ToString() ?? string.Empty).Trim();
                var sets = (int)numSets.Value;

                if (string.IsNullOrWhiteSpace(meterType))
                    throw new ArgumentException("Meter Type is required.");

                SetBusy(true);
                toolStatus.Text = $"Generating {msns.Count * sets:N0} rows…";
                progressBar.Value = 0;

                _cts = new CancellationTokenSource();
                var progress = new Progress<int>(p =>
                {
                    progressBar.Value = Math.Clamp(p, 0, 100);
                    toolStatus.Text = $"Generating… {progressBar.Value}%";
                });

                var rows = await Task.Run(() => _generator.Generate(
                    msns,
                    meterType,
                    string.Empty,
                    setsPerMsn: sets,
                    progress: progress,
                    cancellationToken: _cts.Token));

                // Deduplicate: overwrite existing rows with same key (Msn, MeterType, Model, SetIndex)
                var deduped = DeduplicateRows(rows);
                _rows.RaiseListChangedEvents = false;
                _rows.Clear();
                foreach (var r in deduped)
                    _rows.Add(r);
                _rows.RaiseListChangedEvents = true;

                ApplyFilter();

                // persist rows to database; ignore failures so the UI stay responsive
                try
                {
                    var sessionId = Guid.NewGuid().ToString();
                    PGen.Data.MeterKeyRepository.SaveRows(deduped, sessionId);
                    toolStatus.Text = $"Done. {GroupRows(_rows).Count:N0} MSN(s), {_rows.Count:N0} keys (saved to DB)";
                }
                catch (Exception dbex)
                {
                    // log? for now just alert user, but allow normal operation
                    MessageBox.Show(this, "Warning: failed to save rows to database:\n" + dbex.Message,
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    toolStatus.Text = $"Done. {GroupRows(_rows).Count:N0} MSN(s) (DB save failed)";
                }
                progressBar.Value = 100;
            }
            catch (OperationCanceledException)
            {
                toolStatus.Text = "Canceled.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Generate Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStatus.Text = "Ready.";
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
                SetBusy(false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void btnCopySelected_Click(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows.Count == 0)
                return;

            var parts = new List<string>();
            foreach (DataGridViewRow row in dgvResults.SelectedRows)
            {
                var idx = row.Index;
                if (idx < 0 || idx >= _groupedRowsForActions.Count)
                    continue;
                var gr = _groupedRowsForActions[idx];
                foreach (var r in gr.Sets.OrderBy(s => s.SetIndex))
                    parts.Add($"{r.Msn}\t{r.Ak32}\t{r.Ek32}");
            }

            if (parts.Count == 0)
                return;

            parts.Reverse(); // DataGridView selected rows are reverse order
            Clipboard.SetText(string.Join(Environment.NewLine, parts));
            toolStatus.Text = $"Copied {parts.Count} key(s).";
        }

        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // protect generation-related actions from unauthorized users
            if (!AuthService.HasRight(_currentUser, UserRight.GeneratePasswords))
                return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (e.RowIndex >= _groupedRowsForActions.Count)
                return;
            var gr = _groupedRowsForActions[e.RowIndex];

            var columnName = dgvResults.Columns[e.ColumnIndex].Name;
            
            switch (columnName)
            {
                case "colCopy":
                    var copyLines = gr.Sets.OrderBy(s => s.SetIndex)
                        .Select(s => $"Set{s.SetIndex}: MSN={s.Msn}\r\nAK32={s.Ak32}\r\nEK32={s.Ek32}");
                    Clipboard.SetText(string.Join("\r\n\r\n", copyLines));
                    toolStatus.Text = $"Copied {gr.Sets.Count} set(s) to clipboard.";
                    break;
                    
                case "colEdit":
                    EditGroupRow(gr);
                    break;
                    
                case "colDelete":
                    DeleteGroupRow(gr);
                    break;
            }
        }

        private void btnExport8_Click(object sender, EventArgs e)
        {
            Export(is32: false);
        }

        private void btnExport32_Click(object sender, EventArgs e)
        {
            Export(is32: true);
        }

        private void Export(bool is32)
        {
            if (_groupedRowsForActions.Count == 0)
            {
                MessageBox.Show(this, "No rows to export. Generate first.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = is32 ? "keys_32.xlsx" : "keys_8.xlsx",
                OverwritePrompt = true
            };

            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                toolStatus.Text = "Exporting…";
                // Export only the filtered data currently displayed in the GridView
                var visibleRows = _groupedRowsForActions.SelectMany(g => g.Sets).ToList();
                if (is32)
                    ExcelExporter.Export32Digit(sfd.FileName, visibleRows);
                else
                    ExcelExporter.Export8Digit(sfd.FileName, visibleRows);
                toolStatus.Text = $"Exported: {Path.GetFileName(sfd.FileName)} ({visibleRows.Count} rows)";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStatus.Text = "Ready.";
            }
        }

        private void SetBusy(bool busy)
        {
            btnGenerate.Enabled = !busy;
            btnCancel.Enabled = busy;
            btnExport8.Enabled = !busy;
            btnExport32.Enabled = !busy;
            btnCopySelected.Enabled = !busy;
            txtMsnOrRange.Enabled = !busy;
            cboMeterType.Enabled = !busy;
            numSets.Enabled = !busy;
        }

        private void menuCreateLicense_Click(object sender, EventArgs e)
        {
            if (!AuthService.HasRight(_currentUser, UserRight.ManageLicenses))
            {
                MessageBox.Show(this, "You do not have permission to create licenses.", "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new LicenseAdminForm(_currentUser);
            dlg.ShowDialog(this);
            PostDialogCleanup(InitialDialog.Licenses);
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
            Close();
        }

        private void NavigateInitialForm()
        {
            // if the user can generate passwords, let the main form stay visible
            if (AuthService.HasRight(_currentUser, UserRight.GeneratePasswords))
                return;

            // otherwise immediately open the first available admin dialog
            // license has highest priority, then users, then roles
            if (menuCreateLicense.Enabled)
            {
                _shownDialogs.Add(InitialDialog.Licenses);
                menuCreateLicense.PerformClick();
            }
            else if (menuManageUsers.Enabled)
            {
                _shownDialogs.Add(InitialDialog.Users);
                menuManageUsers.PerformClick();
            }
            else if (menuManageRoles.Enabled)
            {
                _shownDialogs.Add(InitialDialog.Roles);
                menuManageRoles.PerformClick();
            }
        }

        private void menuManageUsers_Click(object sender, EventArgs e)
        {
            if (!AuthService.HasRight(_currentUser, UserRight.CreateUsers) && 
                !AuthService.HasRight(_currentUser, UserRight.EditUsers) && 
                !AuthService.HasRight(_currentUser, UserRight.DeleteUsers))
            {
                MessageBox.Show(this, "You do not have permission to manage users.", "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new UserManagementForm(_currentUser);
            dlg.ShowDialog(this);
            PostDialogCleanup(InitialDialog.Users);
        }

        private void menuManageRoles_Click(object sender, EventArgs e)
        {
            if (!AuthService.HasRight(_currentUser, UserRight.EditRoles))
            {
                MessageBox.Show(this, "You do not have permission to manage roles.", "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new RoleManagementForm(_currentUser);
            dlg.ShowDialog(this);
            PostDialogCleanup(InitialDialog.Roles);
        }

        private enum InitialDialog { None, Users, Roles, Licenses }

        private readonly HashSet<InitialDialog> _shownDialogs = new();

        private void PostDialogCleanup(InitialDialog closed)
        {
            // if user cannot generate passwords, we should not return to generator
            if (!AuthService.HasRight(_currentUser, UserRight.GeneratePasswords))
            {
                // Mark the closed dialog as shown
                _shownDialogs.Add(closed);
                
                // choose next enabled dialog that hasn't been shown yet
                if (menuCreateLicense.Enabled && !_shownDialogs.Contains(InitialDialog.Licenses))
                {
                    menuCreateLicense.PerformClick();
                    return;
                }
                if (menuManageUsers.Enabled && !_shownDialogs.Contains(InitialDialog.Users))
                {
                    menuManageUsers.PerformClick();
                    return;
                }
                if (menuManageRoles.Enabled && !_shownDialogs.Contains(InitialDialog.Roles))
                {
                    menuManageRoles.PerformClick();
                    return;
                }
                // nothing left to show, exit
                // schedule close on the message loop rather than immediately so
                // the current event handler can finish without touching a disposed
                // object.
                if (!IsDisposed && !Disposing)
                {
                    BeginInvoke((Action)Close);
                }
            }
            // If user has GeneratePasswords rights, allow normal operation
            // Don't automatically open any more dialogs
        }

        private void EditGroupRow(MeterKeyGroupRow gr)
        {
            // Edit first set; user can re-edit for others if needed
            var first = gr.Sets.OrderBy(s => s.SetIndex).First();
            using var dlg = new EditRowDialog(first);
            if (dlg.ShowDialog(this) == DialogResult.OK && dlg.UpdatedRow != null)
            {
                var index = _rows.IndexOf(first);
                if (index >= 0)
                {
                    _rows[index] = dlg.UpdatedRow;
                    ApplyFilter();
                    toolStatus.Text = "Row updated successfully.";
                    try
                    {
                        MeterKeyRepository.UpdateRow(first, dlg.UpdatedRow);
                    }
                    catch
                    {
                        // ignore, UI already updated
                    }
                }
            }
        }

        private void DeleteGroupRow(MeterKeyGroupRow gr)
        {
            var result = MessageBox.Show(this, $"Are you sure you want to delete all {gr.Sets.Count} set(s) for MSN {gr.Msn}?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
            if (result == DialogResult.Yes)
            {
                foreach (var r in gr.Sets)
                    _rows.Remove(r);
                ApplyFilter();
                toolStatus.Text = "Row(s) deleted successfully.";
                try
                {
                    foreach (var r in gr.Sets)
                        MeterKeyRepository.DeleteRow(r);
                }
                catch
                {
                    // ignore
                }
            }
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cboFilterField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterField.SelectedIndex = 0;
            ApplyFilter();
        }

        /// <summary>
        /// Deduplicates rows by (AK8, EK8, AK32, EK32). Keeps the last occurrence (overwrites).
        /// </summary>
        private static List<MeterKeyRow> DeduplicateRows(IEnumerable<MeterKeyRow> rows)
        {
            var dict = new Dictionary<(string Ak8, string Ek8, string Ak32, string Ek32), MeterKeyRow>();
            foreach (var r in rows)
            {
                var key = (r.Ak8 ?? string.Empty, r.Ek8 ?? string.Empty, r.Ak32 ?? string.Empty, r.Ek32 ?? string.Empty);
                dict[key] = r; // overwrite duplicate
            }
            return dict.Values.ToList();
        }

        /// <summary>
        /// Groups rows by (Msn, MeterType, Model) into one row per MSN.
        /// </summary>
        private static List<MeterKeyGroupRow> GroupRows(IEnumerable<MeterKeyRow> rows)
        {
            return rows
                .GroupBy(r => (Msn: r.Msn, MeterType: r.MeterType, Model: r.Model ?? string.Empty))
                .Select(g => new MeterKeyGroupRow
                {
                    Msn = g.Key.Msn,
                    MeterType = g.Key.MeterType,
                    Model = g.Key.Model,
                    Sets = g.ToList()
                })
                .ToList();
        }

        private void ApplyFilter()
        {
            var searchText = txtSearch.Text.Trim();
            var filterField = cboFilterField.SelectedItem?.ToString() ?? "All Fields";
            
            IEnumerable<MeterKeyRow> filteredRows;
            
            if (!string.IsNullOrEmpty(searchText))
            {
                try
                {
                    var dbRows = MeterKeyRepository.QueryRows(filterField, searchText);
                    filteredRows = DeduplicateRows(dbRows);
                }
                catch
                {
                    _groupedRowsForActions = new List<MeterKeyGroupRow>();
                    BindGridToTable(_groupedRowsForActions, 0);
                    return;
                }
            }
            else
            {
                filteredRows = _rows;
            }

            if (!string.IsNullOrEmpty(searchText) && filterField != "All Fields")
            {
                filteredRows = filteredRows.Where(row =>
                {
                    var fieldMatch = filterField switch
                    {
                        "MSN" => row.Msn.Contains(searchText, StringComparison.OrdinalIgnoreCase),
                        "Type" => row.MeterType.Contains(searchText, StringComparison.OrdinalIgnoreCase),
                        "AK8" => row.Ak8.Contains(searchText, StringComparison.OrdinalIgnoreCase),
                        "EK8" => row.Ek8.Contains(searchText, StringComparison.OrdinalIgnoreCase),
                        "AK32" => row.Ak32.Contains(searchText, StringComparison.OrdinalIgnoreCase),
                        "EK32" => row.Ek32.Contains(searchText, StringComparison.OrdinalIgnoreCase),
                        _ => false
                    };
                    return fieldMatch;
                });
            }
            else if (!string.IsNullOrEmpty(searchText))
            {
                filteredRows = filteredRows.Where(row =>
                    row.Msn.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    row.MeterType.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    (row.Model ?? "").Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    row.Ak8.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    row.Ek8.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    row.Ak32.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    row.Ek32.Contains(searchText, StringComparison.OrdinalIgnoreCase));
            }

            var grouped = GroupRows(filteredRows);
            _groupedRowsForActions = grouped;

            var maxSet = 0;
            if (grouped.Count > 0)
            {
                var maxInData = grouped.SelectMany(g => g.Sets).Select(s => s.SetIndex).DefaultIfEmpty(0).Max();
                maxSet = Math.Min(MaxSetColumns, maxInData);
            }
            BindGridToTable(grouped, maxSet);
            
            var filteredCount = grouped.Count;
            var totalGroups = GroupRows(_rows).Count;
            toolStatus.Text = filteredCount == totalGroups 
                ? $"Showing all {totalGroups:N0} MSN(s)"
                : $"Showing {filteredCount:N0} of {totalGroups:N0} MSN(s)";
        }

        private void BindGridToTable(List<MeterKeyGroupRow> grouped, int maxSet)
        {
            var dt = new DataTable();
            dt.Columns.Add("MSN", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            for (var i = 1; i <= maxSet; i++)
            {
                dt.Columns.Add($"AK8({i})", typeof(string));
                dt.Columns.Add($"EK8({i})", typeof(string));
                dt.Columns.Add($"AK32({i})", typeof(string));
                dt.Columns.Add($"EK32({i})", typeof(string));
            }

            var setByIndex = new Dictionary<int, MeterKeyRow>();
            foreach (var gr in grouped)
            {
                setByIndex.Clear();
                foreach (var r in gr.Sets)
                    setByIndex[r.SetIndex] = r;

                var row = dt.NewRow();
                row["MSN"] = gr.Msn;
                row["Type"] = gr.MeterType;
                for (var i = 1; i <= maxSet; i++)
                {
                    if (setByIndex.TryGetValue(i, out var r))
                    {
                        row[$"AK8({i})"] = r.Ak8;
                        row[$"EK8({i})"] = r.Ek8;
                        row[$"AK32({i})"] = r.Ak32;
                        row[$"EK32({i})"] = r.Ek32;
                    }
                }
                dt.Rows.Add(row);
            }

            dgvResults.Columns.Clear();
            dgvResults.DataSource = null;

            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MSN",
                HeaderText = "MSN",
                Name = "colMsn",
                ReadOnly = true,
                Width = 120
            });
            dgvResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Type",
                HeaderText = "Type",
                Name = "colType",
                ReadOnly = true,
                Width = 100
            });
            for (var i = 1; i <= maxSet; i++)
            {
                dgvResults.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = $"AK8({i})",
                    HeaderText = $"AK8({i})",
                    Name = $"colAk8_{i}",
                    ReadOnly = true,
                    Width = 90
                });
                dgvResults.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = $"EK8({i})",
                    HeaderText = $"EK8({i})",
                    Name = $"colEk8_{i}",
                    ReadOnly = true,
                    Width = 90
                });
                dgvResults.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = $"AK32({i})",
                    HeaderText = $"AK32({i})",
                    Name = $"colAk32_{i}",
                    ReadOnly = true,
                    Width = 220
                });
                dgvResults.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = $"EK32({i})",
                    HeaderText = $"EK32({i})",
                    Name = $"colEk32_{i}",
                    ReadOnly = true,
                    Width = 220
                });
            }
            dgvResults.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Copy",
                Name = "colCopy",
                ReadOnly = true,
                Text = "Copy",
                UseColumnTextForButtonValue = true,
                Width = 55
            });
            dgvResults.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Edit",
                Name = "colEdit",
                ReadOnly = true,
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 55
            });
            dgvResults.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Delete",
                Name = "colDelete",
                ReadOnly = true,
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 55
            });

            dgvResults.DataSource = dt;
        }
    }
}
