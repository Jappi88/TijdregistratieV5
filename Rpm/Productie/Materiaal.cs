using System;
using System.Runtime.Serialization;

namespace Rpm.Productie
{
    [DataContract]
    public class Materiaal
    {
        internal double _aantal;

        public Materiaal()
        {
        }

        public Materiaal(ProductieFormulier form)
        {
            Parent = form;
        }
        public int ID { get; set; }
        public ProductieFormulier Parent { get; set; }

        public string Path => (Parent == null ? "" : Parent.ProductieNr) + $"\\Materialen\\{ArtikelNr}";

        public string ArtikelNr { get; set; }
        public string Omschrijving { get; set; }
        public string Locatie { get; set; }
        public string Eenheid { get; set; }

        public double AantalPerStuk { get; set; }

        public double Aantal
        {
            get
            {
                if (Parent == null) return _aantal;
                var xret = Math.Round(Parent.TotaalGemaakt * AantalPerStuk,4);
                return xret == 0 && Parent.TotaalGemaakt > 0 ? 1 : xret;
            }
            set
            {
                if (Parent is {TotaalGemaakt: > 0})
                    AantalPerStuk = Math.Round(value / Parent.TotaalGemaakt,4);
                _aantal = value;
            }
        }

        public double AantalAfkeur { get; set; }
        public bool IsKlaarGezet { get; set; }

        public string AfKeurProcent()
        {
            try
            {
                var aantal = (decimal)(AantalPerStuk * Parent?.TotaalGemaakt ?? Aantal);
                var afkeur = (decimal) AantalAfkeur;
                decimal value = afkeur == 0 || aantal == 0? 0 : (afkeur / aantal);
                return value.ToString("0.00%");
            }
            catch (Exception e)
            {
                return "0.00%";
            }
        }
    }
}