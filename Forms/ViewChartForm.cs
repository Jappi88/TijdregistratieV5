using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            try
            {
                chartView1.LoadData();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void ViewChartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                chartView1.CloseUI();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }
    }
}