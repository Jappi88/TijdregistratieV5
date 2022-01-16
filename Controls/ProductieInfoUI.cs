using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
                int index = -1;
                if (Productie is Bewerking bew && bew.Combies.Count > 0)
                {
                    metroTabControl1.TabPages[2].Text = $"Combinaties[{bew.Combies.Count}]";
                }
                else metroTabControl1.TabPages[2].Text = "Combineren";
                switch (metroTabControl1.SelectedIndex)
                {
                    case 0:
                        //Header Html
                        index = 0;
                        txt = Productie.GetHeaderHtmlBody(Title,
                            Productie.GetImageFromResources(),
                            new Size(64, 64), BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 1:
                        productieVerbruikUI1.InitFields((Productie as Bewerking)?.Parent);
                        break;
                    case 2:
                        combineerUI1.UpdateBewerking(Productie as Bewerking);
                        break;
                    case 3:
                        //ProductieInfo
                        index = 3;
                        txt = Productie.GetProductieInfoHtml("Productie Info",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 4:
                        //Notities
                        index = 4;
                        txt = Productie.GetNotitiesHtml("Notities",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 5:
                        //ProductieDatums
                        index = 5;
                        txt = Productie.GetDatumsHtml("Productie Datums",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 6:
                        verpakkingInstructieUI1.AllowEditMode = true;
                        if (!verpakkingInstructieUI1.IsEditmode)
                            verpakkingInstructieUI1.InitFields(Productie.VerpakkingsInstructies,
                                verpakkingInstructieUI1.IsEditmode,
                                "VerpakkingsInstructies", Color.White, Color.Black, Productie);
                        else verpakkingInstructieUI1.Productie = Productie;
                        break;
                    case 7:
                        //Materialen
                        index = 7;
                        txt = Productie.GetMaterialenHtml("Materialen",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 8:
                        //WerkPlaatsen
                        index = 8;
                        txt = Productie.GetWerkplekkenHtml("Werk Plaatsen",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 9:
                        //Aantal Geschiedenis
                        alleWerkPlekAantalHistoryUI1.UpdateBewerking(Productie as Bewerking);
                        break;
                }

                if (index > -1)
                {
                    var xpanel = metroTabControl1.TabPages[index].Controls.Find($"htmlpanel_{index}", false)
                        .FirstOrDefault() as HtmlPanel;
                    bool xinit = xpanel == null;
                    xpanel ??= new HtmlPanel()
                    {
                        IsContextMenuEnabled = false,
                        IsSelectionEnabled = false,
                        Name = $"htmlpanel_{index}"
                    };
                    if (xinit)
                    {
                        xpanel.ImageLoad += xVerpakkingHtmlPanel_ImageLoad;
                    }
                    xpanel.Dock = DockStyle.Fill;
                    if (!string.Equals(xpanel.Text, txt, StringComparison.CurrentCultureIgnoreCase))
                    {
                        metroTabControl1.TabPages[index].SuspendLayout();
                        metroTabControl1.TabPages[index].Controls.Remove(xpanel);
                        var curpos = xpanel.VerticalScroll.Value;
                        xpanel.Text = txt;
                        if (curpos > 0)
                        {
                            for (var i = 0; i < 5; i++)
                            {
                                xpanel.VerticalScroll.Value = curpos;
                            }
                        }
                        metroTabControl1.TabPages[index].Controls.Add(xpanel);
                        metroTabControl1.TabPages[index].ResumeLayout(true);
                        //panel.AutoScrollPosition.Offset(curpos);
                        //panel.Invalidate();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CopyControl(Control sourceControl, Control targetControl)
        {
            // make sure these are the same
            if (sourceControl.GetType() != targetControl.GetType())
            {
                throw new Exception("Incorrect control types");
            }

            foreach (PropertyInfo sourceProperty in sourceControl.GetType().GetProperties())
            {
                object newValue = sourceProperty.GetValue(sourceControl, null);

                MethodInfo mi = sourceProperty.GetSetMethod(true);
                if (mi != null)
                {
                    sourceProperty.SetValue(targetControl, newValue, null);
                }
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
