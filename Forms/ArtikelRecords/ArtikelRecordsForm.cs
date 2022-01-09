using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Productie.ArtikelRecords;
using Rpm.Various;

namespace Forms.ArtikelRecords
{
    public partial class ArtikelRecordsForm : MetroFramework.Forms.MetroForm
    {
        public List<ArtikelRecord> Records = new List<ArtikelRecord>();
        public ArtikelRecordsForm()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.time_management_tasks_64x64);
            imageList1.Images.Add(Resources.time_management_tasks_64x64.CombineImage(Resources.Note_msgIcon_32x32,1.75));
            ((OLVColumn) xArtikelList.Columns[7]).AspectGetter = AantalproductiesGetter;
            ((OLVColumn) xArtikelList.Columns[0]).ImageGetter = ImageGetter;
        }

        private object ImageGetter(object item)
        {
            if (item is ArtikelRecord record)
                return record.Opmerkingen.Count > 0 ? 1 : 0;
            return 0;
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

        public void EnableButton()
        {
            xdeleteartikel.Enabled = xArtikelList.SelectedObjects.Count > 0 && Manager.LogedInGebruiker is
            {
                AccesLevel: >= AccesType.ProductieAdvance
            };
            xaddartikel.Enabled = Manager.LogedInGebruiker is
            {
                AccesLevel: >= AccesType.ProductieAdvance
            };
            xalgemeen.Enabled = Manager.LogedInGebruiker is
            {
                AccesLevel: >= AccesType.ProductieBasis
            };
            xopmerkingen.Enabled = xArtikelList.SelectedObjects.Count == 1;
        }

        public void LoadArtikels(bool reloaddb)
        {
            try
            {
                if (Manager.ArtikelRecords?.Database == null) return;
                if (reloaddb)
                    Records = Manager.ArtikelRecords.Database.GetAllEntries<ArtikelRecord>(new List<string>()
                        {"algemeen"});
                xArtikelList.BeginUpdate();
                xArtikelList.SetObjects(Records.Where(IsAllowed));
                xArtikelList.EndUpdate();
                EnableButton();
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
            if (xname.ToLower().StartsWith("algemeen")) return;
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
                EnableButton();
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
                EnableButton();
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
                if (xname.ToLower().StartsWith("algemeen")) return;
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

        private void xArtikelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButton();
        }

        private void xaddartikel_Click(object sender, EventArgs e)
        {
            if (Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieAdvance})
            {
                try
                {
                    var xnew = new NewArtikelRecord();
                    if (xnew.ShowDialog() == DialogResult.OK)
                    {
                        if (Manager.ArtikelRecords?.Database == null) return;
                        var xs = xnew.SelectedRecord;
                        bool exist = Manager.ArtikelRecords.Database.Exists(xs.ArtikelNr);
                        if (exist)
                        {
                            XMessageBox.Show($"Artikelnr '{xs.ArtikelNr}' bestaat al!", "Bestaat Al",
                                MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (IsAllowed(xs))
                        {
                            xArtikelList.AddObject(xs);
                            xArtikelList.SelectedObject = xs;
                            xArtikelList.SelectedItem?.EnsureVisible();
                        }
                        Manager.ArtikelRecords.Database.Upsert(xs.ArtikelNr, xs, false);

                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void xdeleteartikel_Click(object sender, EventArgs e)
        {
            if (Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieAdvance })
            {
                if (xArtikelList.SelectedObjects.Count > 0)
                {
                    try
                    {
                        var xremove = xArtikelList.SelectedObjects.Cast<ArtikelRecord>().ToList();
                        if (xremove.Count == 0) return;
                        var x1 = xremove.Count == 1 ? "artikel" : "artikelen";
                        if (XMessageBox.Show($"Weetje zeker dat je {xremove.Count} {x1} wilt verwijderen?", "Verwijderen",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            foreach (var xr in xremove)
                            {
                                xArtikelList.RemoveObject(xr);
                                Records.Remove(xr);
                                Manager.ArtikelRecords?.Database?.Delete(xr.ArtikelNr);
                            }

                            EnableButton();
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }
            }
        }

        private void xopmerkingen_Click(object sender, EventArgs e)
        {
            if (xArtikelList.SelectedObject is ArtikelRecord record)
            {
                var xop = new ArtikelOpmerkingenForm(record);
                xop.ShowDialog();
            }
        }

        private void xalgemeen_Click(object sender, EventArgs e)
        {
            if (Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis})
            {
                var xop = new ArtikelOpmerkingenForm();
                xop.LoadAlgemeenOpmerkingen();
                xop.ShowDialog();
            }
        }
    }
}
