using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using LiteDB;
using NPOI.SS.Formula.Functions;
using Polenter.Serialization;
using Rpm.SqlLite;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Various;

namespace Rpm.Productie
{
    [DataContract]
    public sealed class Bewerking : IProductieBase
    {
        private DateTime _gestartop;
        private DateTime _gestoptop;

        public Bewerking()
        {
            State = ProductieState.Gestopt;
        }

        public Bewerking(double doorlooptijd) : this()
        {
            DoorloopTijd = doorlooptijd;
        }

        [BsonId(true)] public int Id { get; set; }

        public List<WerkPlek> WerkPlekken { get; set; } = new();
        public override string WerkplekkenName => string.Join(", ", WerkPlekken.Select(x => x.Naam));
        public override string PersoneelNamen => string.Join(", ", Personen.Select(x => x.PersoneelNaam));
        public List<DeelsGereedMelding> DeelGereedMeldingen { get; set; } = new List<DeelsGereedMelding>();
        public override int AanbevolenPersonen { get; set; }

        [ExcludeFromSerialization]
        public Dictionary<DateTime, DateTime> GewerkteTijden { get; set; } = new();

        [ExcludeFromSerialization]
        public UrenLijst Tijden { get; set; }

        public override bool TeLaat => DateTime.Now > LeverDatum;

        public bool IsBemand { get; set; }

        public override DateTime TijdGestart
        {
            get => GestartOp();
            set => _gestartop = value;
        }

        public override DateTime TijdGestopt
        {
            get => GestoptOp();
            set => _gestoptop = value;
        }

        public int AantalActievePersonen
        {
            get { return GetPersoneel().Count(x => x.IngezetAanKlus(this, true, out _)); }
        }

        public int AantalPersonen
        {
            get { return GetPersoneel().Count(x => x.IngezetAanKlus(this, false, out _)); }
        }

        [ExcludeFromSerialization]
        public override Personeel[] Personen => GetPersoneel();

        [ExcludeFromSerialization]
        public int AantalBewerkingen { get; set; }

        public int _gemaakt { get; set; }

        public override int AantalGemaakt
        {
            get => GetAantalGemaakt();
            set => SetAantalGemaakt(value);
        }

        public override string ProductieNr
        {
            get => Parent == null ? base.ProductieNr : Parent.ProductieNr;
            set => base.ProductieNr = value;
        }

        public override int Aantal
        {
            get => Parent?.Aantal ?? base.Aantal;
            set
            {
                base.Aantal = value;
                if (Parent != null) Parent.Aantal = value;
            }
        }

        public override int AantalNogTeMaken
        {
            get
            {
                int gemaakt = TotaalGemaakt;
                return gemaakt > Aantal ? 0 : Aantal - gemaakt;
            }

        }

        public override int AantalTeMaken
        {
            get
            {
                int gemaakt = DeelGereedMeldingen.Sum(x => x.Aantal);
                return gemaakt > Aantal ? 0 : Aantal - gemaakt;
            }

        }

        public override int TotaalGemaakt => DeelGereedMeldingen.Sum(x => x.Aantal) + AantalGemaakt;
        public override int DeelsGereed => DeelGereedMeldingen.Sum(x => x.Aantal);

        private DateTime _Leverdatum;
        public override DateTime LeverDatum
        {
            get => _Leverdatum;
            set
            {
                _Leverdatum = value;
                if (Parent != null && Parent.LeverDatum < value)
                    Parent.LeverDatum = value;
            }
        }

        public override string ArtikelNr
        {
            get => Parent == null ? base.ArtikelNr : Parent.ArtikelNr;
            set => base.ArtikelNr = value;
        }

        public override string Omschrijving
        {
            get => (Parent == null ? base.Omschrijving : Parent.Omschrijving);//.WrapText(150);
            set => base.Omschrijving = value;
        }

        public override string Path => ProductieNr + "\\" + Naam;

        [BsonRef] public ProductieFormulier Parent { get; set; }
        public override  ProductieFormulier Root => Parent;

        private string _note;
        private string _gereednote;
        [ExcludeFromSerialization]
        public string Notitie
        {
            get => Note?.Notitie??_note;
            set
            {
                _note = value;
                if (Note == null && !string.IsNullOrEmpty(value))
                    Note = new NotitieEntry(value, this);
            }
        }
        [ExcludeFromSerialization]
        public string GereedNotitie
        {
            get => GereedNote?.Notitie?? _gereednote;
            set
            {
                _gereednote = value;
                if (GereedNote == null && !string.IsNullOrEmpty(value))
                    GereedNote = new NotitieEntry(value, this) { Type = NotitieType.BewerkingGereed, Naam = Paraaf };
            }
        }

        public override List<WerkPlek> GetWerkPlekken()
        {
            return WerkPlekken;
        }

        public bool GetBemand()
        {
            if (Manager.BewerkingenLijst == null)
                return IsBemand;
            var ent = Manager.BewerkingenLijst.GetEntry(Naam.Split('[')[0]);
            if (ent == null)
                return IsBemand;
            return ent.IsBemand;
        }

        public DateTime GetStartOp()
        {
            var dt = DateTime.Now;
            int pers = AantalPersonenNodig(ref dt,false);
            return dt;
        }

        public Personeel[] GetPersoneel()
        {
            if (WerkPlekken == null || WerkPlekken.Count == 0)
                return new Personeel[] { };
            var personeel = new List<Personeel>();
            WerkPlekken.ForEach(x => personeel.AddRange(x.Personen));
            return personeel.ToArray();
        }

        public Storing[] GetAlleStoringen(bool openstaand)
        {
            if (WerkPlekken == null || WerkPlekken.Count == 0)
                return new Storing[] { };
            var storingen = new List<Storing>();
            WerkPlekken.ForEach(x =>
            {
                var xstoringen = x.Storingen;
                if (openstaand && xstoringen?.Count > 0)
                    xstoringen = xstoringen.Where(s => !s.IsVerholpen).ToList();
                if (xstoringen?.Count > 0)
                    storingen.AddRange(xstoringen);
            });
            return storingen.ToArray();
        }

