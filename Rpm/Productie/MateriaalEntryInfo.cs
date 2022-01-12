using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpm.Productie;

namespace ProductieManager.Rpm.Productie
{
    public class MateriaalEntryInfo
    {
        public int ID { get; set; }
        public List<Materiaal> Materialen { get; set; } = new List<Materiaal>();
        public string ArtikelNr { get; set; }
        public string Omschrijving { get; set; }
        public string Eenheid { get; set; }
        public double Afkeur => Materialen.Sum(x => x.AantalAfkeur);
        public double Verbruik => Math.Round(Materialen.Sum(x => x.Aantal),2);
        public double PerEenHeid => Materialen.Count == 0 ? 0 : Math.Round(Materialen.Sum(x => x.AantalPerStuk) / Materialen.Count,2);
        public int AantalProducties => Materialen.Count;
    }
}
