using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using Rpm.Misc;

namespace Rpm.Productie.AantalHistory
{
    public class AantallenRecords
    {
        public List<AantalRecord> Aantallen { get; private set; } = new List<AantalRecord>();

        public AantallenRecords()
        {
            Aantallen.Add(new AantalRecord(0));
        }

        public bool UpdateAantal(int aantal)
        {
            try
            {
                //var xold = Aantallen.FirstOrDefault(x => x.Aantal == aantal);
                //if (xold != null)
                //{
                //    var index = Aantallen.IndexOf(xold);
                //    if (index > 0)
                //    {
                //        Aantallen[index - 1].EndDate = DateTime.Now;
                //        Aantallen[index -1].LastAantal = aantal;
                //        if (index + 1 < Aantallen.Count)
                //        {
                //            Aantallen[index + 1].DateChanged = xold.EndDate;
                //            Aantallen[index + 1].LastAantal = aantal;
                //        }
                //    }
                //}
                var xent = Aantallen.LastOrDefault(x =>
                    DateTime.Now >= x.EndDate.Subtract(TimeSpan.FromMinutes(5)) &&
                    DateTime.Now <= x.EndDate.AddMinutes(5));
                if (xent != null)
                {
                    var xindex = Aantallen.IndexOf(xent);
                    if (xindex > 0)
                    {
                        Aantallen[xindex - 1].LastAantal = aantal;
                        Aantallen[xindex - 1].EndDate = DateTime.Now;
                    }

                    xent.Aantal = aantal;
                    xent.DateChanged = DateTime.Now;
                }
                else
                {
                    if (Aantallen.Any(x => x.Aantal == aantal)) return false;
                    Aantallen.RemoveAll(x => x.Aantal >= aantal);
                    var xlast = Aantallen.LastOrDefault();
                    if (xlast != null)
                    {
                        xlast.LastAantal = aantal;
                        xlast.EndDate = DateTime.Now;
                    }

                    Aantallen.Add(new AantalRecord(aantal));
                }

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

        public int AantalGemaakt(DateTime start, DateTime stop, ref double tijd, UrenLijst uren, bool predictaantal, int peruur = -1, Dictionary<DateTime, DateTime> exclude = null)
        {
            try
            {
                int gemaakt = 0;
              
                var xaantallen = Aantallen.Where(x => start < x.EndDate &&
                                                      stop >= x.EndDate).ToList();
                
                Dictionary<DateTime, DateTime> exc = exclude ?? new Dictionary<DateTime, DateTime>();
               
                foreach (var x in xaantallen)
                {
                    if (exc.ContainsKey(x.DateChanged))
                    {
                        if (x.EndDate > exc[x.DateChanged])
                            exc[x.DateChanged] = x.EndDate;
                    }
                    else
                        exc.Add(x.DateChanged, x.EndDate);
                    if (x.DateChanged >= start && x.EndDate <= stop)
                    {
                        gemaakt += x.GetGemaakt();
                        tijd += x.GetTijdGewerkt(uren);
                    }
                }

                if (predictaantal)
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
