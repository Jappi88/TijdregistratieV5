using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Controls;
using Forms.MetroBase;
using MetroFramework;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Various;
using Rpm.Klachten;
using Rpm.Misc;
using Rpm.Opmerking;
using Rpm.Productie;

namespace Forms.Klachten
{
    public partial class NewKlachtForm : MetroBaseForm
    {
        public NewKlachtForm(bool isopmerking, object value = null)
        {
            InitializeComponent();
            if (value is KlachtEntry klacht)
            {
                InitKlacht(klacht);
            }
            else if (value is OpmerkingEntry opmerking)
            {
                InitOpmerking(opmerking);
            }
            else
            {
                if (isopmerking)
                    InitOpmerking(null);
                else InitKlacht(null);
            }
        }

        public KlachtEntry Klacht { get; private set; }
        public OpmerkingEntry Opmerking { get; set; }

        private void Xmessagebox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData("Producties") is ArrayList collection)
            {
                var data = collection.Cast<IProductieBase>().ToList();
                foreach (var xd in data)
                    AddProductieStrip(xd.ProductieNr);
            }
        }

        private void Xmessagebox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData("Producties") is ArrayList)
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        public void InitKlacht(KlachtEntry klacht)
        {
            Klacht = klacht ?? new KlachtEntry();
            xDatePanel.Visible = true;
            ximagebox.Image = Resources.file_warning_40447;
            xplaatsen.Image = Resources.Leave_80_icon_icons_com_57305;
            Style = MetroColorStyle.Red;
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
            else
            {
                xplaatsen.Text = "Klacht Plaatsen";
            }

            Text = xplaatsen.Text;
            Invalidate();
            xmessagebox.Text = klacht?.Omschrijving;
        }

        public void InitOpmerking(OpmerkingEntry opmerking)
        {
            Opmerking = opmerking ?? new OpmerkingEntry();
            xDatePanel.Visible = false;
            ximagebox.Image = Resources.note_document_64x64;
            xplaatsen.Image = Resources.noterespond_general_32x32;
            Style = MetroColorStyle.Teal;
            InitOntvangers();
            InitProducties();
            InitBijlages();
            xonderwerp.Text = Opmerking?.Title;
            xafzender.Text = Opmerking?.Afzender;
            if (opmerking != null)
                xplaatsen.Text = "Wijzig Opmerking";
            else xplaatsen.Text = "Opmerking Plaatsen";
            Text = xplaatsen.Text;
            Invalidate();
            xmessagebox.Text = Opmerking?.Opmerking;
        }

        private void InitOntvangers()
        {
            ClearOntvangers();

            if (ProductieChat.Gebruikers != null)
            {
                var xclients = ProductieChat.Gebruikers.OrderBy(x => x.UserName).Select(x => x.UserName);
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
                foreach (var ont in Klacht.Ontvangers)
                    AddOntvangerStrip(ont);
                return;
            }

            if (Opmerking?.Ontvangers != null && Opmerking.Ontvangers.Count > 0)
                foreach (var ont in Opmerking.Ontvangers)
                    AddOntvangerStrip(ont);
        }

        private void InitBijlages()
        {
            ClearBijlages();

            if (Klacht?.Bijlages != null && Klacht.Bijlages.Count > 0)
            {
                foreach (var ont in Klacht.Bijlages) AddBijlageStrip(ont);
                return;
            }

            if (Opmerking?.Bijlages != null && Opmerking.Bijlages.Count > 0)
                foreach (var ont in Opmerking.Bijlages)
                    AddBijlageStrip(ont.Key);
        }

        private void InitProducties()
        {
            ClearProducties();

            if (Klacht?.ProductieNrs != null && Klacht.ProductieNrs.Count > 0)
                foreach (var ont in Klacht.ProductieNrs)
                    AddProductieStrip(ont);
        }

        private void EmailClient_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem {Tag: string client}) AddOntvangerStrip(client);
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
            var tb = new TextFieldEditor();
            tb.FieldImage = Resources.page_document_16748_128_128;
            tb.Title = "Vul in een ProductieNr";
            tb.EnableSecondaryField = true;
            tb.SecondaryCheckBoxText = "Producties Zoeken";
            tb.SecondaryDescription = "Vul in een Artikelnr, bewerking naam of een stukje omschrijving";
            if (tb.ShowDialog() == DialogResult.OK)
            {
                if (tb.UseSecondary)
                {
                    var calcform = new RangeCalculatorForm();
                    var rf = new ZoekProductiesUI.RangeFilter
                    {
                        Enabled = true,
                        Criteria = tb.SecondaryText.Trim()
                    };
                    calcform.Filter = rf;
                    calcform.Show();
                }
                else
                {
                    var name = tb.SelectedText;
                    var xprod = Manager.Database.GetProductie(name);
                    if (xprod == null)
                        if (XMessageBox.Show(this, $"ProductieNr '{name}' bestaat niet, wil je alsnog toevoegen?",
                                "ProductieNr bestaat niet", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                            DialogResult.No)
                            return;

                    AddProductieStrip(name);
                }
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
                xitems.ForEach(x => xontvangerstrip.Items.Remove(x));
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
                xreturn = xontvangerstrip.Items.Cast<ToolStripMenuItem>().Where(x => x.Tag is string)
                    .Select(x => x.Tag as string).ToList();
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
                xreturn = xbijlagestrip.Items.Cast<ToolStripMenuItem>().Where(x => x.Tag is string)
                    .Select(x => x.Tag as string).ToList();
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
                xreturn = xproductiesstrip.Items.Cast<ToolStripMenuItem>().Where(x => x.Tag is string)
                    .Select(x => x.Tag as string).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        private void ClientRemove_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem client) xontvangerstrip.Items.Remove(client);
        }

        private void ProductieRemove_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem client) xproductiesstrip.Items.Remove(client);
        }

        private void xaddbijlage_Click(object sender, EventArgs e)
        {
            var xbuttons = new Dictionary<string, DialogResult>();
            xbuttons.Add("Annuleren", DialogResult.Cancel);
            xbuttons.Add("Bestand", DialogResult.OK);
            xbuttons.Add("Screen Capture", DialogResult.Yes);
            var result = XMessageBox.Show(this, "Wat voor bijlage wil je gebruiken?\n\n" +
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
                    xscreen.Location = Location;
                    var forms = Application.OpenForms.Cast<Form>().ToList();
                    if (forms.Count > 0)
                        foreach (var form in forms)
                            form.Hide();
                    //this.Hide();
                    xscreen.Closed += (x, y) =>
                    {
                        forms = Application.OpenForms.Cast<Form>().ToList();
                        if (forms.Count > 0)
                            foreach (var form in forms)
                                form.Show();
                        BringToFront();
                        Select();
                        Focus();
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
            if (sender is ToolStripMenuItem item) xbijlagestrip.Items.Remove(item);
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
                XMessageBox.Show(this, e.Message, "Aandacht Vereist!", MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void xplaatsen_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                var ontvangers = GetOntvangers();
                var bijlages = GetBijlages();
                var prods = GetProducties();
                if (Klacht != null)
                {
                    Klacht.Ontvangers = ontvangers;
                    Klacht.Bijlages = bijlages;
                    Klacht.ProductieNrs = prods;
                    Klacht.Onderwerp = xonderwerp.Text.Trim();
                    Klacht.Omschrijving = xmessagebox.Text.Trim();
                    Klacht.Melder = xafzender.Text.Trim();
                    Klacht.DatumKlacht = xdateklacht.Value;
                    Klacht.IsGelezen = false;
                    DialogResult = DialogResult.OK;
                }
                else if (Opmerking != null)
                {
                    Opmerking.Ontvangers = ontvangers;
                    var xbl = new Dictionary<string, byte[]>();
                    foreach (var xb in bijlages)
                    {
                        var xname = Path.GetFileName(xb);
                        byte[] data = null;
                        if (Opmerking.Bijlages.ContainsKey(xname))
                            data = Opmerking.Bijlages[xname];
                        if (data == null && File.Exists(xb)) data = File.ReadAllBytes(xb);
                        if (data != null && !xbl.ContainsKey(xname))
                            xbl.Add(xname, data);
                    }

                    Opmerking.Bijlages = xbl;
                    Opmerking.Producties = prods;
                    Opmerking.Title = xonderwerp.Text.Trim();
                    Opmerking.Opmerking = xmessagebox.Text.Trim();
                    Opmerking.Afzender = xafzender.Text.Trim();
                    Opmerking.SetIsGelezen(false);
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}