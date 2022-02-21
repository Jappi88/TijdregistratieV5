using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms;
using Polenter.Serialization;
using Rpm.Misc;
using Rpm.SqlLite;
using Rpm.Various;

namespace Rpm.Productie
{
    public class Personeel
    {
        private DateTime _gestartop;
        private DateTime _gestoptop;

        public Personeel()
        {
            Efficientie = 100;
            PersoneelNaam = "N.V.T";
        }

        public Personeel(string naam, string werkplek) : this(naam)
        {
            Werkplek = werkplek;
        }

        public Personeel(string naam) : this()
        {
            PersoneelNaam = naam;
        }

        public List<Klus> Klusjes { get; set; } = new();

        public UserChange LastChanged { get; set; }
        public string WerktAan { get; set; }
        public string BewerkingNaam => WerktAan == null ? "N.V.T" : WerktAan.Split('\\').Last();

        [ExcludeFromSerialization] public int ImageIndex { get; set; }

        public string PersoneelNaam { get; set; }
        public string Afdeling { get; set; }
        public string Werkplek { get; set; }
        public double Efficientie { get; set; } = 100;
        public int PerUur { get; set; }
        public DateTime TijdIngezet { get; set; }

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

        public Rooster WerkRooster { get; set; }

        public UrenLijst VrijeDagen { get; set; } = new();
        public bool IsBezig => Klusjes != null && Klusjes.Any(x => x.Status == ProductieState.Gestart);
        public bool IsUitzendKracht { get; set; }
        public bool Actief { get; set; }
        public bool IsAanwezig { get; set; } = true;
        public List<PersoneelTaakEntry> Taken { get; set; }
        public double TotaalTijdGewerkt => Math.Round(GetTotaalTijdGewerkt(null, false).TotalHours, 2);
        public double TijdGewerkt => Math.Round(GetTotaalTijdGewerkt(null, true).TotalHours, 2);

        public static Personeel CreateNew(Personeel persoon)
        {
            var x = new Personeel();
            return persoon.CopyTo(ref x);
        }

        //de tijd dat die persoon heeft gewerkt
        public TimeSpan TijdAanGewerkt(Dictionary<DateTime, DateTime> exclude, Klus klus, Rooster rooster)
        {
            if (klus == null) return new TimeSpan();
            rooster ??= WerkRooster ?? klus.Tijden?.WerkRooster ?? Manager.Opties.GetWerkRooster();
            var storingen = new Dictionary<DateTime, DateTime>();
            if (exclude is {Count: > 0})
                storingen = exclude;

            if (VrijeDagen == null || VrijeDagen.Count <= 0) return klus.TijdGewerkt(storingen, rooster);
            foreach (var v in VrijeDagen.Uren)
                if (storingen.ContainsKey(v.Start))
                    storingen[v.Start] = v.Stop;
                else
                    storingen.Add(v.Start, v.Stop);


            return klus.TijdGewerkt(storingen, rooster);
        }

        public TimeSpan TijdAanGewerkt(Dictionary<DateTime, DateTime> exclude, Klus klus, DateTime vanaf, DateTime tot,
            Rooster rooster)
        {
            if (klus == null) return new TimeSpan();
            rooster ??= WerkRooster ?? klus.Tijden?.WerkRooster ?? Manager.Opties.GetWerkRooster();
            var storingen = new Dictionary<DateTime, DateTime>();
            if (exclude is {Count: > 0})
                storingen = exclude;

            if (VrijeDagen == null || VrijeDagen.Count <= 0) return klus.TijdGewerkt(storingen, vanaf, tot, rooster);
            foreach (var v in VrijeDagen.Uren)
                if (storingen.ContainsKey(v.Start))
                    storingen[v.Start] = v.Stop > tot ? tot : v.Stop;
                else
                    storingen.Add(v.Start, v.Stop > tot ? tot : v.Stop);


            return klus.TijdGewerkt(storingen, vanaf, tot, rooster);
        }

