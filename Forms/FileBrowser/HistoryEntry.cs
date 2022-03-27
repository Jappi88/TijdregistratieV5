using System;

namespace Forms.FileBrowser
{
    public class HistoryEntry
    {
        public string Vorige { get; set; }
        public string Volgende { get; set; }
        public string Huidige { get; set; }

        public HistoryEntry() { }

        public HistoryEntry(string huidige,string vorige, string volgende)
        {
            Vorige = vorige;
            Volgende = volgende;
            Huidige = huidige;
        }

        public HistoryEntry(string huidige)
        {
            Huidige = huidige;
        }

        public HistoryEntry(string huidige, string vorige)
        {
            Huidige = huidige;
            Vorige = vorige;
        }


        public override bool Equals(object obj)
        {
          if(obj is HistoryEntry ent)
                return string.Equals(ent.Huidige, Huidige, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return Huidige?.GetHashCode()??0;
        }
    }
}
