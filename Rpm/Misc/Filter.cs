using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.OpenXmlFormats.Dml;
using Polenter.Serialization;
using Rpm.Various;

namespace Rpm.Misc
{
    public class Filter
    {
        public string Name { get; set; }
        public bool IsTempFilter { get; set; }
        /// <summary>
        /// De lijst waarop gefiltered moet worden.
        /// </summary>
        public List<string> ListNames { get; set; } = new();
        //public Operand OperandType { get; set; }
        public List<FilterEntry> Filters { get; set; } = new List<FilterEntry>();

        public int ID { get; set; }
        public DateTime DateCreated { get; set; }
        public string CriteriaText => ToString();
        public Filter()
        {
            ID = Functions.GenerateRandomID();
            DateCreated = DateTime.Now;
        }

        public bool IsAllowed(object value, string listname, bool tempfilter)
        {
            try
            {
                if (!string.IsNullOrEmpty(listname) && !ListNames.Any(x => string.Equals(x, listname))) return true;
                if (value == null) return false;
                if (!tempfilter && this.IsTempFilter) return true;
                if (Filters == null || Filters.Count == 0) return true;
                bool xdone = false;
                bool xreturn = false;
                foreach (var f in Filters)
                {
                    if (!xdone)
                    {
                        xreturn = f.ContainsFilter(value);
                        xdone = true;
                    }
                    else
                    {
                        switch (f.OperandType)
                        {
                            case Operand.ALS:
                                xreturn = f.ContainsFilter(value);
                                break;
                            case Operand.OF:
                                xreturn |= f.ContainsFilter(value);
                                break;
                            case Operand.EN:
                                xreturn &= f.ContainsFilter(value);
                                break;
                        }
                    }

                }

                return xreturn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public new string ToString()
        {
            try
            {
                string xreturn = "(";
                //bool xdone = false;
                foreach (var filter in Filters)
                {
                    //if (!xdone)
                    //{
                    //    xreturn = $"'{filter.ToString()}'";
                    //    xdone = true;
                    //    continue;
                    //}
                    xreturn += $"{filter.ToString()} ";
                }

                return xreturn.Trim() + ")";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        public string ToHtmlString()
        {
            try
            {
                string xreturn = "";
                //bool xdone = false;
                foreach (var filter in Filters)
                {
                    //if (xdone)
                    //{
                    //    xreturn += $"<li Color = RoyalBlue><b>{Enum.GetName(typeof(Operand), filter.OperandType)}</b></li>";
                    //}
                    //else xdone = true;
                    xreturn += $"{filter.ToHtmlString()}";
                }

                return xreturn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Filter f)
                return f.ID == ID;

            if (obj is string xstring)
                return string.Equals(xstring, Name, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
