using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Rpm.Mailing;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Rpm.Misc;

// ReSharper disable All

namespace ProductieManager.Rpm.ExcelHelper
{
    /// <summary>
    /// Een class voor het maken van excel sheets
    /// </summary>
    public static class ExcelWorkbook
    {
        ///// <summary>
        ///// De productie Columns
        ///// </summary>
        //public static string[] ProductieColumns =
        //{
        //    "ArtikelNr", "ProductieNr", "Omschrijving", "Bewerking Naam", "Status",
        //    "Start Datum", "Eind Datum", "Tijd Gewerkt(uur)", "Productie Aantal",
        //    "Aantal Gemaakt", "Actueel P/u", "Per Uur","Afwijking P/u(%)","Gemiddeld Actueel P/u", "Gemiddeld Afwijking P/u(%)", "Aantal Personen",
        //    "Werkplekken", "Personen"
        //};

        ///// <summary>
        ///// Verkrijg een column beschrijving
        ///// </summary>
        ///// <param name="colmn">De naam van de column</param>
        ///// <returns>De column omschrijving</returns>
        //public static string GetProductieColumnOmschrijving(string colmn)
        //{
        //    var x = ProductieColumns.FirstOrDefault(x =>
        //        string.Equals(x, colmn, StringComparison.CurrentCultureIgnoreCase));
        //    if (string.IsNullOrEmpty(x)) return colmn;
        //    switch (x.ToLower())
        //    {
        //        case "productienr":
        //            return "Unieke nr dat de productie representeerd.";
        //        case "artikelnr":
        //            return "Product artikel nummer.";
        //        case "omschrijving":
        //            return "De omschrijving van de productie.";
        //        case "bewerking naam":
        //            return "Bewerking naam.";
        //        case "status":
        //            return "Huidige status van de bewerking (Gestart,gestopt,gereed of verwijderd).";
        //        case "start datum":
        //            return "De datum waarop de bewerking is gestart.";
        //        case "eind datum":
        //            return "De tijd waarop de bewerking is gestopt.";
        //        case "tijd gewerkt":
        //            return "De aantal gewerkte uren aan de bewerking.";
        //        case "productie aantal":
        //            return "Aantal om te produceren";
        //        case "aantal gemaakt":
        //            return "Aantal gemaakt";
        //        case "actueel p/u":
        //            return "Actuele aantal per uur";
        //        case "per uur":
        //            return "Aantal per uur dat bekend is volgens de productie formulier";
        //        case "afwijking p/u":
        //            return
        //                "Percentage afwijking tussen de actuele aantal per uur en de aantal die bekend is.";
        //        case "gemiddeld actueel p/u":
        //            return "Dit is de gemiddelde actuele aantal per uur op basis van alle bewerkingen met de zelfde artikel nummer en de bewerking naam";
        //        case "afwijking gemiddeld p/u":
        //            return "Percentage afwijking van de gemiddeld actuele per uur op basis van wat er bekend is";
        //        case "aantal personen":
        //            return "De aantal personen gewerkt aan de bewerking";
        //        case "werkplekken":
        //            return "De werkplaatsen gebruikt voor de bewerking";
        //        case "personen":
        //            return "De personen die aan de bewerking hebben gewerkt";
        //    }

        //    return colmn;
        //}

        ///// <summary>
        ///// De productie columns die verborgen moeten worden
        ///// </summary>
        //public static string[] HiddenProductieColumns =
        //{
        //    "ProductieNr", "Omschrijving",
        //    "Start Datum", "Eind Datum", "Productie Aantal",
        //    "Productie Aantal", "Per Uur",
        //    "Personen"
        //};

        /// <summary>
        /// De storing columns
        /// </summary>
        public static string[] StoringColumns =
        {
            "WerkPlek", "Storing Type", "Omschrijving", "Gemeld Door", "Gestart Op",
            "Gestopt Op", "Totaal Tijd", "Is Verholpen", "Verholpen Door", "Oplossing",
            "Productie Omschrijving"
        };


        /// <summary>
        /// Verborgen storing columns
        /// </summary>
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

