using System;
using System.Collections.Generic;
using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class ViewChartForm : MetroBaseForm
    {
        public ViewChartForm()
        {
            InitializeComponent();
        }

        public ViewChartForm(List<Bewerking> bewerkingen)
        {
            InitializeComponent();
            chartView1._producties = bewerkingen;
            chartView1.PeriodeWeergave = false;
        }

        private void ViewChartForm_Load(object sender, EventArgs e)
        {
            chartView1.LoadData();
        }
    }
}