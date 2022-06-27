using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class AddPersoneel : Forms.MetroBase.MetroBaseForm
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
            return base.ShowDialog(this);
        }

        private void xok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xnaam.Text) || xnaam.Text.Trim().Length < 3)
            {
                XMessageBox.Show(this, $"Ongeldige personeel naam!", "Ongeldig", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                if(!IsEditMode && Manager.Database.PersoneelExist(xnaam.Text.Trim()))
                {
                    XMessageBox.Show(this, $"'{xnaam.Text.Trim()}' is al toegevoegd.", "Bestaat al", MessageBoxButtons.OK,
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
                if (vf.ShowDialog(this) == DialogResult.OK) PersoneelLid.VrijeDagen = vf.VrijeTijd;
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
                    if (rs.ShowDialog(this) == DialogResult.OK) 
                        PersoneelLid.WerkRooster = rs.WerkRooster;
                    var xr = PersoneelLid.WerkRooster != null && PersoneelLid.WerkRooster.IsCustom()? " [Aangepast]" : "";
                    xrooster.Text = $"Werk Rooster{xr}";
                }
                catch (Exception ex)
                {
                    XMessageBox.Show(this, ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ak.ShowDialog(this);
            }
        }

        private void xvaardigeheden_Click(object sender, EventArgs e)
        {
            if (PersoneelLid != null)
            {
                var vf = new VaardighedenForm(PersoneelLid);
                vf.ShowDialog(this);
            }
        }
    }
}