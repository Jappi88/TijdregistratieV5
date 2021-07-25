using System;

namespace ProductieManager.Mailing
{
    [Serializable]
    public class UitgaandAdres
    {
        public string Adres { get; set; }

        public ProductieState[] States { get; set; }

        public UitgaandAdres()
        {
            States = new ProductieState[] { };
        }

        public UitgaandAdres(string adres, ProductieState[] states)
        {
            States = states;
            Adres = adres;
        }
    }
}