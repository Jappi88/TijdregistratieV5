using ProductieManager.Rpm.ExcelHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductieManager.Rpm.Settings
{
    public class ExcelSettings
    {
        public int ID { get; set; }
        public string  Name { get; set; }
        public DateTime Created { get; set; }
        public List<ExcelColumnEntry> Columns { get; set; }
        public bool IsSelected { get; set; }

        public ExcelSettings()
        {
            Created = DateTime.Now;
            Columns = new List<ExcelColumnEntry>();
        }

        public ExcelSettings(string name) : this()
        {
            Name = name;
        }

        public static ExcelSettings CreateSettings(int id)
        {
            var xreturn = new ExcelSettings();
            xreturn.Name = $@"Default[{id}]";
            xreturn.Columns = ProductieColumns.Select(x => new ExcelColumnEntry(x)).ToList();
            xreturn.ID = id;
            return xreturn;
        }

        public static string[] ProductieColumns =
        {
            "ArtikelNr", "ProductieNr", "Omschrijving", "Naam", "State",
            "TijdGestart", "TijdGestopt", "TijdGewerkt", "Aantal",
            "TotaalGemaakt", "ActueelPerUur", "PerUur","ProcentAfwijkingPerUur","GemiddeldActueelPerUur", "GemiddeldProcentAfwijkingPerUur",
            "WerkplekkenName", "PersoneelNamen"
        };
    }
}
