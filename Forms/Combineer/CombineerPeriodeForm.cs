using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Windows.Forms;

namespace Forms.Combineer
{
    public partial class CombineerPeriodeForm : Forms.MetroBase.MetroBaseForm
    {
        public TijdEntry SelectedPeriode { get; set; } = new TijdEntry(DateTime.Now, default);
        public CombineerPeriodeForm()
        {
            InitializeComponent();
        }

        public CombineerPeriodeForm(TijdEntry periode):this()
        {
            SelectedPeriode = periode;
            xstartdate.SetValue(periode.Start);
            xstopdate.SetValue(periode.Stop.IsDefault() ? DateTime.Now : periode.Stop);
            checkBox1.Checked = !periode.Stop.IsDefault();
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            xstopdate.Enabled = checkBox1.Checked;
        }

        private void xok_Click(object sender, EventArgs e)
        {
            SelectedPeriode.Start = xstartdate.Value;
            SelectedPeriode.Stop = checkBox1.Checked ? xstopdate.Value.IsDefault()? DateTime.Now : xstopdate.Value : default;
            DialogResult = DialogResult.OK;
        }

        private void xanuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
