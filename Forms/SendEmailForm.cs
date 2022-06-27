using ProductieManager.Rpm.Mailing;
using Rpm.Mailing;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ProductieManager.Properties;

namespace Forms
{
    public partial class SendEmailForm : Forms.MetroBase.MetroBaseForm
    {
        public SendEmailForm()
        {
            InitializeComponent();
            InitOntvangers();
        }

        private void InitOntvangers()
        {
            var items = xontvangermenuitem.DropDownItems.Cast<ToolStripMenuItem>().ToList();
            foreach (var item in items)
            {
                if (item.Tag is EmailClient)
                {
                    xontvangermenuitem.DropDownItems.Remove(item);
                }
            }

            if (Manager.Opties?.EmailClients != null)
            {
                var xclients = Manager.Opties.EmailClients.OrderBy(x => x.Name);
                foreach (var client in xclients)
                {
                    var item = new ToolStripMenuItem(client.Name, Resources.user_customer_person_32x32);
                    item.Tag = client;
                    item.ToolTipText = client.Email;
                    item.Click += EmailClient_Click;
                    xontvangermenuitem.DropDownItems.Add(item);
                }
            }
        }

        private void EmailClient_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem {Tag: EmailClient client})
            {
                var items = xontvangerstrip.Items.Cast<ToolStripMenuItem>().ToList();
                if (items.Any(x => x.Tag is EmailClient xclient && xclient.Equals(client))) return;
                var xitem = new ToolStripMenuItem(client.Name,
                    Resources.delete_delete_deleteusers_delete_male_user_maleclient_2348);
                xitem.Tag = client;
                xitem.ToolTipText = client.Email;
                xitem.Click += EmailClientRemove_Click;
                xontvangerstrip.Items.Add(xitem);
            }
        }

        private void EmailClientRemove_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem client)
            {
                xontvangerstrip.Items.Remove(client);
            }
        }

        private void xaddbijlage_Click(object sender, EventArgs e)
        {
            var xbuttons = new Dictionary<string, DialogResult>();
            xbuttons.Add("Annuleren", DialogResult.Cancel);
            xbuttons.Add("Bestand", DialogResult.OK);
            xbuttons.Add("Screen Capture", DialogResult.Yes);
            var result = XMessageBox.Show(this, $"Wat voor bijlage wil je gebruiken?\n\n" +
                                          "Kies voor 'Bestand' als je een bestand wilt kiezen.\n" +
                                          "Kies voor 'Screen Capture' als je een scherm afbeelding wilt maken.",
                "Bijlage", MessageBoxButtons.OK, MessageBoxIcon.Question, null, xbuttons);
            if (result == DialogResult.Cancel) return;
            string bijlage = null;
            switch (result)
            {
                case DialogResult.OK:
                    var ofd = new OpenFileDialog();
                    ofd.Title = "Kies Bijlage";
                    if (ofd.ShowDialog() == DialogResult.OK)
                        bijlage = ofd.FileName;
                    break;
                case DialogResult.Yes:
                    var xscreen = new SelectScreenImage();
                    this.Hide();
                    if (xscreen.ShowDialog() == DialogResult.OK)
                        bijlage = xscreen.SelectedImagePath;
                    this.Show(this);
                    break;
            }

            if (string.IsNullOrEmpty(bijlage)) return;
            if (xbijlagestrip.Items.Cast<ToolStripMenuItem>().Any(x =>
                string.Equals((string) x.Tag, bijlage, StringComparison.CurrentCultureIgnoreCase))) return;
            var menuitem = new ToolStripMenuItem(Path.GetFileName(bijlage), Resources.delete_1577);
            menuitem.Tag = bijlage;
            menuitem.ToolTipText = bijlage;
            menuitem.Click += Menuitem_Click;
            xbijlagestrip.Items.Add(menuitem);
        }

        private void Menuitem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                xbijlagestrip.Items.Remove(item);
            }
        }

        private void xnieuweontvanger_Click(object sender, EventArgs e)
        {
            var ontvangers = new EmailClientsForm();
            if (ontvangers.ShowDialog(this) == DialogResult.OK)
            {
                InitOntvangers();
            }
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void xverzenden_Click(object sender, EventArgs e)
        {
            List<EmailClient> clients = new List<EmailClient>();
            foreach(var item in xontvangerstrip.Items.Cast<ToolStripMenuItem>())
                if (item.Tag is EmailClient client)
                    clients.Add(client);
            List<string> attachments = new List<string>();
            foreach (var item in xbijlagestrip.Items.Cast<ToolStripMenuItem>())
                if (item.Tag is string bijlage)
                    attachments.Add(bijlage);
            if (clients.Count == 0)
            {
                XMessageBox.Show(this, $"Geen ontvangers gekozen!\n\nKies de mensen waar je de mail naar toe wilt sturen.",
                    "Geen Ontvangers", MessageBoxIcon.Exclamation);
                return;
            }

            if (xafzender.Text.Trim().Length < 4)
            {
                XMessageBox.Show(this, $"Afzender is niet ingevuld of is te kort om te verzenden!\nAfzender moet minstens 4 characters hebben.",
                    "Ongeldige Bericht", MessageBoxIcon.Exclamation);
                return;
            }

            if (xmessagebox.Text.Trim().Length < 10)
            {
                XMessageBox.Show(this, $"Bericht is te kort om te verzenden!\n\nBericht moet minstens 10 characters hebben.",
                    "Ongeldige Bericht", MessageBoxIcon.Exclamation);
                return;
            }

            RemoteProductie.SendEmails(clients, xonderwerp.Text, xafzender.Text,xmessagebox.Text, attachments, true,false);
            this.Close();
        }
    }
}
