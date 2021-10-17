using System.Collections.Generic;

namespace ProductieManager.Rpm.ExcelHelper
{
    public enum CalculationType
    {
        None,
        SOM,
        Gemiddeld
    }

    public enum ColorRuleType
    {
        None,
        Static,
        Dynamic
    }

    public class ExcelColumnEntry
    {
        public string Naam { get; set; }
        public string ColumnText { get; set; }
        public string ColumnFormat { get; set; }
        public int ColumnBreedte { get; set; } = 100;
        public bool IsVerborgen { get; set; }
        public bool AutoSize { get; set; } = true;
        public short ColumnColorIndex { get; set; } = -1;
        public short ColumnTextColorIndex { get; set; } = -1;
        public CalculationType Type { get; set; } = CalculationType.None;
        public List<ExcelRegelEntry> KleurRegels { get; set; } = new List<ExcelRegelEntry>();
        public ColorRuleType ColorType { get; set; }

        public ExcelColumnEntry()
        {

        }

        public ExcelColumnEntry(string name)
        {
            Naam = name;
            ColumnText = name;
        }
    }
}
