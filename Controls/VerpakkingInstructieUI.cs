using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;

namespace Controls
{
    public partial class VerpakkingInstructieUI : UserControl
    {
        public VerpakkingInstructie VerpakkingInstructie => GetVerpakkingInstructie();

        private VerpakkingInstructie _VerpakkingInstructie;
        public bool IsEditmode { get; set; }

        public VerpakkingInstructieUI()
        {
            InitializeComponent();
        }

        public void InitFields(VerpakkingInstructie verpakking, bool editmode, string title)
        {
            IsEditmode = editmode;
            _VerpakkingInstructie = (verpakking == null ? new VerpakkingInstructie() : verpakking.CreateCopy());
            if (editmode)
            {
                xeditpanel.Visible = true;
                htmlPanel1.Visible = false;
                
                var x = _VerpakkingInstructie;
                xverpakkingsoort.Text = x.VerpakkingType?.Trim() ?? "";
                xpalletsoort.Text = x.PalletSoort?.Trim() ?? "";
                xstandaardlocatie.Text = x.StandaardLocatie?.Trim() ?? "";
                xbulklocatie.Text = x.BulkLocatie?.Trim() ?? "";

                xverpakkenper.SetValue(x.VerpakkenPer);
                xlagenpercolli.SetValue(x.LagenOpColli);
                xdozenpercolli.SetValue(x.DozenOpColli);
                xdozenperlaag.SetValue(x.PerLaagOpColli);
                xaantelpercolli.SetValue(x.ProductenPerColli);
            }
            else
            {
                xeditpanel.Visible = false;
                htmlPanel1.Visible = true;
                htmlPanel1.Text = CreateHtmlText(_VerpakkingInstructie, title);
            }
        }

        private string CreateHtmlText(VerpakkingInstructie verpakking, string title)
        {
            var x = verpakking ?? new VerpakkingInstructie();
            string ximage = $"<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                            $"<img width='{64}' height='{64}'  src = 'Verpakkingicon' />\r\n" +
                            $"</td>";

            var xreturn = $"<html>\r\n" +
                          $"<head>\r\n" +
                          $"<link rel=\"Stylesheet\" href=\"StyleSheet\" />\r\n" +
                          $"<Title>Verpakking Instructie</Title>\r\n" +
                          $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          $"</head>\r\n" +
                          $"<body style='background - color: {Color.White.Name}; background-gradient: {Color.SaddleBrown.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 50px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {Color.White.Name}'>\r\n" +
                          $"       {title}\r\n" +
                          $"        <br/>\r\n" +
                          $"        <span style=\'font-size: x-small;\'>Aangepast op {x.LastChanged}</span>\r\n " +
                          $"</h1>\r\n" +
                          $"<blockquote class='whitehole'>\r\n" +
                          $"       <p style = 'margin-top: 0px' >\r\n" +
                          $"<table border = '0' width = '100%' >\r\n" +
                          $"<tr style = 'vertical-align: top;' >\r\n" +
                          ximage +
                          $"<td>" +
                          $"<h2>Verpakking</h2>" +
                          $"<div>";

            if (!string.IsNullOrEmpty(x.VerpakkingType))
                xreturn += $"Verpakking Soort: <b>{x.VerpakkingType?.Trim()}</b><br>";

            if (!string.IsNullOrEmpty(x.PalletSoort))
                xreturn += $"Pallet Soort: <b>{x.PalletSoort?.Trim()}</b><br>";

            xreturn += $"</div>" +
            $"<hr />" +
            $"<h2>Verpakking Aantallen</h2>" +
            $"<div>";

            if (x.VerpakkenPer > 0)
                xreturn += $"Aantal Per Doos/Bak: <b>{x.VerpakkenPer:##,###}</b><br>";

            if (x.LagenOpColli > 0)
                xreturn += $"Lagen Per Colli: <b>{x.LagenOpColli:##,###}</b><br>";

            if (x.PerLaagOpColli > 0)
                xreturn += $"Dozen/Bakken Per Laag: <b>{x.PerLaagOpColli:##,###}</b><br>";

            if (x.DozenOpColli > 0)
                xreturn += $"Dozen/Bakken Per Colli: <b>{x.DozenOpColli:##,###}</b><br>";

            if (x.ProductenPerColli > 0)
                xreturn += $"Aantal Per Colli: <b>{x.ProductenPerColli:##,###}</b>";

            xreturn += $"</div>" +
                      $"<hr />" +
                      $"<h2>Locatie</h2>" +
                      $"<div>";

            if (!string.IsNullOrEmpty(x.StandaardLocatie))
                xreturn += $"Standaard Locatie: <b>{x.StandaardLocatie?.Trim()}</b><br>";
            if (!string.IsNullOrEmpty(x.BulkLocatie))
                xreturn += $"Bulk Locatie: <b>{x.BulkLocatie?.Trim()}</b>";
            xreturn += $"</div>" +
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

        public VerpakkingInstructie GetVerpakkingInstructie()
        {
           
            try
            {
                if (!IsEditmode) return _VerpakkingInstructie;
                var x = new VerpakkingInstructie
                {
                    VerpakkingType = xverpakkingsoort.Text.Trim(),
                    PalletSoort = xpalletsoort.Text.Trim(),
                    StandaardLocatie = xstandaardlocatie.Text.Trim(),
                    BulkLocatie = xbulklocatie.Text.Trim(),
                    VerpakkenPer = (int)xverpakkenper.Value,
                    LagenOpColli = (int)xlagenpercolli.Value,
                    DozenOpColli = (int)xdozenpercolli.Value,
                    PerLaagOpColli = (int)xdozenperlaag.Value,
                    ProductenPerColli = (int)xaantelpercolli.Value
                };

                return x;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void htmlPanel1_StylesheetLoad(object sender, HtmlRenderer.Entities.HtmlStylesheetLoadEventArgs e)
        {
            e.SetStyleSheet = IProductieBase.GetStylesheet(e.Src);
        }

        private void htmlPanel1_ImageLoad(object sender, HtmlRenderer.Entities.HtmlImageLoadEventArgs e)
        {
            var xkey = e.Src;
            switch (xkey.ToLower().Trim())
            {
                case "verpakkingicon":
                    e.Callback(Resources.package_box_128x128);
                    break;
            }
        }
    }
}
