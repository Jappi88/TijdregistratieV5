using ProductieManager.Rpm.ExcelHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using Polenter.Serialization;
using Rpm.Productie;

namespace ProductieManager.Rpm.Settings
{
    public class ExcelSettings
    {
        public bool IsExcelSettings { get; set; }
        public int ID { get; set; }
        public string  Name { get; set; }
        public List<string> ListNames { get; set; }
        public DateTime Created { get; set; }
        public List<ExcelColumnEntry> Columns { get; set; }
        [ExcludeFromSerialization]
        public bool IsSelected { get; set; }
        public bool ShowGroups { get; set; }
        public string GroupBy { get; set; }
        public bool SetSelected(bool selected, string listname)
        {
            var index = ListNames.IndexOf(listname);
            if (index > -1 && !selected)
                ListNames.Remove(listname);
            else if (index < 0 && selected)
                ListNames.Add(listname);
            return selected;
        }

        public bool IsUsed(string lsitname)
        {
            if (ListNames == null) return false;
            return ListNames.Any(x => string.Equals(x, lsitname, StringComparison.CurrentCultureIgnoreCase));
        }

        public ExcelSettings()
        {
            Created = DateTime.Now;
            Columns = new List<ExcelColumnEntry>();
            Columns.Add(new ExcelColumnEntry("ArtikelNr") {AutoSize = IsExcelSettings});
            ListNames = new List<string>();
        }

        public ExcelSettings(string listname, bool isExcel) : this(listname, listname, isExcel)
        {
        }

        public ExcelSettings(string name, string listname, bool isExcel) : this()
        {
            Name = name;
            IsExcelSettings = isExcel;
            ListNames = new List<string> {listname};
        }

        public void ReIndexColumns(bool reorder = false)
        {
            if(Columns == null || Columns.Count == 0)return;
            if (reorder)
                Columns = Columns.OrderBy(x => x.ColumnIndex).ToList();
            int ind = 0;
            for (int i = 0; i < Columns.Count; i++)
            {
                if (!Columns[i].IsVerborgen)
                    Columns[i].ColumnIndex = ind++;
            }
        }

        public static ExcelSettings CreateSettings(string listname, bool isExcel)
        {
            var xreturn = new ExcelSettings(listname,isExcel);
            int count = Manager.Opties?.ExcelColumns == null
                ? 0
                : Manager.Opties.ExcelColumns.Count(x =>
                    string.Equals(x.Name, listname, StringComparison.CurrentCultureIgnoreCase));
            if (count > 0)
                xreturn.Name = $@"{listname}[{count}]";
            else
                xreturn.Name = listname;
            xreturn.Columns = ProductieColumns.Select(x => new ExcelColumnEntry(x)).ToList();
            if(isExcel)
                xreturn.Columns.ForEach(x=> x.AutoSize = true);
            xreturn.ID = count;
            xreturn.ReIndexColumns();
            return xreturn;
        }

        public void SetDefaultColumns()
        {
            Columns = ProductieColumns.Select(x => new ExcelColumnEntry(x)).ToList();
        }

        public static string[] ProductieColumns =
        {
            "ArtikelNr", "ProductieNr","Naam", "Omschrijving", "Aantal", "AantalGemaakt","LeverDatum","TijdGewerkt",
             "ActueelPerUur", "PerUur","VerwachtLeverDatum","VerpakkingsInstructies"
        };

    }
}
