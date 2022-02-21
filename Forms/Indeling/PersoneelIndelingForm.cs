using System;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class PersoneelIndelingForm : MetroBaseForm
    {
        public PersoneelIndelingForm()
        {
            InitializeComponent();
        }

        private void personeelIndelingUI1_StatusTextChanged(object sender, EventArgs e)
        {
            Text = sender as string;
            Invalidate();
        }

        private void PersoneelIndelingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            personeelIndelingUI1.CloseUI();
        }

        private void PersoneelIndelingForm_Shown(object sender, EventArgs e)
        {
            personeelIndelingUI1.InitUI();
        }
    }
}