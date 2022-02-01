using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.SqlLite;
using Rpm.Various;

namespace Forms
{
    public partial class DbUpdater : MetroFramework.Forms.MetroForm
    {
        public UserSettings DefaultSettings { get; private set; }
        private readonly CancellationTokenSource _token = new();

        public DbUpdater()
        {
            InitializeComponent();
            ((OLVColumn) xdbentrylist.Columns[0]).ImageGetter = sender => 0;
            ((OLVColumn) xdbentrylist.Columns[2]).AspectGetter =
                item => ((DatabaseUpdateEntry) item).UpdateMetStartup ? "Ja" : "Nee";
            ((OLVColumn) xdbentrylist.Columns[3]).AspectGetter =
                item => ((DatabaseUpdateEntry) item).AutoUpdate ? "Ja" : "Nee";
            DefaultSettings = Manager.DefaultSettings ?? UserSettings.GetDefaultSettings();
            if (DefaultSettings != null)
                xdbentrylist.SetObjects(DefaultSettings.DbUpdateEntries);
            
            if (xdbentrylist.Items.Count > 0)
                xdbentrylist.SelectedIndex = 0;
            UpdateSelected();
        }


        private void xchoosepath_Click(object sender, EventArgs e)
        {
            var fb = new FolderBrowserDialog();
            fb.Description = "Kies een locatie waar de database bestanden zich bevinden";
            if (fb.ShowDialog() == DialogResult.OK) xpath.Text = fb.SelectedPath;
        }

