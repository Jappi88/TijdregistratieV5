using System;
using System.Windows.Forms;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;

namespace Forms
{
    public partial class CreateAccount : MetroFramework.Forms.MetroForm
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        public UserAccount Account { get; private set; }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(xgebruikersname.Text) ||
                xgebruikersname.Text == "Vul in een gebruikersnaam..." || xgebruikersname.Text.Trim().Length == 0)
            {
                XMessageBox.Show("Vul in een geldige gebruikersnaam", "Ongeldige GebruikersNaam", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else if (await Manager.Database.AccountExist(xgebruikersname.Text))
            {
                XMessageBox.Show($"{xgebruikersname.Text} bestaat al...\nKies andere gebruiksernaam a.u.b",
                    "Bestaat Al",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (xwachtwoord1.Text.Length < 6)
            {
                XMessageBox.Show("Wachtwoord is te kort!\nJe wachtwoord moet minimaal 6 characters lang zijn.",
                    "Ongeldige Wachtwoord", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (xwachtwoord1.Text != xwachtwoord2.Text)
            {
                XMessageBox.Show(
                    "De twee wachtwoorden komen niet overeen\nControlleer je wachtwoord en probeer het opnieuw.",
                    "Ongeldige Wachtwoord", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (xtoegangslevel.SelectedIndex == -1)
            {
                XMessageBox.Show("Vul in de toegangs level en probeer het opnieuw.", "Ongeldige ToegangsLevel",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Account = new UserAccount
                {
                    AccesLevel = (AccesType) xtoegangslevel.SelectedIndex, Password = xwachtwoord2.Text,
                    Username = xgebruikersname.Text
                };
                DialogResult = DialogResult.OK;
            }
        }

        private void xgebruikersname_MouseEnter(object sender, EventArgs e)
        {
            if (xgebruikersname.Text == "Vul in een gebruikersnaam...")
                xgebruikersname.Text = "";
        }

        private void xgebruikersname_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(xgebruikersname.Text) || xgebruikersname.Text.Trim().Length == 0)
                xgebruikersname.Text = "Vul in een gebruikersnaam...";
        }
    }
}