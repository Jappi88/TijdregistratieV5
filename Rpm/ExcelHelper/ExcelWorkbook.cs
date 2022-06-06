using BrightIdeasSoftware;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rpm.ExcelHelper;
using BorderStyle = NPOI.SS.UserModel.BorderStyle;
using HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment;
using ICell = NPOI.SS.UserModel.ICell;

// ReSharper disable All

namespace ProductieManager.Rpm.ExcelHelper
{
    /// <summary>
    /// Een class voor het maken van excel sheets
    /// </summary>
    public static class ExcelWorkbook
    {

        /// <summary>
        /// De storing columns
        /// </summary>
        public static string[] StoringColumns =
        {
            "ArtikelNr", "ProductieNr",  "WerkPlek", "Storing Type", "Omschrijving", "Gemeld Door", "Gestart Op",
            "Gestopt Op", "Totaal Tijd", "Is Verholpen", "Verholpen Door", "Oplossing",
            "Productie Omschrijving"
        };

        public static string[] WerkPlekColumns =
        {
            "ProductieNr","ArtikelNr", "Status", "Naam", "Omschrijving", "Totaal Aantal","Totaal Gemaakt", "Actueel Gemaakt", "PDC P/u","Actueel P/u","Gemiddeld P/u","#Geproduceerd", "Gestart Op",
            "Gestopt Op", "Tijd Gewerkt","Tijd Ombouw","#Ombouw","Tijd Storingen", "#Storingen","Tijd Actief","Combinaties", "Tijd Schoonmaak","Personen"
        };

        


        /// <summary>
        /// Verborgen storing columns
        /// </summary>
        public static string[] HiddenStoringColumns =
        {
        };

        public static string[] HiddenWerkPlekColumns =
        {
            "Naam","Status", "Combinaties","#Geproduceerd", "Totaal Gemaakt"
        };

        public static object GetValue(WerkPlek plek, string value, TijdEntry bereik)
        {
            if (plek == null || string.IsNullOrEmpty(value)) return null;
            double xtijd = 0;
            bereik?.UpdateStartStop(plek.Tijden);
            switch (value.ToLower())
            {
                case "combinaties":
                    if (plek.Werk.Combies.Count == 0)
                        return "N.V.T.";
                    return string.Join(", ", plek.Werk.Combies.Select(x => $"{x.ProductieNr}({x.Activiteit})%"));
                case "pdc p/u":
                    return plek.PerUurBase;
                case "actueel p/u":
                    if (bereik == null)
                        return plek.PerUur;
                    return (int)plek.GetPerUur(bereik);
                case "gemiddeld p/u":
                    return (int)(plek.Werk?.GemiddeldActueelPerUur ?? plek.PerUur);
                case "#geproduceerd":
                    return plek.Werk?.Geproduceerd ?? 0;
                case "totaal aantal":
                    return plek.Werk.Aantal;
                case "actueel gemaakt":
                    return bereik == null
                        ? plek.ActueelAantalGemaakt
                        : plek.GetAantalGemaakt(bereik.Start, bereik.Stop, ref xtijd, false);
                case "totaal gemaakt":
                    return bereik == null
                        ? plek.Werk?.TotaalGemaakt ?? plek.AantalGemaakt
                        : plek.GetAantalGemaakt(DateTime.MinValue, bereik.Stop, ref xtijd, false);
                case "gestart op":
                    if (bereik.Start.Date == bereik.Stop.Date)
                        return plek.GestartOp(bereik).ToString("HH:mm");
                    return plek.GestartOp(bereik).ToString();
                case "gestopt op":
                    if (bereik.Start.Date == bereik.Stop.Date)
                        return plek.GestoptOp(bereik).ToString("HH:mm");
                    return plek.GestoptOp(bereik).ToString();
                case "tijd gewerkt":
                    return bereik == null
                        ? plek.TijdAanGewerkt()
                        : plek.TijdAanGewerkt(bereik.Start, bereik.Stop, true);
                case "tijd actief":
                    return bereik == null
                        ? plek.TijdAanGewerkt(false)
                        : plek.TijdAanGewerkt(bereik.Start, bereik.Stop, false);
                case "personen":
                    return plek.PersonenLijst;
                case "#storingen":
                    return plek.Storingen.Where(x =>
                        (bereik != null && x.GetTotaleTijd(bereik.Start, bereik.Stop) > 0) &&
                        !string.IsNullOrEmpty(x.StoringType) && !x.StoringType.ToLower().Contains("ombouw")).Count();
                case "#ombouw":
                    return plek.Storingen.Where(x =>
                        (bereik != null && x.GetTotaleTijd(bereik.Start, bereik.Stop) > 0) &&
                        !string.IsNullOrEmpty(x.StoringType) && x.StoringType.ToLower().Contains("ombouw")).Count();
                case "tijd storingen":
                    return plek.Storingen.Where(x =>
                        (bereik != null && x.GetTotaleTijd(bereik.Start, bereik.Stop) > 0) &&
                        !string.IsNullOrEmpty(x.StoringType) && !x.StoringType.ToLower().Contains("ombouw") && !x.StoringType.ToLower().Contains("schoonmaak")).Sum(x =>
                        bereik == null ? x.TotaalTijd : x.GetTotaleTijd(bereik.Start, bereik.Stop));
                case "tijd schoonmaak":
                    return plek.Storingen.Where(x =>
                        (bereik != null && x.GetTotaleTijd(bereik.Start, bereik.Stop) > 0) &&
                        !string.IsNullOrEmpty(x.StoringType) && x.StoringType.ToLower().Contains("schoonmaak")).Sum(x =>
                        bereik == null ? x.TotaalTijd : x.GetTotaleTijd(bereik.Start, bereik.Stop));
                case "tijd ombouw":
                    return plek.Storingen.Where(x =>
                        (bereik != null && x.GetTotaleTijd(bereik.Start, bereik.Stop) > 0) &&
                        !string.IsNullOrEmpty(x.StoringType) && x.StoringType.ToLower().Contains("ombouw")).Sum(x =>
                        bereik == null ? x.TotaalTijd : x.GetTotaleTijd(bereik.Start, bereik.Stop));
                case "status":
                    return Enum.GetName(typeof(ProductieState), plek.Werk.State);
            }

            var prop = plek.GetPropValue(value);
            if (prop == null)
            {
                return "N.V.T.";
            }

            return prop;
        }


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
                        CreateHeader(sheet, omschrijving, rowindex, 2, 0, columns.Count + 1, style);
                        rowindex += 2;
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
                    var row = sheet.GetRow(i)??sheet.CreateRow(i);
                    for (var j = startcell; j < startcell + cells; j++)
                        row.CreateCell(j);
                }

