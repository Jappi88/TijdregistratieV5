using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Forms;
using Rpm.Misc;
using Rpm.Productie;

namespace ProductieManager.Forms.Aantal.Controls
{
    public partial class AantalChangerUI : UserControl
    {
        public IProductieBase Productie { get; private set; }
        public AantalChangerUI()
        {
            InitializeComponent();
        }

        #region Aantal Gemaakt

        public void LoadAantalGemaakt(IProductieBase productie)
        {
            
            if (productie is Bewerking bew)
                LoadWerkplekken(bew);
            else if (productie is ProductieFormulier form)
                ProductieLoadWerkplekken(form);
            Productie = productie;
            SetPacketAantal(productie.VerpakkingsInstructies, -1, productie.Aantal);
        }

        private void SetPacketAantal(VerpakkingInstructie instructie, int aantal, int totaalaantal)
        {

            if (instructie == null || instructie.VerpakkenPer == 0)
            {
                this.MinimumSize = new Size(xaantalgemaakt.MinimumSize.Width, 45);
                this.Height = 45;
                xPacketGroup.Visible = false;
            }
            else
            {
                xPacketGroup.Visible = true;
                this.MinimumSize = new Size(xaantalgemaakt.MinimumSize.Width, 100);
                this.Height = 100;
                bool changed = xPacketGroup.Tag is VerpakkingInstructie xold && !xold.Equals(instructie);
                if (aantal > -1 || changed)
                {
                    xPacketGroup.Tag = instructie;
                    if (aantal == -1)
                        aantal = GetAantal();
                    xpacketvalue.ValueChanged -= Xpacketvalue_ValueChanged;
                    int xpackets = aantal <= 0 ? 0 : (int)Math.Ceiling((double)aantal / instructie.VerpakkenPer);
                    xpacketvalue.SetValue(xpackets);
                    xpacketvalue.ValueChanged += Xpacketvalue_ValueChanged;
                }

                xpacketlabel.Text = xpacketvalue.Value.ToString(CultureInfo.InvariantCulture);
                xpacketlabel.Text += " / " +
                                     (totaalaantal <= 0
                                         ? 0
                                         : (int)Math.Ceiling((double)totaalaantal / instructie.VerpakkenPer))
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
            var xnewaantal = xaantalgemaakt.Value + xverp.VerpakkenPer;
            xaantalgemaakt.SetValue(xnewaantal);
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
            xwerkplekken.Tag = bewerking;
            if (bewerking != null && bewerking.WerkPlekken.Count > 0)
            {
                var wps = bewerking.WerkPlekken.Select(x => x.Naam).ToList();
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
            else UpdateAantalGemaakt(bewerking);
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
            var selected = xwerkplekken.SelectedItem?.ToString();
            var alles = selected?.ToLower() == "alle werkplekken";
            if (selected != null)
            {

                if (Productie is Bewerking Bewerking)
                {
                    if (!alles && Bewerking.WerkPlekken is { Count: > 0 })
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
                    if (!alles && Formulier.Bewerkingen is { Length: > 0 })
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

        private void SelectValue()
        {
            xaantalgemaakt.Select(0, xaantalgemaakt.Value.ToString(CultureInfo.InvariantCulture).Length);
            xaantalgemaakt.Focus();
        }

        public void UpdateAantalGemaakt(IProductieBase productie)
        {
            UpdateAantalStatusLabel(productie);
            if (xwerkplekken.SelectedItem != null)
            {
                var selected = xwerkplekken.SelectedItem.ToString();
                var alles = selected.ToLower() == "alle werkplekken";
                if (productie is Bewerking Bewerking)
                {
                    if (!alles && Bewerking.WerkPlekken is { Count: > 0 })
                    {
                        var wp = Bewerking.WerkPlekken.FirstOrDefault(t => string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));
                        if (wp != null && wp.AantalGemaakt != (int) (xaantalgemaakt.Tag ?? 0))
                        {

                            xaantalgemaakt.SetValue(wp.AantalGemaakt);
                            xaantalgemaakt.Tag = wp.AantalGemaakt;
                            SetPacketAantal(Bewerking.VerpakkingsInstructies, wp.AantalGemaakt, Bewerking.Aantal);
                            SelectValue();
                        }
                    }
                    else if (Productie == null || Bewerking.AantalGemaakt != (int)(xaantalgemaakt.Tag ?? 0))
                    {
                        xaantalgemaakt.SetValue(Bewerking.AantalGemaakt);
                        xaantalgemaakt.Tag = Bewerking.AantalGemaakt;
                        SetPacketAantal(Bewerking.VerpakkingsInstructies, Bewerking.AantalGemaakt, Bewerking.Aantal);
                        SelectValue();
                    }

                }
                else if (productie is ProductieFormulier Formulier)
                {
                    xaantalLabel.Text = $"{(Formulier.AantalGemaakt > 0 ? Formulier.AantalGemaakt.ToString("##.###") : "0")}/ " +
                                        $"{(Formulier.Aantal > 0 ? Formulier.Aantal.ToString("##.###") : "0")}";
                    if (!alles && Formulier.Bewerkingen is { Length: > 0 })
                    {
                        var b = Formulier.Bewerkingen.FirstOrDefault(t =>
                            string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));

                        if (b != null && b.AantalGemaakt != (int)(xaantalgemaakt.Tag ?? 0))
                        {
                            xaantalgemaakt.SetValue(b.AantalGemaakt);
                            xaantalgemaakt.Tag = b.AantalGemaakt;
                            SetPacketAantal(b.VerpakkingsInstructies, b.AantalGemaakt, b.Aantal);
                            SelectValue();
                        }
                    }
                    else if (Productie == null || Formulier.AantalGemaakt != (int)(xaantalgemaakt.Tag ?? 0))
                    {
                        xaantalgemaakt.SetValue(Formulier.AantalGemaakt);
                          xaantalgemaakt.Tag = Formulier.AantalGemaakt;
                        SetPacketAantal(Formulier.VerpakkingsInstructies, Formulier.AantalGemaakt, Formulier.Aantal);
                        SelectValue();
                    }
                }
            }
        }

        public void UpdateAantalStatusLabel(IProductieBase productie)
        {
            if (productie != null)
                xaantalLabel.Text =
                    $"{(productie.AantalGemaakt > 0 ? productie.AantalGemaakt.ToString("##.###") : "0")}/ " +
                    $"{(productie.Aantal > 0 ? productie.Aantal.ToString("##.###") : "0")}";
            else xaantalLabel.Text = "";
        }

        private void xwerkplekken_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xwerkplekken.Tag is IProductieBase prod)
                UpdateAantalGemaakt(prod);
        }

        private bool DoLogicalTest()
        {
            if (Productie == null) return false;
            if (Productie.AantalGemaakt == 0) return true;
            var xnewvalues = (double)xaantalgemaakt.Value;
            var actueel = (double)Productie.ActueelAantalGemaakt;
            double perDiff = actueel.GetPercentageDifference(xnewvalues);
            if (perDiff is < -50 or > 50)
            {
                var result = XMessageBox.Show(
                    $"Aantal van '{xnewvalues}' wijkt teveel af van {actueel}!\n\n" +
                    $"Dit is berekent op basis van de tempo en je zet er namelijk met {perDiff}% naast!\n\n" +
                    $"Als je echt zeker bent, wil je dan doorgaan met het wijzigen van de aantal?", "Hoge Afwijking", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (result != DialogResult.Yes) return false;
            }

            return true;
        }

        private async void Next(bool movenext)
        {
            if (xwerkplekken.SelectedItem != null)
            {
                if (!DoLogicalTest())
                {
                    xaantalgemaakt.SetValue((int) xaantalgemaakt.Tag);
                    return;
                }
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
                UpdateAantalStatusLabel(Productie);
                SelectValue();
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

        private void xaantalgemaakt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = e.Handled = true;
                Next(false);
            }
        }
        #endregion
    }
}