        /// <summary>
        /// Maak aan de productie statistieken en grafieken een excel sheet.
        /// </summary>
        /// <param name="workbook">De excel waarvoor de sheet gemaakt moet worden</param>
        /// <param name="bewerkingen">De bewerkingen waarvoor de statistieken gemaakt moeten worden</param>
        /// <param name="iswerkplek">Of de statistieken voor de werkplekken moet zijn i.p.v de bewerkingen</param>
        /// <returns>De excel sheet</returns>
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
                        var style = CreateStyle(workbook, true, HorizontalAlignment.Center, 18, IndexedColors.Black.Index);
                        var omschrijving = $"{xinfo} {fields[fieldindex]} van de afgelopen {overzichten.Count} {xwk}.";
                       // CreateHeader(sheet, omschrijving, rowindex, 2, 0, columns.Count + 1, style);
                        //rowindex += 2;
                        var rowstart = rowindex;
                        //create column font
                        style = CreateStyle(workbook, true, HorizontalAlignment.Left, 12, IndexedColors.Black.Index);
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
                            style = CreateStyle(workbook, false, HorizontalAlignment.Left, 11, IndexedColors.Black.Index);
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

        /// <summary>
        /// Verkrijg een waarde vanuit een cel lokatie
        /// </summary>
        /// <param name="sheet">De sheet waarvan de waarde uit gehaald moet worden</param>
        /// <param name="rowindex">De rij index voor waar de waarde zich bevind</param>
        /// <param name="colindex">De column index waarvan de waarde zich bevind</param>
        /// <returns>De waarde uit de opgegeven lokatie</returns>
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

