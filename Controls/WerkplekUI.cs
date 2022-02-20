using BrightIdeasSoftware;
using Forms;
using Forms.Aantal;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls
{
    public partial class WerkPlekkenUI : UserControl
    {
        public WerkPlekkenUI()
        {
            InitializeComponent();
           
        }

        public WerkPlek SelectedWerkplek
        {
            get => xwerkpleklist.SelectedObject as WerkPlek;
            set
            {
                xwerkpleklist.SelectedObject = value;
                xwerkpleklist.SelectedItem?.EnsureVisible();
            }
        }

        public Manager PManager { get; private set; }
        public bool IsSyncing { get; private set; }
        public bool EnableSync { get; set; }
        public void InitUI(Manager manager)
        {
            xwerkpleklist.CustomSorter = delegate (OLVColumn column, SortOrder order) {
                // check which column is about to be sorted and set your custom comparer
                xwerkpleklist.ListViewItemSorter = new Comparer(order, column);
            };
            PManager = manager;
            imageList1.Images.Clear();
            imageList1.Images.Add(Resources.iconfinder_technology);
            imageList1.Images.Add(
                Resources.iconfinder_technology.CombineImage(Resources.exclamation_warning_15590, 2));
           LoadLayout();
            foreach (var col in xwerkpleklist.Columns.Cast<OLVColumn>())
            {
                col.Groupable = true;
                col.Name = col.AspectName;
                col.ImageGetter = ImageGetter;
                col.GroupFormatter = (@group, parms) =>
                {
                    parms.GroupComparer = Comparer<OLVGroup>.Create((x, y) =>
                        Comparer.Compare(x, y, parms.PrimarySortOrder, parms.PrimarySort ?? parms.SecondarySort));
                };
            }

            xwerkpleklist.RebuildColumns();
            // ((OLVColumn) xwerkpleklist.Columns[0]).ImageGetter = ImageGetter;
            xwerkpleklist.CellToolTipShowing -= Xwerkpleklist_CellToolTipShowing;
            xwerkpleklist.CellToolTipShowing += Xwerkpleklist_CellToolTipShowing;
        }

        private void Xwerkpleklis_BeforeCreatingGroups(object sender, CreateGroupsEventArgs e)
        {
            e.Parameters.SecondarySort = e.Parameters.PrimarySort;
            e.Parameters.SecondarySortOrder = e.Parameters.PrimarySortOrder;
        }

        private void Xwerkpleklist_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            if (e.Model is WerkPlek wp)
            {
                e.Title = e.SubItem.Text;
                e.Text = $"{wp.Path}";

                var xrs = wp.Storingen?.Count(x => !x.IsVerholpen);
                var first = wp.Storingen?.FirstOrDefault(x => !x.IsVerholpen);
                var x1 = xrs == 1 ? "onderbrekening" : "onderbrekeningen";
                var x2 = xrs == 1 ? "staat" : "staan";
                var msg = $"{wp.Werk.Omschrijving}\nMedewerkers: {wp.PersonenLijst}";
                if (xrs > 1)
                    msg += $"\nLet op!!\nEr {x2} {xrs} {x1} open!";
                else if(xrs ==1 && first != null)
                    msg += $"\nLet op!!\n{first.StoringType} {(string.IsNullOrEmpty(first.Omschrijving)? "" : first.Omschrijving.Trim())} staat al {first.TotaalTijd} uur open...";
                e.Text += msg;
            }
        }

        public object ImageGetter(object sender)
        {
            if (sender is WerkPlek st) return st.Storingen != null && st.Storingen.Any(x => !x.IsVerholpen) ? 1 : 0;

            return 0;
        }

        public void InitEvents()
        {
            Manager.OnFormulierChanged += PManager_FormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
            Manager.FilterChanged += Manager_FilterChanged;
            Manager.OnSettingsChanged += Manager_OnSettingsChanged;
        }

        public void LoadLayout()
        {
            try
            {
                if (this.IsDisposed || this.Disposing || xwerkpleklist == null || xwerkpleklist.IsDisposed) return;
                if (Manager.Opties?._viewwerkplekdata == null) return;
                if (xwerkpleklist.InvokeRequired)
                    xwerkpleklist.Invoke(new MethodInvoker(() =>
                        xwerkpleklist.RestoreState(Manager.Opties.ViewDataWerkplekState)));
                else xwerkpleklist.RestoreState(Manager.Opties.ViewDataWerkplekState);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Manager_OnSettingsChanged(object instance, Rpm.Settings.UserSettings settings, bool reinit)
        {
            LoadLayout();
        }

        public void SaveLayout()
        {
            if (this.IsDisposed || this.Disposing || xwerkpleklist == null || xwerkpleklist.IsDisposed) return;
            if (Manager.Opties == null) return;
            if (xwerkpleklist.InvokeRequired)
                xwerkpleklist.Invoke(new MethodInvoker(() =>
                    Manager.Opties.ViewDataWerkplekState = xwerkpleklist.SaveState()));
            else Manager.Opties.ViewDataWerkplekState = xwerkpleklist.SaveState();

        }

        private void Manager_FilterChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(() => LoadPlekken(true)));
                }
                else LoadPlekken(true);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            this.BeginInvoke(new MethodInvoker(()=> LoadPlekken(true)));
        }

        public void DetachEvents()
        {
            Manager.OnFormulierChanged -= PManager_FormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
            Manager.FilterChanged -= Manager_FilterChanged;
            Manager.OnSettingsChanged -= Manager_OnSettingsChanged;
        }

        private bool _IsLoading;
        public async void LoadPlekken(bool startsync)
        {
            if (_IsLoading) return;
            _IsLoading = true;
            bool changed = false;
            try
            {
                int count = xwerkpleklist.Items.Count;
                var selected = xwerkpleklist.SelectedObject;
                var items = await Manager.GetProducties(ViewState.Gestart, true, false);
                var plekken = new List<WerkPlek>();
                foreach (var item in items)
                {

                    await item.UpdateForm(true, false, null, "", false, false, false);
                    var xpl = GetWerkplekken(item);
                    if(xpl.Length > 0)
                        plekken.AddRange(xpl);
                }
                xwerkpleklist.BeginUpdate();
                xwerkpleklist.SetObjects(plekken);
                xwerkpleklist.SelectedObject = selected;
                xwerkpleklist.SelectedItem?.EnsureVisible();
                xwerkpleklist.EndUpdate();
                changed = count != xwerkpleklist.Items.Count;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            _IsLoading = false;
            if (EnableSync && Manager.Opties.AutoProductieLijstSync && !IsSyncing && startsync)
                StartSync();
            if (changed)
                PlekkenChanged();
        }

        private void PManager_FormulierChanged(object sender, ProductieFormulier form)
        {
            Invoke(new Action(() =>
            {
                var changed = false;
                if (form != null)
                    try
                    {
                        var xwerkplekken = xwerkpleklist.Objects?.Cast<WerkPlek>().ToList();
                        if (xwerkplekken != null && form.Bewerkingen is {Length: > 0})
                        {
                            var formplekken = xwerkplekken
                                .Where(x => string.Equals(x.Werk.ProductieNr, form.ProductieNr)).ToList();
                            foreach (var bewerking in form.Bewerkingen)
                                if (bewerking.WerkPlekken is {Count: > 0})
                                {
                                    var isvalid = bewerking.IsAllowed(null);
                                    foreach (var plek in bewerking.WerkPlekken)
                                    {
                                        //&& x.WerktAanKlus(plek.Path, out _)
                                        var valid = isvalid && plek.Personen.Any(x => x.WerktAanKlus(bewerking, out _));
                                        var xplek = xwerkplekken.FirstOrDefault(x => x.Equals(plek));
                                        if (xplek == null && bewerking.State == ProductieState.Gestart && valid)
                                        {
                                            xwerkpleklist.BeginUpdate();
                                            xwerkpleklist.AddObject(plek);
                                            changed = true;
                                        }
                                        else if (xplek != null && (bewerking.State != ProductieState.Gestart || !valid))
                                        {
                                            xwerkpleklist.BeginUpdate();
                                            xwerkpleklist.RemoveObject(xplek);
                                            formplekken.Remove(xplek);
                                            changed = true;
                                        }
                                        else if (xplek != null)
                                        {
                                            xwerkpleklist.RefreshObject(plek);
                                            formplekken.Remove(xplek);
                                        }
                                    }
                                }

                            if (formplekken.Count > 0)
                            {
                                xwerkpleklist.BeginUpdate();
                                foreach (var formplek in formplekken)
                                {
                                    xwerkpleklist.RemoveObject(formplek);
                                    changed = true;
                                }
                            }
                        }
                        if (changed)
                            PlekkenChanged();
                    }
                    catch
                    {
                    }
                    finally { xwerkpleklist.EndUpdate();}
            }));
        }

        #region Syncing
        public void StartSync()
        {
            if (!EnableSync || !Manager.Opties.AutoProductieLijstSync || IsSyncing || Disposing || IsDisposed) return;
            IsSyncing = true;
            Task.Factory.StartNew(async () =>
            {
                while(EnableSync && Manager.Opties.AutoProductieLijstSync && IsSyncing && !Disposing && !IsDisposed)
                {
                    await Task.Delay(Manager.Opties.ProductieLijstSyncInterval);
                    if (!EnableSync || !Manager.Opties.AutoProductieLijstSync || !IsSyncing || IsDisposed || Disposing) break;
                    this.BeginInvoke(new MethodInvoker(()=> LoadPlekken(false)));
                }
            });
        }

        public void StopSync()
        {
            IsSyncing = false;
        }

        public Task<bool> SyncWerkplekken()
        {
            return Task.Run(async () =>
            {
                try
                {
                    bool changed = false;
                    if (xwerkpleklist.Items.Count > 0)
                    {
                        var xwerkplekken = xwerkpleklist.Objects?.Cast<WerkPlek>().ToList();
                        if (xwerkplekken != null)
                        {
                            for (int i = 0; i < xwerkplekken.Count; i++)
                            {
                                var wp = xwerkplekken[i];
                                var werk = Werk.FromPath(wp.Path);
                                if (!IsSyncing || Disposing || IsDisposed) return changed;
                                if (werk?.Plek == null || !werk.Bewerking.IsAllowed(null) ||
                                    werk.Bewerking.State != ProductieState.Gestart ||
                                    !werk.Plek.Personen.Any(personeel => personeel.WerktAanKlus(werk.Bewerking, out _)))
                                {
                                    xwerkpleklist.BeginUpdate();
                                    xwerkpleklist.RemoveObject(wp);
                                    xwerkpleklist.EndUpdate();
                                    changed = true;
                                    continue;
                                }

                                await werk.Formulier.UpdateForm(true, false, null, "", false, false, false);
                                xwerkpleklist.RefreshObject(werk.Plek);
                            }

                            if (changed)
                                PlekkenChanged();
                        }
                    }
                    return changed;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            });
        }
        #endregion;

        public WerkPlek[] GetWerkplekken(List<ProductieFormulier> forms)
        {
            var werkplekken = new List<WerkPlek>();
            try
            {
                if (PManager == null)
                    return werkplekken.ToArray();

                foreach (var form in forms)
                    if (form.Bewerkingen is {Length: > 0})
                    {
                        var bws = form.Bewerkingen.Where(x => x.State == ProductieState.Gestart).ToArray();
                        if (bws.Length <= 0) continue;
                        {
                            foreach (var b in bws)
                                if (b.IsAllowed(null) && b.WerkPlekken is {Count: > 0})
                                    werkplekken.AddRange(b.WerkPlekken.Where(x => x.Personen.Any(t => t.WerktAanKlus(b,out _))));
                        }
                    }
            }
            catch
            {
            }

            return werkplekken.ToArray();
        }

        public WerkPlek[] GetWerkplekken(ProductieFormulier form)
        {
            var werkplekken = new List<WerkPlek>();
            try
            {
                if (PManager == null)
                    return werkplekken.ToArray();
                if (form.Bewerkingen is {Length: > 0})
                {
                    var bws = form.Bewerkingen.Where(x => x.State == ProductieState.Gestart).ToArray();
                    if (bws.Length > 0)
                    {
                        foreach (var b in bws)
                            if (b.IsAllowed(null) && b.WerkPlekken is {Count: > 0})
                                werkplekken.AddRange(b.WerkPlekken.Where(x =>
                                    x.Personen.Any(t => t.WerktAanKlus(b, out _))));
                    }
                }
            }
            catch
            {
            }

            return werkplekken.ToArray();
        }

        public event EventHandler OnRequestOpenWerk;

        public event EventHandler OnPlekkenChanged;

        public void RequestOpenWerk(object sender)
        {
            OnRequestOpenWerk?.Invoke(sender, EventArgs.Empty);
        }

        public void PlekkenChanged()
        {
            OnPlekkenChanged?.Invoke(xwerkpleklist, EventArgs.Empty);
        }

        private void openProductieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xwerkpleklist.SelectedObjects.Count > 0)
                foreach (var w in xwerkpleklist.SelectedObjects.Cast<WerkPlek>())
                {
                    if (w.Werk == null)
                        continue;
                    RequestOpenWerk(w.Werk);
                }
        }

        private void wijzigAantalGemaaktToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xwerkpleklist.SelectedObjects.Count > 0)
            {
                var wp = xwerkpleklist.SelectedObjects[0] as WerkPlek;
                if (wp != null)
                {
                    var ac = new AantalChanger {StartPosition = FormStartPosition.CenterParent};
                    if (wp.Werk == null || ac.ShowDialog(wp) != DialogResult.OK) return;
                    wp.AantalGemaakt = ac.Aantal;
                    wp.LaatstAantalUpdate = DateTime.Now;
                    wp.Werk.UpdateBewerking(null, "Aantal Gemaakt Gewijzigd").Wait();
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var flag = Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis};
            if (!flag || xwerkpleklist.SelectedObjects.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            werkTijdToolStripMenuItem.Enabled = xwerkpleklist.SelectedObjects.Count == 1;
            wijzigAantalGemaaktToolStripMenuItem.Enabled = xwerkpleklist.SelectedObjects.Count == 1;
            openProductieToolStripMenuItem.Enabled = xwerkpleklist.SelectedObjects.Count > 0;
            openWerkplekToolStripMenuItem.Enabled = xwerkpleklist.SelectedObjects.Count == 1;
            storingenToolStripMenuItem.Enabled = xwerkpleklist.SelectedObjects.Count == 1;
            notitieToolStripMenuItem.Enabled = xwerkpleklist.SelectedObjects.Count == 1;
            xafkeurstoolstrip.Enabled = xwerkpleklist.SelectedObjects.Count == 1;
            aantalGeschiedenisToolStripMenuItem.Enabled = xwerkpleklist.SelectedObjects.Count == 1;
            onderbrekenToolStripMenuItem.Enabled = xwerkpleklist.SelectedObjects.Count > 0;
            hervattenToolStripMenuItem.Enabled = xwerkpleklist.SelectedObjects.Count > 0;
        }

        private void openWerkplekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xwerkpleklist.SelectedObjects.Count > 0)
            {
                var wp = xwerkpleklist.SelectedObjects[0] as WerkPlek;
                if (wp != null)
                {
                    var ind = new Indeling(wp);
                    ind.ShowDialog();
                }
            }
        }

        private void storingenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xwerkpleklist.SelectedObjects.Count > 0)
            {
                if (xwerkpleklist.SelectedObjects[0] is WerkPlek wp)
                {
                    var sf = new StoringForm(wp);
                    sf.ShowDialog();
                }
            }
        }

        private void xwerkpleklist_DoubleClick(object sender, EventArgs e)
        {
            if (Manager.LogedInGebruiker == null ||
                Manager.LogedInGebruiker.AccesLevel < AccesType.ProductieBasis) return;
            if (xwerkpleklist.SelectedObjects.Count > 0)
                foreach (var w in xwerkpleklist.SelectedObjects.Cast<WerkPlek>())
                {
                    if (w.Werk == null)
                        continue;
                    RequestOpenWerk(w.Werk);
                }
        }

        private void werkTijdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xwerkpleklist.SelectedObjects.Count > 0)
                if (xwerkpleklist.SelectedObjects[0] is WerkPlek wp)
                {
                    if (wp.Werk == null) return;
                    var wc = new WerktijdChanger(wp) {SaveChanges = true};
                    if (wc.ShowDialog() == DialogResult.OK)
                        wp.Werk.UpdateBewerking(null, $"[{wp.Path}] Werktijd Aangepast").Wait();
                }
        }

        public event EventHandler WerkPlekClicked;

        private void xwerkpleklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            WerkPlekClicked?.Invoke(sender, e);
        }

        private async void ShowSelectedProductieNotitie()
        {
            if (xwerkpleklist.SelectedObject is WerkPlek {Werk: { }} wp)
            {
                var form = wp.Werk;
                var xtxtform = new NotitieForms(wp.Note, wp)
                {
                    Title = $"Notitie voor [{form.ProductieNr}, {form.ArtikelNr}] {form.Naam} {wp.Naam}"
                };
                if (xtxtform.ShowDialog() == DialogResult.OK)
                {
                    wp.Note = xtxtform.Notitie;
                    await form.UpdateBewerking(null, $"[{form.ProductieNr}, {form.ArtikelNr}] {form.Naam} {wp.Naam} Notitie Gewijzigd");
                }
            }
        }


        private void ShowSelectedAfkeur()
        {
            if (xwerkpleklist.SelectedObject is WerkPlek { Werk: { } } wp)
            {
                var bw = wp.Werk;
                var form = bw.GetParent();
                if (form == null) return;
                var xafk = new AfkeurForm(form);
                xafk.ShowDialog();
            }
        }

        private void notitieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSelectedProductieNotitie();
        }

        private void xafkeurstoolstrip_Click(object sender, EventArgs e)
        {
            ShowSelectedAfkeur();
        }

        private void aantalGeschiedenisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xwerkpleklist.SelectedObject is WerkPlek plek)
            {
                var xaantal = new AantalHistoryForm(plek);
                xaantal.ShowDialog();
            }
        }

        private void onderbrekenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xwerkpleklist.SelectedObjects.Count > 0)
            {
                var xst = new Storing();
                var wps = xwerkpleklist.SelectedObjects.Cast<WerkPlek>().Where(x=> x.Storingen.All(s=> s.IsVerholpen)).ToList();
                if (wps.Count == 0)
                {
                    XMessageBox.Show(this,"Geselecteerde werkplekken zijn al onderbroken!", "Al Onderbroken",
                        MessageBoxIcon.Exclamation);
                    return;
                }

                var xstform = new NewStoringForm();
                xstform.SetOnderbreking(wps.FirstOrDefault(), xst, false);
                xstform.AutoUpdateTitle = false;
                xstform.Title = $"Onderbreek {string.Join(", ", wps.Select(x => x.Naam))}";
                if (xstform.ShowDialog() == DialogResult.OK)
                {
                    xst = xstform.Onderbreking;
                    foreach (var wp in wps)
                    {
                        xstform.SetOnderbreking(wp, xst, false);
                        xst = xstform.Onderbreking;
                        wp.Storingen.Add(xst.CreateCopy());
                        xwerkpleklist.RefreshObject(wp);
                        wp.Werk?.UpdateBewerking(null, $"[{wp.Path}] is Onderbroken voor {xst.StoringType}!");
                    }
                }

            }
        }

        private void hervattenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xwerkpleklist.SelectedObjects.Count > 0)
            {
              
                var wps = xwerkpleklist.SelectedObjects.Cast<WerkPlek>().Where(x=> x.Storingen.Any(s=> !s.IsVerholpen)).ToList();
                if (wps.Count == 0)
                {
                    XMessageBox.Show(this,"Geselecteerde werkplekken zijn al bezig!", "Al Bezig",
                        MessageBoxIcon.Exclamation);
                    return;
                }
                var xwp = wps.FirstOrDefault();
                if (xwp == null) return;
                var xst = xwp.Storingen.FirstOrDefault(x => !x.IsVerholpen);
                var xstform = new NewStoringForm();
                xstform.AutoUpdateTitle = false;
                xstform.Title = $"Hervat {string.Join(", ", wps.Select(x => x.Naam))}";
                xstform.SetOnderbreking(xwp, xst, true);

                if (xstform.ShowDialog() == DialogResult.OK)
                {
                    foreach (var wp in wps)
                    {
                        xst = wp.Storingen.FirstOrDefault(x =>
                            !x.IsVerholpen && string.Equals(x.StoringType, xstform.Onderbreking?.StoringType,
                                StringComparison.CurrentCultureIgnoreCase));
                        xstform.SetOnderbreking(wp, xst, true);
                        int index;
                        if ((index = wp.Storingen.IndexOf(xstform.Onderbreking)) > -1)
                            wp.Storingen[index] = xstform.Onderbreking;
                        xwerkpleklist.RefreshObject(wp);
                        wp.Werk?.UpdateBewerking(null, $"[{wp.Path}] is weer hervat van een {xstform.Onderbreking.StoringType}!");
                    }
                }

            }
        }
    }
}