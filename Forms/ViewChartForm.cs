using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class ViewChartForm : MetroFramework.Forms.MetroForm
    {
        public ViewChartForm()
        {
            InitializeComponent();
        }

        private void ViewChartForm_Load(object sender, EventArgs e)
        {
            chartView1.LoadData();
        }
    }
}