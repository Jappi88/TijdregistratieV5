using BrightIdeasSoftware;
using ProductieManager.Properties;
using Rpm.Productie;
using Rpm.Productie.AantalHistory;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Forms.Aantal.Controls
{
    public partial class AantalGemaaktHistoryUI : UserControl
    {
        public WerkPlek Plek { get; private set; }
        public AantalGemaaktHistoryUI()
        {
            InitializeComponent();
            ((OLVColumn) xHistoryList.Columns[3]).AspectGetter = TijdGewerktGetter;
            ((OLVColumn)xHistoryList.Columns[4]).AspectGetter = PerUurGetter;
            ((OLVColumn)xHistoryList.Columns[6]).AspectGetter = GestoptGetter;
            //((OLVColumn) xHistoryList.Columns[0]).ImageGetter = (o) => 0;
            imageList1.Images.Add(Resources.Count_tool_34564);
        }

        private object TijdGewerktGetter(object item)
        {
            if (Plek == null) return 0;
            if (item is AantalRecord record)
            {
                return record.GetTijdGewerkt(Plek.Tijden, Plek.GetStoringen());
            }

            return 0;
        }

        private object PerUurGetter(object item)
        {
            if (Plek == null) return 0;
            if (item is AantalRecord record)
            {
                return record.GetPerUur(Plek.Tijden, Plek.GetStoringen());
            }

            return 0;
        }

        private object GestoptGetter(object item)
        {
            if (Plek == null) return "N.V.T.";
            if (item is AantalRecord record)
            {
                return record.GetGestopt();
            }

            return "N.V.T.";
        }

        private object ActiefGetter(object item)
        {
            if (Plek == null) return "N.V.T.";
            if (item is AantalRecord record)
            {
                if (record.IsActive)
                    return "Ja";
                return "Nee";
            }

            return "N.V.T.";
        }

        public void UpdateList(WerkPlek plek)
        {
            try
            {
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(() => xUpdateList(plek)));
                else xUpdateList(plek);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xUpdateList(WerkPlek plek)
        {
            try
            {
                Plek = plek;
                if (plek == null)
                {
                    xHistoryList.SetObjects(new List<AantalRecord>());
                    UpdateButtons();
                    return;
                }

                var xcuritems = xHistoryList.Items.Count > 0
                    ? xHistoryList.Objects.Cast<AantalRecord>().ToList()
                    : new List<AantalRecord>();
                var remove = xcuritems.Where(x => !plek.AantalHistory.Aantallen.Any(a=> a.Equals(x))).ToList();
                xHistoryList.BeginUpdate();
                foreach (var item in remove)
                {
                    xHistoryList.RemoveObject(item);
                    xcuritems.Remove(item);
                }

                var selected = xHistoryList.SelectedObject;
                foreach (var aantal in plek.AantalHistory.Aantallen)
                {
                    var xold = xcuritems.FirstOrDefault(aantal.Equals);
                    if (xold != null)
                        xHistoryList.RefreshObject(aantal);
                    else xHistoryList.AddObject(aantal);
                }

                xHistoryList.SelectedObject = selected;
                xHistoryList.SelectedItem?.EnsureVisible();
                UpdateButtons();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                xHistoryList.EndUpdate();
            }
        }

        public AantalRecord Selected
        {
            get => xHistoryList.SelectedObject as AantalRecord;
            set
            {
                xHistoryList.SelectedObject = value;
                xHistoryList.SelectedItem?.EnsureVisible();
            }
        }

        private void verwijderenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sel = Selected;
            if (sel == null || Plek?.Werk == null) return;
            bool actief = sel.IsActive;
            if (Plek.AantalHistory.Aantallen.Remove(sel))
            {
                xHistoryList.RemoveObject(sel);
                if (actief)
                {
                    var xlast = Plek.AantalHistory.Aantallen.LastOrDefault();
                    if (xlast != null)
                        xlast.EndDate = new DateTime();
                }
                Plek.Werk.UpdateBewerking(null, $"[{Plek.Path}]\n" +
                                                $"Aantalregel van {sel.Aantal} succesvol verwijderd!");
            }
        }

        private void UpdateButtons()
        {
            verwijderenToolStripMenuItem.Enabled = Selected is {IsActive: false} && Manager.LogedInGebruiker is
            {
                AccesLevel: > AccesType.ProductieBasis
            };
            wijzigenToolStripMenuItem.Enabled = Selected != null && Manager.LogedInGebruiker is
            {
                AccesLevel: > AccesType.ProductieBasis
            };
            toevoegenToolStripMenuItem.Enabled = Manager.LogedInGebruiker is
            {
                AccesLevel: > AccesType.ProductieBasis
            };
        }

        private void xHistoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void toevoegenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Plek?.Werk == null) return;
            var xnew = new NewAantalRecordForm();
            if (xnew.ShowDialog() == DialogResult.OK)
            {
                Plek.AantalHistory.Aantallen.Insert(0, xnew.SelectedRecord);
                xHistoryList.InsertObjects(0, new List<AantalRecord>() {xnew.SelectedRecord});
                Plek.Werk.UpdateBewerking(null, $"[{Plek.Path}]\n" +
                                                $"Nieuwe 'Aantal Record' toegevoegd!");
            }
        }

        private void wijzigenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = Selected;
            if (selected != null && Plek?.Werk != null)
            {
                var xnew = new NewAantalRecordForm(selected);
                if (xnew.ShowDialog() == DialogResult.OK)
                {
                    var index = Plek.AantalHistory.Aantallen.IndexOf(selected);
                    if (index > -1)
                    {
                        Plek.AantalHistory.Aantallen[index] = xnew.SelectedRecord;
                        xHistoryList.RefreshObject(xnew.SelectedRecord);
                        Plek.Werk.UpdateBewerking(null, $"[{Plek.Path}]\n" +
                                                        $"'Aantal Record' Gewijzigd!");
                    }
                }
            }
        }
    }
}
