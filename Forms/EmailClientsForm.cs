using BrightIdeasSoftware;
using ProductieManager.Rpm.Mailing;
using Rpm.Productie;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class EmailClientsForm : Forms.MetroBase.MetroBaseForm
    {
        public EmailClientsForm()
        {
            InitializeComponent();
            if (Manager.Opties == null)
                throw new Exception("Instellingen zijn niet geladen!\nRaadpleeg Ihab voor meer info.");
            ((OLVColumn) xontvangers.Columns[0]).ImageGetter = _ => 0;
            xontvangers.SetObjects(Manager.Opties.EmailClients);
        }

        private void xadduser_Click(object sender, EventArgs e)
        {
            var newuser = new NewEmailClientForm();
            if (newuser.ShowDialog() == DialogResult.OK)
            {
                var client = newuser.SelectedEmailClient;
                xontvangers.AddObject(client);
                xontvangers.SelectedObject = client;
                xontvangers.SelectedItem?.EnsureVisible();
            }
        }

        private void xdeleteuser_Click(object sender, EventArgs e)
        {
            foreach (var item in xontvangers.SelectedObjects.Cast<EmailClient>())
            {
                xontvangers.RemoveObject(item);
            }
        }

        private void xOpslaan_Click(object sender, EventArgs e)
        {
            Manager.Opties.EmailClients = xontvangers.Objects.Cast<EmailClient>().ToList();
            Manager.Opties.Save();
            DialogResult = DialogResult.OK;
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xedituser_Click(object sender, EventArgs e)
        {
            if (xontvangers.SelectedObject is EmailClient client)
            {
                var clientform = new NewEmailClientForm(client);
                if (clientform.ShowDialog() == DialogResult.OK)
                {
                    xontvangers.RemoveObject(client);
                    xontvangers.AddObject(clientform.SelectedEmailClient);
                    xontvangers.SelectedObject = client;
                    xontvangers.SelectedItem?.EnsureVisible();
                }
            }
        }

        private void xontvangers_SelectedIndexChanged(object sender, EventArgs e)
        {
            xedituser.Enabled = xontvangers.SelectedObjects.Count == 1;
            xdeleteuser.Enabled = xontvangers.SelectedObjects.Count > 0;
        }
    }
}