        public Storing[] GetAlleStoringen(DateTime vanaf, DateTime tot)
        {
            if (WerkPlekken == null || WerkPlekken.Count == 0)
                return new Storing[] { };
            var storingen = new List<Storing>();
            foreach (var wp in WerkPlekken)
            {
                var xstoringen = wp.Storingen;
                if (xstoringen != null && xstoringen.Count > 0)
                {
                    foreach (var storing in xstoringen)
                    {
                        var stop = storing.IsVerholpen ? storing.Gestopt : DateTime.Now;
                        if (storing.Gestart >= vanaf && storing.Gestart <= tot || stop > vanaf && stop <= tot)
                            storingen.Add(storing);
                    }

                    //storingen.AddRange(from storing in xstoringen
                    //    let stop = storing.IsVerholpen ? storing.Gestopt : DateTime.Now
                    //    where tot > storing.Gestart && tot <= stop || vanaf >= storing.Gestart && vanaf < stop
                    //    select storing);
                }
            }

            return storingen.ToArray();
        }

        public Personeel[] GetPersoneel(string werkplek)
        {
            if (WerkPlekken == null || WerkPlekken.Count == 0)
                return new Personeel[] { };
            var personeel = new List<Personeel>();
            WerkPlekken.Where(x => string.Equals(x.Naam, werkplek, StringComparison.CurrentCultureIgnoreCase)).ToList()
                .ForEach(x => personeel.AddRange(x.Personen));
            return personeel.ToArray();
        }

        public bool SetPersoneel(Personeel[] personen)
        {
            if (personen == null)
                return false;
            try
            {
                if (WerkPlekken.Count > 0)
                {
                    for (var i = 0; i < WerkPlekken.Count; i++)
                    {
                        if (personen.Length > 0)
                            WerkPlekken[i].Personen.RemoveAll(x => !personen.Any(t => t.Equals(x)));
                        else WerkPlekken[i].Personen.Clear();
                        WerkPlekken[i].UpdateTijdGestart();
                        if (i + 1 < WerkPlekken.Count)
                            for (var j = i + 1; j < WerkPlekken.Count; j++)
                                if (WerkPlekken[j].Naam.ToLower() == WerkPlekken[i].Naam.ToLower())
                                {
                                    if (WerkPlekken[j].Personen.Count > 0)
                                        WerkPlekken[i].AddPersonen(WerkPlekken[j].Personen.ToArray(), this);
                                    WerkPlekken.RemoveAt(j--);
                                    i--;
                                }
                    }
                }

                var xreturn = false;
                foreach (var persoon in personen) xreturn |= AddPersoneel(persoon, persoon.Werkplek);
                WerkPlekken.RemoveAll(x => x.Personen.Count == 0);
                return xreturn;
            }
            catch
            {
                return false;
            }
        }

