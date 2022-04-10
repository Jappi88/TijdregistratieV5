using System;

namespace Rpm.ExcelHelper
{
    public class CellMargeCheck
    {
        public string ColumnName { get; set; }
        public string BasevalueName { get; set; }
        public double Marge { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is CellMargeCheck marge)
                return string.Equals(marge.ColumnName, ColumnName, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return ColumnName?.GetHashCode() ?? 0;
        }

        public bool Equals(string obj)
        {
            return string.Equals(ColumnName, obj, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
