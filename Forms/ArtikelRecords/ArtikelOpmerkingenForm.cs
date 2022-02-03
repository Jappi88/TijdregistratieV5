using BrightIdeasSoftware;
using ProductieManager.Properties;
using Rpm.Productie;
using Rpm.Productie.ArtikelRecords;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Forms.ArtikelRecords
{
    public partial class ArtikelOpmerkingenForm : Forms.MetroBase.MetroBaseForm
    {
        public List<ArtikelOpmerking> Opmerkingen = new List<ArtikelOpmerking>();
        public ArtikelRecord Record { get; set; }

        public string Title
        {
            get => this.Text;
            set
            {
                this.Text = value;
                this.Invalidate();
            }
        }

        public ArtikelOpmerkingenForm()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.default_opmerking_16757_32x32);
            ((OLVColumn)xOpmerkingenList.Columns[0]).ImageGetter = (x) => 0;
        }

        public ArtikelOpmerkingenForm(ArtikelRecord record):this()
        {
            Record = record;
            Opmerkingen = record.Opmerkingen;
        }

        public ArtikelOpmerkingenForm(List<ArtikelOpmerking> opmerkingen) : this()
        {
            Opmerkingen = opmerkingen;
        }

        public void LoadAlgemeenOpmerkingen()
        {
            try
            {
                Opmerkingen = Manager.ArtikelRecords?.GetAllAlgemeenRecordsOpmerkingen();
                LoadList();
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        public void LoadList()
        {
            try
            {
                Opmerkingen ??= new List<ArtikelOpmerking>();
                xOpmerkingenList.BeginUpdate();
                xOpmerkingenList.SetObjects(Opmerkingen.Where(IsAllowed).ToList());
                xOpmerkingenList.EndUpdate();
                EnableButtons();
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        public bool SaveOpmerkingen()
        {
            try
            {
                if (Record != null)
                {

                    Record.Opmerkingen = Opmerkingen;
                    Record.LaatstGeupdate = DateTime.Now;
                    Manager.ArtikelRecords?.Database?.Upsert(Record.ArtikelNr, Record, false);
                }
                else
                {
                    Manager.ArtikelRecords?.SaveAlgemeenOpmerkingen(Opmerkingen);
                }

                return true;
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
                return false;
            }
        }

        public bool IsAllowed(ArtikelOpmerking opmerking)
        {
            try
            {
                if (opmerking == null) return false;
                var xfilter = xsearchbox.Text.ToLower().Replace("zoeken...", "").Trim();
                if (string.IsNullOrEmpty(xfilter)) return true;
                if (opmerking.Opmerking.ToLower().Contains(xfilter))
                    return true;
                if (opmerking.GeplaatstDoor.ToLower().Contains(xfilter))
                    return true;
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void EnableButtons()
        {
            var selected = xOpmerkingenList.SelectedObjects.Cast<ArtikelOpmerking>().ToList();
            xdeleteOpmerking.Enabled = selected.Count > 0 && selected.Any(x => x.IsFromMe);
            xWijzigOpmerking.Enabled = selected.Count == 1 && selected.Any(x => x.IsFromMe);
        }

        private void xsearchArtikel_Enter(object sender, EventArgs e)
        {
            if (string.Equals(xsearchbox.Text.Trim(), "zoeken...", StringComparison.CurrentCultureIgnoreCase))
                xsearchbox.Text = "";
        }

        private string _Filter = string.Empty;
        private void xsearchArtikel_TextChanged(object sender, System.EventArgs e)
        {
            string filter = xsearchbox.Text.Replace("Zoeken...", "").Trim().ToLower();
            if (string.Equals(filter, _Filter, StringComparison.CurrentCultureIgnoreCase)) return;
            LoadList();
            _Filter = filter;
        }

        private void xsearchArtikel_Leave(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim() == "")
                xsearchbox.Text = @"Zoeken...";
        }

        private void xOpmerkingenList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void ArtikelOpmerkingenForm_Shown(object sender, EventArgs e)
        {
            LoadList();
            if (Manager.ArtikelRecords?.Database != null)
            {
                Manager.ArtikelRecords.ArtikelChanged += ArtikelRecords_ArtikelChanged;
                Manager.ArtikelRecords.ArtikelDeleted += ArtikelRecords_ArtikelDeleted;
            }
        }

        private void ArtikelRecords_ArtikelDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                if(e.Name.ToLower().StartsWith("algemeen"))
                    if (Record != null)
                        return;
                if (Record != null)
                {
                    var xid = Path.GetFileNameWithoutExtension(e.FullPath);
                    if (string.Equals(Record.ArtikelNr, xid, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (this.InvokeRequired)
                            this.Invoke(new MethodInvoker(this.Close));
                        else
                            this.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void ArtikelRecords_ArtikelChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (this.IsDisposed || Disposing) return;
                if (e.Name.ToLower().StartsWith("algemeen"))
                    if (Record != null)
                        return;
                    else
                    {
                        Opmerkingen = Manager.ArtikelRecords?.GetAllAlgemeenRecordsOpmerkingen() ?? new List<ArtikelOpmerking>();
                        if (this.InvokeRequired)
                            this.Invoke(new MethodInvoker(LoadList));
                        else
                            LoadList();
                    }
                if (Record != null)
                {
                    var xid = Path.GetFileNameWithoutExtension(e.FullPath);
                    if (string.Equals(Record.ArtikelNr, xid, StringComparison.CurrentCultureIgnoreCase))
                    {
                        var xrecord = Manager.ArtikelRecords?.Database?.GetEntry<ArtikelRecord>(xid);
                        if (xrecord == null)
                            return;
                        Record = xrecord;
                        Opmerkingen = xrecord.Opmerkingen;
                        if (this.InvokeRequired)
                            this.Invoke(new MethodInvoker(LoadList));
                        else
                            LoadList();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void ArtikelOpmerkingenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Manager.ArtikelRecords?.Database != null)
            {
                Manager.ArtikelRecords.ArtikelChanged -= ArtikelRecords_ArtikelChanged;
                Manager.ArtikelRecords.ArtikelDeleted -= ArtikelRecords_ArtikelDeleted;
            }
        }

        private void xOK_Click(object sender, EventArgs e)
        {
          
            DialogResult = DialogResult.OK;
            SaveOpmerkingen();
        }

        private void xAddOpmerking_Click(object sender, EventArgs e)
        {
            Opmerkingen ??= new List<ArtikelOpmerking>();
            var newopmerking = new ArtikelOpmerkingForm();
            if (Record != null)
                newopmerking.Title = $"{Record.ArtikelNr} Melding Aanmaken";
            else
            {
                newopmerking.Title = $"Algemene Melding Aanmaken";
            }
            newopmerking.EnableWerkplekCheckbox = Record == null;
            if (newopmerking.ShowDialog() == DialogResult.OK)
            {
                var xop = newopmerking.SelectedOpmerking;
                xop.IsAlgemeen = Record == null;
                Opmerkingen.Add(xop);
                if (IsAllowed(xop))
                    xOpmerkingenList.AddObject(xop);
                xOpmerkingenList.SelectedObject = xop;
                xOpmerkingenList.SelectedItem?.EnsureVisible();
            }
        }

        private void xWijzigOpmerking_Click(object sender, EventArgs e)
        {
            if (xOpmerkingenList.SelectedObject is ArtikelOpmerking op)
            {
                if (!op.IsFromMe) return;
                var newopmerking = new ArtikelOpmerkingForm(op);
                newopmerking.EnableWerkplekCheckbox = Record == null;
                if (newopmerking.ShowDialog() == DialogResult.OK)
                {
                    var xop = newopmerking.SelectedOpmerking;
                    xop.IsAlgemeen = Record == null;
                    var index = Opmerkingen.IndexOf(op);
                    if (index > -1)
                        Opmerkingen[index] = xop;
                    if (IsAllowed(xop))
                        xOpmerkingenList.RefreshObject(xop);
                    xOpmerkingenList.SelectedObject = xop;
                    xOpmerkingenList.SelectedItem?.EnsureVisible();
                }
            }
        }

        private void xdeleteOpmerking_Click(object sender, EventArgs e)
        {
            if (xOpmerkingenList.SelectedObjects.Count > 0)
            {
                var xremove = xOpmerkingenList.SelectedObjects.Cast<ArtikelOpmerking>().Where(x => x.IsFromMe).ToList();
                if (xremove.Count == 0) return;
                var x1 = xremove.Count == 1 ? "melding" : "meldingen";
                if (XMessageBox.Show(this, $"Weetje zeker dat je {xremove.Count} {x1} wilt verwijderen?", "Verwijderen",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (var xr in xremove)
                    {
                        xOpmerkingenList.RemoveObject(xr);
                        Opmerkingen.Remove(xr);
                    }

                    EnableButtons();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
