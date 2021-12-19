using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms.GereedMelden
{
    public partial class GereedAfwijkingForm : MetroFramework.Forms.MetroForm
    {
        private IProductieBase _Productie;
        public GereedAfwijkingForm()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(IProductieBase productie)
        {
            _Productie = productie;
            this.Text = $"Productie Afwijking: {productie.ProductieNr} | {productie.ArtikelNr}";
            this.Invalidate();
            CreateMessage(productie);
            return base.ShowDialog();
        }

        public string Reden { get; private set; }

        private void CreateMessage(IProductieBase productie)
        {
            if (productie == null) return;
            decimal afwijking = productie.GetAfwijking();
            xfieldmessage.Text = GetText(productie, afwijking);
            InitRedenen(productie, afwijking);
        }

        private void InitRedenen(IProductieBase productie, decimal afwijking)
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

                    var wps = productie.GetWerkPlekken();
                    foreach (var wp in wps)
                    {
                        redens.Add($"'{wp.Naam}' Functioneert niet als behoren.");
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
                    var wps = productie.GetWerkPlekken();
                    foreach (var wp in wps)
                    {
                        redens.Add($"'{wp.Naam}' Heeft boven verwachting gepresteerd");
                    }

                    var pers = productie.GetPersonen(false);
                    foreach (var per in pers)
                    {
                        redens.Add($"'{per.PersoneelNaam}' Heeft boven verwachting gepresteert");
                    }
                   
                }
                redens.Add("Alle tijden kloppen, en alles is gewoon goed gegaan.");
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
                return string.Format(xbase,Color.DarkRed.Name, "negatieve", color.Name, xafw);
            }
            else
            {
                color = IProductieBase.GetNegativeColorByPercentage(afwijking);
                return string.Format(xbase, Color.DarkGreen.Name, "positieve", color.Name, xafw);
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
                XMessageBox.Show(ex.Message, "Fout", MessageBoxIcon.Error);
            }

        }

        private void xmaterialen_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

        }
    }
}
