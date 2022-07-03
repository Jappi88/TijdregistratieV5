using System;
using System.IO;
using System.Linq;
using Rpm.Misc;

namespace ProductieManager.Rpm.Various
{
    public class ProductieChatEntry
    {
        public string ID { get; private set; }
        public UserChat Afzender { get; set; }
        public string Ontvanger { get; set; }

        public string[] Ontvangers =>
            string.IsNullOrEmpty(Ontvanger)
                ? new string[]{}
                : Ontvanger.Split(';').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).ToArray();

        public string Bericht { get; set; }
        public DateTime Tijd { get; set; }
        public bool IsGelezen { get; set; }
        public bool IsPrivate => Ontvangers.Length <= 1;

        public ProductieChatEntry()
        {
            ID = Functions.GenerateRandomID().ToString();
            Tijd = DateTime.Now;
        }

    

        public override bool Equals(object obj)
        {
            if (obj is ProductieChatEntry entry)
                return string.Equals(ID, entry.ID, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return ID?.GetHashCode() ?? 0;
        }
    }
}
