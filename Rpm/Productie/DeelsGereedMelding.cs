using System;
using Polenter.Serialization;
using Rpm.Various;

namespace Rpm.Productie
{
    public class DeelsGereedMelding
    {
        private Bewerking _bewerking;
        private string _note;

        public DeelsGereedMelding()
        {
            Id = DateTime.Now.Ticks;
        }

        public int Aantal { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;
        public string Paraaf { get; set; }

        [ExcludeFromSerialization]
        public string Notitie
        {
            get => Note?.Notitie ?? _note;
            set
            {
                _note = value;
                if (Note == null && !string.IsNullOrEmpty(value))
                    Note = new NotitieEntry(value, Werk) {Type = NotitieType.DeelsGereed};
            }
        }

        //[ExcludeFromSerialization]
        public NotitieEntry Note { get; set; }

        public Bewerking Werk
        {
            get => _bewerking;
            set
            {
                _bewerking = value;
                if (Note != null)
                    Note.Productie = value;
            }
        }

        public string WerkPlek { get; set; }
        public string Path => Werk?.Path + "\\" + WerkPlek;
        public long Id { get; }

        public override bool Equals(object obj)
        {
            if (obj is DeelsGereedMelding dg)
                return dg.Id == Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}