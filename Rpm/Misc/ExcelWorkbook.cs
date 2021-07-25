﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Rpm.Mailing;
using Rpm.Productie;
using Rpm.Various;
// ReSharper disable All

namespace Rpm.Misc
{
    public static class ExcelWorkbook
    {
        public static string[] ProductieColumns =
        {
            "ArtikelNr", "ProductieNr", "Omschrijving", "Bewerking Naam", "Status",
            "Start Datum", "Eind Datum", "Tijd Gewerkt", "Productie Aantal",
            "Aantal Gemaakt", "Actueel Per Uur", "Per Uur", "Aantal Personen",
            "Werkplekken", "Personen"
        };

        public static string[] HiddenProductieColumns =
        {
            "ProductieNr", "Omschrijving",
            "Start Datum", "Eind Datum", "Productie Aantal",
            "Productie Aantal", "Per Uur",
            "Personen"
        };

        public static string[] StoringColumns =
        {
            "WerkPlek", "Storing Type", "Omschrijving", "Gemeld Door", "Gestart Op",
            "Gestopt Op", "Totaal Tijd", "Is Verholpen", "Verholpen Door", "Oplossing",
            "Productie Omschrijving"
        };

        public static string[] HiddenStoringColumns =
        {
        };

        private static ICellStyle GetBorderStyle(XSSFWorkbook workbook)
        {
            var cellStyleBorder = workbook.CreateCellStyle();
            cellStyleBorder.WrapText = true;
            cellStyleBorder.BorderBottom = BorderStyle.Thin;
            cellStyleBorder.BorderLeft = BorderStyle.Thin;
            cellStyleBorder.BorderRight = BorderStyle.Thin;
            cellStyleBorder.BorderTop = BorderStyle.Thin;
            cellStyleBorder.Alignment = HorizontalAlignment.Left;
            cellStyleBorder.VerticalAlignment = VerticalAlignment.Center;
            return cellStyleBorder;
        }

