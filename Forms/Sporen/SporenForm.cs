using BrightIdeasSoftware;
using ProductieManager.Properties;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Controls;
using Forms.Sporen;

namespace Forms
{
    public partial class SporenForm : MetroFramework.Forms.MetroForm
    {
        private List<SpoorEntry> Sporen { get; set; } = new List<SpoorEntry>();
        public SporenForm()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.geometry_measure_32x32);
            ((OLVColumn) xVerpakkingen.Columns[0]).ImageGetter = (x) => 0;
            InitList(true);
        }

        private void InitList(bool reload)
        {
            try
            {
                var xselected = xVerpakkingen.SelectedObject;
                xVerpakkingen.BeginUpdate();
                if (reload)
                {
                    Sporen?.Clear();
                    if (Manager.SporenBeheer is {Disposed: false})
                    {
                        Sporen = Manager.SporenBeheer.GetAlleSporen();
                    }
                }
                this.Text = $"Aangepaste Materiaal Verbruik Overzicht[{Sporen?.Count??0}]";
                this.Invalidate();
                Sporen ??= new List<SpoorEntry>();
                string xfilter = xsearch.Text.Trim();
                var xitems = Sporen;
                if (xfilter.Length > 0)
                    xitems = Sporen.Where(x => x.ContainsFilter(xfilter)).ToList();
                xVerpakkingen.SetObjects(xitems);
                xVerpakkingen.SelectedObject = xselected;
                if (xVerpakkingen.SelectedObject == null)
                {
                    xVerpakkingen.SelectedIndex = 0;
                }

                xVerpakkingen.SelectedItem?.EnsureVisible();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            xVerpakkingen.EndUpdate();
        }

        private void metroTextBox1_TextChanged(object sender, System.EventArgs e)
        {
            InitList(false);
        }

        private void VerpakkingenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.SpoorChanged -= Manager_VerpakkingChanged;
            Manager.SpoorDeleted -= Manager_VerpakkingDeleted;
        }

        private void Manager_VerpakkingDeleted(object sender, System.EventArgs e)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    if (sender is string value)
                    {
                        var xremoved = Sporen.RemoveAll(x =>
                            string.Equals(x.ArtikelNr, value, StringComparison.CurrentCultureIgnoreCase));
                        if (xremoved > 0)
                        {
                            InitList(false);
                        }
                    }

                }));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void UpdateSpoor(SpoorEntry spoor)
        {
            var xold = Sporen.FirstOrDefault(x =>
                string.Equals(x.ArtikelNr, spoor.ArtikelNr, StringComparison.CurrentCultureIgnoreCase));
            if (xold != null)
            {
                var xindex = Sporen.IndexOf(xold);
                if (xindex != -1)
                {
                    Sporen[xindex] = spoor;
                }
            }
            else
            {
                Sporen.Add(spoor);
                xold = spoor;
            }

            UpdateSporen(spoor);
        }

        private void Manager_VerpakkingChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    if (sender is SpoorEntry spoor)
                    {
                        UpdateSpoor(spoor);
                    }

                }));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        private void UpdateSporen(SpoorEntry spoor)
        {
            try
            {
                var xitems = xVerpakkingen.Objects.Cast<SpoorEntry>().ToList();
                var xold = xitems.FirstOrDefault(x =>
                    string.Equals(x.ArtikelNr, spoor.ArtikelNr, StringComparison.CurrentCultureIgnoreCase));
                bool valid = spoor != null && spoor.ContainsFilter(xsearch.Text.Trim());

                if (xold == null && valid)
                {
                    xVerpakkingen.AddObject(spoor);
                    xVerpakkingen.SelectedObject = spoor;
                    xVerpakkingen.SelectedItem?.EnsureVisible();
                }
                else if (xold != null)
                {
                    if (valid)
                    {
                        xVerpakkingen.RefreshObject(spoor);
                    }
                    else xVerpakkingen.RemoveObject(xold);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void VerpakkingenForm_Shown(object sender, System.EventArgs e)
        {
            Manager.SpoorChanged += Manager_VerpakkingChanged;
            Manager.SpoorDeleted += Manager_VerpakkingDeleted;
           // InitList(true);
        }

        private void xVerpakkingen_SelectedIndexChanged(object sender, EventArgs e)
        {
            xdelete.Enabled = xVerpakkingen.SelectedObjects.Count > 0;
            verwijderenToolStripMenuItem.Enabled = xdelete.Enabled;
            if (xVerpakkingen.SelectedObject is SpoorEntry xspoor)
            {
                var xverb = productieVerbruikUI1;
                xverb.UpdateFields(true, xspoor);
                xverb.Title = $"[{xspoor.ArtikelNr}] {xspoor.ProductOmschrijving}";
                xverb.Visible = true;
            }
            else
            {
                productieVerbruikUI1.Visible = false;
            }
        }

        private void xdelete_Click(object sender, EventArgs e)
        {
            if (xVerpakkingen.SelectedObjects.Count > 0)
            {
                if (XMessageBox.Show("Weetje zekere dat je alle geselecteerde verpakkingen wilt verwijderen?",
                        "Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    var xitems = xVerpakkingen.SelectedObjects.Cast<SpoorEntry>();
                    foreach (var item in xitems)
                    {
                        if (Manager.SporenBeheer is {Disposed: false})
                        {
                            Manager.SporenBeheer.RemoveSpoor(item);
                        }
                    }

                    xdelete.Enabled = xVerpakkingen.SelectedObjects.Count > 0;
                }
            }
        }

        private void xadd_Click(object sender, EventArgs e)
        {
            
            var xnew = new NewSpoorForm();
            if (xnew.ShowDialog() == DialogResult.OK)
            {
                if (Manager.SporenBeheer == null || Manager.SporenBeheer.Disposed)
                    return;
                xVerpakkingen.AddObject(xnew.SelectedSpoor);
                xVerpakkingen.SelectedObject = xnew.SelectedSpoor;
                xVerpakkingen.SelectedItem?.EnsureVisible();
                UpdateSpoor(xnew.SelectedSpoor);
                Manager.SporenBeheer.SaveSpoor(xnew.SelectedSpoor,
                    $"Spoor [{xnew.SelectedSpoor.ArtikelNr}] {xnew.SelectedSpoor.ProductOmschrijving} aangemaakt!");
            }
        }
    }
}
