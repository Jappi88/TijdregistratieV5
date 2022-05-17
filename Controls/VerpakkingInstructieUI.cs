using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Drawing;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace Controls
{
    public partial class VerpakkingInstructieUI : UserControl
    {
        public VerpakkingInstructie VerpakkingInstructie { get=> _VerpakkingInstructie;
            private set => _VerpakkingInstructie = value;
        }

        private VerpakkingInstructie _VerpakkingInstructie;
        public bool IsEditmode { get; set; }
        public Color TextColor { get; set; }
        public Color BodyColor { get; set; }
        public string Title { get; set; }
        public IProductieBase Productie { get; set; }

        public bool AllowEditMode
        {
            get => xwijzigPanel.Visible;
            set => xwijzigPanel.Visible = value;
        }

        public VerpakkingInstructieUI()
        {
            InitializeComponent();
            UpdateFields(null);
        }

        public void InitFields(VerpakkingInstructie verpakking, bool editmode, string title, Color color, Color textcolor, IProductieBase productie = null)
        {
            IsEditmode = editmode;
            BodyColor = color;
            TextColor = textcolor;
            Title = title;
            _VerpakkingInstructie = verpakking?.CreateCopy();
            UpdateFields(productie);
        }

        public void Clear()
        {
            IsEditmode = false;
            AllowEditMode = false;
            _VerpakkingInstructie = null;
            UpdateFields(null);
        }

        private void UpdateFields(IProductieBase productie)
        {
            Productie = productie;
            if (Productie != null)
                VerpakkingInstructie = Productie.VerpakkingsInstructies?.CreateCopy();
            if (IsEditmode && VerpakkingInstructie != null)
            {
                xeditpanel.Visible = true;
                htmlPanel1.Visible = false;

                var x = VerpakkingInstructie;
                xverpakkingsoort.Text = x.VerpakkingType?.Trim() ?? "";
                xpalletsoort.Text = x.PalletSoort?.Trim() ?? "";
                xstandaardlocatie.Text = x.StandaardLocatie?.Trim() ?? "";
                xbulklocatie.Text = x.BulkLocatie?.Trim() ?? "";

                xverpakkenper.SetValue(x.VerpakkenPer);
                xlagenpercolli.SetValue(x.LagenOpColli);
                xdozenpercolli.SetValue(x.DozenOpColli);
                xdozenperlaag.SetValue(x.PerLaagOpColli);
                xaantelpercolli.SetValue(x.ProductenPerColli);
                xwijzig.Visible = true;
                xwijzig.Text = "Opslaan";
                xwijzig.Image = Resources.diskette_save_saveas_1514;
                xeditpanel.Visible = true;
                xsluiten.Text = "Annuleren";
                xsluiten.Visible = true;
            }
            else
            {
                xsluiten.Visible = false;
                xeditpanel.Visible = false;
                htmlPanel1.Visible = true;
                htmlPanel1.Text =
                    _VerpakkingInstructie?.CreateHtmlText(Title, Color.White, BodyColor, TextColor, true) ?? "";
                xwijzig.Visible = AllowEditMode && VerpakkingInstructie != null;
                xwijzig.Text = "Wijzig";
                xwijzig.Image = Resources.edit__52382;
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
                if (Productie != null)
                {
                    x.ArtikelNr = Productie.ArtikelNr.Trim();
                    x.ProductOmschrijving = Productie.Omschrijving.Trim();
                }
                else if(_VerpakkingInstructie != null)
                {
                    x.ArtikelNr = _VerpakkingInstructie.ArtikelNr;
                    x.ProductOmschrijving = _VerpakkingInstructie.ProductOmschrijving;
                }
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

        private void xwijzig_Click(object sender, EventArgs e)
        {
            if (IsEditmode && SaveChanges())
                UpdateFields(false,Productie);
            else UpdateFields(true, Productie);
        }

        public void UpdateFields(bool editmode, IProductieBase productie = null)
        {
            if (IsEditmode && editmode) return;
            IsEditmode = editmode;
            UpdateFields(productie);
        }

        public bool SaveChanges()
        {
            try
            {
                 var xverp = GetVerpakkingInstructie();
                if (xverp.CompareTo(VerpakkingInstructie)) return true;
                if (!string.IsNullOrEmpty(xverp?.ArtikelNr))
                {
                    _VerpakkingInstructie = xverp;
                    if (Manager.Verpakkingen is { Disposed: false })
                    {
                        Manager.Verpakkingen.SaveVerpakking(xverp);
                        if (Productie != null)
                        {
                            Productie.VerpakkingsInstructies = xverp;
                            return Productie.Update($"VerpakkingsInstructies aangepast voor [{Productie.ArtikelNr}]{Productie.Omschrijving}", true, true).Result;
                        }

                        
                        return true;
                    }
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            IsEditmode = false;
            UpdateFields(Productie);
        }
    }
}
