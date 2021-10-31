using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;
using BrightIdeasSoftware;
using NPOI.HSSF.Util;
using Rpm.Misc;
using Rpm.Productie;

namespace Rpm.Various
{
    class Comparer : IComparer
    {
        SortOrder _Order;
        private OLVColumn _OlvColmn;
        public Comparer(SortOrder order, OLVColumn colmn)
        {
            _Order = order;
            _OlvColmn = colmn;
        }

        public static int Compare(object x, object y, SortOrder order, OLVColumn colmn)
        {
            return new Comparer(order, colmn).Compare(x, y);
        }

        public int Compare(object x, object y)
        {
            //string xtype = x.GetType().ToString();
            try
            {
                if (_OlvColmn != null)
                {
                    var selected = _OlvColmn.AspectName;
                    if (x is OLVListItem xolv && y is OLVListItem xolv1)
                    {
                        //var index = ;
                        //x = xolv.Text;
                        // y = xolv1.Text;
                        if (selected == null) return 0;
                        if (xolv.RowObject is IProductieBase bew1 && xolv1.RowObject is IProductieBase bew2)
                        {
                            x = bew1.GetPropValue(selected);
                            y = bew2.GetPropValue(selected);
                        }
                        else if (xolv.RowObject is WerkPlek wp1 && xolv1.RowObject is WerkPlek wp2)
                        {
                            x = wp1.GetPropValue(selected);
                            y = wp2.GetPropValue(selected);
                        }
                    }
                    else if (x is OLVGroup group1 && y is OLVGroup group2)
                    {
                        group1.Items = group1.Items.OrderBy(g => g.RowObject?.GetPropValue(_OlvColmn.AspectName))
                            .ToList();
                        group2.Items = group2.Items.OrderBy(g => g.RowObject?.GetPropValue(_OlvColmn.AspectName))
                            .ToList();
                        if (_Order == SortOrder.Descending)
                        {
                            group1.Items = group1.Items.Reverse().ToList();
                            group2.Items = group2.Items.Reverse().ToList();
                        }

                        x = group1.Key;
                        y = group2.Key;
                    }
                }

                if (x == null)
                {
                    if (y == null)
                    {
                        // If x is null and y is null, they're
                        // equal.
   
                        return 0;
                    }

                    switch (_Order)
                    {
                        case SortOrder.Ascending:
                            return  1;
                        case SortOrder.Descending:
                            return -1;
                        case SortOrder.None:
                            return 0;
                    }
                    // If x is null and y is not null, y
                    // is greater.
                    return -1;
                }

                // If x is not null...
                //
                if (y == null)
                    // ...and y is null, x is greater.
                {
                    switch (_Order)
                    {
                        case SortOrder.Ascending:
                            return -1;
                        case SortOrder.Descending:
                            return 1;
                        case SortOrder.None:
                            return 0;
                    }
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the
                    // lengths of the two strings.
                    //
                    if (x is DateTime time1 && y is DateTime time2)
                    {
                        switch (_Order)
                        {
                            case SortOrder.Ascending:
                                return time1 > time2 ? 1 : -1;
                            case SortOrder.Descending:
                                return time1 < time2 ? 1 : -1;
                            case SortOrder.None:
                                return 0;
                        }
                    }

                    if (x is int xint1 && y is int xint2)
                    {
                        switch (_Order)
                        {
                            case SortOrder.Ascending:
                                return xint1 > xint2 ? 1 : -1;
                            case SortOrder.Descending:
                                return xint1 < xint2 ? 1 : -1;
                            case SortOrder.None:
                                return 0;
                        }
                    }

                    if (x is decimal xdec1 && y is decimal xdec2)
                    {
                        switch (_Order)
                        {
                            case SortOrder.Ascending:
                                return xdec1 > xdec2 ? 1 : -1;
                            case SortOrder.Descending:
                                return xdec1 < xdec2 ? 1 : -1;
                            case SortOrder.None:
                                return 0;
                        }
                    }

                    if (x is double xdoub1 && y is double xdoub2)
                    {
                        switch (_Order)
                        {
                            case SortOrder.Ascending:
                                return xdoub1 > xdoub2 ? 1 : -1;
                            case SortOrder.Descending:
                                return xdoub1 < xdoub2 ? 1 : -1;
                            case SortOrder.None:
                                return 0;
                        }
                    }

                    if (x is uint xuint1 && y is uint xuint2)
                    {
                        switch (_Order)
                        {
                            case SortOrder.Ascending:
                                return xuint1 > xuint2 ? 1 : -1;
                            case SortOrder.Descending:
                                return xuint1 < xuint2 ? 1 : -1;
                            case SortOrder.None:
                                return 0;
                        }
                    }

                    if (x is long xlong1 && y is long xlong2)
                    {
                        switch (_Order)
                        {
                            case SortOrder.Ascending:
                                return xlong1 > xlong2 ? 1 : -1;
                            case SortOrder.Descending:
                                return xlong1 < xlong2 ? 1 : -1;
                            case SortOrder.None:
                                return 0;
                        }
                    }

                    if (x is ulong xulong1 && y is ulong xulong2)
                    {
                        switch (_Order)
                        {
                            case SortOrder.Ascending:
                                return xulong1 > xulong2 ? 1 : -1;
                            case SortOrder.Descending:
                                return xulong1 < xulong2 ? 1 : -1;
                            case SortOrder.None:
                                return 0;
                        }
                    }

                    //if (x is string str1 && y is string str2)
                    //{
                    //    if (DateTime.TryParse(str1, out var time3) && DateTime.TryParse(str2, out var time4))
                    //        return Compare(time3, time4);

                    //}



                    int retval = string.Compare(x.ToString(), y.ToString(),
                        StringComparison.CurrentCultureIgnoreCase);
                    if (_Order == SortOrder.Ascending)
                        return retval;
                    retval = retval == 1 ? -1 : retval == -1 ? 1 : 0;
                    // if (retval == 1 && _Order == SortOrder.Descending) return 0;
                    return retval;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return 0;
        }

    }
}
