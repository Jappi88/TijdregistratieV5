using System;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.ViewModels
{
    public class TaakModel
    {
        public TaakModel(Taak taak)
        {
            Parent = taak;
        }

        public Taak Parent { get; set; }
        public string Beschrijving => Parent == null ? "N/A" : Parent.Beschrijving;
        public string Type => Parent == null ? "N/A" : Enum.GetName(typeof(AktieType), Parent.Type);
        public string Status => Parent == null ? "N/A" : Parent.Uitgevoerd ? "Al Uitgevoerd" : "Nog Niet Uitgevoerd";
    }
}