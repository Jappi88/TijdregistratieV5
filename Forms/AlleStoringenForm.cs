using System;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class AlleStoringenForm : MetroBaseForm
    {
        public AlleStoringenForm()
        {
            InitializeComponent();
        }

        public void InitUI()
        {
            alleStoringenUI1.InitUI();
        }

        public void CloseUI()
        {
            alleStoringenUI1.CloseUI();
        }

        public void InitStoringen(ProductieFormulier form = null, WerkPlek selected = null)
        {
            alleStoringenUI1.InitUI();
            alleStoringenUI1.InitStoringen(form, selected);
        }

        private void alleStoringenUI1_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void alleStoringenUI1_StatusChanged(object sender, EventArgs e)
        {
            Text = sender as string;
            Invalidate();
        }

        private void AlleStoringenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseUI();
        }
    }
}