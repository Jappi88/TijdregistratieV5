using System;
using System.Windows.Forms;
using Various;

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
            this.InitLastInfo();
            chartView1.LoadData();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SetLastInfo();
        }
    }
}