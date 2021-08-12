using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Polenter.Serialization;
using ProductieManager.Rpm.Productie;
using Rpm.Misc;
using Rpm.Various;

namespace Rpm.Productie
{
    public class Klus
    {
        private ProductieState _state;

        //  readonly Dictionary<DateTime, DateTime> _tijden;

        public Klus()
        {
            AangemaaktOp = DateTime.Now;
            IsActief = true;
        }

        public Klus(Personeel pers, Bewerking bew, string werkPlek) : this()
        {
            if (bew == null || werkPlek == null || pers == null)
                throw new NullReferenceException();
            Naam = bew.Naam;
            ArtikelNr = bew.ArtikelNr;
            Omschrijving = bew.Omschrijving;
            ProductieNr = bew.ProductieNr;
            WerkPlek = werkPlek;
            Status = bew.State;
            PersoneelNaam = pers.PersoneelNaam;
            Tijden.WerkRooster = pers.WerkRooster;
        }

        public Klus(Personeel pers, WerkPlek werkPlek) : this(pers, werkPlek.Werk, werkPlek.Naam)
        {
            if (werkPlek == null)
                throw new NullReferenceException();
        }

        public string PersoneelNaam { get; set; }

        public string Path => $"{Werk}\\{WerkPlek}";

        public string Werk => $"{ProductieNr}\\{Naam}";
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public string WerkPlek { get; set; }
        public string ProductieNr { get; set; }
        public string ArtikelNr { get; set; }
        public int PerUur { get; set; }
        public DateTime AangemaaktOp { get; set; }

        [ExcludeFromSerialization]
        public Dictionary<DateTime, DateTime> GewerkteTijden
        {
            get => Tijden.ToDictionary();
            set
            {
                if (value != null && value.Count > 0)
                    foreach (var x in value)
                        Tijden.Add(new TijdEntry(x.Key, x.Value, Tijden.WerkRooster));
            }
        }

        public UrenLijst Tijden { get; set; } = new();

        public ProductieState Status
        {
            get => IsActief ? _state : _state == ProductieState.Gestart ? ProductieState.Gestopt : _state;
            set => _state = value;
        }

        public bool IsActief { get; set; }

        public bool IsNieuw => DateTime.Now.AddHours(4) >= AangemaaktOp;

        public TimeSpan TijdGewerkt(Dictionary<DateTime, DateTime> exclude, Rooster rooster)
        {
            var ex = exclude ?? new Dictionary<DateTime, DateTime>();
            return TimeSpan.FromHours(Tijden.TijdGewerkt(rooster,ex));
        }

        public double TijdGewerkt()
        {
            Rooster rooster = null; 
            var ex = new Dictionary<DateTime, DateTime>();
            var per = Manager.Database?.GetPersoneel(PersoneelNaam)?.Result;
            if (per != null)
                rooster = per.WerkRooster;
            if (per?.VrijeDagen != null && per.VrijeDagen.Count > 0)
                ex = per.VrijeDagen.ToDictionary();
            var werk = GetWerk();
            if (werk?.Bewerking != null)
            {
                if (werk.Plek != null)
                {
                    rooster ??= werk.Plek.Tijden?.WerkRooster;
                    var sts = werk.Plek.GetStoringen();
                    foreach (var st in sts)
                        if (ex.ContainsKey(st.Key))
                            ex[st.Key] = st.Value;
                        else ex.Add(st.Key, st.Value);
                }
            }

            return Tijden.TijdGewerkt(rooster, ex);
        }

        public int GetAantalGemaakt(ref double gewerkt)
        {
            Rooster rooster = null;
            var ex = new Dictionary<DateTime, DateTime>();
            var per = Manager.Database?.GetPersoneel(PersoneelNaam)?.Result;
            if (per != null)
                rooster = per.WerkRooster;
            if (per?.VrijeDagen != null && per.VrijeDagen.Count > 0)
                ex = per.VrijeDagen.ToDictionary();
            var werk = GetWerk();
            int aantal = 0;
            double werktijd = 0;
            if (werk?.Bewerking != null)
            {
                if (werk.Plek != null)
                {
                    rooster ??= werk.Plek.Tijden?.WerkRooster;
                    var sts = werk.Plek.GetStoringen();
                    foreach (var st in sts)
                        if (ex.ContainsKey(st.Key))
                            ex[st.Key] = st.Value;
                        else ex.Add(st.Key, st.Value);
                    werktijd = werk.Plek.TijdAanGewerkt();
                    aantal = werk.Plek.TotaalGemaakt;
                }
                else
                {
                    aantal = werk.Bewerking.TotaalGemaakt;
                    werktijd = werk.Bewerking.TijdAanGewerkt();
                }
            }
            var perstijd = Tijden.TijdGewerkt(rooster, ex);
            if (perstijd > werktijd)
                perstijd = werktijd;
            if (aantal > 0 && werktijd > 0)
            {
               
                var perc = (perstijd / werktijd) * 100;
                var xaantal = (int)(aantal * (perc / 100));
                aantal = xaantal;
            }

            gewerkt += perstijd;
            return aantal;
        }

