using BrightIdeasSoftware;
using Forms.MetroBase;
using ProductieManager.Rpm.ExcelHelper;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductieManager.Properties;
using Rpm.ExcelHelper;
using Rpm.Opmerking;
using Rpm.Various;

namespace Forms.Sporen
{
    public partial class OptimaleLengteVerbruikForm : MetroBaseForm
    {
        public OptimaleLengteVerbruikForm()
        {
            InitializeComponent();
            xMaterialenLijst.AllowCellEdit = true;
            imageList1.Images.Add(Resources.bolts_construction_rivet_screw_screws_128x128);
            Generator.GenerateColumns(this.xMaterialenLijst, typeof(OptimaleVerbruikEntry), false);
            foreach (var col in this.xMaterialenLijst.AllColumns)
            {
                col.GroupFormatter = (group, parms) =>
                {
                    parms.GroupComparer = Comparer<OLVGroup>.Create((x, y) =>
                        Comparer.Compare(x, y, parms.PrimarySortOrder,
                            parms.PrimarySort ?? parms.SecondarySort));
                };
            }
        }

        public List<OptimaleVerbruikEntry> Results { get; set; }

        private async void LoadOptimaleVerbruik(OptimaleVerbruikInfo info, bool reload)
        {
            try
            {
                if (info == null || xMaterialenLijst.IsLoading) return;
                xMaterialenLijst.StartWaitUI("Alle Optimale UitgangsLengtes Berekenen");
                await LoadData(info, reload);
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }

            xMaterialenLijst.StopWait();
        }

        public bool HasReloaded { get; private set; }

        private Task LoadData(OptimaleVerbruikInfo info, bool reload)
        {
            return Task.Run(() =>
            {
                try
                {
                    xMaterialenLijst._isLoading = true;
                    if (reload || Results == null)
                    {
                        if (Manager.Database == null || Manager.Database.IsDisposed)
                            throw new Exception("Database is niet toegankelijk!");
                        HasReloaded = reload;
                        var prods = Manager.Database.GetAllProducties(true, false, null, true).Result;
                        if (prods.Count == 0) return;
                        var entries = new List<OptimaleVerbruikEntry>();
                        Results ??= new List<OptimaleVerbruikEntry>();
                        foreach (var prod in prods)
                        {
                            var xmats = prod.Materialen.Where(x =>
                                string.Equals(x.Eenheid, "m", StringComparison.CurrentCultureIgnoreCase)).ToList();
                            if (xmats.Count == 0) continue;
                            foreach (var mat in xmats)
                            {
                                if (mat.AantalPerStuk >= 1.0)
                                    continue;
                                var optimal = new OptimaleVerbruikEntry();
                                optimal.Productlengte = (decimal)Math.Round(mat.AantalPerStuk * 1000, 2);
                                if (optimal.Productlengte == 0) continue;
                                optimal.Omschrijving = mat.Omschrijving;
                                optimal.ArtikelNr = mat.ArtikelNr;
                                var xindex = entries.IndexOf(optimal);
                                if (xindex > -1)
                                {
                                    entries[xindex].Geproduceerd++;
                                    continue;
                                }

                                optimal.RestStuk = info.Reststuk;
                                if (Manager.SporenBeheer != null)
                                {
                                    var spoor = Manager.SporenBeheer.GetSpoor(mat.ArtikelNr) ??
                                                Manager.SporenBeheer.GetSpoor(prod.ArtikelNr);
                                    if (spoor != null)
                                    {
                                        optimal.Uitgangslengte = spoor.UitgangsLengte;
                                    }
                                }

                                if (info.Voorkeur1 > optimal.Productlengte)
                                    optimal.Voorkeur1 =
                                        OptimalUitgangsLengte(optimal.Productlengte, optimal.RestStuk, info.Voorkeur1);
                                if (info.Voorkeur2 > optimal.Productlengte)
                                    optimal.Voorkeur2 =
                                        OptimalUitgangsLengte(optimal.Productlengte, optimal.RestStuk, info.Voorkeur2);
                                if (info.Voorkeur3 > optimal.Productlengte)
                                    optimal.Voorkeur3 =
                                        OptimalUitgangsLengte(optimal.Productlengte, optimal.RestStuk, info.Voorkeur3);
                                var ent = Results.FirstOrDefault(x => x.Equals(optimal));
                                if (ent != null)
                                {
                                   optimal.Uitgangslengte = ent.Uitgangslengte;
                                   optimal.Productlengte = ent.Productlengte;
                                   optimal.Omschrijving = ent.Omschrijving;
                                   //optimal.Changes.Add("Uitgangslengte", ent.Uitgangslengte);
                                   //optimal.Changes.Add("Productlengte", ent.Productlengte);
                                   //optimal.Changes.Add("Omschrijving", ent.Omschrijving);
                                }

                                entries.Add(optimal);
                            }
                        }

                        //ensure all uitgangslengtes
                        for (int i = 0; i < entries.Count; i++)
                        {
                            var ent = entries[i];
                            if (ent.Uitgangslengte == 0)
                            {
                                var xent = entries.FirstOrDefault(x =>
                                    string.Equals(x.ArtikelNr, ent.ArtikelNr,
                                        StringComparison.CurrentCultureIgnoreCase) && x.Uitgangslengte > 0);
                                if (xent != null)
                                {
                                    ent.Uitgangslengte = xent.Uitgangslengte;
                                }
                            }
                        }
                        Results = entries;
                    }

                    var res = Results;
                    var xcrit = xzoekbalk.Text.Trim();
                    if (!string.IsNullOrEmpty(xcrit) &&
                        !string.Equals(xcrit, "zoeken...", StringComparison.CurrentCultureIgnoreCase))
                        res = Results.Where(x => IsAllowed(x, xcrit)).ToList();
                    xMaterialenLijst.SetObjects(res);
                    UpdateButtons();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                xMaterialenLijst._isLoading = false;
            });
        }

        private void UpdateButtons()
        {
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(UpdateButtons));
            else
            {
                xallesafronden.Enabled = xMaterialenLijst.Items.Count > 0;
                xopslaan.Enabled = HasChanges();
            }
        }


