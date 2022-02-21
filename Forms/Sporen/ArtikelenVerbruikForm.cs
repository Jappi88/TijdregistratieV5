using System;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class ArtikelenVerbruikForm : MetroBaseForm
    {
        public ArtikelenVerbruikForm()
        {
            InitializeComponent();
        }

        private void artikelenVerbruikUI1_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void ArtikelenVerbruikForm_Shown(object sender, EventArgs e)
        {
            artikelenVerbruikUI1.InitUI();
        }

        private void ArtikelenVerbruikForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            artikelenVerbruikUI1.CloseUI();
        }

        private void artikelenVerbruikUI1_StatusTextChanged(object sender, EventArgs e)
        {
            Text = sender as string;
            Invalidate();
        }
    }
}