        public TimeSpan TijdGewerkt(Dictionary<DateTime, DateTime> exclude, DateTime vanaf, DateTime tot,
            Rooster rooster)
        {
            var ex = exclude ?? new Dictionary<DateTime, DateTime>();
            return TimeSpan.FromHours(Tijden.TijdGewerkt(rooster,
                vanaf, tot, ex));
        }

        public DateTime GestartOp()
        {
            if (Tijden.Count == 0)
                return DateTime.Now;
            return Tijden.GetFirstStart();
        }

        public DateTime GestoptOp()
        {
            if (Tijden.Count == 0)
                return DateTime.Now;
            return Tijden.GetLastStop();
        }

        public bool Stop()
        {
            try
            {
                    Status = ProductieState.Gestopt;
                    var ent = Tijden.GetInUseEntry(true);
                    if (ent != null)
                        ent.Stop = DateTime.Now;
                    Tijden.Uren.ForEach(x => x.InUse = false);
                    Tijden.RemoveAllEmpty();
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Start()
        {
            try
            {
                if (IsActief)
                {
                    //if (newtime || Tijden.Count == 0)
                    UpdateTijdGewerkt(DateTime.Now, DateTime.Now, true);
                    Status = ProductieState.Gestart;
                    return true;
                }
                Stop();
                return Status == ProductieState.Gestart;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TijdEntry GetAvailibleTijdEntry()
        {
            if (Tijden == null) Tijden = new UrenLijst();
            return Tijden.GetAvailibleEntry();
        }

        public bool MeldGereed()
        {
            try
            {
                if (Status != ProductieState.Verwijderd && Status != ProductieState.Gereed)
                {
                    //if (Status == ProductieState.Gestart)
                    Stop();
                    Status = ProductieState.Gereed;
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ZetActief(bool actief, bool isgestart)
        {
            if (Status == ProductieState.Gestart && !actief)
                Stop();
            else if (actief && Status == ProductieState.Gestopt && isgestart)
                Start();
            IsActief = actief;
            return IsActief;
        }

        public void UpdateTijdGewerkt(DateTime start, DateTime stop, bool isactief)
        {
            Tijden?.UpdateTijdGewerkt(start, stop, isactief);
        }

        public void UpdateTijdGewerkt(TijdEntry entry)
        {
            Tijden?.UpdateTijdGewerkt(entry);
        }

        public bool UpdateFrom(Klus klus)
        {
            try
            {
                PersoneelNaam = PersoneelNaam;
                IsActief = klus.IsActief;
                Status = klus.Status;
                PerUur = klus.PerUur;
                ArtikelNr = klus.ArtikelNr;
                Omschrijving = klus.Omschrijving;
                Tijden.UpdateLijst(klus.Tijden);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Werk GetWerk()
        {
            return Productie.Werk.FromPath(Path);
        }

        public Werk GetWerk(ProductieFormulier form)
        {
            return Productie.Werk.FromPath(Path,form);
        }

        private Task<WerkPlek> GetWerkPlek(bool createnew)
        {
            return Task.Run(async () =>
            {
                var pair = GetWerk();
                var prod = pair.Formulier;
                var bew = pair.Bewerking;
                if (prod != null && bew != null && WerkPlek != null)
                {
                    var wp = bew.WerkPlekken.FirstOrDefault(x => x.Naam.ToLower() == WerkPlek.ToLower());
                    if (wp == null && createnew && PersoneelNaam != null)
                    {
                        var pers = await Manager.Database.GetPersoneel(PersoneelNaam);
                        if (pers != null)
                        {
                            wp = new WerkPlek(pers, pers.Werkplek, bew);
                            bew.WerkPlekken.Add(wp);
                            await bew.UpdateBewerking(null, $"{wp.Path} werkplek aangemaakt");
                            if (bew.State == ProductieState.Gestart)
                            {
                                var klus = pers.Klusjes.GetKlus(wp.Path);
                                if (klus == null)
                                {
                                    pers.ReplaceKlus(new Klus(pers, wp));
                                    await Manager.Database.UpSert(pers, $"{bew.Path} klus aangemaakt op {wp.Naam}");
                                }
                            }
                        }
                    }

                    return wp;
                }

                return null;
            });
        }

        public Task<bool> RemoveWerk()
        {
            return Task.Run(async () =>
            {
                var wp = await GetWerkPlek(false);
                if (wp != null && PersoneelNaam != null)
                {
                    var removed = wp.Personen.RemoveAll(x => string.Equals(x.PersoneelNaam, PersoneelNaam, StringComparison.CurrentCultureIgnoreCase));
                    if (removed > 0)
                    {
                        var xvalue = removed == 1 ? "medewerker" : "medewerkers";
                        await wp.Werk.UpdateBewerking(null, $"[{wp.Path}]{removed} {xvalue} eruit gehaald");
                        return true;
                    }
                }

                return false;
            });
        }

        public override bool Equals(object obj)
        {
            if (obj is string value)
                return string.Equals(Path, value, StringComparison.CurrentCultureIgnoreCase);
            if (obj is Klus klus)
                return string.Equals(klus.Path, Path, StringComparison.CurrentCultureIgnoreCase) &&
                       string.Equals(klus.PersoneelNaam, PersoneelNaam, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            if (PersoneelNaam == null || Path == null) return base.GetHashCode();
            return Path.GetHashCode() & PersoneelNaam.GetHashCode();
        }
    }
}