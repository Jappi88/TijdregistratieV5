using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using iTextSharp.text.pdf;
using Polenter.Serialization;
using Rpm.Productie;

namespace Rpm.Klachten
{
    public class KlachtEntry
    {
        public int ID { get; } = DateTime.Now.GetHashCode();
        public string Onderwerp { get; set; }
        public List<string> Bijlages { get; set; } = new();
        public string Melder { get; set; }
        public string Afzender { get; set; }
        public List<string> Ontvangers { get; set; }
        public string Omschrijving { get; set; }
        public List<string> ProductieNrs { get; set; } = new XfaForm.Stack2<string>();
        public DateTime DatumGeplaatst { get; set; } = DateTime.Now;
        public DateTime DatumKlacht { get; set; }

        [ExcludeFromSerialization]
        public bool IsGelezen
        {
            get
            {
                return string.Equals(Afzender, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase) ||
                       GelezenDoor != null && GelezenDoor.Any(x =>
                           string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
            }
            set
            {
                GelezenDoor.RemoveAll(x =>
                    string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
                if (value && !string.IsNullOrEmpty(Manager.Opties?.Username)) GelezenDoor.Add(Manager.Opties.Username);
            }
        }

        public List<string> GelezenDoor { get; set; } = new();

        public bool IsValid => AllowEdit ||
                               Ontvangers == null || Ontvangers.Count == 0 ||
                               Ontvangers.Any(x => x.ToLower() == "iedereen") || Ontvangers.Any(x => string.Equals(x,
                                   Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));

        public bool AllowEdit => string.Equals(Afzender, Manager.Opties?.Username,
            StringComparison.CurrentCultureIgnoreCase);

        public string GetKlachtInfoHtml()
        {
            var xreturn =
                "<body>" +
                $"<h1 align='left' color='{Color.DarkRed.Name}' style='font-size: x-large; margin:.0em;'><b>{Onderwerp}</b></h3>" +
                $"<h1 align='left' color='{Color.DarkRed.Name}' style='font-size: x-normal; margin:.4em;'><b>'{Melder}'</b> heeft een klacht ingedient op {DatumKlacht:D}</h1>";
            var pr = ProductieNrs.Count > 0
                ? $"[{string.Join(", ", ProductieNrs.Select(x => $"<a href='{x}'><span style = 'color: {Color.DarkOrange.Name}'>{x}</span></a>"))}]"
                : "";
            xreturn +=
                $"<h1 align='bottom' color='{Color.DarkRed.Name}' style='font-size: x-small;'>{pr} Geplaatst op {DatumGeplaatst:f} door <b>'{Afzender}'</b></h1>" +
                "</body>";
            return xreturn;
        }

        public string ToHtml()
        {
            try
            {
                var ximage = "<td width = '64' style = 'padding: 5px 5px 0 0' >\r\n" +
                             $"<img width='{64}' height='{64}'  src = 'ProductieWarningicon' />\r\n" +
                             "</td>";
                var prods = ProductieNrs.Count > 0
                    ? $"{string.Join(", ", ProductieNrs.Select(x => $"<a href='{x}'><span style = 'color: {Color.DarkOrange.Name}'>{x}</span></a>"))}"
                    : "";
                var imgs = Bijlages.Count > 0
                    ? $"{string.Join("<br>", Bijlages.Select(x => $"<img padding='10;10;10;10' src='{x}'/>"))}"
                    : "";
                var xreturn = "<html>\r\n" +
                              "<head>\r\n" +
                              $"<style>{IProductieBase.GetStylesheet("StyleSheet")}</style>\r\n" +
                              $"<Title>{Onderwerp}</Title>\r\n" +
                              "<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                              "</head>\r\n" +
                              "<body style='background - color: white; background-gradient: white; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
                              "<h1 align='center'>\r\n" +
                              $"        <span Color='Maroon' style='font-size: x-large;'>{Onderwerp}</span>\r\n " +
                              "        <br/>\r\n" +
                              $"        <span Color='navy' style='font-size: x-small;'>Geplaatst op {DatumGeplaatst:f}</span>\r\n " +
                              "</h1>\r\n" +
                              "<blockquote class='whitehole'>\r\n" +
                              "       <p style = 'margin-top: 0px' >\r\n" +
                              "<table border = '0' width = '100%' >\r\n" +
                              "<tr style = 'vertical-align: top;' >\r\n" +
                              // ximage +
                              "<td>" +
                              "<div>\r\n" +
                              $"<h2>Klacht van '{Melder}' gemeld door <b>'{Afzender}'</b>.</h2><br><br>" +
                              $"{imgs}<br><hr />" +
                              $"<b>{Omschrijving?.Replace("\n", " <br> ")}</b><br><br>" +
                              " <span Color='Maroon' style='font-size: x-medium;'><b>Geproduceert: </b></span><br>\r\n " +
                              $"{prods}\r\n" +
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is KlachtEntry entry && entry.ID == ID ||
                   obj is int id && id == ID ||
                   obj is string xvalue &&
                   string.Equals(xvalue, ID.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}