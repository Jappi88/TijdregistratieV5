using System;

namespace Forms.FileBrowser
{
    public class HistoryEntry
    {
        public string Path { get; set; }
        public string Criteria { get; set; }

        public HistoryEntry()
        {
        }

        public HistoryEntry(string path)
        {
            Path = path;
        }

        public override bool Equals(object obj)
        {
            if (obj is HistoryEntry ent)
                return string.Equals(ent.Path, Path, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return Path?.GetHashCode() ?? 0;
        }
    }
}
