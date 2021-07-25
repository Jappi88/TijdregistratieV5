using ProductieManager.Productie;
using System;
using System.Collections.Generic;

namespace ProductieManager.Classes.ViewModels
{
    public class VrijeTijdModel
    {
        public Personeel PersoneelLid { get; set; }
        public Dictionary<DateTime, DateTime> VrijeTijd { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public string StartTijd { get => Start.ToString("dddd dd MMMM yyyy HH:mm"); }
        public string EindTijd { get => Stop.ToString("dddd dd MMMM yyyy HH:mm"); }

        public string TotaalUur
        {
            get
            {
                return AantalUrenVrij().ToString() + " uur";
            }
        }

        public Dictionary<DateTime, DateTime> GetVrijeDagen()
        {
            Dictionary<DateTime, DateTime> xreturn = new Dictionary<DateTime, DateTime> { };
            if (VrijeTijd != null && VrijeTijd.Count > 0)
            {
                foreach (KeyValuePair<DateTime, DateTime> v in VrijeTijd)
                    if (v.Key != Start && v.Value != Stop)
                        xreturn.Add(v.Key, v.Value);
            }
            return xreturn;
        }

        public double AantalUrenVrij()
        {
            return Math.Round(ProductieManager.Productie.Werktijd.TijdGewerkt(Start, Stop, PersoneelLid.BeginDag, PersoneelLid.EindDag, GetVrijeDagen()).TotalHours, 2);
        }

        public VrijeTijdModel(Personeel pers, DateTime start, DateTime stop)
        {
            PersoneelLid = pers;
            Start = start;
            Stop = stop;
        }
    }
}