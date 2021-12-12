using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ProductieManager.Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Mailing;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Various;
using Rpm.Klachten;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms.Klachten
{
    public partial class NewKlachtForm : MetroFramework.Forms.MetroForm
    {
        public KlachtEntry Klacht { get; private set; }
        public NewKlachtForm(KlachtEntry entry = null)
        {
            InitializeComponent();
            InitKlacht(entry);
        }

        public void InitKlacht(KlachtEntry klacht)
        {
            Klacht = klacht ?? new KlachtEntry();
            InitOntvangers();
            InitProducties();
            InitBijlages();
            xonderwerp.Text = klacht?.Onderwerp;
            xafzender.Text = klacht?.Melder;
            if (klacht != null)
            {
                xdateklacht.SetValue(klacht.DatumKlacht);
                xplaatsen.Text = "Wijzig Klacht";
            }
            else xplaatsen.Text = "Klacht Plaatsen";
            xmessagebox.Text = klacht?.Omschrijving;
        }

        private void InitOntvangers()
        {
            ClearOntvangers();

            if (ProductieChat.Gebruikers != null)
            {
                var xclients = ProductieChat.Gebruikers.OrderBy(x => x.UserName).Select(x=> x.UserName);
                foreach (var client in xclients)
                {
                    var item = new ToolStripMenuItem(client, Resources.user_customer_person_32x32);
                    item.Tag = client;
                    item.ToolTipText = client;
                    item.Click += EmailClient_Click;
                    xontvangermenuitem.DropDownItems.Add(item);
                }
            }

           
            if (Klacht?.Ontvangers != null && Klacht.Ontvangers.Count > 0)
            {
                foreach(var ont in Klacht.Ontvangers)
                    AddOntvangerStrip(ont);
            }
        }

        private void InitBijlages()
        {
            ClearBijlages();

            if (Klacht?.Bijlages != null && Klacht.Bijlages.Count > 0)
            {
                foreach (var ont in Klacht.Bijlages) AddBijlageStrip(ont);
            }
        }

        private void InitProducties()
        {
            ClearProducties();

            if (Klacht?.ProductieNrs != null && Klacht.ProductieNrs.Count > 0)
            {
                foreach (var ont in Klacht.ProductieNrs) AddProductieStrip(ont);
            }
        }

        private void EmailClient_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem { Tag: string client })
            {
                AddOntvangerStrip(client);
            }
        }

        private void AddOntvangerStrip(string name)
        {
            var items = xontvangerstrip.Items.Cast<ToolStripMenuItem>().ToList();
            if (items.Any(x =>
                    x.Tag is string xclient &&
                    string.Equals(xclient, name, StringComparison.CurrentCultureIgnoreCase))) return;
            var xitem = new ToolStripMenuItem(name,
                Resources.delete_delete_deleteusers_delete_male_user_maleclient_2348);
            xitem.Tag = name;
            xitem.ToolTipText = name;
            xitem.Click += ClientRemove_Click;
            xontvangerstrip.Items.Add(xitem);
        }

        private void AddProductieStrip()
        {
            var xt = new TextFieldEditor();
            xt.FieldImage = Resources.page_document_16748_128_128;
            xt.Title = "Vul in een ProductieNr";
            if (xt.ShowDialog() == DialogResult.OK)
            {
                var name = xt.SelectedText;
                var xprod = Manager.Database.GetProductie(name).Result;
                if (xprod == null)
                {
                    if (XMessageBox.Show($"ProductieNr '{name}' bestaat niet, wil je alsnog toevoegen?",
                            "ProductieNr bestaat niet", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                        DialogResult.No) return;
                }

                AddProductieStrip(name);
            }
        }

        private void AddProductieStrip(string name)
        {
            var items = xproductiesstrip.Items.Cast<ToolStripMenuItem>().ToList();
            if (items.Any(x =>
                    x.Tag is string xclient &&
                    string.Equals(xclient, name, StringComparison.CurrentCultureIgnoreCase))) return;
            var xitem = new ToolStripMenuItem(name,
                Resources.page_document_16748.CombineImage(Resources.delete_1577, 1.5));
            xitem.Tag = name;
            xitem.ToolTipText = name;
            xitem.Click += ProductieRemove_Click;
            xproductiesstrip.Items.Add(xitem);
        }

        private void AddBijlageStrip(string name)
        {
            var items = xbijlagestrip.Items.Cast<ToolStripMenuItem>().ToList();
            if (items.Any(x =>
                    x.Tag is string xclient &&
                    string.Equals(xclient, name, StringComparison.CurrentCultureIgnoreCase))) return;
            var xitem = new ToolStripMenuItem(Path.GetFileName(name),
                Resources.delete_1577);
            xitem.Tag = name;
            xitem.ToolTipText = name;
            xitem.Click += BijlageRemove_Click;
            xbijlagestrip.Items.Add(xitem);
        }

        private void ClearOntvangers()
        {
            if (xontvangerstrip.Items.Count > 0)
            {
                var xitems = xontvangerstrip.Items.Cast<ToolStripMenuItem>().Where(x => x.Tag is string).ToList();
                xitems.ForEach(x=> xontvangerstrip.Items.Remove(x));
            }
        }

        private void ClearBijlages()
        {
            if (xbijlagestrip.Items.Count > 0)
            {
                var xitems = xbijlagestrip.Items.Cast<ToolStripMenuItem>().Where(x => x.Tag is string).ToList();
                xitems.ForEach(x => xbijlagestrip.Items.Remove(x));
            }
        }

        private void ClearProducties()
        {
            if (xproductiesstrip.Items.Count > 0)
            {
                var xitems = xproductiesstrip.Items.Cast<ToolStripMenuItem>().Where(x => x.Tag is string).ToList();
                xitems.ForEach(x => xproductiesstrip.Items.Remove(x));
            }
        }

        public List<string> GetOntvangers()
        {
            var xreturn = new List<string>();
            try
            {
                xreturn = xontvangerstrip.Items.Cast<ToolStripMenuItem>().Where(x => x.Tag is string).Select(x=> x.Tag as string).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }

            return xreturn;
        }

        public List<string> GetBijlages()
        {
            var xreturn = new List<string>();
            try
            {
                xreturn = xbijlagestrip.Items.Cast<ToolStripMenuItem>().Where(x => x.Tag is string).Select(x => x.Tag as string).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return xreturn;
        }

        public List<string> GetProducties()
        {
            var xreturn = new List<string>();
            try
            {
                xreturn = xproductiesstrip.Items.Cast<ToolStripMenuItem>().Where(x => x.Tag is string).Select(x => x.Tag as string).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return xreturn;
        }

        private void ClientRemove_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem client)
            {
                xontvangerstrip.Items.Remove(client);
            }
        }

        private void ProductieRemove_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem client)
            {
                xproductiesstrip.Items.Remove(client);
            }
        }

        private void xaddbijlage_Click(object sender, EventArgs e)
        {
            var xbuttons = new Dictionary<string, DialogResult>();
            xbuttons.Add("Annuleren", DialogResult.Cancel);
            xbuttons.Add("Bestand", DialogResult.OK);
            xbuttons.Add("Screen Capture", DialogResult.Yes);
            var result = XMessageBox.Show("Wat voor bijlage wil je gebruiken?\n\n" +
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
                    xscreen.StartPosition = FormStartPosition.CenterParent;
                    xscreen.Location = this.Location;
                    var forms = Application.OpenForms.Cast<Form>().ToList();
                    if (forms.Count > 0)
                        foreach(var form in forms)
                            form.Hide();
                    //this.Hide();
                    xscreen.Closed += (x, y) =>
                    {
                        forms = Application.OpenForms.Cast<Form>().ToList();
                        if (forms.Count > 0)
                            foreach (var form in forms)
                                form.Show();
                        this.BringToFront();
                        this.Select();
                        this.Focus();
                    };
                    if (xscreen.ShowDialog() == DialogResult.OK)
                        bijlage = xscreen.SelectedImagePath;
                    break;
            }

            if (string.IsNullOrEmpty(bijlage)) return;
            AddBijlageStrip(bijlage);
        }

        private void BijlageRemove_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                xbijlagestrip.Items.Remove(item);
            }
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xnieuweontvanger_Click(object sender, EventArgs e)
        {
            AddOntvangerStrip("iedereen");
        }

        private void xaddproductie_Click(object sender, EventArgs e)
        {
            AddProductieStrip();
        }

        private bool ValidateInput()
        {
            try
            {
                var xontv = GetOntvangers();
                if (xontv.Count == 0)
                    throw new Exception("Geen ontvangers gekozen!");
                if (string.IsNullOrEmpty(xonderwerp.Text.Trim()))
                    throw new Exception("Vul in een onderwerp a.u.b.");
                if (string.IsNullOrEmpty(xafzender.Text.Trim()))
                    throw new Exception("Vul in van wie de klacht is a.u.b.");
                if (string.IsNullOrEmpty(xmessagebox.Text.Trim()))
                    throw new Exception("Vul in een klacht omschrijving a.u.b.");
                return true;
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Aandacht Vereist!", MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void xplaatsen_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Klacht.Ontvangers = GetOntvangers();
                Klacht.Bijlages = GetBijlages();
                Klacht.ProductieNrs = GetProducties();
                Klacht.Onderwerp = xonderwerp.Text.Trim();
                Klacht.Omschrijving = xmessagebox.Text.Trim();
                Klacht.Melder = xafzender.Text.Trim();
                Klacht.DatumKlacht = xdateklacht.Value;
                Klacht.IsGelezen = false;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
