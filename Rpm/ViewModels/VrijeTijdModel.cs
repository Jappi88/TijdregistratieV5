using System;
using System.Collections.Generic;
using Rpm.Productie;

namespace Rpm.ViewModels
{
    public class xVrijeTijdModel
    {
        public xVrijeTijdModel(Personeel pers, DateTime start, DateTime stop)
        {
            PersoneelLid = pers;
            Start = start;
            Stop = stop;
        }

        public Personeel PersoneelLid { get; set; }
        public Dictionary<DateTime, DateTime> VrijeTijd { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public string StartTijd => Start.ToString("dddd dd MMMM yyyy HH:mm");
        public string EindTijd => Stop.ToString("dddd dd MMMM yyyy HH:mm");

        public string TotaalUur => AantalUrenVrij() + " uur";

        public Dictionary<DateTime, DateTime> GetVrijeDagen()
        {
            var xreturn = new Dictionary<DateTime, DateTime>();
            if (VrijeTijd is {Count: > 0})
                foreach (var v in VrijeTijd)
                    if (v.Key != Start && v.Value != Stop)
                        xreturn.Add(v.Key, v.Value);
            return xreturn;
        }

        public double AantalUrenVrij()
        {
            var rooster = PersoneelLid.WerkRooster ?? Manager.Opties.GetWerkRooster();
            return Math.Round(
                Werktijd.TijdGewerkt(new TijdEntry(Start, Stop, rooster), rooster, null,
                        GetVrijeDagen())
                    .TotalHours, 2);
        }
    }
}