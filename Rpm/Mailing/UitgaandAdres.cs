using System;
using System.Collections.Generic;
using Rpm.Various;

namespace Rpm.Mailing
{
    [Serializable]
    public class UitgaandAdres
    {
        public UitgaandAdres()
        {
            States = new ProductieState[] { };
            Adres = "";
        }

        public UitgaandAdres(string adres, ProductieState[] states)
        {
            States = states;
            Adres = adres;
        }

        public string Adres { get; set; }

        public ProductieState[] States { get; set; }
        public bool SendStoringMail { get; set; }
        public bool SendWeekOverzichten { get; set; }
        public int VanafWeek { get; set; }
        public int VanafYear { get; set; }
        public List<WeekOverzicht> VerzondenWeekOverzichten { get; set; } = new();
    }
}