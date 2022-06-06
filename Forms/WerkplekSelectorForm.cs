using Forms.MetroBase;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class WerkplekSelectorForm : MetroBaseForm
    {
        public WerkplekSelectorForm(ViewState[] bewerkingstates, bool filter, bool checkall)
        {
            InitializeComponent();
            LoadBewerkingen(bewerkingstates, filter, checkall);
        }

        public WerkplekSelectorForm(List<Bewerking> bws, bool checkall)
        {
            InitializeComponent();
            Bewerkingen = bws;
            ListBewerkingen(bws, checkall);
        }

        public List<WerkPlek> SelectedWerkplekken { get; private set; } = new List<WerkPlek>();
        public List<Bewerking> Bewerkingen { get; private set; } = new List<Bewerking>();

        private async void LoadBewerkingen(ViewState[] bewerkingstates, bool filter, bool checkall)
        {
            try
            {
                Bewerkingen = await Manager.GetBewerkingen(bewerkingstates, filter,true);
                ListBewerkingen(Bewerkingen, checkall);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void ListBewerkingen(List<Bewerking> bws, bool allchecked)
        {

            try
            {
                xbewerkinglijst.BeginUpdate();
                xbewerkinglijst.Items.Clear();
                imageList1.Images.Clear();
                imageList1.Images.Add(Resources.iconfinder_technology.CombineImage(Resources.play_button_icon_icons_com_60615,
                    2));
                imageList1.Images.Add(Resources.operation);
                foreach (var bw in bws)
                {
                    if (!bw.IsAllowed(xsearchbox.Text.ToLower().Replace("zoeken...", "").Trim())) continue;

                    foreach (var wp in bw.WerkPlekken)
                    {
                        if (!wp.Personen.Any(x => x.IngezetAanKlus(wp.Path))) continue;
                        var lv = new ListViewItem(wp.Naam)
                        {
                            Tag = wp,
                            ImageIndex = 0,
                        };
                        if (allchecked)
                            lv.Checked = true;
                        lv.SubItems.Add(bw.Omschrijving);
                        lv.SubItems.Add(bw.ArtikelNr);
                        lv.SubItems.Add(bw.ProductieNr);
                        xbewerkinglijst.Items.Add(lv);
                    }
                }

                xbewerkinglijst.EndUpdate();
                xbewerkinglijst.Invalidate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }

            
        }

        private void xok_Click(object sender, System.EventArgs e)
        {

            try
            {
                SelectedWerkplekken.Clear();
                foreach (var lv in xbewerkinglijst.Items)
                {
                    if (lv is ListViewItem {Checked: true, Tag: WerkPlek plek})
                        SelectedWerkplekken.Add(plek);
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xannuleren_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void selecteerAllesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in xbewerkinglijst.Items)
                if (item is ListViewItem lv)
                    lv.Checked = true;
        }

        private void deselecteerAllesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(var item in xbewerkinglijst.Items)
                if (item is ListViewItem lv)
                    lv.Checked = false;
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
            ListBewerkingen(Bewerkingen,false);
            _Filter = filter;
        }

        private void xsearchArtikel_Leave(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim() == "")
                xsearchbox.Text = @"Zoeken...";
        }
    }
}
