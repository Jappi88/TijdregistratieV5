using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MetroFramework.Forms;
using ProductieManager.Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Forms
{
    public partial class ProductieInfoForm : MetroForm
    {
        private readonly List<Bewerking> _Bewerkingen;

        public ProductieInfoForm()
        {
            InitializeComponent();
        }

        public ProductieInfoForm(ProductieFormulier productie) : this()
        {
            if (productie == null) return;
            Text = $@"Productie Info [{productie.ArtikelNr} => {productie.ProductieNr}]";
            productieInfoUI1.SetInfo(productie, productie.Omschrijving, Color.Black, Color.Purple, Color.White);
            productieInfoUI1.Visible = true;
            //xinfopanel.Text = productie.GetHtmlBody(productie.Omschrijving, productie.GetImageFromResources(),
            //    new Size(64, 64), Color.Black, Color.Purple, Color.White);
            Invalidate();
        }

        public ProductieInfoForm(List<Bewerking> bewerkingen) : this()
        {
            if (bewerkingen == null) return;
            Text = $@"Info van [{bewerkingen.Count} {(bewerkingen.Count == 1 ? "bewerking" : "bewerkingen")}]";
            productieInfoUI1.Visible = false;
            xinfopanel.Text = CreateProductiesInfoHtml(bewerkingen);
            xstatsb.Visible = bewerkingen is {Count: > 0};
            _Bewerkingen = bewerkingen;
            xexport.Visible = bewerkingen is { Count: > 0 }; ;
            Invalidate();
        }

        public ProductieInfoForm(Bewerking bew) : this()
        {
            if (bew == null) return;
            Text = $@"Werk Info [{bew.ArtikelNr} => {bew.ProductieNr}]";
            productieInfoUI1.SetInfo(bew, bew.Omschrijving, Color.Black, Color.Purple, Color.White);
            productieInfoUI1.Visible = true;
            //xinfopanel.Text = bew.GetHtmlBody($"{bew.Naam} van: {bew.Omschrijving}", bew.GetImageFromResources(),
            //    new Size(64, 64), Color.Black, Color.Purple, Color.White);
            Invalidate();
        }

        public ProductieInfoForm(WerkPlek plek) : this()
        {
            if (plek == null || plek.Werk == null) return;
            Text = $@"Werk Info [{plek.Werk.ArtikelNr} => {plek.Werk.ProductieNr}]";
            productieInfoUI1.SetInfo(plek.Werk, $"{plek.Naam}: {plek.Werk.Omschrijving}", Color.Black, Color.Purple, Color.White);
            productieInfoUI1.Visible = true;
            //xinfopanel.Text = plek.Werk.GetHtmlBody($"{plek.Naam}: {plek.Werk.Omschrijving}",
            //    Resources.iconfinder_technology, new Size(64, 64), Color.Black, Color.Purple, Color.White);
            Invalidate();
        }

        public ProductieInfoForm(Personeel persoon) : this()
        {
            Text = "";
            xinfopanel.Text = "";
            Invalidate();
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private string CreateProductiesInfoHtml(List<Bewerking> bewerkingen)
        {
            var image = Resources.stats_15267_128x128;
            var ximage = "<td width = '64' style = 'padding: 5px 5px 0 0' >\r\n" +
                         $"<img width='64' height='64'  src = 'data:image/png;base64,{image.Base64Encoded()}' />\r\n" +
                         "</td>";
            var x1 = bewerkingen.Count == 1 ? "bewerking" : "bewerkingen";
            var title = $"Informatie berekend over {bewerkingen.Count} {x1}.";
            var totaalbws = bewerkingen.Count;

            var totaalverschillende = bewerkingen.Select(x => x.Naam).Distinct().Count();
            x1 = totaalverschillende == 1 ? "bewerking" : "bewerkingen";
            var xinfo = $"{totaalverschillende} verschillende {x1}";
            var totaalgemaakt = bewerkingen.Sum(x => x.TotaalGemaakt);
            var totaalgemaaktText = totaalgemaakt.ToUitgeschrevenTekst();
            var xbws = bewerkingen.Where(x => x.ActueelPerUur > 0).ToList();
            var totaalgereed = bewerkingen.Count(x => x.State == ProductieState.Gereed);
            var latergereed = bewerkingen.Count(x => x.State == ProductieState.Gereed && x.DatumGereed > x.LeverDatum);
            var optijdgereed = totaalgereed - latergereed;
            var gestart = bewerkingen.Count(x => x.State == ProductieState.Gestart);
            var gestopt = bewerkingen.Count(x => x.State == ProductieState.Gestopt);
            var verwijderd = bewerkingen.Count(x => x.State == ProductieState.Verwijderd);
            var telaat = bewerkingen.Count(x => x.TeLaat);

            var actueelperuur = (int) (xbws.Sum(x => x.ActueelPerUur) / xbws.Count);
            if (actueelperuur < 0) actueelperuur = 0;
            var peruur = bewerkingen.Sum(x => x.PerUur) / bewerkingen.Count;
            if (peruur < 0)
                peruur = 0;
            var actueeltotaaluurgewerkt = Math.Round(bewerkingen.Sum(x => x.TijdAanGewerkt()), 2);
            var totaaluurgewerkt = Math.Round(bewerkingen.Sum(x => x.DoorloopTijd), 2);
            var personen = new List<Personeel>();
            foreach (var bw in bewerkingen)
            foreach (var per in bw.GetPersoneel())
                if (!personen.Any(x =>
                    string.Equals(x.PersoneelNaam, per.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase)))
                    personen.Add(per);
            var totaalpersonen = personen.Count;


            var xreturn = "<html>\r\n" +
                          "<head>\r\n" +
                          $"<style>{IProductieBase.GetStylesheet("StyleSheet")}</style>\r\n" +
                          $"<Title>{title}</Title>\r\n" +
                          "<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          "</head>\r\n" +
                          $"<body style='background - color: {Color.White.Name}; background-gradient: {Color.MediumPurple.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 50px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {Color.White.Name}'>\r\n" +
                          $"       {title}\r\n" +
                          "        <br/>\r\n" +
                          $"        <span style=\'font-size: x-small;\'>{xinfo}</span>\r\n " +
                          "</h1>\r\n" +
                          "<blockquote class='whitehole'>\r\n" +
                          "       <p style = 'margin-top: 0px' >\r\n" +
                          "<table border = '0' width = '100%' >\r\n" +
                          "<tr style = 'vertical-align: top;' >\r\n" +
                          ximage +
                          "<td>" +
                          "<h3>\r\n" +
                          "Cijfers Berekend:\r\n" +
                          "</h3 >\r\n" +
                          "<div>\r\n" +
                          $"Totaal Gemaakt: <b>{(totaalgemaakt == 0 ? "0" : totaalgemaakt.ToString("##,###"))}</b><br>" +
                          $"Totaal Gemaakt Zin: <b>{totaalgemaaktText}</b><br>" +
                          $"Gemeten P/u: <b>{(actueelperuur == 0 ? "0" : actueelperuur.ToString("##,###"))} p/u</b><br>" +
                          $"Formulier P/u: <b>{(peruur == 0? "0" : peruur.ToString("##,###"))} p/u</b><br>" +
                          $"Gemeten TijdGewerkt: <b>{(actueeltotaaluurgewerkt == 0 ? "0" : actueeltotaaluurgewerkt.ToString("##,###"))} uur</b><br>" +
                          $"Formulier Doorlooptijd: <b>{(totaaluurgewerkt == 0 ? "0" : totaaluurgewerkt.ToString("##,###"))} uur</b><br>" +
                          $"Aantal Personen: <b>{(totaalpersonen == 0 ? "0" : totaalpersonen.ToString("##,###"))}</b><br>" +
                          $"Nu te laat: <b>{(telaat == 0 ? "0" : telaat.ToString("##,###"))} {(telaat == 1 ? "bewerking" : "bewerkingen")}</b><br>" +
                          $"Later gereed gemeld: <b>{(latergereed == 0 ? "0" : latergereed.ToString("##,###"))} {(latergereed == 1 ? "bewerking" : "bewerkingen")}</b><br>" +
                          $"Op tijd gereed gemeld: <b>{(optijdgereed == 0 ? "0" : optijdgereed.ToString("##,###"))} {(optijdgereed == 1 ? "bewerking" : "bewerkingen")}</b><br>" +
                          $"Gereed: <b>{(totaalgereed == 0 ? "0" : totaalgereed.ToString("##,###"))} {(totaalgereed == 1 ? "bewerking" : "bewerkingen")}</b><br>" +
                          $"Nu bezig: <b>{(gestart == 0 ? "0" : gestart.ToString("##,###"))} {(gestart == 1 ? "bewerking" : "bewerkingen")}</b><br>" +
                          $"Gestopt: <b>{(gestopt == 0 ? "0" : gestopt.ToString("##,###"))} {(gestopt == 1 ? "bewerking" : "bewerkingen")}</b><br>" +
                          $"Verdwijderd: <b>{(verwijderd == 0 ? "0" : verwijderd.ToString("##,###"))} {(verwijderd == 1 ? "bewerking" : "bewerkingen")}</b><br>" +
                          "</div>\r\n" +
                          "<hr />" +
                          "</td>" +
                          "</tr>\r\n" +
                          "</table >\r\n" +
                          "</p>\r\n" +
                          "</blockquote>\r\n" +
                          "</body>\r\n" +
                          "</html>";
            return xreturn;
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void xstatsb_Click(object sender, EventArgs e)
        {
            if (_Bewerkingen == null || _Bewerkingen.Count == 0) return;
            new ViewChartForm(_Bewerkingen).ShowDialog();
        }

        private void xexport_Click(object sender, EventArgs e)
        {
            if (_Bewerkingen == null || _Bewerkingen.Count == 0) return;
            new CreateExcelForm(_Bewerkingen).ShowDialog();
        }
    }
}