        public TimeSpan TijdAanGewerkt(Dictionary<DateTime, DateTime> exclude, string klus, Rooster rooster)
        {
            if (Klusjes == null) return new TimeSpan();
            var xklus = Klusjes.FirstOrDefault(x => x.Equals(klus));
            if (xklus == null) return new TimeSpan();
            return TijdAanGewerkt(exclude, xklus,
                rooster ?? WerkRooster ?? xklus.Tijden?.WerkRooster ?? Manager.Opties.GetWerkRooster());
        }

        public TimeSpan TijdAanGewerkt(Dictionary<DateTime, DateTime> exclude, string klus, DateTime vanaf,
            DateTime tot, Rooster rooster)
        {
            if (Klusjes == null) return new TimeSpan();
            var xklus = Klusjes.FirstOrDefault(x => x.Equals(klus));
            if (xklus == null) return new TimeSpan();
            return TijdAanGewerkt(exclude, xklus, vanaf, tot,
                rooster ?? WerkRooster ?? xklus.Tijden?.WerkRooster ?? Manager.Opties.GetWerkRooster());
        }

        public TimeSpan TijdAanGewerkt(Dictionary<DateTime, DateTime> exclude, WerkPlek werkplek, Rooster rooster)
        {
            if (werkplek == null)
                return new TimeSpan();
            return TijdAanGewerkt(exclude, werkplek.Path,
                rooster ?? WerkRooster ?? werkplek?.Tijden?.WerkRooster ?? Manager.Opties.GetWerkRooster());
        }

        public TimeSpan TijdAanGewerkt(Dictionary<DateTime, DateTime> exclude, WerkPlek werkplek, DateTime vanaf,
            DateTime tot, Rooster rooster)
        {
            if (werkplek == null)
                return new TimeSpan();
            return TijdAanGewerkt(exclude, werkplek.Path, vanaf, tot,
                rooster ?? WerkRooster ?? werkplek?.Tijden?.WerkRooster ?? Manager.Opties.GetWerkRooster());
        }

        public TimeSpan GetTotaalTijdGewerkt(Dictionary<DateTime, DateTime> exclude, bool gestart)
        {
            var xklusjes = gestart ? Klusjes?.Where(x => x.Status == ProductieState.Gestart).ToList() : Klusjes;
            return xklusjes == null
                ? new TimeSpan()
                : TimeSpan.FromHours(xklusjes.Sum(x =>
                    TijdAanGewerkt(exclude, x, x.Tijden?.WerkRooster ?? WerkRooster ?? Manager.Opties.GetWerkRooster())
                        .TotalHours));
        }

        public double TijdExtraNodig(double uren)
        {
            return uren * (Efficientie / 100);
        }

        public DateTime GestartOp(WerkPlek plek)
        {
            var klus = Klusjes.GetKlus(plek.Path);
            if (klus == null)
                return _gestartop;
            return klus.Tijden.GetFirstStart();
        }

        public DateTime GestoptOp(WerkPlek plek)
        {
            var klus = Klusjes.GetKlus(plek.Path);
            if (klus == null)
                return _gestoptop;
            return klus.Tijden.GetLastStop();
        }

        public DateTime GestartOp()
        {
            var xreturn = default(DateTime);
            for (var i = 0; i < Klusjes.Count; i++)
            {
                var klus = Klusjes[i];
                if (klus.Status != ProductieState.Gestart)
                    continue;
                var start = klus.Tijden.GetFirstStart();
                if (start < xreturn || xreturn.IsDefault())
                    xreturn = start;
            }

            return xreturn;
        }

        public DateTime GestoptOp()
        {
            var xreturn = default(DateTime);
            for (var i = 0; i < Klusjes.Count; i++)
            {
                var klus = Klusjes[i];
                if (klus.Status == ProductieState.Gestart)
                    return DateTime.Now;

                var stop = klus.Tijden.GetLastStop();
                if (stop > xreturn)
                    xreturn = stop;
            }

            return xreturn;
        }

