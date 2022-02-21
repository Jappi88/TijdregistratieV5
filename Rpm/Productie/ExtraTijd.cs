using System;
using Polenter.Serialization;

namespace Rpm.Productie
{
    public class ExtraTijd
    {
        private TimeSpan _tijd;
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }

        [ExcludeFromSerialization]
        public TimeSpan Tijd
        {
            get => Stop - Start;
            set => _tijd = value;
        }

        public bool Herhaaldelijk { get; set; }
        public int Aantalkeer { get; set; }
        public Periode PeriodeSoort { get; set; }
        public DateTime Vanaf { get; set; }
        public DateTime Tot { get; set; }

        public ExtraTijd CreateRange(DateTime start, DateTime stop)
        {
            var xstart = Start;
            var xstop = Stop;
            if (start < xstart && stop < xstart || start > xstop && stop > xstop) return null;
            if (xstart < start)
                xstart = start;
            if (xstop > stop)
                xstop = stop;
            return new ExtraTijd
            {
                Start = xstart,
                Stop = xstop,
                Aantalkeer = Aantalkeer,
                Herhaaldelijk = Herhaaldelijk,
                PeriodeSoort = PeriodeSoort,
                Tot = Tot,
                Vanaf = Vanaf
            };
        }
    }

    public enum Periode
    {
        Dag,
        Week,
        Maand,
        Jaar,
        Altijd
    }
}