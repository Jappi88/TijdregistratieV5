using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductieManager.Productie
{
    public enum DagNamen
    {
        Maandag,
        Dinsdag,
        Woensdag,
        Donderdag,
        Vrijdag,
        Zaterdag,
    }

    public static class Werktijd
    {
        //hoeveel tijd heb je nog om te werken
        public static TimeSpan WerkTijdOver()
        {
            return WerkTijdOver(DateTime.Now.TimeOfDay);
        }

        public static TimeSpan WerkTijdNodigTotLeverdatum(this ProductieFormulier formulier, DateTime start)
        {
            return TijdGewerkt(start, formulier.LeverDatum);
        }

        public static TimeSpan WerkTijdNodigTotLeverdatum(this ProductieFormulier formulier)
        {
            return TijdGewerkt(DateTime.Now, formulier.LeverDatum);
        }

        //hoeveel tijd heb je nog om te werken
        public static TimeSpan WerkTijdOver(TimeSpan vanaf)
        {
            return WerkTijdOver(vanaf, Manager.Opties.StartWerkdag, Manager.Opties.EindWerkdag);
        }

        public static TimeSpan WerkTijdOver(TimeSpan vanaf, TimeSpan start, TimeSpan eind)
        {
            TimeSpan nu = vanaf;
            TimeSpan xeind = eind;
            if (vanaf >= start && vanaf < xeind)
            {
                TimeSpan over = ((eind - nu) - PauzeTijdOver(nu));
                if (over.TotalMinutes > 0)
                    return over;
            }
            return new TimeSpan();
        }

        //hoeveel tijd pauze heb je nog
        public static TimeSpan PauzeTijdOver(TimeSpan vanaf)
        {
            TimeSpan pauze = new TimeSpan();
            if (vanaf <= Manager.Opties.StartPauze1)
                pauze += Manager.Opties.DuurPauze1;
            if (vanaf <= Manager.Opties.StartPauze2)
                pauze += Manager.Opties.DuurPauze2;
            if (vanaf <= Manager.Opties.StartPauze3)
                pauze += Manager.Opties.DuurPauze3;
            return pauze;
        }

        public static TimeSpan AantalWerkdagen(TimeSpan time)
        {
            TimeSpan werkdag = WerkTijdOver(Manager.Opties.StartWerkdag);
            int dagen = (int)(time.TotalHours / werkdag.TotalHours);
            double hours = time.TotalHours % werkdag.TotalHours;
            return TimeSpan.FromHours(hours).Add(TimeSpan.FromDays(dagen));
        }

        public static TimeSpan TijdGewerkt(DateTime start, DateTime stop, TimeSpan startwerkdag, TimeSpan eindwerkdag, Dictionary<DateTime, DateTime> vrijetijd = null)
        {
            if (start > stop || Manager.Opties == null)
                return new TimeSpan();
            try
            {
                start = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, 0);
                stop = new DateTime(stop.Year, stop.Month, stop.Day, stop.Hour, stop.Minute, 0);
                TimeSpan xstart = startwerkdag;
                TimeSpan xstop = eindwerkdag;
                start = EerstVolgendewerkdag(start, xstart, xstop);
                TimeSpan werkdaguren = WerkTijdOver(xstart, xstart, xstop);
                TimeSpan tijd = new TimeSpan();
                TimeSpan cur = start.TimeOfDay;
                TimeSpan vrij = new TimeSpan();
                if (vrijetijd != null && vrijetijd.Count > 0)
                {
                    Dictionary<DateTime, DateTime> xdict = new Dictionary<DateTime, DateTime> { };
                    foreach (var v in vrijetijd)
                    {
                        DateTime vrijstart = v.Key;
                        DateTime vrijend = v.Value;
                        if (vrijend.TimeOfDay > eindwerkdag)
                            vrijend = new DateTime(vrijend.Year, vrijend.Month, vrijend.Day, eindwerkdag.Hours, eindwerkdag.Minutes, 0);
                        if (vrijstart >= start && start <= vrijend)
                        {
                            vrij += TijdGewerkt(vrijstart, vrijend, xstart, xstop, xdict);
                            xdict.Add(v.Key, v.Value);
                        }
                        else if(start >= vrijstart && start <= vrijend)
                        {
                            vrij += TijdGewerkt(start, vrijend, xstart, xstop, xdict);
                            xdict.Add(v.Key, v.Value);
                        }
                    }
                }
                while (start.Date <= stop.Date)
                {
                    if (!Manager.Opties.NationaleFeestdagen.Any(x => x.Date == start.Date) && (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday))
                    {
                        if (start.Date == stop.Date)
                        {
                            cur = stop.TimeOfDay;
                            tijd += TijdGewerkt(start.TimeOfDay, cur, xstart, xstop);
                        }
                        else
                        {
                            tijd += WerkTijdOver(start.TimeOfDay, xstart, xstop);
                        }
                    }
                    start = new DateTime(start.Year, start.Month, start.Day, startwerkdag.Hours, startwerkdag.Minutes, startwerkdag.Seconds).AddDays(1);
                }
                if (vrij >= tijd)
                    tijd = new TimeSpan();
                else
                    tijd -= vrij;//GetMaxTime(tijd, vrij);
                return tijd;
            }
            catch { return new TimeSpan(); }
        }

        public static TimeSpan GetMaxTime(TimeSpan min, TimeSpan max)
        {
            // TimeSpan xreturn = new TimeSpan();
            int count = (int)(max.TotalHours / min.TotalHours);

            double rest = max.TotalHours % min.TotalHours;
            if (double.IsNaN(rest))
                rest = 0;
            if (count > 0)
                return TimeSpan.FromHours(min.TotalHours - rest);
            return TimeSpan.FromHours(rest);
        }

        public static TimeSpan TijdGewerkt(DateTime start, DateTime stop)
        {
            return TijdGewerkt(start, stop, Manager.Opties.StartWerkdag, Manager.Opties.EindWerkdag);
        }

        public static TimeSpan TijdGewerkt(TimeSpan starttime, TimeSpan stoptime)
        {
            return TijdGewerkt(starttime, stoptime, Manager.Opties.StartWerkdag, Manager.Opties.EindWerkdag);
        }

        public static TimeSpan TijdGewerkt(TimeSpan starttime, TimeSpan stoptime, TimeSpan startday, TimeSpan Stopday)
        {
            if (Manager.Opties == null)
                return new TimeSpan();
            if (starttime < startday || starttime > stoptime)
                return new TimeSpan();
            if (stoptime > Stopday)
                stoptime = Stopday;
            return TimeSpan.FromHours((stoptime.TotalHours - starttime.TotalHours) - PauzeGehad(starttime, stoptime).TotalHours);
        }

        public static TimeSpan PauzeGehad(TimeSpan starttijd, TimeSpan eindtijd)
        {
            TimeSpan start = starttijd;
            TimeSpan stop = eindtijd;
            TimeSpan pauze = new TimeSpan();
            if (start <= (Manager.Opties.DuurPauze1 + Manager.Opties.StartPauze1) && stop >= (Manager.Opties.StartPauze1))
            {
                if (start >= Manager.Opties.StartPauze1 && start <= (Manager.Opties.DuurPauze1 + Manager.Opties.StartPauze1))
                    pauze = pauze.Add(start - Manager.Opties.StartPauze1);
                else if (stop >= Manager.Opties.StartPauze1 && stop <= (Manager.Opties.DuurPauze1 + Manager.Opties.StartPauze1))
                    pauze = pauze.Add(stop - Manager.Opties.StartPauze1);
                else
                    pauze = pauze.Add(Manager.Opties.DuurPauze1);
            }
            if (start <= (Manager.Opties.DuurPauze2 + Manager.Opties.StartPauze2) && stop >= (Manager.Opties.StartPauze2))
            {
                if (start >= Manager.Opties.StartPauze2 && start <= (Manager.Opties.DuurPauze2 + Manager.Opties.StartPauze2))
                    pauze = pauze.Add(start - Manager.Opties.StartPauze2);
                else if (stop >= Manager.Opties.StartPauze2 && stop <= (Manager.Opties.DuurPauze2 + Manager.Opties.StartPauze2))
                    pauze = pauze.Add(stop - Manager.Opties.StartPauze2);
                else
                    pauze = pauze.Add(Manager.Opties.DuurPauze2);
            }
            if (start <= (Manager.Opties.DuurPauze3 + Manager.Opties.StartPauze3) && stop >= (Manager.Opties.StartPauze3))
            {
                if (start >= Manager.Opties.StartPauze3 && start <= (Manager.Opties.DuurPauze3 + Manager.Opties.StartPauze3))
                    pauze = pauze.Add(start - Manager.Opties.StartPauze3);
                else if (stop >= Manager.Opties.StartPauze3 && stop <= (Manager.Opties.DuurPauze3 + Manager.Opties.StartPauze3))
                    pauze = pauze.Add(stop - Manager.Opties.StartPauze3);
                else
                    pauze = pauze.Add(Manager.Opties.DuurPauze3);
            }
            return pauze;
        }

        public static TimeSpan MoetPauzeNemen(TimeSpan tijd)
        {
            TimeSpan r = tijd;
            TimeSpan pauze = new TimeSpan();
            if (r <= (Manager.Opties.DuurPauze1 + Manager.Opties.StartPauze1))
                pauze = pauze.Add(Manager.Opties.DuurPauze1);
            if (r <= (Manager.Opties.DuurPauze2 + Manager.Opties.StartPauze2))
                pauze = pauze.Add(Manager.Opties.DuurPauze2);
            if (r <= (Manager.Opties.DuurPauze3 + Manager.Opties.StartPauze3))
                pauze = pauze.Add(Manager.Opties.DuurPauze3);
            return pauze;
        }

        public static DateTime DatumNaTijd(DateTime vanaf, TimeSpan tijd)
        {
            DateTime nu = vanaf;
            try
            {
                nu = EerstVolgendewerkdag(nu);
                TimeSpan werkover = WerkTijdOver(nu.TimeOfDay);
                long dagen = 0;
                TimeSpan tijdnavandaag = new TimeSpan();
                TimeSpan werkdag = WerkTijdOver(Manager.Opties.StartWerkdag);
                TimeSpan rest = new TimeSpan();
                if (tijd.TotalHours >= 0 && tijd <= werkover)
                    rest = tijd;
                else
                {
                    tijdnavandaag = (tijd - werkover);
                    nu = nu.Add(werkover.Add(MoetPauzeNemen(nu.TimeOfDay)));
                    nu = EerstVolgendewerkdag(nu);
                    rest = werkover;
                }
                if (rest.Ticks < 0)
                    werkover = werkover.Add(rest);
                //meer dan 1 dag moeten we hier de dagen gaan berekenen
                if (tijdnavandaag.Ticks > 0)
                {
                    //dagen++;
                    dagen += (tijdnavandaag.Ticks / werkdag.Ticks);
                    rest = new TimeSpan(tijdnavandaag.Ticks % werkdag.Ticks);
                    //rest -= werkover;
                    for (int i = 0; i < dagen; i++)
                        nu = EerstVolgendewerkdag(nu.Add(werkdag.Add(MoetPauzeNemen(nu.TimeOfDay))));
                }
                if (rest.TotalHours > 0)
                {
                    if (nu.TimeOfDay < Manager.Opties.StartWerkdag || nu.TimeOfDay >= Manager.Opties.EindWerkdag)
                        nu = EerstVolgendewerkdag(nu);
                    //als het minder dan een dag is voor de volgende dag, moeten we de tijd bij optellen bij een nieuwe werkdag
                    TimeSpan r = (nu.TimeOfDay.Add(rest));
                    r = r.Add(PauzeGehad(Manager.Opties.StartWerkdag, r));
                    return new DateTime(nu.Year, nu.Month, nu.Day, r.Hours, r.Minutes, r.Seconds);
                }
            }
            catch { }
            return nu;
        }

        public static double AantalPersTijdMultiplier(this Personeel[] shifts)
        {
            double x = 0;
            if (shifts?.Length > 0)
                foreach (var v in shifts)
                {
                    if (v.Efficientie == 0)
                        v.Efficientie = 100;
                    x += (double)((v.Efficientie / 100));
                }
            else return 1;
            return x;
        }

        public static DateTime EerstVolgendewerkdag(DateTime vanaf)
        {
            return EerstVolgendewerkdag(vanaf, Manager.Opties.StartWerkdag, Manager.Opties.EindWerkdag);
        }

        public static DateTime EerstVolgendewerkdag(DateTime vanaf, TimeSpan startdag, TimeSpan einddag)
        {
            DateTime x = vanaf;
            while (Manager.Opties.NationaleFeestdagen.Where(t => t.Date == x.Date).FirstOrDefault().Date == x.Date)
            {
                switch (x.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        x = x.AddDays(3);
                        break;

                    case DayOfWeek.Saturday:
                        x = x.AddDays(2);
                        break;

                    default:
                        x = x.AddDays(1);
                        break;
                }
            }
            TimeSpan time = startdag;
            switch (x.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    if (vanaf.TimeOfDay >= einddag)
                        x = x.AddDays(3);
                    else if (vanaf.TimeOfDay >= startdag) time = vanaf.TimeOfDay;
                    else if (vanaf.TimeOfDay < startdag) time = startdag;
                    break;

                case DayOfWeek.Sunday:
                    x = x.AddDays(1);
                    break;

                case DayOfWeek.Saturday:
                    x = x.AddDays(2);
                    break;

                default:
                    if (vanaf.TimeOfDay >= einddag)
                        x = x.AddDays(1);
                    else if (vanaf.TimeOfDay >= startdag) time = vanaf.TimeOfDay;
                    else if (vanaf.TimeOfDay < startdag) time = startdag;
                    break;
            }
            if (time >= Manager.Opties.StartPauze1 && time < Manager.Opties.StartPauze1.Add(Manager.Opties.DuurPauze1))
                time = Manager.Opties.StartPauze1.Add(Manager.Opties.DuurPauze1);
            if (time >= Manager.Opties.StartPauze2 && time < Manager.Opties.StartPauze2.Add(Manager.Opties.DuurPauze2))
                time = Manager.Opties.StartPauze2.Add(Manager.Opties.DuurPauze2);
            if (time >= Manager.Opties.StartPauze3 && time < Manager.Opties.StartPauze3.Add(Manager.Opties.DuurPauze3))
                time = Manager.Opties.StartPauze3.Add(Manager.Opties.DuurPauze3);
            return new DateTime(x.Year, x.Month, x.Day, time.Hours, time.Minutes, time.Seconds);
        }
    }
}