using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Rpm.ViewModels;

namespace Rpm.Productie
{
    public enum DagNamen
    {
        Maandag,
        Dinsdag,
        Woensdag,
        Donderdag,
        Vrijdag,
        Zaterdag
    }

    public static class Werktijd
    {
        //hoeveel tijd heb je nog om te werken
        public static TimeSpan WerkTijdOver()
        {
            var rooster = Manager.Opties.GetWerkRooster();
            return WerkTijdOver(DateTime.Now.TimeOfDay, rooster);
        }

        public static TimeSpan WerkTijdNodigTotLeverdatum(this IProductieBase formulier, DateTime start)
        {
            return TijdGewerkt(start, formulier.LeverDatum, null,null);
        }

        public static TimeSpan WerkTijdNodigTotLeverdatum(DateTime leverdatum, DateTime start)
        {
            return TijdGewerkt(start, leverdatum, null, null);
        }

        public static TimeSpan WerkTijdNodigTotLeverdatum(this IProductieBase formulier)
        {
            return TijdGewerkt(DateTime.Now, formulier.LeverDatum, null,null);
        }

        public static TimeSpan WerkTijdOver(TimeSpan vanaf, Rooster rooster)
        {
            var nu = vanaf;
            rooster = rooster == null || !rooster.IsValid() ? Manager.Opties.GetWerkRooster() : rooster;
            var xeind = rooster.EindWerkdag;
            if (vanaf >= rooster.StartWerkdag && vanaf < xeind)
            {
                var over = rooster.EindWerkdag - nu;
                if (rooster.GebruiktPauze)
                    over -= PauzeTijdOver(nu, rooster);
                if (over.TotalMinutes > 0)
                    return over;
            }

            return new TimeSpan();
        }

        //hoeveel tijd pauze heb je nog
        public static TimeSpan PauzeTijdOver(TimeSpan vanaf, Rooster rooster)
        {
            var pauze = new TimeSpan();
            rooster = rooster == null || !rooster.IsValid() ? Manager.Opties.GetWerkRooster() : rooster;
            if (vanaf <= rooster.StartPauze1)
                pauze += rooster.DuurPauze1;
            if (vanaf <= rooster.StartPauze2)
                pauze += rooster.DuurPauze2;
            if (vanaf <= rooster.StartPauze3)
                pauze += rooster.DuurPauze3;
            return pauze;
        }

        public static TimeSpan AantalWerkdagen(TimeSpan time, Rooster rooster)
        {
            rooster = rooster == null || !rooster.IsValid() ? Manager.Opties.GetWerkRooster() : rooster;
            var werkdag = WerkTijdOver(rooster.StartWerkdag, rooster);
            var dagen = (int) (time.TotalHours / werkdag.TotalHours);
            var hours = time.TotalHours % werkdag.TotalHours;
            return TimeSpan.FromHours(hours).Add(TimeSpan.FromDays(dagen));
        }

        public static int AantalWerkdagen(DateTime van, DateTime tot, Rooster rooster,List<Rooster> specialeRoosters)
        {
            try
            {
                var count = 0;
                rooster = rooster == null || !rooster.IsValid() ? Manager.Opties.GetWerkRooster() : rooster;
                var startwerkdag = rooster.StartWerkdag;
                var eindwerkdag = rooster.EindWerkdag;
                startwerkdag = new TimeSpan(startwerkdag.Hours, startwerkdag.Minutes, 0);
                eindwerkdag = new TimeSpan(eindwerkdag.Hours, eindwerkdag.Minutes, 0);
                van = EerstVolgendeWerkdag(van, ref rooster, rooster, specialeRoosters);
                while (van < tot)
                {
                    if (van.TimeOfDay >= startwerkdag && van.TimeOfDay <= eindwerkdag)
                        count++;
                    van = new DateTime(van.Year, van.Month, van.Day, eindwerkdag.Hours, eindwerkdag.Minutes, 0);
                    van = EerstVolgendeWerkdag(van, ref rooster, rooster, specialeRoosters);
                }

                return count;
            }
            catch
            {
                return 0;
            }
        }

