using System;
using System.Collections.Generic;
using Rpm.Misc;

namespace Rpm.Productie.AantalHistory
{
    public class AantalRecord
    {
        public int Aantal { get; set; }
        public int LastAantal { get; set; }

        public DateTime DateChanged { get; set; }

        internal DateTime _endDate;

        public bool IsActive => _endDate.IsDefault();

        public DateTime EndDate
        {
            get
            {
                if (_endDate.IsDefault())
                    return DateTime.Now;
                return _endDate;
            }
            set => _endDate = value;
        }

        public AantalRecord()
        {
            DateChanged = DateTime.Now;
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
                Werktijd.TijdGewerkt(new TijdEntry(DateChanged, EndDate), uren?.WerkRooster, uren?.SpecialeRoosters,exclude).TotalHours, 2);
        }
    }
}