        /// <summary>
        /// Verkrijg een opgetelde waarde binnen een specifieke tijd frame
        /// </summary>
        /// <param name="producties">De bewerkingen waarvan de waardes van opgeteld moeten worden</param>
        /// <param name="bereik">De tijd bereik voor het optellen van de waarde</param>
        /// <param name="type">De waarde naam dat opgeteld moet worden</param>
        /// <returns>Opgetelde waarde als een double afgerond naar 2 decimalen</returns>
        public static double GetSumValueRange(List<Bewerking> producties, TijdEntry bereik, string type)
        {
            if (producties.Count == 0) return 0;
            switch (type.ToLower())
            {
                case "tijd gewerkt(uur)":
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

        /// <summary>
        /// Verkrijg de producties als secties
        /// </summary>
        /// <param name="producties">De producties die verdeeld moeten worden als secties</param>
        /// <param name="bereik">De tijd bereik voor het indelen van de producties</param>
        /// <returns>Een dictionary met de bewerking naam als de key waarvan de waarde een lijst bevat met de bewerkingen die dezelfde naam hebben als de key</returns>
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

        /// <summary>
        /// Maak een Titel aan op een excel sheet
        /// </summary>
        /// <param name="sheet">De excel sheet waarvoor er een titel gemaakt moet worden</param>
        /// <param name="header">De titel</param>
        /// <param name="startrow">De start rij index waar de titel moet beginnen</param>
        /// <param name="rows">De aantal rijen die gebruikt worden voor de titel</param>
        /// <param name="startcell">De start cell index voor waar de titel moet beginnen</param>
        /// <param name="cells">De aantal cellen die ervoor gebruikt moeten worden</param>
        /// <param name="style">De style van de titel</param>
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

        /// <summary>
        /// Maak aan een nieuwe cell style
        /// </summary>
        /// <param name="workbook">De excel waarvan er een style gemaakt kan worden</param>
        /// <param name="isbold">Of de text dik gedrukt moet zijn</param>
        /// <param name="textalign">Waar de tekst lokatie zou moeten zijn</param>
        /// <param name="fontheight">De grootte van de tekst</param>
        /// <param name="fontcolor">Text kleur</param>
        /// <returns>De nieuw aangemaakte style met de opgegeven argumenten</returns>
        public static ICellStyle CreateStyle(XSSFWorkbook workbook, bool isbold, HorizontalAlignment textalign,
            int fontheight, short fontcolor)
        {
            var cellStyleBorder = GetBorderStyle(workbook);
            cellStyleBorder.Alignment = textalign;
            //cell header font
            var cellStylefont = workbook.CreateFont();
            cellStylefont.IsBold = isbold;
            cellStylefont.Color = fontcolor;
            cellStylefont.FontHeightInPoints = fontheight;
            cellStyleBorder.SetFont(cellStylefont);
            return cellStyleBorder;
        }

        ///// <summary>
        ///// Maak aan een sheet dat uitlegt wat de productie columns betekenen
        ///// </summary>
        ///// <param name="workbook">De excel waarvoor de sheet gemaakt moet worden</param>
        ///// <returns>De nieuw aangemaakte sheet</returns>
        //public static ISheet CreateProductieUitlegSheet(XSSFWorkbook workbook)
        //{
        //    try
        //    {
        //        var sheet = workbook.CreateSheet("Columns Uitleg");

        //        //border layaout
        //        var rowindex = 4;
        //        //CreateHeader(sheet, omschrijving, 0, 2, 0, ProductieColumns.Length, cellStyleBorder);
        //        //rowindex += 2;
        //        //create column font
               
        //        //init the columns and font
                
        //        foreach (var xrow in ProductieColumns)
        //        {
        //            var row = sheet.CreateRow(rowindex);
        //            var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12);
        //            CreateCell(row, 0, xrow + ": ", cellStyleBorder);
        //            cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Left, 12);
        //            CreateCell(row, 1, GetProductieColumnOmschrijving(xrow), cellStyleBorder);
        //            rowindex++;
        //        }
        //        sheet.AutoSizeColumn(0, true);
        //        sheet.AutoSizeColumn(1, true);
        //        return sheet;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public delegate bool IsRunningHandler(ProgressArg arg);

        /// <summary>
        /// Maak aan een niew productie sheet binnen een bepaald tijd periode
        /// </summary>
        /// <param name="workbook">De excel pagina waarvoor de sheet gemaakt moet worden</param>
        /// <param name="bereik">Het bereik voor de productie overzicht</param>
        /// <param name="naam"></param>
        /// <param name="producties"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static ISheet CreateProductieOverzicht(XSSFWorkbook workbook, TijdEntry bereik,
            string naam, List<Bewerking> producties, IsRunningHandler handler)
        {
            try
            {
                naam += $"({producties.Count})";
                var sheet = workbook.CreateSheet(naam);
                var xcols = Manager.Opties?.ExcelColumns.FirstOrDefault(x => x.IsSelected);
                var arg = new ProgressArg();
                arg.Type = ProgressType.WriteBussy;
                arg.Value = workbook;
                arg.Message = $"Excel bestand aanmaken...";
                if (xcols == null) return sheet;
                if (handler != null && !handler.Invoke(arg)) return sheet;
                //border layaout
                //var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Center, 18, IndexedColors.Black.Index);
                var rowindex = 0;
                var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12, IndexedColors.Black.Index);
                //init the columns and font
                
                var row = sheet.CreateRow(rowindex);
                
                var cellindex = 0;
               
                foreach (var xrow in xcols.Columns)
                {
                    arg.Message = $"Columns Aanmaken {cellindex}/{xcols.Columns.Count}...";
                    arg.Pogress = cellindex == 0 ? 0 : (int)((double)(cellindex / xcols.Columns.Count) * 100);
                    CreateCell(row, cellindex++, xrow.ColumnText, cellStyleBorder);
                    if (handler != null && !handler.Invoke(arg)) return sheet;
                }
               
                rowindex++;

                #region Populate Values

                //Populate fields
               

                var aantalpers = 0;
                double totaaltg = 0;
                //var allbws = producties.Select(x => x.Bewerkingen).ToArray();
                var personeel = new List<string>();
               
                string lastid = null;
                short rowcolor = IndexedColors.White.Index;
                int xcurindex = 0;
                foreach (var bw in producties)
                {
                    arg.Message = $"Rij Aanmaken voor '{bw.ProductieNr}'({xcurindex}/{producties.Count})...";
                    arg.Pogress = xcurindex == 0 ? 0 : (int)((double)(xcurindex / producties.Count) * 100);
                    if (handler != null && !handler.Invoke(arg)) break;
                    if (!string.Equals(lastid, bw.ArtikelNr, StringComparison.CurrentCultureIgnoreCase))
                    {
                        rowcolor = rowcolor == IndexedColors.LightGreen.Index ? IndexedColors.White.Index : IndexedColors.LightGreen.Index;
                        lastid = bw.ArtikelNr;
                    }
                    var tg = bereik == null ? bw.TijdAanGewerkt() : bw.TijdAanGewerkt(bereik.Start, bereik.Stop);
                    //if (tg <= 0) continue;
                    row = sheet.CreateRow(rowindex);
                    row.HeightInPoints = 15;
                    //Fill Green if Passing Score

                    //var formatting = CreateRowConditionalFormatRules(sheet, rowindex + 1, ProductieColumns.Length);
                    cellindex = 0;
                    cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Left, 11,HSSFColor.Black.Index);
                    cellStyleBorder.FillBackgroundColor =  rowcolor;
                    cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                    cellStyleBorder.FillForegroundColor = rowcolor;
                    bool defstyle = true;
                    foreach (var xcolmn in xcols.Columns)
                    {
                        if (handler != null && !handler.Invoke(arg)) break;
                        var value = GetValue(bw, xcolmn.Naam, bereik);
                        short xcolcolor = -1;
                        short xtxtcolor = -1;
                        if (xcolmn.ColorType != ColorRuleType.None)
                        {
                            
                            if (xcolmn.ColorType == ColorRuleType.Static)
                            {
                                xcolcolor = xcolmn.ColumnColorIndex;
                                xtxtcolor = xcolmn.ColumnTextColorIndex;
                            }
                            else if (xcolmn.ColorType == ColorRuleType.Dynamic)
                            {
                                foreach (var regel in xcolmn.KleurRegels)
                                {
                                    if (regel.Filter.ContainsFilter(bw))
                                    {
                                        if (regel.IsFontColor)
                                            xtxtcolor = regel.ColorIndex;
                                        else
                                            xcolcolor = regel.ColorIndex;
                                    }
                                }
                            }
                        }
                        if (xtxtcolor > -1 || xcolcolor > -1 || !string.IsNullOrEmpty(xcolmn.ColumnFormat))
                        {
                            cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Left, 11,
                                xtxtcolor == -1 ? HSSFColor.Black.Index : xtxtcolor);
                            cellStyleBorder.FillBackgroundColor = xcolcolor == -1 ? rowcolor : xcolcolor;
                            cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                            cellStyleBorder.FillForegroundColor = xcolcolor == -1 ? rowcolor : xcolcolor;
                            if (!string.IsNullOrEmpty(xcolmn.ColumnFormat))
                            {
                                IDataFormat dataFormatCustom = workbook.CreateDataFormat();
                                cellStyleBorder.DataFormat = dataFormatCustom.GetFormat(xcolmn.ColumnFormat);
                            }
                            defstyle = false;
                        }
                        else if (!defstyle)
                        {
                            cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Left, 11,
                                HSSFColor.Black.Index);
                            cellStyleBorder.FillBackgroundColor = rowcolor;
                            cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                            cellStyleBorder.FillForegroundColor = rowcolor;
                            cellStyleBorder.DataFormat = 0;
                            defstyle = true;
                        }
                        var cell = CreateCell(row, cellindex++, value, cellStyleBorder);
                       
                        //if (!string.IsNullOrEmpty(format))
                        //{
                        //    cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Left, 11);
                        //    cellStyleBorder.DataFormat = workbook.CreateDataFormat().GetFormat(format);
                        //}
                        //else cellStyleBorder.DataFormat = 0;


                    }
                    