        public static double ExtraUren(this ExtraTijd tijd, TijdEntry entry, Rooster rooster)
        {
            try
            {
                if (tijd == null)
                    return 0;
                rooster ??= entry?.WerkRooster ?? Manager.Opties.GetWerkRooster();
                if (rooster != null && !rooster.IsValid())
                    rooster = Manager.Opties.GetWerkRooster();
                if (tijd.Herhaaldelijk && entry != null)
                {
                    var vanaf = tijd.Vanaf;
                    var tot = tijd.Tot;
                    if (vanaf > entry.Stop)
                    {
                        return 0;
                    }

                    if (vanaf < entry.Start)
                        vanaf = entry.Start;
                    if (tot > entry.Stop)
                        tot = entry.Stop;
                    if (vanaf >= entry.Start && vanaf < entry.Stop)
                    {
                        var dagen = AantalWerkdagen(vanaf, tot, rooster,null);
                        if (dagen <= 0)
                            return 0;
                        if (tijd.Aantalkeer > 0)
                            switch (tijd.PeriodeSoort)
                            {
                                case Periode.Dag:
                                    if (tijd.Aantalkeer < dagen)
                                        dagen = tijd.Aantalkeer;
                                    break;
                                case Periode.Week:
                                    if (tijd.Aantalkeer * 5 < dagen)
                                        dagen = tijd.Aantalkeer * 5;
                                    break;
                                case Periode.Maand:
                                    if (tijd.Aantalkeer * 20 < dagen)
                                        dagen = tijd.Aantalkeer * 20;
                                    break;
                                case Periode.Jaar:
                                    if (tijd.Aantalkeer * 240 < dagen)
                                        dagen = tijd.Aantalkeer * 240;
                                    break;
                            }

                        return tijd.Tijd.TotalHours * dagen;
                    }

                    return 0;
                }

                return tijd.Tijd.TotalHours;
            }
            catch
            {
                return 0;
            }
        }

        public static TimeSpan TijdGewerkt(TijdEntry entry, Rooster rooster,List<Rooster> specialeRoosters,
            Dictionary<DateTime, DateTime> vrijetijd = null, double extratijd = 0)
        {
            if (entry == null)
                return new TimeSpan();

            var start = entry.Start;
            var stop = entry.InUse ? DateTime.Now : entry.Stop;
            Rooster realrooster = rooster;
            if (entry.WerkRooster != null && entry.WerkRooster.IsValid())
                realrooster = entry.WerkRooster;
            realrooster ??= Manager.Opties.GetWerkRooster();
            if (entry.ExtraTijd != null) return TimeSpan.FromHours(entry.ExtraTijd.ExtraUren(null, rooster));
            if (start > stop || Manager.Opties == null)
                return new TimeSpan();
            try
            {
                start = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, 0);
                stop = new DateTime(stop.Year, stop.Month, stop.Day, stop.Hour, stop.Minute, 0);
                var xstart = realrooster.StartWerkdag;
                var xstop = realrooster.EindWerkdag;
                //var xpauze = rooster.GebruiktPauze;
                start = EerstVolgendeWerkdag(start, ref rooster, realrooster,specialeRoosters);
                //var werkdaguren = WerkTijdOver(xstart, rooster);
                var tijd = TimeSpan.FromHours(extratijd);
                var cur = start.TimeOfDay;
                var vrij = new TimeSpan();
                //eerst gaan we alle tijd berekenen dat niet gewerkt is.
                if (vrijetijd is {Count: > 0})
                {
                    var xdict = new Dictionary<DateTime, DateTime>();
                    foreach (var v in vrijetijd)
                    {
                        var vrijstart = EerstVolgendeWerkdag(v.Key, ref rooster, realrooster,specialeRoosters);
                        if (vrijstart < start)
                            vrijstart = start;
                        var vrijend = v.Value;
                        if (vrijend.TimeOfDay > xstop)
                            vrijend = new DateTime(vrijend.Year, vrijend.Month, vrijend.Day, xstop.Hours,
                                xstop.Minutes, 0);
                        else vrijend = new DateTime(vrijend.Year, vrijend.Month, vrijend.Day, vrijend.Hour,
                            vrijend.Minute, 0);
                        if (vrijend > start)
                        {
                            if (vrijend > stop)
                                vrijend = stop;
                            //if (vrijstart < start)
                            //    vrijstart = start;
                            vrij += TijdGewerkt(new TijdEntry(vrijstart, vrijend, realrooster), realrooster,specialeRoosters, xdict);
                            xdict.Add(v.Key, v.Value);
                        }
                    }
                }
                //hier gaan we door elke dag heen en pakken we de uren die beschikbaar zijn volgens de rooster
                while (start.Date <= stop.Date)
                {
                    //we pakken de eerstvolgende werktijd samen met de rooster.
                    start = EerstVolgendeWerkdag(start, ref rooster, realrooster,specialeRoosters);
                    //we gaan dan kijken of de tijd die terug komt wel binnen het bereik is van wat de eindtijd is.
                    if (start.Date > stop.Date) break;
                    //we gaan dan kijken of het weekend is met een speciale rooster,geen feestdag of een normale werkdag. 
                    if (rooster.IsSpecial() || (Manager.Opties.NationaleFeestdagen.All(x => x.Date != start.Date) &&
                        start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday))
                    {
                        //als we op de zelfde datum zijn gaan we de tijd pakken dat al gewerkt is.
                        if (start.Date == stop.Date)
                        {
                            cur = stop.TimeOfDay;
                            tijd += TijdGewerkt(start.TimeOfDay, cur, rooster);
                        }
                        else // anders pakken we de tijd dat nog over is afhankelijk van de rooster
                        {
                            tijd += WerkTijdOver(start.TimeOfDay, rooster);
                        }
                    }
                    
                    start = new DateTime(start.Year, start.Month, start.Day).AddDays(1);
                    
                }
                //de tijd die we berekent hebben gaan we de vrije tijd er vanaf halen als dat er is.
                tijd = vrij >= tijd ? new TimeSpan() : tijd.Subtract(vrij);
                return tijd;
            }
            catch
            {
                return new TimeSpan();
            }
        }

