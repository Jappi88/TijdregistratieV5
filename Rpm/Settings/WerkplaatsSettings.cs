using Rpm.Misc;
using Rpm.Productie;
using System.Collections.Generic;

namespace Rpm.Settings
{
    public class WerkplaatsSettings
    {
        public string Name { get; set; }
        public bool IsCompact { get; set; }
        public Rooster WerkRooster { get; set; }
        public List<Rooster> SpecialeRoosters { get; set; } = new List<Rooster>();
        public Filter Voorwaardes { get; set; }
    }
}
