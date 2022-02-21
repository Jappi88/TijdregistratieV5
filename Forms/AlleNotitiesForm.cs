using System;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class AlleNotitiesForm : MetroBaseForm
    {
        public AlleNotitiesForm()
        {
            InitializeComponent();
        }

        private void AlleNotitiesForm_Shown(object sender, EventArgs e)
        {
            alleNotitiesUI1.InitUI();
        }

        private void AlleNotitiesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            alleNotitiesUI1.CloseUI();
        }
    }
}