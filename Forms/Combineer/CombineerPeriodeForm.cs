using System;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms.Combineer
{
    public partial class CombineerPeriodeForm : MetroBaseForm
    {
        public CombineerPeriodeForm()
        {
            InitializeComponent();
        }

        public CombineerPeriodeForm(TijdEntry periode) : this()
        {
            SelectedPeriode = periode;
            xstartdate.SetValue(periode.Start);
            xstopdate.SetValue(periode.Stop.IsDefault() ? DateTime.Now : periode.Stop);
            checkBox1.Checked = !periode.Stop.IsDefault();
        }

        public TijdEntry SelectedPeriode { get; set; } = new(DateTime.Now, default);

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            xstopdate.Enabled = checkBox1.Checked;
        }

        private void xok_Click(object sender, EventArgs e)
        {
            SelectedPeriode.Start = xstartdate.Value;
            SelectedPeriode.Stop =
                checkBox1.Checked ? xstopdate.Value.IsDefault() ? DateTime.Now : xstopdate.Value : default;
            DialogResult = DialogResult.OK;
        }

        private void xanuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}