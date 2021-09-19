using System;
using System.Drawing;
using System.Windows.Forms;
using Rpm.Misc;
using Rpm.Productie;

namespace Controls
{
    public partial class AfkeurEntryUI : UserControl
    {
        public AfkeurEntryUI()
        {
            InitializeComponent();
        }

        public Materiaal Materiaal { get; private set; }

        public void InitMateriaal(Materiaal materiaal)
        {
            Materiaal = materiaal;
            xeenheid.Text = Materiaal.Eenheid;
            if (Materiaal.Eenheid.ToLower().StartsWith("stuk"))
                xvalue.DecimalPlaces = 0;
            else xvalue.DecimalPlaces = 2;
            xvalue.SetValue((decimal) Materiaal.AantalAfkeur);
            xomschrijving.Text = Materiaal.Omschrijving;
            xartikelnr.Text = Materiaal.ArtikelNr;
            UpdateAantal();
        }

        private void UpdateAantal()
        {
            if (Materiaal == null)
            {
                xpercent.Text = "0.00$";
            }
            else
            {
                var aantal = (decimal) (Materiaal.Parent == null
                    ? Materiaal.Aantal
                    : Materiaal.AantalPerStuk * Materiaal.Parent.TotaalGemaakt);
                var value = xvalue.Value == 0 || aantal == 0 ? 0 : xvalue.Value / aantal;
                if (value == 0 && xvalue.Value > 0)
                    value = 1;
                var xperc = value.ToString("0.00%");
                var txtcolor = Color.Green;
                if (value > 0 && value < (decimal) 0.01)
                    txtcolor = Color.Orange;
                else if (value >= (decimal) 0.01)
                    txtcolor = Color.Red;
                xpercent.Text = xperc;
                xpercent.ForeColor = txtcolor;
                Materiaal.AantalAfkeur = (double) xvalue.Value;
                Invalidate();
            }
        }

        private void xvalue_ValueChanged(object sender, EventArgs e)
        {
            UpdateAantal();
        }
    }
}