        public bool AddPersoneel(Personeel persoon, string werkplek)
        {
            try
            {
                if (persoon != null && werkplek != null)
                {
                    var current = WerkPlekken.FirstOrDefault(x =>
                        string.Equals(x.Naam, werkplek, StringComparison.CurrentCultureIgnoreCase));

                    if (current == null)
                    {
                        current = new WerkPlek(new[] {persoon}, werkplek, this);
                        WerkPlekken.Add(current);
                        return true;
                    }

                    return current.AddPersoon(persoon, this);
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateBewerking(List<ProductieFormulier> allforms = null, string change = null,
            bool save = true)
        {
            change ??= $"[{Path}] Update.";
            if (allforms != null)
            {
                var forms = ArtikelNr == null
                    ? allforms.ToArray()
                    : allforms.Where(t =>string.Equals(ArtikelNr, t.ArtikelNr, StringComparison.CurrentCultureIgnoreCase))
                        .ToArray();
                Geproduceerd = AlleBewerkingen(forms).Length;
                GemiddeldPerUur = GetGemiddeldAantalPerUur(forms);
                GemiddeldActueelPerUur = GetGemiddeldActueelPerUur(forms);
                TotaalTijdGewerkt = TotaalGewerkteUren(forms);
                var xaantal = TotaalGemaakt > Aantal ? TotaalGemaakt : Aantal;
                GemiddeldDoorlooptijd = xaantal > 0 && GemiddeldPerUur > 0? Math.Round(xaantal / GemiddeldPerUur, 2) : DoorloopTijd;
            }

            if(WerkPlekken != null)
                foreach (WerkPlek wp in WerkPlekken)
                {
                    wp.UpdateWerkplek();
                }

            if (DeelGereedMeldingen != null)
                foreach (var deels in DeelGereedMeldingen)
                    deels.Werk = this;

            GemiddeldDoorlooptijd = GemiddeldDoorlooptijd > 0 ? GemiddeldDoorlooptijd : DoorloopTijd;
            TijdGewerkt = TijdAanGewerkt();
            if (TotaalTijdGewerkt < TijdGewerkt)
                TotaalTijdGewerkt = TijdGewerkt;
            ActueelPerUur = ActueelProductenPerUur();
            if (GemiddeldPerUur <= 0)
                GemiddeldPerUur = ActueelPerUur;
            VerwachtLeverDatum = VerwachtDatumGereed();
            Gereed = GereedPercentage();
            var dt = DateTime.Now;
            AanbevolenPersonen = AantalPersonenNodig(ref dt,false);
            StartOp = dt;
            if (AantalActievePersonen == 0 && State == ProductieState.Gestart)
                await StopProductie(true);
            LastChanged = LastChanged.UpdateChange(change, DbType.Producties);
            if (Parent != null)
            {
                Aantal = Parent.Aantal;
                Omschrijving = Parent.Omschrijving;
                ArtikelNr = Parent.ArtikelNr;
                ProductieNr = Parent.ProductieNr;

                if (save)
                    Parent.BewerkingChanged(this, this, change);
            }

            BewerkingChanged(this, this, change);
            return true;
        }

        public async Task<Bewerking> CreateNewInstance(ProductieFormulier parent)
        {
            var b = new Bewerking
            {
                State = State,
                Naam = Naam,
                DoorloopTijd = DoorloopTijd,
                TijdGewerkt = TijdGewerkt,
                TotaalTijdGewerkt = TotaalTijdGewerkt,
                TijdGestart = TijdGestart,
                TijdGestopt = TijdGestopt,
                VerwachtLeverDatum = VerwachtLeverDatum,
                LeverDatum = parent.LeverDatum,
                DatumToegevoegd = parent.DatumToegevoegd,
                Omschrijving = parent.Omschrijving,
                Paraaf = Paraaf,
                Aantal = parent.Aantal,
                DatumVerwijderd = DatumVerwijderd
            };
            b.SetPersoneel(GetPersoneel().Select(Personeel.CreateNew).ToArray());
            await b.UpdateBewerking(null, $"[{Path}] Bewerkingen aangemaakt", false);
            return b;
        }

        public async Task<bool> StartProductie(bool email, bool newtime)
        {
            try
            {
                if (Manager.Opties == null) throw new Exception("Instellingen zijn nog niet geladen!");
                switch (State)
                {
                    case ProductieState.Verwijderd:
                        throw new Exception("Productie is verwijdert en kan daarom niet gestart worden.");
                    case ProductieState.Gereed:
                        throw new Exception("Productie is al gereed gemeld en kan daarom niet gestart worden.");
                    case ProductieState.Gestart:
                        throw new Exception("Productie is al gestart!");
                }

                if (AantalActievePersonen == 0)
                    throw new Exception("Geen personeel geselecteerd om te werken.");
                LaatstAantalUpdate = DateTime.Now;
                State = ProductieState.Gestart;
                TijdGestart = DateTime.Now;
                var xtasks = new List<Task<bool>>();
                foreach (var plek in WerkPlekken)
                {
                    plek.UpdateWerkRooster(true,false,false,false,true);
                    foreach (var per in plek.Personen)
                    {
                        var klus = per.Klusjes.GetKlus(plek.Path);
                        if (klus == null)
                        {
                           klus = new Klus(per, plek);
                           per.ReplaceKlus(klus);
                        }

                        if (klus.Start())
                        {
                            var x = await Manager.Database.GetPersoneel(per.PersoneelNaam);
                            if (x != null)
                            {
                                per.VrijeDagen = x.VrijeDagen;
                                if (per.WerkRooster == null)
                                    per.WerkRooster = x.WerkRooster;
                                x.ReplaceKlus(klus);
                                xtasks.Add(Manager.Database.UpSert(x,
                                    $"[{x.PersoneelNaam}] is gestart aan klus '{klus.Naam} op {klus.WerkPlek}'."));
                            }
                        }
                    }

                    if (plek.IsActief())
                    {
                        plek.Tijden.SetStart();
                        plek.TijdGestart = DateTime.Now;
                    }

                    plek.LaatstAantalUpdate = DateTime.Now;
                    plek.UpdateTijdGestart();
                }

                if (xtasks.Count > 0)
                    _=Task.WhenAll(xtasks);
                //if (newtime || Tijden.Count == 0)

                if (Parent != null && Parent.State != ProductieState.Gestart)
                    Parent.TijdGestart = DateTime.Now;
               
                GestartDoor = Manager.Opties.Username;
                await UpdateBewerking(null, $"[{Path}] Bewerking Gestart");
                if (email)
                    RemoteProductie.RespondByEmail(this,
                        $"Productie [{ProductieNr.ToUpper()}] {Naam} is zojuist gestart op {WerkplekkenName}.");
                return true;
            }
            catch
            {
                await StopProductie(false);
                return false;
            }
        }

        public void ZetPersoneelActief(string personeelnaam,string werkplek,bool actief)
        {
            if (WerkPlekken != null)
            {
                var xwp = WerkPlekken.FirstOrDefault(x => string.Equals(x.Naam, werkplek, StringComparison.CurrentCultureIgnoreCase));
                if(xwp != null)
                {
                    var xpers = xwp.Personen.Where(x =>
                   string.Equals(x.PersoneelNaam, personeelnaam, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    foreach (var xper in xpers)
                    {
                        var xklus = xper.Klusjes.GetKlus($"{Path}\\{werkplek}");
                        xklus?.ZetActief(actief, State == ProductieState.Gestart);
                    }
                }
                if (!actief) return;
                foreach (var wp in WerkPlekken)
                {
                    if (string.Equals(wp.Naam, werkplek, StringComparison.CurrentCultureIgnoreCase)) continue;
                  
                    var xpers = wp.Personen.Where(x =>
                    string.Equals(x.PersoneelNaam, personeelnaam, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    foreach (var xper in xpers)
                    {
                        var xklus = xper.Klusjes.GetKlus($"{Path}\\{wp.Naam}");
                        xklus?.ZetActief(false, State == ProductieState.Gestart);
                    }
                }
            }
        }

        public async Task<bool> StopProductie(bool email)
        {
            try
            {
                if (State == ProductieState.Gestopt)
                    return false;
                if (State == ProductieState.Gereed || State == ProductieState.Verwijderd)
                    return false;
                TijdGestopt = DateTime.Now;
                if (Parent != null && Parent.State != ProductieState.Gestopt)
                    Parent.TijdGestopt = DateTime.Now;
                State = ProductieState.Gestopt;

                var xtasks = new List<Task<bool>>();
                foreach (var plek in WerkPlekken)
                {
                    plek.TijdGestopt = DateTime.Now;
                    plek.Tijden.SetStop();
                    foreach (var per in plek.Personen)
                    {
                        var klus = per.Klusjes.GetKlus(plek.Path) ??
                                   new Klus(per, plek);

                        if (!klus.Stop()) continue;
                        per.ReplaceKlus(klus);
                        var x = await Manager.Database.GetPersoneel(per.PersoneelNaam);
                        if (x == null) continue;
                        per.VrijeDagen = x.VrijeDagen;
                        x.ReplaceKlus(klus);
                        xtasks.Add(Manager.Database.UpSert(x, $"[{x.PersoneelNaam}] uit werk [{Path}] gehaald"));
                    }
                }

                if (xtasks.Count > 0)
                   _=Task.WhenAll(xtasks);
                await UpdateBewerking(null, $"[{Path}] Bewerking Gestopt");
                if (email)
                    RemoteProductie.RespondByEmail(this,
                        $"Productie [{ProductieNr.ToUpper()}] {Naam} is zojuist gestopt op {WerkplekkenName}.");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<bool> RemoveBewerking(bool skip)
        {
            return Task.Run(async() =>
            {
                var personen = GetPersoneel();

                if (State == ProductieState.Verwijderd)
                {
                    if (Parent != null && !skip)
                    {
                        foreach (var per in personen)
                        {
                            if (per.IngezetAanKlus(this, false, out var klusjes))
                            {
                                var dbpers = await Manager.Database.GetPersoneel(per.PersoneelNaam);
                                if (dbpers != null)
                                {
                                    foreach (var klus in klusjes)
                                        dbpers.Klusjes.Remove(klus);
                                    await Manager.Database.UpSert(dbpers, $"{Path} Klusjes verwijderd");
                                }
                            }
                        }

                        var bws = Parent.Bewerkingen.Where(x => !x.Equals(this)).ToArray();
                        var deleted = bws.Length < Parent.Bewerkingen.Length;
                        Parent.Bewerkingen = bws;
                        if (Parent.Bewerkingen.Length == 0)
                        {
                            return await Manager.Database.Delete(Parent);
                        }

                        if (deleted)
                        {
                            Manager.BewerkingDeleted(this, this);
                            await Parent.UpdateForm(true, false);

                        }

                        return deleted;
                    }

                    return false;
                }

                if (State == ProductieState.Gestart) await StopProductie(false);
                foreach (var per in personen)
                {
                    if (per.IngezetAanKlus(this, false, out var klusjes))
                    {
                        var dbpers = await Manager.Database.GetPersoneel(per.PersoneelNaam);
                        if (dbpers != null)
                        {
                            foreach (var klus in klusjes)
                            {
                                klus.Stop();
                                klus.Status = ProductieState.Verwijderd;
                                dbpers.ReplaceKlus(klus);
                            }

                            await Manager.Database.UpSert(dbpers, $"{Path} Klusjes verwijderd");
                        }
                    }
                }

                DatumVerwijderd = DateTime.Now;
                State = ProductieState.Verwijderd;
                await UpdateBewerking(null, $"[{Path}] Verwijderd.");
                return true;
            });
        }

        public Task<bool> Undo()
        {
            return Task.Run(async() =>
            {
                if (State != ProductieState.Gestart && State != ProductieState.Gestopt)
                {
                    State = ProductieState.Gestopt;
                    var personen = GetPersoneel();
                    foreach (var per in personen)
                    {
                        if (per.IngezetAanKlus(this, false, out var klusjes))
                        {
                            var dbpers = await Manager.Database.GetPersoneel(per.PersoneelNaam);
                            if (dbpers != null)
                            {
                                foreach (var klus in klusjes)
                                {
                                    klus.Stop();
                                    dbpers.ReplaceKlus(klus);
                                }

                                await Manager.Database.UpSert(dbpers, $"{Path} Klusjes terug gezet");
                            }
                        }
                    }

                    await UpdateBewerking();
                    return true;
                }

                return false;
            });
        }

        public DateTime GestartOp()
        {
            var dt = new DateTime();
            if (WerkPlekken == null || WerkPlekken.Count == 0)
                    return _gestartop;

            //if (!IsBemand) return Tijden.GetFirstStart();
            foreach (var wp in WerkPlekken)
                if (wp.GestartOp() < dt || dt.IsDefault())
                    dt = wp.GestartOp();
            return dt;
        }

        public DateTime GestoptOp()
        {
            var dt = new DateTime();
            if (WerkPlekken == null || WerkPlekken.Count == 0)
                return _gestoptop;

            // if (!IsBemand) return Tijden?.GetLastStop() ?? _gestoptop;
            foreach (var wp in WerkPlekken)
                if (wp.GestoptOp() > dt)
                    dt = wp.GestoptOp();
            return dt;
        }

        public ProductieFormulier GetParent()
        {
            if (string.IsNullOrEmpty(ProductieNr))
                return null;
            var parent = Manager.Database.GetProductie(ProductieNr).Result;
            if (parent?.Bewerkingen != null)
                for (var i = 0; i < parent.Bewerkingen.Length; i++)
                    if (string.Equals(parent.Bewerkingen[i].Naam, Naam, StringComparison.CurrentCultureIgnoreCase))
                        parent.Bewerkingen[i] = this;
            parent ??= Parent;
            return parent;
        }

        public async Task<bool> MeldBewerkingGereed(string paraaf, int aantal,
            string notitie,
            bool update, bool sendmail)
        {
            await StopProductie(false);
           // await MeldDeelsGereed(paraaf, aantal, notitie,null,DateTime.Now, false);
            LaatstAantalUpdate = DateTime.Now;
            DatumGereed = DateTime.Now;
            GereedNote = new NotitieEntry(notitie, this) {Type = NotitieType.BewerkingGereed, Naam = paraaf};
            Paraaf = paraaf;
            State = ProductieState.Gereed;
            AantalGemaakt = aantal;
            var personen = GetPersoneel();
            foreach (var per in personen)
                if (per.IngezetAanKlus(this, false, out var klusjes))
                {
                    var count = 0;
                    var xper = await Manager.Database.GetPersoneel(per.PersoneelNaam);
                    foreach (var klus in klusjes)
                        if (klus.MeldGereed())
                            if ( xper != null && xper.ReplaceKlus(klus))
                                count++;

                    if (count > 0 && xper != null) await Manager.Database.UpSert(xper, $"[{xper.PersoneelNaam}] {Path} klus gereed gemeld");
                }

            var xa = aantal == 1 ? "stuk" : "stuks";

            var change =
                $"[{Path}] {paraaf} heeft is zojuist {TotaalGemaakt} {xa} gereed gemeld in {TijdGewerkt} uur({ActueelPerUur} P/u) op {WerkplekkenName}.";
           
            await UpdateBewerking(null, change, update);
            if (sendmail)
                RemoteProductie.RespondByEmail(this, change);

            var xcount = 0;
            var parent = Parent;
            if (parent.Bewerkingen != null)
                xcount = parent.Bewerkingen.Count(t => t.State != ProductieState.Gereed);
            if (xcount == 0)
                await parent.MeldGereed(aantal, paraaf, notitie, false);
            //else
            //{
            //    if (sendmail)
            //        RemoteProductie.RespondByEmail(this, change);
            //    await UpdateBewerking(null, change, update);
            //}

            return true;
        }

        public Task<bool> MeldDeelsGereed(DeelsGereedMelding gereedmelding, bool update)
        {
            return MeldDeelsGereed(gereedmelding.Paraaf, gereedmelding.Aantal, gereedmelding.Notitie,
                gereedmelding.WerkPlek,gereedmelding.Datum, update);
        }

        public Task<bool> MeldDeelsGereed(string paraaf, int aantal,
            string notitie,string werkplek,DateTime datum, bool update)
        {
            return Task.Run(async ()=>
            {
                try
                {
                    int xaantal = aantal;
                   
                    if (werkplek != null && WerkPlekken.Count > 0)
                    {
                        int xparts = xaantal / WerkPlekken.Count;
                        int xrestpart = xaantal % WerkPlekken.Count;
                        int index = 0;
                        foreach (var wp in WerkPlekken)
                        {
                            var deelgereed = new DeelsGereedMelding()
                            {
                                Aantal = xparts,
                                Datum = datum,
                                Notitie = notitie,
                                Paraaf = paraaf,
                                WerkPlek = werkplek,
                                Werk = this
                            };
                            
                            if (wp.AantalGemaakt >= xparts)
                                wp.AantalGemaakt -= xparts;
                            else wp.AantalGemaakt = 0;
                            if (index == WerkPlekken.Count - 1)
                            {
                                deelgereed.Aantal += xrestpart;
                                if (wp.AantalGemaakt > 0)
                                    wp.AantalGemaakt -= xrestpart;
                            }
                            DeelGereedMeldingen.Add(deelgereed);
                            index++;
                        }
                    }
                    else
                    {
                        var deelgereed = new DeelsGereedMelding()
                        {
                            Aantal = aantal,
                            Datum = datum,
                            Notitie = notitie,
                            Paraaf = paraaf,
                            WerkPlek = werkplek,
                            Werk = this
                        };
                        DeelGereedMeldingen.Add(deelgereed);
                        var wp = WerkPlekken?.FirstOrDefault(x =>
                            string.Equals(werkplek, x.Naam, StringComparison.CurrentCultureIgnoreCase));
                        if (wp != null)
                        {
                            if (wp.AantalGemaakt >= xaantal)
                                wp.AantalGemaakt -= xaantal;
                            else
                                wp.AantalGemaakt = 0;
                        }
                        else
                        {
                            if (AantalGemaakt >= xaantal)
                                AantalGemaakt -= xaantal;
                            else
                                AantalGemaakt = 0;
                        }
                    }
                    if (update)
                    {
                        var xa = aantal == 1 ? "stuk" : "stuks";
                        var change =
                            $"[{Path}] {paraaf} heeft is zojuist {aantal} {xa} deels gereed gemeld in {TijdGewerkt} uur({ActueelPerUur} P/u).";
                        return await UpdateBewerking(null, change);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            });
        }

        public DateTime VerwachtDatumGereed()
        {
            if (State == ProductieState.Gereed) return DatumGereed;
            var tijd = TijdOver();
            //tijd /= GetPersoneel().AantalPersTijdMultiplier();
            var rooster = Manager.Opties?.GetWerkRooster();
            if (TotaalGemaakt >= Aantal || tijd == 0 || double.IsNaN(tijd) || double.IsInfinity(tijd))
                return Werktijd.EerstVolgendeWerkdag(DateTime.Now,  ref rooster,rooster,Tijden?.SpecialeRoosters);
            return Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(tijd),
                rooster,Tijden?.SpecialeRoosters);
        }

        public Bewerking[] AlleBewerkingen(ProductieFormulier[] forms)
        {
            var bws = new List<Bewerking>();
            if (forms == null)
                return bws.ToArray();
            foreach (var v in forms)
                if (v.Bewerkingen != null)
                    bws.AddRange(v.Bewerkingen
                        .Where(s => string.Equals(s.Naam, Naam, StringComparison.CurrentCultureIgnoreCase)).ToArray());
            return bws.ToArray();
        }

        public double TotaalGewerkteUren(ProductieFormulier[] forms)
        {
            if (forms == null)
                return TotaalTijdGewerkt;
            var x = forms;
            var gewerkt =
                Math.Round(
                    x.Sum(t => t.Bewerkingen.Sum(s =>
                        string.Equals(s.Naam, Naam, StringComparison.CurrentCultureIgnoreCase) ? s.TijdGewerkt : 0)),
                    2);
            if (double.IsNaN(gewerkt) || double.IsInfinity(gewerkt))
                gewerkt = 0;
            return gewerkt;
        }

        public double GetGemiddeldAantalPerUur(ProductieFormulier[] forms)
        {
            if (forms == null)
                return 0;
            var x = forms;
            var bws = x.Select(t =>
                t.Bewerkingen.Where(b =>
                        b.PerUur > 0 && string.Equals(b.Naam, Naam, StringComparison.CurrentCultureIgnoreCase))
                    .ToArray()).ToArray();
            var aantalbws = bws.Sum(t => t.Length);
            if (aantalbws == 0)
                return 0;
            var peruur = (double)bws.Sum(t => t.Sum(b => b.PerUur)) / aantalbws;
            return Math.Round(peruur, 0);
        }

        public double GetGemiddeldActueelPerUur(ProductieFormulier[] forms)
        {
            if (forms == null)
                return 0;
            var x = forms;
            var bws = x.Select(t =>
                t.Bewerkingen.Where(b =>
                        b.ActueelPerUur > 0 && string.Equals(b.Naam, Naam, StringComparison.CurrentCultureIgnoreCase))
                    .ToArray()).ToArray();
            var aantalbws = bws.Sum(t => t.Length);
            if (aantalbws == 0)
                return 0;
            var peruur = bws.Sum(t => t.Sum(b => b.ActueelPerUur)) / aantalbws;
            return Math.Round(peruur, 0);
        }

        public double GereedPercentage()
        {
            if (Aantal > 0)
            {
                var val = Math.Round(TotaalGemaakt / (double) Aantal * 100, 1);
                if (val > 100)
                    val = 100;
                return val;
            }

            return 0;
        }

        public int ActueelProductenPerUur()
        {
            var tijd = TijdAanGewerkt();
            var peruur = 0;
            if (tijd > 0 && TotaalGemaakt > 0)
                peruur = (int) (TotaalGemaakt / tijd);
            return peruur;
        }

        public double TijdAanGewerkt()
        {
            //if (!string.IsNullOrEmpty(GestartDoor) && !string.Equals(GestartDoor, Manager.Opties.Username,
            //    StringComparison.CurrentCultureIgnoreCase))
            //    return TijdGewerkt;
            double tijd = 0;
            if (!IsBemand)
            {
                tijd = CalculateMachineTijd();
            }
            else
            {
                if (WerkPlekken != null && WerkPlekken.Count > 0)
                    foreach (var wp in WerkPlekken)
                        tijd += wp.TijdAanGewerkt();

                if (tijd < 0 || double.IsInfinity(tijd)) tijd = 0;
            }

            return Math.Round(tijd, 2);
        }

        public double TijdAanGewerkt(DateTime vanaf, DateTime tot)
        {
            double tijd = 0;
            if (!IsBemand)
            {
                tijd = CalculateMachineTijd(vanaf, tot);
            }
            else
            {
                if (WerkPlekken != null && WerkPlekken.Count > 0)
                    foreach (var wp in WerkPlekken)
                        tijd += wp.TijdAanGewerkt(vanaf, tot);

                if (tijd < 0 || double.IsInfinity(tijd)) tijd = 0;
            }

            return Math.Round(tijd, 2);
        }

        public double CalculateMachineTijd()
        {
            var storingen = WerkPlekken.ToArray().CreateStoringDictionary();
            return Math.Round(WerkPlekken.Sum(x=> x.Tijden.TijdGewerkt(null, storingen)),2);
        }

        public double CalculateMachineTijd(DateTime vanaf, DateTime tot)
        {
            var storingen = WerkPlekken.ToArray().CreateStoringDictionary();
            return Math.Round(WerkPlekken.Sum(x => x.Tijden.TijdGewerkt(null, vanaf,tot,storingen)), 2);
        }

        public double TijdNodig()
        {
            try
            {
                if (TotaalGemaakt >= Aantal || State == ProductieState.Gereed || State == ProductieState.Verwijderd)
                    return 0;
                if (Aantal - TotaalGemaakt <= 0)
                    return 0;
                double peruur = ActueelProductenPerUur();
                if (peruur == 0)
                    peruur = PerUur;
                if (peruur == 0)
                    peruur = Aantal - TotaalGemaakt;
                var tijd = (Aantal - TotaalGemaakt) / peruur;
                return Math.Round(tijd, 2);
            }
            catch
            {
                return 0;
            }
        }

        public double TijdOver()
        {
            var tijd = TijdNodig();
            if (tijd > 0 && IsBemand)
            {
                var xpers = AantalActievePersonen;
                if (xpers == 0) xpers++;
                tijd /= xpers;
                tijd = Math.Round(tijd, 2);
            }

            return tijd;
        }

        public int AantalPersonenNodig(ref DateTime startop, bool onlyifsmaller)
        {
            var nodig = TijdNodig();
            if (nodig == 0)
                return 0;
            var tijdover = Werktijd
                .TijdGewerkt(DateTime.Now, LeverDatum, Tijden?.WerkRooster,Tijden?.SpecialeRoosters);
            var uurover = tijdover.TotalHours;
            int pers = 1;

            if (IsBemand)
            {
                if (uurover == 0)
                    pers = (int) Math.Ceiling(nodig / (ActueelPerUur > 0 ? ActueelPerUur : PerUur > 0 ? PerUur : 100));
                else if (nodig <= uurover)
                    pers = 1;
                else
                    pers = (int) Math.Ceiling(nodig / uurover);

                if (pers < 1) return pers = 1;
            }

            var uurpp = nodig / pers;
            //voeg start marge toe van 1 uur
            uurpp += 1;
            //startop = DateTime.Now;
            Rooster rs = Tijden?.WerkRooster??Manager.Opties?.GetWerkRooster();
            // if (uurpp > 0)
            var xt = Werktijd.DatumVoorTijd(LeverDatum, TimeSpan.FromHours(uurpp), rs, Tijden?.SpecialeRoosters);
            if (onlyifsmaller && xt < startop)
                startop = xt;
            else if (!onlyifsmaller)
                startop = xt;
            return pers;
        }

        public event BewerkingChangedHandler OnBewerkingChanged;

        public void BewerkingChanged(object sender, Bewerking bewerking, string change)
        {
            OnBewerkingChanged?.Invoke(sender, bewerking, change);
        }

        public async void BewerkingChanged(string change = null)
        {
            if (change == null)
                change = $"[{Path}] Bewerking Gewijzigd";
            await UpdateBewerking(null, change);
        }

        public int GetAantalGemaakt()
        {
            var aantal = 0;
            var done = false;
            if (WerkPlekken != null && WerkPlekken.Count > 0)
                foreach (var plek in WerkPlekken)
                    if (plek.Werk.Equals(this))
                    {
                        aantal += plek.AantalGemaakt;
                        done = true;
                    }

            return done ? aantal : _gemaakt;
        }

        public Task<Dictionary<string, List<Klus>>> GetAanbevolenPersonen()
        {
            return Task.Run(async () =>
            {
                var xreturn = new Dictionary<string, List<Klus>>();
                try
                {
                    var pers = await Manager.Database.GetAllPersoneel();
                    if (pers != null && pers.Count > 0)
                    {
                        foreach (var per in pers)
                        {
                            var klusjes = per.HeeftOoitGewerktAan(this);
                            if (klusjes != null && klusjes.Count > 0)
                            {
                                if (xreturn.ContainsKey(per.PersoneelNaam))
                                    xreturn[per.PersoneelNaam].AddRange(klusjes);
                                else xreturn.Add(per.PersoneelNaam, klusjes);
                            }
                        }

                        xreturn = xreturn.OrderBy(x => x.Value.Sum(s => s.TijdGewerkt())).Reverse()
                            .ToDictionary(x => x.Key, x => x.Value);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return xreturn;
            });
        }

        public Task<Dictionary<string, List<WerkPlek>>> GetAanbevolenWerkplekken()
        {
            return Task.Run(async () =>
            {
                var xreturn = new Dictionary<string, List<WerkPlek>>();
                try
                {
                    var prods = await Manager.Database.GetProducties($"{ArtikelNr}",true, true);
                    if (prods is {Count: > 0})
                    {
                        foreach (var prod in prods)
                        {
                            var bw = prod.Bewerkingen?.FirstOrDefault(x =>
                                string.Equals(x.Naam, Naam, StringComparison.CurrentCultureIgnoreCase));
                            if (bw?.WerkPlekken != null && bw.WerkPlekken.Count > 0)
                            {
                                foreach (var wp in bw.WerkPlekken)
                                {
                                    if (wp.TijdAanGewerkt() <= 0) continue;
                                    if (xreturn.ContainsKey(wp.Naam))
                                        xreturn[wp.Naam].Add(wp);
                                    else xreturn.Add(wp.Naam, new List<WerkPlek>() {wp});
                                }
                            }
                        }

                        xreturn = xreturn.OrderBy(x => x.Value.Sum(s => s.PerUur)).Reverse().ToDictionary(x => x.Key, x => x.Value);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return xreturn;
            });
        }

        public async Task<KeyValuePair<string, int>> GetAanbevolenPersoneelHtml(bool inclheader, int aantbevolen)
        {
            var xret = await GetAanbevolenPersoneelHtml(inclheader);
            aantbevolen += xret.Value;
            return new KeyValuePair<string, int>(xret.Key, aantbevolen);
        }

        public async Task<KeyValuePair<string,int>> GetAanbevolenWerkplekHtml(bool inclheader, int aantbevolen)
        {
            var xret = await GetAanbevolenWerkplekkenHtml(inclheader);
            aantbevolen += xret.Value;
            return new KeyValuePair<string, int>(xret.Key, aantbevolen);
        }

        public Task<KeyValuePair<string,int>> GetAanbevolenPersoneelHtml(bool inclheader)
        {
            return Task.Run(async() =>
            {
                var xpers = 0;
                string xkey = string.Empty;
                try
                {
                    var xdict = await GetAanbevolenPersonen();
                    xpers = xdict.Count;
                    if (xdict.Count > 0)
                    {
                        var x1 = xdict.Count == 1 ? "persoon" : "personen";
                        var xtitle = $"{xdict.Count} Aanbevolen {x1} voor {this.Naam}";
                        if (inclheader)
                            xkey = $"<html>\r\n" +
                                   $"<head>\r\n" +
                                   $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
                                   $"<Title>{ArtikelNr}</Title>\r\n" +
                                   $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                                   $"</head>\r\n" +
                                   $"<body style='background - color: {Color.DarkGreen.Name}; background-gradient: {Color.DarkGreen.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 50px 0px'>\r\n" +
                                   $"<h1 align='center' style='color: {Color.White.Name}'>\r\n" +
                                   $"       {xtitle}\r\n" +
                                   $"        <br/>\r\n" +
                                   $"        <span style=\'font-size: x-small;\'>ArtikelNr: {ArtikelNr}, ProductieNr: {ProductieNr}</span>\r\n " +
                                   $"</h1>\r\n" +
                                   $"<blockquote class='whitehole'>\r\n" +
                                   $"       <p style = 'margin-top: 0px' >\r\n" +
                                   $"<table border = '0' width = '100%' >\r\n" +
                                   $"<tr style = 'vertical-align: top;' >\r\n" +
                                   $"<td>\r\n";
                        else xkey = "";
                        xkey += $"<h3><u><b>{Naam}</b></u></h3>\r\n";
                        foreach (var key in xdict)
                        {
                            var x2 = key.Value.Count == 1 ? "klus" : "klusjes";
                            double tijd = 0;
                            int aantal = 0;
                            xkey += $"<h3>{key.Key}</h3>\r\n" +
                                    $"<div>\r\n" +
                                    $"<b>{key.Key}</b> heeft <b>{key.Value.Count}</b> keer aan {Naam} van {Omschrijving} gewerkt.<br>" +
                                    $"Tijd Gewerkt: <b>{Math.Round(key.Value.Sum(x => x.TijdGewerkt()), 2)} uur</b><br>" +
                                    $"Aantal Gemaakt: <b>{(aantal = key.Value.Sum(x => x.GetAantalGemaakt(ref tijd)))}</b><br>" +
                                    $"Gemiddeld P/u: <b>{(tijd > 0 ? (int) (aantal / tijd) : 0)} p/u</b><br>" +
                                    $"</div>\r\n";

                        }

                        xkey += $"<hr/>";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return new KeyValuePair<string, int>(xkey, xpers);
            });
        }

        public Task<KeyValuePair<string, int>> GetAanbevolenWerkplekkenHtml(bool inclheader)
        {
            return Task.Run(async () =>
            {
                string xret = string.Empty;
                int xpers = 0;
                try
                {
                    var xdict = await GetAanbevolenWerkplekken();
                    xpers = xdict.Count;
                    if (xdict.Count > 0)
                    {
                        var x1 = xdict.Count == 1 ? "werkplek" : "werkplekken";
                        var xtitle = $"{xdict.Count} Aanbevolen {x1} voor {this.Naam}";
                        if (inclheader)
                            xret = $"<html>\r\n" +
                                   $"<head>\r\n" +
                                   $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
                                   $"<Title>{ArtikelNr}</Title>\r\n" +
                                   $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                                   $"</head>\r\n" +
                                   $"<body style='background - color: {Color.DarkGreen.Name}; background-gradient: {Color.DarkGreen.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 50px 0px'>\r\n" +
                                   $"<h1 align='center' style='color: {Color.White.Name}'>\r\n" +
                                   $"       {xtitle}\r\n" +
                                   $"        <br/>\r\n" +
                                   $"        <span style=\'font-size: x-small;\'>ArtikelNr: {ArtikelNr}, ProductieNr: {ProductieNr}</span>\r\n " +
                                   $"</h1>\r\n" +
                                   $"<blockquote class='whitehole'>\r\n" +
                                   $"       <p style = 'margin-top: 0px' >\r\n" +
                                   $"<table border = '0' width = '100%' >\r\n" +
                                   $"<tr style = 'vertical-align: top;' >\r\n" +
                                   $"<td>\r\n";
                        else xret = "";
                        xret += $"<h3><u><b>{Naam}</b></u></h3>\r\n";
                        foreach (var key in xdict)
                        {
                            var x2 = key.Value.Count == 1 ? "klus" : "klusjes";
                            double tijd = key.Value.Sum(x => x.TijdAanGewerkt());
                            int aantal = 0;
                            xret += $"<h3>{key.Key}</h3>\r\n" +
                                    $"<div>\r\n" +
                                    $"<b>{key.Key}</b> is <b>{key.Value.Count}</b> keer gebruikt voor {Naam} van {Omschrijving}.<br>" +
                                    $"Tijd Gewerkt: <b>{Math.Round(tijd, 2)} uur</b><br>" +
                                    $"Aantal Gemaakt: <b>{(aantal = key.Value.Sum(x => x.AantalGemaakt))}</b><br>" +
                                    $"Gemiddeld P/u: <b>{(tijd > 0 ? (int) (aantal / tijd) : 0)} p/u</b><br>" +
                                    $"</div>\r\n";

                        }

                        xret += $"<hr/>";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return new KeyValuePair<string, int>(xret, xpers);
            });
        }

        public void UpdateAantal()
        {
            if (WerkPlekken != null)
            {
                var plekken = WerkPlekken.Where(x => x.Werk != null && x.Werk.Equals(this)).ToArray();
                if (plekken.Length > 0)
                {
                    var aantal = plekken.Sum(x => x.AantalGemaakt);
                    if (_gemaakt != aantal)
                    {
                        _gemaakt = aantal;
                        if (Parent != null)
                            Parent.LaatstAantalUpdate = DateTime.Now;
                        LaatstAantalUpdate = DateTime.Now;
                    }
                }
            }
        }

        public void SetAantalGemaakt(int aantal)
        {
            if (_gemaakt != aantal)
            {
                _gemaakt = aantal;
                if (Parent != null)
                    Parent.LaatstAantalUpdate = DateTime.Now;
            }

            if (WerkPlekken != null)
            {
                var plekken = WerkPlekken.Where(x => x.Werk != null && x.Werk.Equals(this)).ToArray();
                if (plekken.Length > 0)
                {
                    int totaalgemaakt = plekken.Sum(x => x.AantalGemaakt);
                    if ( totaalgemaakt != _gemaakt)
                    {
                        int verschil = 0;
                        bool add = false;
                        if(totaalgemaakt > _gemaakt)
                        {
                            verschil = totaalgemaakt - _gemaakt;
                        }
                        else if (_gemaakt > totaalgemaakt)
                        {
                            add = true;
                            verschil = _gemaakt - totaalgemaakt;
                        }

                        if (verschil == 0) return;
                        var split = verschil / plekken.Length;
                        var rest = verschil % plekken.Length;
                        verschil -= rest;
                        int index = 0;
                        while (verschil > 0)
                        {
                            if (verschil < split)
                                split = verschil;
                            var plek = plekken[index];
                            if (add)
                            {
                                plek.AantalGemaakt += split;
                                verschil -= split;
                            }
                            else
                            {
                                if (plek.AantalGemaakt < split)
                                {
                                    if (plek.AantalGemaakt < 0)
                                        plek.AantalGemaakt = 0;
                                    verschil -= plek.AantalGemaakt;
                                    plek.AantalGemaakt = 0;
                                }
                                else
                                {
                                    plek.AantalGemaakt -= split;
                                    verschil -= split;
                                }
                            }
                            plek.LaatstAantalUpdate = DateTime.Now;
                            index++;
                            if (index > plekken.Length - 1)
                                index = 0;
                            if (!add && plekken.Sum(x => x.AantalGemaakt) == 0) break;
                        }

                        if (add)
                            plekken[plekken.Length - 1].AantalGemaakt += rest;
                        else
                        {
                            var xplek = plekken.LastOrDefault(x => x.AantalGemaakt >= rest);
                            if (xplek == null) return;
                            xplek.AantalGemaakt -= rest;
                        }
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is string)
                return string.Equals(Path, ((string) obj), StringComparison.CurrentCultureIgnoreCase);
            if (!(obj is Bewerking bew))
                return false;

            return string.Equals(bew.Path, Path, StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }
}