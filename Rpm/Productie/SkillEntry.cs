using System;
using System.Runtime.Serialization;

namespace Rpm.Productie
{
    [DataContract]
    public class SkillEntry
    {
        private double _tijdgewerkt;

        public SkillEntry()
        {
            Gestart = DateTime.Now;
            Gestopt = DateTime.Now;
            BijGewerkt = DateTime.Now;
        }

        public DateTime Gestart { get; set; }
        public DateTime Gestopt { get; set; }
        public DateTime BijGewerkt { get; set; }
        public string Path { get; set; }
        public string Omschrijving { get; set; }
        public string ArtikelNr { get; set; }
        public string WerkPlek { get; set; }

        public double TijdGewerkt
        {
            get => Math.Round(_tijdgewerkt, 2);
            set => _tijdgewerkt = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is SkillEntry entry)
            {
                if (entry.Path != null && Path == null || entry.Path == null && Path != null)
                    return false;
                if (entry.ArtikelNr != null && ArtikelNr == null || entry.ArtikelNr == null && ArtikelNr != null)
                    return false;
                if (entry.WerkPlek != null && WerkPlek == null || entry.WerkPlek == null && WerkPlek != null)
                    return false;
                return string.Equals(entry.Path, Path, StringComparison.CurrentCultureIgnoreCase) &&
                       string.Equals(entry.ArtikelNr, ArtikelNr, StringComparison.CurrentCultureIgnoreCase) &&
                       string.Equals(entry.WerkPlek, WerkPlek, StringComparison.CurrentCultureIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Path == null || ArtikelNr == null || WerkPlek == null)
                return base.GetHashCode();
            return Path.GetHashCode() & ArtikelNr.GetHashCode() & WerkPlek.GetHashCode();
        }
    }
}