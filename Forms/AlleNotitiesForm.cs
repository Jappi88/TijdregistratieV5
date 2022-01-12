using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager.Rpm.Productie;
using Rpm.Productie;
using Rpm.Various;

namespace ProductieManager.Forms
{
    public partial class AlleNotitiesForm : MetroFramework.Forms.MetroForm
    {
        private List<NotitieEntry> Notities = null;
        public AlleNotitiesForm()
        {
            InitializeComponent();
            ((OLVColumn) xnotitielist.Columns[0]).ImageGetter = (item) => 0;
            foreach (var col in xnotitielist.Columns.Cast<OLVColumn>())
                col.GroupKeyGetter = GroupKey;
            if (Manager.Opties?._viewallenotitiesdata != null)
                xnotitielist.RestoreState(Manager.Opties.AlleNotitiesState);
            LoadNotities();
        }
        private object GroupKey(object item)
        {
            if (item is NotitieEntry ent)
                return Enum.GetName(typeof(NotitieType), ent.Type);
            return "N.V.T.";
        }

        private void xsearch_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb is {Text: "Zoeken..."}) tb.Text = "";
        }

        private void xsearch_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.Text = "Zoeken...";
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim().ToLower() != "zoeken...")
            {
                LoadNotities();
            }
        }

        private bool _isbusy = false;
        private async void LoadNotities()
        {
            if (_isbusy || Manager.Database?.ProductieFormulieren == null) return;
            _isbusy = true;
            string filter = xsearchbox.Text.Trim().ToLower();
            bool xs = !string.IsNullOrEmpty(filter) && !filter.Contains("zoeken...");
            if (Notities == null)
            {
                Notities = new List<NotitieEntry>();
                var prods = await Manager.Database.GetAllProducties(true, true);
                foreach (var prod in prods)
                {
                    var xnotes = prod.GetNotities();
                    if(xnotes.Count > 0)
                        Notities.AddRange(xnotes);
                }
                if(Manager.Opties?.Notities != null)
                    Notities.AddRange(Manager.Opties.Notities);
            }

            var selected = xnotitielist.SelectedObject;

            var xview = !xs
                ? Notities
                : Notities.Where(x => IsAllowed(x,filter));

            xnotitielist.SetObjects(xview);

            xnotitielist.SelectedObject = selected;
            xnotitielist.SelectedItem?.EnsureVisible();
            _isbusy = false;
        }

        public bool IsAllowed(NotitieEntry ent, string filter)
        {
            bool xs = !string.IsNullOrEmpty(filter) && !filter.Contains("zoeken...");
            if (!xs) return true;
            return ent.Naam != null && ent.Naam.ToLower().Contains(filter) ||
                   ent.Notitie != null && ent.Notitie.ToLower().Contains(filter) ||
                   ent.Path != null && ent.Path.ToLower().Contains(filter);
        }

        private void wijzigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xnotitielist.SelectedObject is NotitieEntry ent)
            {
                var xedit = new NotitieForms(ent);
                if (xedit.ShowDialog() == DialogResult.OK)
                {
                    ent.UpdateEntry(xedit.Notitie, true);
                }
            }
        }

        private void openProductieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xnotitielist.SelectedObjects.Count > 0)
            {
                foreach (var note in xnotitielist.SelectedObjects.Cast<NotitieEntry>())
                {
                    var werk = Werk.FromPath(note.Path);
                    if (werk == null || !werk.IsValid) continue;
                    Manager.FormulierActie(new object[] {werk.Formulier, werk.Bewerking}, MainAktie.OpenProductie);
                }
            }
        }

        private void verwijderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xnotitielist.SelectedObjects.Count > 0)
            {
                foreach (var note in xnotitielist.SelectedObjects.Cast<NotitieEntry>())
                {
                    note.UpdateEntry(null, true);
                }
            }
        }

        private void UpdateNotitie(ProductieFormulier form)
        {
            if (this.IsDisposed || _isbusy) return;
            if (form != null)
                try
                {
                    var filter = xsearchbox.Text.ToLower().Trim();
                    var formnotes = form.GetNotities();
                    var notes = Notities.Where(x => x.Path != null && x.Path.ToLower().StartsWith(form.ProductieNr.ToLower())).ToList();
                    if (notes.Count == 0 && formnotes.Count == 0) return;
                    foreach (var note in notes)
                        Notities.Remove(note);
                    if(formnotes.Count > 0)
                        Notities.AddRange(formnotes);
                    foreach (var note in formnotes)
                        if (IsAllowed(note, filter))
                        {
                            if (notes.Any(x => x.Equals(note)))
                                xnotitielist.RefreshObject(note);
                            else xnotitielist.AddObject(note);
                        }
                        else xnotitielist.RemoveObject(note);

                    var toremove = notes.Where(x => !formnotes.Any(s => s.Equals(x)));
                    foreach (var remove in toremove) 
                        xnotitielist.RemoveObject(remove);
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine(@"Disposed!");
                }
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            try
            {
                if (_isbusy) return;
                if (InvokeRequired)
                    BeginInvoke(new Action(() => UpdateNotitie(changedform)));
                else UpdateNotitie(changedform);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AlleNotitiesForm_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnSettingsChanged += Manager_OnSettingsChanged;
        }

        private void UpdateSettingsNotes()
        {
            var filter = xsearchbox.Text.ToLower().Trim();
            var formnotes = Manager.Opties?.Notities ?? new List<NotitieEntry>();
            var oldnotes = Notities.Where(x => x.Type == NotitieType.Algemeen).ToList();
            foreach (var old in oldnotes)
                Notities.Remove(old);
            foreach (var note in formnotes)
                Notities.Add(note);
            foreach (var note in formnotes)
                if (IsAllowed(note, filter))
                {
                    if (oldnotes.Any(x => x.Equals(note)))
                        xnotitielist.RefreshObject(note);
                    else xnotitielist.AddObject(note);
                }
                else xnotitielist.RemoveObject(note);

            var toremove = oldnotes.Where(x => !formnotes.Any(s => s.Equals(x)));
            foreach (var remove in toremove)
                xnotitielist.RemoveObject(remove);
        }

        private void Manager_OnSettingsChanged(object instance, global::Rpm.Settings.UserSettings settings, bool init)
        {
            try
            {
                if (InvokeRequired)
                    BeginInvoke(new Action(UpdateSettingsNotes));
                else UpdateSettingsNotes();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AlleNotitiesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged; 
            Manager.OnSettingsChanged -= Manager_OnSettingsChanged;
            if (Manager.Opties != null)
                Manager.Opties.AlleNotitiesState = xnotitielist.SaveState();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = xnotitielist.SelectedObjects.Count == 0;
            openProductieToolStripMenuItem.Enabled = (xnotitielist.SelectedObject is NotitieEntry ent) &&
                                                     (ent.Werkplek != null || ent.Productie != null);
        }

        private void xnewnotitie_Click(object sender, EventArgs e)
        {
            var xnote = new NotitieEntry("", NotitieType.Algemeen);
            var xnewnote = new NotitieForms(xnote);
            if (xnewnote.ShowDialog() == DialogResult.OK)
            {
                xnote.UpdateEntry(xnewnote.Notitie,true);
            }
        }

        private void xnotitielist_DoubleClick(object sender, EventArgs e)
        {
            if (xnotitielist.SelectedObject is NotitieEntry ent)
            {
                var xedit = new NotitieForms(ent);
                if (xedit.ShowDialog() == DialogResult.OK)
                {
                    ent.UpdateEntry(xedit.Notitie, true);
                }
            }
        }
    }
}