        public bool IsVrij(DateTime time)
        {
            if (VrijeDagen == null || VrijeDagen.Count == 0)
                return false;
            foreach (var v in VrijeDagen.Uren)
                if (time >= v.Start && time <= v.Stop)
                    return true;
            return false;
        }

        public double IsVrijOver()
        {
            var firststart = new DateTime();
            if (VrijeDagen is {Count: > 0})
                foreach (var v in VrijeDagen.Uren)
                    if (firststart.IsDefault() || firststart < v.Start)
                        firststart = v.Start;
            if (firststart.IsDefault())
                return -1;
            return Math.Round(Werktijd.TijdGewerkt(DateTime.Now, firststart, WerkRooster, null).TotalHours, 2);
        }

        public bool IsVrijOver(TimeSpan time)
        {
            return IsVrij(DateTime.Now.Add(time));
        }

        public TimeSpan TijdVrij()
        {
            var time = new TimeSpan();
            if (VrijeDagen is {Count: > 0})
                foreach (var v in VrijeDagen.Uren)
                    time = time.Add(
                        Werktijd.TijdGewerkt(new TijdEntry(v.Start, v.Stop, WerkRooster), WerkRooster, null));
            return time;
        }

        public static async Task<bool> UpdateKlusjes(IWin32Window owner, Personeel persoon, string naam = null)
        {
            try
            {
                naam ??= persoon.PersoneelNaam;
                var rooster = persoon.WerkRooster;
                if (rooster == null) return true;
                var flag = persoon.Klusjes.Any(x =>
                    x.Status is ProductieState.Gestart or ProductieState.Gestopt &&
                    !rooster.SameTijden(x.Tijden.WerkRooster));
                var result = flag
                    ? XMessageBox.Show(
                        owner, $"Wil je alle loopende klusjes van {persoon.PersoneelNaam} ook updaten?",
                        "Personeel Klusjes Updaten", MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question)
                    : DialogResult.No;
                if (result == DialogResult.Cancel) return false;
                if (result == DialogResult.Yes)
                    foreach (var klus in persoon.Klusjes)
                    {
                        if (klus.Status == ProductieState.Verwijderd ||
                            klus.Status == ProductieState.Gereed) continue;
                        var werk = klus.GetWerk();
                        if (werk == null || !werk.IsValid || werk.Bewerking == null || werk.Plek == null) continue;
                        var changed = false;
                        klus.PersoneelNaam = persoon.PersoneelNaam.Trim();
                        klus.Tijden.WerkRooster = persoon.WerkRooster;
                        foreach (var xper in werk.Plek.Personen)
                        {
                            if (!string.Equals(naam, xper.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase))
                                continue;
                            xper.PersoneelNaam = persoon.PersoneelNaam.Trim();
                            xper.WerkRooster = persoon.WerkRooster;
                            xper.VrijeDagen = persoon.VrijeDagen;
                            xper.Efficientie = persoon.Efficientie;
                            xper.IsAanwezig = persoon.IsAanwezig;
                            xper.IsUitzendKracht = persoon.IsUitzendKracht;
                            foreach (var xklus in xper.Klusjes)
                            {
                                xklus.PersoneelNaam = xper.PersoneelNaam;
                                xklus.Tijden.WerkRooster = xper.WerkRooster;
                                var xactive = xklus.Tijden.GetInUseEntry(false);
                                if (xactive != null)
                                    xactive.WerkRooster = xper.WerkRooster;
                                xklus.WerkPlek = werk.Plek.Naam;
                                xklus.Naam = werk.Bewerking.Naam;
                                xklus.ProductieNr = werk.Bewerking.ProductieNr;
                            }

                            changed = true;
                        }

                        if (changed)
                            await werk.Bewerking.UpdateBewerking(null,
                                $"{persoon.PersoneelNaam} Werkrooster aangepast op klus {klus.Path}");
                    }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool IngezetAanKlus(Bewerking bew, bool onlyactive, out List<Klus> klusjes)
        {
            klusjes = new List<Klus>();
            if (bew == null || Klusjes == null)
                return false;
            if (onlyactive)
                klusjes = Klusjes.Where(x => x.IsActief && x.Path.ToLower().StartsWith(bew.Path.ToLower())).ToList();
            else
                klusjes = Klusjes.Where(x => x.Path.ToLower().StartsWith(bew.Path.ToLower())).ToList();
            return klusjes.Count > 0;
        }

        public bool IngezetAanKlus(string path, bool onlyactive, out List<Klus> klusjes)
        {
            klusjes = new List<Klus>();
            if (path == null || Klusjes == null)
                return false;
            if (onlyactive)
                klusjes = Klusjes.Where(x =>
                    x.IsActief && string.Equals(path, x.Path, StringComparison.CurrentCultureIgnoreCase) ||
                    string.Equals(path, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase) ||
                    string.Equals(path, x.Werk, StringComparison.CurrentCultureIgnoreCase)).ToList();
            else
                klusjes = Klusjes.Where(x => string.Equals(path, x.Path, StringComparison.CurrentCultureIgnoreCase) ||
                                             string.Equals(path, x.ProductieNr,
                                                 StringComparison.CurrentCultureIgnoreCase) ||
                                             string.Equals(path, x.Werk, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            return klusjes.Count > 0;
        }

        public bool IngezetAanKlus(string path)
        {
            return Klusjes?.Any(x => string.Equals(path, x.Path, StringComparison.CurrentCultureIgnoreCase) ||
                                     string.Equals(path, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase) ||
                                     string.Equals(path, x.Werk, StringComparison.CurrentCultureIgnoreCase)) ?? false;
        }

        public bool IngezetAanKlus(string path, bool isactief)
        {
            return Klusjes?.Any(x =>
                x.IsActief == isactief && string.Equals(path, x.Path, StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(path, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(path, x.Werk, StringComparison.CurrentCultureIgnoreCase)) ?? false;
        }

        public bool WerktAanKlus(Bewerking bew, out List<Klus> klusjes)
        {
            klusjes = new List<Klus>();
            if (bew == null || Klusjes == null || Klusjes.Count == 0)
                return false;

            klusjes = Klusjes?.Where(x => string.Equals(bew.Path, x.Path, StringComparison.CurrentCultureIgnoreCase) ||
                                          string.Equals(bew.Path, x.ProductieNr,
                                              StringComparison.CurrentCultureIgnoreCase) ||
                                          string.Equals(bew.Path, x.Werk, StringComparison.CurrentCultureIgnoreCase) &&
                                          x.Status == ProductieState.Gestart).ToList();

            return klusjes.Count > 0;
        }

        public List<Klus> HeeftOoitGewerktAan(Bewerking bew)
        {
            var klusjes = new List<Klus>();
            if (bew == null || Klusjes == null || Klusjes.Count == 0)
                return klusjes;

            klusjes = Klusjes.Where(x =>
                    string.Equals(bew.ArtikelNr, x.ArtikelNr, StringComparison.CurrentCultureIgnoreCase) &&
                    string.Equals(bew.Naam, x.Naam, StringComparison.CurrentCultureIgnoreCase) && x.TijdGewerkt() > 0)
                .ToList();

            return klusjes;
        }

        public bool WerktAanKlus(string path, out Klus klus)
        {
            klus = Klusjes?.FirstOrDefault(x =>
                string.Equals(path, x.Path, StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(path, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(path, x.Werk, StringComparison.CurrentCultureIgnoreCase) &&
                x.Status == ProductieState.Gestart);
            return klus != null;
        }

        public int StopAlleKlussen()
        {
            var count = 0;
            try
            {
                if (Klusjes == null || Klusjes.Count == 0)
                    return 0;
                count += Klusjes.Count(klus => klus.Stop());
            }
            catch (Exception)
            {
            }

            return count;
        }

        public Klus CurrentKlus()
        {
            if (string.IsNullOrEmpty(WerktAan) || string.IsNullOrEmpty(Werkplek) || Klusjes == null ||
                Klusjes.Count == 0)
                return null;
            return Klusjes.FirstOrDefault(x =>
                string.Equals($"{WerktAan}\\{Werkplek}", x.Path, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool xUpdateKlus(Klus klus)
        {
            try
            {
                if (Klusjes == null || klus.Omschrijving == null || klus.ProductieNr == null ||
                    klus.WerkPlek == null || klus.Naam == null)
                    return false;
                klus.PersoneelNaam = PersoneelNaam;
                var oldklus = Klusjes.GetKlus(klus.Path);
                if (oldklus == null)
                    Klusjes.Add(klus);
                else
                    oldklus.UpdateFrom(klus);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ReplaceKlus(Klus klus)
        {
            try
            {
                if (Klusjes == null || klus.Omschrijving == null || klus.ProductieNr == null ||
                    klus.WerkPlek == null || klus.Naam == null)
                    return false;
                klus.PersoneelNaam = PersoneelNaam;
                var oldklus = Klusjes.GetKlus(klus.Path);
                if (oldklus != null)
                    Klusjes.Remove(oldklus);
                Klusjes.Add(klus);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SkillTree GetSkills()
        {
            var xreturn = new SkillTree();
            if (Klusjes != null)
                foreach (var klus in Klusjes)
                    xreturn.Update(klus, VrijeDagen?.ToDictionary());
            return xreturn;
        }

        public int UpdateFrom(Personeel persoon, bool save)
        {
            if (persoon == null)
                return 0;
            var xupdate = 0;
            //update vrije dagen
            if (persoon.VrijeDagen != null)
            {
                if (VrijeDagen == null)
                    VrijeDagen = new UrenLijst();
                xupdate += VrijeDagen.UpdateLijst(persoon.VrijeDagen, true);
            }

            //update klusjes
            if (persoon.Klusjes != null)
            {
                if (Klusjes == null)
                    Klusjes = new List<Klus>();
                foreach (var i in persoon.Klusjes)
                {
                    var klus = Klusjes.FirstOrDefault(x => x.Equals(i));
                    if (klus == null)
                    {
                        Klusjes.Add(i);
                        xupdate++;
                    }
                    else
                    {
                        //if (ObjectsComparer.Comparer.Equals(klus, i)) continue;
                        if (i.xPublicInstancePropertiesEqual(klus)) continue;
                        i.CopyTo(ref klus);
                        xupdate++;
                    }
                }
            }

            LastChanged = new UserChange($"{PersoneelNaam} heeft {xupdate} wijziging(en) doorgevoerd.",
                DbType.Medewerkers);
            if (xupdate > 0 && save)
                Manager.Database.UpSert(this, LastChanged.Change).Wait();
            return xupdate;
        }

        public override bool Equals(object obj)
        {
            if (obj is string s)
                return string.Equals(PersoneelNaam, s, StringComparison.CurrentCultureIgnoreCase);
            if (obj is Personeel personeel)
            {
                var value = PersoneelNaam;
                //if (WerktAan != null)
                //    value += WerktAan;
                //if (Werkplek != null)
                //    value += Werkplek;

                var value2 = personeel.PersoneelNaam;
                //if (personeel.WerktAan != null)
                //    value2 += personeel.WerktAan;
                //if (personeel.Werkplek != null)
                //    value2 += personeel.Werkplek;
                var xreturn = string.Equals(value, value2, StringComparison.CurrentCultureIgnoreCase);
                return xreturn;
                // && string.Equals(Werkplek, personeel.Werkplek, StringComparison.CurrentCultureIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            var value = PersoneelNaam;
            if (WerktAan != null)
                value += WerktAan;
            if (Werkplek != null)
                value += Werkplek;
            return value.GetHashCode();
        }
    }
}