        public static TimeSpan GetMaxTime(TimeSpan min, TimeSpan max)
        {
            // TimeSpan xreturn = new TimeSpan();
            var count = (int) (max.TotalHours / min.TotalHours);

            var rest = max.TotalHours % min.TotalHours;
            if (double.IsNaN(rest))
                rest = 0;
            if (count > 0)
                return TimeSpan.FromHours(min.TotalHours - rest);
            return TimeSpan.FromHours(rest);
        }

        //public static TimeSpan TijdGewerkt(DateTime start, DateTime stop)
        //{
        //    return TijdGewerkt(new TijdEntry(start, stop), Manager.Opties.WerkRooster);
        //}

        public static TimeSpan TijdGewerkt(DateTime start, DateTime stop, Rooster rooster,List<Rooster> specialeRoosters)
        {
            return TijdGewerkt(new TijdEntry(start, stop, rooster), rooster ?? Manager.Opties.GetWerkRooster(),specialeRoosters);
        }

        public static TimeSpan TijdGewerkt(TimeSpan starttime, TimeSpan stoptime, Rooster rooster)
        {
            if (Manager.Opties == null)
                return new TimeSpan();
            rooster = rooster == null || !rooster.IsValid() ?Manager.Opties.GetWerkRooster() : rooster;
            if (starttime < rooster.StartWerkdag || starttime > stoptime)
                return new TimeSpan();
            if (stoptime > rooster.EindWerkdag)
                stoptime = rooster.EindWerkdag;
            var hours = stoptime.TotalHours - starttime.TotalHours;
            if (rooster.GebruiktPauze)
                hours -= PauzeGehad(starttime, stoptime, rooster).TotalHours;
            return TimeSpan.FromHours(hours);
        }