        public bool HasChanges()
        {
            return Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieAdvance } && (HasReloaded || (Results != null && Results.Any(x=> x.Changes.Count > 0)));
        }

        public bool IsAllowed(OptimaleVerbruikEntry entry, string crit)
        {
            try
            {
                if (string.IsNullOrEmpty(crit)) return true;
                var props = entry.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var val = prop.GetValue(entry).ToString();
                    if (string.IsNullOrEmpty(val)) continue;
                    if (val.ToLower().Contains(crit.ToLower()))
                        return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private OptimaleVerbruikInfo _Info;

        private void xmaakoverzicht_Click(object sender, EventArgs e)
        {
            try
            {
                var xform = new OptimaleVerbruikSettingForm();
                xform.SelectedInfo = _Info;
                if (xform.ShowDialog() == DialogResult.OK)
                {
                    _Info = xform.SelectedInfo;
                    LoadOptimaleVerbruik(_Info, true);
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        public decimal OptimalUitgangsLengte(decimal prodlengte, decimal rest, decimal maxlengte)
        {
            var xcurlengte = maxlengte;
            if (prodlengte == 0) return maxlengte;
            var xrest = (xcurlengte % prodlengte);
            if (xrest >= rest)
            {
                var dif = xrest - rest;
                xcurlengte -= dif;
            }
            else
            {
                var xaantallen = xcurlengte - rest;
                xrest = (xaantallen % prodlengte);
                if (xrest == 0)
                    xcurlengte = xaantallen;
                else
                    xcurlengte -= xrest; // - xrest;
            }

            return Math.Round(xcurlengte, 2);
        }

        private void xMaterialenLijst_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            if (e.RowObject is OptimaleVerbruikEntry entry)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(e.NewValue.ToString().Trim()))
                        throw new Exception("Vul in een geldige naam");
                    var xnewname = e.NewValue.ToString();
                    var xoldname = e.Value.ToString();
                    if (string.Equals(xnewname, xoldname))
                    {
                        e.Cancel = true;
                    }

                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    e.Control.Text = e.Value.ToString();
                    e.NewValue = e.Value;
                    XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);

                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void xMaterialenLijst_CellEditFinished(object sender, CellEditEventArgs e)
        {
            if (e.RowObject is OptimaleVerbruikEntry entry)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(e.NewValue.ToString().Trim()))
                        throw new Exception("Vul in een geldige naam");
                    var xnewname = e.NewValue.ToString();
                    var xoldname = e.Value.ToString();
                    if (string.Equals(xnewname, xoldname))
                    {
                        e.Cancel = true;
                        return;
                    }

                    var prop = entry.GetType().GetProperty(e.Column.AspectName);
                    if (prop != null)
                    {
                        var xnewvalue = xnewname.ToObjectValue(prop.PropertyType);
                        var xold = prop.GetValue(entry);
                        if (!xold.Equals(xnewvalue))
                        {
                            if(entry.GetType().GetProperty("Changes")?.GetValue(entry) is Dictionary<string,object> changes)
                            {
                                if (changes.ContainsKey(e.Column.AspectName))
                                {
                                    if (changes[e.Column.AspectName].Equals(xnewvalue))
                                        changes.Remove(e.Column.AspectName);
                                }
                                else changes.Add(e.Column.AspectName, xold);
                                entry.GetType().GetProperty("Changes")?.SetValue(entry, changes);
                            }
                            prop.SetValue(entry,xnewvalue);
                        }
                    }
                    xMaterialenLijst.RefreshObject(entry);
                    UpdateButtons();
                }
                catch (Exception ex)
                {
                    XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
                }
            }
        }

