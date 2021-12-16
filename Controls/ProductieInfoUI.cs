using ProductieManager.Properties;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Navigation;
using iTextSharp.text.pdf;
using NPOI.HSSF.Record;
using Rpm.Misc;
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
        public bool ShowAantal
        {
            get => xaantalpanel.Visible;
            set => xaantalpanel.Visible = value;
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
                    if (Productie is Bewerking bew)
                        LoadWerkplekken(bew);
                    else if (Productie is ProductieFormulier form)
                        ProductieLoadWerkplekken(form);
                }

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
                       // Application.DoEvents();
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
                        //Application.DoEvents();
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
                       // Application.DoEvents();
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
                        //Application.DoEvents();
                    }
                    return;
                }

                if (metroTabControl1.SelectedIndex == 4)
                {
                    verpakkingInstructieUI1.InitFields(Productie.VerpakkingsInstructies, false,
                        "VerpakkingsInstructies", Color.White, Color.Black);
                    ////VerpakkingsInstructies
                    //curpos = xVerpakkingHtmlPanel.VerticalScroll.Value;
                    //xVerpakkingHtmlPanel.Text = Productie.GetVerpakkingHtmlText(null, "VerpakkingsInstructies",
                    //    BackColor, BackColorGradient, TextColor, true);

                    //for (var i = 0; i < 3; i++)
                    //{
                    //    xVerpakkingHtmlPanel.VerticalScroll.Value = curpos;
                    //    ///Application.DoEvents();
                    //}
                    //return;
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
                        //Application.DoEvents();
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
                        //Application.DoEvents();
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
                SetPacketAantal(productie.VerpakkingsInstructies, -1, Productie.Aantal);
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

        #region Aantal Gemaakt

        private void SetPacketAantal(VerpakkingInstructie instructie, int aantal, int totaalaantal)
        {
            if (instructie == null || instructie.VerpakkenPer == 0)
            {
                xaantalpanel.Height = 45;
                xPacketGroup.Visible = false;
            }
            else
            {
                xPacketGroup.Visible = true;
                xaantalpanel.Height = 100;
                if (aantal > -1)
                {
                    xpacketvalue.ValueChanged -= Xpacketvalue_ValueChanged;
                    int xpackets = aantal <= 0 ? 0 : (int) Math.Ceiling((double) aantal / instructie.VerpakkenPer);
                    xpacketvalue.SetValue(xpackets);
                    xpacketvalue.ValueChanged += Xpacketvalue_ValueChanged;
                }

                xpacketlabel.Text = xpacketvalue.Value.ToString(CultureInfo.InvariantCulture);
                xpacketlabel.Text += " / " +
                                     (totaalaantal <= 0
                                         ? 0
                                         : (int) Math.Ceiling((double) totaalaantal / instructie.VerpakkenPer))
                                     .ToString();

                var x0 = "Verpakken Per {0}";
                var x1 = string.IsNullOrEmpty(instructie.VerpakkingType)
                    ? x0
                    : $"{x0} in {instructie.VerpakkingType}";
                xPacketGroup.Text = string.Format(x1, instructie.VerpakkenPer);
            }
        }

        private void Xpacketvalue_ValueChanged(object sender, EventArgs e)
        {
            var xverp = Productie?.VerpakkingsInstructies;
            if (xverp == null || xverp.VerpakkenPer == 0) return;
            var aantal = xaantalgemaakt.Value;
            int xpackets = aantal <= 0 ? 0 : (int)Math.Ceiling((double)aantal / xverp.VerpakkenPer);
            if (xpacketvalue.Value != xpackets)
            {
                xaantalgemaakt.Value = xpacketvalue.Value * xverp.VerpakkenPer;
                Next(false);
            }
        }

        private void AddPacket()
        {
            var xverp = Productie?.VerpakkingsInstructies;
            if (xverp == null || xverp.VerpakkenPer == 0) return;

            xaantalgemaakt.SetValue(xaantalgemaakt.Value + xverp.VerpakkenPer);
            Next(false);
        }

        private void RemovePacket()
        {
            var xverp = Productie?.VerpakkingsInstructies;
            if (xverp == null || xverp.VerpakkenPer == 0) return;

            xaantalgemaakt.SetValue(xaantalgemaakt.Value - xverp.VerpakkenPer);
            Next(false);
        }

        private void LoadWerkplekken(Bewerking bewerking)
        {
            if (bewerking != null && bewerking.WerkPlekken.Count > 0)
            {
                var wps = bewerking.WerkPlekken.Select(x=> x.Naam).ToList();
                var xwps = xwerkplekken.Items.Cast<string>().Where(w =>
                    !string.Equals(w, "alle werkplekken", StringComparison.CurrentCultureIgnoreCase)).ToList();
                IEnumerable<string> inFirstOnly = wps.Except(xwps);
                IEnumerable<string> inSecondOnly = xwps.Except(wps);
                bool allInBoth = !inFirstOnly.Any() && !inSecondOnly.Any();
                bool thesame = xwps.Count == wps.Count && allInBoth;
                if (!thesame)
                {
                    xwerkplekken.Items.Clear();
                    foreach (var w in bewerking.WerkPlekken)
                        if (w.Werk != null && bewerking.Equals(w.Werk))
                            xwerkplekken.Items.Add(w.Naam);
                    if (xwerkplekken.Items.Count > 1)
                        xwerkplekken.Items.Add("Alle Werkplekken");
                }
            }
            else
            {
                xwerkplekken.Items.Clear();
                if (bewerking != null) xwerkplekken.Items.Add(bewerking.Naam);
            }

            if (xwerkplekken.Items.Count > 0 && xwerkplekken.SelectedIndex == -1)
            {
                xwerkplekken.SelectedItem = "Alle Werkplekken";
                if (xwerkplekken.SelectedItem == null)
                    xwerkplekken.SelectedIndex = 0;
            }
            else UpdateAantalGemaakt();
        }

        private void ProductieLoadWerkplekken(ProductieFormulier formulier)
        {
            xwerkplekken.Items.Clear();
            if (formulier?.Bewerkingen != null)
            {
                var bws = formulier.Bewerkingen.Where(x => x.IsAllowed()).ToArray();
                List<string> xitems = new List<string>();
                if (bws.Length > 0)
                    xitems.AddRange(bws.Select(x => x.Naam));
                else xitems.Add($"'{formulier.ArtikelNr} | {formulier.ProductieNr}'");
                if (xitems.Count != xwerkplekken.Items.Count ||
                    xitems.Any(x =>
                        !xwerkplekken.Items.Cast<string>().Any(s =>
                            string.Equals(s, x, StringComparison.CurrentCultureIgnoreCase))) ||
                    xwerkplekken.Items.Cast<string>().Any(s =>
                        !xitems.Any(x =>
                            string.Equals(s, x, StringComparison.CurrentCultureIgnoreCase))))
                {
                    xwerkplekken.Items.Clear();
                    foreach (var w in xitems)
                        xwerkplekken.Items.Add(w);
                    if (xwerkplekken.Items.Count > 1)
                        xwerkplekken.Items.Add("Alle Werkplekken");
                }
            }
        }

        private int GetAantal()
        {
            if (!ShowAantal) return 0;
            var selected = xwerkplekken.SelectedItem?.ToString();
            var alles = selected?.ToLower() == "alle werkplekken";
            if (selected != null)
            {

                if (Productie is Bewerking Bewerking)
                {
                    if (!alles && Bewerking.WerkPlekken is {Count: > 0})
                    {
                        var wp = Bewerking.WerkPlekken.FirstOrDefault(t =>
                            string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));
                        if (wp != null)
                        {
                            return wp.AantalGemaakt;
                        }
                    }
                    else
                    {
                        return Bewerking.AantalGemaakt;
                    }
                }
                else if (Productie is ProductieFormulier Formulier)
                {
                    if (!alles && Formulier.Bewerkingen is {Length: > 0})
                    {
                        var b = Formulier.Bewerkingen.FirstOrDefault(t =>
                            string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));

                        if (b != null)
                        {
                            return b.AantalGemaakt;
                        }
                    }
                    else
                    {
                        return Formulier.AantalGemaakt;
                    }

                }
            }

            return 0;
        }

        private void UpdateAantalGemaakt()
        {
            if (!ShowAantal) return;
            if (xwerkplekken.SelectedItem != null)
            {
                var selected = xwerkplekken.SelectedItem.ToString();
                var alles = selected.ToLower() == "alle werkplekken";
                if (Productie is Bewerking Bewerking)
                {
                    if (!alles && Bewerking.WerkPlekken is { Count: > 0 })
                    {
                        var wp = Bewerking.WerkPlekken.FirstOrDefault(t => string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));
                        if (wp != null && wp.AantalGemaakt != xaantalgemaakt.Value)
                        {
                            xaantalgemaakt.SetValue(wp.AantalGemaakt);
                            SetPacketAantal(Bewerking.VerpakkingsInstructies, wp.AantalGemaakt, Bewerking.Aantal);
                        }
                        else return;
                    }
                    else if (Bewerking.AantalGemaakt != xaantalgemaakt.Value)
                    {
                        xaantalgemaakt.SetValue(Bewerking.AantalGemaakt);
                        SetPacketAantal(Bewerking.VerpakkingsInstructies, Bewerking.AantalGemaakt, Bewerking.Aantal);
                    }
                    else return;

                }
                else if (Productie is ProductieFormulier Formulier)
                {
                    if (!alles && Formulier.Bewerkingen is { Length: > 0 })
                    {
                        var b = Formulier.Bewerkingen.FirstOrDefault(t =>
                            string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));

                        if (b != null && b.AantalGemaakt != xaantalgemaakt.Value)
                        {
                            xaantalgemaakt.SetValue(b.AantalGemaakt);
                            SetPacketAantal(b.VerpakkingsInstructies, b.AantalGemaakt, b.Aantal);
                        }
                        else return;
                    }
                    else if (Formulier.AantalGemaakt != xaantalgemaakt.Value)
                    {
                        xaantalgemaakt.SetValue(Formulier.AantalGemaakt);
                        SetPacketAantal(Formulier.VerpakkingsInstructies, Formulier.AantalGemaakt, Formulier.Aantal);
                    }
                    else return;
                }

                xaantalgemaakt.Select(0, xaantalgemaakt.Value.ToString(CultureInfo.InvariantCulture).Length);
                xaantalgemaakt.Focus();
            }
        }

        private void xwerkplekken_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAantalGemaakt();
        }

        private async void Next(bool movenext)
        {
            if (xwerkplekken.SelectedItem != null)
            {
                var selected = xwerkplekken.SelectedItem.ToString();
                var alles = selected.ToLower() == "alle werkplekken";
                var changed = false;
                if (Productie is Bewerking Bewerking)
                {
                    var skip = false;
                    var change = $"Aantal gemaakt aangepast naar {xaantalgemaakt.Value}";
                    if (alles && Bewerking.AantalGemaakt != (int)xaantalgemaakt.Value)
                    {
                        Bewerking.AantalGemaakt = (int)xaantalgemaakt.Value;
                        SetPacketAantal(Bewerking.VerpakkingsInstructies, Bewerking.AantalGemaakt, Bewerking.Aantal);
                        changed = true;
                    }

                    if (!changed && Bewerking.WerkPlekken is { Count: > 0 })
                    {
                        var wp = Bewerking.WerkPlekken.FirstOrDefault(t => string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));
                        if (wp != null)
                            if (wp.AantalGemaakt != (int)xaantalgemaakt.Value)
                            {
                                change =
                                    $"[{wp.Path}] Aantal aangepast van {wp.AantalGemaakt} naar {xaantalgemaakt.Value}";
                                wp.AantalGemaakt = (int)xaantalgemaakt.Value;
                                wp.LaatstAantalUpdate = DateTime.Now;
                                SetPacketAantal(Bewerking.VerpakkingsInstructies, wp.AantalGemaakt, Bewerking.Aantal);
                                changed = true;
                            }
                            else
                            {
                                skip = true;
                            }
                    }


                    if (!changed && !skip && Bewerking.AantalGemaakt != xaantalgemaakt.Value)
                    {
                        change =
                            $"[{Bewerking.Path}] Aantal aangepast van {Bewerking.AantalGemaakt} naar {xaantalgemaakt.Value}";
                        Bewerking.AantalGemaakt = (int)xaantalgemaakt.Value;
                        Bewerking.LaatstAantalUpdate = DateTime.Now;
                        SetPacketAantal(Bewerking.VerpakkingsInstructies, Bewerking.AantalGemaakt, Bewerking.Aantal);
                        changed = true;
                    }
                    
                    if (changed)
                        await Bewerking.UpdateBewerking(null, change);
                }
                else if (Productie is ProductieFormulier Formulier)
                {
                    if (alles)
                    {
                        if (Formulier.AantalGemaakt != (int)xaantalgemaakt.Value)
                        {
                            var change =
                                $"[{Formulier.ProductieNr}] Aantal aangepast van {Formulier.AantalGemaakt} naar {xaantalgemaakt.Value}";
                            Formulier.AantalGemaakt = (int)xaantalgemaakt.Value;
                            Formulier.LaatstAantalUpdate = DateTime.Now;
                            SetPacketAantal(Formulier.VerpakkingsInstructies, Formulier.AantalGemaakt, Formulier.Aantal);
                            await Formulier.UpdateForm(false, false, null, change);
                        }
                    }
                    else if (Formulier.Bewerkingen is { Length: > 0 })
                    {
                        var b = Formulier.Bewerkingen.FirstOrDefault(t => string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));

                        if (b != null && b.AantalGemaakt != (int)xaantalgemaakt.Value)
                        {
                            var change =
                                $"[{b.Path}] Aantal aangepast van {b.AantalGemaakt} naar {xaantalgemaakt.Value}";
                            b.AantalGemaakt = (int)xaantalgemaakt.Value;
                            b.LaatstAantalUpdate = DateTime.Now;
                            SetPacketAantal(b.VerpakkingsInstructies, b.AantalGemaakt, b.Aantal);
                            await b.UpdateBewerking(null, change);
                        }
                    }
                }

                if (xwerkplekken.Items.Count > 0 && movenext)
                {
                    if (xwerkplekken.SelectedIndex + 1 < xwerkplekken.Items.Count)
                        xwerkplekken.SelectedIndex++;
                    else xwerkplekken.SelectedIndex = 0;
                }

                xaantalgemaakt.Select(0, xaantalgemaakt.Value.ToString().Length);
                xaantalgemaakt.Focus();
            }
        }

        private void xnextb_Click(object sender, EventArgs e)
        {
            Next(false);
        }

        private void xremovePacket_Click(object sender, EventArgs e)
        {
            RemovePacket();
        }

        private void xaddPacket_Click(object sender, EventArgs e)
        {
            AddPacket();
        }
        #endregion

        private void xaantalgemaakt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = e.Handled = true;
                Next(false);
            }
        }
    }
}
