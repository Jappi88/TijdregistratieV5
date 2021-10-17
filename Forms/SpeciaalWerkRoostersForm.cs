using BrightIdeasSoftware;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Forms;

namespace ProductieManager.Forms
{
    public partial class SpeciaalWerkRoostersForm : MetroFramework.Forms.MetroForm
    {
        public List<Rooster> Roosters { get; set; }
        public SpeciaalWerkRoostersForm()
        {
            InitializeComponent();
            roosterUI1.Enabled = false;
            ((OLVColumn) xroosterlist.Columns[0]).ImageGetter = sender => 0;
            ((OLVColumn) xroosterlist.Columns[0]).AspectGetter = DateGetter;
        }

        private object DateGetter(object item)
        {
            if (item is Rooster rooster)
            {
                return rooster.Vanaf.ToString("D");
            }

            return "N/A";
        }

        public SpeciaalWerkRoostersForm(List<Rooster> roosters) : this()
        {
            xroosterlist.SetObjects((roosters?.CreateCopy()?? new List<Rooster>()));
            if (xroosterlist.Items.Count > 0)
            {
                xroosterlist.SelectedIndex = 0;
                xroosterlist.SelectedItem?.EnsureVisible();
            }
            UpdateRoosterListLabel();
        }

        private void xroosterlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            xeditrooster.Enabled = xroosterlist.SelectedObjects.Count == 1;
            xdeleterooster.Enabled = xroosterlist.SelectedObjects.Count > 0;
            if (xroosterlist.SelectedObject is Rooster rooster)
            {
                xroosterdatelabel.Text = rooster.Vanaf.ToString("D");
                roosterUI1.WerkRooster = rooster;
                roosterUI1.Enabled = true;
            }
            else
            {
                xroosterdatelabel.Text = "";
                roosterUI1.Enabled = false;
            }
        }

        private void xOpslaan_Click(object sender, EventArgs e)
        {
            Roosters = xroosterlist.Objects.Cast<Rooster>().ToList();
            DialogResult = DialogResult.OK;
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void UpdateRoosterListLabel()
        {
            this.Text = $"Speciale Roosters ({xroosterlist.Items.Count})";
        }

        private void xaddrooster_Click(object sender, EventArgs e)
        {
            var dt = new DatumChanger();
            dt.DateFormat = "dddd dd MMMM yyyy";
            dt.DisplayText = "Kies datum voor de speciale rooster.\n\nSpeciale roosters zijn afwijkende roosters op specifieke dagen.";
            if (dt.ShowDialog() == DialogResult.OK)
            {
                var roosters = xroosterlist.Objects.Cast<Rooster>().ToList();
                if (roosters.Any(x => x.Vanaf.Date == dt.SelectedValue.Date))
                {
                    XMessageBox.Show($"Er is al een speciale rooster op '{dt.SelectedValue:D}'", "Rooster Bestaat Al",
                        MessageBoxIcon.Exclamation);
                    return;
                }
                var rooster = Rooster.StandaartRooster();
                rooster.GebruiktPauze = false;
                rooster.StartWerkdag = new TimeSpan(7, 0, 0);
                rooster.EindWerkdag = new TimeSpan(12, 0, 0);
                rooster.Vanaf = dt.SelectedValue;

                xroosterlist.AddObject(rooster);
                xroosterlist.SelectedObject = rooster;
                if (xroosterlist.SelectedItem != null)
                {
                    xroosterlist.SelectedItem.EnsureVisible();
                    xroosterdatelabel.Text = rooster.Vanaf.ToString("D");
                }
                xroosterlist.Sort(0);
                UpdateRoosterListLabel();
            }
        }

        private void xeditrooster_Click(object sender, EventArgs e)
        {
            if (xroosterlist.SelectedObject is Rooster rooster)
            {
                var dt = new DatumChanger();
                dt.DateFormat = "dddd dd MMMM yyyy";
                dt.DisplayText = $"Wijzig rooster datum van {rooster.Vanaf:D}";
                dt.SelectedValue = rooster.Vanaf;
                if (dt.ShowDialog() == DialogResult.OK)
                {
                    if (dt.SelectedValue.DayOfWeek != DayOfWeek.Saturday && dt.SelectedValue.DayOfWeek != DayOfWeek.Sunday)
                    {
                        XMessageBox.Show($"Speciale roosters zijn alleen voor een zaterdag of zondag!\n\n Je hebt '{dt.SelectedValue:D}' gekozen...", "Ongeldige Datum",
                            MessageBoxIcon.Exclamation);
                        return;
                    }
                    rooster.Vanaf = dt.SelectedValue;
                    xroosterlist.RefreshObject(rooster);
                    xroosterlist.SelectedObject = rooster;
                    if (xroosterlist.SelectedItem != null)
                    {
                        xroosterlist.SelectedItem.EnsureVisible();
                        xroosterdatelabel.Text = rooster.Vanaf.ToString("D");
                    }
                    xroosterlist.Sort(0);
                }
            }
        }

        private void xdeleterooster_Click(object sender, EventArgs e)
        {
            if (xroosterlist.SelectedObjects?.Count > 0)
            {
                //if (XMessageBox.Show("Wil je echt alle geselecteerde roosters verwijderen?", "Verwijderen",
                //    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) return;
                xroosterlist.RemoveObjects(xroosterlist.SelectedObjects);
                UpdateRoosterListLabel();
            }
        }
    }
}
