using BrightIdeasSoftware;
using ProductieManager.Rpm.Productie;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Exception = System.Exception;

namespace Forms
{
    public partial class MateriaalVerbruikForm : Forms.MetroBase.MetroBaseForm
    {
        public List<MateriaalEntryInfo> Materialen = new List<MateriaalEntryInfo>();
        public MateriaalVerbruikForm()
        {
            InitializeComponent();
            xstart.Value = DateTime.Now.Subtract(TimeSpan.FromDays(365));
            xsearchbox.ShowClearButton = true;
            //((OLVColumn) xmateriaalList.Columns[2]).AspectGetter = VerbruikAspect;
            //((OLVColumn)xmateriaalList.Columns[3]).AspectGetter = AfkeurAspect;
        }

        private object VerbruikAspect(object item)
        {
            if (item == null) return 0;
            if (item is MateriaalEntryInfo entry)
            {
                return entry.Verbruik.ToString() + $" {entry.Eenheid}";
            }

            return 0;
        }

        private object AfkeurAspect(object item)
        {
            if (item == null) return 0;
            if (item is MateriaalEntryInfo entry)
            {
                return entry.Afkeur.ToString() + $" {entry.Eenheid}";
            }

            return 0;
        }

        private bool _isbussy;
        public async void LoadMaterialen()
        {
            if (_isbussy) return;
            try
            {
                _isbussy = true;
                xloadinglabel.Visible = true;
                var mats = await MateriaalBeheer.GetMateriaalVerbruik(new TijdEntry(xstart.Value, xstop.Value, null));
                Materialen = mats.Select(x => x.Value).ToList();
                xmateriaalList.SetObjects(Materialen);
                xloadinglabel.Visible = false;
                this.Text = $"Materiaal verbruik vanaf {xstart.Value:D} t/m {xstop.Value:D}";
                UpdateStatus();
                this.Invalidate();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            _isbussy = false;
        }

        private void UpdateStatus()
        {
            if (xmateriaalList.Items.Count > 0)
            {
                List<MateriaalEntryInfo> xitems = new List<MateriaalEntryInfo>();
                if (xmateriaalList.SelectedObjects.Count > 0)
                {
                    if (xmateriaalList.SelectedObjects.Count == 1)
                    {
                        var item = xmateriaalList.SelectedObject as MateriaalEntryInfo;
                        xselected.Text = $"[{item?.ArtikelNr}]{item?.Omschrijving}";
                        xitems = xmateriaalList.Objects.Cast<MateriaalEntryInfo>().ToList();
                    }
                    else
                    {
                        xselected.Text = "";
                        xitems = xmateriaalList.SelectedObjects.Cast<MateriaalEntryInfo>().ToList();
                    }
                }
                else
                {
                    xselected.Text = "";
                    xitems = xmateriaalList.Objects.Cast<MateriaalEntryInfo>().ToList();
                }

                var xafkstuks = xitems.Where(x => x.Eenheid.ToLower().StartsWith("stuks")).Sum(x => x.Afkeur);
                var xafkmeter = xitems.Where(x => !x.Eenheid.ToLower().StartsWith("stuks")).Sum(x => x.Afkeur);
                var xverbrstuks = xitems.Where(x => x.Eenheid.ToLower().StartsWith("stuks")).Sum(x => x.Verbruik);
                var xverbrmeter = xitems.Where(x => !x.Eenheid.ToLower().StartsWith("stuks")).Sum(x => x.Verbruik);
                xmaterialencount.Text = $"Materialen: {xitems.Count}";
                xafkeurstuks.Text = $"Afkeur(Stuks): {xafkstuks} stuk";
                xafkeurmeter.Text = $"Afkeur(m): {xafkmeter} meter";
                xverbruikstuks.Text = $"Vebruik(Stuks): {xverbrstuks} stuk";
                xverbruikmeter.Text = $"Verbruik(m): {xverbrmeter} meter";
            }
            else
            {
                xselected.Text = "";
                xmaterialencount.Text = "Materialen: 0";
                xafkeurstuks.Text = "Afkeur(Stuks): 0 stuk";
                xafkeurmeter.Text = "Afkeur(m): 0 meter";
                xverbruikstuks.Text = "Vebruik(Stuks): 0 stuk";
                xverbruikmeter.Text = "Verbruik(m): 0 meter";
            }
        }

        private void DoProgress(object sender, ProgressArg arg)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                xloadinglabel.Text = arg.Message + $"{arg.Progress}%";
                this.Invalidate();
            }));
            Application.DoEvents();
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xloadmaterialen_Click(object sender, EventArgs e)
        {
            LoadMaterialen();
        }

        private void xsearchbox_Enter(object sender, EventArgs e)
        {
            if (xsearchbox.Text.ToLower() == "zoeken...")
                xsearchbox.Text = "";
        }

        private void xsearchbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xsearchbox.Text.Trim()))
                xsearchbox.Text = "Zoeken...";
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearchbox.Text.ToLower() == "zoeken..." || _isbussy) return;
            var xcrit = xsearchbox.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(xcrit))
                xmateriaalList.SetObjects(Materialen);
            else
                xmateriaalList.SetObjects(Materialen.Where(x => (!string.IsNullOrEmpty(x.ArtikelNr) &&
                    x.ArtikelNr.ToLower().Contains(xcrit)) || (!string.IsNullOrEmpty(x.Omschrijving) && x.Omschrijving.ToLower().Contains(xcrit))));
            UpdateStatus();
        }

        private void xmateriaalList_FormatCell(object sender, FormatCellEventArgs e)
        {
            switch (e.Column.Text.ToLower())
            {
                case "artikelnr":
                    e.SubItem.BackColor = Color.LightCyan;
                    break;
                case "omschrijving":
                    e.SubItem.BackColor = Color.LightSkyBlue;
                    break;
                case "verbruik":
                    e.SubItem.BackColor = Color.PaleGreen;
                    break;
                case "afkeur":
                    e.SubItem.BackColor = Color.PaleVioletRed;
                    break;
                case "gemiddeld p/eenheid":
                    e.SubItem.BackColor = Color.LightSteelBlue;
                    break;
                case "aantal producties":
                    e.SubItem.BackColor = Color.AliceBlue;
                    break;
                case "eenheid":
                    e.SubItem.BackColor = Color.LightGoldenrodYellow;
                    break;
            }
        }

        private void MateriaalVerbruikForm_Shown(object sender, EventArgs e)
        {
            LoadMaterialen();
        }

        private void xmateriaalList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }
    }
}
