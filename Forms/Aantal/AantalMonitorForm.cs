using Rpm.Productie;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class AantalMonitorForm : Forms.MetroBase.MetroBaseForm
    {
        public Bewerking Werk { get; private set; }
        //public SeriesCollection Series
        //{
        //    get => xChart.Series;
        //    set => xChart.Series = value;
        //}

        public AantalMonitorForm()
        {
            InitializeComponent();
        }

        public AantalMonitorForm(Bewerking bew) : this()
        {
            //InitBewerking(bew);
        }

        //private IChartValues GetChartValues(WerkPlek wp)
        //{
        //    var xret = new ChartValues<ObservableValue>();
        //    try
        //    {
        //        if (wp.AantalHistory == null || wp.AantalHistory.Aantallen.Count == 0) return xret;
        //        foreach (var xa in wp.AantalHistory.Aantallen)
        //        {
        //            var obs = new ObservableValue(xa.Aantal);
        //            obs.PropertyChanged += Obs_PropertyChanged;
        //        }
        //        xret.AddRange(wp.AantalHistory.Aantallen.Select(x => x.Aantal));
        //        xret.Add(wp.GetActueelAantalGemaakt());
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }

        //    return xret;
        //}

        private void Obs_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           
        }

        //public void InitBewerking(Bewerking bew)
        //{
        //    Werk = bew;
        //    if (bew == null) return;

          
        //    var xAxis = new Axis
        //    {
        //        Title = "Periode",
        //        MinValue = 0,
        //        MinRange = 0,
        //    };
          
        //    var yAxis = new Axis
        //    {
        //        Title = $"Aantal",
        //        MinValue = 0,
        //        MinRange = 0
        //    };
        //    xAxis.Labels =
        //        xChart.AxisX.FirstOrDefault(x =>
        //            string.Equals(x.Title, "periode", StringComparison.CurrentCultureIgnoreCase))?.Labels ??
        //        new List<string>();
        //    yAxis.Labels =
        //        xChart.AxisY.FirstOrDefault(x =>
        //            string.Equals(x.Title, "aantal", StringComparison.CurrentCultureIgnoreCase))?.Labels ??
        //        new List<string>();
        //    foreach (var wp in bew.WerkPlekken)
        //    {
        //        var xold = Series.Cast<LineSeries>().FirstOrDefault(x => x.Tag is WerkPlek xwp && wp.Equals(xwp));
        //        foreach (var xdt in wp.AantalHistory.Aantallen)
        //        {
        //            var xdate = xdt.DateChanged.ToString("M/d/yy HH:mm");
        //            if (xAxis.Labels.Any(X => string.Equals(xdate, X, StringComparison.CurrentCultureIgnoreCase)))
        //                continue;
        //            xAxis.Labels.Add(xdate);
        //        }
        //        foreach (var xdt in wp.AantalHistory.Aantallen)
        //        {
        //            var xdate = xdt.Aantal.ToString();
        //            if (yAxis.Labels.Any(X => string.Equals(xdate, X, StringComparison.CurrentCultureIgnoreCase)))
        //                continue;
        //            yAxis.Labels.Add(xdate);
        //        }
        //        var xserie = xold ?? new LineSeries();
        //        xserie.Title = wp.Naam;
        //        xserie.Tag = wp;
        //        xserie.Values = GetChartValues(wp);
        //        if (xold == null)
        //            Series.Add(xserie);
        //    }
        //    xChart.AxisX.Clear();
        //    xChart.AxisY.Clear();
        //    xChart.AxisX.Add(xAxis);
        //    xChart.AxisY.Add(yAxis);
        //    xChart.Update(false,false);
        //    xChart.Invalidate();
        //}

        //private void UpdateCharts()
        //{
        //    if (Werk == null) return;
        //    try
        //    {
                
        //        foreach (var wp in Werk.WerkPlekken)
        //        {
        //            var serie = Series.FirstOrDefault(x =>
        //                x is LineSeries { Tag: WerkPlek xwp } && wp.Equals(xwp));
        //            if (serie != null)
        //                serie.Values = GetChartValues(wp);
        //        }

        //        xChart.Update(false, true);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}

        private void UpdateForm(ProductieFormulier form)
        {
            if (Werk == null || form == null || IsDisposed || Disposing) return;
            try
            {
                var bw = form.Bewerkingen?.FirstOrDefault(x => x.Equals(Werk));
                if (bw == null) return;
                Werk = bw;
                //if (xChart.InvokeRequired)
                //    xChart.Invoke(new MethodInvoker(UpdateCharts));
                //else
                //    UpdateCharts();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AantalMonitorForm_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            if (Werk == null || string.IsNullOrEmpty(id) || IsDisposed || Disposing ||
                string.Equals(Werk.ProductieNr, id, StringComparison.CurrentCultureIgnoreCase)) return;
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(Close));
                else
                    this.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
           UpdateForm(changedform);
        }

        private void AantalMonitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
        }
    }
}
