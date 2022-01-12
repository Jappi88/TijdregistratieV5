using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using BrightIdeasSoftware;
using Rpm.Misc;
using Rpm.Various;

namespace Rpm.Productie.AantalHistory
{
    public class AantallenRecords
    {
        public List<AantalRecord> Aantallen { get; private set; } = new List<AantalRecord>();
        public bool IsActive => Aantallen?.Any(x => x.IsActive)?? false;

        public AantallenRecords()
        {
            Aantallen.Add(new AantalRecord(0));
        }

        public void SetActive(bool active, AantalRecord record = null)
        {
            var xent = record ?? Aantallen.LastOrDefault();
            if (xent == null)
            {
                return;
            }

            //if (!active)
            // {

            foreach (var xt in Aantallen)
            {
                if (xt._endDate.IsDefault())
                    xt._endDate = DateTime.Now;
            }
            // }
            if (active)
                xent._endDate = default;
            else if (xent.IsActive)
                xent._endDate = DateTime.Now;
        }

        public bool UpdateAantal(int aantal, bool active, UrenLijst tijden)
        {
            try
            {
                lock (Aantallen)
                {
                    if (aantal > 0 && Aantallen.Any(x => x.Aantal == aantal)) return false;
                    Aantallen.RemoveAll(x => x.Aantal >= aantal);
                    var xent = Aantallen.Count > 1
                        ? Aantallen.LastOrDefault(x => DateTime.Now >= x.DateChanged.Subtract(TimeSpan.FromMinutes(2)) &&
                                                       DateTime.Now <= x.DateChanged.AddMinutes(2))
                        : null;
                    if (xent != null)
                    {
                        var xindex = Aantallen.IndexOf(xent);
                        xent.Aantal = aantal;
                        xent.DateChanged = DateTime.Now;
                        if (xindex > 0)
                        {
                            Aantallen[xindex - 1].LastAantal = aantal;
                            Aantallen[xindex - 1].EndDate = DateTime.Now;
                        }
                        else
                        {
                            xent = new AantalRecord(aantal);
                            xent.LastAantal = aantal;
                            Aantallen.Add(xent);
                        }
                    }
                    else
                    {

                        var xlast = Aantallen.LastOrDefault();
                        var xstart = DateTime.Now;
                        if (xlast != null)
                        {
                            xlast.LastAantal = aantal;
                            if (xlast.DateChanged.Date != DateTime.Now)
                            {
                                var dt = xlast.DateChanged;
                                xlast.EndDate = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 0);
                                Rooster rs = tijden?.WerkRooster ??
                                             Manager.Opties?.GetWerkRooster() ?? Rooster.StandaartRooster();
                                xstart = Werktijd.EerstVolgendeWerkdag(xstart, ref rs, rs, tijden?.SpecialeRoosters);
                            }
                            else
                                xlast.EndDate = xstart;
                        }

                        xent = new AantalRecord(aantal);
                        xent.DateChanged = xstart;
                        Aantallen.Add(xent);
                    }
                }

                SetActive(active);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public int AantalGemaakt()
        {
            try
            {
                var xret = 0;
                foreach(var x in Aantallen)
                    if (x.Aantal > xret)
                        xret = x.Aantal;
                return xret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public int AantalGemaakt(UrenLijst uren, ref double tijd, bool predictaantal, int peruur, Dictionary<DateTime,DateTime> exclude = null)
        {
            var xfirst = Aantallen.OrderBy(x => x.DateChanged).FirstOrDefault();
            if (xfirst == null)
            {
                tijd = 0;
                return 0;
            }
            return AantalGemaakt(xfirst.DateChanged, DateTime.Now,ref tijd, uren, predictaantal, peruur, exclude);
        }

        public int AantalGemaakt(UrenLijst uren,ref double tijd, bool predictaantal)
        {
            var xfirst = Aantallen.OrderBy(x => x.DateChanged).FirstOrDefault();
            if (xfirst == null)
            {
                tijd = 0;
                return 0;
            }
            return AantalGemaakt(xfirst.DateChanged, DateTime.Now, ref tijd, uren, predictaantal);
        }

        public int AantalGemaakt(DateTime stop, ref double tijd, UrenLijst uren, bool predictaantal)
        {
            var xfirst = Aantallen.OrderBy(x => x.DateChanged).FirstOrDefault();
            if (xfirst == null)
            {
                tijd = 0;
                return 0;
            }
            return AantalGemaakt(xfirst.DateChanged, stop,ref tijd, uren, predictaantal);
        }

        public AantalRecord[] GetRecords(TijdEntry bereik, Rooster rooster, List<Rooster> specialeroosters)
        {
            if (bereik == null) return Aantallen.ToArray();
            return Aantallen.Where(x => x.ContainsBereik(bereik, rooster, specialeroosters)).ToArray();
        }

        public int AantalGemaakt(DateTime start, DateTime stop, ref double tijd, UrenLijst uren, bool predictaantal, int peruur = -1, Dictionary<DateTime, DateTime> exclude = null)
        {
            try
            {
                int gemaakt = 0;

                var xaantallen = Aantallen.Where(x => x.ContainsBereik(new TijdEntry(start, stop), uren?.WerkRooster, uren?.SpecialeRoosters))
                    .OrderBy(x => x.Aantal).ToList();
                
                Dictionary<DateTime, DateTime> exc = exclude ?? new Dictionary<DateTime, DateTime>();

                foreach (var x in xaantallen)
                {
                    if (!x.IsActive)
                    {
                        gemaakt += x.GetGemaakt();
                        tijd += x.GetTijdGewerkt(uren);

                        if (exc.ContainsKey(x.DateChanged))
                        {
                            if (x.GetGestopt() > exc[x.DateChanged])
                                exc[x.DateChanged] = x.GetGestopt();
                        }
                        else
                            exc.Add(x.DateChanged, x.GetGestopt());
                    }

                }

                var xfirst = Aantallen.FirstOrDefault();
                if (xfirst != null)
                {
                    if (start < xfirst.DateChanged)
                        start = xfirst.DateChanged;
                }
                if (predictaantal && IsActive)
                {
                    var xtijd = Werktijd
                        .TijdGewerkt(new TijdEntry(start, stop), uren?.WerkRooster, uren?.SpecialeRoosters, exc)
                        .TotalHours;
                    var pu = peruur > -1 ? peruur : tijd > 0 ? gemaakt > 0 ? (gemaakt / tijd) : 0 : gemaakt;
                    if (xtijd > 0 && pu > 0)
                    {
                        gemaakt += (int) (pu * xtijd);
                        tijd += xtijd;
                    }
                }

                return gemaakt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
    }
}
