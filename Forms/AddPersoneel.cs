using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Rpm.Productie;

namespace Forms
{
    public partial class AddPersoneel : MetroFramework.Forms.MetroForm
    {
        public AddPersoneel()
        {
            InitializeComponent();
        }

        public Personeel PersoneelLid { get; private set; }

        public new DialogResult ShowDialog()
        {
            PersoneelLid = new Personeel();
            return base.ShowDialog();
        }

        private async void xok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xnaam.Text) || xnaam.Text.Trim().Length < 3)
            {
                XMessageBox.Show("Ongeldige personeel naam!", "Ongeldig", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                var exist = await Manager.Database.PersoneelExist(xnaam.Text.Trim());
                if (!exist)
                {
                    UpdateUser();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    XMessageBox.Show($"'{xnaam.Text.Trim()}' is al toegevoegd.", "Bestaat al", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
        }

        private void UpdateUser()
        {
            if (PersoneelLid != null)
            {
                PersoneelLid.PersoneelNaam = xnaam.Text.Trim();
                PersoneelLid.Afdeling = xafdeling.Text.Trim();
                PersoneelLid.IsUitzendKracht = xisuitzendcheck.Checked;
            }
        }

        private void xannueer_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xkiesvrijetijd_Click(object sender, EventArgs e)
        {
            if (PersoneelLid != null)
            {
                UpdateUser();
                var vf = new VrijeTijdForm(PersoneelLid);
                if (vf.ShowDialog() == DialogResult.OK) PersoneelLid.VrijeDagen = vf.VrijeTijd;
            }
        }

        private void xnaam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter) xok_Click(sender, EventArgs.Empty);
        }

        private void EditSelectedRooster()
        {
            if (PersoneelLid != null)
            {
                var bttns = new Dictionary<string, DialogResult>();
                bttns.Add("Annuleren", DialogResult.Cancel);
                bttns.Add("Standaard", DialogResult.No);
                bttns.Add("Eigen Rooster", DialogResult.Yes);

                try
                {
                    PersoneelLid.PersoneelNaam = xnaam.Text.Trim();
                    var dialog = XMessageBox.Show(
                        $"Wat voor rooster zou je willen gebruiken voor '{PersoneelLid.PersoneelNaam}'?\n" +
                        $"Kies voor '{PersoneelLid.PersoneelNaam}' een standaard of eigen rooster.\n", "Rooster",
                        MessageBoxButtons.OK, MessageBoxIcon.Question, null, bttns);
                    if (dialog == DialogResult.Cancel) return;

                    switch (dialog)
                    {
                        case DialogResult.No:
                            PersoneelLid.WerkRooster = null;
                            break;
                        case DialogResult.Yes:
                            var rs = new RoosterForm(PersoneelLid.WerkRooster ?? Manager.Opties.GetWerkRooster(),
                                $"Rooster voor {PersoneelLid.PersoneelNaam}");
                            if (rs.ShowDialog() == DialogResult.OK) PersoneelLid.WerkRooster = rs.WerkRooster;
                            break;
                    }

                    var xr = PersoneelLid.WerkRooster == null ? "" : " [EIGEN]";
                    xrooster.Text = $"Werk Rooster{xr}";
                }
                catch (Exception ex)
                {
                    XMessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void xrooster_Click(object sender, EventArgs e)
        {
            EditSelectedRooster();
        }
    }
}