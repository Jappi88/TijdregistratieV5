using MetroFramework;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms.GereedMelden
{
    public partial class GereedAfwijkingForm : Forms.MetroBase.MetroBaseForm
    {
        private IProductieBase _Productie;
        public GereedAfwijkingForm()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(IProductieBase productie, string paraaf)
        {
            _Productie = productie;
            this.Text = $"Productie Afwijking: {productie.ProductieNr} | {productie.ArtikelNr}";
            this.Invalidate();
            CreateMessage(productie, paraaf);
            return base.ShowDialog();
        }

        public string Reden { get; private set; }

        private void CreateMessage(IProductieBase productie, string paraaf)
        {
            if (productie == null) return;
            decimal afwijking = productie.GetAfwijking();
            xfieldmessage.Text = GetText(productie, afwijking);
            InitRedenen(productie, afwijking, paraaf);
        }

        private void InitRedenen(IProductieBase productie, decimal afwijking, string paraaf)
        {
            try
            {
                xredenen.BeginUpdate();
                xredenen.Items.Clear();
                List<string> redens = new List<string>();
                if (afwijking < 0)
                {
                    var xmats = productie.GetMaterialen();
                    foreach (var mat in xmats)
                    {
                        redens.Add($"Problemen met {mat.Omschrijving}({mat.ArtikelNr}).");
                    }

                    if (productie is Bewerking {IsBemand: false})
                    {
                        var wps = productie.GetWerkPlekken();
                        foreach (var wp in wps)
                        {
                            redens.Add($"'{wp.Naam}' Functioneert niet als behoren.");
                        }
                    }

                    var pers = productie.GetPersonen(false);
                    foreach (var per in pers)
                    {
                        redens.Add($"'{per.PersoneelNaam}' Heeft ondermaats gepresteert");
                    }
                    redens.Add("Ontbrekend hulpmiddel.");
                }
                else
                {
                    if (productie is Bewerking {IsBemand: false})
                    {
                        var wps = productie.GetWerkPlekken();
                        foreach (var wp in wps)
                        {
                            redens.Add($"'{wp.Naam}' Heeft boven verwachting gepresteerd");
                        }
                    }

                    var pers = productie.GetPersonen(false);
                    foreach (var per in pers)
                    {
                        redens.Add($"'{per.PersoneelNaam}' Heeft boven verwachting gepresteert");
                    }
                   
                }
                redens.Add($"Ik '{paraaf}' heb alle tijden gecontrolleerd en het klopt allemaal.");
                redens.Add("Andere reden, namelijk: ");
                foreach (var xreden in redens)
                {
                    var lv = new ListViewItem(xreden);
                    lv.Tag = xreden;
                    lv.ToolTipText = xreden;
                    xredenen.Items.Add(lv);
                }
                xredenen.EndUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private string GetText(IProductieBase productie, decimal afwijking)
        {
            Color color;
            var xbase = $"<h3 Align='left'>" +
                        "<span color='{0}'>Productie heeft een te hoge {1} afwijking van <span Color='{2}'>{3}</span>!<br>" +
                        $"Wat is de reden dat de productie zo'n hoge afwijking heeft?" +
                        $"</span></h3>";
            string xafw = $"{(afwijking > 0 ? "+" : "")}{afwijking}% ({productie.ActueelPerUur}/{productie.PerUur} P/u)";
            if (afwijking < 0)
            {
                color = IProductieBase.GetNegativeColorByPercentage(afwijking);
                return string.Format(xbase,Color.DarkRed.Name, "<u>Negatieve</u>", color.Name, xafw);
            }
            else
            {
                color = IProductieBase.GetNegativeColorByPercentage(afwijking);
                return string.Format(xbase, Color.DarkGreen.Name, "<u>Positieve</u>", color.Name, xafw);
            }
        }

       
        private void button1_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            try
            {
                var xitems = xredenen.Items.Cast<ListViewItem>().Where(x => x.Checked).Select(x => x.Text).ToList();
                if (xitems.Count == 0)
                    throw new Exception("Kies een geldige reden!");
                Reden = string.Join("\n", xitems);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }

        }

        private void xredenen_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked && e.Item.Text.ToLower().StartsWith("andere"))
            {
                var xvalue = e.Item.Text.Split(':').LastOrDefault()?.Trim();
                var txt = new TextFieldEditor();
                txt.UseSecondary = false;
                txt.EnableSecondaryField = false;
                txt.MultiLine = true;
                txt.Style = MetroColorStyle.Red;
                txt.Title = "Vul in een geldige reden voor de afwijking";
                txt.SelectedText = xvalue;
                if (txt.ShowDialog() == DialogResult.OK && txt.SelectedText.Trim().Length > 1)
                {
                    e.Item.Text = $"Andere reden, namelijk: {txt.SelectedText}";
                    e.Item.Tag = e.Item.Text;
                }
                else e.Item.Checked = false;
            }
        }
    }
}
