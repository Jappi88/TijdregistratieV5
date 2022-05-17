using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Polenter.Serialization;

namespace Rpm.Productie
{
    public class Rooster
    {
        private TimeSpan _duurpauze1;
        private TimeSpan _duurpauze2;
        private TimeSpan _duurpauze3;
        private readonly TimeSpan _Maxduurpauze = new TimeSpan(0, 1, 0, 0);
        public TimeSpan StartWerkdag { get; set; }
        public TimeSpan EindWerkdag { get; set; }
        public TimeSpan StartPauze1 { get; set; }
        public TimeSpan DuurPauze1 { get=> _duurpauze1 >= _Maxduurpauze? _Maxduurpauze : _duurpauze1;
            set => _duurpauze1 = value;
        }
        public TimeSpan DuurPauze2
        {
            get => _duurpauze2 >= _Maxduurpauze ? _Maxduurpauze : _duurpauze2;
            set => _duurpauze2 = value;
        }
        public TimeSpan DuurPauze3
        {
            get => _duurpauze3 >= _Maxduurpauze ? _Maxduurpauze : _duurpauze3;
            set => _duurpauze3 = value;
        }
        public TimeSpan StartPauze2 { get; set; }
        public TimeSpan StartPauze3 { get; set; }
        public bool GebruiktPauze { get; set; }
        [ExcludeFromSerialization] public bool GebruiktBereik { get; set; }
        public bool GebruiktVanaf { get; set; }
        public DateTime Vanaf { get; set; }
        public bool GebruiktTot { get; set; }
        public DateTime Tot { get; set; }

        public bool IsValid()
        {
            bool xreturn = true;
            if (GebruiktVanaf)
                xreturn &= DateTime.Now >= Vanaf;
            if (GebruiktTot)
                xreturn &= DateTime.Now <= Tot;
            return xreturn;
        }

        public bool IsCustom()
        {
            var rooster = Manager.Opties?.WerkRooster;
            if (rooster == null) return true;

            bool xreturn = IsValid() && !SameTijden(rooster);
            return xreturn;
        }

        public bool IsSpecial()
        {
            return !GebruiktVanaf && (Vanaf.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday || (Manager.Opties?.NationaleFeestdagen != null &&
                Manager.Opties.NationaleFeestdagen.Any(x => x.Date == Vanaf.Date)));
        }

        public static Rooster StandaartRooster()
        {
            return new Rooster
            {
                StartWerkdag = new TimeSpan(07, 30, 00),
                EindWerkdag = new TimeSpan(16, 30, 00),
                StartPauze1 = new TimeSpan(09, 45, 00),
                DuurPauze1 = new TimeSpan(00, 15, 00),
                StartPauze2 = new TimeSpan(12, 00, 00),
                DuurPauze2 = new TimeSpan(00, 30, 00),
                StartPauze3 = new TimeSpan(14, 45, 00),
                DuurPauze3 = new TimeSpan(00, 15, 00),
                GebruiktPauze = true
            };
        }

        public double TotaalPauze()
        {
            if (!GebruiktPauze) return 0;
            return (DuurPauze1 + DuurPauze2 + DuurPauze3).TotalHours;
        }

        public bool SameTijden(Rooster r)
        {
            if (r == null) return false;
            if (r.GebruiktPauze != GebruiktPauze) return false;
            if (r.StartWerkdag != StartWerkdag) return false;
            if (r.EindWerkdag != EindWerkdag) return false;
            if (r.StartPauze1 != StartPauze1) return false;
            if (r.StartPauze2 != StartPauze2) return false;
            if (r.StartPauze3 != StartPauze3) return false;
            if (r.DuurPauze1 != DuurPauze1) return false;
            if (r.DuurPauze2 != DuurPauze2) return false;
            if (r.DuurPauze3 != DuurPauze3) return false;
            return true;
        }

        public string WerkRoosterInfo()
        {
            var pauze = Math.Round(TotaalPauze(), 2);
            var xp = pauze > 0 ? $" met {pauze} uur pauze" : " zonder pauze";
            var rstitle = $"Rooster van {StartWerkdag.ToString(@"hh\:mm")} tot {EindWerkdag.ToString(@"hh\:mm")} uur,{xp}";
            return rstitle;
        }
    }
}