using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class LogForm : Forms.MetroBase.MetroBaseForm
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