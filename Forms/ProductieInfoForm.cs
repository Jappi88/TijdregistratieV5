using System;
using System.Drawing;
using System.Windows.Forms;
using Rpm.Productie;
using Resources = ProductieManager.Properties.Resources;

namespace Forms
{
    public partial class ProductieInfoForm : MetroFramework.Forms.MetroForm
    {
        public ProductieInfoForm()
        {
            InitializeComponent();
        }

        public ProductieInfoForm(ProductieFormulier productie) : this()
        {
            if (productie == null) return;
            Text = $"Productie Info [{productie.ArtikelNr} => {productie.ProductieNr}]";
            xinfopanel.Text = productie.GetHtmlBody(productie.Omschrijving, productie.GetImageFromResources(), new Size(64, 64), Color.Black, Color.Purple, Color.White);
            Invalidate();
        }

        public ProductieInfoForm(Bewerking bew) : this()
        {
            if (bew == null) return;
            Text = $"Werk Info [{bew.ArtikelNr} => {bew.ProductieNr}]";
            xinfopanel.Text = bew.GetHtmlBody($"{bew.Naam} van: {bew.Omschrijving}", bew.GetImageFromResources(), new Size(64, 64), Color.Black, Color.Purple, Color.White);
            Invalidate();
        }

        public ProductieInfoForm(WerkPlek plek) : this()
        {
            if (plek == null || plek.Werk == null) return;
            Text = $"Werk Info [{plek.Werk.ArtikelNr} => {plek.Werk.ProductieNr}]";
            xinfopanel.Text = plek.Werk.GetHtmlBody($"{plek.Naam}: {plek.Werk.Omschrijving}", Resources.iconfinder_technology, new Size(64, 64), Color.Black, Color.Purple, Color.White);
            Invalidate();
        }

        public ProductieInfoForm(Personeel persoon) : this()
        {
            this.Text = "";
            xinfopanel.Text = "";
            Invalidate();
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}