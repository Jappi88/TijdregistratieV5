using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductieManager.Classes.Productie
{
    public class Storing
    {
        [LiteDB.BsonId(true)]
        public LiteDB.ObjectId Id { get; set; }
        public DateTime Gestart { get; set; } = DateTime.Now;
        public DateTime Gestopt { get; set; } = DateTime.Now;
        public string Omschrijving { get; set; } = "";
        public string Oplossing { get; set; } = "";
        public string VerholpenDoor { get; set; } = "";
        public string GemeldDoor { get; set; } = "";
        public string WerkPlek { get; set; } = "";
        public bool IsVerholpen { get; set; }

        public Storing()
        {

        }

        public Storing(string werkplek, string omschrijving, string naampersoneel)
        {
            WerkPlek = werkplek;
            Omschrijving = omschrijving;
            GemeldDoor = naampersoneel;
        }
    }
}
