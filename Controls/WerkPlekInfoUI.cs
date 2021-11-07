using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Controls
{
    public partial class WerkPlekInfoUI : UserControl
    {
        public string Werkplek { get; private set; }
        public Bewerking Huidig { get; private set; }
        public Bewerking Volgende { get; private set; }

        public WerkPlekInfoUI()
        {
            InitializeComponent();
        }

        private string GetWerkplekOmschrijving(WerkPlek plek)
        {
            return $"[{plek.ProductieNr}|{plek.ArtikelNr}] {plek.WerkNaam} van {plek.Omschrijving}.";
        }

        private string GetWerkplekOmschrijving(string plek, Bewerking werk)
        {
            return $"<a color= black href='{werk.ProductieNr}'>[{werk.ProductieNr}|{werk.ArtikelNr}] {werk.Naam} van {werk.Omschrijving}.</a>";
        }

        public void Init(string werkplek, Bewerking huidig, Bewerking volgende)
        {
            Werkplek = werkplek;
            Huidig = huidig;
            Volgende = volgende;

            xmeebezig.Text = GetInfoHtml(werkplek, huidig, volgende);
        }

        public string GetInfoHtml(string werkplek, Bewerking huidig, Bewerking volgende)
        {
            var ximagename = huidig != null ? "PlayIcon" : "StopIcon";
            string ximage = $"<img width='48' height='48'  src = '{ximagename}' />\r\n";
            var xmeebzig = "";
            var xvolgnde = "";
            if (huidig == null)
                xmeebzig = $"<b>{werkplek} is niet bezig!</b>";
            else xmeebzig = $"<b>NU OP <u>{werkplek}</u></b>: {GetWerkplekOmschrijving(werkplek, huidig)}";
            if (volgende != null)
                xvolgnde = $"<b>VOLGENDE OP <u>{werkplek}</u></b>: {GetWerkplekOmschrijving(werkplek, volgende)}";
            else xvolgnde = $"<b>Er is geen productie beschikbaar voor {werkplek}.</b>";
            var xreturn = $"<html>\r\n" +
                          $"<head>\r\n" +
                          $"<style>{IProductieBase.GetStylesheet("StyleSheet")}</style>\r\n" +
                          $"<Title>{werkplek}</Title>\r\n" +
                          $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          $"</head>\r\n" +
                          $"<body style='background - color: {Color.White.Name}; background-gradient: {Color.White.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {Color.Black.Name}'>\r\n" +
                          $"       {werkplek}\r\n" +
                          $"</h1>\r\n" +
                          $"<blockquote class='whitehole'>\r\n" +
                          $"       <p style = 'margin-top: 0px' >\r\n" +
                          $"<table border = '0' width = '100%' >\r\n" +
                          $"<tr style = 'vertical-align: top;' >\r\n" +
                          $"<td>" +
                          ximage +
                          $"</td><td><h2><div>{xmeebzig}</div></h2></td>" +
                          $"<td>" +
                          $"<img width='48' height='48'  src = 'VolgendeIcon' />\r\n" +
                          $"</td><td><h2>" +
                          $"<div>" +
                          $"{xvolgnde}" +
                          $"</div></h2></td>" +
                          
                          $"</tr>\r\n" +
                          $"<hr />" +
                          $"</table >\r\n" +
                          $"</p>\r\n" +
                          $"</blockquote>\r\n" +
                          $"</body>\r\n" +
                          $"</html>";
            return xreturn;
        }

        private void xmeebezig_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Control c)
                c.BackColor = Color.AliceBlue;
        }

        private void xmeebezig_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Control c)
                c.BackColor = Color.White;
        }

        private void xmeebezig_DoubleClick(object sender, EventArgs e)
        {
            if (Huidig != null)
                Manager.FormulierActie(new object[] {Huidig.Parent.CreateCopy(), Huidig}, MainAktie.OpenProductie);
        }

        private void xvolgende_DoubleClick(object sender, EventArgs e)
        {
            if (Volgende != null)
                Manager.FormulierActie(new object[] {Volgende.Parent.CreateCopy(), Volgende}, MainAktie.OpenProductie);
        }

        private void xmeebezig_ImageLoad(object sender, HtmlRenderer.Entities.HtmlImageLoadEventArgs e)
        {
            if (e.Src.Contains("Play"))
            {
                e.Callback(Resources.play_button_icon_icons_com_60615);
            }
            if (e.Src.Contains("Stop"))
            {
                e.Callback(Resources.stop_red256_24890);
            }
            if (e.Src.Contains("Volgende"))
            {
                e.Callback(Resources.arrow_right_64x64);
            }

            e.Handled = true;
        }

        private async void xmeebezig_LinkClicked(object sender, HtmlRenderer.Entities.HtmlLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Link))
            {
                try
                {
                    var prod = await Manager.Database.GetProductie(e.Link);
                    if (prod == null) return;
                    var bew = prod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                    Manager.FormulierActie(new object[] { prod, bew }, MainAktie.OpenProductie);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            e.Handled = true;
        }
    }
}