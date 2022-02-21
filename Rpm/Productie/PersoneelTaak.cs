using System.Collections.Generic;
using Rpm.Various;

namespace Rpm.Productie
{
    public class PersoneelTaak
    {
        public string TaakNaam { get; set; }
        public RefreshRate RefreshRate { get; set; }
        public List<string> Personen { get; set; } = new();
    }
}