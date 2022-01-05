using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Productie.ArtikelRecords;

namespace Forms.ArtikelRecords
{
    public partial class ArtikelRecordsForm : MetroFramework.Forms.MetroForm
    {
        public List<ArtikelRecord> Records = new List<ArtikelRecord>();
        public ArtikelRecordsForm()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.time_management_tasks_64x64);
            ((OLVColumn) xArtikelList.Columns[7]).AspectGetter = AantalproductiesGetter;
            ((OLVColumn) xArtikelList.Columns[0]).ImageGetter = (x) => 0;
        }

        private object AantalproductiesGetter(object item)
        {
            if (item is ArtikelRecord record)
                return record.UpdatedProducties.Count;
            return 0;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        public void LoadArtikels(bool reloaddb)
        {
            try
            {
                if (Manager.ArtikelRecords?.Database == null) return;
                if (reloaddb)
                    Records = Manager.ArtikelRecords.Database.GetAllEntries<ArtikelRecord>();
                xArtikelList.BeginUpdate();
                xArtikelList.SetObjects(Records.Where(IsAllowed));
                xArtikelList.EndUpdate();
                string x1 = Records.Count == 1 ? "Artikel" : "Artikels";
                this.Text = $"Totaal {Records.Count} {x1}";
                this.Invalidate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Database_InstanceDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
            if (Manager.ArtikelRecords?.Database == null) return;
            var xname = Path.GetFileNameWithoutExtension(e.FullPath);
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(() => DeleteFromList(xname)));
            else DeleteFromList(xname);
        }

        private void DeleteFromList(string artnr)
        {
            try
            {
                if (xArtikelList.Items.Count == 0) return;
                var xremove = xArtikelList.Objects.Cast<ArtikelRecord>().Where(x =>
                    string.Equals(artnr, x.ArtikelNr, StringComparison.CurrentCultureIgnoreCase)).ToList();
                xArtikelList.RemoveObjects(xremove);
                Records.RemoveAll(x => string.Equals(artnr, x.ArtikelNr, StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateArtikel(ArtikelRecord record)
        {
            try
            {
                if (record == null) return;
                if (Manager.ArtikelRecords?.Database == null) return;
                var xitems = xArtikelList.Objects.Cast<ArtikelRecord>().ToList();
                var xold = xitems.FirstOrDefault(x => x.Equals(record));
                bool valid = IsAllowed(record);
                if (xold != null)
                {
                    if (!valid)
                        xArtikelList.RemoveObject(xold);
                    else xArtikelList.RefreshObject(record);
                }
                else
                {
                    if (valid)
                    {
                        xArtikelList.AddObject(record);
                    }
                }

                var index = Records.IndexOf(record);
                if (index > -1)
                    Records[index] = record;
                else Records.Add(record);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public string Filter => xsearchbox.Text.ToLower().Replace("zoeken...", "").Trim();

        public bool IsAllowed(ArtikelRecord record)
        {
            if (string.IsNullOrEmpty(Filter)) return true;
            return (record.Omschrijving != null && record.Omschrijving.ToLower().Contains(Filter)) ||
                   (record.ArtikelNr != null && record.ArtikelNr.ToLower().Contains(Filter));

        }


        private void Database_InstanceChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            if (Manager.ArtikelRecords?.Database == null) return;
            try
            {
                var xname = Path.GetFileNameWithoutExtension(e.FullPath);
                var xrecord = Manager.ArtikelRecords.Database.GetEntry<ArtikelRecord>(xname);
                if (xrecord != null)
                {
                    if (this.InvokeRequired)
                        this.Invoke(new MethodInvoker(() => UpdateArtikel(xrecord)));
                    else UpdateArtikel(xrecord);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
           
        }

        private void xsearchArtikel_Enter(object sender, EventArgs e)
        {
            if (string.Equals(xsearchbox.Text.Trim(), "zoeken...", StringComparison.CurrentCultureIgnoreCase))
                xsearchbox.Text = "";
        }

        private string _Filter = String.Empty;
        private void xsearchArtikel_TextChanged(object sender, System.EventArgs e)
        {
            string filter = xsearchbox.Text.Replace("Zoeken...", "").Trim().ToLower();
            if (string.Equals(filter, _Filter, StringComparison.CurrentCultureIgnoreCase)) return;
            LoadArtikels(false);
            _Filter = filter;
        }

        private void xsearchArtikel_Leave(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim() == "")
                xsearchbox.Text = @"Zoeken...";
        }

        private void ArtikelRecordsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Manager.ArtikelRecords?.Database == null) return;
            Manager.ArtikelRecords.Database.FileChanged -= Database_InstanceChanged;
            Manager.ArtikelRecords.Database.FileDeleted -= Database_InstanceDeleted;
        }

        private void ArtikelRecordsForm_Shown(object sender, System.EventArgs e)
        {
            if (Manager.ArtikelRecords?.Database == null) return;
            Manager.ArtikelRecords.ArtikelChanged += Database_InstanceChanged;
            Manager.ArtikelRecords.ArtikelDeleted += Database_InstanceDeleted;
            LoadArtikels(true);
        }
    }
}
