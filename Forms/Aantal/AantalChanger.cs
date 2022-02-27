using Forms.MetroBase;
using Rpm.Productie;
using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class AantalChanger : MetroBaseForm
    {
        public AantalChanger()
        {
            InitializeComponent();
            
        }

        public int Aantal
        {
            get => (int) xaantal.Value;
            set => xaantal.Value = value > xaantal.Maximum ? xaantal.Maximum : value < 0 ? 0 : value;
        }

        public DialogResult ShowDialog(int aantal, string msg)
        {
            Aantal = aantal;
            Text = msg;
            xtotaal.Text = $"/ {aantal}";
            return ShowDialog();
        }

        public DialogResult ShowDialog(WerkPlek wp)
        {
            if (wp?.Werk == null)
                return DialogResult.Cancel;
            Aantal = wp.AantalGemaakt;
            Text = $"Wijzig Aantal: [{wp.Path}] | {wp.Naam}";
            xinfolabel.Text = $"Vul het aantal in dat gemaakt is op {wp.Naam}.";
            xtotaal.Text = $"/ {wp.Werk.Aantal}";
            return ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xaantal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter) DialogResult = DialogResult.OK;
        }

        private void AantalChanger_Shown(object sender, EventArgs e)
        {
            xaantal.Select();
            xaantal.Select(0, xaantal.Value.ToString().Length);
            xaantal.Focus();
        }
    }
}