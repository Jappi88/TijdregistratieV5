using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Misc;

namespace Forms.Sporen
{
    public partial class OptimaleVerbruikSettingForm : MetroBaseForm
    {
        public OptimaleVerbruikSettingForm()
        {
            InitializeComponent();
        }

        public OptimaleVerbruikInfo SelectedInfo { get; set; }

        private void xok_Click(object sender, System.EventArgs e)
        {
            SelectedInfo = new OptimaleVerbruikInfo()
            {
                Reststuk = xreststuk.Value,
                Voorkeur1 = xvoorkeur1Lengte.Value,
                Voorkeur2 = xvoorkeur2Lengte.Value,
                Voorkeur3 = xvoorkeur3Lengte.Value,
            };
        }

        private void SetInfo(OptimaleVerbruikInfo info)
        {
            xreststuk.SetValue(info.Reststuk);
            xvoorkeur1Lengte.SetValue(info.Voorkeur1);
            xvoorkeur2Lengte.SetValue(info.Voorkeur2);
            xvoorkeur3Lengte.SetValue(info.Voorkeur3);
        }

        private void OptimaleVerbruikSettingForm_Shown(object sender, System.EventArgs e)
        {
            if (SelectedInfo != null)
            {
                SetInfo(SelectedInfo);
            }
        }

        private void xvoorkeur1Lengte_Enter(object sender, System.EventArgs e)
        {
            if(sender is NumericUpDown nr)
                nr.Select(0,nr.Text.Length);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var e = new KeyEventArgs(keyData);
            if (e.KeyCode == Keys.Enter)
            {
                if (xvoorkeur1Lengte.Value > 0 && xreststuk.Value > 0)
                {
                    xok.PerformClick();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
