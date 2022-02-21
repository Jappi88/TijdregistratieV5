﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Rpm.Various;

namespace Rpm.Misc
{
    [Serializable]
    public class FilterTypeInstance
    {
        public Operand OperandType { get; set; }
        public FilterType FilterType { get; set; }
    }

    public enum RangeDeviderType
    {
        None,
        Plus,
        Min,
        DelenDoor,
        Keer,
        Procent,
        MOD
    }

    [Serializable]
    public class FilterEntry
    {
        private Operand _fOperand;

        public FilterEntry()
        {
            ID = DateTime.Now.GetHashCode();
        }

        public int ID { get; set; }
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public bool CompareWithProperty { get; set; }
        public bool DezeWeekDateTime { get; set; }
        public FilterType FilterType { get; set; }
        public Operand OldOperandType { get; set; } = Operand.None;
        public List<FilterEntry> ChildEntries { get; set; }
        public int RangeValue { get; set; }
        public RangeDeviderType DeviderType { get; set; }

        public Operand OperandType
        {
            get => _fOperand;
            set
            {
                _fOperand = value;
                if (OldOperandType == Operand.None)
                    OldOperandType = value;
            }
        }

        public string Criteria => ToString();

        public bool ContainsFilter(object instance)
        {
            try
            {
                if (instance == null || Value == null) return false;
                object xvalue = null;
                if (CompareWithProperty && Value is string xpropname && !string.IsNullOrEmpty(xpropname))
                    xvalue = instance.GetPropValue(xpropname);
                var propertyvalue =
                    string.IsNullOrEmpty(PropertyName) ? instance : instance.GetPropValue(PropertyName);
                if (propertyvalue == null) return false;
                var xreturn = ContainsFilter(xvalue ?? Value, propertyvalue, FilterType);
                if (ChildEntries is {Count: > 0})
                    foreach (var filter in ChildEntries)
                        switch (filter.OperandType)
                        {
                            case Operand.None:
                                continue;
                            case Operand.ALS:
                                if (filter.ContainsFilter(instance)) return true;
                                continue;
                            case Operand.OF:
                                xreturn |= filter.ContainsFilter(instance);
                                continue;
                            case Operand.EN:
                                xreturn &= filter.ContainsFilter(instance);
                                continue;
                        }

                return xreturn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static List<string> GetFilterStringTypesByType(Type type)
        {
            var xvaltypes = Enum.GetNames(typeof(FilterType)).ToList();
            xvaltypes.RemoveAll(x => x.ToLower() == "none");
            if (type == typeof(string))
                xvaltypes.RemoveAll(x => x.ToLower().StartsWith("kleiner") || x.ToLower().StartsWith("groter"));
            else if (type == typeof(bool))
                xvaltypes.RemoveAll(x =>
                    x.ToLower().StartsWith("kleiner") || x.ToLower().StartsWith("groter") ||
                    x.ToLower().StartsWith("bevat") || x.ToLower().Contains("met"));
            else xvaltypes.RemoveAll(x => x.ToLower().StartsWith("bevat") || x.ToLower().Contains("met"));

            return xvaltypes;
        }

        public static List<FilterType> GetFilterTypesByType(Type type)
        {
            try
            {
                var xvaltypes = GetFilterStringTypesByType(type);

                return xvaltypes.Select(x => (FilterType) Enum.Parse(typeof(FilterType), x)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<FilterType>();
            }
        }

        public static FilterEntry CreateNewFromValue(string propertyname, object value, FilterType filtertype)
        {
            var fe = new FilterEntry();
            fe.PropertyName = propertyname;
            fe.FilterType = filtertype;
            fe.OperandType = Operand.ALS;
            fe.Value = value;
            return fe;
        }

        public double GetRangeValue(double value)
        {
            switch (DeviderType)
            {
                case RangeDeviderType.None:
                    return value;
                case RangeDeviderType.Plus:
                    return value + RangeValue;
                case RangeDeviderType.Min:
                    return value - RangeValue;
                case RangeDeviderType.DelenDoor:
                    if (RangeValue > 0 && value > 0)
                        return value / RangeValue;
                    return 0;
                case RangeDeviderType.Keer:
                    return value * RangeValue;
                case RangeDeviderType.Procent:
                    return value * ((double) RangeValue / 100);
                case RangeDeviderType.MOD:
                    return value % RangeValue;
            }

            return value;
        }

        public decimal GetRangeValue(decimal value)
        {
            switch (DeviderType)
            {
                case RangeDeviderType.None:
                    return value;
                case RangeDeviderType.Plus:
                    return value + RangeValue;
                case RangeDeviderType.Min:
                    return value - RangeValue;
                case RangeDeviderType.DelenDoor:
                    if (RangeValue > 0 && value > 0)
                        return value / RangeValue;
                    return 0;
                case RangeDeviderType.Keer:
                    return value * RangeValue;
                case RangeDeviderType.Procent:
                    return value * ((decimal) RangeValue / 100);
                case RangeDeviderType.MOD:
                    return value % RangeValue;
            }

            return value;
        }

        public int GetRangeValue(int value)
        {
            switch (DeviderType)
            {
                case RangeDeviderType.None:
                    return value;
                case RangeDeviderType.Plus:
                    return value + RangeValue;
                case RangeDeviderType.Min:
                    return value - RangeValue;
                case RangeDeviderType.DelenDoor:
                    if (RangeValue > 0 && value > 0)
                        return value / RangeValue;
                    return 0;
                case RangeDeviderType.Keer:
                    return value * RangeValue;
                case RangeDeviderType.Procent:
                    return (int) (value * ((double) RangeValue / 100));
                case RangeDeviderType.MOD:
                    return value % RangeValue;
            }

            return value;
        }

        private bool ContainsFilter(object valueA, object valueB, FilterType type)
        {
            try
            {
                if (valueA == null || valueB == null) return false;
                var xisfilter = false;
                switch (valueB)
                {
                    case string xvalue:
                        if (valueA is not string value0) return false;
                        switch (type)
                        {
                            case FilterType.GelijkAan:
                                xisfilter = string.Equals(xvalue, value0,
                                    StringComparison.CurrentCultureIgnoreCase);
                                break;
                            case FilterType.BegintMet:
                                xisfilter = xvalue.ToLower().StartsWith(value0.ToLower());
                                break;
                            case FilterType.EindigtMet:
                                xisfilter = xvalue.ToLower().EndsWith(value0.ToLower());
                                break;
                            case FilterType.Bevat:
                                xisfilter = xvalue.ToLower().Contains(value0.ToLower());
                                break;
                            case FilterType.Kleinerdan:
                                if (int.TryParse(value0, out var valueint) && int.TryParse(xvalue, out var xvalueint))
                                    xisfilter = xvalueint < valueint;
                                break;
                            case FilterType.KleinerOfGelijkAan:
                                if (int.TryParse(value0, out var valueint2) && int.TryParse(xvalue, out var xvalueint2))
                                    xisfilter = xvalueint2 <= valueint2;
                                break;
                            case FilterType.Groterdan:
                                if (int.TryParse(value0, out var valueint1) && int.TryParse(xvalue, out var xvalueint1))
                                    xisfilter = xvalueint1 > valueint1;
                                break;
                            case FilterType.GroterOfGelijkAan:
                                if (int.TryParse(value0, out var valueint3) && int.TryParse(xvalue, out var xvalueint3))
                                    xisfilter = xvalueint3 >= valueint3;
                                break;
                            case FilterType.NietGelijkAan:
                                xisfilter = !string.Equals(xvalue, value0,
                                    StringComparison.CurrentCultureIgnoreCase);
                                break;
                            case FilterType.BevatNiet:
                                xisfilter = !xvalue.ToLower().Contains(value0.ToLower());
                                break;
                        }

                        break;
                    case int xvalue:
                        if (!int.TryParse(valueA.ToString(), out var value1)) return false;
                        switch (type)
                        {
                            case FilterType.GelijkAan:
                                xisfilter = GetRangeValue(value1) == xvalue;
                                break;
                            case FilterType.Bevat:
                                xisfilter = xvalue.ToString().Contains(GetRangeValue(value1).ToString());
                                break;
                            case FilterType.BegintMet:
                                xisfilter = xvalue.ToString().ToLower().StartsWith(GetRangeValue(value1).ToString());
                                break;
                            case FilterType.EindigtMet:
                                xisfilter = xvalue.ToString().ToLower().EndsWith(GetRangeValue(value1).ToString());
                                break;
                            case FilterType.Kleinerdan:
                                xisfilter = xvalue < GetRangeValue(value1);
                                break;
                            case FilterType.KleinerOfGelijkAan:
                                xisfilter = xvalue <= GetRangeValue(value1);
                                break;
                            case FilterType.Groterdan:
                                xisfilter = xvalue > GetRangeValue(value1);
                                break;
                            case FilterType.GroterOfGelijkAan:
                                xisfilter = xvalue >= GetRangeValue(value1);
                                break;
                            case FilterType.NietGelijkAan:
                                xisfilter = GetRangeValue(value1) != xvalue;
                                break;
                            case FilterType.BevatNiet:
                                xisfilter = !xvalue.ToString().Contains(GetRangeValue(value1).ToString());
                                break;
                        }

                        break;
                    case double xvalue:
                        if (!double.TryParse(valueA.ToString(), out var xdouble)) return false;
                        switch (type)
                        {
                            case FilterType.GelijkAan:
                                xisfilter = xvalue == GetRangeValue(xdouble);
                                break;
                            case FilterType.Bevat:
                                xisfilter = xvalue.ToString().Contains(GetRangeValue(xdouble).ToString());
                                break;
                            case FilterType.BegintMet:
                                xisfilter = xvalue.ToString().ToLower().StartsWith(GetRangeValue(xdouble).ToString());
                                break;
                            case FilterType.EindigtMet:
                                xisfilter = xvalue.ToString().ToLower().EndsWith(GetRangeValue(xdouble).ToString());
                                break;
                            case FilterType.Kleinerdan:
                                xisfilter = xvalue < GetRangeValue(xdouble);
                                break;
                            case FilterType.KleinerOfGelijkAan:
                                xisfilter = xvalue <= GetRangeValue(xdouble);
                                break;
                            case FilterType.Groterdan:
                                xisfilter = xvalue > GetRangeValue(xdouble);
                                break;
                            case FilterType.GroterOfGelijkAan:
                                xisfilter = xvalue >= GetRangeValue(xdouble);
                                break;
                            case FilterType.NietGelijkAan:
                                xisfilter = GetRangeValue(xdouble) != xvalue;
                                break;
                            case FilterType.BevatNiet:
                                xisfilter = !xvalue.ToString().Contains(GetRangeValue(xdouble).ToString());
                                break;
                        }

                        break;
                    case decimal xvalue:
                        if (!decimal.TryParse(valueA.ToString(), out var xdecimal)) return false;
                        switch (type)
                        {
                            case FilterType.GelijkAan:
                                xisfilter = GetRangeValue(xdecimal) == xvalue;
                                break;
                            case FilterType.Bevat:
                                xisfilter = xvalue.ToString().Contains(GetRangeValue(xdecimal).ToString());
                                break;
                            case FilterType.BegintMet:
                                xisfilter = xvalue.ToString().ToLower().StartsWith(GetRangeValue(xdecimal).ToString());
                                break;
                            case FilterType.EindigtMet:
                                xisfilter = xvalue.ToString().ToLower().EndsWith(GetRangeValue(xdecimal).ToString());
                                break;
                            case FilterType.Kleinerdan:
                                xisfilter = xvalue < GetRangeValue(xdecimal);
                                break;
                            case FilterType.KleinerOfGelijkAan:
                                xisfilter = xvalue <= GetRangeValue(xdecimal);
                                break;
                            case FilterType.Groterdan:
                                xisfilter = xvalue > GetRangeValue(xdecimal);
                                break;
                            case FilterType.GroterOfGelijkAan:
                                xisfilter = xvalue >= GetRangeValue(xdecimal);
                                break;
                            case FilterType.NietGelijkAan:
                                xisfilter = GetRangeValue(xdecimal) != xvalue;
                                break;
                            case FilterType.BevatNiet:
                                xisfilter = !xvalue.ToString().Contains(GetRangeValue(xdecimal).ToString());
                                break;
                        }

                        break;
                    case DateTime xvalue:
                        if (valueA is not DateTime value2)
                            if (!DateTime.TryParse(valueA.ToString(), out value2))
                                return false;

                        if (xvalue.Year == 9999 && xvalue.Month == 1 && xvalue.Day == 1)
                            xvalue = DateTime.Now.ChangeTime(xvalue.TimeOfDay);

                        if (xvalue.TimeOfDay == new TimeSpan())
                            xvalue = xvalue.ChangeTime(DateTime.Now.TimeOfDay);

                        if (value2.Year == 9999 && value2.Month == 1 && value2.Day == 1)
                            value2 = DateTime.Now.ChangeTime(value2.TimeOfDay);

                        if (value2.TimeOfDay == new TimeSpan())
                            value2 = value2.ChangeTime(DateTime.Now.TimeOfDay);
                        var weeknr = -1;
                        var xweeknr = -1;
                        if (DezeWeekDateTime)
                        {
                            var xcur = xvalue.Year - DateTime.Now.Year;
                            weeknr = DateTime.Now.GetWeekNr();
                            xweeknr = xvalue.GetWeekNr();
                            if (xcur > 0)
                                xweeknr += 52 * xcur;
                            else if (xcur < 0)
                                weeknr += 52 * -xcur;
                        }

                        switch (type)
                        {
                            case FilterType.GelijkAan:
                                if (weeknr > -1)
                                    xisfilter = xweeknr == weeknr;
                                else
                                    xisfilter = value2.Date == xvalue.Date && value2.TimeOfDay == xvalue.TimeOfDay;
                                break;
                            case FilterType.Bevat:
                                if (weeknr > -1)
                                    xisfilter = xweeknr == weeknr;
                                else
                                    xisfilter = xvalue.ToString().Contains(value2.ToString());
                                break;
                            case FilterType.BegintMet:
                                if (weeknr > -1)
                                    xisfilter = xweeknr == weeknr;
                                else
                                    xisfilter = xvalue.ToString().ToLower().StartsWith(value2.ToString());
                                break;
                            case FilterType.EindigtMet:
                                if (weeknr > -1)
                                    xisfilter = xweeknr == weeknr;
                                else
                                    xisfilter = xvalue.ToString().ToLower().EndsWith(value2.ToString());
                                break;
                            case FilterType.Kleinerdan:
                                if (weeknr > -1)
                                    xisfilter = xweeknr < weeknr;
                                else
                                    xisfilter = xvalue < value2;
                                break;
                            case FilterType.KleinerOfGelijkAan:
                                if (weeknr > -1)
                                    xisfilter = xweeknr <= weeknr;
                                else
                                    xisfilter = xvalue <= value2;
                                break;
                            case FilterType.Groterdan:
                                if (weeknr > -1)
                                    xisfilter = xweeknr > weeknr;
                                else
                                    xisfilter = xvalue > value2;
                                break;
                            case FilterType.GroterOfGelijkAan:
                                if (weeknr > -1)
                                    xisfilter = xweeknr >= weeknr;
                                else
                                    xisfilter = xvalue >= value2;
                                break;
                            case FilterType.NietGelijkAan:
                                if (weeknr > -1)
                                    xisfilter = xweeknr != weeknr;
                                else
                                    xisfilter = value2 != xvalue;
                                break;
                            case FilterType.BevatNiet:
                                if (weeknr > -1)
                                    xisfilter = xweeknr != weeknr;
                                else
                                    xisfilter = !xvalue.ToString().Contains(value2.ToString());
                                break;
                        }

                        break;
                    case Enum xvalue:
                        if (valueA is not Enum value3) return false;
                        var xvalueenum = Enum.GetName(xvalue.GetType(), xvalue);
                        var valueenum = Enum.GetName(value3.GetType(), value3);
                        xisfilter = ContainsFilter(valueenum, xvalueenum, type);
                        break;
                    case bool xvalue:
                        if (valueA is not bool value4) return false;
                        switch (type)
                        {
                            case FilterType.GelijkAan:
                                xisfilter = value4 == xvalue;
                                break;
                            case FilterType.Bevat:
                                xisfilter = xvalue.ToString().Contains(value4.ToString());
                                break;
                            case FilterType.KleinerOfGelijkAan:
                            case FilterType.GroterOfGelijkAan:
                            case FilterType.Kleinerdan:
                            case FilterType.Groterdan:
                                xisfilter = value4 != xvalue;
                                break;

                            case FilterType.NietGelijkAan:
                                xisfilter = value4 != xvalue;
                                break;
                            case FilterType.BevatNiet:
                                xisfilter = !xvalue.ToString().Contains(value4.ToString());
                                break;
                        }

                        break;
                }

                return xisfilter;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public string ValueToString()
        {
            try
            {
                if (Value == null) return null;
                switch (Value)
                {
                    case string xvalue:
                        return $"<b>{xvalue.ToString(CultureInfo.InvariantCulture)}</b>";
                    case decimal xvalue:
                        return $"<b>{xvalue.ToString(CultureInfo.InvariantCulture)}</b>";
                    case double xvalue:
                        return $"<b>{xvalue.ToString(CultureInfo.InvariantCulture)}</b>";
                    case int xvalue:
                        return $"<b>{xvalue.ToString(CultureInfo.InvariantCulture)}</b>";
                    case DateTime xvalue:
                        var xreturn = "";
                        if (DezeWeekDateTime)
                        {
                            var weeknr = DateTime.Now.GetWeekNr();
                            return $"'<b>Deze Week({weeknr})</b>'";
                        }

                        if (xvalue.Year == 9999 && xvalue.Month == 1 && xvalue.Day == 1)
                            xreturn = "'<b>Huidige Datum</b>' ";
                        else xreturn = $"'<b>{xvalue.Date:D}</b>' ";
                        if (xvalue.TimeOfDay > new TimeSpan())
                            xreturn += $"'<b>{xvalue.TimeOfDay.Hours}:{xvalue.TimeOfDay.Minutes}</b>'";
                        else
                            xreturn += "'<b>Huidige Tijd</b>'";
                        return xreturn.Trim();
                    case Enum xvalue:
                        var xvalueenum = Enum.GetName(xvalue.GetType(), xvalue);
                        return xvalueenum;
                    case bool xvalue:
                        return $"<b>{(xvalue ? "WAAR" : "NIETWAAR")}</b>";
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private string GetRangeValueString()
        {
            return DeviderType == RangeDeviderType.None
                ? ""
                : $" ({Enum.GetName(typeof(RangeDeviderType), DeviderType)} {RangeValue})";
        }

        private string StripHtmlTagsFromString(string html)
        {
            if (html == null) return null;
            int index;
            while ((index = html.IndexOf("<", StringComparison.Ordinal)) > -1)
            {
                var xend = html.IndexOf(">", StringComparison.Ordinal);
                if (xend > -1)
                    html = html.Remove(index, xend + 1 - index);
            }

            return html;
        }

        public new string ToString()
        {
            try
            {
                var value = StripHtmlTagsFromString(ValueToString());
                var xreturn =
                    $"{Enum.GetName(typeof(Operand), OperandType)} '{PropertyName}' {Enum.GetName(typeof(FilterType), FilterType)} '{value}' {GetRangeValueString()}";
                if (ChildEntries is {Count: > 0})
                    xreturn += $"\n({string.Join("\n", ChildEntries.Select(x => x.ToString()))})";
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
                var value = ValueToString();
                var xreturn =
                    $"<div>(<span Color=RoyalBlue>{Enum.GetName(typeof(Operand), OperandType)}</span> <span Color=Purple><b>{PropertyName}</b></span> <span Color=RoyalBlue>{Enum.GetName(typeof(FilterType), FilterType)}</span><span Color=Purple> {value}</span><span Color=Red> {GetRangeValueString()}</span>)</div>";
                if (ChildEntries is {Count: > 0})
                    xreturn += "\n" + string.Join("\n", ChildEntries.Select(x => x.ToHtmlString()));
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
            return obj is FilterEntry entry &&
                   string.Equals(entry.ToString(), ToString(), StringComparison.CurrentCultureIgnoreCase);
        }

        protected bool Equals(FilterEntry other)
        {
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}