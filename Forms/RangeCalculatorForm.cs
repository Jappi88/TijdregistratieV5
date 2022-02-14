using System.IO;
using System.Windows.Forms;
using Controls;
using Forms.MetroBase;
using Rpm.Misc;

namespace Forms
{
    public partial class RangeCalculatorForm : MetroBaseForm
    {
        public RangeCalculatorForm()
        {
            InitializeComponent();
        }

        public ZoekProductiesUI.RangeFilter Filter
        {
            get => zoekProductiesUI1.ShowFilter;
            set=> zoekProductiesUI1.ShowFilter = value;
        }

        private void zoekProductiesUI1_ClosedClicked(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void RangeCalculatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            zoekProductiesUI1.CloseUI();
        }

        private void RangeCalculatorForm_Shown(object sender, System.EventArgs e)
        {
            zoekProductiesUI1.InitUI();
            if (!Filter.IsDefault())
            {
                zoekProductiesUI1.SetFilter(Filter);
                _ = zoekProductiesUI1.Verwerk();
            }
        }

        private void zoekProductiesUI1_StatusTextChanged(object sender, System.EventArgs e)
        {
            this.Text = sender as string;
            this.Invalidate();
        }
    }
}