                var cra = new CellRangeAddress(startrow, startrow + rows - 1, startcell, startcell + cells - 1);
                sheet.AddMergedRegion(cra);
                int xwidth = 2;
                RegionUtil.SetBorderTop(xwidth, cra, sheet);
                RegionUtil.SetBorderBottom(xwidth, cra, sheet);
                RegionUtil.SetBorderLeft(xwidth, cra, sheet);
                RegionUtil.SetBorderRight(xwidth, cra, sheet);
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
        /// <param name="border"></param>
        /// <param name="color"></param>
        /// <returns>De nieuw aangemaakte style met de opgegeven argumenten</returns>
        public static ICellStyle CreateStyle(XSSFWorkbook workbook, bool isbold, HorizontalAlignment textalign,
            int fontheight, short fontcolor, BorderStyle border = BorderStyle.None, Color color = default)
        {
            var cellStyleBorder = GetBorderStyle(workbook);
            cellStyleBorder.Alignment = textalign;
            cellStyleBorder.WrapText = true;
            cellStyleBorder.VerticalAlignment = VerticalAlignment.Center;
            //cell header font
            var cellStylefont = workbook.CreateFont();
            cellStylefont.IsBold = isbold;
            cellStylefont.Color = fontcolor;
            cellStylefont.FontHeightInPoints = fontheight;
            cellStyleBorder.SetFont(cellStylefont);
            cellStyleBorder.BorderBottom = border;
            cellStyleBorder.BorderLeft = border;
            cellStyleBorder.BorderRight = border;
            cellStyleBorder.BorderTop = border;
            if (!color.IsDefault())
            {
                cellStyleBorder.FillForegroundColor = 0;
                cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                ((XSSFColor)cellStyleBorder.FillForegroundColorColor).SetRgb(new byte[] { color.R, color.G, color.B });
            }
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

        public static bool CreateColumns(XSSFWorkbook workbook, ISheet sheet,int startrow, int startcell, List<ExcelColumnEntry> columns)
        {
            try
            {
                var row = sheet.GetRow(startrow)??sheet.CreateRow(startrow);

                var cellindex = startcell;
                var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12, IndexedColors.Black.Index, BorderStyle.Medium);
                foreach (var xrow in columns)
                {
                    CreateCell(row, cellindex++, xrow.ColumnText, cellStyleBorder);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static bool CreateColumns(XSSFWorkbook workbook, ISheet sheet, int startrow, int startcell, string[] columns, Color color)
        {
            try
            {
                var row = sheet.GetRow(startrow) ?? sheet.CreateRow(startrow);

                var cellindex = startcell;
                var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Center, 12, IndexedColors.Black.Index, BorderStyle.Medium,color);
                cellStyleBorder.WrapText = false;
                //cellStyleBorder.FillBackgroundColor = color;
                //cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                //cellStyleBorder.FillForegroundColor = color;

                foreach (var xrow in columns)
                {
                    CreateCell(row, cellindex++, xrow, cellStyleBorder);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static bool FillColumnsData(XSSFWorkbook workbook, ISheet sheet, int startrow, int startcell, List<ExcelColumnEntry> columns, Bewerking bewerking, TijdEntry bereik)
        {
            try
            {
                var row = sheet.GetRow(startrow) ?? sheet.CreateRow(startrow);
                row.HeightInPoints = 30f;
                var cellindex = startcell;
                var cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Left, 11, IndexedColors.Black.Index, BorderStyle.Hair);
                bool defstyle = true;
                foreach (var xcolmn in columns)
                {
                    var value = GetValue(bewerking, xcolmn.Naam, bereik);
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
                                if (regel.Filter.ContainsFilter(bewerking))
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
                            xtxtcolor == -1 ? IndexedColors.Black.Index : xtxtcolor);
                        cellStyleBorder.FillBackgroundColor = xcolcolor == -1 ? IndexedColors.White.Index : xcolcolor;
                        cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                        cellStyleBorder.FillForegroundColor = xcolcolor == -1 ? IndexedColors.White.Index : xcolcolor;
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
                            IndexedColors.Black.Index);
                        cellStyleBorder.FillBackgroundColor = IndexedColors.White.Index;
                        cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                        cellStyleBorder.FillForegroundColor = IndexedColors.White.Index;
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
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private static ICellStyle GetPerUurStyle(XSSFWorkbook workbook, double baseperuur, int peruur, Color backcolor)
        {
            var xdiffer = Math.Round(baseperuur.GetPercentageDifference(peruur), 1);
            var xdfstring = xdiffer < 0 ? xdiffer.ToString() : "+" + xdiffer.ToString();
            //CreateCellConditionalFormatRules(sheet, startrow + 1, "pdc p/u", xcolmn);
            var txtcolor = IndexedColors.Black.Index;//xdiffer < 0 ? IndexedColors.Maroon.Index : IndexedColors.DarkGreen.Index;
            var xbackcolor = xdiffer < 0 ? Color.LightPink : Color.FromArgb(200, 255, 200);
            if (!backcolor.IsEmpty)
                xbackcolor = backcolor;
            var txtformatcolor = xdiffer < -10 ? "red" : xdiffer > 10? "color5" : "color10";

            IDataFormat dataFormatCustom = workbook.CreateDataFormat();
            var xstyle = CreateStyle(workbook, false, HorizontalAlignment.Center, 11, txtcolor, BorderStyle.Double, xbackcolor);
            xstyle.DataFormat = dataFormatCustom.GetFormat($"[{txtformatcolor}]####0\" ({xdfstring}%)\"");
            return xstyle;
        }

        private static ICellStyle GetMargeCellStyle(XSSFWorkbook workbook, double basevalue,double value, double marge,string customformat, Color backcolor)
        {
            var xdiffer = Math.Round(basevalue.GetPercentageDifference(value), 1);
            var xdfstring = xdiffer < 0 ? xdiffer.ToString() : "+" + xdiffer.ToString();
            //CreateCellConditionalFormatRules(sheet, startrow + 1, "pdc p/u", xcolmn);
            var txtcolor = IndexedColors.Black.Index;//xdiffer < 0 ? IndexedColors.Maroon.Index : IndexedColors.DarkGreen.Index;
            var xbackcolor = xdiffer < 0 ? Color.LightPink : Color.FromArgb(200, 255, 200);
            if (!backcolor.IsEmpty)
                xbackcolor = backcolor;
            var txtformatcolor = xdiffer < marge ? "red" : xdiffer > marge? "color5" : "color10";

            IDataFormat dataFormatCustom = workbook.CreateDataFormat();
            var xstyle = CreateStyle(workbook, false, HorizontalAlignment.Center, 11, txtcolor, BorderStyle.Double, xbackcolor);
            xstyle.DataFormat = dataFormatCustom.GetFormat($"{customformat}[{txtformatcolor}]####0\" ({xdfstring}%)\"");
            return xstyle;
        }


        public static bool FillColumnsData(XSSFWorkbook workbook, ISheet sheet, int startrow, int startcell, WerkPlek plek, TijdEntry bereik)
        {
            try
            {
                var row = sheet.GetRow(startrow) ?? sheet.CreateRow(startrow);
                row.HeightInPoints = 30f;
                var cellindex = startcell;
                var cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Center, 11, IndexedColors.Black.Index, BorderStyle.Double, IProductieBase.GetProductieStateColor(plek.Werk.State));
                foreach (var xcolmn in WerkPlekColumns)
                {
                    var value = GetValue(plek, xcolmn, bereik);
                    switch (xcolmn.ToLower())
                    {
                        case "actueel p/u":
                        case "gemiddeld p/u":
                            if (value is int xint)
                            {
                                var xstyle = GetPerUurStyle(workbook, plek.PerUurBase, xint, IProductieBase.GetProductieStateColor(plek.Werk.State));
                                CreateCell(row, cellindex++, value, xstyle);
                                continue;
                            }
                            break;
                    }
                    var cell = CreateCell(row, cellindex++, value, cellStyleBorder);
                    
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static bool CreateColumnsFormulas(XSSFWorkbook workbook, ISheet sheet, int startrow,int rows, int startcell, List<ExcelColumnEntry> columns)
        {
            try
            {
                var row = sheet.GetRow(startrow) ?? sheet.CreateRow(startrow);

                var cellindex = startcell;
                //Laten we nu wat extra velden aanmaken om het totale te kunnen weergeven van sommige velden.
                //hier maken we de cel styles aan!
                var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12, IndexedColors.Black.Index, BorderStyle.Medium);
                cellStyleBorder.FillBackgroundColor = IndexedColors.LightTurquoise.Index;
                cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                cellStyleBorder.FillForegroundColor = cellStyleBorder.FillBackgroundColor;
                CreateCell(row, 0, "Totaal", cellStyleBorder);
                var xcurindex = startcell;
                foreach (var xcol in columns)
                {
                    if (xcol.Type == CalculationType.None) continue;
                    //if (handler != null && !handler.Invoke()) return sheet;
                    cellindex = GetProductieColumnIndex(columns, xcol.Naam);
                    if (cellindex == -1) continue;
                    var xss = GetColumnName(cellindex);
                    switch (xcol.Type)
                    {
                        case CalculationType.SOM:
                            CreateCellFormula(row, cellindex, $"SUM({xss}{startrow - rows}:{xss}{startrow})", cellStyleBorder);
                            break;
                        case CalculationType.Gemiddeld:
                            CreateCellFormula(row, cellindex, $"ROUND(SUM({xss}{startrow - rows}:{xss}{startrow}) / {rows},0)",
                                cellStyleBorder);
                            break;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static bool CreateColumnsWerkplekFormulas(XSSFWorkbook workbook, ISheet sheet, int startrow, int rows, int startcell)
        {
            try
            {
                var row = sheet.GetRow(startrow) ?? sheet.CreateRow(startrow);

                var cellindex = startcell;
                //Laten we nu wat extra velden aanmaken om het totale te kunnen weergeven van sommige velden.
                //hier maken we de cel styles aan!
                var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Center, 12,
                    IndexedColors.White.Index, BorderStyle.Medium,IProductieBase.GetProductSoortColor("red"));
                //cellStyleBorder.FillBackgroundColor = IndexedColors.LemonChiffon.Index;
                //cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                //cellStyleBorder.FillForegroundColor = cellStyleBorder.FillBackgroundColor;
                CreateCell(row, 0, "Totaal", cellStyleBorder);
                var xcurindex = startcell;
                foreach (var xcol in WerkPlekColumns)
                {
                    cellindex = GetWerkplekColumnIndex(xcol);
                    if (cellindex == -1)
                        continue;
                    var xss = GetColumnName(cellindex);
                    switch (xcol.ToLower())
                    {
                        case "actueel gemaakt":
                        case "totaal aantal":
                        case "totaal gemaakt":
                        case "#storingen":
                        case "#ombouw":
                        case "tijd storingen":
                        case "tijd ombouw":
                        case "tijd schoonmaak":
                        case "tijd gewerkt":
                        case "tijd actief":
                            CreateCellFormula(row, cellindex, $"SUM({xss}{startrow - rows}:{xss}{startrow})", cellStyleBorder);
                            break;
                        case "pdc p/u":
                        case "actueel p/u":
                        case "gemiddeld p/u":
                            CreateCellFormula(row, cellindex, $"ROUND(SUM({xss}{startrow - rows}:{xss}{startrow}) / {rows},0)",
                                cellStyleBorder);
                            break;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

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
                var xcols = Manager.ListLayouts?.GetAlleLayouts()?.FirstOrDefault(x => x.IsExcelSettings && x.IsUsed("ExcelColumns"));
                var arg = new ProgressArg();
                arg.Type = ProgressType.WriteBussy;
                arg.Value = workbook;
                arg.Message = $"Excel bestand aanmaken...";
                if (xcols == null) return sheet;
                if (handler != null && !handler.Invoke(arg)) return sheet;
                //border layaout
                //var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Center, 18, IndexedColors.Black.Index);
                var rowindex = 0;
                var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12, IndexedColors.Black.Index, BorderStyle.Medium);
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
                    if (bereik != null && tg <= 0) continue;
                    row = sheet.CreateRow(rowindex);
                    row.HeightInPoints = 15;
                    //Fill Green if Passing Score

                    //var formatting = CreateRowConditionalFormatRules(sheet, rowindex + 1, ProductieColumns.Length);
                    cellindex = 0;
                    cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Left, 11,HSSFColor.Black.Index, BorderStyle.Double);
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
                                xtxtcolor == -1 ? HSSFColor.Black.Index : xtxtcolor, BorderStyle.Double);
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
                                HSSFColor.Black.Index, BorderStyle.Double);
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
                cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12, IndexedColors.Black.Index, BorderStyle.Medium);

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
                    cellindex = GetProductieColumnIndex(xcols.Columns, xcol.Naam);
                    if (cellindex == -1) continue;
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
                        sheet.SetColumnHidden(GetProductieColumnIndex(xcols.Columns, xcols.Columns[i].Naam), true);
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
                var cellindex = 0; //GetProductieColumnIndex("status");
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

        public static ISheetConditionalFormatting CreateCellConditionalFormatRules(ISheet sheet, int rowindex,
            string cellbasevalue, string cellvalue)
        {
            try
            {
                var formatting = sheet.SheetConditionalFormatting;
                List<IConditionalFormattingRule> rules = new List<IConditionalFormattingRule>();
                //basevalue
                var cellbaseindex = GetWerkplekColumnIndex(cellbasevalue);
                var xcolbasename = $"${GetColumnName(cellbaseindex)}${rowindex}";
                //value
                var cellindex = GetWerkplekColumnIndex(cellvalue);
                var xcolname = $"${GetColumnName(cellindex)}${rowindex}";

                var rule1 = formatting.CreateConditionalFormattingRule(
                    $"{xcolname} > {xcolbasename}");

                var rulepatern = rule1.CreatePatternFormatting();
                rulepatern.FillBackgroundColor = IndexedColors.LightGreen.Index;
                rulepatern.FillForegroundColor = IndexedColors.DarkGreen.Index;
                rulepatern.FillPattern = FillPattern.SolidForeground;
                rules.Add(rule1);
                var rule2 = formatting.CreateConditionalFormattingRule(
                    $"{xcolname} < {xcolbasename}");
                rulepatern = rule2.CreatePatternFormatting();
                rulepatern.FillBackgroundColor = IndexedColors.Yellow.Index;
                rulepatern.FillForegroundColor = IndexedColors.DarkRed.Index;
                rulepatern.FillPattern = FillPattern.SolidForeground;
                rules.Add(rule2);
                string range = $"{xcolname}:{xcolname}";
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
                cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Center, 12, IndexedColors.Black.Index, BorderStyle.Medium);
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
                cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Center, 10, IndexedColors.Black.Index, BorderStyle.Medium);
                foreach (var st in storingen)
                {
                    row = sheet.CreateRow(rowindex);
                    row.HeightInPoints = 20;
                    var formatting = CreateStoringRowConditionalFormatRules(sheet, rowindex + 1, StoringColumns.Length);
                    cellindex = 0;
                    foreach (var col in StoringColumns)
                        CreateCell(row, cellindex++, GetValue(st, col,vanaf,null), cellStyleBorder);

                    rowindex++;
                }

                #endregion

                #region Totale Rijen Aanmaken

                cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Center, 12, IndexedColors.Black.Index, BorderStyle.Medium);

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

        private static int GetProductieColumnIndex(List<ExcelColumnEntry> settings, string naam)
        {
            if (settings == null) return -1;
            for (var i = 0; i < settings.Count; i++)
                if (string.Equals(settings[i].Naam, naam, StringComparison.CurrentCultureIgnoreCase))
                    return i;
            return -1;
        }

        private static int GetWerkplekColumnIndex(string naam)
        {
            for (var i = 0; i < WerkPlekColumns.Length; i++)
                if (string.Equals(WerkPlekColumns[i], naam, StringComparison.CurrentCultureIgnoreCase))
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
        /// <param name="specialeroosters"></param>
        /// <returns>Een object als waarde van de storing</returns>
        public static object GetValue(Storing storing, string value, TijdEntry vanaf, List<Rooster> specialeroosters)
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
                    if (vanaf != null && vanaf.Start.Date == vanaf.Stop.Date)
                        return vanaf == null ? storing.Gestart.ToString() : storing.GestartOp(vanaf, specialeroosters).ToString("HH:mm");
                    return vanaf == null ? storing.Gestart.ToString() : storing.GestartOp(vanaf, specialeroosters).ToString();
                case "gestopt op":
                    var stop = (storing.IsVerholpen ? storing.Gestopt : DateTime.Now);
                    if (vanaf != null && vanaf.Start.Date == vanaf.Stop.Date)
                        return vanaf == null ? stop.ToString() : storing.GestoptOp(vanaf, specialeroosters).ToString("HH:mm");
                    return vanaf == null ? stop.ToString() : storing.GestoptOp(vanaf, specialeroosters).ToString();
                case "totaal tijd":
                    return vanaf == null? storing.GetTotaleTijd() : storing.GetTotaleTijd(vanaf.Start, vanaf.Stop);
                case "is verholpen":
                    return storing.IsVerholpen;
                case "verholpen door":
                    return storing.VerholpenDoor;
                case "oplossing":
                    return storing.Oplossing;
                case "artikelnr":
                    return storing.Plek?.ArtikelNr;
                case "productienr":
                    return storing.Plek?.ProductieNr;
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
            double xtijd = 0;
            switch (value.ToLower())
            {
                case "actueelaantalgemaakt":
                    return vanaf == null ? bew.ActueelAantalGemaakt : bew.GetAantalGemaakt(vanaf.Start, vanaf.Stop,ref xtijd, bew.State == ProductieState.Gestart);
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
            var prop = bew.GetPropValue(value);
            if (prop == null)
            {
                return "N.V.T.";
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
        /// <param name="handler"></param>
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
                    throw e;
                }
            });
        }

        public static Task<string> ExportToExcel(ObjectListView listview, string filepath, string sheetname,
            IsRunningHandler handler, List<CellMargeCheck> checkmarges)
        {
            return Task.Run(() =>
            {
                try
                {
                    var path = filepath; //$"{filepath}.xlsx";
                    //var bewerkingen = new List<Bewerking>();
                    var arg = new ProgressArg();
                    arg.Type = ProgressType.WriteBussy;

                    var workbook = new XSSFWorkbook();
                    arg.Message = $"Overzicht aanmaken...";
                    bool flag = handler != null && !handler.Invoke(arg);
                    if (!flag)
                    {
                        var sheet = workbook.CreateSheet(sheetname);
                        if (listview.Columns.Count > 0)
                        {
                            int rowindex = 0;
                            var row = sheet.GetRow(rowindex) ?? sheet.CreateRow(rowindex);
                            var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12,
                                IndexedColors.Black.Index, BorderStyle.Medium);
                            //init the columns and font
                            var cellindex = 0;
                            List<OLVColumn> columns = new List<OLVColumn>();
                            listview.Invoke(new MethodInvoker(() =>
                                columns = listview.Columns.Cast<OLVColumn>().ToList()));
                            foreach (var xcol in columns)
                            {

                                arg.Message = $"Columns Aanmaken {cellindex}/{columns.Count}...";
                                arg.Pogress = cellindex == 0 ? 0 : (int)((double)(cellindex / columns.Count) * 100);
                                CreateCell(row, cellindex++, xcol.Text, cellStyleBorder);
                                if (handler != null && !handler.Invoke(arg)) break;
                            }

                            rowindex++;
                            List<OLVListItem> items = new List<OLVListItem>();
                            listview.Invoke(
                                new MethodInvoker(() => items = listview.Items.Cast<OLVListItem>().ToList()));
                            cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Left, 11,
                                IndexedColors.Black.Index, BorderStyle.Double);
                            var style = cellStyleBorder;
                            string lastformat = String.Empty;
                            for (int i = 0; i < items.Count; i++)
                            {
                                var item = items[i];
                                row = sheet.GetRow(rowindex) ?? sheet.CreateRow(rowindex);
                               
                                for (int j = 0; j < columns.Count; j++)
                                {                                    
                                    var val = item.GetSubItem(j).ModelValue;
                                    if (val != null)
                                    {
                                        string format = columns[j].AspectToStringFormat;
                                        if (!string.IsNullOrEmpty(format))
                                        {
                                            var index = format.IndexOf("}");
                                            if (index > -1)
                                            {
                                                index++;
                                                format = format.Substring(index, format.Length - index);
                                            }
                                        }
                                        string f = "";
                                        if (val is decimal dec)
                                            if (dec > 0)
                                            {
                                                if ((dec % 1) == 0)
                                                    f = "#,##0";
                                                else
                                                    f = "#,##0.##";
                                            }
                                            else f = "0";
                                        else if (val is double d)
                                        {
                                            if (d > 0)
                                            {
                                                if ((d % 1) == 0)
                                                    f = "#,##0";
                                                else
                                                    f = "#,##0.##";
                                            }
                                            else f = "0";
                                        }
                                        else if (val is int v)
                                        {
                                            if (v > 0)
                                                f = "#,###";
                                            else f = "0";
                                        }
                                        format = $"{f}\"{format}\"";
                                        bool changed = false;
                                        if (checkmarges != null && checkmarges.Count > 0)
                                        {
                                            var marge = checkmarges.FirstOrDefault(x =>
                                                x.Equals(columns[j].AspectName));
                                            if (marge != null)
                                            {
                                                var basevalue = (item.RowObject.GetType()
                                                    .GetProperty(marge.BasevalueName)?.GetValue(item.RowObject) ?? -1);
                                                var value = (item.RowObject.GetType()
                                                    .GetProperty(marge.ColumnName)?.GetValue(item.RowObject) ?? -1);
                                                if (double.TryParse(basevalue.ToString(), out var baseval) &&
                                                    double.TryParse(value.ToString(), out var xval))
                                                {
                                                    style = GetMargeCellStyle(workbook, baseval,
                                                        xval, marge.Marge, format, Color.White);
                                                    changed = true;
                                                }

                                            }
                                            else style = cellStyleBorder;
                                        }

                                        if (!changed && !string.IsNullOrEmpty(format))
                                        {
                                            if (!string.Equals(lastformat, format,
                                                    StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                style = CreateStyle(workbook, false, HorizontalAlignment.Left, 11,
                                                    IndexedColors.Black.Index, BorderStyle.Double);
                                                IDataFormat dataFormatCustom = workbook.CreateDataFormat();
                                                style.DataFormat = dataFormatCustom.GetFormat(format);
                                                lastformat = format;
                                            }
                                        }
                                        else if (!changed)
                                        {
                                            style = cellStyleBorder;
                                            lastformat = String.Empty;
                                        }

                                        CreateCell(row, j, val, style);
                                    }
                                }

                                rowindex++;
                            }

                            for (var i = 0; i < columns.Count; i++)
                                sheet.AutoSizeColumn(i, true);
                        }
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
                    throw e;
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
        /// <param name="handler"></param>
        /// <returns>Een taak die op de achtergrond excel bestanden aanmaakt</returns>
        public static Task<WeekOverzicht[]> CreateWeekOverzicht(WeekOverzicht[] overzichten, IsRunningHandler handler)
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
                        var rootpath = Path.Combine(Manager.Opties.WeekOverzichtPath, Manager.Opties.Username);
                        if (!Directory.Exists(rootpath))
                            Directory.CreateDirectory(rootpath);
                        if (overzichten != null)
                            foreach (var overzicht in overzichten)
                                try
                                {
                                    arg.Message = $"Overzicht '{overzicht.ToString()}' aanmaken...";
                                    if (handler != null && !handler.Invoke(arg)) break;
                                    var weekNum = overzicht.WeekNr;
                                    var year = overzicht.Jaar;
                                    var afdeling = overzicht.Afdeling ?? "Hele";
                                    var sheetname = overzicht.ToString();
                                    var path = $"{rootpath}\\{sheetname}.xlsx";
                                    if (await CreateDagelijksProductieOverzicht(overzicht.WeekNr, overzicht.Jaar, path,
                                            handler,
                                            null))
                                        aangemaakt.Add(overzicht);
                                }
                                catch (Exception e)
                                {
                                   Console.WriteLine(e.Message);
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
                        var newcreated = await CreateWeekOverzicht(tobecreated.ToArray(),handler);
                        if (newcreated?.Length > 0) bestaande.AddRange(newcreated);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                if (Manager.Opties == null) return 0;
               
                try
                {
                    var path = Path.Combine(Manager.Opties.WeekOverzichtPath, Manager.Opties.Username);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    var opties = Manager.Opties;
                    if (!_issendingweek)
                    {
                        _issendingweek = true;

                        var files = Directory.GetFiles(path);
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
                                    var fpath = $"{opties.WeekOverzichtPath}\\{overzicht.ToString()}.xlsx";
                                    if (File.Exists(path))
                                    {
                                        var omschrijving =
                                            $"Week {overzicht.WeekNr} {overzicht.Jaar} {overzicht.Afdeling} overzicht";
                                        var msg = CreateOverzichtMailBody(overzicht);
                                        if (RemoteProductie.SendEmail(adres.Adres, omschrijving, msg,
                                            new List<string>() { fpath }, false,false))
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
                               $"U bent aangemeld voor een wekelijks overzicht van de 'ProductieManager' voor '{overzicht.Afdeling}'.\n" +
                               "Als u deze mails niet wenst te ontvangen, dan kunt u dat aangeven bij Ihab zodat hij u kan afmelden.\n\n" +
                               $"Dit overzicht is voor week {overzicht.WeekNr} {overzicht.Jaar} en geld alleen voor '{overzicht.Afdeling}'.\n\n" +
                             
                               $"Dit overzicht is hooguit een beeld van hoe de 'ProductieManager' is bijgehouden in week {overzicht.WeekNr}.\n" +
                               "Hoe beter het wordt bijgehouden, hoe nauwkeuriger het Excel overzicht.\n\n" +
                               "Mocht er vragen zijn tot betrekking van dit mail, of het weekoverzicht dat kunt u dat aangeven bij Ihab.\n" +
                               "Veel leesplezier!";

            return omschrijving;
        }

        public static string CleanSheetName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            //string illegal = "\"M\"\\a/ry/ h**ad:>> a\\/:*?\"| li*tt|le|| la\"mb.?[]";
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                name = name.Replace(c.ToString(), "_").Replace("[", "(").Replace("]", ")");
            }

            return name;
        }

        public static short ColorToIndex(System.Drawing.Color color)
        {
            return new XSSFColor(new byte[]{color.R, color.G, color.B}).Index;
        }

        public static bool InserImage(XSSFWorkbook workbook,ISheet sheet, Image image, int colindex, int rowindex)
        {
            try
            {
                if (sheet == null || workbook == null) return false;
                int pictureIndex = workbook.AddPicture(image.ToByteArray(), PictureType.JPEG);
                ICreationHelper helper = workbook.GetCreationHelper();
                IDrawing drawing = sheet.CreateDrawingPatriarch();
                IClientAnchor anchor = helper.CreateClientAnchor();
                anchor.Col1 = colindex;//0 index based column
                anchor.Row1 = rowindex;//0 index based row
                IPicture picture = drawing.CreatePicture(anchor, pictureIndex);
                picture.Resize();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static Task<bool> CreateDagelijksProductieOverzicht(int weeknr, int jaar, string filepath, IsRunningHandler handler, List<Filter> filters)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    var arg = new ProgressArg();
                    arg.Type = ProgressType.WriteBussy;
                    var xafdeling = Manager.Opties?.Username;
                   // var xrooster = Manager.Opties?.GetWerkRooster();
                    if (string.IsNullOrEmpty(xafdeling)) return false;
                    //eerst de dagen pakken waarvan we een overzicht willen maken.
                    //eerste werkdag/tijd
                    var xstartweek = Functions.DateOfWeek(jaar, DayOfWeek.Monday, weeknr);
                    //eerste werkdag/tijd
                    var xeidnweek = Functions.DateOfWeek(jaar, DayOfWeek.Sunday, weeknr);
                    //xstartweek = xstartweek.Add(xrooster.StartWerkdag);
                    //laatste werkdag/tijd
                    //xeidnweek = xeidnweek.Add(xrooster.EindWerkdag);
                    //we verkrijgen alle producties
                    var weekbereik = new TijdEntry(xstartweek, xeidnweek);
                    arg.Message = $"Producties Verzamelen...";
                    if (handler != null && !handler.Invoke(arg)) return false;
                    var producties = Manager.Database
                        .xGetBewerkingen(ViewState.Alles, true, null, null,false);
                    if (filters != null && filters.Count > 0)
                        producties = producties.Where(x => filters.Any(f => f.IsAllowed(x,"ExcelWeekOverzicht"))).ToList();
                    arg.Message = $"Geen producties gevonden!";
                    if (handler != null && !handler.Invoke(arg)) return false;
                    if (producties.Count == 0) return false;
                    var xwerkplekken = new Dictionary<string, List<WerkPlek>>();
                    List<Storing> storingen = new List<Storing>();
                    foreach (var prod in producties)
                    {
                        arg.Message = $"Laden '{prod.ProductieNr}'...";
                        if (handler != null && !handler.Invoke(arg)) return false;
                        foreach (var wp in prod.WerkPlekken)
                        {
                            if (!wp.HeeftGewerkt(weekbereik))
                                continue;
                            string clean = CleanSheetName(wp.Naam?.Trim())?.ToLower();
                            if (string.IsNullOrEmpty(clean)) continue;
                            if (xwerkplekken.ContainsKey(clean))
                            {
                                if (xwerkplekken[clean].Any(x => x.Equals(wp))) continue;
                                xwerkplekken[clean].Add(wp);

                            }
                            else
                                xwerkplekken.Add(clean, new List<WerkPlek>() { wp });
                            if (wp.Storingen.Count > 0)
                                storingen.AddRange(wp.Storingen);
                        }
                    }

                    if (xwerkplekken.Count == 0) return false;
                    xwerkplekken = xwerkplekken.OrderBy(x => x.Key)
                        .ToDictionary(s => s.Key, s => s.Value);

                    arg.Message = $"Excel voor week {weeknr} {jaar} aanmaken...";
                    if (handler != null && !handler.Invoke(arg)) return false;

                    var workbook = new XSSFWorkbook();
                    foreach (var wp in xwerkplekken)
                    {
                        if (wp.Value == null || wp.Value.Count == 0) continue;
                        var x2 = wp.Value.Count == 1 ? "productie" : "producties";
                        arg.Message = $"Sheet aanmaken voor '{wp.Key}'...";
                        if (handler != null && !handler.Invoke(arg)) return false;
                        var xdagspan = WerkPlekColumns.Length;
                        ISheet sheet = null;
                        var xstyle = CreateStyle(workbook, true, HorizontalAlignment.Center, 28,
                            IndexedColors.Black.Index, BorderStyle.None);
                        var rowindex = 5;
                        int cur = 0;
                        int xstartCell = 0;
                        int currow = rowindex;
                        var xcurdag = xstartweek;
                       
                        while (xcurdag.Date <= xeidnweek.Date)
                        {
                            arg.Message = $"Sheet aanmaken voor '{wp.Key}'...";
                            if (handler != null && !handler.Invoke(arg)) return false;
                            //var dag = Enum.GetName(typeof(DayOfWeek), xcurdag.DayOfWeek);
                            var xenddag = new DateTime(xcurdag.Year, xcurdag.Month, xcurdag.Day,
                                0, 0, 0);
                            var xbereik = new TijdEntry(xcurdag, xenddag);

                            var xbws = wp.Value.OrderBy(x => x.TijdGestart).ToList();
                            xbws = xbws.Where(x => x.TijdAanGewerkt(xbereik.Start, xbereik.Stop, false,false) > 0)
                                .ToList();
                            if (xbws.Count > 0)
                            {
                                if (sheet == null)
                                {
                                    sheet = workbook.CreateSheet(CleanSheetName(wp.Value.First().Naam));
                                    CreateHeader(sheet,
                                        $"{xafdeling.FirstCharToUpper()} Tijd Registratie Voor '{wp.Key.FirstCharToUpper()}' In Week {weeknr} {jaar}",
                                        0, rowindex, 0, xdagspan, xstyle);
                                   
                                }

                                xstyle = CreateStyle(workbook, true, HorizontalAlignment.Center, 20,
                                    IndexedColors.White.Index, BorderStyle.None, IProductieBase.GetProductSoortColor("horti"));
                                //xstyle.FillBackgroundColor = IndexedColors.LightYellow.Index;
                                //xstyle.FillPattern = FillPattern.SolidForeground;
                                //xstyle.FillForegroundColor = IndexedColors.LightYellow.Index;
                                CreateHeader(sheet, xcurdag.ToString("D").FirstCharToUpper(), currow, 2, xstartCell,
                                    xdagspan, xstyle);
                                currow += 2;
                                //write producties
                                int added = 0;

                                if (CreateColumns(workbook, sheet, currow, xstartCell, WerkPlekColumns,
                                        Color.FromArgb(217, 225, 242)))
                                {
                                    currow++;
                                    foreach (var bw in xbws)
                                    {
                                        if (FillColumnsData(workbook, sheet, currow, xstartCell, bw,
                                                new TijdEntry(xcurdag, xenddag)))
                                        {
                                            currow++;
                                            cur++;
                                            added++;
                                        }
                                    }

                                    if (CreateColumnsWerkplekFormulas(workbook, sheet, currow, added, xstartCell))
                                        currow++;

                                }
                            }

                            xcurdag = xcurdag.AddDays(1);
                            rowindex = currow;
                        }

                        var bereik = new TijdEntry(xstartweek, xeidnweek);
                        var bws = wp.Value.Where(x => x.TijdAanGewerkt(bereik.Start, bereik.Stop, false, false) > 0).OrderBy(x => x.TijdGestart)
                            .ToList();
                        var x1 = bws.Count == 1 ? "Productie" : "Producties";
                        if (bws.Count > 0)
                        {
                            arg.Message = $"Sheet aanmaken voor '{wp.Key}'...";
                            if (handler != null && !handler.Invoke(arg)) return false;
                            if (sheet == null)
                            {
                                xstyle = CreateStyle(workbook, true, HorizontalAlignment.Center, 28,
                                    IndexedColors.Black.Index, BorderStyle.None);
                                sheet = workbook.CreateSheet(CleanSheetName(wp.Value.First().Naam));
                                CreateHeader(sheet,
                                    $"{xafdeling.FirstCharToUpper()} Tijd Registratie Voor '{wp.Key.FirstCharToUpper()}' In Week {weeknr} {jaar}",
                                    0, 5, 0, xdagspan, xstyle);
                            }

                            xstyle = (XSSFCellStyle)CreateStyle(workbook, true, HorizontalAlignment.Center, 24,
                                IndexedColors.White.Index, BorderStyle.None, IProductieBase.GetProductSoortColor("horti"));
                            //xstyle.FillBackgroundColor = IndexedColors.LightOrange.Index;
                            //xstyle.FillPattern = FillPattern.SolidForeground;
                            //xstyle.FillForegroundColor = IndexedColors.LightOrange.Index;
                            
                            CreateHeader(sheet,
                                $"Totaal {bws.Count} {x1} Voor '{wp.Key.FirstCharToUpper()}' In Week {weeknr} {jaar}",
                                currow, 3, xstartCell, xdagspan, xstyle);
                            currow += 3;

                            //write producties
                            int added = 0;
                            arg.Message = $"Sheet aanmaken voor '{wp.Key}'...";
                            if (handler != null && !handler.Invoke(arg)) return false;
                            if (CreateColumns(workbook, sheet, currow, xstartCell, WerkPlekColumns,
                                    Color.FromArgb(217, 225, 242)))
                            {
                                currow++;
                                foreach (var bw in bws)
                                {
                                    arg.Message = $"Sheet aanmaken voor '{wp.Key}'...";
                                    if (handler != null && !handler.Invoke(arg)) return false;
                                    if (FillColumnsData(workbook, sheet, currow, xstartCell, bw,
                                            bereik))
                                    {
                                        currow++;
                                        cur++;
                                        added++;
                                    }
                                }

                                if (CreateColumnsWerkplekFormulas(workbook, sheet, currow, added, xstartCell))
                                    currow++;

                            }
                        }

                        if (sheet != null)
                        {
                            for (var i = 0; i < WerkPlekColumns.Length; i++)
                            {
                                switch (i)
                                {
                                    case 9:
                                    case 10:
                                        sheet.SetColumnWidth(i,200 * 24);
                                        break;
                                    default:
                                        sheet.AutoSizeColumn(i);
                                        break;
                                }
                                
                            }
                            InserImage(workbook, sheet, (Image)Resources.logo_vanderValk, xdagspan - 2, 1);
                            for (var i = 0; i < HiddenWerkPlekColumns.Length; i++)
                            {
                                var xindex = GetWerkplekColumnIndex(HiddenWerkPlekColumns[i]);
                                sheet.SetColumnHidden(xindex, true);
                            }
                            sheet = null;
                        }

                        xcurdag = xstartweek;
                        currow = 5;
                        //storingen
                        if (storingen.Count > 0)
                        {
                            xdagspan = StoringColumns.Length;
                            ISheet storingsheet = null;
                            var xweekstoringen = storingen.Where(x =>
                                string.Equals(x.WerkPlek, wp.Key, StringComparison.CurrentCultureIgnoreCase) &&
                                x.GetTotaleTijd(weekbereik.Start, weekbereik.Stop) > 0).OrderBy(x=> x.Gestart).ToList();
                            while (xcurdag.Date <= xeidnweek.Date)
                            {

                                arg.Message = $"Sheet aanmaken voor '{wp.Key}'...";
                                if (handler != null && !handler.Invoke(arg)) return false;
                                //var dag = Enum.GetName(typeof(DayOfWeek), xcurdag.DayOfWeek);
                                var xenddag = new DateTime(xcurdag.Year, xcurdag.Month, xcurdag.Day,
                                    0, 0, 0);
                                var xbereik = new TijdEntry(xcurdag, xenddag);
                                var xstoringen = xweekstoringen.Where(x =>
                                        x.GetTotaleTijd(xbereik.Start, xbereik.Stop) > 0)
                                    .ToList();
                                if (xstoringen.Count > 0)
                                {
                                    x1 = xstoringen.Count == 1 ? "storing" : "storingen";
                                    if (storingsheet == null)
                                    {
                                        storingsheet = workbook.CreateSheet(CleanSheetName(wp.Value.First().Naam) +
                                                                            $" Storingen({xweekstoringen.Count})");
                                        xstyle = CreateStyle(workbook, true, HorizontalAlignment.Center, 28,
                                            IndexedColors.Black.Index, BorderStyle.None);
                                        CreateHeader(storingsheet,
                                            $"{xafdeling.FirstCharToUpper()} Storingen van '{wp.Key.FirstCharToUpper()}' In Week {weeknr} {jaar}",
                                            0, 5, 0, xdagspan, xstyle);
                                    }

                                    xstyle = CreateStyle(workbook, true, HorizontalAlignment.Center, 20,
                                        IndexedColors.White.Index, BorderStyle.None, IProductieBase.GetProductSoortColor("horti"));
                                    //xstyle.FillBackgroundColor = IndexedColors.LemonChiffon.Index;
                                    //xstyle.FillPattern = FillPattern.SolidForeground;
                                    //xstyle.FillForegroundColor = IndexedColors.LemonChiffon.Index;
                                    CreateHeader(storingsheet, $"{xstoringen.Count} {x1} op {xcurdag.ToString("D")}",
                                        currow, 2,
                                        xstartCell, xdagspan, xstyle);
                                    currow += 2;
                                    if (CreateColumns(workbook, storingsheet, currow, xstartCell, StoringColumns,
                                            Color.FromArgb(217, 225, 242)))
                                    {
                                        currow++;
                                        rowindex = currow;
                                        var xststyle = CreateStyle(workbook, false, HorizontalAlignment.Left, 11,
                                            IndexedColors.Black.Index, BorderStyle.Double);
                                        IRow row = null;
                                        int cellindex = 0;
                                        foreach (var st in xstoringen)
                                        {
                                            arg.Message = $"Sheet aanmaken voor '{wp.Key}'...";
                                            if (handler != null && !handler.Invoke(arg)) return false;
                                            row = storingsheet.CreateRow(currow);
                                            row.HeightInPoints = 30;
                                            var formatting =
                                                CreateStoringRowConditionalFormatRules(storingsheet, currow + 1,
                                                    StoringColumns.Length);
                                            cellindex = xstartCell;
                                            var xstplek = wp.Value.FirstOrDefault(x =>
                                                string.Equals(x.Path, st.Path, StringComparison.CurrentCultureIgnoreCase));
                                            foreach (var col in StoringColumns)
                                                CreateCell(row, cellindex++, GetValue(st, col, xbereik, xstplek?.Tijden?.SpecialeRoosters), xststyle);

                                            currow++;
                                        }

                                        var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12,
                                            IndexedColors.White.Index, BorderStyle.Medium, IProductieBase.GetProductSoortColor("red"));
                                        //cellStyleBorder.FillBackgroundColor = IndexedColors.LightTurquoise.Index;
                                        //cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                                        //cellStyleBorder.FillForegroundColor = cellStyleBorder.FillBackgroundColor;
                                        row = storingsheet.CreateRow(currow);
                                        CreateCell(row, 0, "Totaal", cellStyleBorder);
                                        //totaal tijd gewerkt.
                                        cellindex = GetStoringColumnIndex("totaal tijd");
                                        if (cellindex > -1)
                                        {
                                            var xs = GetColumnName(cellindex);
                                            CreateCellFormula(row, cellindex, $"SUM({xs}{rowindex}:{xs}{currow})",
                                                cellStyleBorder);
                                        }

                                        currow++;
                                    }

                                }
                                arg.Message = $"Sheet aanmaken voor '{wp.Key}'...";
                                if (handler != null && !handler.Invoke(arg)) return false;
                                xcurdag = xcurdag.AddDays(1);
                                rowindex = currow;
                            }
                            if (xweekstoringen.Count > 0)
                            {
                                arg.Message = $"Sheet aanmaken voor '{wp.Key}'...";
                                if (handler != null && !handler.Invoke(arg)) return false;
                                if (storingsheet == null)
                                {
                                    storingsheet = workbook.CreateSheet(CleanSheetName(wp.Value.First().Naam) +
                                                                        $" Storingen({xweekstoringen.Count})");
                                    xstyle = CreateStyle(workbook, true, HorizontalAlignment.Center, 28,
                                        IndexedColors.Black.Index, BorderStyle.None);
                                    CreateHeader(storingsheet,
                                        $"{xafdeling.FirstCharToUpper()} Storingen van '{wp.Key.FirstCharToUpper()}' In Week {weeknr} {jaar}",
                                        0, 5, 0, xdagspan, xstyle);
                                }

                                xstyle = CreateStyle(workbook, true, HorizontalAlignment.Center, 24,
                                    IndexedColors.White.Index, BorderStyle.None, IProductieBase.GetProductSoortColor("horti"));
                                x1 = xweekstoringen.Count == 1 ? "Storing" : "Storingen";
                                CreateHeader(storingsheet, $"Totaal {xweekstoringen.Count} {x1} van '{wp.Key.FirstCharToUpper()}' In Week {weeknr} {jaar}",
                                    currow, 3,
                                    xstartCell, xdagspan, xstyle);
                                currow += 3;
                                if (CreateColumns(workbook, storingsheet, currow, xstartCell, StoringColumns,
                                        Color.FromArgb(217,225,242)))
                                {
                                    currow++;
                                    rowindex = currow;
                                    var xststyle = CreateStyle(workbook, false, HorizontalAlignment.Left, 11,
                                        IndexedColors.Black.Index, BorderStyle.Double);
                                    IRow row = null;
                                    int cellindex = 0;
                                    foreach (var st in xweekstoringen)
                                    {
                                        arg.Message = $"Sheet aanmaken voor '{wp.Key}'...";
                                        if (handler != null && !handler.Invoke(arg)) return false;
                                        row = storingsheet.CreateRow(currow);
                                        row.HeightInPoints = 30;
                                        var formatting =
                                            CreateStoringRowConditionalFormatRules(storingsheet, currow + 1,
                                                StoringColumns.Length);
                                        cellindex = xstartCell;
                                        var xstplek = wp.Value.FirstOrDefault(x =>
                                            string.Equals(x.Path, st.Path, StringComparison.CurrentCultureIgnoreCase));
                                        foreach (var col in StoringColumns)
                                            CreateCell(row, cellindex++, GetValue(st, col, bereik,xstplek?.Tijden.SpecialeRoosters), xststyle);

                                        currow++;
                                    }

                                    var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12,
                                        IndexedColors.White.Index, BorderStyle.Medium, IProductieBase.GetProductSoortColor("red"));
                                    //cellStyleBorder.FillBackgroundColor = IndexedColors.LightTurquoise.Index;
                                    //cellStyleBorder.FillPattern = FillPattern.SolidForeground;
                                    //cellStyleBorder.FillForegroundColor = cellStyleBorder.FillBackgroundColor;
                                    row = storingsheet.CreateRow(currow);
                                    CreateCell(row, 0, "Totaal", cellStyleBorder);
                                    //totaal tijd gewerkt.
                                    cellindex = GetStoringColumnIndex("totaal tijd");
                                    if (cellindex > -1)
                                    {
                                        var xs = GetColumnName(cellindex);
                                        CreateCellFormula(row, cellindex, $"SUM({xs}{rowindex}:{xs}{currow})",
                                            cellStyleBorder);
                                    }

                                    currow++;
                                }

                            }
                            if (storingsheet == null) continue;
                            for (var i = 0; i < StoringColumns.Length; i++)
                                storingsheet.AutoSizeColumn(i);
                            InserImage(workbook, storingsheet, (Image)Resources.logo_vanderValk, xdagspan - 1, 1);
                            storingsheet = null;
                        }
                    }
                    arg.Message = $"Excel Aangemaakt!";
                    arg.Type = ProgressType.WriteCompleet;
                    if (handler != null && !handler.Invoke(arg)) return false;
                    if (File.Exists(filepath))
                        File.Delete(filepath);
                    using (var fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write))
                    {
                        workbook.Write(fs);
                    }

                    workbook.Close();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });

        }

        public static string CreateArtikelNr(string[] artikels, string filepath, string sheetname)
        {
            try
            {
                var xartikels = new List<string>();
                if (artikels.Length > 0)
                    xartikels = artikels.OrderBy(x => x).ToList();

                var xsection = new Dictionary<string, List<string>>();

                while (xartikels.Count > 0)
                {
                    var xfirst = xartikels[0];
                    if (string.IsNullOrEmpty(xfirst) || xfirst.Length < 2)
                    {
                        xartikels.Remove(xfirst);
                        continue;
                    }

                    var xstart = xfirst.Substring(0, 2);
                    var xrt = xartikels.Where(x => x.ToLower().StartsWith(xstart.ToLower())).ToList();
                    xrt.ForEach(x=> xartikels.Remove(x));
                    if (xsection.ContainsKey(xstart))
                        xsection[xstart].AddRange(xrt);
                    else xsection.Add(xstart, xrt);
                }

                if (xsection.Count > 0)
                {
                    var workbook = new XSSFWorkbook();
                    var sheet = workbook.CreateSheet($"{sheetname}({artikels.Length})");
                    var rowindex = 0;
                    var cellStyleBorder = CreateStyle(workbook, true, HorizontalAlignment.Left, 12, IndexedColors.Black.Index);
                    //init the columns and font

                    var row = sheet.CreateRow(rowindex);
                    var cellindex = 0;
                    foreach (var xsec in xsection)
                    {
                        CreateCell(row, cellindex++, xsec.Key + $".XX..({xsec.Value.Count})", cellStyleBorder);
                    }
                    rowindex++;
                    cellStyleBorder = CreateStyle(workbook, false, HorizontalAlignment.Left, 11, IndexedColors.Black.Index);
                    cellindex = 0;
                    int xrowscreated = 0;
                    foreach (var xsec in xsection)
                    {
                        var xrows = rowindex;
                        foreach (var xart in xsec.Value)
                        {
                            row = sheet.GetRow(xrows);
                            if (row == null)
                            {
                                row = sheet.CreateRow(xrows);
                            }

                            xrows++;
                            CreateCell(row, cellindex, xart, cellStyleBorder);
                        }

                        if (xrows > xrowscreated)
                            xrowscreated = xrows;
                        cellindex++;
                    }
                    rowindex += xrowscreated;
                    for (var i = 0; i < xsection.Count; i++)
                        sheet.AutoSizeColumn(i, true);
                    using (var fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        workbook.Write(fs);
                    }

                    workbook.Close();
                }
                else return null;
                return filepath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}