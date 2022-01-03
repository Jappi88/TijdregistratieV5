using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Controls
{
    public partial class ProductieInfoUI : UserControl
    {
        public IProductieBase Productie { get; private set; }
        public string Title { get; private set; }
        public Color HtmlBackColor { get; private set; }
        public Color BackColorGradient { get;private set; }
        public Color TextColor { get; private set; }
        public bool ShowAantal
        {
            get => aantalChangerUI1.Visible;
            set => aantalChangerUI1.Visible = value;
        }

        public bool AllowVerpakkingEdit
        {
            get => verpakkingInstructieUI1.AllowEditMode;
            set => verpakkingInstructieUI1.AllowEditMode = value;
        }

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
                if (ShowAantal)
                {
                    aantalChangerUI1.Visible = true;
                   aantalChangerUI1.LoadAantalGemaakt(Productie);
                }
                string txt = String.Empty;
                HtmlPanel panel = null;
                if (Productie is Bewerking bew && bew.Combies.Count > 0)
                {
                    metroTabControl1.TabPages[1].Text = $"Combinaties[{bew.Combies.Count}]";
                }
                else metroTabControl1.TabPages[1].Text = "Combineren";
                switch (metroTabControl1.SelectedIndex)
                {
                    case 0:
                        //Header Html
                        panel = xHeaderHtmlPanel;
                        txt = Productie.GetHeaderHtmlBody(Title,
                            Productie.GetImageFromResources(),
                            new Size(64, 64), BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 1:
                        combineerUI1.UpdateBewerking(Productie as Bewerking);
                        break;
                    case 2:
                        //ProductieInfo
                        panel = xInforHtmlPanel;
                        txt = Productie.GetProductieInfoHtml("Productie Info",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 3:
                        //Notities
                        panel = xNotitieHtmlPanel;
                        txt = Productie.GetNotitiesHtml("Notities",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 4:
                        //ProductieDatums
                        panel = xDatumsHtmlPanel;
                        txt = Productie.GetDatumsHtml("Productie Datums",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 5:
                        verpakkingInstructieUI1.AllowEditMode = true;
                        if (!verpakkingInstructieUI1.IsEditmode)
                            verpakkingInstructieUI1.InitFields(Productie.VerpakkingsInstructies,
                                verpakkingInstructieUI1.IsEditmode,
                                "VerpakkingsInstructies", Color.White, Color.Black, Productie);
                        else verpakkingInstructieUI1.Productie = Productie;
                        break;
                    case 6:
                        //Materialen
                        panel = xMaterialenHtmlPanel;
                        txt = Productie.GetMaterialenHtml("Materialen",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 7:
                        //WerkPlaatsen
                        panel = xWerkPlaatsenHtmlPanel;
                        txt = Productie.GetWerkplekkenHtml("Werk Plaatsen",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 8:
                        //Aantal Geschiedenis
                        alleWerkPlekAantalHistoryUI1.UpdateBewerking(Productie as Bewerking);
                        break;
                }

                if (panel != null)
                {
                    int curpos = panel.VerticalScroll.Value;
                    panel.Text = txt;
                    for (var i = 0; i < 3; i++)
                    {
                        panel.VerticalScroll.Value = curpos;
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
                if (InvokeRequired)
                    this.BeginInvoke(new MethodInvoker(UpdateView));
                else
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
