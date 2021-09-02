using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BrightIdeasSoftware;
using LiveCharts;
using LiveCharts.Wpf;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Point = System.Drawing.Point;

namespace Controls
{
    public partial class ChartView : UserControl
    {
        private bool _isbussy;
        public List<Bewerking> _producties;

        public ChartView()
        {
            InitializeComponent();
            xdatachart.DataTooltip = new DefaultTooltip
            {
                SelectionMode = TooltipSelectionMode.OnlySender 
            };
            xserieslist.CheckStateGetter = GetCheckedState;
        }

        public bool PeriodeWeergave
        {
            get => xweergaveperiodegroup.Visible;
            set => xweergaveperiodegroup.Visible = value;
        }

        private CheckState GetCheckedState(object item)
        {
            if (item is LineSeries serie)
                return serie.Visibility == Visibility.Visible ? CheckState.Checked : CheckState.Unchecked;
            return CheckState.Unchecked;
        }

        private bool _IsWaiting;
        private void StartWaiting()
        {
            if (_IsWaiting) return;
            _IsWaiting = true;
            Task.Run(async () =>
            {
                int cur = 0;
                string xvalue = "Producties Laden.";
                this.BeginInvoke(new MethodInvoker(() => xstatus.Visible = true));
                while (_IsWaiting && !IsDisposed)
                {
                    try
                    {
                        int xcur = cur;
                        this.BeginInvoke(new MethodInvoker(() => xstatus.Text = xvalue.PadRight(xvalue.Length + xcur, '.')));
                        cur++;
                        if (cur > 5) cur = 0;
                        await Task.Delay(350);
                    }
                    catch (Exception e)
                    {
                    }
                   
                }
                this.BeginInvoke(new MethodInvoker(() => xstatus.Visible = false));
                _IsWaiting = false;
            });
        }

        private void StopWaiting()
        {
            _IsWaiting = false;
        }

        private Task UpdateTijdgewerktChart(bool iswerkplek, int startweek, int startjaar, string type, bool shownow)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (type == null || _isbussy) return;
                    StartWaiting();
                    _isbussy = true;
                    if (_producties == null)
                        _producties =
                            await Manager.GetBewerkingen(new[] {ViewState.Alles, ViewState.Gereed}, true);
                    var chartdata = _producties.CreateChartData(iswerkplek, startweek, startjaar, type, true, shownow);
                    
                    //xtijdgewerktlist.Columns?.Clear();
                    if (chartdata.Count > 0)
                    {
                     
                        Invoke(new Action(() =>
                        {
                            var werkplek = iswerkplek ? "Werkplek" : "Bewerking";
                            var xperiode = shownow ? "uur" :
                                chartdata.Any(t => t.Key.ToLower() == "maandag") ? "dagen" : "weken";
                            if (_producties == null)
                            {
                                xstatuslabel.Text =
                                    $@"{werkplek} {type} overzicht van de afgelopen {chartdata.Count} {xperiode}";
                            }
                            else
                            {
                                xstatuslabel.Text =
                                    $@"{werkplek} {type} overzicht van {_producties.Count} {(_producties.Count == 1 ? "bewerking" : "bewerkingen")}";
                            }
                        }));
                        var table = new DataTable(type);
                        table.Columns.Add("Periode");
                        var seriedata = new Dictionary<string, List<double>>();
                        foreach (var chart in chartdata)
                        {
                            var datarow = table.NewRow();
                            datarow["Periode"] = chart.Key;
                            foreach (var colmn in chart.Value)
                                if (table.Columns.Cast<DataColumn>().All(x =>
                                    !string.Equals(x.ColumnName, colmn.Key, StringComparison.CurrentCultureIgnoreCase)))
                                    table.Columns.Add(colmn.Key);
                        }

                        var rows = chartdata.Select(x => x.Key).ToArray();
                        var valuetype = "";
                        switch (type.ToLower())
                        {
                            case "storingen":
                            case "tijd gewerkt":
                                valuetype = "uur";
                                break;
                            case "aantal gemaakt":
                                valuetype = "stuks";
                                break;
                            case "per uur":
                                valuetype = "p/u";
                                break;
                        }

                        foreach (var rowname in rows)
                        {
                            var datarow = table.NewRow();
                            foreach (var col in table.Columns)
                            {
                                var colname = col.ToString();
                                object value = 0;
                                var chartd = chartdata[rowname];
                                if (colname == "Periode")
                                {
                                    value = rowname;
                                }
                                else
                                {
                                    if (chartd.ContainsKey(colname))
                                        value = chartd[colname];
                                    else value = 0d;
                                    if (!seriedata.ContainsKey(colname))
                                        seriedata.Add(colname, new List<double>());
                                    if (value is double xdoublevalue)
                                        seriedata[colname].Add(xdoublevalue);
                                    else seriedata[colname].Add(0);
                                    //value = $"{value} {valuetype}";
                                }

                                datarow[colname] = value;
                            }

                            table.Rows.Add(datarow);
                        }

                        xdatachart.Invoke(new Action(() =>
                        {
                            xdatachart.AxisX.Clear();
                            xdatachart.AxisY.Clear();
                            var xAxis = new Axis {Title = "Periode"};
                            xAxis.MinValue = 0;
                            xAxis.MinRange = 0;
                            xAxis.Labels = chartdata.Select(x => x.Key).ToList();
                            xdatachart.AxisX.Add(xAxis);
                            var yAxis = new Axis {Title = type + $"({valuetype})"};
                            yAxis.MinValue = 0;
                            yAxis.MinRange = 0;
                            xAxis.Labels = chartdata.Select(x => x.Key).ToList();
                            xdatachart.AxisY.Add(yAxis);
                        }));
                        var series = new List<LineSeries>();
                        bool selected = false;
                        xdatachart.Invoke(new Action(() =>
                        {
                            foreach (var data in seriedata)
                            {
                                var serie = new LineSeries
                                {
                                    Values = new ChartValues<double>(data.Value),
                                    Title = data.Key,
                                    StrokeThickness = 2,
                                    LineSmoothness = 1,
                                    Focusable = true
                                };
                                
                                serie.IsVisibleChanged += Serie_IsVisibleChanged;
                                series.Add(serie);
                               
                            }

                            xdatachart.Series.Clear();
                            xdatachart.Series.AddRange(series);
                            string[] checkedvalues = xserieslist.Objects?.Cast<LineSeries>()
                                .Where(x => x.Visibility == Visibility.Visible).Select(x => x.Title).ToArray();
                            foreach (var serie in series)
                            {
                                bool shouldselected = (checkedvalues != null && checkedvalues.Any(x =>
                                    string.Equals(x, serie.Title, StringComparison.CurrentCultureIgnoreCase)));
                                if (shouldselected)
                                {
                                    serie.Visibility = Visibility.Visible;

                                    selected = true;
                                }
                                else
                                {
                                    serie.Visibility = Visibility.Hidden;
                                }
                            }

                            if (!selected && series.Count > 0)
                                series[0].Visibility = Visibility.Visible;
                            //xdatachart.LegendLocation = LegendLocation.Bottom;
                        }));
                        //xdatagrid.Invoke(new Action(() => xdatagrid.DataSource = table));
                        xserieslist.Invoke(new Action(() => { xserieslist.SetObjects(series); }));
                    }
                }
                catch (Exception ex)
                {
                }