        private void OptimaleLengteVerbruikForm_Shown(object sender, EventArgs e)
        {
            LoadResult();
            if (_Info == null)
                xmaakoverzicht.PerformClick();
            else
            {
                LoadData(_Info, false);
            }
        }

        private async void xok_Click(object sender, EventArgs e)
        {
            if (xMaterialenLijst.IsLoading) return;
            try
            {
                var save = new SaveFileDialog();
                save.Filter = "Excel Bestand|*.Xlsx";
                save.Title = "Exporteer data naar Excel";
                save.FileName =
                    $"OptimaleUitgangslengtes_{DateTime.Now.ToString("G").Replace("-", "").Replace(":", "").Replace(" ", "")}";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    xMaterialenLijst.StartWaitUI("Exporteren naar Excel");
                    var path = await ExcelWorkbook.ExportToExcel(xMaterialenLijst, save.FileName, "Optimale Lengtes",
                        null,new List<CellMargeCheck>());
                    if (!string.IsNullOrEmpty(path))
                        Process.Start(path);
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }

            xMaterialenLijst.StopWait();
        }

        private List<CellMargeCheck> CreateCellMarges()
        {
            var xret = new List<CellMargeCheck>();
            xret.Add(new CellMargeCheck() { ColumnName = "Reststuk1", BasevalueName = "HuidigeReststuk", Marge = 10 });
            xret.Add(new CellMargeCheck() { ColumnName = "Reststuk2", BasevalueName = "HuidigeReststuk", Marge = 10 });
            xret.Add(new CellMargeCheck() { ColumnName = "Reststuk3", BasevalueName = "HuidigeReststuk", Marge = 10 });
            xret.Add(new CellMargeCheck() { ColumnName = "Voorkeur1Stuks", BasevalueName = "AantalStuks", Marge = 10 });
            xret.Add(new CellMargeCheck() { ColumnName = "Voorkeur2Stuks", BasevalueName = "AantalStuks", Marge = 10 });
            xret.Add(new CellMargeCheck() { ColumnName = "Voorkeur3Stuks", BasevalueName = "AantalStuks", Marge = 10 });
            return xret;
        }

        private void xzoekbalk_Enter(object sender, EventArgs e)
        {
            if (string.Equals(xzoekbalk.Text.Trim(), "zoeken...", StringComparison.CurrentCultureIgnoreCase))
            {
                xzoekbalk.Text = "";
            }
        }

