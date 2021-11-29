using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Forms;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using TheArtOfDev.HtmlRenderer.Core.Entities;

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
            return $"[{plek.ProductieNr}|{plek.ArtikelNr}]<br>{plek.WerkNaam} van {plek.Omschrijving}.";
        }

        private string GetWerkplekOmschrijving(string plek, Bewerking werk)
        {
            return $"[{werk.ProductieNr}|{werk.ArtikelNr}]<br>{werk.Naam} van {werk.Omschrijving}";
        }

        public void Init(string werkplek, Bewerking huidig, Bewerking volgende)
        {
            Werkplek = werkplek;
            Huidig = huidig;
            Volgende = volgende;

            xmeebezig.Text = GetHuidigHtml();
            xvolgende.Text = GetVolgendeHtml();
        }

        public string GetHuidigHtml()
        {
            var ximagename = Huidig != null ? "PlayIcon" : "StopIcon";
            string ximage = $"<img width='48' height='48'  src = '{ximagename}' />\r\n";
            var xmeebzig = "";
            if (Huidig == null)
                xmeebzig = $"<b>{Werkplek} is niet bezig!</b>";
            else xmeebzig = $"<a color= DarkBlue href='{Huidig.ProductieNr};{Huidig.Naam};{Werkplek}'><span color= DarkRed><b><u>NU OP {Werkplek}</u></b>:</span><br> {GetWerkplekOmschrijving(Werkplek, Huidig)}</a>";

            var xreturn = $"<html>\r\n" +
                          $"<head>\r\n" +
                          $"<style>{IProductieBase.GetStylesheet("StyleSheet")}</style>\r\n" +
                          $"<Title>{Werkplek}</Title>\r\n" +
                          $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          $"</head>\r\n" +
                          $"<body style='background - color: {Color.White.Name}; background-gradient: {Color.White.Name}; background-gradient-angle: 180; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {Color.DarkBlue.Name}'>\r\n" +
                          $"      Nu Op {Werkplek}\r\n" +
                          $"</h1>\r\n" +
                          $"<hr /><br><br>" +
                          $"       <p style = 'margin: 5px' >\r\n" +
                          $"<table border = '0' width = '100%' >\r\n" +
                          $"<tr style = 'vertical-align: top;' >\r\n" +
                          $"<td>" +
                          ximage +
                          $"</td>" +
                          $"<td><h2>{xmeebzig}</h2></td>" +
                         $"</tr>\r\n" +
                          $"<hr />" +
                          $"</table >\r\n" +
                          $"</p>\r\n" +
                          $"</body>\r\n" +
                          $"</html>";
            return xreturn;
        }

        public string GetVolgendeHtml()
        {
            var xvolgnde = "";
            if (Volgende != null)
                xvolgnde = $"<a color= DarkBlue href='{Volgende.ProductieNr};{Volgende.Naam};{Werkplek}'><span color= DarkRed><b><u>VOLGENDE OP {Werkplek}</u></b>:</span><br> {GetWerkplekOmschrijving(Werkplek, Volgende)}</a>";
            else xvolgnde = $"<b>Er is geen productie beschikbaar voor {Werkplek}.</b>";
            var xreturn = $"<html>\r\n" +
                          $"<head>\r\n" +
                          $"<style>{IProductieBase.GetStylesheet("StyleSheet")}</style>\r\n" +
                          $"<Title>{Werkplek}</Title>\r\n" +
                          $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          $"</head>\r\n" +
                          $"<body style='background - color: {Color.White.Name}; background-gradient: {Color.White.Name}; background-gradient-angle: 0; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {Color.DarkBlue.Name}'>\r\n" +
                          $"      Volgende Op {Werkplek}\r\n" +
                          $"</h1>\r\n" +
                          $"<hr /><br><br>" +
                          $"       <p style = 'margin: 5px' >\r\n" +
                          $"<table border = '0' width = '100%' >\r\n" +
                          $"<tr style = 'vertical-align: top;' >\r\n" +
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

        private async void xmeebezig_DoubleClick(object sender, EventArgs e)
        {
            if (Huidig != null)
            {
                var prod = await Manager.Database.GetProductie(Huidig.ProductieNr);
                if (prod == null) return;
                var bew = prod.Bewerkingen?.FirstOrDefault(x =>
                    x.IsAllowed() && string.Equals(x.Naam, Huidig.Naam, StringComparison.CurrentCultureIgnoreCase));
                Manager.FormulierActie(new object[] {prod, bew}, MainAktie.OpenProductie);
            }
        }

        private async void xvolgende_DoubleClick(object sender, EventArgs e)
        {
            if (Volgende != null)
            {
                var prod = await Manager.Database.GetProductie(Volgende.ProductieNr);
                if (prod == null) return;
                var bew = prod.Bewerkingen?.FirstOrDefault(x =>
                    x.IsAllowed() && string.Equals(x.Naam, Volgende.Naam, StringComparison.CurrentCultureIgnoreCase));
                Manager.FormulierActie(new object[] {prod, bew}, MainAktie.OpenProductie);
            }
        }

        private void xmeebezig_ImageLoad(object sender, HtmlImageLoadEventArgs e)
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
                e.Callback(Resources.playcircle_start_arrow_6048);
            }

            e.Handled = true;
        }

        private async void xmeebezig_LinkClicked(object sender, HtmlLinkClickedEventArgs e)
        {
            if (Manager.LogedInGebruiker == null ||
                Manager.LogedInGebruiker.AccesLevel < AccesType.ProductieBasis) return;
            if (!string.IsNullOrEmpty(e.Link))
            {
                try
                {
                    var xvals = e.Link.Split(';');
                    if (xvals.Length < 3) return;
                    var prod = await Manager.Database.GetProductie(xvals[0]);
                    if (prod == null) return;
                    var bew = prod.Bewerkingen?.FirstOrDefault(x =>
                        x.IsAllowed() && string.Equals(x.Naam, xvals[1], StringComparison.CurrentCultureIgnoreCase));

                    
                    if (bew is { State: ProductieState.Gestopt })
                    {
                        if (XMessageBox.Show($"Wil je {bew.Naam} van {bew.Omschrijving} starten op {xvals[2]}?", "Productie Starten",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var wp = bew.GetWerkPlek(xvals[2], true);
                            if (!wp.Personen.Any(x => x.IngezetAanKlus(bew, true, out var klusjes)))
                            {
                                var xpers = new PersoneelsForm(true);
                                if (xpers.ShowDialog() == DialogResult.OK)
                                {
                                    var selected = xpers.SelectedPersoneel;
                                    if (selected.Length > 0)
                                    {
                                        wp.AddPersonen(selected, bew);
                                        await bew.StartProductie(true, true);
                                    }
                                }
                            }
                            else await bew.StartProductie(true, true);
                        }
                    }
                    else Manager.FormulierActie(new object[] { prod, bew }, MainAktie.OpenProductie);
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