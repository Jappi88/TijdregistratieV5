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
                        //else
                        //{

                        //    xent = new AantalRecord(aantal);
                        //    xent.DateChanged = xstart;
                        //    xent.LastAantal = aantal;
                        //    xent.EndDate = DateTime.Now;
                        //    Aantallen.Add(xent);
                        //}
                    }


                    var xaantal = xlast?.LastAantal??0;
                    //if (xlast != null)
                    //{
                    //    xlast.LastAantal = aantal;
                    //    xstart = xlast.EndDate;
                    //    xaantal = xlast.LastAantal;


                    //    //if (xlast.DateChanged.Date != DateTime.Now)
                    //    //{
                    //    //    var dt = xlast.DateChanged;
                    //    //    Rooster rs = tijden?.WerkRooster ??
                    //    //                 Manager.Opties?.GetWerkRooster() ?? Rooster.StandaartRooster();
                    //    //    xstart = Werktijd.EerstVolgendeWerkdag(xstart, ref rs, rs, tijden?.SpecialeRoosters);
                    //    //    var xgestopt = xlast.GetGestopt();
                    //    //    if (xstart > xlast.GetGestopt())
                    //    //    {
                    //    //        xlast.EndDate = new DateTime(xgestopt.Year, xgestopt.Month, xgestopt.Day, 23, 59, 0);
                    //    //    }
                    //    //    else xlast.EndDate = xstart;
                    //    //}
                    //    //else
                    //    //    xlast.EndDate = xstart;
                    //}
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
                    gemaakt += x.GetGemaakt();
                    tijd += x.GetTijdGewerkt(uren,exc);

                    if (exc.ContainsKey(x.DateChanged))
                    {
                        if (x.GetGestopt() > exc[x.DateChanged])
                            exc[x.DateChanged] = x.GetGestopt();
                    }
                    else
                        exc.Add(x.DateChanged, x.GetGestopt());
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
