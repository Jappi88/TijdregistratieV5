using System;
using System.Linq;
using System.Windows.Forms;
using Forms;
using ProductieManager.Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;

namespace ProductieManager.Forms
{
    public partial class NewEmailClientForm : MetroFramework.Forms.MetroForm
    {
        public NewEmailClientForm()
        {
            InitializeComponent();
        }

        public NewEmailClientForm(EmailClient client):this()
        {
            xname.Text = client.Name;
            xemail.Text = client.Email;
        }

        public EmailClient SelectedEmailClient => new EmailClient()
            {Email = xemail.Text.Trim(), Name = xname.Text.Trim()};

        private void xok_Click(object sender, EventArgs e)
        {
            if (!xemail.Text.Trim().EmailIsValid())
                XMessageBox.Show("Ongeldige email!\n\nVul in een geldige email en probeer het opnieuw.",
                    "Ongeldig Email", MessageBoxIcon.Exclamation);
            else if (xname.Text.Trim().Length < 4)
                XMessageBox.Show("Ongeldige naam!\n\nVul in een geldige naam en probeer het opnieuw.",
                    "Ongeldig Email", MessageBoxIcon.Exclamation);
            else
            {
                var clients = Manager.Opties.EmailClients;
                if (clients?.Count > 0)
                {
                    if (clients.Any(x => string.Equals(x.Name, xname.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                    {
                        XMessageBox.Show($"Gebruiker naam '{xname.Text.Trim()}' is al toegevoegd!", "Bestaat Al",
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (clients.Any(
                        x => string.Equals(x.Email, xemail.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                    {
                        XMessageBox.Show($"Gebruiker email '{xemail.Text.Trim()}' is al toegevoegd!", "Bestaat Al",
                            MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                DialogResult = DialogResult.OK;

            }
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
