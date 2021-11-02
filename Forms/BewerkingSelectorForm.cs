using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Navigation;
using MetroFramework.Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Forms
{
    public partial class BewerkingSelectorForm : MetroForm
    {
        public BewerkingSelectorForm(ViewState[] bewerkingstates, bool filter)
        {
            InitializeComponent();
            LoadBewerkingen(bewerkingstates, filter);
        }

        public string Title
        {
            get => this.Text;
            set
            {
                this.Text = value;
                this.Invalidate();
            }
        }

        public BewerkingSelectorForm(List<Bewerking> bws)
        {
            InitializeComponent();
            ListBewerkingen(bws);
        }

        public List<WerkPlek> SelectedWerkplekken { get; private set; } = new List<WerkPlek>();

        private async void LoadBewerkingen(ViewState[] bewerkingstates, bool filter)
        {
            try
            {
                var bws = await Manager.GetBewerkingen(bewerkingstates, filter);
                ListBewerkingen(bws);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void ListBewerkingen(List<Bewerking> bws)
        {

            try
            {
                xbewerkinglijst.BeginUpdate();
                xbewerkinglijst.Items.Clear();
                imageList1.Images.Clear();
                imageList1.Images.Add(Resources.iconfinder_technology.CombineImage(Resources.play_button_icon_icons_com_60615,
                    2));
                foreach (var bw in bws)
                {
                    foreach (var wp in bw.WerkPlekken)
                    {
                        if (!wp.Personen.Any(x => x.IngezetAanKlus(wp.Path))) continue;
                        var lv = new ListViewItem(wp.Naam)
                        {
                            Tag = wp,
                            ImageIndex = 0,
                            Checked = true
                        };
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
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
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
                XMessageBox.Show(ex.Message, "Fout", MessageBoxIcon.Error);
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
    }
}
