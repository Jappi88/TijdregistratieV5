using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using ProductieManager.Forms.Charting;
using Rpm.Productie;

namespace Forms.Charting
{
    public partial class WerkPlekChart : UserControl
    {
        public WerkPlekChart()
        {
            InitializeComponent();
            xChart.Series.Clear();
            xChart.AxisX.Clear();
        }

        private List<string> GetTijden()
        {
            var xret = new List<string>();
            var start = DateTime.Now.Date.AddHours(6);
            for (var i = 0; i < 13; i++) xret.Add(start.AddHours(i).ToString("t"));

            return xret;
        }

        public bool UpdateCharts(Bewerking bewerking)
        {
            try
            {
                if (bewerking?.WerkPlekken == null) return false;
                var xremove = xChart.Series.Where(x => !bewerking.WerkPlekken.Any(w =>
                    string.Equals(w.Naam, x.Title, StringComparison.CurrentCultureIgnoreCase))).ToList();
                xremove.ForEach(r => xChart.Series.Remove(r));
                var labels = new List<string>();
                xChart.LegendLocation = LegendLocation.Bottom;
                xChart.AxisX.Clear();
                foreach (var wp in bewerking.WerkPlekken)
                {
                    var serie = xChart.Series.FirstOrDefault(x =>
                        string.Equals(wp.Naam, x.Title, StringComparison.CurrentCultureIgnoreCase));
                    ChartAantalSerie aantals = null;
                    if (serie == null)
                    {
                        aantals = new ChartAantalSerie();
                        aantals.UpdateGemaakt(wp, ref labels);
                        xChart.Series.Add(new LineSeries
                        {
                            Title = wp.Naam,
                            Tag = aantals,
                            Values = aantals.Values
                        });
                    }
                    else if (serie is LineSeries {Tag: ChartAantalSerie aantal})
                    {
                        aantal.UpdateGemaakt(wp, ref labels);
                        aantals = aantal;
                    }

                    xChart.AxisX.Add(new Axis
                    {
                        Separator = new Separator
                        {
                            StrokeThickness = 1,
                            StrokeDashArray = new DoubleCollection(2),
                            Stroke = new SolidColorBrush(Color.FromRgb(64, 79, 86))
                        },
                        Title = wp.Naam,
                        Labels = labels
                        //Sections = new SectionsCollection().AddRange()
                    });
                    labels = new List<string>();
                }

                //var exis = xChart.AxisX.FirstOrDefault();
                //if (exis != null && labels != null)
                //    exis.Labels = labels;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}