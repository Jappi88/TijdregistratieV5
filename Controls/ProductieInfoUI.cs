using ProductieManager.Properties;
using Rpm.Productie;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Navigation;
using iTextSharp.text.pdf;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace Controls
{
    public partial class ProductieInfoUI : UserControl
    {
        public IProductieBase Productie { get; private set; }
        public string Title { get; private set; }
        public Color HtmlBackColor { get; private set; }
        public Color BackColorGradient { get;private set; }
        public Color TextColor { get; private set; }
        public ProductieInfoUI()
        {
            InitializeComponent();
        }

        public void SetInfo(IProductieBase productie,string title, Color backColor, Color backGroundGradient, Color textColor)
        {
            if (productie == null) return;
            try
            {
                Productie = productie;
                Title = title;
                HtmlBackColor = backColor;
                BackColorGradient = backGroundGradient;
                TextColor = textColor;

                int curpos = 0;
                if (xHeaderHtmlPanel.Visible)
                {
                    //Header Html
                    curpos = xHeaderHtmlPanel.VerticalScroll.Value;
                    xHeaderHtmlPanel.Text = productie.GetHeaderHtmlBody(title,
                        productie.GetImageFromResources(),
                        new Size(64, 64), backColor, backGroundGradient, textColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xHeaderHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                }

                if (xInforHtmlPanel.Visible)
                {
                    //ProductieInfo
                    curpos = xInforHtmlPanel.VerticalScroll.Value;
                    xInforHtmlPanel.Text = productie.GetProductieInfoHtml("Productie Info",
                        backColor, backGroundGradient, textColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xInforHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                }

                if (xNotitieHtmlPanel.Visible)
                {

                    //Notities
                    curpos = xNotitieHtmlPanel.VerticalScroll.Value;
                    xNotitieHtmlPanel.Text = productie.GetNotitiesHtml("Notities",
                        backColor, backGroundGradient, textColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xNotitieHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                }

                if (xDatumsHtmlPanel.Visible)
                {
                    //ProductieDatums
                    curpos = xDatumsHtmlPanel.VerticalScroll.Value;
                    xDatumsHtmlPanel.Text = productie.GetDatumsHtml("Productie Datums",
                        backColor, backGroundGradient, textColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xDatumsHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                }

                if (xVerpakkingHtmlPanel.Visible)
                {
                    //VerpakkingsInstructies
                    curpos = xVerpakkingHtmlPanel.VerticalScroll.Value;
                    xVerpakkingHtmlPanel.Text = productie.GetVerpakkingHtmlText(null, "VerpakkingsInstructies",
                        backColor, backGroundGradient, textColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xVerpakkingHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                }

                if (xMaterialenHtmlPanel.Visible)
                {

                    //Materialen
                    curpos = xMaterialenHtmlPanel.VerticalScroll.Value;
                    xMaterialenHtmlPanel.Text = productie.GetMaterialenHtml("Materialen",
                        backColor, backGroundGradient, textColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xMaterialenHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                }

                if (xWerkPlaatsenHtmlPanel.Visible)
                {
                    //WerkPlaatsen
                    curpos = xWerkPlaatsenHtmlPanel.VerticalScroll.Value;
                    xWerkPlaatsenHtmlPanel.Text = productie.GetWerkplekkenHtml("Werk Plaatsen",
                        backColor, backGroundGradient, textColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xWerkPlaatsenHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xProductieInfoButton_Click(object sender, EventArgs e)
        {
            if (Productie == null) return;
            if (xInforHtmlPanel.Visible)
            {
                xInforHtmlPanel.Visible = false;
                xProductieInfoButton.Image = Resources.Navigate_down_36747;
            }
            else
            {
                xInforHtmlPanel.Text = Productie.GetProductieInfoHtml("Productie Info",
                    HtmlBackColor, BackColorGradient, TextColor, true);
                xInforHtmlPanel.Visible = true;
                xInforHtmlPanel.Height = 300;
                xProductieInfoButton.Image = Resources.Navigate_up_36744;
            }
        }

        private void xProductieDatumsButton_Click(object sender, EventArgs e)
        {
            if (Productie == null) return;
            if (xDatumsHtmlPanel.Visible)
            {
                xDatumsHtmlPanel.Visible = false;
                xProductieDatumsButton.Image = Resources.Navigate_down_36747;
            }
            else
            {
                xDatumsHtmlPanel.Text = Productie.GetDatumsHtml("Productie Datums",
                    HtmlBackColor, BackColorGradient, TextColor, true);
                xDatumsHtmlPanel.Visible = true;
                xDatumsHtmlPanel.Height = 225;
                xProductieDatumsButton.Image = Resources.Navigate_up_36744;
            }
        }

        private void xVerpakkingsButton_Click(object sender, EventArgs e)
        {
            if (Productie == null) return;
            if (xVerpakkingHtmlPanel.Visible)
            {
                xVerpakkingHtmlPanel.Visible = false;
                xVerpakkingsButton.Image = Resources.Navigate_down_36747;
            }
            else
            {
                xVerpakkingHtmlPanel.Text = Productie.GetVerpakkingHtmlText(null,"VerpakkingsInstructies",
                    HtmlBackColor, BackColorGradient, TextColor, true);
                xVerpakkingHtmlPanel.Visible = true;
                xVerpakkingHtmlPanel.Height = 350;
                xVerpakkingsButton.Image = Resources.Navigate_up_36744;
            }
        }

        private void xMaterialenButton_Click(object sender, EventArgs e)
        {
            if (Productie == null) return;
            if (xMaterialenHtmlPanel.Visible)
            {
                xMaterialenHtmlPanel.Visible = false;
                xMaterialenButton.Image = Resources.Navigate_down_36747;
            }
            else
            {
                xMaterialenHtmlPanel.Text = Productie.GetMaterialenHtml("Materialen",
                    HtmlBackColor, BackColorGradient, TextColor, true);
                xMaterialenHtmlPanel.Visible = true;
                xMaterialenHtmlPanel.Height = 350;
                xMaterialenButton.Image = Resources.Navigate_up_36744;
            }
        }

        private void xWerkPlaatsenButton_Click(object sender, EventArgs e)
        {
            if (Productie == null) return;
            if (xWerkPlaatsenHtmlPanel.Visible)
            {
                xWerkPlaatsenHtmlPanel.Visible = false;
                xWerkPlaatsenButton.Image = Resources.Navigate_down_36747;
            }
            else
            {
                xWerkPlaatsenHtmlPanel.Text = Productie.GetWerkplekkenHtml("Werk Plaatsen",
                    HtmlBackColor, BackColorGradient, TextColor, true);
                xWerkPlaatsenHtmlPanel.Visible = true;
                xWerkPlaatsenHtmlPanel.Height = 250;
                xWerkPlaatsenButton.Image = Resources.Navigate_up_36744;
            }
        }

        private void xNotitieButton_Click(object sender, EventArgs e)
        {
            if (Productie == null) return;
            if (xNotitieHtmlPanel.Visible)
            {
                xNotitieHtmlPanel.Visible = false;
                xNotitieButton.Image = Resources.Navigate_down_36747;
            }
            else
            {
                xNotitieHtmlPanel.Text = Productie.GetNotitiesHtml("Notities",
                    HtmlBackColor, BackColorGradient, TextColor, true);
                xNotitieHtmlPanel.Visible = true;
                xNotitieHtmlPanel.Height = 200;
                xNotitieButton.Image = Resources.Navigate_up_36744;
            }
        }

        private void xProductieStatusButton_Click(object sender, EventArgs e)
        {
            if (Productie == null) return;
            if (xHeaderHtmlPanel.Visible)
            {
                xHeaderHtmlPanel.Visible = false;
                xProductieStatusButton.Image = Resources.Navigate_down_36747;
            }
            else
            {
                xHeaderHtmlPanel.Text = Productie.GetHeaderHtmlBody(Title,
                    Productie.GetImageFromResources(),
                    new Size(64, 64), HtmlBackColor, BackColorGradient, TextColor, true);
                xHeaderHtmlPanel.Visible = true;
                xHeaderHtmlPanel.Height = 300;
                xProductieStatusButton.Image = Resources.Navigate_up_36744;
            }
        }

        private void xVerpakkingHtmlPanel_ImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            var xkey = e.Src;
            switch (xkey.ToLower().Trim())
            {
                case "verpakkingicon":
                    e.Callback(Resources.package_box_128x128);
                    break;
                case "productieinfoicon":
                    e.Callback(Resources.microsoft_info_22732);
                    break;
                case "datumsicon":
                    e.Callback(Resources.systemtime_778);
                    break;
                case "materialenicon":
                    e.Callback(Resources.bolts_construction_rivet_screw_screws_128x128);
                    break;
                case "notitiesicon":
                    e.Callback(Resources.memo_pad_notes_reminder_task_icon_128x128);
                    break;
                case "werkplaatsenicon":
                    e.Callback(Resources.iconfinder_technology);
                    break;
            }
        }

        public void ExpandPanel(bool expand, string panelname)
        {
            if (Productie == null) return;
            switch (panelname.ToLower().Trim())
            {
                case "productiestatus":
                    if (xHeaderHtmlPanel.Visible && expand) return;
                    xProductieStatusButton_Click(this, EventArgs.Empty);
                    break;
                case "verpakking":
                    if (xVerpakkingHtmlPanel.Visible && expand) return;
                    xVerpakkingsButton_Click(this, EventArgs.Empty);
                    break;
                case "productieinfo":
                    if (xInforHtmlPanel.Visible && expand) return;
                    xProductieInfoButton_Click(this, EventArgs.Empty);
                    break;
                case "datums":
                    if (xDatumsHtmlPanel.Visible && expand) return;
                    xProductieDatumsButton_Click(this, EventArgs.Empty);
                    break;
                case "materialen":
                    if (xMaterialenHtmlPanel.Visible && expand) return;
                    xMaterialenButton_Click(this, EventArgs.Empty);
                    break;
                case "notities":
                    if (xNotitieHtmlPanel.Visible && expand) return;
                    xNotitieButton_Click(this, EventArgs.Empty);
                    break;
                case "werkplaatsen":
                    if (xWerkPlaatsenHtmlPanel.Visible && expand) return;
                    xWerkPlaatsenButton_Click(this, EventArgs.Empty);
                    break;
            }
        }

        private void panel1_VisibleChanged(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                ExpandPanel(true, "productiestatus");
            }
        }
    }
}
