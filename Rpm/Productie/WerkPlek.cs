using Polenter.Serialization;
using Rpm.Misc;
using Rpm.Productie.AantalHistory;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Rpm.Productie
{
    [DataContract]
    public class WerkPlek
    {
        private int _aantalgemaakt;

        private DateTime _gestartop;
        private DateTime _gestoptop;

        private int _peruur;

        public WerkPlek(Personeel[] personen, string werkplek, Bewerking werk)
        {
            Personen = new List<Personeel>();
            if (personen is {Length: > 0}) Personen.AddRange(personen);
            Naam = werkplek ?? "N.V.T";
            Werk = werk;
        }

        public WerkPlek(Personeel persoon, string werkplek, Bewerking werk) : this(new[] {persoon}, werkplek,
            werk)
        {
        }

        public WerkPlek(string naam, Bewerking bew) : this(Array.Empty<Personeel>(), naam, bew)
        {
        }

        public WerkPlek()
        {
        }

        public int Id { get; set; }

        
        public Bewerking Werk { get; set; }

        public UrenLijst Tijden { get; set; } = new UrenLijst();

        public List<Personeel> Personen { get; set; }

        private List<Storing> _Storingen = new List<Storing>();

        public List<Storing> Storingen
        {
            get => _Storingen;
            set
            {
                _Storingen = value;
                if (value is {Count: > 0})
                    _Storingen.ForEach(x => x.Plek = this);
            }
        }

        public string Naam { get; set; }

        public NotitieEntry Note { get; set; }

        public string Notitie => Note?.Notitie?? string.Empty;

        public DateTime LaatstAantalUpdate { get; set; } = DateTime.Now;

        public string Path => string.IsNullOrEmpty(Werk?.Path) ? Naam : System.IO.Path.Combine(Werk.Path, Naam);
        public double Activiteit { get; set; } = 100;

        public string Omschrijving => Werk?.Omschrijving;
        public string WerkNaam => Werk?.Naam;
        public string ArtikelNr => Werk?.ArtikelNr;
        public string ProductieNr => Werk?.ProductieNr;
        public AantalHistory.AantallenRecords AantalHistory { get; private set; } = new AantallenRecords();
        public DateTime FriendlyVerwachtGereed =>
            Werk?.VerwachtDatumGereed() ?? DateTime.Now;

        [ExcludeFromSerialization]
        public int PerUur => GetPerUur();

        public int GetPerUur()
        {
            try
            {
                int aantal = TotaalGemaakt;
                double tijd = TijdGewerkt;
                if (TijdGewerkt > 0)
                    return (int)(aantal / tijd);
                return aantal;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        [ExcludeFromSerialization]
        public double PerUurBase => Werk?.PerUur ?? 0;

        // public double LastTijdGewerkt { get; set; }

        public double TijdGewerkt => TijdAanGewerkt();

        public virtual int AantalGemaakt
        {
            get => _aantalgemaakt;
            set
            {
                if (value != _aantalgemaakt)
                {
                    _aantalgemaakt = value;
                    Werk?.UpdateAantal();
                    AantalHistory?.UpdateAantal(value,Tijden);
                }
            }
        }

        public int ActueelAantalGemaakt
        {
            get
            {
                double xdt = 0;
                var xt = GetActueelAantalGemaakt(ref xdt);
                return xt;
            } 
        }

        public int GetAantalGemaakt(DateTime start, DateTime stop,ref double tijd ,bool predict)
        {
           
            if (AantalHistory == null)
            {
                return _aantalgemaakt;
            }

            int multiply = 1;
            if (Werk is {IsBemand: true})
            {
                multiply = Personen.Where(x => x.IngezetAanKlus(Werk, true, out _)).ToList().Count;
            }
            return AantalHistory.AantalGemaakt(start, stop, ref tijd, Tijden, predict, multiply, - 1, GetStoringen());
        }

        public int GetActueelAantalGemaakt(ref double tijd)
        {
            if (AantalHistory == null)
            {
                return _aantalgemaakt;
            }

            int xmultyply = (Werk?.IsBemand ?? false) ? Personen.Count : 1;
            return AantalHistory.AantalGemaakt(Tijden, ref tijd,Werk is {State: ProductieState.Gestart },xmultyply, -1,GetStoringen());
        }

        public int TotaalGemaakt
        {
            get
            {
                if (Werk != null)
                {
                    var xant = Werk.DeelGereedMeldingen
                        .Where(x => string.Equals(Naam, x.WerkPlek, StringComparison.CurrentCultureIgnoreCase))
                        .Sum(x => x.Aantal);
                    return xant + AantalGemaakt;
                }

                return AantalGemaakt;
            }
        }

        public string PersonenLijst
        {
            get
            {
                var value = "Geen Personeel";
                if (Personen is {Count: > 0})
                {
                    value = string.Join(", ", Personen.Select(x => x.PersoneelNaam));
                }

                return value;
            }
        }

        public DateTime LeverDatum => Werk?.LeverDatum ?? DateTime.Now;

        public DateTime TijdGestart
        {
            get => GestartOp();
            set => _gestartop = value;
        }

        public DateTime TijdGestopt
        {
            get => GestoptOp();
            set => _gestoptop = value;
        }

        public DateTime GestartOp()
        {
            if (Werk == null)
                return _gestartop;
            if (!Werk.IsBemand)
                return Tijden.GetFirstStart();
            //return _gestartop;
            if (Personen == null || Personen.Count == 0)
            {
                return _gestartop;
            }

            var dt = new DateTime();
            foreach (var per in Personen)
                if (per.GestartOp(this) < dt || dt.IsDefault())
                    dt = per.GestartOp(this);
            return dt;
        }

        public DateTime GestartOp(TijdEntry bereik)
        {
            if (Werk.IsBemand)
            {
                var dt = new DateTime();
                foreach (var per in Personen)
                {
                    var xgestart = per.GestartOp(this);
                    var xgestopt = per.GestoptOp(this);
                    var bereikstop = bereik.Stop.TimeOfDay == new TimeSpan()? DateTime.Now : bereik.Stop;
                    var xb = new TijdEntry(xgestart, xgestopt);
                    var t = xb.CreateRange(bereik.Start, bereikstop, Tijden.WerkRooster, Tijden.SpecialeRoosters);
                    if (t == null) continue;
                    if (t.Start < dt || dt.IsDefault())
                        dt = t.Start;

                }

                return dt;
            }

            if (Tijden?.Uren == null)
                return _gestartop;
            DateTime xret = default;
            foreach (var uur in Tijden.Uren)
            {
                var t = uur.CreateRange(bereik.Start, bereik.Stop, Tijden.WerkRooster, Tijden.SpecialeRoosters);
                if (t == null) continue;
                if (xret.IsDefault() || t.Start < xret)
                    xret = t.Start;
            }

            return xret;
        }

        public DateTime GestoptOp()
        {
            if (Werk == null)
                return _gestoptop;
            if (!Werk.IsBemand)
                return Tijden.GetLastStop();
            //return _gestoptop;
            if (Personen == null || Personen.Count == 0)
            {
                return _gestoptop;
            }

            var dt = new DateTime();
            foreach (var per in Personen)
                if (per.GestoptOp(this) > dt)
                    dt = per.GestoptOp(this);
            return dt;
        }

        public DateTime GestoptOp(TijdEntry bereik)
        {
            if (Werk.IsBemand)
            {
                var dt = new DateTime();
                foreach (var per in Personen)
                {
                    var xgestart = per.GestartOp(this);
                    var xgestopt = per.GestoptOp(this);
                    var xb = new TijdEntry(xgestart, xgestopt);
                    var bereikstop = bereik.Stop.TimeOfDay == new TimeSpan() ? DateTime.Now : bereik.Stop;
                    var t = xb.CreateRange(bereik.Start, bereikstop, Tijden.WerkRooster, Tijden.SpecialeRoosters);
                    if (t == null) continue;
                    if (t.Stop > dt)
                        dt = t.Stop;

                }

                return dt;
            }
            if (Tijden?.Uren == null)
                return _gestoptop;
            DateTime xret = default;
            foreach (var uur in Tijden.Uren)
            {
                var t = uur.CreateRange(bereik.Start, bereik.Stop, Tijden.WerkRooster, Tijden.SpecialeRoosters);
                if (t == null) continue;
               
                if (t.Stop > xret)
                    xret = t.Stop;
            }
            return xret;
        }


        public void UpdateStoringWerkplekken()
        {
            if (Storingen is {Count: > 0})
                Storingen.ForEach(x => x.Plek = this);
        }

        public bool IsActief()
        {
            if (Personen == null || Personen.Count == 0)
                return false;
            return Personen.Any(x => x.IngezetAanKlus(Path, true, out _));
        }

        public bool AddPersoon(Personeel persoon, Bewerking werk)
        {
            if (Personen == null || werk == null)
                return false;
            Werk = werk;
            persoon.Werkplek = Naam;
            var xpers = Personen.FirstOrDefault(t =>
                string.Equals(persoon.PersoneelNaam, t.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase));
            if (xpers != null)
            {
                var klus = persoon.Klusjes.FirstOrDefault(k =>
                    string.Equals(werk.Path + "\\" + Naam, k.Path, StringComparison.CurrentCultureIgnoreCase));

                if (klus != null)
                {
                    xpers.ReplaceKlus(klus);
                }
                else
                {
                    xpers.ReplaceKlus(new Klus(persoon, this));
                }
            }
            else
            {
                var klus = persoon.Klusjes.FirstOrDefault(k =>
                    string.Equals(werk.Path + "\\" + Naam, k.Path, StringComparison.CurrentCultureIgnoreCase));

                if (klus != null)
                {
                    klus.IsActief = true;
                    persoon.ReplaceKlus(klus);
                }
                else
                {
                    persoon.ReplaceKlus(new Klus(persoon, this));
                }

                Personen.Add(persoon);
            }

            //Klus klus = persoon.Klusjes.GetKlus(Path) ?? new Klus(persoon, this);
            //klus.IsActief = true;
            //persoon.UpdateKlus(klus);
            UpdateTijdGestart();

            return true;
        }

        public void UpdateTijdGestart()
        {
            foreach (var pers in Personen)
            {
                var klus = pers.Klusjes.GetKlus(Path);
                if (klus != null)
                    foreach (var wt in klus.Tijden.Uren)
                        if (TijdGestart > wt.Start)
                            TijdGestart = wt.Start;
            }
        }

        public double GetPerUur(TijdEntry bereik)
        {
            if (AantalHistory.Aantallen.Count > 0)
            {
                var xtijd = 0d;
                var gemaakt = GetAantalGemaakt(bereik.Start, bereik.Stop, ref xtijd, false);

                var pu = xtijd > 0 && gemaakt > 0 ? gemaakt / xtijd : 0;
                if (Werk is {IsBemand: true})
                {
                    var xcount = Personen.Count;
                    if (xcount > 0)
                        pu /= xcount;
                }
                return Math.Round(pu, 0);
            }

            return 0;
        }

        public bool UpdateStoring(Storing storing)
        {
            if (Storingen == null || storing == null) return false;
            storing.WerkRooster = Tijden.WerkRooster?.CreateCopy() ?? Manager.Opties.GetWerkRooster();
            if (Storingen.Count == 0)
            {
                Storingen.Add(storing);
            }
            else
            {
                var xold = Storingen.FirstOrDefault(x => x.Equals(storing));
                if (xold == null)
                {
                    Storingen.Add(storing);
                }
                else
                {
                    Storingen.Remove(xold);
                    Storingen.Add(storing);
                }
            }

            return true;
        }

        public void UpdateWerkRooster(Rooster rooster, bool alleengestart,bool updatetijdenrooster, bool editpersoneelklusjes, bool savepersoneelklusjes,bool updatewerkplek, bool showsavenotofication, bool checkforspecialrooster)
        {
            if (Werk == null || (alleengestart && Werk.State != ProductieState.Gestart)) return;
            rooster ??= Tijden.WerkRooster??Manager.Opties.GetWerkRooster();
            if (Werk.IsBemand)
            {
                var actiefroosters = Personen.Where(x => x.Actief && x.WerkRooster != null)
                    .Select(x => x.WerkRooster).ToList();
                if (actiefroosters.Count > 0)
                {
                    rooster.GebruiktPauze = actiefroosters.All(x => x.GebruiktPauze);
                    actiefroosters = actiefroosters.OrderBy(x => x.StartWerkdag).ToList();
                    rooster.StartWerkdag = actiefroosters[0].StartWerkdag;
                    actiefroosters = actiefroosters.OrderBy(x => x.EindWerkdag).ToList();
                    rooster.EindWerkdag = actiefroosters[actiefroosters.Count - 1].EindWerkdag;
                }
            }

            if (updatetijdenrooster)
                Tijden.UpdateUrenRooster(checkforspecialrooster, rooster);
            for (int i = 0; i < Personen.Count; i++)
            {
                var pers = Personen[i];
                pers.WerktAan = Werk.Path;
                pers.Werkplek = Naam;
                if (editpersoneelklusjes)
                {
                        if (pers.IngezetAanKlus(Path, true, out var klusjes))
                        {
                            Personeel dbpers = null;
                            if (savepersoneelklusjes)
                                dbpers = Manager.Database.GetPersoneel(pers.PersoneelNaam).Result;
                            bool save = false;
                            foreach (var klus in klusjes)
                            {
                                klus.Tijden.WerkRooster = rooster;
                                klus.Tijden.SpecialeRoosters = Tijden.SpecialeRoosters;
                                klus.Tijden.SetUren(Tijden.Uren.ToArray(), Werk.State == ProductieState.Gestart,true);
                                save |= dbpers?.ReplaceKlus(klus) ?? false;
                            }

                            if (save)
                                save |= Manager.Database.UpSert(dbpers, $"{dbpers.PersoneelNaam} Werktijden aangepast.",showsavenotofication)
                                    .Result;
                        }
                }
            }


            for (int i = 0; i < Storingen.Count; i++)
            {
                var st = Storingen[i];
                if (st.IsVerholpen) continue;
                st.WerkRooster = rooster;
            }

            if (updatewerkplek)
                UpdateWerkplek(false);
        }

        public bool UpdateWerkplek(bool updaterooster)
        {
            try
            {

                Personen.RemoveAll(
                    t => !string.Equals(t.WerktAan, Werk.Path, StringComparison.CurrentCultureIgnoreCase));
                if (Werk.State == ProductieState.Gestart)
                {
                    if (!Tijden.IsActief && IsActief())
                        Tijden.SetStart();
                    else if (Tijden.IsActief && !IsActief())
                        Tijden.SetStop();
                }
                else if (Tijden.IsActief)
                    Tijden.SetStop();
                if (updaterooster)
                    Tijden?.UpdateUrenRooster(false, null);
                //AantalHistory?.SetActive((Werk?.State ?? ProductieState.Gestopt) == ProductieState.Gestart);
                CalculatePerUur(false);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public int AddPersonen(Personeel[] personen, Bewerking werk)
        {
            if (Personen == null || werk == null || personen == null)
                return 0;
            var xreturn = 0;
            foreach (var persoon in personen)
                if (AddPersoon(persoon, werk))
                    xreturn++;
            return xreturn;
        }

        public async void CalculatePerUur(bool updatedb)
        {
            var xtotaal = TotaalGemaakt;
            if (Personen?.Count > 0 && Werk is {State: ProductieState.Gestart} && xtotaal > 0)
            {
                var tijd = TijdAanGewerkt();
                for (int i = 0; i < Personen.Count; i++)
                {
                    var per = Personen[i];
                    var tg = per.TijdAanGewerkt(GetStoringen(), this, per.WerkRooster ?? Tijden?.WerkRooster)
                        .TotalHours;
                    if (tg > 0)
                    {
                        var klus = per.Klusjes.GetKlus(Path);
                        if (klus == null) continue;
                        //pak het percentage van hoveel tijd gewerkt is.
                        var percentage = tg / tijd * 100;
                        var eachp = percentage > 0 ? (int) ((xtotaal / 100) * percentage) : 0;
                        var peruur = tg == 0 ? eachp : (int) (eachp / tg);
                        var same = peruur == per.PerUur;
                        klus.PerUur = peruur;
                        if (!same || tg > 0)
                        {
                            var xdbpers = await Manager.Database.GetPersoneel(per.PersoneelNaam);
                            if (xdbpers == null) continue;
                            xdbpers.ReplaceKlus(klus);
                            per.VrijeDagen = xdbpers.VrijeDagen;
                            xdbpers.PerUur = peruur;
                            if (per.WerkRooster == null)
                                per.WerkRooster = xdbpers.WerkRooster;
                            if (updatedb)
                                await Manager.Database.UpSert(xdbpers, $"{xdbpers.PersoneelNaam} productie update.");
                            else
                                Manager.PersoneelChanged(this, xdbpers);
                        }
                    }
                }
            }
        }

        public double TijdAanGewerkt(bool includestoringen = true, bool calculatecombis = true)
        {
            double tijd = 0;
            Dictionary<DateTime, DateTime> xstoringen = new Dictionary<DateTime, DateTime>();
            if (includestoringen)
                xstoringen = GetStoringen();
            if (Werk is {IsBemand: false})
                tijd = Tijden.TijdGewerkt(null, xstoringen);
            else
                tijd = Personen.Sum(x =>
                    x.TijdAanGewerkt(xstoringen, this, x.WerkRooster ?? Tijden?.WerkRooster).TotalHours);
            if (Werk != null && Werk.Combies.Count > 0 && calculatecombis)
            {
                for (int i = 0; i < Werk.Combies.Count; i++)
                {
                    var combi = Werk.Combies[i];
                    var xstop = combi.IsRunning ? DateTime.Now : combi.Periode.Stop;
                    var combitijd = TijdAanGewerkt(combi.Periode.Start, xstop, true, false);
                    tijd = (tijd - combitijd) + ((combitijd / 100) * (100 - combi.Activiteit));
                }
            }

            if (tijd <= 0) return 0;
            return Math.Round(((tijd / 100) * Activiteit), 2);
        }

        public double TijdAanGewerkt(DateTime vanaf, DateTime tot, bool includestoringen, bool calculatecombis = true)
        {
            double tijd = 0;
            Dictionary<DateTime, DateTime> xstoringen = new Dictionary<DateTime, DateTime>();
            if (includestoringen)
                xstoringen = GetStoringen();
            if (Werk is {IsBemand: false})
                tijd = Tijden.TijdGewerkt(null, vanaf, tot, xstoringen);
            else
                tijd = Personen.Sum(x =>
                    x.TijdAanGewerkt(xstoringen, this, vanaf, tot, x.WerkRooster ?? Tijden?.WerkRooster)
                        .TotalHours);
            if (Werk != null && Werk.Combies.Count > 0 && calculatecombis)
            {
                for (int i = 0; i < Werk.Combies.Count; i++)
                {
                    var combi = Werk.Combies[i];
                    var xstart = combi.Periode.Start < vanaf ? vanaf : combi.Periode.Start;
                    var xstop = combi.IsRunning ? DateTime.Now : combi.Periode.Stop;
                    if (xstop > tot)
                        xstop = tot;
                    var combitijd = TijdAanGewerkt(xstart, xstop, true, false);
                    tijd = (tijd - combitijd) + ((combitijd / 100) * (100 - combi.Activiteit));
                }
            }

            if (tijd <= 0) return 0;
            return Math.Round(((tijd / 100) * Activiteit), 2);
        }

        public double LaatsAantalUpdateMinutes()
        {
            var xstoringen = GetStoringen();
            var tijd = TimeSpan.FromHours(Tijden.TijdGewerkt(null, LaatstAantalUpdate, DateTime.Now, xstoringen)).TotalMinutes;
            return tijd;
        }

        public double ControleRatio()
        {
            var xvalue = AantalHistory.Aantallen.Where(x => x.Gemaakt > 0).ToList();
            var xtijd = TijdGewerkt;
            if (Werk is {IsBemand: true})
            {
                if (Personen is {Count: > 0})
                    xtijd /= Personen.Count;
            }
            var xratio = xtijd.GetPercentageDifference(xvalue.Count * 1.5d);
            return xratio;
        }

        public bool NeedsAantalUpdate(int interval)
        {
            if (Storingen != null && Storingen.Any(x => !x.IsVerholpen)) return false;
            var xtijd = LaatsAantalUpdateMinutes();
            return xtijd > interval;
        }

        public bool HeeftGewerkt(TijdEntry bereik)
        {
            try
            {
                if (Tijden == null || Tijden.Count == 0) return false;
                return Tijden.ContainsBereik(bereik);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Dictionary<DateTime, DateTime> GetStoringen()
        {
            var storingen = new Dictionary<DateTime, DateTime>();
            Storingen.ForEach(x => storingen[x.Gestart] = x.IsVerholpen ? x.Gestopt : DateTime.Now);
            return storingen;
        }

        public bool RemovePersoon(string naam)
        {
            return Personen?.RemoveAll(x =>
                string.Equals(x.PersoneelNaam, naam, StringComparison.CurrentCultureIgnoreCase)) > 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is WerkPlek plek)
                return string.Equals(plek.Path, Path, StringComparison.CurrentCultureIgnoreCase);
            if (obj is string s)
                return string.Equals(s, Path, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }
}