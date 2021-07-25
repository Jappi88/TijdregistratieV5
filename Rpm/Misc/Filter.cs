using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpm.Various;

namespace Rpm.Misc
{
    public class Filter
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        //public Operand OperandType { get; set; }
        public List<FilterEntry> Filters { get; set; } = new List<FilterEntry>();

        public bool IsAllowed(object value)
        {
            try
            {
                if (!Enabled) return true;
                if (value == null) return false;
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
                string xreturn = "";
                bool xdone = false;
                foreach (var filter in Filters)
                {
                    if (!xdone)
                    {
                        xreturn = $"('{filter.ToString()}')";
                        xdone = true;
                        continue;
                    }
                    xreturn += $"{Enum.GetName(typeof(Operand), filter.OperandType)} ('{filter.ToString()}')";
                }

                return xreturn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return string.Empty;
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
    }
}
