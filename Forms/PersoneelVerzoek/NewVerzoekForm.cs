using Forms.MetroBase;
using MetroFramework.Controls;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Verzoeken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Forms.PersoneelVerzoek
{
    public partial class NewVerzoekForm : MetroBaseForm
    {
        public Personeel Personeel { get; set; }
        public VerzoekEntry Verzoek { get; set; }

        public NewVerzoekForm()
        {
            InitializeComponent();
            SetPersoneelNamen();
            this.Load += NewVerzoekForm_Load;
            xVerzoekType.SelectedIndex = 0;
        }

        private void SetPersoneelNamen()
        {
            try
            {
                var namen = Manager.Database.PersoneelLijst?.GetAllIDs(false) ?? new List<string>();
                xPersoneelNaam.AutoCompleteCustomSource = new System.Windows.Forms.AutoCompleteStringCollection();
                xPersoneelNaam.AutoCompleteCustomSource.AddRange(namen.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void NewVerzoekForm_Load(object sender, EventArgs e)
        {
            InitFormUI();
        }

        public void InitFormUI()
        {
            var x = Verzoek ??= new VerzoekEntry();
            if(x.VerzoekSoort != VerzoekType.Verwijderen)
                xVerzoekType.Items.RemoveAt(2);
            xPersoneelNaam.Text = x.PersoneelNaam;
            xMelderNaam.Text = x.NaamMelder;
            var xtype = (int)x.VerzoekSoort;
            xVerzoekType.SelectedIndex = xtype;
            xVanafDate.SetValue(x.StartDatum);
            xTotDate.SetValue(x.EindDatum);
            if (Personeel?.PersoneelNaam != null)
                xPersoneelNaam.Text = Personeel.PersoneelNaam;
            if (string.IsNullOrEmpty(xPersoneelNaam.Text.Trim()))
            {
                xPersoneelNaam.Focus();
                xPersoneelNaam.Select();
            }
            else if (string.IsNullOrEmpty(xMelderNaam.Text.Trim()))
            {
                xMelderNaam.Focus();
                xMelderNaam.Select();
            }
        }

        public NewVerzoekForm(VerzoekEntry entry) : this()
        {
            Verzoek = entry;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is MetroTextBox textBox)
            {
                textBox.ShowClearButton = true;
                textBox.Invalidate();
            }
        }

        private bool DoCheck()
        {
            if (string.IsNullOrEmpty(xPersoneelNaam.Text.Trim()))
            {
                XMessageBox.Show(this, "Vul in of kies een PersoneelNaam.", "Ongeldige Personeel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(xMelderNaam.Text.Trim()))
            {
                XMessageBox.Show(this, "Vul in jou naam a.u.b.", "Ongeldige Naam", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (xVerzoekType.SelectedIndex == -1)
            {
                XMessageBox.Show(this, "Kies een Verzoeksoort a.u.b.", "Ongeldige Verzoeksoort", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void xOK_Click(object sender, EventArgs e)
        {
            if (DoCheck())
            {
                Verzoek ??= new VerzoekEntry();
                Verzoek.PersoneelNaam = xPersoneelNaam.Text.Trim();
                Verzoek.NaamMelder = xMelderNaam.Text.Trim();
                Verzoek.VerzoekSoort = (VerzoekType)xVerzoekType.SelectedIndex;
                Verzoek.StartDatum = xVanafDate.Value;
                Verzoek.EindDatum = xTotDate.Value;
                if (Manager.LogedInGebruiker is { AccesLevel: < Rpm.Various.AccesType.Manager } || Verzoek.Status == VerzoekStatus.Geen)
                    Verzoek.Status = VerzoekStatus.InAfwachting;
                DialogResult = DialogResult.OK;
            }
        }

        private void xVanafDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotaalTijd();
        }

        private void UpdateTotaalTijd()
        {
            try
            {
                Verzoek ??= new VerzoekEntry();
                var spec = new List<Rooster>();
                if (xVerzoekType.SelectedIndex == 1 && xVanafDate.Value.Date == xTotDate.Value.Date)
                {
                    if ((Manager.Opties?.NationaleFeestdagen?.Any(x => x.Date == xVanafDate.Value.Date) ?? false) || (xVanafDate.Value.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday))
                    {
                        spec.Add(new Rooster() { StartWerkdag = xVanafDate.Value.TimeOfDay.Subtract(TimeSpan.FromSeconds(xVanafDate.Value.TimeOfDay.Seconds)), EindWerkdag = xTotDate.Value.TimeOfDay.Subtract(TimeSpan.FromSeconds(xTotDate.Value.TimeOfDay.Seconds)), Vanaf = xVanafDate.Value.Date });
                    }
                }
                Verzoek.TotaalTijd = Werktijd.TijdGewerkt(xVanafDate.Value, xTotDate.Value, Personeel?.WerkRooster, spec).TotalHours;
                groupBox1.Text = $"Verzoek Periode ({Math.Round(Verzoek.TotaalTijd, 2)} uur)";
                groupBox1.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void xVerzoekType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTotaalTijd();
        }

        private void xKiesPersoneel_Click(object sender, EventArgs e)
        {
            try
            {
                var xpers = new PersoneelsForm(null, true);
                if (xpers.ShowDialog(this) == DialogResult.OK)
                {
                    var pers = xpers.SelectedPersoneel?.First();
                    xPersoneelNaam.Text = pers?.PersoneelNaam;
                    Personeel = pers;
                    xPersoneelNaam.Select();
                    xPersoneelNaam.Focus();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
