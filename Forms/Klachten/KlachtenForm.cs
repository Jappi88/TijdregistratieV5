using Forms.Klachten;
using ProductieManager.Forms.Klachten;
using Rpm.Klachten;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Various;

namespace Forms
{
    public partial class KlachtenForm : Forms.MetroBase.MetroBaseForm 
    {
        public KlachtenForm()
        {
            InitializeComponent();
        }

        public KlachtControl SelectedKlacht
        {
            get=> GetAlleKlachtenControl().FirstOrDefault(x => x.IsSelected);
            set => SelectControl(value?.Klacht, true);
        }

        public void LoadEntries()
        {
            try
            {
                if (this.Disposing || this.IsDisposed || Manager.Klachten == null) return;
                UpdateSelectedButtons();
                var selected = SelectedKlacht;
                var xents = Manager.Klachten.GetAlleKlachten().Where(IsAllowed).OrderBy(x=> x.DatumGeplaatst);
                xklachtenContainer.SuspendLayout();
                xklachtenContainer.Controls.Clear();
                foreach (var xent in xents)
                {
                    AddKlacht(xent,false);
                }
                xklachtenContainer.ResumeLayout(true);
                if (selected?.Klacht != null)
                    SelectControl(selected.Klacht, true);
                UpdateKlachtenCount();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private List<KlachtEntry> GetLoadedEntries()
        {
            try
            {
                return GetAlleKlachtenControl().Select(x => x.Klacht).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<KlachtEntry>();
            }
        }

        public bool DeleteEntry(object id)
        {
            try
            {
                bool deleted = false;
                for (int i = 0; i < xklachtenContainer.Controls.Count; i++)
                {
                    if (xklachtenContainer.Controls[i] is KlachtControl control)
                    {
                        if (control.Klacht != null && control.Klacht.Equals(id))
                        {
                            xklachtenContainer.Controls.RemoveAt(i--);
                            deleted = true;
                        }
                    }
                }

                UpdateKlachtenCount();
                UpdateSelectedButtons();
                xklachtenContainer.Invalidate();
                return deleted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool IsAllowed(KlachtEntry entry)
        {
            if (entry == null || !entry.IsValid) return false;
            var xfilter = xsearchbox.Text.ToLower().StartsWith("zoeken") ? "" : xsearchbox.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(xfilter)) return true;
            if (entry.Onderwerp != null && entry.Onderwerp.ToLower().Contains(xfilter)) return true;
            if (entry.Omschrijving != null && entry.Omschrijving.ToLower().Contains(xfilter)) return true;
            if (entry.Afzender != null && entry.Afzender.ToLower().Contains(xfilter)) return true;
            if (entry.Melder != null && entry.Melder.ToLower().Contains(xfilter)) return true;
            return false;
        }

        public List<KlachtControl> GetAlleKlachtenControl()
        {
            try
            {
                if (xklachtenContainer.Controls.Count == 0) return new List<KlachtControl>();
                return xklachtenContainer.Controls.Cast<KlachtControl>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<KlachtControl>();
            }
        }

        public KlachtControl GetKlachtControl(object id)
        {
            for (int i = 0; i < xklachtenContainer.Controls.Count; i++)
            {
                if (xklachtenContainer.Controls[i] is KlachtControl control)
                {
                    if (control.Klacht != null && control.Klacht.Equals(id))
                    {
                        return control;
                    }
                }
            }

            return null;
        }

        public void AddKlacht(KlachtEntry entry, bool select)
        {
            try
            {
                var Kl = new KlachtControl();
                Kl.InitKlacht(entry);
                Kl.Dock = DockStyle.Top;
                Kl.Padding = new Padding(5);
                Kl.Margin = new Padding(5);
                Kl.KlacktClicked += Kl_KlacktClicked;
                xklachtenContainer.Controls.Add(Kl);
                Kl.SendToBack();
                if (select)
                {
                    Kl.Select();
                    Kl.Focus();
                    SelectControl(entry, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Kl_KlacktClicked(object sender, EventArgs e)
        {
            if (sender is KlachtControl {Klacht: { }} control)
                SelectControl(control.Klacht, true);

        }

        private void SelectControl(KlachtEntry entry, bool selected)
        {
            if (entry == null) return;
            if (selected)
            {
                var kc = GetAlleKlachtenControl();
                foreach (var c in kc)
                {
                    if (c.Klacht != null && c.Klacht.Equals(entry))
                        c.IsSelected = true;
                    else c.IsSelected = false;
                }
            }
            else
            {
                var c = GetKlachtControl(entry.ID);
                if (c != null)
                    c.IsSelected = false;
            }

            OnSelectedChanged();
        }

        public void UpdateKlacht(KlachtEntry entry)
        {
            try
            {
                var control = GetKlachtControl(entry.ID);
                bool isvalid = IsAllowed(entry);
                if (control != null)
                {
                    if (isvalid)
                    {
                        control.InitKlacht(entry);
                        control.Invalidate();
                    }
                    else
                    {
                        xklachtenContainer.SuspendLayout();
                        xklachtenContainer.Controls.Remove(control);
                        xklachtenContainer.ResumeLayout(true);
                    }
                }
                else if(isvalid)
                {
                    xklachtenContainer.SuspendLayout();
                    AddKlacht(entry,true);
                    xklachtenContainer.ResumeLayout(true);
                }
                UpdateKlachtenCount();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateKlachtenCount()
        {
            try
            {
                var xklachten = Manager.Klachten.GetAlleKlachten();
                var xunread = Manager.Klachten.GetUnreadKlachten();
                var x1 = xklachten.Count == 1 ? "Klacht" : "Klachten";
                this.Text = $"{xklachten.Count} {x1}";
                if (xunread.Count > 0)
                    this.Text += $", waarvan {xunread.Count} niet gelezen";
                this.Invalidate();
            }
            catch (Exception e)
            {
               
            }
        }

        private void Klachten_KlachtDeleted(object sender, EventArgs e)
        {
            if (IsDisposed || Disposing) return;
            if (sender is string id)
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(() => DeleteEntry(id)));
                else DeleteEntry(id);
            }
        }

        private void Klachten_KlachtChanged(object sender, EventArgs e)
        {
            if (IsDisposed || Disposing) return;
            if (sender is KlachtEntry entry)
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(() => UpdateKlacht(entry)));
                else UpdateKlacht(entry);
            }
        }

        private void KlachtenForm_Shown(object sender, EventArgs e)
        {
            LoadEntries();
            InitEvents();
        }

        private void KlachtenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DetachEvents();
        }

        public void InitEvents()
        {
            Manager.KlachtChanged += Klachten_KlachtChanged;
            Manager.KlachtDeleted += Klachten_KlachtDeleted;
        }

        public void DetachEvents()
        {
            Manager.KlachtChanged -= Klachten_KlachtChanged;
            Manager.KlachtDeleted -= Klachten_KlachtDeleted;
        }

        private void xsearchbox_Enter(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim().ToLower().StartsWith("zoeken"))
                xsearchbox.Text = "";
        }

        private void xsearchbox_Leave(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim() == "")
                xsearchbox.Text = "Zoeken...";
        }

        private void xaddklacht_Click(object sender, EventArgs e)
        {
            if (Manager.Klachten == null) return;
            var xklacht = new NewKlachtForm(false);
            if (xklacht.ShowDialog(this) == DialogResult.OK)
            {
                xklacht.Klacht.Afzender = Manager.Opties?.Username;
                Manager.Klachten.SaveKlacht(xklacht.Klacht,true);
            }
        }

        private void xeditklacht_Click(object sender, EventArgs e)
        {
            if (Manager.Klachten == null) return;
            var xs = SelectedKlacht;
            if (xs?.Klacht == null) return;
            var xklacht = new NewKlachtForm(false, xs.Klacht);
            if (xklacht.ShowDialog(this) == DialogResult.OK)
            {
                xklacht.Klacht.GelezenDoor.Clear();
                Manager.Klachten.SaveKlacht(xklacht.Klacht,true);
            }
        }

        private void xdeleteklacht_Click(object sender, EventArgs e)
        {
            if (Manager.Klachten == null) return;
            var xs = SelectedKlacht;
            if (xs?.Klacht == null) return;
            if (XMessageBox.Show(this, $"Weetje zeker dat je '{xs.Klacht.Onderwerp}' wilt verwijderen?", "Verwijderen",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                Manager.Klachten.RemoveKlacht(xs.Klacht);
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (Manager.Klachten == null) return;
            if (xsearchbox.Text.Trim().ToLower().StartsWith("zoeken")) return;
            LoadEntries();
        }

        public void UpdateSelectedButtons()
        {
            bool xvalid = Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis};
            var xselected = SelectedKlacht;
            xaddklacht.Enabled = xvalid;
            xeditklacht.Enabled = xvalid && xselected is {Klacht: {AllowEdit: true}};
            xdeleteklacht.Enabled = xvalid && xselected is { Klacht: { AllowEdit: true } };
            xshowkrachtinfo.Enabled = xselected != null;
        }

        public event EventHandler SelectedChanged;

        protected virtual void OnSelectedChanged()
        {
            UpdateSelectedButtons();
            SelectedChanged?.Invoke(this, EventArgs.Empty);
        }

        private void xshowkrachtinfo_Click(object sender, EventArgs e)
        {
            var xs = SelectedKlacht;
            if (xs?.Klacht == null) return;
            new KlachtInfoForm(xs.Klacht).ShowDialog(this);
        }
    }
}
