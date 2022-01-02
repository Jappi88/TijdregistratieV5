﻿using System;
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
        public BewerkingSelectorForm(ViewState[] bewerkingstates, bool filter, bool showwerkplekken, bool checkall)
        {
            InitializeComponent();
            LoadBewerkingen(bewerkingstates, filter, showwerkplekken, checkall);
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

        public BewerkingSelectorForm(List<Bewerking> bws, bool showwerkplekken, bool checkall)
        {
            InitializeComponent();
            ListBewerkingen(bws, showwerkplekken, checkall);
        }

        public List<WerkPlek> SelectedWerkplekken { get; private set; } = new List<WerkPlek>();
        public List<Bewerking> SelectedBewerkingen { get; private set; } = new List<Bewerking>();

        private async void LoadBewerkingen(ViewState[] bewerkingstates, bool filter, bool showwerkplekken, bool checkall)
        {
            try
            {
                var bws = await Manager.GetBewerkingen(bewerkingstates, filter);
                ListBewerkingen(bws,showwerkplekken, checkall);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void ListBewerkingen(List<Bewerking> bws, bool showwerkplekken, bool allchecked)
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
                    if (showwerkplekken)
                    {
                        foreach (var wp in bw.WerkPlekken)
                        {
                            if (!wp.Personen.Any(x => x.IngezetAanKlus(wp.Path))) continue;
                            var lv = new ListViewItem(wp.Naam)
                            {
                                Tag = wp,
                                ImageIndex = 0,
                                Checked = allchecked
                            };
                            lv.SubItems.Add(bw.Omschrijving);
                            lv.SubItems.Add(bw.ArtikelNr);
                            lv.SubItems.Add(bw.ProductieNr);
                            xbewerkinglijst.Items.Add(lv);
                        }
                    }
                    else
                    {
                        var lv = new ListViewItem(bw.Naam)
                        {
                            Tag = bw,
                            ImageIndex = 1,
                            Checked = allchecked
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
                SelectedBewerkingen.Clear();
                foreach (var lv in xbewerkinglijst.Items)
                {
                    if (lv is ListViewItem {Checked: true, Tag: WerkPlek plek})
                        SelectedWerkplekken.Add(plek);
                    else if (lv is ListViewItem { Checked: true, Tag: Bewerking bew })
                        SelectedBewerkingen.Add(bew);
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