        public static TimeSpan PauzeGehad(TimeSpan starttijd, TimeSpan eindtijd, Rooster rooster)
        {
            var start = starttijd;
            var stop = eindtijd;
            var pauze = new TimeSpan();
            rooster = rooster == null || !rooster.IsValid() ? Manager.Opties.GetWerkRooster() : rooster;
            if (start < rooster.DuurPauze1 + rooster.StartPauze1 && stop > rooster.StartPauze1)
            {
                if (start > rooster.StartPauze1 &&
                    start < rooster.DuurPauze1 + rooster.StartPauze1)
                    pauze = pauze.Add(start - rooster.StartPauze1);
                else if (stop > rooster.StartPauze1 &&
                         stop < rooster.DuurPauze1 + rooster.StartPauze1)
                    pauze = pauze.Add(stop - rooster.StartPauze1);
                else
                    pauze = pauze.Add(rooster.DuurPauze1);
            }

            if (start < rooster.DuurPauze2 + rooster.StartPauze2 && stop > rooster.StartPauze2)
            {
                if (start > rooster.StartPauze2 &&
                    start < rooster.DuurPauze2 + rooster.StartPauze2)
                    pauze = pauze.Add(start - rooster.StartPauze2);
                else if (stop > rooster.StartPauze2 &&
                         stop < rooster.DuurPauze2 + rooster.StartPauze2)
                    pauze = pauze.Add(stop - rooster.StartPauze2);
                else
                    pauze = pauze.Add(rooster.DuurPauze2);
            }

            if (start < rooster.DuurPauze3 + rooster.StartPauze3 && stop > rooster.StartPauze3)
            {
                if (start > rooster.StartPauze3 &&
                    start < rooster.DuurPauze3 + rooster.StartPauze3)
                    pauze = pauze.Add(start - rooster.StartPauze3);
                else if (stop > rooster.StartPauze3 &&
                         stop < rooster.DuurPauze3 + rooster.StartPauze3)
                    pauze = pauze.Add(stop - rooster.StartPauze3);
                else
                    pauze = pauze.Add(rooster.DuurPauze3);
            }

            return pauze;
        }

        public static TimeSpan MoetPauzeNemen(TimeSpan tijd, Rooster rooster)
        {
            var r = tijd;
            rooster = rooster == null || !rooster.IsValid() ? Manager.Opties.GetWerkRooster() : rooster;
            var pauze = new TimeSpan();
            if (r <= rooster.DuurPauze1 + rooster.StartPauze1)
                pauze = pauze.Add(rooster.DuurPauze1);
            if (r <= rooster.DuurPauze2 + rooster.StartPauze2)
                pauze = pauze.Add(rooster.DuurPauze2);
            if (r <= rooster.DuurPauze3 + rooster.StartPauze3)
                pauze = pauze.Add(rooster.DuurPauze3);
            return pauze;
        }

        //public static TimeSpan MoetPauzeNemen(TimeSpan starttijd,TimeSpan eindtijd)
        //{
        //    var r = starttijd;
        //    var t = eindtijd;
        //    var pauze = new TimeSpan();
        //    if (r <= Manager.Opties.WerkRooster.DuurPauze1 + Manager.Opties.WerkRooster.StartPauze1)
        //        pauze = pauze.Add(Manager.Opties.WerkRooster.DuurPauze1);
        //    if (r <= Manager.Opties.WerkRooster.DuurPauze2 + Manager.Opties.WerkRooster.StartPauze2)
        //        pauze = pauze.Add(Manager.Opties.WerkRooster.DuurPauze2);
        //    if (r <= Manager.Opties.WerkRooster.DuurPauze3 + Manager.Opties.WerkRooster.StartPauze3)
        //        pauze = pauze.Add(Manager.Opties.WerkRooster.DuurPauze3);
        //    return pauze;
        //}

