using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Productie;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Controls
{
    public partial class TakenManager : UserControl
    {
        public List<Taak> Taken { get; private set; } = new List<Taak>();
        private Taak _selectedtaak;

        public TakenManager()
        {
            InitializeComponent();
            Visible = false;
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.SupportsTransparentBackColor,
                true);
            Manager.OnSettingsChanged += Manager_OnSettingsChanged;
        }

        public Taak SelectedItem
        {
            get => _selectedtaak;
            set => SetSelected(value);
        }

        public void InitManager()
        {
            imageList1.Images.Clear();
            imageList1.Images.Add(
                Resources.taskboardflat_106022.CombineImage(Resources.exclamation_warning_15590, 2.5));
            imageList1.Images.Add(Resources.taskboardflat_106022.CombineImage(Resources.Warning_36828, 2.5));
            imageList1.Images.Add(Resources.taskboardflat_106022.CombineImage(Resources.emblemimportant_103451, 2.5));
            imageList1.Images.Add(Resources.taskboardflat_106022.CombineImage(Resources.info_15260, 2.5));
            imageList1.Images.Add(Resources.taskboardflat_106022);
            var x = xtakenlijst.Columns[0] as OLVColumn;
            x.ImageGetter = ImageGetter;
            x.GroupKeyGetter = GroupGetter;
            UpdateTakenViewState(false,false);
           
        }

        public void InitEvents()
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
           
        }
        
        public void DetachEvents()
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
        }

        private void Manager_OnSettingsChanged(object instance, Rpm.Settings.UserSettings settings, bool reinit)
        {
            if(!settings.GebruikTaken || Manager.LogedInGebruiker == null)
            {
                Taken.Clear();
                xtakenlijst.SetObjects(Taken);
                UpdateStatus();
                DetachEvents();
            }
            else if(settings.GebruikTaken && Manager.LogedInGebruiker != null)
            {
                DetachEvents();
                UpdateAllTaken();
                InitEvents();
            }
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            try
            {
                if (this.IsDisposed || this.Disposing) return;
                this.BeginInvoke(new MethodInvoker(() => DeleteProductieId(id)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            UpdateFormulier(changedform);
        }

        private object GroupGetter(object value)
        {
            if (value is Taak xt)
            {
                return xt.Type;
            }

            return "N/A";
        }

        private object ImageGetter(object value)
        {
            if (value is Taak xt)
            {
                switch (xt.Urgentie)
                {
                    case TaakUrgentie.ZodraMogelijk:
                        return 0;
                    case TaakUrgentie.ZSM:
                        return 1;
                    case TaakUrgentie.PerDirect:
                        return 2;
                    case TaakUrgentie.Geen_Prioriteit:
                        return 3;
                }
            }

            return 4;
        }

        private bool SetSelected(Taak taak)
        {
            if (taak != null)
            {
                xtakenlijst.SelectedObject = taak;
                _selectedtaak = taak;
                return true;
            }

            _selectedtaak = null;
            return false;
        }

        public bool IsAllowed(Taak taak)
        {
            if (taak == null || Manager.Opties == null || !Manager.Opties.GebruikTaken || this.IsDisposed || this.Disposing) return false;
            if (xpriotaken.DropDownItems.Cast<ToolStripMenuItem>().Any(t => t.Checked) && 
                !xpriotaken.DropDownItems.Cast<ToolStripMenuItem>().Any(t =>
                t.Checked && ((string) t.Tag).Equals(((int) taak.Urgentie).ToString())))
                return false;
            if (xsoorttaken.DropDownItems.Cast<ToolStripMenuItem>().Any(t => t.Checked) && !xsoorttaken.DropDownItems
                .Cast<ToolStripMenuItem>().Any(t =>
                    t.Checked && ((string) t.Tag).Equals(((int) taak.Type).ToString())))
                return false;
            return true;
        }

        public bool DeleteProductieId(string id)
        {
            try
            {
                if (this.IsDisposed || this.Disposing) return false;
                var takenlijst = xtakenlijst.Objects.Cast<Taak>().ToArray();
                int r = Taken.RemoveAll(x => string.Equals(x.ProductieNr, id,
                    StringComparison.CurrentCultureIgnoreCase));
                var toremove = takenlijst.Where(x => string.Equals(x.ProductieNr, id,
                    StringComparison.CurrentCultureIgnoreCase)).ToList();
                if (toremove.Count > 0)
                {
                    xtakenlijst.BeginUpdate();
                    xtakenlijst.RemoveObjects(toremove);
                    xtakenlijst.Sort();
                    return true;
                }

                if (r > 0)
                    UpdateStatus();
                return r > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { xtakenlijst.EndUpdate(); }
        }

        public void UpdateFormulier(ProductieFormulier formulier)
        {
            try
            {
                if (this.IsDisposed || this.Disposing) return;
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        if (xtakenlijst.Objects == null)
                            xtakenlijst.SetObjects(new Taak[] { });
                        int xcount = Taken.Count;
                        var takenlijst = xtakenlijst.Objects.Cast<Taak>().ToList();
                        var taken = formulier.GetProductieTaken();
                        bool changed = false;
                        if (taken == null || taken.Length == 0)
                        {
                            Taken.RemoveAll(x => x == null || string.Equals(x.ProductieNr, formulier.ProductieNr,
                                StringComparison.CurrentCultureIgnoreCase));
                            var toremove = takenlijst.Where(x => string.Equals(x.ProductieNr, formulier.ProductieNr,
                                StringComparison.CurrentCultureIgnoreCase)).ToList();
                            if (toremove.Count > 0)
                            {
                                xtakenlijst.BeginUpdate();
                                xtakenlijst.RemoveObjects(toremove);
                                changed = true;
                            }
                        }
                        else
                        {
                            var xremove = Taken.Where(x => x == null || 
                                    (string.Equals(x.ProductieNr, formulier.ProductieNr,
                                        StringComparison.CurrentCultureIgnoreCase) && !taken.Any(t => t != null && t.Equals(x))))
                                .ToArray();
                            if (xremove.Length > 0)
                            {
                                foreach (var xt in xremove)
                                {
                                    Taken.Remove(xt);
                                    takenlijst.Remove(xt);
                                    changed = true;
                                }
                                xtakenlijst.BeginUpdate();
                                xtakenlijst.RemoveObjects(xremove);
                                
                            }
                            foreach (var t in taken)
                            {
                                if (t == null) continue;
                                int index = -1;
                                if ((index = Taken.IndexOf(t)) < 0)
                                    Taken.Add(t);
                                else Taken[index] = t;
                                var old = takenlijst.FirstOrDefault(x => x.Equals(t));
                                if (old == null)
                                {
                                    if (IsAllowed(t))
                                    {
                                        xtakenlijst.BeginUpdate();
                                        xtakenlijst.AddObject(t);
                                        changed = true;
                                    }
                                }
                                else
                                {
                                    if (IsAllowed(t))
                                        xtakenlijst.RefreshObject(t);
                                    else
                                    {
                                        xtakenlijst.BeginUpdate();
                                        xtakenlijst.RemoveObject(t);
                                        changed = true;
                                    }
                                }
                            }
                        }

                        if (changed)
                        {
                            xtakenlijst.Sort();
                            UpdateTakenViewState(Taken.Count > xcount && Manager.Opties.ToonLijstNaNieuweTaak, false);
                        }
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally { xtakenlijst.EndUpdate(); }
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateAllTaken()
        {
            try
            {
                if (this.IsDisposed || this.Disposing) return;
                this.BeginInvoke(new MethodInvoker(MethodInvoker));
            }
            catch (Exception e)
            {
            }
        }

        private async void MethodInvoker()
        {
            try
            {
                Taken = await TaakBeheer.GetAlleTaken();
                if (Taken is {Count: > 0})
                {
                    for (int i = 0; i < Taken.Count; i++)
                    {
                        var taak = Taken[i];
                        if (taak == null)
                        {
                            Taken.RemoveAt(i--);
                            continue;
                        }
                        if (taak.Bewerking != null && !taak.Bewerking.IsAllowed(null))
                            Taken.RemoveAt(i--);
                        else if (taak.Plek?.Werk != null && !taak.Plek.Werk.IsAllowed(null))
                            Taken.RemoveAt(i--);
                        else if (taak.Formulier != null && !taak.Formulier.IsAllowed(null)) Taken.RemoveAt(i--);
                    }


                    LoadTaken();
                }
            }
            catch (Exception e)
            {
            }
        }

        public void LoadTaken()
        {
            //filter volgens geselecteerde prioriteiten
            if (IsDisposed || xtakenlijst.IsDisposed) return;
            try
            {
                int count = xtakenlijst.Items.Count;
                var xselected = xtakenlijst.SelectedObject;
                var xtaken = Taken.Where(IsAllowed).ToList();
                xtakenlijst.BeginUpdate();
                xtakenlijst.SetObjects(xtaken);
                xtakenlijst.Sort();
                xtakenlijst.EndUpdate();
                xnexttaak.Enabled = xtakenlijst.Items.Count > 0;
                UpdateTakenViewState(xtakenlijst.Items.Count > count && Manager.Opties.ToonLijstNaNieuweTaak, false);
                xtakenlijst.SelectedObject = xselected;
                xtakenlijst.SelectedItem?.EnsureVisible();
                UpdateStatus();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public event TaakHandler OnTaakClicked;

        public void TaakClicked(Taak taak)
        {
            OnTaakClicked?.Invoke(taak);
        }

        public event TaakHandler OnTaakUitvoeren;

        public void TaakUitvoeren(Taak taak)
        {
            OnTaakUitvoeren?.Invoke(taak);
        }

        private void xsoorttaken_ButtonClick(object sender, EventArgs e)
        {
            xsoorttaken.ShowDropDown();
        }

        private void xpriotaken_ButtonClick(object sender, EventArgs e)
        {
            xpriotaken.ShowDropDown();
        }

        private void SetCheckToolItem(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                item.Checked = !item.Checked;
                LoadTaken();
            }
          
        }

        private void xpriotaken_MouseEnter(object sender, EventArgs e)
        {
            xpriotaken.ShowDropDown();
        }

        private void xsoorttaken_MouseEnter(object sender, EventArgs e)
        {
            xsoorttaken.ShowDropDown();
        }

        private void xtakenlijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xtakenlijst.SelectedObject is Taak taak) TaakClicked(taak);

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            Visible = Taken.Count > 0;
            xtaakstatus.Text = Width <= 40 ? "" : $"{Taken.Count} Openstaande " + (Taken.Count == 1 ? "Taak" : "Taken");
            pictureBox1.Image = Width <= 40 ? Resources.arrow_left_15601 : Resources.arrow_right_15600;
            pictureBox2.Image = pictureBox1.Image;
            if (Width <= 38)
            {
                xstatus.Text = $"{xtakenlijst.Items.Count}";
            }
            else
            {
                if (xtakenlijst.SelectedObject != null)
                {
                    if (!(xtakenlijst.SelectedObject is Taak taak))
                        return;
                    xstatus.Text = $"[{Enum.GetName(typeof(TaakUrgentie), taak.Urgentie)?.ToUpper()}] " +
                                   $"{Enum.GetName(typeof(AktieType), taak.Type)} | ";
                    if (taak.Formulier != null)
                        xstatus.Text += $"ProdNr: {taak.Formulier.ProductieNr} | ArtNr: {taak.Formulier.ArtikelNr}";
                }
                else
                {
                    string value;
                    if (xtakenlijst.SelectedItems.Count > 0)
                    {
                        value = xtakenlijst.SelectedItems.Count == 1 ? "Taak" : "Taken";
                        xstatus.Text = $"{xtakenlijst.SelectedItems.Count} {value} Geselecteerd";
                    }
                    else
                    {
                        value = xtakenlijst.Items.Count == 1 ? "Taak" : "Taken";
                        xstatus.Text = $"{xtakenlijst.Items.Count} Getoonde {value}";
                    }
                }
            }
        }

        private void xtakenlijst_DoubleClick(object sender, EventArgs e)
        {
            if (xtakenlijst.SelectedObject != null)
            {
                if (!(xtakenlijst.SelectedObject is Taak taak))
                    return;
                TaakUitvoeren(taak);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (xtakenlijst.SelectedObjects.Count == 0)
            {
                startTaakToolStripMenuItem.Enabled = false;
                openProductieToolStripMenuItem.Enabled = false;
            }
            else
            {
                openProductieToolStripMenuItem.Enabled =
                    xtakenlijst.SelectedObjects.Cast<Taak>().Any(x => x.Formulier != null);
                startTaakToolStripMenuItem.Enabled = true;
            }
        }

        private void startTaakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xtakenlijst.SelectedObjects.Count > 0)
            {
                if (!(xtakenlijst.SelectedObjects[0] is Taak taak))
                    return;
                TaakUitvoeren(taak);
            }
        }

        private void vouwAllGroepenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewGroup group in xtakenlijst.Groups)
                ((OLVGroup) group.Tag).Collapsed = true;
        }

        private void ontvouwAlleGroepenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewGroup group in xtakenlijst.Groups)
                ((OLVGroup) group.Tag).Collapsed = false;
        }

        private void xnexttaak_Click(object sender, EventArgs e)
        {
            if (xtakenlijst.Items.Count == 0)
                return;
            var t = xtakenlijst.Objects.Cast<Taak>().FirstOrDefault();
            if (t != null) TaakUitvoeren(t);
        }

        private void openProductieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xtakenlijst.SelectedObjects.Count > 0)
                foreach (var t in xtakenlijst.SelectedObjects.Cast<Taak>())
                    if (t.Formulier != null)
                        TaakUitvoeren(new Taak(t.Formulier, t.Bewerking, AktieType.Beginnen,
                            TaakUrgentie.PerDirect));
        }

        private void TakenShow_Click(object sender, EventArgs e)
        {
            UpdateTakenViewState(false,true);
        }

        private void UpdateTakenViewState(bool makevisible, bool collapse)
        {
            var count = xtakenlijst.Items.Count;
            if (makevisible)
            {
                Width = 650;
            }
            else
            {
                if (Width <= 50)
                {
                    Width = collapse ? 650 : 38;
                }
                else
                {
                    Width = collapse ? 38 : 650;
                }
            }
            UpdateStatus();
        }

        private void TakenManager_SizeChanged(object sender, EventArgs e)
        {
            if (Width > 50)
                xtakenlijst.TileSize = new Size(Width - 75, 96);
            else xtakenlijst.TileSize = new Size(575, 96);
        }

        private void xtakenlijst_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            if (e.Model is Taak wp)
            {
                e.Title = $"[{Enum.GetName(typeof(AktieType), wp.Type)}]";
                e.Text = wp.Beschrijving;
            }
        }
    }
}