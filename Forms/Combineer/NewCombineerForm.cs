using Rpm.Productie;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Forms.Combineer
{
    public partial class NewCombineerForm : Forms.MetroBase.MetroBaseForm
    {
        private readonly Bewerking _parent;
        private readonly Bewerking _productie;
        public CombineerEntry SelectedEntry { get; set; }
        public NewCombineerForm(Bewerking parent, Bewerking combi)
        {
            InitializeComponent();
            _parent = parent;
            _productie = combi;
            this.Text = $"Combineer [{_productie.ProductieNr}] {_productie.Naam} van {_productie.Omschrijving}";
            UpdateText();
            this.Invalidate();
        }

        private void UpdateText()
        {
            if (_parent == null || _productie == null)
            {
                xomschrijving.Text = "";
                xok.Enabled = false;
            }
            else
            {
                xok.Enabled = true;
                xomschrijving.Text = $"<span color='darkred'><b>Combineren met HoofdProductie:<br>" +
                                     $"[{_parent.ProductieNr}] {_parent.Naam} van {_parent.Omschrijving}</b></span><br><br>" +
                                     $"<b>Vul in het percentage dat de combi zou moeten mee tellen.<br>" +
                                     $"Bijv: 50% houdt in dat zowel de hoofdproductie als de combi bijde de helft zullen produceren.<br><br>" +
                                     $"<span color='darkred'>Huidig Gekozen: {xhoofdvalue.Value} / {Beschikbaar()}%</span></b>";
            }
        }

        private double Beschikbaar()
        {
            return 99 - _parent.Combies.Sum(x => x.Activiteit);
        }

        public decimal GCD(decimal a, decimal b)

        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            if (a == 0)
                return b;
            return a;
        }

        private void xhoofdvalue_ValueChanged(object sender, System.EventArgs e)
        {
            UpdateText();
        }

        private void xok_Click(object sender, System.EventArgs e)
        {
            if (_parent == null | _productie == null) return;
            if (_parent.Combies.Any(x => string.Equals(Path.Combine(x.ProductieNr, x.BewerkingNaam), _productie.Path,
                    StringComparison.CurrentCultureIgnoreCase)))
            {
                XMessageBox.Show(this, $"Kan productie niet combineren omdat het als is gecombineerd...","Bestaat Al", MessageBoxIcon.Exclamation);
                return;
            }

            var gcd = xhoofdvalue.Value;
            var xtotal = (decimal)_parent.Combies.Where(x=> x.IsRunning).Sum(x => x.Activiteit);
            if ((xtotal + gcd) >= 100)
            {
                var extra = (xtotal + gcd + 1) - 100;
                XMessageBox.Show(this, $"Er is geen ruimte meer om deze productie te combineren met {gcd}% aan activiteit...\n" +
                                 $"Je hebt {extra}% meer ingevuld dan wat er gecombineerd kan worden.", "Geen Ruimte", MessageBoxIcon.Exclamation);
                return;
            }

            gcd = 100 - gcd;
            xtotal = (decimal)_productie.Combies.Where(x=> x.IsRunning).Sum(x => x.Activiteit);
            if ((xtotal + gcd) >= 100)
            {
                var extra = (xtotal + gcd + 1) - 100;
                XMessageBox.Show(this, $"De gekozen combi heeft al een combinaties van {xtotal}%!\n" +
                                 $"Je hebt {extra}% meer ingevuld dan wat er gecombineerd kan worden.\n\n" +
                                 $"Als je een productie combineert, dan dient de combinatie de resterede activiteit te kunnen opvangen.", "Geen Ruimte", MessageBoxIcon.Exclamation);
                return;
            }
            SelectedEntry = new CombineerEntry
            {
                BewerkingNaam = _productie.Naam,
                ProductieNr = _productie.ProductieNr,
                Activiteit = (double)xhoofdvalue.Value
            };
            SelectedEntry.Periode.Start = DateTime.Now;
            DialogResult = DialogResult.OK;
        }

        private void xanuleren_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
