using System;

namespace ProductieManager.Productie
{
    public class SkillEntry
    {
        public DateTime Gestart { get; set; }
        public DateTime Gestopt { get; set; }
        public string Path { get; set; }
        public string Omschrijving { get; set; }
        public string ArtikelNr { get; set; }
        public string WerkPlek { get; set; }
        public double Tijdgewerkt { get; set; }

        public SkillEntry()
        {
            Gestart = DateTime.Now;
            Gestopt = DateTime.Now;
        }
    }
}