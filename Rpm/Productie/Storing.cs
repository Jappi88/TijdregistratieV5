using Polenter.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Rpm.Productie
{
    [DataContract]
    public class Storing
    {
        public Storing()
        {
            Gestart = DateTime.Now;
            InstanceId = Gestart.GetHashCode();
            Gestopt = DateTime.Now;
            StoringType = "";
            Omschrijving = "";
            Oplossing = "";
            VerholpenDoor = "";
            GemeldDoor = "";
            WerkPlek = "";
            Path = "";
        }

        public Storing(WerkPlek werkplek, string omschrijving, string naampersoneel) : this()
        {
            WerkPlek = werkplek?.Naam;
            Plek = werkplek;
            Omschrijving = omschrijving;
            GemeldDoor = naampersoneel;
        }

        public Storing(WerkPlek werkplek, string omschrijving, string naampersoneel, Rooster rooster) : this(werkplek,
            omschrijving, naampersoneel)
        {
            WerkRooster = rooster;
        }

        public int Id { get; set; }
        public int InstanceId { get; set; }

        public Rooster WerkRooster { get; set; }

        public DateTime Gestart { get; set; }
        public DateTime Gestopt { get; set; }
        public string StoringType { get; set; }
        public string Omschrijving { get; set; }
        public string Oplossing { get; set; }
        public string VerholpenDoor { get; set; }
        public string GemeldDoor { get; set; }
        public string WerkPlek { get; set; }
        [ExcludeFromSerialization]
        public WerkPlek Plek { get; set; }
        public string Path { get; set; }
        public bool IsVerholpen { get; set; }
        [ExcludeFromSerialization] public double TotaalTijd => GetTotaleTijd();

        public double GetTotaleTijd()
        {
            var gestopt = IsVerholpen ? Gestopt : DateTime.Now;
            return GetTotaleTijd(Gestart, gestopt);
        }
        
        public double GetTotaleTijd(DateTime vanaf, DateTime tot)
        {
            if (string.IsNullOrEmpty(Path)) return 0;
            var werk = Plek??Werk.FromPath(Path)?.Plek;
            if (werk == null) return 0;
            if (vanaf.TimeOfDay == new TimeSpan() && WerkRooster != null)
                vanaf = vanaf.Add(WerkRooster.StartWerkdag);
            var gestopt = IsVerholpen ? Gestopt : DateTime.Now;
            if (tot.TimeOfDay == new TimeSpan() && WerkRooster != null)
                tot = tot.Add(WerkRooster.EindWerkdag);
            if (gestopt > tot) gestopt = tot;
            var gestart = Gestart < vanaf ? vanaf : Gestart;
            return Math.Round(Werktijd.TijdGewerkt(gestart, gestopt, werk.Tijden?.WerkRooster, werk.Tijden?.SpecialeRoosters).TotalHours, 2);
        }

        public DateTime GestartOp(TijdEntry bereik, List<Rooster> specialeroosters)
        {
            if (bereik == null) return Gestart;
            var xspr = specialeroosters?.FirstOrDefault(x => x.Vanaf.Date == Gestart.Date) ?? WerkRooster;
            return new TijdEntry(Gestart,Gestopt).CreateRange(bereik.Start, bereik.Stop,xspr, specialeroosters)?.Start??new DateTime();
        }

        public DateTime GestoptOp(TijdEntry bereik, List<Rooster> specialeroosters)
        {
            var stop = IsVerholpen ? Gestopt : DateTime.Now;
            if (bereik == null)
                return stop;
            var xspr = specialeroosters?.FirstOrDefault(x => x.Vanaf.Date == stop.Date)??WerkRooster;
            return new TijdEntry(Gestart, stop).CreateRange(bereik.Start, bereik.Stop, xspr, specialeroosters)?.Stop ??
                   stop;
        }

        public new string ToString()
        {
            return $"[{StoringType} op {WerkPlek}] {Omschrijving}";
        }

        public override bool Equals(object obj)
        {
            if (obj is string value)
            {
                if (string.Equals(value, ToString(), StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }
            else if (obj is Storing st)
            {
                //if (string.Equals(st.ToString(), ToString(), StringComparison.CurrentCultureIgnoreCase))
                //    return true;
                return st.InstanceId == InstanceId;
                //var xreturn = st.StoringType == StoringType;

                //if (st.Path == null && Path != null || Path == null && st.Path != null)
                //    return false;

                //if (st.Omschrijving == null && Omschrijving != null ||
                //    Omschrijving == null && st.Omschrijving != null)
                //    return false;

                //if (st.WerkPlek == null && WerkPlek != null || WerkPlek == null && st.WerkPlek != null)
                //    return false;

                //if (st.GemeldDoor == null && GemeldDoor != null || GemeldDoor == null && st.GemeldDoor != null)
                //    return false;

                //if (st.Path != null && Path != null)
                //    xreturn &= st.Path.ToLower() == Path.ToLower();
                //if (st.Omschrijving != null && Omschrijving != null)
                //    xreturn &= st.Omschrijving.ToLower() == Omschrijving.ToLower();
                //if (st.GemeldDoor != null && GemeldDoor != null)
                //    xreturn &= st.GemeldDoor.ToLower() == GemeldDoor.ToLower();

                //return xreturn;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return InstanceId;
        }
    }
}