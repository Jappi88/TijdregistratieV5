using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms
{
    public partial class AddPersoneel : MetroFramework.Forms.MetroForm
    {
        public bool IsEditMode { get; private set; }

        public AddPersoneel()
        {
            InitializeComponent();
        }

        public AddPersoneel(Personeel pers) : this()
        {
            IsEditMode = pers != null;
            PersoneelLid = pers?.CreateCopy();
            xbuttonpanel.Visible = IsEditMode;
            if (pers != null)
            {
                this.Text = $"Beheer {pers.PersoneelNaam}";
                xnaam.Text = pers.PersoneelNaam;
                xafdeling.Text = pers.Afdeling;
                xisuitzendcheck.Checked = pers.IsUitzendKracht;
                this.Invalidate();
            }
        }

        public Personeel PersoneelLid { get; private set; }

        public new DialogResult ShowDialog()
        {
            PersoneelLid ??= new Personeel();
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
                if(!IsEditMode && await Manager.Database.PersoneelExist(xnaam.Text.Trim()))
                {
                    XMessageBox.Show($"'{xnaam.Text.Trim()}' is al toegevoegd.", "Bestaat al", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else
                {
                    UpdateUser();
                    DialogResult = DialogResult.OK;
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
                try
                {
                    PersoneelLid.PersoneelNaam = xnaam.Text.Trim();
                    var rs = new RoosterForm(PersoneelLid.WerkRooster ?? Manager.Opties.GetWerkRooster(),
                        $"Rooster voor {PersoneelLid.PersoneelNaam}");
                    if (rs.ShowDialog() == DialogResult.OK) PersoneelLid.WerkRooster = rs.WerkRooster;
                    var xr = PersoneelLid.WerkRooster.IsCustom()? "" : " [EIGEN]";
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

        private void xklusjesb_Click(object sender, EventArgs e)
        {
            if (PersoneelLid != null)
            {
                var ak = new AlleKlusjes(PersoneelLid);
                ak.ShowDialog();
            }
        }

        private void xvaardigeheden_Click(object sender, EventArgs e)
        {
            if (PersoneelLid != null)
            {
                var vf = new VaardighedenForm(PersoneelLid);
                vf.ShowDialog();
            }
        }
    }
}