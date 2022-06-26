using Polenter.Serialization;
using System;
using System.Drawing;
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

        [ExcludeFromSerialization]
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
                if (Parent == null) return 0;
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

        public double AantalNodig
        {
            get
            {
                if (Parent == null) return 0;
                var xret = Math.Round(Parent.Aantal * AantalPerStuk, 4);
                return xret == 0 && Parent.Aantal > 0 ? 1 : xret;
            }
            set
            {
                if (Parent is { Aantal: > 0 })
                    AantalPerStuk = Math.Round(value / Parent.Aantal, 4);
                _aantal = value;
            }
        }

        public double AantalAfkeur { get; set; }
        public bool IsKlaarGezet { get; set; }

        public string AfKeurProcent(decimal afkeur)
        {
            try
            {
                decimal value = AfKeurProcentValue(afkeur);
                return value.ToString("0.00%");
            }
            catch (Exception e)
            {
                return "0.00%";
            }
        }

        public decimal AfKeurProcentValue(decimal afkeur)
        {
            try
            {
                var aantal = (decimal)(AantalPerStuk * Parent?.TotaalGemaakt ?? Aantal);
                decimal value = afkeur == 0 || aantal == 0 ? 0 : (afkeur / aantal);
                return value;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public Color GetAfkeurProcentColor(decimal afkeur)
        {
            var ret = Color.Green;
            var value = AfKeurProcentValue(afkeur);
            if (value is > 0 and < (decimal)0.01)
                ret = Color.Orange;
            else if (value >= (decimal)0.01)
                ret = Color.Red;
            return ret;
        }
    }
}