using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class AddExtraTijdForm : Forms.MetroBase.MetroBaseForm
    {
        private readonly TijdEntry _entry;

        public AddExtraTijdForm()
        {
            InitializeComponent();
            UpdateTijd();
            xaantaltype.Items.AddRange(Enum.GetNames(typeof(Periode)));
            xaantaltype.SelectedIndex = 0;
        }

        public AddExtraTijdForm(TijdEntry tijd) : this()
        {
            _entry = tijd;
            Init();
        }

        public TijdEntry ExtraTijd => CreateEntry();

        private void Init()
        {
            if (_entry?.ExtraTijd != null)
            {
                xstarttime.SetValue(_entry.Start);
                xstoptime.SetValue(_entry.Stop);
                xgebruikbereik.Checked = _entry.ExtraTijd.Herhaaldelijk;
                xvanafdate.SetValue(_entry.ExtraTijd.Vanaf);
                xtotdate.SetValue(_entry.ExtraTijd.Tot);
                xaantalkeer.Value = _entry.ExtraTijd.Aantalkeer;
                xaantaltype.SelectedItem = Enum.GetName(typeof(Periode), _entry.ExtraTijd.PeriodeSoort);
            }
        }

        private void xcancelb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xokb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private TijdEntry CreateEntry()
        {
            var vanaf = xvanafdate.Value;
            var tot = xtotdate.Value;
            var te = new TijdEntry(xstarttime.Value, xstoptime.Value, null);
            var per = Periode.Altijd;
            if (xaantaltype.SelectedItem != null)
                Enum.TryParse(xaantaltype.SelectedItem.ToString(), out per);
            te.ExtraTijd = new ExtraTijd
            {
                Start = xstarttime.Value,
                Stop = xstoptime.Value,
                Herhaaldelijk = xgebruikbereik.Checked,
                Aantalkeer = (int) xaantalkeer.Value,
                PeriodeSoort = per,
                Vanaf = vanaf,
                Tot = tot
            };

            return te;
        }

        private void xstarttime_ValueChanged(object sender, EventArgs e)
        {
            UpdateTijd();
        }

        private void UpdateTijd()
        {
            xaantaltijd.Text = $"{Tijd()} uur";
        }

        private double Tijd()
        {
            double tijd = 0;
            if (xstarttime.Value < xstoptime.Value)
                tijd = Math.Round((xstoptime.Value - xstarttime.Value).TotalHours, 2);
            return tijd;
        }

        private void xgebruikbereik_CheckedChanged(object sender, EventArgs e)
        {
            xbereikfield.Enabled = xgebruikbereik.Checked;
        }

        private void xstarttime_ValueChanged_1(object sender, EventArgs e)
        {
            UpdateTijd();
        }
    }
}