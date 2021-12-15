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
            metroTabControl1.SelectedIndex = 0;
        }

        public void UpdateView()
        {
            if (Productie == null) return;
            try
            {
                int curpos = 0;
                if (metroTabControl1.SelectedIndex == 0)
                {
                    //Header Html
                    curpos = xHeaderHtmlPanel.VerticalScroll.Value;
                    xHeaderHtmlPanel.Text = Productie.GetHeaderHtmlBody(Title,
                        Productie.GetImageFromResources(),
                        new Size(64, 64), BackColor, BackColorGradient, TextColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xHeaderHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                    return;
                }

                if (metroTabControl1.SelectedIndex == 1)
                {
                    //ProductieInfo
                    curpos = xInforHtmlPanel.VerticalScroll.Value;
                    xInforHtmlPanel.Text = Productie.GetProductieInfoHtml("Productie Info",
                        BackColor, BackColorGradient, TextColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xInforHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                    return;
                }

                if (metroTabControl1.SelectedIndex == 2)
                {

                    //Notities
                    curpos = xNotitieHtmlPanel.VerticalScroll.Value;
                    xNotitieHtmlPanel.Text = Productie.GetNotitiesHtml("Notities",
                        BackColor, BackColorGradient, TextColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xNotitieHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                    return;
                }

                if (metroTabControl1.SelectedIndex == 3)
                {
                    //ProductieDatums
                    curpos = xDatumsHtmlPanel.VerticalScroll.Value;
                    xDatumsHtmlPanel.Text = Productie.GetDatumsHtml("Productie Datums",
                        BackColor, BackColorGradient, TextColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xDatumsHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                    return;
                }

                if (metroTabControl1.SelectedIndex == 4)
                {
                    //VerpakkingsInstructies
                    curpos = xVerpakkingHtmlPanel.VerticalScroll.Value;
                    xVerpakkingHtmlPanel.Text = Productie.GetVerpakkingHtmlText(null, "VerpakkingsInstructies",
                        BackColor, BackColorGradient, TextColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xVerpakkingHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }
                    return;
                }

                if (metroTabControl1.SelectedIndex == 5)
                {

                    //Materialen
                    curpos = xMaterialenHtmlPanel.VerticalScroll.Value;
                    xMaterialenHtmlPanel.Text = Productie.GetMaterialenHtml("Materialen",
                        BackColor, BackColorGradient, TextColor, true);

                    for (var i = 0; i < 3; i++)
                    {
                        xMaterialenHtmlPanel.VerticalScroll.Value = curpos;
                        Application.DoEvents();
                    }

                    return;
                }

                if (metroTabControl1.SelectedIndex == 6)
                {
                    //WerkPlaatsen
                    curpos = xWerkPlaatsenHtmlPanel.VerticalScroll.Value;
                    xWerkPlaatsenHtmlPanel.Text = Productie.GetWerkplekkenHtml("Werk Plaatsen",
                        BackColor, BackColorGradient, TextColor, true);

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
                UpdateView();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateView();
        }
    }
}