                StopWaiting();
                _isbussy = false;
            });
        }

        private void Serie_MouseEnter(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Source);
        }

        private void Serie_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var series = (Series) sender;

            if (series.Visibility == Visibility.Collapsed || series.Visibility == Visibility.Hidden)
                series.Erase(false);

            series.Model?.Chart.Updater.Run();
        }

        private string GetSelectedType()
        {
            if (xtijdgewerktradio.Checked) return "Tijd Gewerkt";
            if (xaantalgemaaktradio.Checked) return "Aantal Gemaakt";
            if (xaantalperuurradio.Checked) return "Per Uur";
            if (xstoringenradio.Checked) return "Storingen";
            return null;
        }

        private void xserieslist_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (((OLVListItem) e.Item).RowObject is LineSeries serie) TogleSerie(serie);
        }

        private void xserieslist_DoubleClick(object sender, EventArgs e)
        {
            if (xserieslist.SelectedObject is LineSeries serie) TogleSerie(serie);
        }

        private void TogleSerie(LineSeries serie)
        {
            try
            {
                serie.Visibility = serie.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
                xserieslist.RefreshObject(serie);
            }
            catch
            {
            }
        }

        private async void radiocheckchanged_CheckedChanged(object sender, EventArgs e)
        {
            await UpdateTijdgewerktChart(xwerkplekradio.Checked, (int) xstartweek.Value, (int) xstartjaar.Value,
                GetSelectedType(), xalleennucheckbox.Checked);
        }

        private async void xstartweek_ValueChanged(object sender, EventArgs e)
        {
            await UpdateTijdgewerktChart(xwerkplekradio.Checked, (int) xstartweek.Value, (int) xstartjaar.Value,
                GetSelectedType(), xalleennucheckbox.Checked);
        }

        public async void LoadData()
        {
            await UpdateTijdgewerktChart(xwerkplekradio.Checked, (int) xstartweek.Value, (int) xstartjaar.Value,
                GetSelectedType(), xalleennucheckbox.Checked);
        }

        private void xserieslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xserieslist.SelectedObject is LineSeries serie) serie.BringIntoView();
        }

        private async void xalleennucheckbox_CheckedChanged(object sender, EventArgs e)
        {
            xstartjaar.Enabled = !xalleennucheckbox.Checked;
            xstartweek.Enabled = !xalleennucheckbox.Checked;
            await UpdateTijdgewerktChart(xwerkplekradio.Checked, (int) xstartweek.Value, (int) xstartjaar.Value,
                GetSelectedType(), xalleennucheckbox.Checked);
        }
    }
}