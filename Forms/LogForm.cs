using System;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class LogForm : MetroBaseForm
    {
        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Shown(object sender, EventArgs e)
        {
            realtimeLog1.Start();
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            realtimeLog1.Stop();
        }

        private void realtimeLog1_OnCloseButtonPressed(object sender, EventArgs e)
        {
            Close();
        }
    }
}