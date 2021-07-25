using System;
using Rpm.Misc;
using Rpm.Productie;

namespace Rpm.Mailing
{
    public class WeekOverzicht
    {
        public WeekOverzicht()
        {
            Afdeling = Manager.LogedInGebruiker?.Username;
            var dt = DateTime.Now;
            WeekNr = dt.GetWeekNr();
            Jaar = dt.Year;
        }

        public WeekOverzicht(DateTime startdate)
        {
            Afdeling = Manager.LogedInGebruiker?.Username;
            WeekNr = startdate.GetWeekNr();
            Jaar = startdate.Year;
        }

        public WeekOverzicht(int weeknr, int jaar, string afdeling, int versie)
        {
            WeekNr = weeknr;
            Jaar = jaar;
            Afdeling = afdeling;
            Versie = versie;
        }

        public int WeekNr { get; set; }
        public int Jaar { get; set; }
        public string Afdeling { get; set; }
        public int Versie { get; set; }

        public static WeekOverzicht FromString(string value)
        {
            if (value == null) return null;
            try
            {
                var xs = value.Split('_');
                if (xs?.Length < 5) return null;
                if (!int.TryParse(xs[1], out var weeknr)) return null;
                if (!int.TryParse(xs[2], out var jaar)) return null;
                if (!int.TryParse(xs[4].Replace("V", ""), out var versie)) return null;
                return new WeekOverzicht(weeknr, jaar, xs[3], versie);
            }
            catch
            {
                return null;
            }
        }

        public new string ToString()
        {
            if (Afdeling == null) return null;
            return $"Week_{WeekNr}_{Jaar}_{Afdeling}_V{Versie}";
        }

        public override bool Equals(object obj)
        {
            if (obj is WeekOverzicht overzicht)
                return overzicht.WeekNr == WeekNr &&
                       overzicht.Jaar == Jaar &&
                       string.Equals(overzicht.Afdeling, Afdeling, StringComparison.CurrentCultureIgnoreCase) &&
                       overzicht.Versie == Versie;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}