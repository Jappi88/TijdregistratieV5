using System;
using System.Collections.Generic;
using System.Linq;
using Rpm.Misc;

namespace Rpm.Productie.AantalHistory
{
    public class AantalRecord
    {
        public int ID { get; private set; }
        public int Aantal { get; set; }
        public int LastAantal { get; set; }
        public int Gemaakt => GetGemaakt();

        public DateTime DateChanged { get; set; }

        internal DateTime _endDate;

        public bool IsActive => _endDate.IsDefault();
        

        public DateTime EndDate
        {
            get => _endDate;
            set => _endDate = value;
        }

        public DateTime GetGestopt()
        {
            if (IsActive)
            {
                var dt = DateTime.Now;
                var xt = DateChanged;
                if (xt.Date != dt.Date)
                    return new DateTime(xt.Year, xt.Month, xt.Day, DateChanged.Hour, DateChanged.Minute, 0);
                return dt;
            }
            return EndDate;
        }

        public AantalRecord()
        {
            DateChanged = DateTime.Now;
            ID = DateChanged.GetHashCode();
        }

        public AantalRecord(int aantal) : this()
        {
            Aantal = aantal;
        }

        public int GetGemaakt()
        {
            if (LastAantal <= 0) return 0;
            if (LastAantal > Aantal)
                return LastAantal - Aantal;
            return Aantal - LastAantal;
        }

        public bool ContainsBereik(TijdEntry bereik, Rooster rooster, List<Rooster> specialeroosters)
        {
            if (bereik == null) return true;
            var xstart = bereik.Start;
            var xstop = bereik.Stop;
            if (bereik.Start.TimeOfDay == new TimeSpan())
            {
                var xstartr = specialeroosters?.FirstOrDefault(x => x.Vanaf.Date == bereik.Start.Date) ??
                              rooster;
                if (xstartr != null)
                    xstart = bereik.Start.Add(xstartr.StartWerkdag);
            }
            if (bereik.Stop.TimeOfDay == new TimeSpan())
            {
                var xstartr = specialeroosters?.FirstOrDefault(x => x.Vanaf.Date == bereik.Stop.Date) ??
                              rooster;
                if (xstartr != null)
                    xstop = bereik.Stop.Add(xstartr.EindWerkdag);
            }

            return new TijdEntry(DateChanged, GetGestopt()).ContainsBereik(new TijdEntry(xstart, xstop));
        }

        public double GetPerUur(UrenLijst uren, Dictionary<DateTime, DateTime> exclude = null)
        {
            var aantal = (double)GetGemaakt();
            var tijd = GetTijdGewerkt(uren, exclude);
            var pu = tijd > 0 ? aantal > 0 ? (aantal / tijd) : 0 : aantal;
            return (int)pu;
        }

        public double GetTijdGewerkt(UrenLijst uren, Dictionary<DateTime, DateTime> exclude = null)
        {
            return Math.Round(
                Werktijd.TijdGewerkt(new TijdEntry(DateChanged, GetGestopt()), uren?.WerkRooster, uren?.SpecialeRoosters,exclude).TotalHours, 2);
        }

        public override bool Equals(object obj)
        {
            if (obj is AantalRecord record)
                return record.ID == ID;
            return false;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
