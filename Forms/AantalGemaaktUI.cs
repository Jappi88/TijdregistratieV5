using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Forms;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms
{
    public partial class AantalGemaaktUI : MetroForm
    {
        public AantalGemaaktUI()
        {
            InitializeComponent();
        }

        public int Aantal { get; set; }
        public Bewerking Bewerking { get; set; }
        public ProductieFormulier Formulier { get; set; }

        public DialogResult ShowDialog(ProductieFormulier parent, Bewerking bew, WerkPlek plek = null)
        {
            Bewerking = bew;
            Formulier = parent;
            LoadWerkplekken(bew);
            if (xwerkplekken.Items.Count > 0)
            {
                if (plek != null)
                    xwerkplekken.SelectedItem = plek.Naam;
                if (xwerkplekken.SelectedItem == null)
                    xwerkplekken.SelectedIndex = 0;
            }
            else xaantalgemaakt.SetValue(bew.AantalGemaakt);
            xaantal.Text = bew.AantalTeMaken.ToString();
            xaantalgemaaktlabel.Text = bew.AantalGemaakt.ToString();
            Text = $"[{bew.ArtikelNr}][{bew.ProductieNr}] {bew.Omschrijving}";
            LoadBewerkingText(Bewerking);
            return ShowDialog();
        }

        public DialogResult ShowDialog(ProductieFormulier form)
        {
            Formulier = form;
            ProductieLoadWerkplekken(form);
            if (xwerkplekken.Items.Count > 0)
                xwerkplekken.SelectedIndex = 0;
            else xaantalgemaakt.SetValue(form.AantalGemaakt);
            xaantal.Text = form.AantalTeMaken.ToString();
            Text = $"[{form.ArtikelNr}][{form.ProductieNr}] {form.Omschrijving}";
            LoadProductieText(Formulier);
            return ShowDialog();
        }

        private void LoadProductieText(ProductieFormulier form)
        {
            productieInfoUI1.SetInfo(form, "Wijzig Aantal Gemaakt", Color.AliceBlue, Color.White, Color.Black);
            //var xcurpos = xwerkinfopanel.VerticalScroll.Value;
            //xwerkinfopanel.Text = form.GetHtmlBody("Wijzig Aantal Gemaakt", null, new Size(32, 32), Color.AliceBlue,
            //    Color.White, Color.Black);//ProductieText(form);
            //for (int i = 0; i < 3; i++)
            //{
            //    xwerkinfopanel.VerticalScroll.Value = xcurpos;
            //    Application.DoEvents();
            //}
            // xfieldinfo.Text = DefaultFieldText();
            //xpersooninfo.Text = ProductiewWerkPlekken(form);
        }

        private void LoadBewerkingText(Bewerking bew)
        {
            productieInfoUI1.SetInfo(bew, "Wijzig Aantal Gemaakt", Color.AliceBlue, Color.White, Color.Black);
            //var xcurpos = xwerkinfopanel.VerticalScroll.Value;
            //xwerkinfopanel.Text = bew.GetHtmlBody("Wijzig Aantal Gemaakt", null, new Size(32, 32), Color.AliceBlue,
            //    Color.White, Color.Black);
            //for (int i = 0; i < 3; i++)
            //{
            //    xwerkinfopanel.VerticalScroll.Value = xcurpos;
            //    Application.DoEvents();
            //}
            //xwerkinfo.Text = BewerkingText(bew);
            //// xfieldinfo.Text = DefaultFieldText();
            //xpersooninfo.Text = BewerkingPersoneelText(bew);
        }

        private void LoadWerkplekken(Bewerking bewerking)
        {
            xwerkplekken.Items.Clear();
            if (bewerking != null && bewerking.WerkPlekken.Count > 0)
            {
                foreach (var w in bewerking.WerkPlekken)
                    if (w.Werk != null && bewerking.Equals(w.Werk))
                        xwerkplekken.Items.Add(w.Naam);
            }
            else
            {
                if (bewerking != null) xwerkplekken.Items.Add(bewerking.Naam);
            }

            if (xwerkplekken.Items.Count > 1)
                xwerkplekken.Items.Add("Alle Werkplekken");
        }

        private void ProductieLoadWerkplekken(ProductieFormulier formulier)
        {
            xwerkplekken.Items.Clear();
            if (formulier?.Bewerkingen != null)
            {
                var bws = formulier.Bewerkingen.Where(x => x.IsAllowed()).ToArray();
                if (bws.Length > 0)
                    xwerkplekken.Items.AddRange(bws.Select(x => (object) x.Naam).ToArray());
                else xwerkplekken.Items.Add($"'{formulier.ArtikelNr} | {formulier.ProductieNr}'");
            }
        }

        private string ProductieText(ProductieFormulier form)
        {
            var value = $"Controlleer '{form.Omschrijving}' op kwaliteit en aantal.\n\n";
            var tijd = form.TijdAanGewerkt();
            var peruur = tijd > 0 && form.TotaalGemaakt > 0 ? Math.Round(form.TotaalGemaakt / tijd, 0) : 0;
            value += $"Aantal Gemaakt: {form.AantalGemaakt} van de {form.AantalTeMaken}\n" +
                     $"Actueel Per Uur: {peruur} i.p.v. {form.PerUur}";//"\n\n" + DefaultFieldText();
            return value;
        }

        private string ProductiewWerkPlekken(ProductieFormulier form)
        {
            var value = "";
            if (form is {Bewerkingen: {Length: > 0}})
                foreach (var bew in form.Bewerkingen)
                {
                    value += $"[{bew.Naam}]\n";
                    value += BewerkingPersoneelText(bew);
                    value += "\n";
                }

            return value;
        }

        private string BewerkingText(Bewerking bew)
        {
            var value = $"Controlleer {bew.Omschrijving} ({bew.Naam}) op kwaliteit en aantal.\n\n";
            var tijd = bew.TijdAanGewerkt();
            var peruur = tijd > 0 && bew.TotaalGemaakt > 0 ? Math.Round(bew.TotaalGemaakt / tijd, 0) : 0;
            value += $"Aantal Gemaakt: {bew.AantalGemaakt} van de {bew.AantalTeMaken}\n" +
                     $"Actueel Per Uur: {peruur} i.p.v. {bew.PerUur}";//"\n\n" + DefaultFieldText();
            return value;
        }

        private string BewerkingPersoneelText(Bewerking bew)
        {
            var werkplek = "";
            if (bew.WerkPlekken is {Count: > 0})
            {
                string.Join(", ",
                    bew.WerkPlekken.SelectMany(x =>
                        x.Personen.Select(x => x.PersoneelNaam + (x.Actief ? "" : "[Niet Actief]"))));
                foreach (var werk in bew.WerkPlekken)
                {
                    var personen = string.Join(", ",
                        werk.Personen.Select(x => x.PersoneelNaam + (x.Actief ? "" : "[Niet Actief]")));

                    var xvalue = werk.Personen.Count == 1 ? "heeft" : "hebben";
                    if (personen.Length > 0)
                        werkplek +=
                            $"{personen} {xvalue} op  '{werk.Naam}' {werk.AantalGemaakt} gemaakt in {werk.TijdAanGewerkt()} uur\n";
                }

                werkplek = werkplek.TrimStart(',', ' ');
            }

            return werkplek;
        }

        private string DefaultFieldText()
        {
            return "LET OP!!\n" +
                   "* Contolleer het product goed volgens de tekening.\n" +
                   "* Maak steekproeven op producten die al zijn verpakt.\n" +
                   "* Controlleer of de juiste stikkers en verpakking wordt gebruikt.\n" +
                   "* Controlleer of de aantallen kloppen.\n\n" +
                   "Vul het aantal in die je hebt gecontrolleerd, en druk op 'OK'\n";
        }

        private void xopslaan_Click(object sender, EventArgs e)
        {
            var xok = true;
            if (Bewerking != null && xaantalgemaakt.Value == Bewerking.AantalGemaakt)
            {
                xok = false;
                XMessageBox.Show(
                    "Aantal dat je invult is hetzelfde als de oude aantal... en is daarom niet nodig.\nAnnuleer of vul in een aantal dat groter is dan de oude.",
                    "Onzin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (Bewerking != null && Bewerking.AantalGemaakt > xaantalgemaakt.Value)
            {
                xok = XMessageBox.Show("Aantal dat je invult is kleiner dan de oude aantal...\n" +
                                       "Kan zijn dat je je vergist had en daarom wil corrigeren, \nWeetje zeker dat je door wilt gaan?",
                    "Aantal Gemaakt", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            }
        }

        private void xannuleer_Click(object sender, EventArgs e)
        {
            Next();
            DialogResult = DialogResult.OK;
        }

        private void AantalUI_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            xaantalgemaakt.Select();
            xaantalgemaakt.Focus();
            xaantalgemaakt.Select(0, xaantalgemaakt.Value.ToString().Length);
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            var prodnr = Bewerking?.ProductieNr ?? Formulier?.ProductieNr;
            if (changedform == null || !string.Equals(changedform.ProductieNr, prodnr)) return;
            if (Bewerking != null)
            {
                var xbew = changedform.Bewerkingen.FirstOrDefault(x => x.Equals(Bewerking));
                if (xbew != null)
                    Bewerking = xbew;
            }
            else
            {
                Formulier = changedform;
            }
        }

        private void Aantal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter) Next();
        }

        private void xwerkplekken_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xwerkplekken.SelectedItem != null)
            {
                var selected = xwerkplekken.SelectedItem.ToString();
                var alles = selected.ToLower() == "alle werkplekken";
                if (Bewerking != null)
                {
                    if (alles)
                    {
                        xaantalgemaakt.SetValue(Bewerking.AantalGemaakt);
                    }
                    else if (Bewerking.WerkPlekken is {Count: > 0})
                    {
                        var wp = Bewerking.WerkPlekken.FirstOrDefault(t => string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));
                        if (wp != null) xaantalgemaakt.SetValue(wp.AantalGemaakt);
                    }
                    else
                    {
                        xaantalgemaakt.SetValue(Bewerking.AantalGemaakt);
                    }

                    xaantalgemaaktlabel.Text = Bewerking.AantalGemaakt.ToString();
                }
                else if (Formulier != null)
                {
                    if (alles)
                    {
                        xaantalgemaakt.SetValue(Formulier.AantalGemaakt);
                    }
                    else if (Formulier.Bewerkingen is {Length: > 0})
                    {
                        var b = Bewerking ?? Formulier.Bewerkingen.FirstOrDefault(t =>
                            string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));

                        if (b != null)
                        {
                            xaantalgemaakt.SetValue(b.AantalGemaakt);
                            xaantalgemaaktlabel.Text = b.AantalGemaakt.ToString();
                        }
                    }
                    else
                    {
                        xaantalgemaakt.SetValue(Formulier.AantalGemaakt);
                        xaantalgemaaktlabel.Text = Formulier.AantalGemaakt.ToString();
                    }
                   
                }

                xaantalgemaakt.Select(0, xaantalgemaakt.Value.ToString().Length);
                xaantalgemaakt.Focus();
            }
        }

        private async void Next()
        {
            if (xwerkplekken.SelectedItem != null)
            {
                var selected = xwerkplekken.SelectedItem.ToString();
                var alles = selected.ToLower() == "alle werkplekken";
                var changed = false;
                if (Bewerking != null)
                {
                    var skip = false;
                    var change = "Aantal Gemaakt Aangepast";
                    if (alles && Bewerking.AantalGemaakt != (int) xaantalgemaakt.Value)
                    {
                        Bewerking.AantalGemaakt = (int) xaantalgemaakt.Value;
                        changed = true;
                    }

                    if (!changed && Bewerking.WerkPlekken is {Count: > 0})
                    {
                        var wp = Bewerking.WerkPlekken.FirstOrDefault(t => string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));
                        if (wp != null)
                            if (wp.AantalGemaakt != (int) xaantalgemaakt.Value)
                            {
                                change =
                                    $"[{wp.Path}] Aantal aangepast van {wp.AantalGemaakt} naar {xaantalgemaakt.Value}";
                                wp.AantalGemaakt = (int) xaantalgemaakt.Value;
                                wp.LaatstAantalUpdate = DateTime.Now;
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
                        Bewerking.AantalGemaakt = (int) xaantalgemaakt.Value;
                        Bewerking.LaatstAantalUpdate = DateTime.Now;
                        changed = true;
                    }

                    xaantalgemaaktlabel.Text = Bewerking.AantalGemaakt.ToString();
                    if (changed)
                        await Bewerking.UpdateBewerking(null, change);
                    LoadBewerkingText(Bewerking);
                }
                else if (Formulier != null)
                {
                    if (alles)
                    {
                        if (Formulier.AantalGemaakt != (int) xaantalgemaakt.Value)
                        {
                            var change =
                                $"[{Formulier.ProductieNr}] Aantal aangepast van {Formulier.AantalGemaakt} naar {xaantalgemaakt.Value}";
                            Formulier.AantalGemaakt = (int) xaantalgemaakt.Value;
                            Formulier.LaatstAantalUpdate = DateTime.Now;
                            xaantalgemaaktlabel.Text = Formulier.AantalGemaakt.ToString();
                            LoadProductieText(Formulier);
                            await Formulier.UpdateForm(false, false, null, change);
                        }
                    }
                    else if (Formulier.Bewerkingen is {Length: > 0})
                    {
                        var b = Bewerking ??
                                Formulier.Bewerkingen.FirstOrDefault(t => string.Equals(t.Naam, selected, StringComparison.CurrentCultureIgnoreCase));

                        if (b != null && b.AantalGemaakt != (int) xaantalgemaakt.Value)
                        {
                            var change =
                                $"[{b.Path}] Aantal aangepast van {b.AantalGemaakt} naar {xaantalgemaakt.Value}";
                            b.AantalGemaakt = (int) xaantalgemaakt.Value;
                            b.LaatstAantalUpdate = DateTime.Now;
                            LoadProductieText(Formulier);
                            await b.UpdateBewerking(null, change);

                            xaantalgemaaktlabel.Text = b.AantalGemaakt.ToString();
                        }
                    }
                }

                if (xwerkplekken.Items.Count > 0)
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
            Next();
        }

        private void AantalGemaaktUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
        }
    }
}