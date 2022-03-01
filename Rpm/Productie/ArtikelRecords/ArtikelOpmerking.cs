using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polenter.Serialization;

namespace Rpm.Productie.ArtikelRecords
{
    public enum ArtikelFilter
    {
        GelijkAan,
        GelijkAanOfHoger,
        Hoger,
        HerhaalElke
    }

    public enum ArtikelFilterSoort
    {
        AantalGemaakt,
        TijdGewerkt
    }

    public enum FilterOp
    {
        Artikelen,
        Werkplaatsen,
        Beiden
    }

    public class ArtikelOpmerking
    {
        public int ID { get; private set; }
        public string GeplaatstDoor { get; set; } = string.Empty;
        public DateTime GeplaatstOp { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Opmerking { get; set; } = string.Empty;
        public ArtikelFilter Filter { get; set; } = ArtikelFilter.GelijkAan;
        public ArtikelFilterSoort FilterSoort { get; set; } = ArtikelFilterSoort.AantalGemaakt; 
        public decimal FilterWaarde { get; set; }
        public FilterOp FilterOp { get; set; }
        public byte[] ImageData { get; set; }
        public List<string> OpmerkingVoor { get; set; } = new List<string>();
        public Dictionary<string, DateTime> GelezenDoor { get; set; } = new Dictionary<string, DateTime>();
        public bool IsAlgemeen { get; set; }
        public int AantalGetoond { get; set; }
        public string Ontvangers => string.Join(", ", OpmerkingVoor);

        public bool IsFromMe =>
            string.Equals(Manager.Opties?.Username, GeplaatstDoor, StringComparison.CurrentCultureIgnoreCase);

        public ArtikelOpmerking()
        {
            GeplaatstOp = DateTime.Now;
            ID = GeplaatstOp.GetHashCode();
            GeplaatstDoor = Manager.Opties?.Username;
        }

        public ArtikelOpmerking(string opmerking) : this()
        {
            Opmerking = opmerking;
        }

        public ArtikelOpmerking(string opmerking, ArtikelFilter filter):this(opmerking)
        {
            Filter = filter;
        }


        public override bool Equals(object obj)
        {
            if (obj is ArtikelOpmerking op)
                return op.ID == ID;
            return false;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
