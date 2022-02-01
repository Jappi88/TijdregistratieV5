using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Forms
{
    public partial class NieuwKlusForm : MetroFramework.Forms.MetroForm
    {
        private readonly bool _save;

        private Klus _origklus;
        private string _werkplek;
        private Personeel[] OrigPersoon;
        public Klus SelectedKlus;
        public bool EditMode { get; private set; }

        public NieuwKlusForm(ProductieFormulier formulier, Personeel pers, bool save,bool editmode, Bewerking bew = null, string werkplek = null)
        {
            InitializeComponent();
            EditMode = editmode;
            _save = save; 
            _origklus = pers.CurrentKlus() ?? new Klus(pers, bew,werkplek);
            SelectedKlus = _origklus.CreateCopy();
            InitFields(formulier, new[] {pers}, bew, werkplek);
        }

        public NieuwKlusForm(ProductieFormulier formulier, Personeel[] pers, bool save, bool editmode, Bewerking bew = null,
            string werkplek = null)
        {
            InitializeComponent();
            EditMode = editmode;
            _save = save;
            var xpers = pers.FirstOrDefault();
            _origklus = xpers?.CurrentKlus() ?? new Klus(xpers, bew, werkplek);
            SelectedKlus = _origklus.CreateCopy();
            InitFields(formulier, pers, bew, werkplek);
        }

        public NieuwKlusForm(Personeel pers, Klus klus,bool editmode)
        {
            InitializeComponent();
            EditMode = editmode;
            _save = true;
            _origklus = klus;
            SelectedKlus = klus.CreateCopy();
            var pair = klus.GetWerk();
            var prod = pair.Formulier;
            var bew = pair.Bewerking;
            if (!pair.IsValid)
                throw new Exception("Ongeldige klus!\nProductie is niet meer beschikbaar.");
            InitFields(prod, new[] {pers}, bew, klus.WerkPlek);
        }

        public Personeel[] Persoon { get; private set; }
        public ProductieFormulier Formulier { get; private set; }

        public void InitFields(ProductieFormulier formulier, Personeel[] pers, Bewerking bew = null,
            string werkplek = null)
        {
            Formulier = formulier ?? throw new Exception(
                "Ongeldige productie formulier!");
            OrigPersoon = pers;
            Persoon = pers.Select(x => x.CreateCopy()).ToArray();
            _werkplek = werkplek;
            SelectedKlus.ProductieNr = Formulier.ProductieNr;
            SelectedKlus.ArtikelNr = Formulier.ArtikelNr;
            SelectedKlus.Omschrijving = Formulier.Omschrijving;
            var description = $"Vul in klus gegevens voor {string.Join(", ", Persoon.Select(x => x.PersoneelNaam))}";
            this.Text = description;
            xstatus.Text = description + "\n" +
                           $"[{formulier.ArtikelNr}, {formulier.ProductieNr}] {formulier.Omschrijving.Replace("\n", " ")}";
            if (formulier.Bewerkingen == null || formulier.Bewerkingen.Length == 0)
                throw new Exception(
                    $"Productie '{formulier.ProductieNr}' bevat geen bewerkingen... voeg eerst wat bewerkingen toe.");

            foreach (var b in formulier.Bewerkingen)
            {
                if (!b.IsAllowed(null)) continue;
                xbewerkingen.Items.Add(b.Naam);
            }

            if (xbewerkingen.Items.Count > 0)
            {
                if (bew != null)
                    xbewerkingen.SelectedItem = bew.Naam;
                if (xbewerkingen.SelectedItem == null)
                    xbewerkingen.SelectedIndex = 0;
            }
            else
            {
                throw new Exception(
                    "Klus bevat geen geldige bewerkingen!\n\nHet kan zjn dat de bewerking{en) in de filter lijst zijn geplaatst.");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (await Save())
                DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xbewerkingen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xbewerkingen.SelectedItem == null)
                return;
            foreach (var per in Persoon) per.WerktAan = Formulier.ProductieNr + "\\" + xbewerkingen.SelectedItem;
            SelectedKlus.Naam = xbewerkingen.SelectedItem.ToString();
            var xs = Manager.BewerkingenLijst.GetWerkplekken(xbewerkingen.SelectedItem.ToString().Split('[')[0]);
            xwerkplekken.Items.Clear();
            if (xs is {Count: > 0})
            {
                xwerkplekken.Items.AddRange(xs.Select(x => (object) x).ToArray());

                if (_werkplek != null)
                    xwerkplekken.SelectedItem = _werkplek;
                if (xwerkplekken.SelectedItem == null)
                    xwerkplekken.SelectedIndex = 0;
            }
        }

        private void xwerkplekken_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xwerkplekken.SelectedItem == null || xbewerkingen.SelectedItem == null)
                return;
            //Persoon.UpdateKlus(SelectedKlus);
            var curbew = Formulier.Bewerkingen.FirstOrDefault(x =>
                string.Equals(x.Naam, xbewerkingen.SelectedItem.ToString(), StringComparison.CurrentCultureIgnoreCase));
            if (curbew == null) return;
            bool changed = false;
            foreach (var per in Persoon)
            {
                per.Werkplek = xwerkplekken.SelectedItem.ToString();
                if (changed) continue;
                var xklus =
                    per.Klusjes.GetKlus(
                        $"{Formulier.ProductieNr}\\{xbewerkingen.SelectedItem}\\{xwerkplekken.SelectedItem}");
                if (xklus == null)
                {
                    if (SelectedKlus == null)
                    {
                        SelectedKlus = new Klus(per, curbew, xwerkplekken.SelectedItem.ToString());
                    }
                }
                else
                    SelectedKlus = xklus.CreateCopy();
                bool isnew = false;
                SelectedKlus.Tijden.Add(new TijdEntry(xstart.Value, xstop.Value, per.WerkRooster), ref isnew);
                changed = true;
            }
            SelectedKlus.WerkPlek = xwerkplekken.SelectedItem.ToString();
            SelectedKlus.Status = curbew.State;
            var tent = SelectedKlus.GetAvailibleTijdEntry();
            xstart.SetValue(tent.Start);
            xstop.SetValue(tent.Stop);
            if (SelectedKlus.Status == ProductieState.Gestart)
            {
                xstop.Enabled = false;
                xgestoptlabel.Text = "Klus is momenteel actief!";
            }
            else
            {
                xstop.Enabled = true;
                xgestoptlabel.Text = "Wanneer Gestopt?";
            }
        }

        private WerkPlek GetWerkPlek(Personeel pers, bool createnew)
        {
            if (pers?.Werkplek == null || pers?.WerktAan == null)
                return null;
            var bew = Formulier.Bewerkingen.FirstOrDefault(x => x.Path.ToLower() == pers.WerktAan.ToLower());
            if (bew == null)
                return null;
            var wp = bew.WerkPlekken.FirstOrDefault(x => x.Naam.ToLower() == pers.Werkplek.ToLower());
            if (wp == null && createnew)
            {
                wp = new WerkPlek(pers, pers.Werkplek, bew);
                bew.WerkPlekken.Add(wp);
            }

            return wp;
        }

        private WerkPlek GetWerkPlek(string naam, Bewerking bewerking, bool createnew)
        {
            if (naam == null || bewerking == null)
                return null;
            //var bew = Formulier.Bewerkingen.FirstOrDefault(x => x.Path.ToLower() == bewerking.Path.ToLower());
            var wp = bewerking.WerkPlekken.FirstOrDefault(x => x.Naam.ToLower() == naam.ToLower());
            if (wp == null && createnew)
            {
                wp = new WerkPlek(naam, bewerking);
                bewerking.WerkPlekken.Add(wp);
            }

            return wp;
        }

        public Task<bool> Save()
        {
            return Task.Run(async () =>
            {
                var per = OrigPersoon;
                //we gaan eerst het oude verwijderen.
                var pair = _origklus.GetWerk(Formulier);
                var prod = pair.Formulier;
                var bewerking = pair.Bewerking;
                var wp = GetWerkPlek(_origklus.WerkPlek, bewerking, false);
                var xpersonen = new List<Personeel>();
                Rooster rooster = Persoon.Length == 1
                    ? Persoon[0].WerkRooster
                    : wp?.Tijden?.WerkRooster ?? Manager.Opties?.GetWerkRooster() ?? Rooster.StandaartRooster();
                foreach (var xpers in Persoon)
                {
                    var xklus = SelectedKlus.CreateCopy();
                    xpers.Klusjes.Remove(_origklus);
                    var dbpers = await Manager.Database.GetPersoneel(xpers.PersoneelNaam) ?? xpers;
                    if (!string.Equals(_origklus.Naam, xklus.Naam, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (wp != null)
                        {
                            string path = wp.Path;
                            wp.Personen.Remove(xpers);
                            xpers.Klusjes.RemoveAll(x =>
                                string.Equals(x.Path, path, StringComparison.CurrentCultureIgnoreCase));
                            dbpers.Klusjes.RemoveAll(x =>
                                string.Equals(x.Path, path, StringComparison.CurrentCultureIgnoreCase));
                        }

                        if (_save && bewerking != null)
                            await bewerking.UpdateBewerking(null,
                                $"{xpers.PersoneelNaam} aangepast op {bewerking.Path}");
                        //nu het oude verwijderd is kunnen we nieuwe gegevens toevoegen.
                        pair = xklus.GetWerk(Formulier);
                        //prod = pair.Key;
                        bewerking = pair.Bewerking;
                    }

                    if (bewerking == null)
                    {
                        pair = xklus.GetWerk(Formulier);
                        //prod = pair.Key;
                        bewerking = pair.Bewerking;
                        if (bewerking == null)
                            continue;
                    }
                    //We gaan eerst kijken of de personen zich ook in een andere werkplek zitten.
                    //Met editmode doen we de dezelfde personen gewoon wijzigen. Dus verwijderen van de oude werkplek.
                    //Zonder editmode doen we dezelfde personen op een andere werkplek gewoon op inactief zetten
                    if (!string.Equals(_origklus.WerkPlek, xklus.WerkPlek,
                        StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (wp != null)
                        {
                            var xcurpers = wp.Personen.FirstOrDefault(x=> string.Equals(x.PersoneelNaam, xpers.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase));
                            if (EditMode)
                            {
                                wp.Personen.Remove(xpers);
                                var xwp = wp;
                                xpers.Klusjes.RemoveAll(x =>
                                    string.Equals(x.Path, xwp.Path, StringComparison.CurrentCultureIgnoreCase));
                                dbpers.Klusjes.RemoveAll(x =>
                                    string.Equals(x.Path, xwp.Path, StringComparison.CurrentCultureIgnoreCase));
                                wp = xwp;
                            }
                            else
                            {
                                var klus = xcurpers?.Klusjes.GetKlus(wp.Path);
                                klus?.ZetActief(false, bewerking.State == ProductieState.Gestart);

                                if (wp.Personen.Count == 0 && wp.TijdGewerkt <= 0 && wp.AantalGemaakt == 0)
                                {
                                    string path = wp.Path;
                                    bewerking.WerkPlekken.Remove(wp);
                                    dbpers?.Klusjes.RemoveAll(x =>
                                        string.Equals(x.Path, path, StringComparison.CurrentCultureIgnoreCase));
                                }
                            }
                        }

                        wp = GetWerkPlek(xklus.WerkPlek, bewerking, false);
                    }

                    //nu het oude verwijderd is kunnen we nieuwe gegevens toevoegen.
                    //if (wp == null)
                    //    wp = GetWerkPlek(xklus.WerkPlek, bewerking, false);
                    //wp?.Personen.Remove(per);
                    if (wp == null)
                    {
                        wp = new WerkPlek(xklus.WerkPlek, bewerking);
                        bewerking.WerkPlekken.Add(wp);
                    }
                    else if (!EditMode && wp.Personen.Any(x => string.Equals(x.PersoneelNaam, xpers.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        if (XMessageBox.Show(this, $"{xpers.PersoneelNaam} is al toegevoegd op {wp.Naam}...\n\n" +
                                                   $"Zou je {xpers.PersoneelNaam} willen overschrijven?", $"{xpers.PersoneelNaam} bestaat al!",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            continue;
                    }

                    xklus.Tijden.WerkRooster = rooster;
                    xpers.ReplaceKlus(xklus);
                    wp.AddPersoon(xpers, bewerking);
                    wp.Tijden.SetUren(xklus.Tijden.Uren.ToArray(), bewerking.State == ProductieState.Gestart,
                        false);
                    bewerking.ZetPersoneelActief(xpers.PersoneelNaam, wp.Naam, !EditMode || xpers.IngezetAanKlus(bewerking,true, out _));
                    dbpers.ReplaceKlus(xklus);
                    if (_save)
                        await Manager.Database.UpSert(dbpers,
                            $"{xpers.PersoneelNaam} Klus {xklus.Path} Update.");
                    xpersonen.Add(xpers);
                }
                if (xpersonen.Count == 0) return false;
                Persoon = xpersonen.ToArray().CopyTo(ref OrigPersoon);
                SelectedKlus = SelectedKlus.CopyTo(ref _origklus);
                if (Formulier != null && bewerking != null)
                {
                    var xbwlist = new List<Bewerking>();
                    xbwlist.AddRange(Formulier.Bewerkingen.Where(x =>
                        !string.Equals(x.Path, bewerking.Path, StringComparison.CurrentCultureIgnoreCase)));
                    xbwlist.Add(bewerking);
                    Formulier.Bewerkingen = xbwlist.ToArray();
                    if (_save)
                        await bewerking.UpdateBewerking(null,
                            $"{string.Join(", ", Persoon.Select(x => x.PersoneelNaam))} aangepast op {bewerking.Path}");
                }
                return true;
            });
        }

        private void xstart_ValueChanged(object sender, EventArgs e)
        {
            UpdateTijdGewerkt();
        }

        private void xstop_ValueChanged(object sender, EventArgs e)
        {
            UpdateTijdGewerkt();
        }



        private void UpdateTijdGewerkt()
        {
            if (SelectedKlus == null) return;
            var tent = SelectedKlus.GetAvailibleTijdEntry();
            tent.Start = xstart.Value;
            tent.Stop = xstop.Value;
            tent.InUse = SelectedKlus.Status == ProductieState.Gestart;
            SelectedKlus.UpdateTijdGewerkt(tent);
            xgewerkt.Text =
                $"Gewerkte Tijd: {Math.Round(SelectedKlus.TijdGewerkt(GetPersoneelVrijeDagen(), SelectedKlus?.Tijden?.WerkRooster).TotalHours, 2)} uur.";
        }

        private Dictionary<DateTime, DateTime> GetPersoneelVrijeDagen()
        {
            var dates = new Dictionary<DateTime, DateTime>();
            if (Persoon == null || Persoon.Length == 0)
                return dates;
            foreach (var per in Persoon)
                if (per.VrijeDagen?.Count > 0)
                    foreach (var dt in per.VrijeDagen.Uren)
                        dates.Add(dt.Start, dt.Stop);
            return dates;
        }

        private void xgewerktetijden_Click(object sender, EventArgs e)
        {
            var changer = new WerktijdChanger(SelectedKlus);
            if (changer.ShowDialog() == DialogResult.OK)
            {
                changer.Klusje.CopyTo(ref SelectedKlus);
                var tent = SelectedKlus.GetAvailibleTijdEntry();
                xstart.SetValue(tent.Start);
                xstop.SetValue(tent.Stop);
                UpdateTijdGewerkt();
            }
        }
    }
}