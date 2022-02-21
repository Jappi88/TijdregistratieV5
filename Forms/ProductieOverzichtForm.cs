using System;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class ProductieOverzichtForm : MetroBaseForm
    {
        public ProductieOverzichtForm()
        {
            InitializeComponent();
        }

        private void ProductieOverzichtForm_Shown(object sender, EventArgs e)
        {
            productieOverzichtUI1.InitUI();
        }

        private void ProductieOverzichtForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            productieOverzichtUI1.CloseUI();
        }
    }
}