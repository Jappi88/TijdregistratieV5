using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms;
using Rpm.Productie;
using Various;

namespace Forms
{
    public partial class ViewChartForm : Forms.MetroBase.MetroBaseForm
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