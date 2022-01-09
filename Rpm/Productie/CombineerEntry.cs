using System;
using Polenter.Serialization;
using Rpm.Misc;
using Rpm.Various;

namespace Rpm.Productie
{
    public class CombineerEntry
    {
        public string ProductieNr { get; set; }
        public string BewerkingNaam { get; set; }
        public double Activiteit { get; set; } = 100;
        public string Path => System.IO.Path.Combine(ProductieNr, BewerkingNaam);
        public TijdEntry Periode { get; set; } = new TijdEntry();

        public bool IsRunning
        {
            get => Periode?.Stop.IsDefault() ?? true;
            set
            {
                Periode ??= new TijdEntry();
                if (value && Periode.Stop.IsDefault()) return;
                if (!value && !Periode.Stop.IsDefault()) return;
                Periode.Stop = value ? default : DateTime.Now;
            }
        }

        public bool IsValid()
        {
            try
            {
                var bw = GetProductie();
                if (bw == null) return false;
                return bw.State != ProductieState.Gereed && bw.State != ProductieState.Verwijderd;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Bewerking GetProductie()
        {
            try
            {
                return Werk.FromPath(Path)?.Bewerking;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public double GetTijdGewerkt(Bewerking werk)
        {
            var xstop = IsRunning ? DateTime.Now : Periode.Stop;
            return werk.TijdAanGewerkt(Periode.Start, xstop);
        }

        public override bool Equals(object obj)
        {
            if (obj is CombineerEntry ent)
                return string.Equals(ent.Path, Path, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }
}
