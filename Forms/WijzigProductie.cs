using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Forms
{
    public partial class WijzigProductie : MetroFramework.Forms.MetroForm
    {
        private readonly bool editmode;

        private changetype _change;
        internal string[] prods = { };

        public WijzigProductie()
        {
            InitializeComponent();
            Formulier = new ProductieFormulier();
            xartnr.Enabled = true;
            xprodnr.Enabled = true;
            ((OLVColumn) xbewerkinglijst.Columns[0]).ImageGetter = sender => 0;
        }

        public WijzigProductie(ProductieFormulier form)
        {
            InitializeComponent();
            Formulier = form;
            editmode = true;
            xartnr.Enabled = false;
            xprodnr.Enabled = false;
            ((OLVColumn) xbewerkinglijst.Columns[0]).ImageGetter = sender => 0;
        }

        public ProductieFormulier Formulier { get; private set; }

        private void Form_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            var thesame = Formulier.Equals(changedform);
            if (!thesame) return;
            if (changedform.State == ProductieState.Verwijderd)
                DialogResult = DialogResult.Cancel;
            else
                Formulier = changedform.CreateCopy();
        }

        public async void SetTextboxAutofill()
        {
            var sourceName = new AutoCompleteStringCollection();
            prods = (await Manager.Database.GetAllProducties(true,true)).Select(x => x.ProductieNr).ToArray();
            sourceName.AddRange(prods);
            xartnr.AutoCompleteCustomSource = sourceName;
            xartnr.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            xartnr.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private async void AddBewerkingen(ProductieFormulier form)
        {
            var p = form;
            if (p.Bewerkingen?.Length > 0)
            {
                xbewerkingen.Text = p.Bewerkingen[0].Naam;
                foreach (var b in p.Bewerkingen)
                    await b.UpdateBewerking(null, "", false);
                xbewerkinglijst.SetObjects(p.Bewerkingen);
                if (p.Bewerkingen.Length <= 0) return;
                xbewerkinglijst.SelectedObject = xbewerkingen.Tag ?? p.Bewerkingen[0];
                xbewerkinglijst.SelectedItem?.EnsureVisible();
            }
        }

        private Bewerking[] GetBewerkingen()
        {
            var bws = new List<Bewerking>();
            if (xbewerkinglijst.Objects != null && xbewerkinglijst.Items.Count > 0)
            {
                var xitems = xbewerkinglijst.Objects.Cast<Bewerking>().ToArray();
                foreach (var item in xitems)
                {
                    item.Parent = Formulier;
                    bws.Add(item);
                }
            }

            return bws.ToArray();
        }

        public bool SetFormInfo()
        {
            var haveform = Formulier != null;
            if (!haveform)
                Formulier = new ProductieFormulier();

            Formulier.ArtikelNr = xartnr.Text;
            Formulier.ProductieNr = xprodnr.Text;
            Formulier.Aantal = (int) xaantal.Value;
            Formulier.Omschrijving =
                xbeschrijving.Text.Trim().Length == 0 ? "Geen Product Omschrijving" : xbeschrijving.Text;
            Formulier.Note = new NotitieEntry(xnotitie.Text.Trim(), Formulier);
            Formulier.LeverDatum = xdatumgereed.Value;
            Formulier.DatumGereed = xprodgereedop.Value;
            Formulier.GereedNote = new NotitieEntry(xgereednotitie.Text.Trim(), Formulier){Type = NotitieType.ProductieGereed};
            Formulier.AantalGemaakt = (int) xprodaantalgemaakt.Value;
            Formulier.TijdGewerkt = (int) xprodtijdgewerkt.Value;
            Formulier.Paraaf = xprodparaaf.Text;
            materiaalUI1.SaveMaterials();
            return true;
        }

        public void SetFormFields(ProductieFormulier p)
        {
            if (p == null)
                return;
            materiaalUI1.InitMaterialen(p);
            AddBewerkingen(p);
            Text = $"Productie : [{Formulier.ProductieNr}]-----[{Formulier.Omschrijving}]";
            xartnr.Text = p.ArtikelNr;
            xprodnr.Text = p.ProductieNr;
            xaantal.SetValue(p.Aantal);
            xbeschrijving.Text = p.Omschrijving;
            xnotitie.Text = p.Note?.Notitie;
            xgereednotitie.Text = p.GereedNote?.Notitie;
            xprodgereedop.SetValue(p.DatumGereed);
            xprodparaaf.Text = p.Paraaf;
            xprodaantalgemaakt.SetValue(p.AantalGemaakt);
            xprodtijdgewerkt.SetValue((decimal) p.TijdGewerkt);
            var rooster = Manager.Opties.GetWerkRooster();
            if (p.LeverDatum < xdatumgereed.MinDate || p.LeverDatum > xdatumgereed.MaxDate)
                p.LeverDatum = Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster,rooster,null);
            xdatumgereed.SetValue(p.LeverDatum);

            UpdateDatum();
        }

        private void SetBewFields(Bewerking b)
        {
            if (b != null)
            {
                xbewerkingen.Tag = b;
                xbewerkingen.SelectedItem = b.Naam.Split('[')[0];
                xbewleverdatum.SetValue(b.LeverDatum);
                xbewgereedop.SetValue(b.DatumGereed);
                xbewgereed.Text = b.Paraaf;
                xbewstatus.SelectedItem = Enum.GetName(typeof(ProductieState), b.State);
                xbewtijdgewerkt.SetValue((decimal) b.TijdGewerkt);
                xbewnotitie.Text = b.Note?.Notitie;
                xbewperuur.Text = Math.Round(b.Aantal / b.DoorloopTijd, 1).ToString("0 Per Uur");
                xbewalgemaakt.SetValue(b.AantalGemaakt);
                xbewdoorlooptijd.SetValue((decimal) b.DoorloopTijd);
            }
            else
            {
                xbewerkingen.Tag = null;
            }
        }

        private void xaantal_ValueChanged(object sender, EventArgs e)
        {
            if (_change != changetype.bewperuur)
            {
                _change = changetype.aantal;
                if (xaantal.Value > 0 && xbewdoorlooptijd.Value > 0)
                    xbewperuur.Text = Math.Round(xaantal.Value / xbewdoorlooptijd.Value, 1).ToString("0 Per Uur");
            }

            _change = changetype.none;
            UpdateDatum();
        }

        private void xbewdoorlooptijd_ValueChanged(object sender, EventArgs e)
        {
            if (_change != changetype.bewperuur)
            {
                _change = changetype.doorlooptijd;
                if (xaantal.Value > 0 && xbewdoorlooptijd.Value > 0)
                    xbewperuur.Text = Math.Round(xaantal.Value / xbewdoorlooptijd.Value, 1).ToString("0 Per Uur");
            }

            _change = changetype.none;
            UpdateDatum();
        }

        private void xaantalgemaakt_ValueChanged(object sender, EventArgs e)
        {
            _change = changetype.aantalgemaakt;
            UpdateDatum();
        }

        private void UpdateDatum()
        {
            var bws = GetBewerkingen();
            var gedaan = bws.Length == 0 ? 0 : bws.Sum(t => t.AantalGemaakt) / bws.Length;
            var doorloop = bws.Length == 0
                ? (double) (xaantal.Value / 100)
                : bws.Sum(t => t.DoorloopTijd);
            xperuur.Text = $"{Formulier.ProductenPerUur((int) xaantal.Value, doorloop).ToString()} Per Uur";
            xgereedprocent.Text = Formulier.GereedPercentage((double) xaantal.Value, gedaan) + "% Gereed";
            xverwachtgereed.Text = "Gereed op " + Formulier
                .VerwachtDatumGereed(DateTime.Now, doorloop, (int) xaantal.Value, gedaan, bws)
                .ToString("dddd d MMMM yyyy HH:mm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void InitForm(ProductieFormulier p)
        {
            Text = $"Productie : [{p.ProductieNr}]-----[{p.Omschrijving}]";
            //SetTextboxAutofill();
            var bewerkingen = Manager.BewerkingenLijst.GetAllEntries().Select(x => (object) x.Naam).ToArray();
            xbewerkingen.Items.AddRange(bewerkingen);
            SetFormFields(p);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xartnr.Text))
            {
                XMessageBox.Show("Vul een geldige artikel nummer in a.u.b", "ArtikelNr", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(xprodnr.Text))
            {
                XMessageBox.Show("Vul een geldige productie nummer in a.u.b", "ProductieNr", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else if (!editmode && prods.Any(t => string.Equals(t, xprodnr.Text, StringComparison.CurrentCultureIgnoreCase)))
            {
                XMessageBox.Show(
                    $"Productie {xprodnr.Text} bestaat al en kan daarom niet nogmaals gebruikt worden!\nProbeer een andere nummer a.u.b",
                    "ProductieNr", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (xaantal.Value <= 0)
            {
                XMessageBox.Show("Aantal moet meer zijn dan 0!\n Vul eerst een geldige aantal in a.u.b", "Aantal",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (await Save())
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void xpersoneel_Click(object sender, EventArgs e)
        {
            switch (xbewerkinglijst.SelectedObject)
            {
                case null:
                    return;
                case Bewerking b:
                {
                    var x = new Indeling(Formulier, b) {StartPosition = FormStartPosition.CenterParent};
                    if (x.ShowDialog() == DialogResult.OK)
                    {
                        xbewerkinglijst.RefreshObject(b);
                        UpdateDatum();
                    }

                    break;
                }
            }
        }

        private async Task<bool> UpdateSelectedItem()
        {
            if (xbewerkingen.Tag == null)
                return false;
            if (xbewerkingen.Text.Trim().Replace(" ", "").Length < 5)
            {
                XMessageBox.Show("Vul eerst een geldige bewerking in voordat je verder gaat", "Ongeldige Bewerking",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!(xbewerkingen.Tag is Bewerking b)) return false;
            var count = 0;
            if ((count += xbewerkinglijst.Objects.Cast<Bewerking>().Sum(x =>
                string.Equals(x.Naam, xbewerkingen.Text.Trim().Split('[')[0],
                    StringComparison.CurrentCultureIgnoreCase)
                    ? 1
                    : 0)) > 1)
            {
                b.Naam = xbewerkingen.SelectedItem + $"[{count}]";
            }
            else
            {
                b.Naam = xbewerkingen.Text;
            }

            b.Parent = Formulier;
            b.DoorloopTijd = (double) xbewdoorlooptijd.Value;
            b.AantalGemaakt = (int) xbewalgemaakt.Value;
            b.LeverDatum = xbewleverdatum.Value;
            b.DatumGereed = xbewgereedop.Value;
            b.Paraaf = xbewgereed.Text;
            b.TijdGewerkt = (double) xbewtijdgewerkt.Value;
            b.State = (ProductieState) Enum.Parse(typeof(ProductieState), xbewstatus.SelectedItem.ToString());
            b.Note = new NotitieEntry(xbewnotitie.Text, b);
            await b.UpdateBewerking(null, "Bewerking Update", false);
            xbewerkinglijst.RefreshObject(b);
            xbewerkinglijst.SelectedObject = b;
            xbewerkinglijst.SelectedItem?.EnsureVisible();
            return true;
        }

        private async Task<bool> Save()
        {
            SetFormInfo();
            Formulier.Bewerkingen = GetBewerkingen();
            await Formulier.UpdateForm(true, false, null, $"[{Formulier.ProductieNr}] Bewerkingen Gewijzigd");
            return true;
        }

        private async void xpasbewaan_Click(object sender, EventArgs e)
        {
            //update bewerking;
            //if (await UpdateSelectedItem())
            //    XMessageBox.Show("Wijzingen zijn doorgevoerd!", "Opgeslagen", MessageBoxButtons.OK,
            //        MessageBoxIcon.Information);
            await UpdateSelectedItem();
            UpdateDatum();
        }

        private async void xvoegbewtoe_Click(object sender, EventArgs e)
        {
            string naam = null;
            var count = 0;
            if (xbewerkingen.SelectedItem == null || xbewerkingen.Text.Trim().Length < 5 ||
                xbewerkingen.Text.Trim() == "")
            {
                XMessageBox.Show("Vul eerst een geldige bewerking in voordat je verder gaat", "Ongeldige Bewerking",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (xbewerkinglijst.Objects != null && (count += xbewerkinglijst.Objects.Cast<Bewerking>()
                    .Sum(x => x.Naam.ToLower().Split('[')[0] == xbewerkingen.Text.ToLower() ? 1 : 0)) >
                0) naam = xbewerkingen.SelectedItem + $"[{count}]";
            var bw = await new Bewerking((double) xbewdoorlooptijd.Value).CreateNewInstance(Formulier);
            bw.Naam = naam ?? xbewerkingen.SelectedItem.ToString();
            bw.DatumToegevoegd = DateTime.Now;
            xbewerkinglijst.AddObject(bw);
        }

        private void AddProduction_FormClosed(object sender, FormClosedEventArgs e)
        {
            Manager.OnFormulierChanged -= Form_OnFormulierChanged;
        }

        private void WijzigProductie_Shown(object sender, EventArgs e)
        {
            InitForm(Formulier);
            Manager.OnFormulierChanged += Form_OnFormulierChanged;
            xaantal.ValueChanged += xaantal_ValueChanged;
            xbewalgemaakt.ValueChanged += xaantalgemaakt_ValueChanged;
            xbewdoorlooptijd.ValueChanged += xbewdoorlooptijd_ValueChanged;
        }

        private void xverwijderbewerking_Click(object sender, EventArgs e)
        {
            var count = 0;
            if ((count = xbewerkinglijst.SelectedObjects.Count) == 0)
                return;

            var xvalue = count == 1 ? "bewerking" : "bewerkingen";
            if (XMessageBox.Show(
                $"Je staat op het punt {count} {xvalue} te verwijderen.\n\nWeet je zeker dat je door wilt gaan?\nClick 'Nee' om te annuleren.",
                "Bewerking Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                xbewerkinglijst.RemoveObjects(xbewerkinglijst.SelectedObjects);
        }

        private void xbewerkinglijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xbewerkinglijst.SelectedObject != null)
            {
                var b = xbewerkinglijst.SelectedObject as Bewerking;
                SetBewFields(b);
            }
            else
            {
                xbewerkingen.Tag = null;
            }

            xpasbewaan.Enabled = xbewerkinglijst.SelectedObject != null;
            xverwijderbewerking.Enabled = xbewerkinglijst.SelectedObjects.Count > 0;
            xwerkplekken.Enabled = xbewerkinglijst.SelectedObject != null;
            xbeheeronderbrekeningen.Enabled = xbewerkinglijst.SelectedObject != null;
        }

        private void xbeheeronderbrekeningen_Click(object sender, EventArgs e)
        {
            if (xbewerkinglijst.SelectedObject == null)
                return;
            if (xbewerkinglijst.SelectedObject is Bewerking b)
            {
                var st = new StoringForm(b.Path, b.WerkPlekken);
                st.ShowDialog();
            }
        }

        private async void xbewopslaan_Click(object sender, EventArgs e)
        {
            try
            {
                if (await UpdateSelectedItem())
                {
                    UpdateDatum();
                    await Save();
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(exception.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private enum changetype
        {
            doorlooptijd,
            bewperuur,
            peruur,
            aantal,
            aantalgemaakt,
            none
        }
    }
}