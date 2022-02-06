using Polenter.Serialization;
using System;
using System.Collections.Generic;

namespace Rpm.SqlLite
{
    public class BewerkingEntry
    {
        public BewerkingEntry()
        {
            ID = Guid.NewGuid().GetHashCode();
        }

        public BewerkingEntry(string naam) : this()
        {
            Naam = naam;
        }

        public BewerkingEntry(string naam, bool isbemand) : this(naam)
        {
            IsBemand = isbemand;
        }

        public BewerkingEntry(string naam, bool isbemand, List<string> werkplekken) : this(naam, isbemand)
        {
            WerkPlekken = werkplekken;
        }

        public int ID { get; private set; }
        public bool IsBemand { get; set; }
        [ExcludeFromSerialization]
        public bool IsExtern { get; set; }
        public string Naam { get; set; }

        public string NewName { get; set; }
        [ExcludeFromSerialization]
        public double DoorloopTijd { get; set; }
        [ExcludeFromSerialization]
        public DateTime Leverdatum { get; set; }
        [ExcludeFromSerialization]
        public string Opmerking { get; set; }

        public bool HasChanged => !string.IsNullOrEmpty(NewName) &&
                                  !string.Equals(Naam, NewName, StringComparison.CurrentCultureIgnoreCase);

        public List<string> WerkPlekken { get; set; } = new();

        public override bool Equals(object obj)
        {
            return obj is BewerkingEntry ent && ent.ID == ID;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}