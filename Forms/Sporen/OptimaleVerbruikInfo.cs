using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Sporen
{
    public enum ZoekRichting
    {
        Boven,
        Onder,
        Geen
    }

    public class OptimaleVerbruikInfo
    {
        public decimal Reststuk { get; set; }
        public decimal Voorkeur1 { get; set; }
        public decimal Voorkeur2 { get; set; }
        public decimal Voorkeur3 { get; set; }

        public ZoekRichting ZoekVerbruikRichting { get; set; }
    }
}