        public static DateTime DatumNaTijd(DateTime vanaf, TimeSpan tijd, Rooster rooster,List<Rooster> specialeRoosters)
        {
            var nu = vanaf;
            try
            {
                var realrooster = rooster == null || !rooster.IsValid() ? Manager.Opties.GetWerkRooster() : rooster;
                while (tijd.TotalSeconds > 0)
                {
                    nu = EerstVolgendeWerkdag(nu, ref rooster, realrooster, specialeRoosters);
                    var werkover = WerkTijdOver(nu.TimeOfDay, rooster);
                    var xleft = tijd > werkover ? werkover : tijd;
                    var pauze = PauzeGehad(nu.TimeOfDay, nu.Add(xleft).TimeOfDay, rooster);
                    nu = nu.Add(xleft);
                    //var xpause = pauze;
                    while (pauze.TotalSeconds > 0)
                    {
                        //xpause += pauze;
                        if (nu.Add(pauze).TimeOfDay > rooster.EindWerkdag)
                        {
                            xleft -= (rooster.EindWerkdag - nu.Add(pauze).TimeOfDay);
                            break;
                        }

                       
                            //xleft += pauze;
                        var xpauze = PauzeGehad(nu.TimeOfDay, nu.Add(pauze).TimeOfDay, rooster);
                        nu = nu.Add(pauze);
                        pauze = xpauze;
                    }
                    
                    tijd -= xleft;
                }
                return EerstVolgendeWerkdag(nu, ref rooster, realrooster, specialeRoosters);
                //long dagen = 0;
                //var tijdnavandaag = new TimeSpan();
                //var werkdag = WerkTijdOver(rooster.StartWerkdag, rooster);
                //var rest = new TimeSpan();
                //if (tijd.TotalHours >= 0 && tijd <= werkover)
                //{
                //    rest = tijd;
                //}
                //else
                //{
                //    tijdnavandaag = tijd - werkover;
                //    nu = nu.Add(werkover.Add(MoetPauzeNemen(nu.TimeOfDay, rooster)));
                //    nu = EerstVolgendeWerkdag(nu, ref rooster, realrooster, specialeRoosters);
                //    rest = werkover;
                //}

                //if (rest.Ticks < 0)
                //    werkover = werkover.Add(rest);
                ////meer dan 1 dag moeten we hier de dagen gaan berekenen
                //if (tijdnavandaag.Ticks > 0)
                //{
                //    //dagen++;
                //    dagen += tijdnavandaag.Ticks / werkdag.Ticks;
                //    rest = new TimeSpan(tijdnavandaag.Ticks % werkdag.Ticks);
                //    //rest -= werkover;
                //    for (var i = 0; i < dagen; i++)
                //        nu = EerstVolgendeWerkdag(nu.Add(werkdag.Add(MoetPauzeNemen(nu.TimeOfDay, rooster))), ref rooster, realrooster,specialeRoosters);
                //}

                //if (rest.TotalHours > 0)
                //{
                //    if (nu.TimeOfDay < rooster.StartWerkdag || nu.TimeOfDay >= rooster.EindWerkdag)
                //        nu = EerstVolgendeWerkdag(nu, ref rooster, realrooster, specialeRoosters);
                //    //als het minder dan een dag is voor de volgende dag, moeten we de tijd bij optellen bij een nieuwe werkdag
                //    var r = nu.TimeOfDay.Add(rest);
                //    r = r.Add(PauzeGehad(rooster.StartWerkdag, r, rooster));
                //    return new DateTime(nu.Year, nu.Month, nu.Day, r.Hours, r.Minutes, r.Seconds);
                //}
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return nu;
        }

        public static DateTime DatumVoorTijd(DateTime vanaf, TimeSpan tijd, Rooster rooster, List<Rooster> specialeRoosters)
        {
            var xreturn = vanaf;
            try
            {
                var tijdover = tijd;
                var xdag = vanaf;
                var xrooster = rooster;
                while (tijdover.TotalHours > 0)
                {
                    xdag = EerstVorigeWerkdag(xdag, ref xrooster, rooster, specialeRoosters);
                   var xtijdover = WerkTijdOver(xdag.TimeOfDay, xrooster);
                    var xfulldag = WerkTijdOver(xrooster.StartWerkdag, xrooster);

                    //xdag = new DateTime(xdag.Year, xdag.Month, xdag.Day, xrooster.EindWerkdag.Hours,
                    //    xrooster.EindWerkdag.Minutes, 0);

                    var xtijd = xfulldag - xtijdover;
                    if (xtijd.TotalHours > 0)
                    {
                        if (xtijd > tijdover)
                        {
                            xtijd = tijdover;
                            tijdover = new TimeSpan();
                        }
                        else
                            tijdover -= xtijd;
                    }

                    var pausegehad = PauzeGehad(xdag.TimeOfDay - xtijd, xdag.TimeOfDay, xrooster);
                    xdag = xdag.Subtract(xtijd + pausegehad);
                    if (tijdover.TotalHours > 0)
                    {
                        xdag = xdag.Subtract(TimeSpan.FromDays(1));
                        xdag = new DateTime(xdag.Year, xdag.Month, xdag.Day, xrooster.EindWerkdag.Hours,
                            xrooster.EindWerkdag.Minutes, 0);
                    }
                }

                xdag = EerstVorigeWerkdag(xdag, ref xrooster, rooster, specialeRoosters);
                
                xreturn = xdag;
            }
            catch (Exception v)
            {
                Console.WriteLine(v);
            }

            return xreturn;
        }

        public static bool IsWerkDag(DateTime datum, List<Rooster> specialeroosters, ref Rooster rooster)
        {
            
            var specialrooster = specialeroosters?.FirstOrDefault(x => x.Vanaf.Date == datum.Date);
            rooster = specialrooster ?? rooster;
            if (specialrooster != null) return true;

            if (Manager.Opties?.NationaleFeestdagen != null &&
                Manager.Opties.NationaleFeestdagen.Any(x => x.Date == datum.Date)) return false;

            if (datum.DayOfWeek == DayOfWeek.Saturday || datum.DayOfWeek == DayOfWeek.Sunday) return false;
            //if (rooster != null && datum.TimeOfDay < rooster.StartWerkdag) return false;
            return true;
        }

        public static double AantalPersTijdMultiplier(this Personeel[] shifts)
        {
            double x = 0;
            if (shifts?.Length > 0)
                foreach (var v in shifts)
                {
                    if (v.Efficientie == 0)
                        v.Efficientie = 100;
                    x += v.Efficientie / 100;
                }
            else return 1;

            return x;
        }

        public static DateTime EerstVolgendeWerkdag(DateTime vanaf, ref Rooster rooster, Rooster realrooster, List<Rooster> specialeRoosters)
        {
            var x = vanaf;
            rooster = rooster == null || !rooster.IsValid() ? Manager.Opties.GetWerkRooster() : rooster;
            var specialrooster = (specialeRoosters?.FirstOrDefault(t => t.Vanaf.Date == vanaf.Date));
            if (specialrooster != null)
            {
                rooster = specialrooster;
                if (vanaf.TimeOfDay < rooster.StartWerkdag)
                    return new DateTime(vanaf.Year, vanaf.Month, vanaf.Day, rooster.StartWerkdag.Hours,
                        rooster.StartWerkdag.Minutes, 0);
                if (vanaf.TimeOfDay >= rooster.StartWerkdag && vanaf.TimeOfDay < rooster.EindWerkdag)
                    return new DateTime(vanaf.Year, vanaf.Month, vanaf.Day, vanaf.Hour,
                        vanaf.Minute, 0);
            }
            else rooster = realrooster;

            var einddag = new TimeSpan(rooster.EindWerkdag.Hours, rooster.EindWerkdag.Minutes, 0);
            var time = vanaf.TimeOfDay;
            bool isnewday = false;
            while (Manager.Opties.NationaleFeestdagen.FirstOrDefault(t => t.Date == x.Date).Date == x.Date)
                switch (x.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        x = x.AddDays(3);
                        isnewday = true;
                        break;

                    case DayOfWeek.Saturday:
                        x = x.AddDays(2);
                        isnewday = true;
                        break;

                    default:
                        x = x.AddDays(1);
                        isnewday = true;
                        break;
                }

           
            switch (x.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    if (vanaf.TimeOfDay >= einddag)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            x = x.AddDays(1);
                            specialrooster =
                                specialeRoosters?.FirstOrDefault(t =>
                                    t.Vanaf.Date == x.Date);
                            if (specialrooster != null)
                                break;
                        }
                        isnewday = true;
                    }

                    break;
                case DayOfWeek.Sunday:
                    x = x.AddDays(1);
                    specialrooster = specialeRoosters?.FirstOrDefault(t => t.Vanaf.Date == x.Date);
                    isnewday = true;
                    break;

                case DayOfWeek.Saturday:
                    for (int i = 0; i < 2; i++)
                    {
                        x = x.AddDays(1);
                        specialrooster = specialeRoosters?.FirstOrDefault(t => t.Vanaf.Date == x.Date);
                        if (specialrooster != null)
                            break;
                    }
                    isnewday = true;
                    break;

                default:
                    if (vanaf.TimeOfDay >= einddag)
                    {
                        x = vanaf.AddDays(1);
                        isnewday = true;
                    }
                    break;
            }
            rooster = specialrooster ?? realrooster ?? Manager.Opties.GetWerkRooster();

