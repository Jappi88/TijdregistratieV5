using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class PersoneelsForm : MetroBaseForm
    {

        public PersoneelsForm(Bewerking werk =  null, bool choose = false)
        {
            InitializeComponent();
            personeelsUI1.InitUI(werk, choose);
        }

        public Personeel[] SelectedPersoneel => personeelsUI1.SelectedPersoneel;

        private void personeelsUI1_StatusTextChanged(object sender, System.EventArgs e)
        {
            this.Text = sender as string;
            this.Invalidate();
        }

        private void personeelsUI1_CloseClicked(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void personeelsUI1_OKClicked(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PersoneelsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            personeelsUI1.CloseUI();
        }
    }
}
