using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using MetroFramework;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Productie.ArtikelRecords;

namespace Forms.ArtikelRecords
{
    public partial class ArtikelOpmerkingForm : MetroFramework.Forms.MetroForm
    {
        public ArtikelOpmerking SelectedOpmerking { get; set; }

        public ArtikelOpmerkingForm()
        {
            InitializeComponent();
            xGelezenDoorImageList.Images.Add(Resources.user_customer_person_32x32);
            ((OLVColumn) xGelezenDoorList.Columns[0]).ImageGetter = (x) => 0;
        }

        public ArtikelOpmerkingForm(ArtikelOpmerking opmerking):this()
        {
            if (opmerking == null) return;
            Title = $"Wijzig Opmerking van '{opmerking.GeplaatstDoor}'";
            xok.Text = "Opslaan";
            xok.Image = Resources.diskette_save_saveas_1514;
            xok.Invalidate();
            SelectedOpmerking = opmerking;
        }

        public string Title
        {
            get => this.Text;
            set
            {
                this.Text = value;
                this.Invalidate();
            }
        }

        public void InitCombos()
        {
            try
            {
                var xvalues = Enum.GetNames(typeof(ArtikelFilter)).Select(x=> (object)x).ToArray();
                xFilterCombo.Items.Clear();
                xFilterCombo.Items.AddRange(xvalues);
                xvalues = Enum.GetNames(typeof(ArtikelFilterSoort)).Select(x => (object)x).ToArray();
                xFilterTypeCombo.Items.Clear();
                xFilterTypeCombo.Items.AddRange(xvalues);
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void EmailClient_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem { Tag: string client })
            {
                AddOntvangerStrip(client);
            }
        }