                    var pers = bw.GetPersoneel().Select(x => x.PersoneelNaam).Where(x =>
                        personeel.All(t => !string.Equals(x, t, StringComparison.CurrentCultureIgnoreCase))).ToArray();
                    if (pers.Length > 0)
                        personeel.AddRange(pers);
                    aantalpers += pers.Length;

                    totaaltg += tg;
                    rowindex++;
                    xcurindex++;
                }

                //Laten we nu wat extra velden aanmaken om het totale te kunnen weergeven van sommige velden.
                //hier maken we de cel styles aan!
                cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12, IndexedColors.Black.Index);

                //hier gaan we de cells aanmaken.
                row = sheet.CreateRow(rowindex);
                CreateCell(row, 0, "Totaal", cellStyleBorder);
                xcurindex = 0;
                foreach (var xcol in xcols.Columns)
                {
                    if (xcol.Type == CalculationType.None) continue;
                    arg.Message = $"Berekening Column Aanmaken voor '{xcol.ColumnText}'({xcurindex}/{xcols.Columns.Count})...";
                    arg.Pogress = xcurindex == 0 ? 0 : (int)((double)(xcurindex / xcols.Columns.Count) * 100);
                    handler?.Invoke(arg);
                    //if (handler != null && !handler.Invoke()) return sheet;
                    cellindex = GetProductieColumnIndex(xcol.Naam);
                    var xss = GetColumnName(cellindex);
                    switch (xcol.Type)
                    {
                        case CalculationType.SOM:
                            CreateCellFormula(row, cellindex, $"SUM({xss}2:{xss}{rowindex})", cellStyleBorder);
                            break;
                        case CalculationType.Gemiddeld:
                            CreateCellFormula(row, cellindex, $"ROUND(SUM({xss}2:{xss}{rowindex}) / {rowindex - 1},0)",
                                cellStyleBorder);
                            break;
                    }
                }
                rowindex++;

                #endregion

                for (var i = 0; i < xcols.Columns.Count; i++)

                {
                    arg.Message = $"Breedte aanpassen voor Column '{xcols.Columns[i].ColumnText}'({i}/{xcols.Columns.Count})...";
                    arg.Pogress = xcurindex == 0 ? 0 : (int)((double)(i / xcols.Columns.Count) * 100);
                    //if (handler != null && !handler.Invoke()) return sheet;
                    handler?.Invoke(arg);
                    if (xcols.Columns[i].AutoSize)
                        sheet.AutoSizeColumn(i, true);
                    else
                    {
                        sheet.SetColumnWidth(i, xcols.Columns[i].ColumnBreedte * 256);
                    }
                    if (xcols.Columns[i].IsVerborgen)
                        sheet.SetColumnHidden(GetProductieColumnIndex(xcols.Columns[i].Naam), true);
                }
                return sheet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Maak aan een rij regel op basis van de productie status
        /// </summary>
        /// <param name="sheet">De productie sheet waar de regels voor moeten gelden</param>
        /// <param name="rowindex">De rij index waar de regel moet beginnen</param>
        /// <param name="cellcount">De aantal cellen waarvoor de regel moet gelden</param>
        /// <returns>De nieuw aangemaakte regel</returns>
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

        /// <summary>
        /// Maak aan een storing rij regel
        /// </summary>
        /// <param name="sheet">De storing sheet waarvoor de regel moet gelden</param>
        /// <param name="rowindex">De rij bereik waarvoor de regel moet gelden</param>
        /// <param name="cellcount">De aantal cellen waarvoor de regel moet gelden</param>
        /// <returns>De nieuw aangemaakte regel</returns>
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

        /// <summary>
        /// Verkrijg de kleur index op basis van de productie status
        /// </summary>
        /// <param name="state">De productie status</param>
        /// <returns>De excel kleur index</returns>
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

        /// <summary>
        /// Maak aan een nieuwe storing overzicht
        /// </summary>
        /// <param name="workbook">De excel waarvoor de overzicht gemaakt moet worden</param>
        /// <param name="storingen">De storingen waarvan de overzicht gemaakt moet worden</param>
        /// <param name="naam">De naam van de overzicht sheet</param>
        /// <param name="vanaf">De periode waarvan de overzicht gemaakt moet worden</param>
        /// <returns></returns>
        public static ISheet CreateStoringOverzicht(XSSFWorkbook workbook, List<Storing> storingen,
            string naam, TijdEntry vanaf)
        {
            try
            {
                if (storingen?.Count == 0)
                    return null;
                var sheet = workbook.CreateSheet(naam + $"({storingen.Count})");
                #region Sheet Titel Aanmaken

                //create header cells
                //var row = sheet.CreateRow(0);
                //for (var i = 0; i < StoringColumns.Length; i++)
                //    row.CreateCell(i);
                //row = sheet.CreateRow(1);
                //for (var i = 0; i < StoringColumns.Length; i++)
                //    row.CreateCell(i);

                //border layaout
                var cellStyleBorder = GetBorderStyle(workbook);
                //cellStyleBorder.Alignment = HorizontalAlignment.Center;
                ////cell header font
                var cellStylefont = workbook.CreateFont();
                //cellStylefont.IsBold = true;
                //cellStylefont.FontHeightInPoints = 18;
                //cellStyleBorder.SetFont(cellStylefont);

                //var cra = new CellRangeAddress(0, 1, 0, StoringColumns.Length - 1);
                //sheet.AddMergedRegion(cra);

                //var cell = sheet.GetRow(0).GetCell(0);
                //cell.CellStyle = cellStyleBorder;
                //cell.CellStyle.SetFont(cellStylefont);
                //cell.SetCellValue(omschrijving);

                #endregion

                #region Columns Aanmaken

                //create column font
                cellStyleBorder = GetBorderStyle(workbook);
                cellStylefont = workbook.CreateFont();
                cellStylefont.IsBold = true;
                cellStylefont.FontHeightInPoints = 12;
                cellStyleBorder.SetFont(cellStylefont);
                //init the columns and font
                var rowindex = 0;
                var row = sheet.CreateRow(rowindex);
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
                    CreateCellFormula(row, cellindex, $"SUM({xs}2:{xs}{rowindex})", cellStyleBorder);
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
            var xcol = Manager.Opties?.ExcelColumns?.FirstOrDefault(x => x.IsSelected);
            if (xcol == null) return -1;
            for (var i = 0; i < xcol.Columns.Count; i++)
                if (string.Equals(xcol.Columns[i].Naam, naam, StringComparison.CurrentCultureIgnoreCase))
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

        /// <summary>
        /// Maak aan een nieuwe cel op basis van de argumenten
        /// </summary>
        /// <param name="row">De rij waarin de nieuwe cell gemaakt moet worden</param>
        /// <param name="index">De cell index waar de cell zich moet bevinden</param>
        /// <param name="value">De waarde dat zich in de cell moet bevinden</param>
        /// <param name="style">De cell style</param>
        /// <returns>De nieuw aangemaakt cell</returns>
        public static ICell CreateCell(IRow row, int index, object value, ICellStyle style)
        {
            if (row == null || value == null) return null;
            var cell = row.CreateCell(index);
            if (value is DateTime time)
                cell.SetCellValue(time.ToString());
            else if (value is NotitieEntry xnote)
                cell.SetCellValue(xnote.Notitie);
            else if (value is VerpakkingInstructie xverp)
                cell.SetCellValue(xverp.VerpakkingType);
            else if (value is double xdouble)
                cell.SetCellValue(Math.Round(xdouble, 2));
            else if (value is decimal xdecimal)
                cell.SetCellValue((double)Math.Round(xdecimal,2));
            else if (value is int xint)
                cell.SetCellValue(xint);
            else if (value.GetType().IsEnum)
            {
                var xname = Enum.GetName(value.GetType(), value);
                cell.SetCellValue(xname);
            }
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

        /// <summary>
        /// Maak een een nieuwe cell aan met een formule
        /// </summary>
        /// <param name="row">De rij index waar de cell zich moet plaatsvinden</param>
        /// <param name="index">De index waar de cell zich moet bevinden</param>
        /// <param name="formula">De formule voor de aangemaakte cell</param>
        /// <param name="style">De style van de cell</param>
        /// <returns>De niewe aangemaakte cell met de aangegeven formule</returns>
        public static ICell CreateCellFormula(IRow row, int index, string formula, ICellStyle style)
        {
            if (row == null || string.IsNullOrEmpty(formula)) return null;
            var cell = row.CreateCell(index);
            cell.SetCellFormula(formula);
            cell.CellStyle = style;
            return cell;
        }

        /// <summary>
        /// Verkrijg de waarde van een storing op basis van de column naam
        /// </summary>
        /// <param name="storing">De storing waarvan de waarde verkregen moet worden</param>
        /// <param name="value">De column naam waarvoor de waarde verkregen moet worden</param>
        /// <param name="vanaf">de periode waarvan de waarde moet zijn</param>
        /// <returns>Een object als waarde van de storing</returns>
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

        /// <summary>
        /// Verkrijg de waarde van een bewerking op basis van de column naam
        /// </summary>
        /// <param name="bew">De bewerking waarvan de waarde verkregen moet worden</param>
        /// <param name="value">De column naam waarvan de waarde verkregen moet worden</param>
        /// <param name="vanaf">De periode waarvan de waarde moet zijn</param>
        /// <param name="format">Geeft een format terug als dat nodig is</param>
        /// <returns></returns>
        public static object GetValue(Bewerking bew, string value, TijdEntry vanaf)
        {
            if (bew == null || string.IsNullOrEmpty(value)) return null;
            var prop = bew.GetPropValue(value);
            if (prop == null)
            {
                return "N.V.T.";
            }

            switch (value.ToLower())
            {
                case "tijdgewerkt":
                    return vanaf == null ? bew.TijdAanGewerkt() : bew.TijdAanGewerkt(vanaf.Start, vanaf.Stop);
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
            return prop;
        }

        /// <summary>
        /// Maak aan een nieuwe overzicht
        /// </summary>
        /// <param name="bereik">De bereik waarvoor de overzicht moet zijn</param>
        /// <param name="bewerkingen">De bewerkingen waarvoor de overzicht gemaakt moet worden</param>
        /// <param name="createoverzicht">True voor als je een statistieken Sheet aan wilt maken</param>
        /// <param name="filepath">De excel bestand die aangemaakt moet worden</param>
        /// <param name="omschrijving">Omschrijving voor de excel</param>
        /// <returns>Een taak die een excel overzicht op de achtergrond maakt</returns>
        public static Task<string> CreateWeekOverzicht(TijdEntry bereik,
            List<Bewerking> bewerkingen, bool createoverzicht, string filepath, string omschrijving, IsRunningHandler handler)
        {
            return Task.Run(() =>
            {
                try
                {
                    var path = filepath;//$"{filepath}.xlsx";
                    var storingen = new List<Storing>();
                    //var bewerkingen = new List<Bewerking>();
                    var arg = new ProgressArg();
                    arg.Type = ProgressType.WriteBussy;
                   
                    for (var i = 0; i < bewerkingen.Count; i++)
                    {
                        arg.Message = $"Bewerkingen Verzamelen ({i}/{bewerkingen.Count})...";
                        arg.Pogress = i == 0 ? 0 : (int)(((double) (i / bewerkingen.Count)) * 100);
                        if (handler != null && !handler.Invoke(arg)) return null;
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
                        var sheet = CreateProductieOverzicht(workbook, bereik, sheetname, bewerkingen,handler);
                    }
                    arg.Message = $"Overzicht aanmaken...";
                    bool flag = handler != null && !handler.Invoke(arg);
                    if (!flag && storingen.Count > 0)
                    {
                        storingen = storingen.OrderBy(x => x.WerkPlek).ToList();
                        //Creeer een niew tapblad als er storingen zijn.
                        var sheetname = $"Onderbrekeningen";
                        var xomschrijving =
                            $"Onderbreking {omschrijving}";
                        CreateStoringOverzicht(workbook, storingen, sheetname,
                           bereik);
                    }

                    if (!flag && createoverzicht)
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

        public static bool CanMakeOverzicht(ProgressArg arg)
        {
            if (Manager.Opties == null) return false;
            return Manager.Opties.CreateWeekOverzichten;
        }

        /// <summary>
        /// Maak aan meerdere week overzichten
        /// </summary>
        /// <param name="overzichten">Een serie overzichten die gemaakt moeten worden</param>
        /// <param name="createoverzicht">True voor als je ook de statistieken wilt maken</param>
        /// <returns>Een taak die op de achtergrond excel bestanden aanmaakt</returns>
        public static Task<WeekOverzicht[]> CreateWeekOverzicht(WeekOverzicht[] overzichten, bool createoverzicht, IsRunningHandler handler)
        {
            return Task.Run(async () =>
            {
                var aangemaakt = new List<WeekOverzicht>();
                try
                {
                    if (Manager.Database == null || Manager.Database.IsDisposed || overzichten?.Length == 0)
                        return aangemaakt.ToArray();
                    var arg = new ProgressArg();
                    arg.Type = ProgressType.WriteBussy;
                    arg.Message = "Bewerkingen Verzamelen...";
                    if (handler != null && !handler.Invoke(arg)) return aangemaakt.ToArray();
                    var bewerkingen = await Manager.Database.GetAllBewerkingen(true,true);
                    arg.Message = "Locatie aanmaken...";
                    if (handler != null && !handler.Invoke(arg)) return aangemaakt.ToArray();
                        var rootpath = Manager.Opties.WeekOverzichtPath;
                    if (!Directory.Exists(rootpath))
                        Directory.CreateDirectory(rootpath);
                    if (bewerkingen.Count > 0)
                        if (overzichten != null)
                            foreach (var overzicht in overzichten)
                                try
                                {
                                    arg.Message = $"Overzicht '{overzicht.ToString()}' aanmaken...";
                                    if (handler != null && !handler.Invoke(arg)) break;
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
                                        arg.Message =
                                            $"Bewerkingen verzamelen voor '{overzicht.ToString()}'({i}/{bewerkingen.Count})...";
                                        arg.Pogress = i == 0 ? 0 : (int) (((double) (i / bewerkingen.Count)) * 100);
                                        if (handler != null && !handler.Invoke(arg)) break;
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

                                    var sheet = CreateProductieOverzicht(workbook, new TijdEntry(startweek, endweek, null), sheetname, xbws,handler);
                                    if (sheet == null) continue;
                                    arg.Message =
                                        $"Statistieken aanmaken...";
                                    bool flag = handler != null && !handler.Invoke(arg);
                                    if (!flag && storingen.Count > 0)
                                    {
                                        //Creeer een niew tapblad als er storingen zijn.
                                        sheetname = $"Week {weekNum} {year} Storingen";
                                        omschrijving = $"Week {weekNum} {year} {afdeling} Storing Overzicht";
                                        CreateStoringOverzicht(workbook, storingen, sheetname,new TijdEntry(startweek, endweek, null));
                                    }

                                    if (!flag && createoverzicht)
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

        /// <summary>
        /// Update de bestaande overzichten met nieuwe als die nog niet bestaan
        /// </summary>
        /// <param name="startweek">De startweek waarvoor de overzicht gemaakt moet worden</param>
        /// <param name="startjaar">De startjaar waarvan de overzicht gemaakt moet maken</param>
        /// <param name="bestaande">Een lijst met al bestaande overzichten</param>
        /// <param name="updateweeknr">De weeknr die geupdate moet worden</param>
        /// <returns></returns>
        public static Task<List<WeekOverzicht>> UpdateWeekOverzichten(int startweek, int startjaar,
            List<WeekOverzicht> bestaande, int updateweeknr, IsRunningHandler handler)
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
                        var newcreated = await ExcelWorkbook.CreateWeekOverzicht(tobecreated.ToArray(), true,handler);
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
        /// <summary>
        /// Maak een taak aan die alle week overzichten update op de achtergrond
        /// </summary>
        /// <returns>Een taak die week overzichten update op de achtergrond</returns>
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
                            opties.DoCurrentWeekOverzicht ? currentweek : 0, CanMakeOverzicht);
                        if (opties.VerzendAdres?.Count > 0)
                        {
                            foreach (var adres in opties.VerzendAdres)
                            {
                                if (!CanMakeOverzicht(null)) break;
                                if (!adres.SendWeekOverzichten || adres.VanafWeek < 1 || adres.VanafYear < 2021)
                                    continue;
                                if (adres.VerzondenWeekOverzichten == null)
                                    adres.VerzondenWeekOverzichten = new List<WeekOverzicht>();
                                overzichten =
                                    await UpdateWeekOverzichten(adres.VanafWeek, adres.VanafYear, overzichten, 0,CanMakeOverzicht);
                                
                                foreach (var overzicht in overzichten)
                                {
                                    if (!CanMakeOverzicht(null)) break;
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