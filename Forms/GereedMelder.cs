using ProductieManager.Forms;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Properties;

namespace Forms
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
        public string Notitie { get; set; }

        public DialogResult ShowDialog(ProductieFormulier form)
        {
            _prod = form.CreateCopy();
            SetString();
            xparaaf.Text = form.Paraaf;
            xaantal.Value = form.AantalGemaakt;
            xdeelsgereed.Visible = false;
            xgereedlijst.Visible = false;
            xaantal.ValueChanged += xaantal_ValueChanged;
            return ShowDialog();
        }

        public DialogResult ShowDialog(Bewerking bewerking)
        {
            _prod = bewerking.CreateCopy();
            SetString();
            xparaaf.Text = bewerking.Paraaf;
            xaantal.Value = bewerking.AantalGemaakt;
            xdeelsgereed.Visible = true;
            xgereedlijst.Visible = true;
            xaantal.ValueChanged += xaantal_ValueChanged;
            return ShowDialog();
        }

        private async void MeldGereed()
        {
            if (DoCheck() && XMessageBox.Show($"{xtextfield1.Text}...\n\n" +
                                              "Klopt dit allemaal hoe er is geproduceerd?",
                "Gereed Melden",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(_prod.TotaalGemaakt < _prod.Aantal)
                {
                    var xt = new GereedNotitieForm();
                    if (xt.ShowDialog(_prod) == DialogResult.Cancel) return;
                    Notitie = xt.Reden;
                }
                if (_prod is ProductieFormulier form)
                {
                    Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
                    await form.MeldGereed((int)xaantal.Value, xparaaf.Text.Trim(), Notitie,true);
                    DialogResult = DialogResult.OK;
                }
                else if (_prod is Bewerking bew)
                {
                    Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
                    await bew.MeldBewerkingGereed(xparaaf.Text.Trim(), (int) xaantal.Value, Notitie, true,true);
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

            if (storingen.Length > 0)
            {
                var xk1 = storingen.Length == 1 ? "staat" : "staan";
                var xk2 = storingen.Length == 1 ? "onderbreking" : "onderbrekeningen";
                string xmsg = $"Er {xk1} nog {storingen.Length} {xk2} open!\n\n" +
                              $"Je kan geen productie gereedmelden als er een onderbreking openstaat!\n" +
                              $"Wil je nu de openstaande {xk2} wijzigen?";
                if (XMessageBox.Show(xmsg,
                    $"Openstaande {xk2}", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                    return false;
                var allst = new AlleStoringen();
                allst.InitStoringen(_prod.Root);
                allst.ShowDialog();
                if (_prod.Root.GetAlleStoringen(true).Count > 0)
                    return false;
            }

            return true;
        }

        private void SetString()
        {
            if (_prod == null)
                return;
            string xfieldinfo =
                $"Je staat op het punt {_prod.Naam} gereed te melden met {_prod.TotaalGemaakt} stuk(s) van de {_prod.Aantal}.";
            if (_prod.TotaalGemaakt > _prod.Aantal)
                xfieldinfo = $"Je staat op het punt {_prod.Naam} gereed te melden met {_prod.TotaalGemaakt - _prod.Aantal} stuk(s) extra!";
            var bericht =
                $"{xfieldinfo}\n\n" +
                $"* Totaal {_prod.TijdGewerkt} / {_prod.DoorloopTijd} uur aan gewerkt met {_prod.ActueelPerUur} i.p.v. {_prod.PerUur} per uur.\n\n" +
                $"* Er is {_prod.TotaalGemaakt} van de {_prod.Aantal} gemaakt.\n\n" +
                $"* Er is {_prod.DeelsGereed} deels gereed gemeld.";
            xtextfield1.Text = bericht;
            xtextfield2.Text = _prod.Omschrijving;
            this.Text = $"Meld Gereed [{_prod.ProductieNr} | {_prod.ArtikelNr}]";
            this.Invalidate();
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

        private void GereedMelder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter) MeldGereed();
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