        private void ClearOntvangers()
        {
            if (xontvangerstrip.Items.Count > 0)
            {
                var xitems = xontvangerstrip.Items.Cast<ToolStripMenuItem>().Where(x =>
                        x.Tag is string xval &&
                        !string.Equals(xval, "iedereen", StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
                xitems.ForEach(x => xontvangerstrip.Items.Remove(x));
            }
        }

        private void InitOntvangers(ArtikelOpmerking opmerking)
        {
            ClearOntvangers();
            var xusers = Manager.Database?.GetAllSettings().Result;
            
            if (xusers is {Count: > 0})
            {
                var xclients = xusers.Select(x => x.Username).ToList();
                xclients = xclients.OrderBy(x => x).ToList();
                foreach (var client in xclients)
                {
                    var item = new ToolStripMenuItem(client, Resources.user_customer_person_32x32);
                    item.Tag = client;
                    item.ToolTipText = client;
                    item.Click += EmailClient_Click;
                    xontvangermenuitem.DropDownItems.Add(item);
                }
            }


            if (opmerking?.OpmerkingVoor != null && opmerking.OpmerkingVoor.Count > 0)
            {
                var xclients = opmerking.OpmerkingVoor.OrderBy(x => x);
                foreach (var ont in xclients)
                    AddOntvangerStrip(ont);
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

        private void ClientRemove_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem client)
            {
                xontvangerstrip.Items.Remove(client);
            }
        }

        public void LoadValues(ArtikelOpmerking opmerking)
        {
            try
            {
                InitCombos();
                opmerking ??= new ArtikelOpmerking();
                InitOntvangers(opmerking);
                xGelezenDoorPanel.Visible = opmerking.IsFromMe && opmerking.GelezenDoor.Count > 0;
                var xvalue = Enum.GetName(typeof(ArtikelFilter), opmerking.Filter);
                xFilterCombo.SelectedItem = xvalue;
                xvalue = Enum.GetName(typeof(ArtikelFilterSoort), opmerking.FilterSoort);
                xFilterTypeCombo.SelectedItem = xvalue;
                xFilterWaarde.SetValue(opmerking.FilterWaarde);
                xtitletextbox.Text = opmerking.Title;
                xOpmerking.Text = opmerking.Opmerking;
                xImage.Image = opmerking.ImageData?.ImageFromBytes();
                xGelezenDoorList.SetObjects(opmerking.GelezenDoor);
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private bool SaveValues()
        {
            try
            {
                SelectedOpmerking ??= new ArtikelOpmerking();
                if (xFilterCombo.SelectedIndex > -1)
                    SelectedOpmerking.Filter = (ArtikelFilter) xFilterCombo.SelectedIndex;
                else throw new Exception("Kies een FilterType a.u.b.");
                if (xFilterTypeCombo.SelectedIndex > -1)
                    SelectedOpmerking.FilterSoort = (ArtikelFilterSoort)xFilterTypeCombo.SelectedIndex;
                else throw new Exception("Kies een FilterSoort a.u.b.");
                var ontvangers = GetOntvangers();
                if(ontvangers.Count == 0)
                    throw new Exception("Kies onvanger(s) a.u.b.");
                if (xFilterWaarde.Value == 0)
                    throw new Exception("Vul in een waarde dat niet gelijk is aan '0' a.u.b.");
                if (xtitletextbox.Text.Trim().Length < 6)
                    throw new Exception("Vul in een geldige Title a.u.b.");
                if (xOpmerking.Text.Trim().Length < 6)
                    throw new Exception("Vul in een geldige Opmerking a.u.b.");
                SelectedOpmerking.FilterWaarde = xFilterWaarde.Value;
                SelectedOpmerking.Opmerking = xOpmerking.Text.Trim();
                SelectedOpmerking.Title = xtitletextbox.Text.Trim();
                SelectedOpmerking.ImageData = xImage.Image?.ToByteArray();
                SelectedOpmerking.GelezenDoor = xGelezenDoorList.SelectedObjects.Cast<KeyValuePair<string, DateTime>>()
                    .ToDictionary(x => x.Key, x=> x.Value);
                SelectedOpmerking.OpmerkingVoor = ontvangers;
                return true;
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                return false;
            }
        }

        public ArtikelOpmerking GetOpmerking(ArtikelOpmerking record)
        {
            try
            {
                record ??= new ArtikelOpmerking();
                if (xFilterCombo.SelectedIndex > -1)
                    record.Filter = (ArtikelFilter)xFilterCombo.SelectedIndex;
                else throw new Exception("Kies een FilterType a.u.b.");
                if (xFilterTypeCombo.SelectedIndex > -1)
                    record.FilterSoort = (ArtikelFilterSoort)xFilterTypeCombo.SelectedIndex;
                else throw new Exception("Kies een FilterSoort a.u.b.");
                var ontvangers = GetOntvangers();
                if (ontvangers.Count == 0)
                    throw new Exception("Kies onvanger(s) a.u.b.");
                if (xFilterWaarde.Value == 0)
                    throw new Exception("Vul in een waarde dat niet gelijk is aan '0' a.u.b.");
                if (string.IsNullOrEmpty(xOpmerking.Text.Trim()))
                    throw new Exception("Vul in een geldige opmerking a.u.b.");
                record.FilterWaarde = xFilterWaarde.Value;
                record.Opmerking = xOpmerking.Text.Trim();
                record.Title = xtitletextbox.Text.Trim();
                record.ImageData = xImage.Image?.ToByteArray();
                record.OpmerkingVoor = ontvangers;
                return record;
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                return null;
            }
        }

        public List<string> GetOntvangers()
        {
            var xreturn = new List<string>();
            try
            {
                xreturn = xontvangerstrip.Items.Cast<ToolStripMenuItem>().Where(x => x.Tag is string).Select(x => x.Tag as string).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return xreturn;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Title = "Kies een Afbeelding";
                ofd.Filter = "Alles|*.*|Png|*.png|JPG|*.jpg|GIF|*.gif";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var ximg = Image.FromFile(ofd.FileName);
                    xImage.Image = ximg;
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            xImage.BackColor = Color.AliceBlue;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            xImage.BackColor = Color.Transparent;
        }

        private void xok_Click(object sender, EventArgs e)
        {
            if (SaveValues())
                DialogResult = DialogResult.OK;
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xVoorbeeld_Click(object sender, EventArgs e)
        {
            var op = GetOpmerking(new ArtikelOpmerking());
            if (op != null)
            {
                var testrec = new ArtikelRecord
                {
                    ArtikelNr = "12345678",
                    Omschrijving = "Test Omschrijving",
                    AantalGemaakt = 5000,
                    TijdGewerkt = 50
                };
                testrec.UpdatedProducties.AddRange(new List<string>() { "test1", "test2"});
                var bttns = new Dictionary<string, DialogResult>();
                bttns.Add("Sluiten", DialogResult.No);
                bttns.Add("Begrepen", DialogResult.Yes);
                var xop = testrec.GetOpmerking(op);
                Manager.OnRequestRespondDialog(xop, testrec.GetTitle(op), MessageBoxButtons.OK, MessageBoxIcon.Information, null, bttns,
                    xImage.Image??Resources.default_opmerking_16757_256x256, MetroColorStyle.Purple);
            }
        }

        private void xFilterTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xFilterTypeCombo.SelectedIndex == 0)
                xFilterWaarde.DecimalPlaces = 0;
            else xFilterWaarde.DecimalPlaces = 2;
        }

        private void ArtikelOpmerkingForm_Shown(object sender, EventArgs e)
        {
            LoadValues(SelectedOpmerking);
        }

        private void verwijderenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xGelezenDoorList.SelectedObjects.Count > 0 && SelectedOpmerking != null)
            {
                var xitems = xGelezenDoorList.SelectedObjects.Cast<KeyValuePair<string, DateTime>>().ToList();
                foreach (var item in xitems)
                {
                    SelectedOpmerking.GelezenDoor.Remove(item.Key);
                    xGelezenDoorList.RemoveObject(item);
                }
            }
        }
    }
}
