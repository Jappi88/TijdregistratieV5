using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpm.Productie
{
    public class TijdEntry
    {
        internal DateTime _gestopt;

        public TijdEntry(string omschrijving, DateTime start, DateTime stop, Rooster rooster)
        {
            Omschrijving = omschrijving;
            Start = start;
            Stop = stop;
            ID = start.GetHashCode();
            WerkRooster = rooster;
        }

        public TijdEntry(DateTime start, DateTime stop)
        {
            Omschrijving = string.Empty;
            Start = start;
            Stop = stop;
            ID = start.GetHashCode();
        }

        public TijdEntry(DateTime start, DateTime stop, Rooster rooster)
        {
            Omschrijving = string.Empty;
            Start = start;
            Stop = stop;
            ID = start.GetHashCode();
            WerkRooster = rooster;
        }

        public TijdEntry()
        {
            Omschrijving = string.Empty;
            Start = DateTime.Now;
            Stop = DateTime.Now;
            ID = Start.GetHashCode();
        }

        public int ID { get; set; }
        public string Omschrijving { get; set; }
        public DateTime Start { get; set; }

        public DateTime Stop
        {
            get => InUse ? DateTime.Now : _gestopt;
            set => _gestopt = value;
        }

        public Rooster WerkRooster { get; set; }

        public double TotaalTijd => TijdGewerkt(WerkRooster, null,null);
        public bool InUse { get; set; }
        public ExtraTijd ExtraTijd { get; set; }

        public TijdEntry CreateRange(DateTime start, DateTime stop, Rooster rooster, List<Rooster> specialeroosters)
        {
            rooster ??= WerkRooster;
            var xstart = Start;
            var xstartrooster = specialeroosters?.FirstOrDefault(x => x.Vanaf.Date == start.Date)??rooster;
            var xstoprooster = specialeroosters?.FirstOrDefault(x => x.Vanaf.Date == start.Date)??rooster;
            if (xstartrooster != null && start.TimeOfDay == new TimeSpan())
                start = start.Add(xstartrooster.StartWerkdag);
            var xstop = Stop;
            if (xstoprooster != null && stop.TimeOfDay == new TimeSpan())
                stop = stop.Add(xstoprooster.EindWerkdag);
            if (start < xstart && stop < xstart || start > xstop && stop > xstop) return null;
            if (xstart < start)
                xstart = start;
            if (xstop > stop)
                xstop = stop;
            return new TijdEntry(xstart, xstop, rooster);
        }

        public double TijdGewerkt(Rooster rooster, Dictionary<DateTime, DateTime> exclude, List<Rooster> speciaaleRoosters, double extratijd = 0)
        {
            return Math.Round(Werktijd.TijdGewerkt(this, rooster, speciaaleRoosters, exclude, extratijd).TotalHours, 2);
        }

        public override bool Equals(object obj)
        {
            if (obj is TijdEntry entry)
            {
                if (ID != 0 && entry.ID != 0 && entry.ID == ID) return true;
                if (entry.ExtraTijd != null)
                {
                    if (ExtraTijd == null)
                        return false;
                    return entry.ExtraTijd.Equals(ExtraTijd);
                }

                if (ExtraTijd != null)
                {
                    return false;
                }

                if (entry.Start >= Start && entry.Start <= Start &&
                    entry.Stop >= Stop && entry.Stop <= Stop)
                    return true;

                //if (entry.Start == Start || entry.Stop == Stop)
                //    return true;
                //if ((entry.Start >= Start && entry.Start < Stop) && (entry.Stop <= Stop)) // geef de tijd een bereik van 5 minuten extra.
                //    return true;
                return false;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}