        private void xupdate_Click(object sender, EventArgs e)
        {
            var cur = Current();
            if (cur == null)
                return;
            if (string.IsNullOrEmpty(cur.UpdatePath) || !Directory.Exists(cur.UpdatePath))
                XMessageBox.Show(
                    this, "Opgegeven locatie bestaat niet!\nKies een geldige database locatie en probeer het opnieuw.",
                    "Ongeldig", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (!cur.UpdateDatabases.Any())
                XMessageBox.Show(
                    this, "Kies minstins 1 update type!",
                    "Geen Update Geselecteerd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                UpdateDb();
        }

        private async void UpdateDb()
        {
            var cur = Current();
            if (cur == null)
                return;
            xannuleren.Text = "&Annuleren";
            var xtype = string.Join(", ", cur.UpdateDatabases.Select(x => Enum.GetName(typeof(DbType), x)));
            if (Manager.DbUpdater.IsBussy)
            {
                XMessageBox.Show(this, $"Er wordt momenteel al een update uitgevoerd...\nProbeer het later nog een keer.",
                    "DbUpdater Bezig", MessageBoxIcon.Warning);
                return;
            }
            var done = await Manager.DbUpdater.UpdateEntryAsync(cur, _token, DoProgress);
            UpdateSelected();
            if (done > 0)
                XMessageBox.Show(
                    this, $"{done} Entries van {xtype} zijn succesvol geupdate!",
                    "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                XMessageBox.Show(
                    this, $"Geen updates beschikbaar voor {xtype}.",
                    "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            xannuleren.Text = "&Sluiten";
        }

        private void DoProgress(object sender, ProgressArg arg)
        {
            xstatus.Invoke(new Action(() => xstatus.Text = arg.Message));
            xstatus.Invoke(new Action(xstatus.Invalidate));
            progressBar1.Invoke(new Action(() => progressBar1.Value = arg.Pogress));
            progressBar1.Invoke(new Action(progressBar1.Invalidate));
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            _token.Cancel();
            DialogResult = DialogResult.Cancel;
        }

        private void xdbentrylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelected();
        }

        private void UpdateSelected()
        {
            var item = Current();
            if (item == null)
            {
                xlocatie.Text = "";
                xnaam.Text = "";
                xupdatewhenstartup.Checked = false;
                xaautoupdate.Checked = false;
                xinterval.Value = xinterval.Minimum;
                xperstype.Checked = false;
                xprodtype.Checked = false;
                xinstellingtype.Checked = false;
                xaccounttype.Checked = false;
                xupdate.Enabled = false;
                xdbentrypanel.Visible = false;
                xvanafdate.SetValue(DateTime.MinValue);
            }
            else
            {
                xlocatie.Text = item.UpdatePath;
                xnaam.Text = item.Naam;
                xupdatewhenstartup.Checked = item.UpdateMetStartup;
                xaautoupdate.Checked = item.AutoUpdate;
                xinterval.Value = Manager.Opties?.DbUpdateInterval??1000;
                if (item.UpdateDatabases == null)
                    item.UpdateDatabases = new List<DbType>();
                var xitems = item.UpdateDatabases;
                xperstype.Checked = xitems.Any(x => x == DbType.Medewerkers);
                xprodtype.Checked = xitems.Any(x => x == DbType.Producties);
                xinstellingtype.Checked = xitems.Any(x => x == DbType.Opties);
                xaccounttype.Checked = xitems.Any(x => x == DbType.Accounts);
                xupdate.Enabled = true;
                xvanafdate.SetValue(item.LastUpdated);
                xdbentrypanel.Visible = true;
            }
        }

        public DatabaseUpdateEntry Current()
        {
            return (DatabaseUpdateEntry) xdbentrylist.SelectedObject;
        }

        private void xdbchecktype_CheckedChanged(object sender, EventArgs e)
        {
            var cur = Current();
            var cb = (CheckBox) sender;
            if (cb == null) return;
            if (cur == null)
            {
                cb.Checked = false;
            }
            else
            {
                cur.UpdateDatabases = GetCheckedDbs();
               // cur.LastUpdated = xvanafdate.Value;
            }
        }

        private List<DbType> GetCheckedDbs()
        {
            var types = new List<DbType>();
            if (xaccounttype.Checked)
                types.Add(DbType.Accounts);
            if (xinstellingtype.Checked)
                types.Add(DbType.Opties);
            if (xprodtype.Checked)
                types.Add(DbType.Producties);
            if (xperstype.Checked)
                types.Add(DbType.Medewerkers);
            return types;
        }

        private void xinterval_ValueChanged(object sender, EventArgs e)
        {
            if (DefaultSettings != null)
                DefaultSettings.DbUpdateInterval = (int) xinterval.Value;
        }

        private void xaautoupdate_CheckedChanged(object sender, EventArgs e)
        {
            var cur = Current();
            if (cur == null)
            {
                xaautoupdate.Checked = false;
            }
            else
            {
                cur.AutoUpdate = xaautoupdate.Checked;
                xdbentrylist.RefreshObject(cur);
            }
        }

        private void xupdatewhenstartup_CheckedChanged(object sender, EventArgs e)
        {
            var cur = Current();
            if (cur == null)
            {
                xupdatewhenstartup.Checked = false;
            }
            else
            {
                cur.UpdateMetStartup = xupdatewhenstartup.Checked;
                xdbentrylist.RefreshObject(cur);
            }
        }

        private void xaddb_Click(object sender, EventArgs e)
        {
            if (DefaultSettings == null)
                return;
            if (DefaultSettings.DbUpdateEntries == null || DefaultSettings.DbUpdateEntries.Any(x =>
                string.Equals(x.UpdatePath, xpath.Text, StringComparison.OrdinalIgnoreCase)))
            {
                XMessageBox.Show(this, $"'{xpath.Text}' Is al toegevoegd!", "Bestaat Al", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else if (!Directory.Exists(xpath.Text))
            {
                XMessageBox.Show(this, $"'{xpath.Text}' Bestaat niet!\nKies een geldige locatie a.u.b.", "Bestaat Niet",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                var dg = new TextFieldEditor {Title = "Vul in de naam van de database"};
                if (dg.ShowDialog() != DialogResult.OK) return;
                var naam = dg.SelectedText;
                DefaultSettings.DbUpdateEntries.Add(new DatabaseUpdateEntry {Naam = naam, UpdatePath = xpath.Text});
                var selected = xdbentrylist.SelectedObject;
                xdbentrylist.SetObjects(DefaultSettings.DbUpdateEntries);
                xdbentrylist.SelectedObject = selected;
                xdbentrylist.SelectedItem?.EnsureVisible();
            }
        }

        private void xdeleteb_Click(object sender, EventArgs e)
        {
            if (DefaultSettings == null || xdbentrylist.SelectedObjects.Count == 0)
                return;
            foreach (var x in xdbentrylist.SelectedObjects.Cast<DatabaseUpdateEntry>())
            {
                DefaultSettings.DbUpdateEntries.RemoveAll(s =>
                    string.Equals(x.UpdatePath, s.UpdatePath, StringComparison.OrdinalIgnoreCase));
                xdbentrylist.RemoveObject(x);
            }

            UpdateSelected();
        }

        private void xnaam_TextChanged(object sender, EventArgs e)
        {
            var cur = Current();
            if (cur != null)
            {
                cur.Naam = xnaam.Text;
                xdbentrylist.RefreshObject(cur);
            }
        }

        private void DbUpdater_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DefaultSettings == null) return;
            DefaultSettings.SaveAsDefault();
            Manager.DbUpdater?.Start(DefaultSettings);
        }

        private void xvanafdate_ValueChanged(object sender, EventArgs e)
        {
            var cur = Current();
            if (cur != null)
            {
                cur.LastUpdated = xvanafdate.Value;
                xdbentrylist.RefreshObject(cur);
            }
        }
    }
}