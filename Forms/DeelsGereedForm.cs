using System;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Forms
{
    public partial class DeelsGereedForm : MetroBaseForm
    {
        private readonly Bewerking _werk;

        public DeelsGereedForm(Bewerking werk)
        {
            InitializeComponent();
            _werk = werk;
        }


        public DeelsGereedForm(DeelsGereedMelding gereedmelding) : this(gereedmelding.Werk)
        {
            GereedMelding = gereedmelding;
            UpdateFields();
        }

        public DeelsGereedMelding GereedMelding { get; } = new();

        private void UpdateFields()
        {
            xnaam.Text = GereedMelding.Paraaf;
            xaantal.Value = GereedMelding.Aantal;
            xgereeddate.SetValue(GereedMelding.Datum);
            xgereednotitie.Text = GereedMelding.Notitie;
        }

        private void xokb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xnaam.Text.Trim()))
            {
                XMessageBox.Show(this, "Ongeldige paraaf!", "Ongeldig", MessageBoxIcon.Exclamation);
            }
            else if (xaantal.Value <= 0)
            {
                XMessageBox.Show(this, "Ongeldige aantal!\nJe kunt minimaal 1 stuk gereed melden.", "Ongeldig",
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                GereedMelding.Datum = xgereeddate.Value;
                GereedMelding.Aantal = (int) xaantal.Value;
                GereedMelding.Paraaf = xnaam.Text.Trim();
                GereedMelding.Note = new NotitieEntry(xgereednotitie.Text.Trim(), _werk)
                    {Type = NotitieType.DeelsGereed, Naam = xnaam.Text.Trim()};
                DialogResult = DialogResult.OK;
            }
        }

        private void xcancelb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}