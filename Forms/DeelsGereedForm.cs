using Forms;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class DeelsGereedForm : Forms.MetroBase.MetroBaseForm
    {
        public DeelsGereedMelding GereedMelding { get; private set; } = new DeelsGereedMelding();
        private Bewerking _werk;
        public DeelsGereedForm(Bewerking werk)
        {
            InitializeComponent();
            _werk = werk;
        }

        private void UpdateFields()
        {

            xnaam.Text = GereedMelding.Paraaf;
            xaantal.Value = GereedMelding.Aantal;
            xgereeddate.SetValue(GereedMelding.Datum);
            xgereednotitie.Text = GereedMelding.Notitie;
        }


        public DeelsGereedForm(DeelsGereedMelding gereedmelding):this(gereedmelding.Werk)
        {
            GereedMelding = gereedmelding;
            UpdateFields();
        }

        private void xokb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xnaam.Text.Trim()))
                XMessageBox.Show(this, $"Ongeldige paraaf!", "Ongeldig", MessageBoxIcon.Exclamation);
            else if (xaantal.Value <= 0)
                XMessageBox.Show(this, $"Ongeldige aantal!\nJe kunt minimaal 1 stuk gereed melden.", "Ongeldig",
                    MessageBoxIcon.Exclamation);
            else
            {
                GereedMelding.Datum = xgereeddate.Value;
                GereedMelding.Aantal = (int)xaantal.Value;
                GereedMelding.Paraaf = xnaam.Text.Trim();
                GereedMelding.Note = new NotitieEntry(xgereednotitie.Text.Trim(), _werk)
                    { Type = NotitieType.DeelsGereed, Naam = xnaam.Text.Trim() };
                DialogResult = DialogResult.OK;
            }
        }

        private void xcancelb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
