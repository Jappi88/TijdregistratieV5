using System;
using System.Windows.Forms;
using Rpm.Productie;

namespace Forms.Sporen
{
    public partial class NewSpoorForm : Forms.MetroBase.MetroBaseForm
    {
        public NewSpoorForm()
        {
            InitializeComponent();
        }

        public SpoorEntry SelectedSpoor { get; private set; }

        private void button2_Click(object sender, System.EventArgs e)
        {
            if (xnaam.Text.Length < 3)
                XMessageBox.Show(this, $"Vul in jou naam a.u.b.", "Ongeldige Naam",
                    MessageBoxIcon.Exclamation);
            else if (xartikelnr.Text.Length < 5)
                XMessageBox.Show(this, $"Vul in een geldige ArtikelNr a.u.b.", "Ongeldige ArtikelNr",
                    MessageBoxIcon.Exclamation);
            else if (xomschrijving.Text.Length < 5)
                XMessageBox.Show(this, $"Vul in een geldige Omschrijving a.u.b.", "Ongeldige Omschrijving",
                    MessageBoxIcon.Exclamation);
            else
            {
                SelectedSpoor ??= productieVerbruikUI1.GetSpoorInfo();
                SelectedSpoor.ProductOmschrijving = xomschrijving.Text.Trim();
                SelectedSpoor.ArtikelNr = xartikelnr.Text.Trim();
                SelectedSpoor.AangepastDoor = xnaam.Text.Trim();
                SelectedSpoor.AangepastOp = DateTime.Now;
                DialogResult = DialogResult.OK;
            }
        }

        public void SetSpoorInfo(SpoorEntry spoor)
        {
            xnaam.Text = spoor?.AangepastDoor;
            xartikelnr.Text = spoor?.ArtikelNr;
            xomschrijving.Text = spoor?.ProductOmschrijving;
            if (spoor != null)
            {
                productieVerbruikUI1.UpdateFields(true, spoor);
                productieVerbruikUI1.Title = $"[{spoor.ArtikelNr}] {spoor.ProductOmschrijving}";
            }
        }
    }
}
