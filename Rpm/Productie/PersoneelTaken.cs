using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable All

namespace Rpm.Productie
{
    public class PersoneelTakenLijst
    {
        public List<string> Taken { get; set; } = new List<string>();
        public List<Personeel> Personen { get; set; } = new();
        public List<PersoneelTaak> PersoneelTaken { get; set; } = new();

        public void Refresh()
        {
            try
            {
                if (Personen?.Count == 0) throw new Exception("Geen personeel ingezet!");
                if (Taken?.Count == 0) throw new Exception("Geen taken toegevoegd!");
                //hier even iets maken dat we de juiste mensen de juiste taken geven.
                var personen = Personen.Where(x => x.IsAanwezig).ToList();
                if (personen.Count == 0) throw new Exception("Geen aanwezige personeel!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}