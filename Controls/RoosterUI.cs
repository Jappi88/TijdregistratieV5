using System;
using System.Windows.Forms;
using Forms;
using Rpm.Misc;
using Rpm.Productie;

namespace Controls
{
    public partial class RoosterUI : UserControl
    {
        private Rooster _rooster;

        public RoosterUI()
        {
            InitializeComponent();
        }

        public Rooster WerkRooster
        {
            get => _rooster;
            set => SetRooster(value);
        }

        private void SetRooster(Rooster rooster)
        {
            _rooster = rooster ?? Manager.Opties?.WerkRooster?.CreateCopy();
            if (_rooster != null)
            {
                xstartwerkdag.Value = Convert.ToDateTime(_rooster.StartWerkdag.ToString());
                xeindwerkdag.Value = Convert.ToDateTime(_rooster.EindWerkdag.ToString());

                xstartpauze1.Value = Convert.ToDateTime(_rooster.StartPauze1.ToString());
                xstartpauze2.Value = Convert.ToDateTime(_rooster.StartPauze2.ToString());
                xstartpauze3.Value = Convert.ToDateTime(_rooster.StartPauze3.ToString());

                xduurpauze1.Value = Convert.ToDateTime(_rooster.DuurPauze1.ToString());
                xduurpauze2.Value = Convert.ToDateTime(_rooster.DuurPauze2.ToString());
                xduurpauze3.Value = Convert.ToDateTime(_rooster.DuurPauze3.ToString());

                xgebruiktpauze.Checked = _rooster.GebruiktPauze;
            }
        }

        private Rooster GetRooster()
        {
            return new Rooster
            {
                StartWerkdag = xstartwerkdag.Value.TimeOfDay,
                EindWerkdag = xeindwerkdag.Value.TimeOfDay,
                StartPauze1 = xstartpauze1.Value.TimeOfDay,
                StartPauze2 = xstartpauze2.Value.TimeOfDay,
                StartPauze3 = xstartpauze3.Value.TimeOfDay,
                DuurPauze1 = xduurpauze1.Value.TimeOfDay,
                DuurPauze2 = xduurpauze2.Value.TimeOfDay,
                DuurPauze3 = xduurpauze3.Value.TimeOfDay,
                GebruiktPauze = xgebruiktpauze.Checked
            };
        }

        private void xgebruiktpauze_CheckedChanged(object sender, EventArgs e)
        {
            xpauzetijdengroup.Enabled = xgebruiktpauze.Checked;
            _rooster.GebruiktPauze = xgebruiktpauze.Checked;
            OnValueChanged(sender);
        }

        private void xstartwerkdag_ValueChanged(object sender, EventArgs e)
        {
            if (sender is DateTimePicker nrc)
            {
                var max = new TimeSpan(1, 0, 0);
                switch (nrc.Name)
                {
                    case "xstartwerkdag":
                        _rooster.StartWerkdag = xstartwerkdag.Value.TimeOfDay;
                        break;
                    case "xeindwerkdag":
                        _rooster.EindWerkdag = xeindwerkdag.Value.TimeOfDay;
                        break;
                    case "xstartpauze1":
                        _rooster.StartPauze1 = xstartpauze1.Value.TimeOfDay;
                        break;
                    case "xstartpauze2":
                        _rooster.StartPauze2 = xstartpauze2.Value.TimeOfDay;
                        break;
                    case "xstartpauze3":
                        _rooster.StartPauze3 = xstartpauze3.Value.TimeOfDay;
                        break;
                    case "xduurpauze1":
                        if (nrc.Value.TimeOfDay >= max)
                            nrc.Value = nrc.Value.Date + max;
                        _rooster.DuurPauze1 = xduurpauze1.Value.TimeOfDay;
                        break;
                    case "xduurpauze2":
                        if (nrc.Value.TimeOfDay >= max)
                            nrc.Value = nrc.Value.Date + max;
                        _rooster.DuurPauze2 = xduurpauze2.Value.TimeOfDay;
                        break;
                    case "xduurpauze3":
                        if (nrc.Value.TimeOfDay >= max)
                            nrc.Value = nrc.Value.Date + max;
                        _rooster.DuurPauze3 = xduurpauze3.Value.TimeOfDay;
                        break;
                }

                OnValueChanged(sender);
            }
        }

        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged(object sender)
        {
            ValueChanged?.Invoke(sender, EventArgs.Empty);
        }

        private void xstandaard_Click(object sender, EventArgs e)
        {
            if (XMessageBox.Show("Weetje zeker dat je een standaard rooster wilt?", "Standaard Rooster",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                SetRooster(Rooster.StandaartRooster());
        }
    }
}