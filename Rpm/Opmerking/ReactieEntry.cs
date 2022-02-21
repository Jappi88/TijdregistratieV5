using System;
using System.Collections.Generic;
using System.Linq;
using Rpm.Misc;
using Rpm.Productie;

namespace Rpm.Opmerking
{
    public class ReactieEntry
    {
        public ReactieEntry()
        {
            GelezenDoor = new List<string>();
            GelezenOp = DateTime.Now;
            Reactie = "";
            ReactieVan = Manager.Opties?.Username ?? "Onbekend";
        }

        public DateTime GelezenOp { get; set; }
        public List<string> GelezenDoor { get; set; }
        public string Reactie { get; set; }
        public string ReactieVan { get; set; }
        public DateTime ReactieOp { get; set; }

        public bool IsGelezen =>
            string.Equals(Manager.Opties?.Username, ReactieVan, StringComparison.CurrentCultureIgnoreCase) ||
            GelezenDoor.Any(x =>
                string.Equals(Manager.Opties?.Username, x, StringComparison.CurrentCultureIgnoreCase)) &&
            !GelezenOp.IsDefault();

        public void SetReactie(string reactie)
        {
            GelezenDoor.RemoveAll(x =>
                string.Equals(Manager.Opties?.Username, x, StringComparison.CurrentCultureIgnoreCase));
            GelezenDoor.Add(Manager.Opties?.Username ?? "Onbekend");
            GelezenOp = DateTime.Now;
            ReactieOp = DateTime.Now;
            ReactieVan = Manager.Opties?.Username ?? "Onbekend";
            Reactie = reactie;
        }

        public void SetGelezen()
        {
            GelezenOp = DateTime.Now;
            GelezenDoor.RemoveAll(x =>
                string.Equals(Manager.Opties?.Username, x, StringComparison.CurrentCultureIgnoreCase));
            GelezenDoor.Add(Manager.Opties?.Username ?? "Onbekend");
        }
    }
}