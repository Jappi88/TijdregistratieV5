using System;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class PersoneelsForm : MetroBaseForm
    {
        public PersoneelsForm(Bewerking werk = null, bool choose = false)
        {
            InitializeComponent();
            personeelsUI1.InitUI(werk, choose);
        }

        public Personeel[] SelectedPersoneel => personeelsUI1.SelectedPersoneel;

        private void personeelsUI1_StatusTextChanged(object sender, EventArgs e)
        {
            Text = sender as string;
            Invalidate();
        }

        private void personeelsUI1_CloseClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void personeelsUI1_OKClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PersoneelsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            personeelsUI1.CloseUI();
        }
    }
}