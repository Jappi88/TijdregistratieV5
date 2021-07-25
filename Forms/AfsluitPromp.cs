using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class AfsluitPromp : MetroFramework.Forms.MetroForm
    {
        private readonly Timer _timer;
        private readonly double max = 30.0;
        private double current;

        public AfsluitPromp()
        {
            InitializeComponent();
            VerlengTijd = new TimeSpan();
            _timer = new Timer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = 100;
            Shown += AfsluitPromp_Shown;
            comboBox1.SelectedIndex = 0;
        }

        public TimeSpan VerlengTijd { get; set; }

        private void _timer_Tick(object sender, EventArgs e)
        {
            current += 0.10;
            var remaining = Math.Round(max - current, 1);
            xstatus.Text = $"Pc word afgeloten over {remaining} seconden...";

            if (current > max)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                progressBar1.Value = (int) (current / max * 100);
                progressBar1.Invalidate();
                Application.DoEvents();
            }
        }

        private void AfsluitPromp_Shown(object sender, EventArgs e)
        {
            _timer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: //5min
                    VerlengTijd = new TimeSpan(0, 5, 0);
                    break;

                case 1:
                    VerlengTijd = new TimeSpan(0, 10, 0);
                    break;

                case 2:
                    VerlengTijd = new TimeSpan(0, 15, 0);
                    break;

                case 3:
                    VerlengTijd = new TimeSpan(0, 30, 0);
                    break;

                case 4:
                    VerlengTijd = new TimeSpan(1, 0, 0);
                    break;

                default:
                    VerlengTijd = new TimeSpan();
                    break;
            }

            if (comboBox1.SelectedIndex > -1)
            {
                current = 0;
                DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            current = 0;
            _timer.Stop();
            DialogResult = DialogResult.OK;
        }
    }
}