            if (isnewday)
                    time = rooster.StartWerkdag;

            x = new DateTime(x.Year, x.Month, x.Day, time.Hours, time.Minutes, time.Seconds);
            var startdag = new TimeSpan(rooster.StartWerkdag.Hours, rooster.StartWerkdag.Minutes, 0);
            einddag = new TimeSpan(rooster.EindWerkdag.Hours, rooster.EindWerkdag.Minutes, 0);
            if (x.TimeOfDay >= startdag && x.TimeOfDay < einddag) time = x.TimeOfDay;
            if (x.TimeOfDay < startdag) time = startdag;
            if (rooster.GebruiktPauze)
            {
                if (time >= rooster.StartPauze1 && time < rooster.StartPauze1.Add(rooster.DuurPauze1))
                    time = rooster.StartPauze1.Add(rooster.DuurPauze1);
                if (time >= rooster.StartPauze2 && time < rooster.StartPauze2.Add(rooster.DuurPauze2))
                    time = rooster.StartPauze2.Add(rooster.DuurPauze2);
                if (time >= rooster.StartPauze3 && time < rooster.StartPauze3.Add(rooster.DuurPauze3))
                    time = rooster.StartPauze3.Add(rooster.DuurPauze3);
            }

            return new DateTime(x.Year, x.Month, x.Day, time.Hours, time.Minutes, 0);
        }

        public static DateTime EerstVorigeWerkdag(DateTime vanaf, ref Rooster rooster, Rooster realrooster, List<Rooster> specialeRoosters)
        {
            var x = vanaf;
            rooster = rooster == null || !rooster.IsValid() ? Manager.Opties.GetWerkRooster() : rooster;
            var specialrooster = specialeRoosters?.FirstOrDefault(t => t.Vanaf.Date == vanaf.Date);
            if (specialrooster != null)
            {
                rooster = specialrooster;
                if (vanaf.TimeOfDay < rooster.StartWerkdag)
                    return new DateTime(vanaf.Year, vanaf.Month, vanaf.Day, rooster.StartWerkdag.Hours,
                        rooster.StartWerkdag.Minutes, 0);
                if (vanaf.TimeOfDay >= rooster.StartWerkdag && vanaf.TimeOfDay <= rooster.EindWerkdag)
                    return vanaf;
            }
            else rooster = realrooster;
            var startdag = new TimeSpan(rooster.StartWerkdag.Hours, rooster.StartWerkdag.Minutes, 0);
            while (!IsWerkDag(x,specialeRoosters,ref rooster) || x.TimeOfDay < startdag)
            {
                x = x.Subtract(TimeSpan.FromDays(1));
                startdag = new TimeSpan(rooster.StartWerkdag.Hours, rooster.StartWerkdag.Minutes, 0);
                x = new DateTime(x.Year, x.Month, x.Day, rooster.StartWerkdag.Hours, rooster.StartWerkdag.Minutes, 0);
            }
            var einddag = new TimeSpan(rooster.EindWerkdag.Hours, rooster.EindWerkdag.Minutes, 0);
            var time = startdag;
            if (x.TimeOfDay >= startdag && x.TimeOfDay <= einddag) time = x.TimeOfDay;

            if (time >= rooster.StartPauze1 && time < rooster.StartPauze1.Add(rooster.DuurPauze1))
                time = rooster.StartPauze1.Add(rooster.DuurPauze1);
            if (time >= rooster.StartPauze2 && time < rooster.StartPauze2.Add(rooster.DuurPauze2))
                time = rooster.StartPauze2.Add(rooster.DuurPauze2);
            if (time >= rooster.StartPauze3 && time < rooster.StartPauze3.Add(rooster.DuurPauze3))
                time = rooster.StartPauze3.Add(rooster.DuurPauze3);
            return new DateTime(x.Year, x.Month, x.Day, time.Hours, time.Minutes, 0);
        }
    }
}