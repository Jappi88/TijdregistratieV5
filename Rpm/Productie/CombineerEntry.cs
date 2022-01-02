using Polenter.Serialization;

namespace Rpm.Productie
{
    public class CombineerEntry
    {
        public string ProductieNr { get; set; }
        public string BewerkingNaam { get; set; }
        public double Activiteit { get; set; } = 100;
        public string Path => System.IO.Path.Combine(ProductieNr, BewerkingNaam);
    }
}
