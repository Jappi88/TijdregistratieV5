using BrightIdeasSoftware;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Forms;
using System.Threading.Tasks;
using System.Threading;

namespace Controls
{
    public partial class AlleNotitiesUI : UserControl
    {
        private List<NotitieEntry> Notities = null;
        private Label xloadinglabel;
        public AlleNotitiesUI()
        {
            InitializeComponent();
            InitLoadingLabel();
            this.Controls.Add(xloadinglabel);
            xloadinglabel.BringToFront();
        }

        private void InitLoadingLabel()
        {
            xloadinglabel = new Label();
            this.xloadinglabel.BackColor = System.Drawing.Color.Transparent;
            this.xloadinglabel.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xloadinglabel.Location = new System.Drawing.Point(0, 0);
            this.xloadinglabel.Size = new System.Drawing.Size(100, 23);
            this.xloadinglabel.TabIndex = 0;
            this.xloadinglabel.Text = "Loading...";
            this.xloadinglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xloadinglabel.Visible = false;
            this.xloadinglabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            this.xloadinglabel.Size = this.Size;
        }

        public void InitUI()
        {
            ((OLVColumn)xnotitielist.Columns[0]).ImageGetter = (item) => 0;
            foreach (var col in xnotitielist.Columns.Cast<OLVColumn>())
                col.GroupKeyGetter = GroupKey;
            if (Manager.Opties?._viewallenotitiesdata != null)
                xnotitielist.RestoreState(Manager.Opties.AlleNotitiesState);
            LoadNotities();
            InitEvents();
        }

        public void CloseUI()
        {
            DetachEvents();
            if (Manager.Opties != null)
                Manager.Opties.AlleNotitiesState = xnotitielist.SaveState();
        }

        public void InitEvents()
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnSettingsChanged += Manager_OnSettingsChanged;
        }

        public void DetachEvents()
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnSettingsChanged -= Manager_OnSettingsChanged;
        }

        private object GroupKey(object item)
        {
            if (item is NotitieEntry ent)
                return Enum.GetName(typeof(NotitieType), ent.Type);
            return "N.V.T.";
        }

        private void xsearch_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox {Text: "Zoeken..."} tb) tb.Text = "";
        }

        private void xsearch_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
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

        public void StartWaitUI(string value)
        {
            if (_isbusy) return;
            _isbusy = true;
            Task.Run(() =>
            {
                try
                {
                    if (Disposing || IsDisposed) return;
                    this.Invoke(new MethodInvoker(() =>
                    {
                        xloadinglabel.Visible = true;
                    }));

                    var cur = 0;
                    var xwv = value;
                    //var xcurvalue = xwv;
                    var tries = 0;
                    try
                    {
                        while (_isbusy && tries < 200)
                        {
                            if (cur > 5) cur = 0;
                            if (Disposing || IsDisposed) return;
                            var curvalue = xwv.PadRight(xwv.Length + cur, '.');
                            //xcurvalue = curvalue;
                            this.Invoke(new MethodInvoker(() =>
                            {
                                xloadinglabel.Text = curvalue;
                                xloadinglabel.Invalidate();
                            }));
                            //Application.DoEvents();

                            Thread.Sleep(350);
                            //Application.DoEvents();
                            tries++;
                            cur++;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    if (Disposing || IsDisposed) return;
                    this.Invoke(new MethodInvoker(() =>
                    {
                        xloadinglabel.Visible = false;
                    }));
                }
                catch (Exception e)
                {
                }

                StopWait();
            });
        }

        /// <summary>
        ///     verberg het laad scherm
        /// </summary>
        public void StopWait()
        {
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(StopWait));
            else
            {
                _isbusy = false;
                xloadinglabel.Visible = false;
            }
        }

        private bool _isbusy = false;
        private async void LoadNotities()
        {
            if (_isbusy || Manager.Database?.ProductieFormulieren == null) return;
            try
            {
                StartWaitUI("Notities laden");
                string filter = xsearchbox.Text.Trim().ToLower();
                bool xs = !string.IsNullOrEmpty(filter) && !filter.Contains("zoeken...");
                if (Notities == null)
                {
                    Notities = new List<NotitieEntry>();
                    var prods = await Manager.Database.GetAllProducties(true, true, true);
                    foreach (var prod in prods)
                    {
                        var xnotes = prod.GetNotities();
                        if (xnotes.Count > 0)
                            Notities.AddRange(xnotes);
                    }
                    if (Manager.Opties?.Notities != null)
                        Notities.AddRange(Manager.Opties.Notities);
                }


                if (!this.IsDisposed && !this.Disposing)
                {
                    var selected = xnotitielist.SelectedObject;

                    var xview = !xs
                        ? Notities
                        : Notities.Where(x => IsAllowed(x, filter));
                    xnotitielist.SetObjects(xview);

                    xnotitielist.SelectedObject = selected;
                    xnotitielist.SelectedItem?.EnsureVisible();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                if (xedit.ShowDialog(this) == DialogResult.OK)
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
            if (xnewnote.ShowDialog(this) == DialogResult.OK)
            {
                xnote.UpdateEntry(xnewnote.Notitie,true);
            }
        }

        private void xnotitielist_DoubleClick(object sender, EventArgs e)
        {
            if (xnotitielist.SelectedObject is NotitieEntry ent)
            {
                var xedit = new NotitieForms(ent);
                if (xedit.ShowDialog(this) == DialogResult.OK)
                {
                    ent.UpdateEntry(xedit.Notitie, true);
                }
            }
        }
    }
}
