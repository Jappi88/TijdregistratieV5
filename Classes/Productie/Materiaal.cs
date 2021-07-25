using LiteDB;
using ProductieManager.Productie;

namespace ProductieManager.Classes.Productie
{
    public class Materiaal
    {
        [BsonRef]
        public ProductieFormulier Parent { get; set; }

        [BsonId]
        public string Path { get { return ((Parent == null ? "" : Parent.ProductieNr) + $"\\Materialen\\{ArtikelNr}"); } }

        public string ArtikelNr { get; set; }
        public string Omschrijving { get; set; }
        public string Locatie { get; set; }
        public string Eenheid { get; set; }
        public double AantalPerStuk { get; set; }
        private double _aantal;
        public double Aantal { get { return Parent == null ? _aantal : (Parent.Aantal * AantalPerStuk); } set { _aantal = value; } }
        public double AantalAfkeur { get; set; }
        public bool IsKlaarGezet { get; set; }

        public Materiaal()
        {
        }

        public Materiaal(ProductieFormulier form)
        {
            Parent = form;
        }
    }
}