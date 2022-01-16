﻿using BrightIdeasSoftware;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Productie;
using Rpm.Productie.ArtikelRecords;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
            ((OLVColumn)xwerkpleklist.Columns[7]).AspectGetter = AantalproductiesGetter;
            ((OLVColumn)xwerkpleklist.Columns[0]).ImageGetter = ImageGetter;
            LoadArtikels(true);
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

        public void EnableButton(bool updatetitle)
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
            xeditArtikel.Enabled = xArtikelList.SelectedObjects.Count > 0 && Manager.LogedInGebruiker is
            {
                AccesLevel: >= AccesType.ProductieAdvance
            };
            xopmerkingen.Enabled = xArtikelList.SelectedObjects.Count == 1 && Manager.LogedInGebruiker is
            {
                AccesLevel: >= AccesType.ProductieBasis
            };

            xdeletewerkplek.Enabled = xwerkpleklist.SelectedObjects.Count > 0 && Manager.LogedInGebruiker is
            {
                AccesLevel: >= AccesType.ProductieAdvance
            };
            
            xeditWerkplek.Enabled = xwerkpleklist.SelectedObjects.Count > 0 && Manager.LogedInGebruiker is
            {
                AccesLevel: >= AccesType.ProductieAdvance
            };
            xaddwerkplek.Enabled = Manager.LogedInGebruiker is
            {
                AccesLevel: >= AccesType.ProductieAdvance
            };
            xwerkplekopmerkingen.Enabled = xwerkpleklist.SelectedObjects.Count == 1 && Manager.LogedInGebruiker is
            {
                AccesLevel: >= AccesType.ProductieAdvance
            };
            if (updatetitle)
                UpdateTitle();
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
                xArtikelList.SetObjects(Records.Where(x => IsAllowed(x) && !x.IsWerkplek));
                xArtikelList.EndUpdate();
                xwerkpleklist.BeginUpdate();
                xwerkpleklist.SetObjects(Records.Where(x => IsAllowed(x) && x.IsWerkplek));
                xwerkpleklist.EndUpdate();
                EnableButton(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateTitle()
        {
            try
            {
                var r1 = Records.Where(x => !x.IsWerkplek).ToList();
                var r2 = Records.Where(x => x.IsWerkplek).ToList();
                string x1 = r1.Count == 1 ? "Artikel" : "Artikels";
                string x2 = r2.Count == 1 ? "Werkplek" : "Werkplaatsen";
                this.Text = $"Totaal {r1.Count} {x1} en {r2.Count} {x2}";
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
                if (xremove.Count > 0)
                    xArtikelList.RemoveObjects(xremove);
                else
                {
                    xremove = xwerkpleklist.Objects.Cast<ArtikelRecord>().Where(x =>
                        string.Equals(artnr, x.ArtikelNr, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (xremove.Count > 0)
                        xwerkpleklist.RemoveObjects(xremove);
                }
                Records.RemoveAll(x => string.Equals(artnr, x.ArtikelNr, StringComparison.CurrentCultureIgnoreCase));
                EnableButton(true);
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
                var xwps = xwerkpleklist.Objects.Cast<ArtikelRecord>().ToList();
                xitems.AddRange(xwps);
                var xold = xitems.FirstOrDefault(x => x.Equals(record));
                bool valid = IsAllowed(record);
                if (xold != null)
                {
                    if (!valid)
                    {
                        if (xold.IsWerkplek)
                            xwerkpleklist.RemoveObject(xold);
                        else
                            xArtikelList.RemoveObject(xold);
                    }
                    else
                    {
                        if (record.IsWerkplek)
                            xwerkpleklist.RefreshObject(record);
                        else xArtikelList.RefreshObject(record);
                    }
                }
                else
                {
                    if (valid)
                    {
                        if (record.IsWerkplek)
                            xwerkpleklist.AddObject(record);
                        else
                            xArtikelList.AddObject(record);
                    }
                }

                var index = Records.IndexOf(record);
                if (index > -1)
                    Records[index] = record;
                else Records.Add(record);
                EnableButton(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public string Filter1 => xsearchbox.Text.ToLower().Replace("zoeken...", "").Trim();
        public string Filter2 => xsearch2.Text.ToLower().Replace("zoeken...", "").Trim();

        public bool IsAllowed(ArtikelRecord record)
        {
            if (record.IsWerkplek)
            {
                if (string.IsNullOrEmpty(Filter2)) return true;
                return record.ArtikelNr != null && record.ArtikelNr.ToLower().Contains(Filter2);
            }
            if (string.IsNullOrEmpty(Filter1)) return true;
            return (record.Omschrijving != null && record.Omschrijving.ToLower().Contains(Filter1)) ||
                   (record.ArtikelNr != null && record.ArtikelNr.ToLower().Contains(Filter1));

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
            if (xsearchbox.Text.Trim().ToLower().Contains("zoeken..."))
                xsearchbox.Text = @"";
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
        }

        private void xArtikelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButton(false);
        }

        private void xaddartikel_Click(object sender, EventArgs e)
        {
            if (Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieAdvance})
            {
                try
                {
                    var xnew = new NewArtikelRecord();
                    xnew.IsWerkplek = metroTabControl1.SelectedIndex == 1;
                    if (xnew.ShowDialog() == DialogResult.OK)
                    {
                        if (Manager.ArtikelRecords?.Database == null) return;
                        var xs = xnew.SelectedRecord;
                        bool exist = Manager.ArtikelRecords.Database.Exists(xs.ArtikelNr);
                        if (exist)
                        {
                            XMessageBox.Show($"Artikelnr/ Werkplek '{xs.ArtikelNr}' bestaat al!", "Bestaat Al",
                                MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (IsAllowed(xs))
                        {
                            if (xs.IsWerkplek)
                            {
                                xwerkpleklist.AddObject(xs);
                                xwerkpleklist.SelectedObject = xs;
                                xwerkpleklist.SelectedItem?.EnsureVisible();
                                metroTabControl1.SelectedIndex = 1;
                            }
                            else
                            {
                                xArtikelList.AddObject(xs);
                                xArtikelList.SelectedObject = xs;
                                xArtikelList.SelectedItem?.EnsureVisible();
                                metroTabControl1.SelectedIndex = 0;
                            }
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

        private void DeleteSelectedArtikelen()
        {
            try
            {
                var xremove = xArtikelList.SelectedObjects.Cast<ArtikelRecord>().ToList();
                if (xremove.Count == 0) return;
                var x1 = xremove.Count == 1 ? "artikel" : "artikelen";
                var changed = false;
                if (XMessageBox.Show($"Weetje zeker dat je {xremove.Count} {x1} wilt verwijderen?",
                        "Verwijderen",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (var xr in xremove)
                    {
                        if (xr.IsWerkplek)
                            xwerkpleklist.RemoveObject(xr);
                        else
                            xArtikelList.RemoveObject(xr);
                        Records.Remove(xr);
                        changed = true;
                        Manager.ArtikelRecords?.Database?.Delete(xr.ArtikelNr);
                    }

                    EnableButton(changed);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void DeleteSelectedWerkPlaatsen()
        {
            try
            {
                var xremove = xwerkpleklist.SelectedObjects.Cast<ArtikelRecord>().ToList();
                if (xremove.Count == 0) return;
                var x1 = xremove.Count == 1 ? "werkplek" : "werkplaatsen";
                var changed = false;
                if (XMessageBox.Show($"Weetje zeker dat je {xremove.Count} {x1} wilt verwijderen?",
                        "Verwijderen",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (var xr in xremove)
                    {
                        if (xr.IsWerkplek)
                            xwerkpleklist.RemoveObject(xr);
                        else
                            xArtikelList.RemoveObject(xr);
                        Records.Remove(xr);
                        changed = true;
                        Manager.ArtikelRecords?.Database?.Delete(xr.ArtikelNr);
                    }

                    EnableButton(changed);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void xdeleteartikel_Click(object sender, EventArgs e)
        {
            if (Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieAdvance })
            {
                if (metroTabControl1.SelectedIndex == 0)
                {
                    if (xArtikelList.SelectedObjects.Count > 0)
                    {
                        DeleteSelectedArtikelen();
                    }
                }
                else
                {
                    if (xwerkpleklist.SelectedObjects.Count > 0)
                    {
                        DeleteSelectedWerkPlaatsen();
                    }
                }
            }
        }

        private void xopmerkingen_Click(object sender, EventArgs e)
        {
            var xindex = metroTabControl1.SelectedIndex;
            if (xindex == 0)
            {
                if (xArtikelList.SelectedObject is ArtikelRecord record)
                {
                    var xop = new ArtikelOpmerkingenForm(record);
                    xop.Text = $"{record.ArtikelNr} Meldingen";
                    xop.ShowDialog();
                }
            }
            else
            {
                if (xwerkpleklist.SelectedObject is ArtikelRecord record)
                {
                    var xop = new ArtikelOpmerkingenForm(record);
                    xop.Text = $"{record.ArtikelNr} Meldingen";
                    xop.ShowDialog();
                }
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

        private void xsearch2_Enter(object sender, EventArgs e)
        {
            if (xsearch2.Text.Trim().ToLower().Contains("zoeken..."))
                xsearch2.Text = @"";
        }

        private void xsearch2_Leave(object sender, EventArgs e)
        {
            if (xsearch2.Text.Trim() == "")
                xsearch2.Text = @"Zoeken...";
        }

        private string _Filter2 = String.Empty;
        private void xsearch2_TextChanged(object sender, EventArgs e)
        {
            string filter = xsearch2.Text.Replace("Zoeken...", "").Trim().ToLower();
            if (string.Equals(filter, _Filter2, StringComparison.CurrentCultureIgnoreCase)) return;
            LoadArtikels(false);
            _Filter2 = filter;
        }

        private void xeditArtikel_Click(object sender, EventArgs e)
        {
            if (Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieAdvance })
            {
                try
                {
                    if (metroTabControl1.SelectedIndex == 0 && xArtikelList.SelectedObject is ArtikelRecord record)
                    {
                        var xform = new NewArtikelRecord(record);
                        xform.AllowArtikelEdit = false;
                        xform.AllowWerkplekEdit = false;
                        if (xform.ShowDialog() == DialogResult.OK)
                        {
                            xArtikelList.RefreshObject(record);
                            Manager.ArtikelRecords.Database.Upsert(record.ArtikelNr, record, false);
                        }
                    }
                    else if(metroTabControl1.SelectedIndex == 1 && xwerkpleklist.SelectedObject is ArtikelRecord xrecord)
                    {
                        var xform = new NewArtikelRecord(xrecord);
                        xform.AllowArtikelEdit = false;
                        xform.AllowWerkplekEdit = false;
                        if (xform.ShowDialog() == DialogResult.OK)
                        {
                            xwerkpleklist.RefreshObject(xrecord);
                            Manager.ArtikelRecords.Database.Upsert(xrecord.ArtikelNr, xrecord, false);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            meldingenToolStripMenuItem.Enabled = metroTabControl1.SelectedIndex == 0 && xopmerkingen.Enabled || (metroTabControl1.SelectedIndex == 1 && xwerkplekopmerkingen.Enabled);
            wijzigenToolStripMenuItem.Enabled = metroTabControl1.SelectedIndex == 0 && xeditArtikel.Enabled || (metroTabControl1.SelectedIndex == 1 && xeditWerkplek.Enabled);
            verwijderenToolStripMenuItem.Enabled = metroTabControl1.SelectedIndex == 0 && xdeleteartikel.Enabled || (metroTabControl1.SelectedIndex == 1 && xdeletewerkplek.Enabled);
        }
    }
}
