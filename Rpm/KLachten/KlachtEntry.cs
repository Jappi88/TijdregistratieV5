using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using Rpm.Productie;
using Rpm.Various;

namespace ProductieManager.Rpm.KLachten
{
    public class KlachtEntry
    {
        public int ID { get; private set; } = DateTime.Now.GetHashCode();
        public string Onderwerp { get; set; }
        public List<string> Bijlages { get; set; } = new List<string>();
        public string Melder { get; set; }
        public string Afzender { get; set; }
        public string Ontvanger { get; set; }
        public string Omschrijving { get; set; }
        public List<string> ProductieNrs { get; set; } = new XfaForm.Stack2<string>();
        public DateTime DatumGeplaatst { get; set; } = DateTime.Now;
        public bool IsGelezen { get; set; }


        public string ToHtml()
        {
            try
            {
                string ximage = $"<td width = '64' style = 'padding: 5px 5px 0 0' >\r\n" +
                                $"<img width='{64}' height='{64}'  src = 'ProductieWarningicon' />\r\n" +
                                $"</td>";
                string prods = ProductieNrs.Count > 0
                    ? $"{string.Join("\r\n", ProductieNrs.Select(x => $"<span style = 'color: {Color.DarkOrange.Name}'><a href='{x}'>{x}</a></span>"))}"
                    : "";
                string imgs = Bijlages.Count > 0
                    ? $"{string.Join("\r\n", Bijlages.Select(x => $"<img width = \"75%\" height=\"500\" padding=\"10;10;10;10\"src=\"{x}\"/>"))}"
                    : "";
                var xreturn = $"<html>\r\n" +
             $"<head>\r\n" +
             $"<style>{IProductieBase.GetStylesheet("StyleSheet")}</style>\r\n" +
             $"<Title>{Onderwerp}</Title>\r\n" +
             $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
             $"</head>\r\n" +
             $"<body style='background - color: white; background-gradient: white; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
             $"<h1 align='center' style='color: black'>\r\n" +
             $"       {Onderwerp}\r\n" +
             $"        <br/>\r\n" +
             $"        <span style=\'font-size: x-small;\'>{DatumGeplaatst}</span>\r\n " +
             $"</h1>\r\n" +
             $"<blockquote class='whitehole'>\r\n" +
             $"       <p style = 'margin-top: 0px' >\r\n" +
             $"<table border = '0' width = '100%' >\r\n" +
             $"<tr style = 'vertical-align: top;' >\r\n" +
             ximage +
             $"<td>" +
             $"<div>\r\n" +
             $"Klacht gemeld door <b>'{Melder}'</b>.<br><br>" +
             $"{imgs}" +
             $"<b>{Omschrijving?.Replace("\n", " <br> ")}</b><br><br>" +
             $"<b>Geproduceerd</b> :<br>" +
             $"{prods}\r\n" +
             $"</div>\r\n" +
             $"<hr />" +
             $"</td>" +
             $"</tr>\r\n" +
             $"</table >\r\n" +
             $"</p>\r\n" +
             $"</blockquote>\r\n" +
             $"</body>\r\n" +
             $"</html>";
                return xreturn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