        private void xzoekbalk_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xzoekbalk.Text.Trim()))
                xzoekbalk.Text = "Zoeken...";
        }

        private void xcleartext_Click(object sender, EventArgs e)
        {
            xzoekbalk.Text = "";
            xzoekbalk.Select();
            xzoekbalk.Focus();
        }

        private string _LastCrit;

        private void xzoekbalk_TextChanged(object sender, EventArgs e)
        {
            if (xzoekbalk.Text.ToLower() == "zoeken...") return;
            if (string.Equals(_LastCrit, xzoekbalk.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
                return;
            _LastCrit = xzoekbalk.Text;
            LoadData(_Info, false);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var e = new KeyEventArgs(keyData);
            if (e.KeyCode == Keys.Delete)
            {
                return RemoveSelected();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool RemoveSelected()
        {
            try
            {
                if (xMaterialenLijst.SelectedObjects.Count > 0)
                {
                    var items = xMaterialenLijst.SelectedObjects.Cast<OptimaleVerbruikEntry>().ToList();
                    xMaterialenLijst.RemoveObjects(items);
                    foreach (var item in items)
                        Results?.Remove(item);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void verwijderenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelected();
        }

        private void Afronden(List<OptimaleVerbruikEntry> entries)
        {
            if (entries.Count > 0)
            {
                var form = new AfrondForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in entries)
                    {
                        var boven = !form.IsLager;
                        var vork1 = item.Voorkeur1;
                        var vork2 = item.Voorkeur2;
                        var vork3 = item.Voorkeur3;
                        var voorkeur1 = boven
                            ? Math.Ceiling(vork1 / form.Factor)
                            : Math.Floor(vork1 / form.Factor);
                        var voorkeur2 = boven
                            ? Math.Ceiling(vork2 / form.Factor)
                            : Math.Floor(vork2 / form.Factor);
                        var voorkeur3 = boven
                            ? Math.Ceiling(vork3 / form.Factor)
                            : Math.Floor(vork3 / form.Factor);
                        var xp1 = (voorkeur1 * form.Factor);
                        var xp2 = (voorkeur2 * form.Factor);
                        var xp3 = (voorkeur3 * form.Factor);
                        if (item.Voorkeur1 == xp1)
                        {
                            if (boven)
                               xp1 += form.Factor;
                            else xp1 -= form.Factor;
                        }

                        if (item.Changes.ContainsKey("Voorkeur1"))
                        {
                            if (item.Changes["Voorkeur1"].Equals(xp1))
                                item.Changes.Remove("Voorkeur1");
                        }
                        else item.Changes.Add("Voorkeur1", xp1);
                        item.Voorkeur1 = xp1;

                        if (item.Voorkeur2 == xp2)
                        {
                            if (boven)
                                xp2 += form.Factor;
                            else xp2 -= form.Factor;
                        }
                        if (item.Changes.ContainsKey("Voorkeur2"))
                        {
                            if (item.Changes["Voorkeur2"].Equals(xp2))
                                item.Changes.Remove("Voorkeur2");
                        }
                        else item.Changes.Add("Voorkeur2", xp2);
                        item.Voorkeur2 = xp2;

                        if (item.Voorkeur3 == xp3)
                        {
                            if (boven)
                               xp3 += form.Factor;
                            else xp3 -= form.Factor;
                        }

                        if (item.Changes.ContainsKey("Voorkeur3"))
                        {
                            if (item.Changes["Voorkeur3"].Equals(xp3))
                                item.Changes.Remove("Voorkeur3");
                        }
                        else item.Changes.Add("Voorkeur3", xp3);
                        item.Voorkeur3 = xp3;

                        xMaterialenLijst.RefreshObject(item);
                    }
                    UpdateButtons();
                }
            }
        }

        private void wijzigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (xMaterialenLijst.SelectedObjects.Count > 0)
                {
                    Afronden(xMaterialenLijst.SelectedObjects.Cast<OptimaleVerbruikEntry>().ToList());
                }
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xallesafronden_Click(object sender, EventArgs e)
        {
            try
            {
                if (xMaterialenLijst.Items.Count > 0)
                {
                    Afronden(xMaterialenLijst.Objects.Cast<OptimaleVerbruikEntry>().ToList());
                }
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xopslaan_Click(object sender, EventArgs e)
        {
            Opslaan();
        }

        private async void Opslaan()
        {
            try
            {
                if (Results != null && _Info != null && HasChanges())
                {
                    if (xMaterialenLijst.IsLoading) return;
                    xMaterialenLijst.StartWaitUI("Opslaan");
                    await Task.Run(() =>
                    {
                        var filename = Path.Combine(Manager.DbPath, "OptimaleResult.rpm");
                        using var fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                        try
                        {
                            var info = _Info.Serialize();
                            Results.ForEach(x => x.Changes.Clear());
                            var result = Results.Serialize();
                            if (info != null && result != null)
                            {
                                fs.Write(info, 0, info.Length);
                                fs.Write(result, 0, result.Length);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        fs.Flush();
                        fs.Close();
                        HasReloaded = false;
                        UpdateButtons();
                    });
                }
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }

            xMaterialenLijst.StopWait();
        }

        private void LoadResult()
        {
            try
            {
                var filename = Path.Combine(Manager.DbPath, "OptimaleResult.rpm");
                if (!File.Exists(filename)) return;
                using var fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                _Info = fs.DeSerialize<OptimaleVerbruikInfo>();
                Results = fs.DeSerialize<List<OptimaleVerbruikEntry>>();
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void OptimaleLengteVerbruikForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HasChanges())
            {
                var result = XMessageBox.Show(this, "Er zijn wijzigingen gemaakt, wil je opslaan?", "Opslaan",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (result == DialogResult.Yes)
                    Opslaan();
            }
        }
    }
}
