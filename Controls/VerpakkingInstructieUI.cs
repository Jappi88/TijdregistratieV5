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
using MetroFramework.Drawing.Html;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using TheArtOfDev.HtmlRenderer.Core.Entities;

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
                htmlPanel1.Text =
                    _VerpakkingInstructie.CreateHtmlText(title, Color.White, Color.SaddleBrown, Color.White,true);
            }
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

        private void htmlPanel1_StylesheetLoad(object sender, HtmlStylesheetLoadEventArgs e)
        {
            e.SetStyleSheet = IProductieBase.GetStylesheet(e.Src);
        }

        private void htmlPanel1_ImageLoad(object sender, HtmlImageLoadEventArgs e)
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
