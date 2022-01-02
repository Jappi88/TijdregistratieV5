using ProductieManager.Forms;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace Forms.GereedMelden
{
    public partial class GereedMelder : MetroFramework.Forms.MetroForm
    {
        public readonly string Melding = "Controlleer eerst alle gegevens voordat je verder gaat!\n\n" +
                                         "*Kijk en tel de producten goed na.\n" +
                                         "*Controlleer op de juiste verpakking instructies en stevigheid voor transport.\n" +
                                         "*Controlleer de pallet of het sterk genoeg is om het gewicht te dragen.\n" +
                                         "*Controlleer alle stikkers of het overeenkomt met de productiebon en het product.\n" +
                                         "*Kijk eerst na of de Productiebon goed is ingevuld.\n\n" +
                                         "" +
                                         "Als alles goed is nagelopen en in orde is, dan kan je verder gaan met gereedmelden.";

        private IProductieBase _prod;

        public string ParentCombi { get; set; }

        public GereedMelder()
        {
            InitializeComponent();
        }

        public int Aantal
        {
            get => (int) xaantal.Value;
            set => xaantal.Value = value;
        }

        public string Paraaf
        {
            get => xparaaf.Text;
            set => xparaaf.Text = value;
        }

        public string Naam { get; set; }
        public string Notitie { get; set; } = string.Empty;

        public DialogResult ShowDialog(ProductieFormulier form)
        {
            _prod = form.CreateCopy();
            xparaaf.Text = form.Paraaf;
            xaantal.Value = form.AantalGemaakt;
            xdeelsgereed.Visible = false;
            xgereedlijst.Visible = false;
            xaantal.ValueChanged += xaantal_ValueChanged;
            this.Text = @$"Meld Gereed [{_prod.ProductieNr} | {_prod.ArtikelNr}]";
            this.Invalidate();
            productieInfoUI1.SetInfo(_prod, this.Text, Color.White,
                Color.White, Color.DarkGreen);
            return ShowDialog();
        }

        public DialogResult ShowDialog(Bewerking bewerking)
        {
            _prod = bewerking.CreateCopy();
            xparaaf.Text = bewerking.Paraaf;
            xaantal.Value = bewerking.AantalGemaakt;
            xdeelsgereed.Visible = true;
            xgereedlijst.Visible = true;
            xaantal.ValueChanged += xaantal_ValueChanged;
            this.Text = @$"Meld Gereed [{_prod.ProductieNr} | {_prod.ArtikelNr}]";
            this.Invalidate();
            productieInfoUI1.SetInfo(_prod, this.Text, Color.White,
                Color.White, Color.DarkGreen);
            return ShowDialog();
        }

        private async void MeldGereed()
        {
            if (DoCheck() && XMessageBox.Show(Melding,
                "Gereed Melden",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _prod.AantalGemaakt = (int)xaantal.Value;
                if (_prod.TotaalGemaakt < _prod.Aantal)
                {
                    var xt = new GereedNotitieForm();
                    if (xt.ShowDialog(_prod) == DialogResult.Cancel) return;
                    Notitie = xt.Reden;
                }

                
                var afwijking = _prod.GetAfwijking();
                if (afwijking is < -10 or > 10)
                {
                    var xt = new GereedAfwijkingForm();
                    if (xt.ShowDialog(_prod) == DialogResult.Cancel) return;
                    if (string.IsNullOrEmpty(Notitie))
                        Notitie = xt.Reden;
                    else Notitie += $"\n\nDe reden voor een te hoge 'PerUur' afwijking van '{afwijking}%':\n\n" + xt.Reden?.Trim().Split(':').LastOrDefault()?.Trim();
                }
                if (_prod is ProductieFormulier form)
                {
                    Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
                    await form.MeldGereed((int)xaantal.Value, xparaaf.Text.Trim(), Notitie,true,true);
                    DialogResult = DialogResult.OK;
                }
                else if (_prod is Bewerking bew)
                {
                    var xcombies = bew.Combies.Where(x =>
                        !string.Equals(x.Path, ParentCombi, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (xcombies.Count > 0)
                    {
                        var x0 = xcombies.Count == 1 ? "is" : "zijn";
                        var x1 = xcombies.Count == 1 ? "productie" : "producties";
                        var result = XMessageBox.Show($"Er {x0} {xcombies.Count} gecombineerde {x1}...\n\n" +
                                                      $"Wil je die ook gereedmelden?", "Gereed Melden",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Cancel) return;
                        for(int i =0; i < xcombies.Count; i++)
                        {
                            var combi = xcombies[i];
                            var xbw = Werk.FromPath(combi.Path)?.Bewerking;
                            if (xbw == null)
                            {
                                continue;
                            }

                            if (result == DialogResult.No)
                                _=bew.StopProductie(true);
                            else
                            {
                                var gereedmelder = new GereedMelder();
                                gereedmelder.ParentCombi = bew.Path;
                                gereedmelder.ShowDialog(xbw);
                            }
                        }
                    }

                    Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
                    await bew.MeldBewerkingGereed(xparaaf.Text.Trim(), (int) xaantal.Value, Notitie, true,true,true);
                    ProductieManager.Properties.Settings.Default.Paraaf = xparaaf.Text.Trim();
                    ProductieManager.Properties.Settings.Default.Save();
                    DialogResult = DialogResult.OK;
                }
                else DialogResult = DialogResult.Cancel;
            }
        }

        private bool DoCheck()
        {
            _prod.AantalGemaakt = (int)xaantal.Value;
            if ((_prod.TotaalGemaakt) <= 0 && xaantal.Value == 0)
            {
                XMessageBox.Show("Je aantal kan niet 0 zijn!\nJe kan minstins maar 1 product gereedmelden",
                    "Ongeldig Aantal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (string.IsNullOrEmpty(xparaaf.Text) || string.IsNullOrWhiteSpace(xparaaf.Text))
            {
                XMessageBox.Show("Paraaf kan niet leeg zijn!\nVul eerst een geldige paraaf in en probeer het opnieuw.",
                    "Ongeldig Naam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            Storing[] storingen = _prod.Root.GetAlleStoringen(true).ToArray();

            if ( (storingen.Length > 0 || _prod.TijdGewerkt <= 0 || _prod.GetPersonen(true).Length == 0))
            {
                var xopenform = new OpenTakenForm(_prod);
                if (xopenform.ShowDialog() != DialogResult.OK)
                    return false;
            }

            return true;
        }

        private void SetString()
        {
            if (_prod == null)
                return;
            if (this.IsDisposed || this.Disposing) return;
            this.Text = @$"Meld Gereed [{_prod.ProductieNr} | {_prod.ArtikelNr}]";
            this.Invalidate();
            productieInfoUI1.SetInfo(_prod, this.Text, Color.White,
                Color.White, Color.DarkGreen);
            //this.BeginInvoke(new Action( () =>
            //{
               
                

            //}));
            // xtextfield3.Text = Melding;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MeldGereed();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void GereedMelder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                MeldGereed();
            }
        }

        private void xnotitie_Click(object sender, EventArgs e)
        {
            var xtxtform = new TextFieldEditor
            {
                Title = $"Gereed notitie voor [{_prod.ProductieNr}, {_prod.ArtikelNr}] {_prod.Naam}",
                MultiLine = true,
                SelectedText = _prod.GereedNote?.Notitie
            };
            if (xtxtform.ShowDialog() == DialogResult.OK)
            {
                Notitie = xtxtform.SelectedText;
                _prod.GereedNote = new NotitieEntry(Notitie, _prod);
            }
        }

        private void GereedMelder_Shown(object sender, System.EventArgs e)
        {
            SetString();
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            xparaaf.Text = ProductieManager.Properties.Settings.Default.Paraaf;
            xparaaf.Select();
        }

        private void GereedMelder_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            try
            {
                if (this.Disposing || this.IsDisposed || changedform == null || _prod == null ||
                    !string.Equals(_prod.ProductieNr, changedform.ProductieNr, StringComparison.CurrentCultureIgnoreCase))
                    return;
                if (_prod is Bewerking bew)
                {
                    var xbw = changedform.Bewerkingen.FirstOrDefault(x =>
                        string.Equals(x.Naam, bew.Naam, StringComparison.CurrentCultureIgnoreCase));
                    if (xbw != null)
                        _prod = xbw.CreateCopy();
                }
                else
                    _prod = changedform.CreateCopy();
                _prod.GereedNote = new NotitieEntry(Notitie, _prod);
                if (_prod.AantalGemaakt != (int)xaantal.Value)
                    _prod.AantalGemaakt = (int)xaantal.Value;
                SetString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xaantal_ValueChanged(object sender, EventArgs e)
        {
            _prod.AantalGemaakt = (int)xaantal.Value;
            SetString();
        }

        private async void xgereedlijst_Click(object sender, EventArgs e)
        {
            if (_prod is Bewerking bew)
            {
                var xform = new DeelsGereedMeldingenForm(bew);
                if (xform.ShowDialog() == DialogResult.OK)
                {
                    bew = xform.Bewerking;
                    _prod = xform.Bewerking;
                    await bew.UpdateBewerking(null, $"[{bew.Path}] Deels gereedmeldingen aangepast");
                    xaantal.Value = bew.AantalGemaakt;
                    SetString();
                }
            }
        }

        private async void xdeelsgereed_Click(object sender, EventArgs e)
        {
            if (_prod is Bewerking bew && DoCheck())
            {
                int aantal = (int)xaantal.Value;
                if (aantal == 0)
                {
                    XMessageBox.Show("Je kan niet 0 gereedmelden!\nAantal moet minimaal 1 zijn.",
                        "Ongeldige Aantal",
                        MessageBoxIcon.Exclamation);
                    return;
                }
                int totaal = aantal + bew.DeelGereedMeldingen.Sum(x=> x.Aantal);
                string xvar = aantal == 1 ? "stuk" : "stuks";
                string xmsg = $"Je wilt {aantal} {xvar} deels gereedmelden.\n\n";
                if (totaal > bew.Aantal)
                {
                    string xmsgval = totaal == bew.Aantal ? "precies" : "meer dan";
                   var xmsg1 = $"{xmsg}Als je {aantal} gereedmeld heb je totaal {totaal} van de {bew.Aantal} gemaakt.\n" +
                            $"Dat is {xmsgval} wat je nodig hebt, wil je anders de productie helemaal gereed melden?";
                   var bttns = new Dictionary<string, DialogResult>();
                   bttns.Add("Annuleren", DialogResult.Cancel);
                   bttns.Add("Gereedmelden", DialogResult.Yes);
                   bttns.Add("Deels Gereedmelden", DialogResult.OK);
                   var xresult = XMessageBox.Show(xmsg1, "Gereed Melden", MessageBoxButtons.YesNoCancel,
                       MessageBoxIcon.Question, null, bttns);
                   if (xresult == DialogResult.Cancel) return;
                   if (xresult == DialogResult.Yes)
                   {
                       MeldGereed();
                       return;
                   }

                }
                else
                {
                    int temaken = bew.Aantal >= totaal ? bew.Aantal - totaal : 0;
                    xmsg += "Weetje zeker dat je dat wilt doen?\n" +
                            $"Je zal dan totaal {totaal} stuks deels hebben gereed gemeld, en {temaken} om nog te maken\n";
                    if (XMessageBox.Show(xmsg, "Gereed Melden", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.No) return;
                }
                var wps = bew.WerkPlekken.Select(x => x.Naam).ToList();
                string wp = null;
                if (wps.Count > 1)
                {
                    wps.Add("Alle Werkplekken");
                    WerkPlekChooser wpChooser = new WerkPlekChooser(wps.ToArray(),null);
                    if (wpChooser.ShowDialog() == DialogResult.Cancel) return;
                    wp = wpChooser.SelectedName?.ToLower() == "alle werkplekken" ? null : wpChooser.SelectedName;
                }
                else wp = wps.FirstOrDefault();

                await bew.MeldDeelsGereed(xparaaf.Text.Trim(), aantal, Notitie,wp, DateTime.Now, true);
                xaantal.SetValue(bew.AantalGemaakt);
                SetString();
            }
        }

        private void xafkeur_Click(object sender, EventArgs e)
        {
            if (_prod == null) return;
            ProductieFormulier prod = null;
            if (_prod is Bewerking bew)
                prod = bew.Parent;
            else if (_prod is ProductieFormulier xprod)
                prod = xprod;
            if (prod == null) return;
            var xafk = new AfkeurForm(prod);
            xafk.ShowDialog();
        }
    }
}