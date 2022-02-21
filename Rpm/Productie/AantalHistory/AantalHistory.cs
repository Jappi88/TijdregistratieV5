using System;
using System.Collections.Generic;
using System.Linq;
using Rpm.Misc;

namespace Rpm.Productie.AantalHistory
{
    public class AantallenRecords
    {
        public AantallenRecords()
        {
            Aantallen.Add(new AantalRecord(0));
        }

        public List<AantalRecord> Aantallen { get; } = new();
        public bool IsActive => Aantallen?.Any(x => x.IsActive) ?? false;

        public void SetActive(bool active, AantalRecord record = null)
        {
            var xent = record ?? Aantallen.LastOrDefault();
            if (xent == null) return;

            //if (!active)
            // {

            foreach (var xt in Aantallen)
                if (xt._endDate.IsDefault())
                    xt._endDate = DateTime.Now;
            // }
            if (active)
                xent._endDate = default;
            else if (xent.IsActive)
                xent._endDate = DateTime.Now;
        }

        public bool UpdateAantal(int aantal, UrenLijst tijden)
        {
            try
            {
                lock (Aantallen)
                {
                    if (aantal > 0 && Aantallen.Any(x => x.LastAantal == aantal)) return false;
                    Aantallen.RemoveAll(x => x.LastAantal >= aantal);
                    if (aantal == 0) return true;
                    var xent = Aantallen.Count > 0
                        ? Aantallen.LastOrDefault(x => x.IsActive ||
                                                       x.EndDate.AddMinutes(5) >= DateTime.Now)
                        : null;
                    var xlast = Aantallen.LastOrDefault();

                    var xstart = xlast?.EndDate ?? tijden?.GetFirstStart() ?? DateTime.Now;
                    var xtijd = tijden?.Uren.FirstOrDefault(x =>
                        x.ContainsBereik(new TijdEntry(DateTime.Now, DateTime.Now)));
                    if (xtijd != null)
                    {
                        if (xlast != null && xtijd.ContainsBereik(new TijdEntry(xlast.DateChanged, xlast.EndDate)))
                            xstart = xlast.EndDate;
                        else
                            xstart = xtijd.Start;
                    }

                    if (xstart.IsDefault())
                        xstart = DateTime.Now;
                    if (xent != null)
                    {
                        var xindex = Aantallen.IndexOf(xent);
                        //xent.Aantal = aantal;
                        //xent.DateChanged = DateTime.Now;
                        if (xindex > -1)
                        {
                            Aantallen[xindex].LastAantal = aantal;
                            Aantallen[xindex].EndDate = DateTime.Now;
                            return true;
                        }
                    }


                    var xaantal = xlast?.LastAantal ?? 0;
                    xent = new AantalRecord(xaantal)
                    {
                        DateChanged = xstart,
                        EndDate = DateTime.Now,
                        LastAantal = aantal
                    };
                    Aantallen.Add(xent);
                }

                //SetActive(active);
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
                foreach (var x in Aantallen)
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

        public int AantalGemaakt(UrenLijst uren, ref double tijd, bool predictaantal, int multiply, int peruur,
            Dictionary<DateTime, DateTime> exclude = null)
        {
            var xfirst = Aantallen.OrderBy(x => x.DateChanged).FirstOrDefault();
            if (xfirst == null)
            {
                tijd = 0;
                return 0;
            }

            return AantalGemaakt(xfirst.DateChanged, DateTime.Now, ref tijd, uren, predictaantal, multiply, peruur,
                exclude);
        }

        public int AantalGemaakt(UrenLijst uren, ref double tijd, bool predictaantal, int multiply)
        {
            var xfirst = Aantallen.OrderBy(x => x.DateChanged).FirstOrDefault();
            if (xfirst == null)
            {
                tijd = 0;
                return 0;
            }

            return AantalGemaakt(xfirst.DateChanged, DateTime.Now, ref tijd, uren, predictaantal, multiply);
        }

        public int AantalGemaakt(DateTime stop, ref double tijd, UrenLijst uren, bool predictaantal, int multiply)
        {
            var xfirst = Aantallen.OrderBy(x => x.DateChanged).FirstOrDefault();
            if (xfirst == null)
            {
                tijd = 0;
                return 0;
            }

            return AantalGemaakt(xfirst.DateChanged, stop, ref tijd, uren, predictaantal, multiply);
        }

        public AantalRecord[] GetRecords(TijdEntry bereik, Rooster rooster, List<Rooster> specialeroosters)
        {
            if (bereik == null) return Aantallen.ToArray();
            return Aantallen.Where(x => x.ContainsBereik(bereik, rooster, specialeroosters)).ToArray();
        }

        public int AantalGemaakt(DateTime start, DateTime stop, ref double tijd, UrenLijst uren, bool predictaantal,
            int multiply = 1, int peruur = -1, Dictionary<DateTime, DateTime> exclude = null)
        {
            try
            {
                var gemaakt = 0;

                var xaantallen = Aantallen.Where(x =>
                        x.ContainsBereik(new TijdEntry(start, stop), uren?.WerkRooster, uren?.SpecialeRoosters))
                    .OrderBy(x => x.Aantal).ToList();

                var exc = exclude ?? new Dictionary<DateTime, DateTime>();
                var pus = new List<double>();
                foreach (var x in xaantallen)
                {
                    var xgemaakt = x.GetGemaakt();
                    var xtijd = x.GetTijdGewerkt(uren, exc);
                    gemaakt += xgemaakt;
                    tijd += xtijd;
                    if (xtijd > 0 && xgemaakt > 0) pus.Add(xgemaakt / xtijd);
                }

                if (predictaantal && pus.Count > 0)
                {
                    var xan = Aantallen.OrderBy(x => x.EndDate).ToList();
                    var xlast = xan.LastOrDefault();
                    if (xlast != null)
                    {
                        var xtijd = Werktijd
                            .TijdGewerkt(new TijdEntry(xlast.EndDate, stop), uren?.WerkRooster, uren?.SpecialeRoosters,
                                exc)
                            .TotalHours;
                        var xaantal = (int) (pus.Sum() / pus.Count * (xtijd * multiply));
                        gemaakt += xaantal;
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