        public static ISheet CreateOverzichtChartSheet(XSSFWorkbook workbook, List<Bewerking> bewerkingen,
            bool iswerkplek)
        {
            try
            {
                if (workbook == null) return null;

                var rowindex = 0;
                string[] fields = {"Tijd gewerkt", "Aantal gemaakt", "Per uur", "Storingen"};
                string[] fieldsformat = {"uur", "stuks", "p/uur", "uur"};
                var overzichten = Functions.GetWeekRanges(Manager.Opties.VanafWeek, Manager.Opties.VanafJaar, false,false);
                if (overzichten.Count > 0)
                {
                    var colsmade = 0;
                    var xwk = overzichten.Count == 1 ? "dagen" : "weken";
                    var xinfo = iswerkplek ? "Werkplek" : "Bewerking";
                    var sheet = workbook.CreateSheet($"{xinfo} overzicht van {overzichten.Count} {xwk}");
                    for (var fieldindex = 0; fieldindex < fields.Length; fieldindex++)
                    {
                        var chartdata = bewerkingen.CreateChartData(iswerkplek, Manager.Opties.VanafWeek,
                            Manager.Opties.VanafJaar, fields[fieldindex], false,false);
                        var columns = new List<string>();
                        foreach (var charrow in chartdata)
                        foreach (var charcol in charrow.Value)
                            if (columns.All(x =>
                                !string.Equals(charcol.Key, x, StringComparison.CurrentCultureIgnoreCase)))
                                columns.Add(charcol.Key);
                        var style = CreateStyle(workbook, true, HorizontalAlignment.Center, 18);
                        var omschrijving = $"{xinfo} {fields[fieldindex]} van de afgelopen {overzichten.Count} {xwk}.";
                        CreateHeader(sheet, omschrijving, rowindex, 2, 0, columns.Count + 1, style);
                        rowindex += 2;
                        var rowstart = rowindex;
                        //create column font
                        style = CreateStyle(workbook, true, HorizontalAlignment.Left, 12);
                        style.WrapText = false;
                        //init the columns

                        var row = sheet.CreateRow(rowindex);
                        var cellindex = 0;
                        CreateCell(row, cellindex++, "Periode", style);
                        foreach (var prod in columns)
                        {
                            CreateCell(row, cellindex++, prod, style);
                            if (cellindex > colsmade)
                                colsmade = cellindex;
                        }

                        rowindex++;
                        var rowkeys = chartdata.Select(x => x.Key).ToArray();
                        for (var xrowindex = 0; xrowindex < rowkeys.Length; xrowindex++)
                        {
                            //init values
                            style = CreateStyle(workbook, false, HorizontalAlignment.Left, 11);
                            style.WrapText = false;
                            row = sheet.CreateRow(rowindex);
                            row.HeightInPoints = 15;
                            cellindex = 0;
                            var xname = rowkeys[xrowindex];
                            CreateCell(row, cellindex++, xname, style);
                            string xcurcol = null;
                            while ((xcurcol = GetValue(sheet, rowstart, cellindex)) != null)
                            {
                                var xcharvalue = chartdata[xname].FirstOrDefault(t =>
                                    string.Equals(xcurcol, t.Key, StringComparison.CurrentCultureIgnoreCase));
                                var value = xcharvalue.Value;
                                value = Math.Round(value, 0, MidpointRounding.AwayFromZero);
                                CreateCell(row, cellindex++, value, style);
                            }

                            rowindex++;
                        }

                        int cells = overzichten.Count;
                        if (cells < columns.Count)
                            cells = columns.Count;
                        //create chart
                        CreateProductieChart(sheet, omschrijving, rowstart,chartdata.Count, 0, cells + 1,
                            columns.ToArray());
                        rowindex += 19;
                    }

                    for (var i = 0; i <= colsmade; i++)
                        sheet.AutoSizeColumn(i, true);

                    return sheet;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetValue(ISheet sheet, int rowindex, int colindex)
        {
            var value = sheet.GetRow(rowindex)?.GetCell(colindex);
            if (value == null) return null;
            switch (value.CellType)
            {
                case CellType.String:
                case CellType.Unknown:
                case CellType.Blank:
                    return value.StringCellValue;
                case CellType.Numeric:
                    return value.NumericCellValue.ToString();
                case CellType.Formula:
                    return value.CellFormula;
                case CellType.Boolean:
                    return value.BooleanCellValue.ToString();
                case CellType.Error:
                    return value.ErrorCellValue.ToString();
            }

            return null;
        }

        public static double GetSumValueRange(List<Bewerking> producties, TijdEntry bereik, string type)
        {
            if (producties.Count == 0) return 0;
            switch (type.ToLower())
            {
                case "tijd gewerkt":
                    return Math.Round(producties.Sum(x => x.TijdAanGewerkt(bereik.Start, bereik.Stop)), 2);
                case "aantal gemaakt":
                    double done = 0;
                    foreach (var prod in producties)
                    {
                        if (prod.TotaalGemaakt == 0) continue;
                        var tg = prod.TijdAanGewerkt(bereik.Start, bereik.Stop);
                        var totaltg = prod.TijdAanGewerkt();
                        if (tg <= 0) continue;
                        var percentage = tg / totaltg * 100;
                        var aantal = prod.TotaalGemaakt / 100 * percentage;
                        done += aantal;
                    }

                    return Math.Round(done, 0);
                case "per uur":
                    var gewerkt = GetSumValueRange(producties, bereik, "tijd gewerkt");
                    var gemaakt = GetSumValueRange(producties, bereik, "aantal gemaakt");
                    if (gewerkt <= 0 || gemaakt <= 0) return 0;
                    return Math.Round(gemaakt / gewerkt, 0);
                case "storingen":
                    return Math.Round(
                        producties.Sum(x => x.GetAlleStoringen(false).Sum(t => t.GetTotaleTijd(bereik.Start, bereik.Stop))),
                        2);
            }

            return 0;
        }

        public static Dictionary<string, List<Bewerking>> CreateProductieSections(List<ProductieFormulier> producties,
            TijdEntry bereik)
        {
            var xreturn = new Dictionary<string, List<Bewerking>>();
            if (producties?.Count == 0) return xreturn;
            if (producties != null)
                foreach (var prod in producties)
                {
                    if (prod.Bewerkingen == null || prod.Bewerkingen.Length == 0) continue;
                    foreach (var bw in prod.Bewerkingen)
                    {
                        if (bw.TotaalGemaakt == 0 || bw.TijdAanGewerkt(bereik.Start, bereik.Stop) <= 0) continue;
                        if (!xreturn.ContainsKey(bw.Naam))
                            xreturn.Add(bw.Naam, new List<Bewerking> {bw});
                        else xreturn[bw.Naam].Add(bw);
                    }
                }

            return xreturn;
        }

        private static void CreateProductieChart(ISheet sheet, string title, int startrow, int endrow, int startcell,
            int endcell, string[] legends)
        {
            var drawing = sheet.CreateDrawingPatriarch();
            var anchor1 = drawing.CreateAnchor(startcell, startrow, startcell + endcell, startrow + endrow, startcell,
                startrow + endrow + 1, startcell + endcell, startrow + endrow + 20);
            var chart = drawing.CreateChart(anchor1);
            var legend = chart.GetOrCreateLegend();
            legend.Position = LegendPosition.Right;
            var data = chart.ChartDataFactory.CreateLineChartData<string, double>();
            // Use a category axis for the bottom axis.
            var bottomAxis = chart.ChartAxisFactory.CreateCategoryAxis(AxisPosition.Bottom);
            bottomAxis.MajorTickMark = AxisTickMark.None;
            bottomAxis.Minimum = 0;
            var leftAxis = chart.ChartAxisFactory.CreateValueAxis(AxisPosition.Left);
            leftAxis.Crosses = AxisCrosses.AutoZero;
            leftAxis.MajorTickMark = AxisTickMark.None;
            leftAxis.Minimum = 0;
            var curcol = 1;
            foreach (var xlegend in legends)
            {
                var datanamerange = DataSources.FromStringCellRange(sheet,
                    new CellRangeAddress(startrow, startrow + endrow, startcell, startcell));
                var datavaluerange = DataSources.FromNumericCellRange(sheet,
                    new CellRangeAddress(startrow, startrow + endrow, curcol, curcol));
                //double[] values = valuerange.Select(x => Math.Round(x.Value[curcol],0)).ToArray();
                //IChartDataSource<string> datanamerange = DataSources.FromArray<string>(namerages);
                //IChartDataSource<double> datavaluerange = DataSources.FromArray<double>(values);

                var series = data.AddSeries(datanamerange, datavaluerange);
                series.SetTitle(xlegend);
                curcol++;
            }

            chart.Plot(data, bottomAxis, leftAxis);
        }

        public static void CreateHeader(ISheet sheet, string header, int startrow, int rows, int startcell, int cells,
            ICellStyle style)
        {
            if (rows > 0 && cells > 0)
            {
                for (var i = startrow; i < startrow + rows; i++)
                {
                    var row = sheet.CreateRow(i);
                    for (var j = startcell; j < startcell + cells; j++)
                        row.CreateCell(j);
                }

                var cra = new CellRangeAddress(startrow, startrow + rows - 1, startcell, startcell + cells - 1);
                sheet.AddMergedRegion(cra);

                var cell = sheet.GetRow(startrow).GetCell(startcell);
                cell.CellStyle = style;
                cell.SetCellValue(header);
            }
        }

        public static ICellStyle CreateStyle(XSSFWorkbook workbook, bool isbold, HorizontalAlignment textalign,
            int fontheight)
        {
            var cellStyleBorder = GetBorderStyle(workbook);
            cellStyleBorder.Alignment = textalign;
            //cell header font
            var cellStylefont = workbook.CreateFont();
            cellStylefont.IsBold = isbold;
            cellStylefont.FontHeightInPoints = fontheight;
            cellStyleBorder.SetFont(cellStylefont);
            return cellStyleBorder;
        }

        public static ISheet CreateProductieOverzicht(XSSFWorkbook workbook, TijdEntry bereik, string omschrijving,
            string naam, List<Bewerking> producties)
        {
            try
            {
                naam += $"({producties.Count})";

                var sheet = workbook.CreateSheet(naam);

                //border layaout
                var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Center, 18);
                CreateHeader(sheet, omschrijving, 0, 2, 0, ProductieColumns.Length, cellStyleBorder);

                //create column font
                cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12);
                //init the columns and font
                var rowindex = 2;
                var row = sheet.CreateRow(rowindex);
               
                var cellindex = 0;
                foreach (var xrow in ProductieColumns)
                    CreateCell(row, cellindex++, xrow, cellStyleBorder);
                rowindex++;

                #region Populate Values

                //Populate fields
               

                var aantalpers = 0;
                double totaaltg = 0;
                //var allbws = producties.Select(x => x.Bewerkingen).ToArray();
                var personeel = new List<string>();
               
                foreach (var bw in producties)
                {
                    var tg = bereik == null ? bw.TijdAanGewerkt() : bw.TijdAanGewerkt(bereik.Start, bereik.Stop);
                    //if (tg <= 0) continue;
                    row = sheet.CreateRow(rowindex);
                    row.HeightInPoints = 15;
                    //Fill Green if Passing Score

                    var formatting = CreateRowConditionalFormatRules(sheet, rowindex + 1, ProductieColumns.Length);
                    cellindex = 0;
                    cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Left, 11);
                    foreach (var xrow in ProductieColumns)
                    {
                        var cell = CreateCell(row, cellindex++, GetValue(bw, xrow, bereik), cellStyleBorder);
                    }

                    var pers = bw.GetPersoneel().Select(x => x.PersoneelNaam).Where(x =>
                        personeel.All(t => !string.Equals(x, t, StringComparison.CurrentCultureIgnoreCase))).ToArray();
                    if (pers.Length > 0)
                        personeel.AddRange(pers);
                    aantalpers += pers.Length;

                    totaaltg += tg;
                    rowindex++;
                }

                //Laten we nu wat extra velden aanmaken om het totale te kunnen weergeven van sommige velden.
                //hier maken we de cel styles aan!
                cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12);

                //hier gaan we de cells aanmaken.
                row = sheet.CreateRow(rowindex);
                CreateCell(row, 0, "Totaal", cellStyleBorder);

                cellindex = GetProductieColumnIndex("tijd gewerkt");
                var xs = GetColumnName(cellindex);
                CreateCellFormula(row, cellindex, $"SUM({xs}4:{xs}{rowindex})", cellStyleBorder);

                cellindex = GetProductieColumnIndex("productie aantal");
                xs = GetColumnName(cellindex);
                CreateCellFormula(row, cellindex, $"SUM({xs}4:{xs}{rowindex})", cellStyleBorder);

                cellindex = GetProductieColumnIndex("aantal gemaakt");
                xs = GetColumnName(cellindex);
                CreateCellFormula(row, cellindex, $"SUM({xs}4:{xs}{rowindex})", cellStyleBorder);

                cellindex = GetProductieColumnIndex("actueel per uur");
                xs = GetColumnName(cellindex);
                CreateCellFormula(row, cellindex, $"ROUND(SUM({xs}4:{xs}{rowindex}) / {rowindex - 3},0)",
                    cellStyleBorder);

                cellindex = GetProductieColumnIndex("per uur");
                xs = GetColumnName(cellindex);
                CreateCellFormula(row, cellindex, $"ROUND(SUM({xs}4:{xs}{rowindex}) / {rowindex - 3},0)",
                    cellStyleBorder);

                cellindex = GetProductieColumnIndex("aantal personen");
                xs = GetColumnName(cellindex);
                CreateCellFormula(row, cellindex, $"SUM({xs}4:{xs}{rowindex})", cellStyleBorder);

                if (aantalpers > 0)
                {
                    var tijdpp = totaaltg == 0 ? 0 : totaaltg / aantalpers;
                    var xpers = aantalpers == 1 ? "persoon" : "personen";
                    var xvalue = $"Gewerkt met {aantalpers} verschillende {xpers} ({Math.Round(tijdpp, 2)} uur p.p.)";
                    CreateCell(row, GetProductieColumnIndex("werkplekken"), xvalue, cellStyleBorder);
                }

                rowindex++;

                #endregion

                for (var i = 0; i < ProductieColumns.Length; i++)
                    sheet.AutoSizeColumn(i, true);

                //verberg sommige info velden die niet nodig zijn.
                foreach (var xv in HiddenProductieColumns)
                    sheet.SetColumnHidden(GetProductieColumnIndex(xv), true);

                return sheet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static ISheetConditionalFormatting CreateRowConditionalFormatRules(ISheet sheet, int rowindex, int cellcount)
        {
            try
            {
                var formatting = sheet.SheetConditionalFormatting;
                List<IConditionalFormattingRule> rules = new List<IConditionalFormattingRule>();
                var states = Enum.GetValues(typeof(ProductieState));
                var cellindex = GetProductieColumnIndex("status");
                var xcolname = $"${GetColumnName(cellindex)}${rowindex}";
                foreach (var state in states.Cast<ProductieState>())
                {
                    if (state == ProductieState.Verwijderd) continue;
                    var rule = formatting.CreateConditionalFormattingRule(
                        $"{xcolname} = \"{Enum.GetName(typeof(ProductieState), state)}\"");
                    var rulepatern = rule.CreatePatternFormatting();
                    switch (state)
                    {
                        case ProductieState.Gestopt:
                            rulepatern.FillBackgroundColor = IndexedColors.LightYellow.Index;
                            break;
                        case ProductieState.Gestart:
                            rulepatern.FillBackgroundColor = IndexedColors.LightCornflowerBlue.Index;
                            break;
                        case ProductieState.Gereed:
                            rulepatern.FillBackgroundColor = IndexedColors.LightGreen.Index;
                            break;
                        case ProductieState.Verwijderd:
                            rulepatern.FillBackgroundColor = IndexedColors.Orange.Index;
                            break;
                    }
                    
                    rulepatern.FillPattern = FillPattern.SolidForeground;

                    rules.Add(rule);
                }
                string range = $"{GetColumnName(0)}{rowindex}:{GetColumnName(cellcount - 1)}{rowindex}";
                CellRangeAddress[] cfRange = { CellRangeAddress.ValueOf(range) };
                formatting.AddConditionalFormatting(cfRange, rules.ToArray());
                return formatting;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static ISheetConditionalFormatting CreateStoringRowConditionalFormatRules(ISheet sheet, int rowindex,
            int cellcount)
        {
            try
            {
                var formatting = sheet.SheetConditionalFormatting;
                List<IConditionalFormattingRule> rules = new List<IConditionalFormattingRule>();
                //var states = Enum.GetValues(typeof(ProductieState));
                var cellindex = GetStoringColumnIndex("Is Verholpen");
                var xcolname = $"${GetColumnName(cellindex)}${rowindex}";
                var rule = formatting.CreateConditionalFormattingRule(
                    $"{xcolname} = \"Nee\"");
                var rulepatern = rule.CreatePatternFormatting();
                rulepatern.FillBackgroundColor = IndexedColors.LightYellow.Index;
                rulepatern.FillPattern = FillPattern.SolidForeground;
                rules.Add(rule);
                rule = formatting.CreateConditionalFormattingRule(
                    $"{xcolname} = \"Ja\"");
                rulepatern = rule.CreatePatternFormatting();
                rulepatern.FillBackgroundColor = IndexedColors.LightGreen.Index;
                rulepatern.FillPattern = FillPattern.SolidForeground;
                rules.Add(rule);

                string range = $"{GetColumnName(0)}{rowindex}:{GetColumnName(cellcount - 1)}{rowindex}";
                CellRangeAddress[] cfRange = {CellRangeAddress.ValueOf(range)};
                formatting.AddConditionalFormatting(cfRange, rules.ToArray());
                return formatting;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static short GetRowColor(ProductieState state)
        {
            switch (state)
            {
                case ProductieState.Gestopt:
                    return HSSFColor.Yellow.Index;
                case ProductieState.Gestart:
                    return HSSFColor.Green.Index;
                case ProductieState.Gereed:
                     return HSSFColor.LightCornflowerBlue.Index;
                case ProductieState.Verwijderd:
                    return HSSFColor.Red.Index;
            }
            return HSSFColor.White.Index;
        }

        public static ISheet CreateStoringOverzicht(XSSFWorkbook workbook, List<Storing> storingen, string omschrijving,
            string naam, TijdEntry vanaf)
        {
            try
            {
                if (storingen?.Count == 0)
                    return null;
                var sheet = workbook.CreateSheet(naam + $"({storingen.Count})");
                #region Sheet Titel Aanmaken

                //create header cells
                var row = sheet.CreateRow(0);
                for (var i = 0; i < StoringColumns.Length; i++)
                    row.CreateCell(i);
                row = sheet.CreateRow(1);
                for (var i = 0; i < StoringColumns.Length; i++)
                    row.CreateCell(i);

                //border layaout
                var cellStyleBorder = GetBorderStyle(workbook);
                cellStyleBorder.Alignment = HorizontalAlignment.Center;
                //cell header font
                var cellStylefont = workbook.CreateFont();
                cellStylefont.IsBold = true;
                cellStylefont.FontHeightInPoints = 18;
                cellStyleBorder.SetFont(cellStylefont);

                var cra = new CellRangeAddress(0, 1, 0, StoringColumns.Length - 1);
                sheet.AddMergedRegion(cra);

                var cell = sheet.GetRow(0).GetCell(0);
                cell.CellStyle = cellStyleBorder;
                cell.CellStyle.SetFont(cellStylefont);
                cell.SetCellValue(omschrijving);

                #endregion

                #region Columns Aanmaken

                //create column font
                cellStyleBorder = GetBorderStyle(workbook);
                cellStylefont = workbook.CreateFont();
                cellStylefont.IsBold = true;
                cellStylefont.FontHeightInPoints = 12;
                cellStyleBorder.SetFont(cellStylefont);
                //init the columns and font
                var rowindex = 2;
                row = sheet.CreateRow(rowindex);
                var cellindex = 0;
                foreach (var col in StoringColumns)
                    CreateCell(row, cellindex++, col, cellStyleBorder);

                #endregion

                rowindex++;

                #region Columns Aanvullen
               
                //Populate fields
                cellStyleBorder = GetBorderStyle(workbook);
                cellStylefont = workbook.CreateFont();
                cellStylefont.IsBold = false;
                cellStylefont.FontHeightInPoints = 11;
                cellStyleBorder.SetFont(cellStylefont);
                foreach (var st in storingen)
                {
                    row = sheet.CreateRow(rowindex);
                    row.HeightInPoints = 15;
                    var formatting = CreateStoringRowConditionalFormatRules(sheet, rowindex + 1, StoringColumns.Length);
                    cellindex = 0;
                    foreach (var col in StoringColumns)
                        CreateCell(row, cellindex++, GetValue(st, col,vanaf), cellStyleBorder);

                    rowindex++;
                }

                #endregion

                #region Totale Rijen Aanmaken

                cellStyleBorder = GetBorderStyle(workbook);
                cellStylefont = workbook.CreateFont();
                cellStylefont.IsBold = true;
                cellStylefont.FontHeightInPoints = 12;
                cellStyleBorder.SetFont(cellStylefont);

                //hier gaan we de cells aanmaken.
                row = sheet.CreateRow(rowindex);
                CreateCell(row, 0, "Totaal", cellStyleBorder);
                //totaal tijd gewerkt.
                cellindex = GetStoringColumnIndex("totaal tijd");
                if (cellindex > -1)
                {
                    var xs = GetColumnName(cellindex);
                    CreateCellFormula(row, cellindex, $"SUM({xs}4:{xs}{rowindex})", cellStyleBorder);
                }

                #endregion

                for (var i = 0; i < StoringColumns.Length; i++)
                    sheet.AutoSizeColumn(i, true);

                for (var i = 0; i < HiddenStoringColumns.Length; i++)
                    sheet.SetColumnHidden(GetStoringColumnIndex(HiddenStoringColumns[i]), true);

                return sheet;
            }
            catch
            {
                return null;
            }
        }

        private static int GetProductieColumnIndex(string naam)
        {
            for (var i = 0; i < ProductieColumns.Length; i++)
                if (string.Equals(ProductieColumns[i], naam, StringComparison.CurrentCultureIgnoreCase))
                    return i;
            return -1;
        }

        private static int GetStoringColumnIndex(string naam)
        {
            for (var i = 0; i < StoringColumns.Length; i++)
                if (string.Equals(StoringColumns[i], naam, StringComparison.CurrentCultureIgnoreCase))
                    return i;
            return -1;
        }

        private static string GetColumnName(int columnNumber)
        {
            var dividend = columnNumber + 1;
            var columnName = string.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

        public static ICell CreateCell(IRow row, int index, object value, ICellStyle style)
        {
            if (row == null || value == null) return null;
            var cell = row.CreateCell(index);
            if (value is DateTime time)
                cell.SetCellValue(time.ToString());
            else if (value is double xdouble)
                cell.SetCellValue(xdouble);
            else if (value is int xint)
                cell.SetCellValue(xint);
            else if (value is string xstring)
                cell.SetCellValue(xstring);
            else if (value is bool xbool)
                cell.SetCellValue(xbool ? "Ja" : "Nee");
            else if (value is IRichTextString xrich)
                cell.SetCellValue(xrich);
            else
                cell.SetCellValue(value.ToString());
            cell.CellStyle = style;
            return cell;
        }

        public static ICell CreateCellFormula(IRow row, int index, string formula, ICellStyle style)
        {
            if (row == null || string.IsNullOrEmpty(formula)) return null;
            var cell = row.CreateCell(index);
            cell.SetCellFormula(formula);
            cell.CellStyle = style;
            return cell;
        }

        public static object GetValue(Storing storing, string value, TijdEntry vanaf)
        {
            if (storing == null || string.IsNullOrEmpty(value)) return null;
            switch (value.ToLower())
            {
                case "storing type":
                    return storing.StoringType;
                case "omschrijving":
                    return storing.Omschrijving;
                case "gemeld door":
                    return storing.GemeldDoor;
                case "gestart op":
                    return storing.Gestart.ToString();
                case "gestopt op":
                    return (storing.IsVerholpen ? storing.Gestopt : DateTime.Now).ToString();
                case "totaal tijd":
                    return vanaf == null? storing.GetTotaleTijd() : storing.GetTotaleTijd(vanaf.Start, vanaf.Stop);
                case "is verholpen":
                    return storing.IsVerholpen;
                case "verholpen door":
                    return storing.VerholpenDoor;
                case "oplossing":
                    return storing.Oplossing;
                case "productie omschrijving":
                    return storing.Path;
                case "werkplek":
                    return storing.WerkPlek;
            }

            return "N.V.T";
        }

        public static object GetValue(Bewerking bew, string value, TijdEntry vanaf)
        {
            if (bew == null || string.IsNullOrEmpty(value)) return null;
            switch (value.ToLower())
            {
                case "productienr":
                    return bew.ProductieNr;
                case "artikelnr":
                    return bew.ArtikelNr;
                case "omschrijving":
                    return bew.Omschrijving;
                case "bewerking naam":
                    return bew.Naam;
                case "status":
                    return Enum.GetName(typeof(ProductieState), bew.State);
                case "start datum":
                    return bew.GestartOp().ToString();
                case "eind datum":
                    return bew.GestoptOp().ToString();
                case "tijd gewerkt":
                    return vanaf == null? bew.TijdAanGewerkt() : bew.TijdAanGewerkt(vanaf.Start, vanaf.Stop);
                case "productie aantal":
                    return bew.Aantal;
                case "aantal gemaakt":
                    return bew.TotaalGemaakt;
                case "actueel per uur":
                    return bew.ActueelProductenPerUur();
                case "per uur":
                    return bew.PerUur;
                case "aantal personen":
                    return bew.AantalPersonen;
                case "werkplekken":
                    var wps = bew.WerkPlekken?.Where(x => x.Personen.Count > 0).ToArray();
                    if (wps?.Length > 0)
                        return string.Join(", ", wps.Select(x => x.Naam));
                    return "Geen Werkplekken";
                case "personen":
                    var pers = bew.GetPersoneel().Select(x => x.PersoneelNaam).ToArray();
                    if (pers.Length > 0)
                        return string.Join(", ", pers);
                    return "Geen Personeel";
            }

            return "N.V.T.";
        }

        public static Task<string> CreateWeekOverzicht(TijdEntry bereik,
            List<Bewerking> bewerkingen, bool createoverzicht, string filepath, string omschrijving)
        {
            return Task.Run(() =>
            {
                try
                {
                    var path = filepath;//$"{filepath}.xlsx";
                    var storingen = new List<Storing>();
                    //var bewerkingen = new List<Bewerking>();
                    for (var i = 0; i < bewerkingen.Count; i++)
                    {
                        var bw = bewerkingen[i];
                        //if (producties[i].Bewerkingen?.Length == 0) continue;
                        if (!bw.IsAllowed())
                        {
                            bewerkingen.RemoveAt(i--);
                            continue;
                        }
                        var sts = bereik == null
                            ? bw.GetAlleStoringen(false)
                            : bw.GetAlleStoringen(bereik.Start, bereik.Stop);
                        if (sts?.Length > 0)
                            storingen.AddRange(sts);
                        var tg = bereik == null
                            ? bw.TijdAanGewerkt()
                            : bw.TijdAanGewerkt(bereik.Start, bereik.Stop);
                        if (bereik != null && tg == 0)
                            bewerkingen.RemoveAt(i--);
                    }

                    var workbook = new XSSFWorkbook();
                    if (bewerkingen.Count > 0)
                    {
                        bewerkingen = bewerkingen.OrderBy(x => x.ArtikelNr).ToList();
                        var sheetname = $"Producties";
                        var xomschrijving = $"Productie {omschrijving}";
                        var sheet = CreateProductieOverzicht(workbook, bereik,
                            xomschrijving, sheetname, bewerkingen);
                    }

                    if (storingen.Count > 0)
                    {
                        storingen = storingen.OrderBy(x => x.WerkPlek).ToList();
                        //Creeer een niew tapblad als er storingen zijn.
                        var sheetname = $"Onderbrekeningen";
                        var xomschrijving =
                            $"Onderbreking {omschrijving}";
                        CreateStoringOverzicht(workbook, storingen, xomschrijving, sheetname,
                           bereik);
                    }

                    if (createoverzicht)
                    {
                        CreateOverzichtChartSheet(workbook, bewerkingen, false);
                        CreateOverzichtChartSheet(workbook, bewerkingen, true);
                    }

                    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fs);
                    }

                    workbook.Close();
                    return path;
                }
                catch (Exception e)
                {
                    return null;
                }
            });
        }

        public static Task<WeekOverzicht[]> CreateWeekOverzicht(WeekOverzicht[] overzichten, bool createoverzicht)
        {
            return Task.Run(async () =>
            {
                var aangemaakt = new List<WeekOverzicht>();
                try
                {
                    if (Manager.Database == null || Manager.Database.IsDisposed || overzichten?.Length == 0)
                        return aangemaakt.ToArray();

                    var bewerkingen = await Manager.Database.GetAllBewerkingen(true,true);
                    var rootpath = Manager.Opties.WeekOverzichtPath;
                    if (!Directory.Exists(rootpath))
                        Directory.CreateDirectory(rootpath);
                    if (bewerkingen.Count > 0)
                        if (overzichten != null)
                            foreach (var overzicht in overzichten)
                                try
                                {
                                    var xbws = new List<Bewerking>();
                                    var startweek =
                                        Functions.DateOfWeek(overzicht.Jaar, DayOfWeek.Monday, overzicht.WeekNr);
                                    var endweek = startweek.DateOfWeek(DayOfWeek.Sunday);
                                    if (Manager.Opties?.WerkRooster != null)
                                        startweek = startweek.Add(Manager.Opties.WerkRooster.StartWerkdag);

                                    if (Manager.Opties?.WerkRooster != null)
                                        endweek = endweek.Add(Manager.Opties.WerkRooster.EindWerkdag);
                                    var storingen = new List<Storing>();
                                    //var xbewerkingen = new List<Bewerking>();
                                    for (var i = 0; i < bewerkingen.Count; i++)
                                    {
                                        var bw = bewerkingen[i];
                                        var sts = bw.GetAlleStoringen(startweek, endweek);
                                        if (sts?.Length > 0)
                                            storingen.AddRange(sts);
                                        if (bw.TijdAanGewerkt(startweek, endweek) > 0)
                                            xbws.Add(bw);
                                    }

                                    if (xbws.Count == 0 && storingen.Count == 0) continue;
                                    xbws = xbws.OrderBy(x => x.ArtikelNr).ToList();
                                    storingen = storingen.OrderBy(x => x.WerkPlek).ToList();
                                    var weekNum = overzicht.WeekNr;
                                    var year = overzicht.Jaar;
                                    var afdeling = overzicht.Afdeling ?? "Hele";
                                    var sheetname = overzicht.ToString();
                                    var path = $"{rootpath}\\{sheetname}.xlsx";

                                    var omschrijving = $"Week {weekNum} {year} {afdeling} productie overzicht";

                                    var workbook = new XSSFWorkbook();

                                    var sheet = CreateProductieOverzicht(workbook, new TijdEntry(startweek, endweek, null),
                                        omschrijving, sheetname, xbws);
                                    if (sheet == null) continue;
                                    if (storingen.Count > 0)
                                    {
                                        //Creeer een niew tapblad als er storingen zijn.
                                        sheetname = $"Week {weekNum} {year} Storingen";
                                        omschrijving = $"Week {weekNum} {year} {afdeling} Storing Overzicht";
                                        CreateStoringOverzicht(workbook, storingen, omschrijving, sheetname,new TijdEntry(startweek, endweek, null));
                                    }

                                    if (createoverzicht)
                                    {
                                        CreateOverzichtChartSheet(workbook, bewerkingen, false);
                                        CreateOverzichtChartSheet(workbook, bewerkingen, true);
                                    }

                                    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                                    {
                                        workbook.Write(fs);
                                    }

                                    workbook.Close();
                                    aangemaakt.Add(overzicht);
                                }
                                catch (Exception)
                                {
                                }

                    return aangemaakt.ToArray();
                }
                catch (Exception)
                {
                    return aangemaakt.ToArray();
                }
            });
        }

        public static Task<List<WeekOverzicht>> UpdateWeekOverzichten(int startweek, int startjaar,
            List<WeekOverzicht> bestaande, int updateweeknr)
        {
            return Task.Run(async () =>
            {
                try
                {
                    //we gaan controleren welke overzichten we moeten gaan sturen van afgelopen weken.
                    var alleoverzichten = Functions.GetWeekRanges(startweek, startjaar,
                        Manager.Opties.DoCurrentWeekOverzicht, false);
                    var tobecreated = new List<WeekOverzicht>();

                    foreach (var xweekrange in alleoverzichten)
                    {
                        var current = new WeekOverzicht {WeekNr = xweekrange.Week, Jaar = xweekrange.Year, Versie = 1};
                        if ((updateweeknr == 0 || updateweeknr != xweekrange.Week) &&
                            bestaande.Any(x => x.Equals(current))) continue;
                        tobecreated.Add(current);
                    }

                    if (tobecreated.Count > 0)
                    {
                        var newcreated = await ExcelWorkbook.CreateWeekOverzicht(tobecreated.ToArray(), true);
                        if (newcreated?.Length > 0) bestaande.AddRange(newcreated);
                    }
                }
                catch
                {
                }

                return bestaande;
            });
        }

        private static bool _issendingweek;
        public static Task<int> UpdateWeekOverzichten()
        {
            return Task.Run(async () =>
            {
                if (Manager.LogedInGebruiker == null)
                    return 0;
                var created = 0;
                var send = 0;
                if (Manager.Opties == null || !Directory.Exists(Manager.Opties.WeekOverzichtPath)) return 0;
                try
                {
                    var opties = Manager.Opties;
                    if (!_issendingweek)
                    {
                        _issendingweek = true;

                        var files = Directory.GetFiles(Manager.Opties.WeekOverzichtPath);
                        var overzichten = new List<WeekOverzicht>();

                        foreach (var file in files)
                        {
                            var overzicht = WeekOverzicht.FromString(Path.GetFileNameWithoutExtension(file));
                            if (overzicht != null) overzichten.Add(overzicht);
                        }


                        int currentweek = DateTime.Now.GetWeekNr();
                        overzichten = await UpdateWeekOverzichten(opties.VanafWeek, opties.VanafJaar, overzichten,
                            opties.DoCurrentWeekOverzicht ? currentweek : 0);
                        if (opties.VerzendAdres?.Count > 0)
                        {
                            foreach (var adres in opties.VerzendAdres)
                            {
                                if (!adres.SendWeekOverzichten || adres.VanafWeek < 1 || adres.VanafYear < 2021)
                                    continue;
                                if (adres.VerzondenWeekOverzichten == null)
                                    adres.VerzondenWeekOverzichten = new List<WeekOverzicht>();
                                overzichten =
                                    await UpdateWeekOverzichten(adres.VanafWeek, adres.VanafYear, overzichten, 0);
                                foreach (var overzicht in overzichten)
                                {
                                    if (overzicht.WeekNr == currentweek || adres.VerzondenWeekOverzichten.Any(t => t.Equals(overzicht))) continue;
                                    if (adres.VanafYear > overzicht.Jaar) continue;
                                    if (adres.VanafWeek > overzicht.WeekNr && adres.VanafYear >= overzicht.Jaar)
                                        continue;
                                    var path = $"{opties.WeekOverzichtPath}\\{overzicht.ToString()}.xlsx";
                                    if (File.Exists(path))
                                    {
                                        var omschrijving =
                                            $"Week {overzicht.WeekNr} {overzicht.Jaar} {overzicht.Afdeling} overzicht";
                                        var msg = CreateOverzichtMailBody(overzicht);
                                        if (RemoteProductie.SendEmail(adres.Adres, omschrijving, msg,
                                            new List<string>() { path }, false,false))
                                        {
                                            adres.VerzondenWeekOverzichten.Add(overzicht);
                                            send++;
                                        }
                                    }
                                }
                            }

                            if (send > 0)
                                await opties.Save($"{send} Verzonden week overzicht(en) update");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                _issendingweek = false;
                return send > 0 ? send : created;
            });
        }

        private static string CreateOverzichtMailBody(WeekOverzicht overzicht)
        {
            var omschrijving = "Beste Collega,\n\n" +
                               $"U bent aangemeld voor een wekelijks overzicht door de Productie Manager voor {overzicht.Afdeling}.\n" +
                               "Als u deze mails niet wenst te ontvangen, dan kunt u dat aangeven bij Ihab zodat hij u kan afmelden.\n\n" +
                               $"Dit overzicht is voor week {overzicht.WeekNr} {overzicht.Jaar} en geld alleen voor {overzicht.Afdeling}.\n" +
                               "In de bijlage is er een Excel bestand met het weekoverzicht en de statistieken van de afgelopen weken.\n\n" +
                               $"In de eerste sheet zijn alle producties van week {overzicht.WeekNr} te zien, waarvan veel velden zijn verborgen om de overzicht te bewaren.\n" +
                               "Zo kunt u zien wat er is geproduceerd, wat de gemiddelde zijn en door wie het is gedaan.\n" +
                               "Als er onderbrekingen zijn zoals storingen, onderhoudt en schoonmaak, dan is daar een aparte sheet voor.\n" +
                               "Zo kunt u alle onderbrekingen gedetailleerd zien van heel de week.\n" +
                               "Dit overzicht is hooguit een beeld van hoe de Productie Manager is bijgehouden.\n" +
                               "Hoe beter het wordt bijgehouden, hoe nauwkeuriger het Excel overzicht.\n\n" +
                               "Mocht er vragen zijn tot betrekking van dit mail, of het weekoverzicht dat kunt u dat aangeven bij Ihab.\n" +
                               "Veel leesplezier!";

            return omschrijving;
        }
    }
}