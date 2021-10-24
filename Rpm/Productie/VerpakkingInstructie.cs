using System;
using System.Drawing;

namespace Rpm.Productie
{
    public class VerpakkingInstructie
    {
        public string VerpakkingType { get; set; }
        public string PalletSoort { get; set; }
        public int VerpakkenPer { get; set; }
        public int LagenOpColli { get; set; }
        public int DozenOpColli { get; set; }
        public int PerLaagOpColli { get; set; }
        public int ProductenPerColli { get; set; }
        public string StandaardLocatie { get; set; }
        public string BulkLocatie { get; set; }
        public bool IsLijdend { get; set; }
        public DateTime LastChanged { get; set; }

        public VerpakkingInstructie()
        {
            LastChanged = DateTime.Now;
        }

        public string CreateHtmlText(string title, Color background, Color backgroundgradient, Color textcolor, bool useimage)
        {
            var x = this;
            string ximage = $"<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                            $"<img width='{64}' height='{64}'  src = 'Verpakkingicon' />\r\n" +
                            $"</td>";
            if (!useimage) ximage = "";
            string value = $"<div>" +
                           $"<h2>Verpakking</h2>" +
                           $"<div>";
            if (!string.IsNullOrEmpty(x.VerpakkingType))
                value += $"Verpakking Soort: <b>{x.VerpakkingType?.Trim()}</b><br>";

            if (!string.IsNullOrEmpty(x.PalletSoort))
                value += $"Pallet Soort: <b>{x.PalletSoort?.Trim()}</b><br>";

            value += $"</div>" +
                     $"<hr />" +
                     $"<h2>Verpakking Aantallen</h2>" +
                     $"<div>";

            if (x.VerpakkenPer > 0)
                value += $"Aantal Per Doos/Bak: <b>{x.VerpakkenPer:##,###}</b><br>";

            if (x.LagenOpColli > 0)
                value += $"Lagen Per Colli: <b>{x.LagenOpColli:##,###}</b><br>";

            if (x.PerLaagOpColli > 0)
                value += $"Dozen/Bakken Per Laag: <b>{x.PerLaagOpColli:##,###}</b><br>";

            if (x.DozenOpColli > 0)
                value += $"Dozen/Bakken Per Colli: <b>{x.DozenOpColli:##,###}</b><br>";

            if (x.ProductenPerColli > 0)
                value += $"Aantal Per Colli: <b>{x.ProductenPerColli:##,###}</b>";

            value += $"</div>" +
                     $"<hr />" +
                     $"<h2>Locatie</h2>" +
                     $"<div>";

            if (!string.IsNullOrEmpty(x.StandaardLocatie))
                value += $"Standaard Locatie: <b>{x.StandaardLocatie?.Trim()}</b><br>";
            if (!string.IsNullOrEmpty(x.BulkLocatie))
                value += $"Bulk Locatie: <b>{x.BulkLocatie?.Trim()}</b>";
            value += $"</div>" +
                     $"<hr />" +
                     $"</div>";

                     var xreturn = $"<html>\r\n" +
                                   $"<head>\r\n" +
                                   $"<link rel=\"Stylesheet\" href=\"StyleSheet\" />\r\n" +
                                   $"<Title>Verpakking Instructie</Title>\r\n" +
                                   $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                                   $"</head>\r\n" +
                                   $"<body style='background - color: {background.Name}; background-gradient: {backgroundgradient.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
                                   $"<h1 align='center' style='color: {textcolor.Name}'>\r\n" +
                                   $"       {title}\r\n" +
                                   $"        <br/>\r\n" +
                                   $"        <span style=\'font-size: x-small;\'>Aangepast op {x.LastChanged}</span>\r\n " +
                                   $"</h1>\r\n" +
                                   $"<blockquote class='whitehole'>\r\n" +
                                   $"       <p style = 'margin-top: 0px' >\r\n" +
                                   $"<table border = '0' width = '100%' >\r\n" +
                                   $"<tr style = 'vertical-align: top;' >\r\n" +
                                   ximage +
                                   "<td>{0}</td>" +
                                   $"</tr>\r\n" +
                                   $"</table >\r\n" +
                                   $"</p>\r\n" +
                                   $"</blockquote>\r\n" +
                                   $"</body>\r\n" +
                                   $"</html>";
                     if (string.IsNullOrEmpty(title))
                         return value;

                     return string.Format(xreturn, value);
        }
    }
}
