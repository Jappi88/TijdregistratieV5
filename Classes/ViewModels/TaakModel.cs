using iTextSharp.text;
using ProductieManager.Classes.Productie;
using System;

namespace ProductieManager.Classes.ViewModels
{
    public class TaakModel
    {
        public Taak Parent { get; set; }
        public string Beschrijving { get { return Parent == null ? "N/A" : Parent.Beschrijving; } }
        public string Type { get { return Parent == null ? "N/A" : Enum.GetName(typeof(AktieType), Parent.Type); } }
        public string Status { get { return Parent == null ? "N/A" : Parent.Uitgevoerd ? "Al Uitgevoerd" : "Nog Niet Uitgevoerd"; } }
        public Image TaakImage { get; set; }

        public TaakModel(Taak taak)
        {
            Parent = taak;
        }

        public Image GetStatusImage()
        {
            try
            {
            }
            catch
            {
            }
            return null;
        }
    }
}