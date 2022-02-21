using System;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class WerkplaatsIndelingForm : MetroBaseForm
    {
        public WerkplaatsIndelingForm()
        {
            InitializeComponent();
        }

        private void PersoneelIndelingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            werkplaatsIndeling1.CloseUI();
        }

        private void PersoneelIndelingForm_Shown(object sender, EventArgs e)
        {
            werkplaatsIndeling1.InitUI();
        }

        private void werkplaatsIndeling1_StatusTextChanged(object sender, EventArgs e)
        {
            Text = sender as string;
            Invalidate();
        }
    }
}