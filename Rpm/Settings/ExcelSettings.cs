using System;
using System.Collections.Generic;
using System.Linq;
using ProductieManager.Rpm.ExcelHelper;
using Rpm.Productie;

namespace ProductieManager.Rpm.Settings
{
    public class ExcelSettings
    {
        public static string[] ProductieColumns =
        {
            "ArtikelNr", "ProductieNr", "Naam", "Omschrijving", "Aantal", "AantalGemaakt", "LeverDatum", "TijdGewerkt",
            "ActueelPerUur", "PerUur", "VerwachtLeverDatum", "VerpakkingsInstructies"
        };

        public ExcelSettings()
        {
            Created = DateTime.Now;
            ID = Created.GetHashCode();
            Columns = new List<ExcelColumnEntry>();
            Columns.Add(new ExcelColumnEntry("ArtikelNr") {AutoSize = IsExcelSettings});
            ListNames = new List<string>();
        }

        public ExcelSettings(string listname, bool isExcel) : this(listname, listname, isExcel)
        {
        }

        public ExcelSettings(string name, string listname, bool isExcel) : this()
        {
            var xname = $"{Manager.Opties?.Username}_{listname}";
            Name = name;
            IsExcelSettings = isExcel;
            ListNames = new List<string> {xname};
        }

        public bool IsExcelSettings { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public List<string> ListNames { get; set; }
        public DateTime Created { get; set; }
        public List<ExcelColumnEntry> Columns { get; set; }
        public bool ShowGroups { get; set; }
        public string GroupBy { get; set; }
        public bool UseAsDefault { get; set; }

        public bool SetSelected(bool selected, string listname)
        {
            var xname = $"{Manager.Opties?.Username}_{listname}";
            var index = ListNames.IndexOf(xname);
            if (index > -1 && !selected)
                ListNames.RemoveAt(index);
            else if (index < 0 && selected)
                ListNames.Add(xname);
            return selected;
        }

        public bool IsUsed(string lsitname)
        {
            if (ListNames == null) return false;
            var xname = $"{Manager.Opties?.Username}_{lsitname}";
            return ListNames.Any(x => string.Equals(x, xname, StringComparison.CurrentCultureIgnoreCase));
        }

        public void ReIndexColumns(bool reorder = false)
        {
            if (Columns == null || Columns.Count == 0) return;
            if (reorder)
                Columns = Columns.OrderBy(x => x.ColumnIndex).ToList();
            var ind = 0;
            for (var i = 0; i < Columns.Count; i++)
                if (!Columns[i].IsVerborgen)
                    Columns[i].ColumnIndex = ind++;
        }

        public static ExcelSettings CreateSettings(string listname, bool isExcel, List<ExcelSettings> settings = null)
        {
            if (Manager.ListLayouts == null || Manager.ListLayouts.Disposed)
                return null;
            var xlists = settings ?? Manager.ListLayouts.GetAlleLayouts();
            var xreturn = new ExcelSettings(listname, isExcel);
            var count = xlists?.Count(x =>
                string.Equals(x.Name, listname, StringComparison.CurrentCultureIgnoreCase)) ?? 0;
            if (count > 0)
                xreturn.Name = $@"{listname}[{count}]";
            else
                xreturn.Name = listname;
            xreturn.Columns = ProductieColumns.Select(x => new ExcelColumnEntry(x)).ToList();
            if (isExcel)
                xreturn.Columns.ForEach(x => x.AutoSize = true);
            xreturn.ReIndexColumns();
            return xreturn;
        }

        public void SetDefaultColumns()
        {
            Columns = ProductieColumns.Select(x => new ExcelColumnEntry(x)).ToList();
        }
    }
}