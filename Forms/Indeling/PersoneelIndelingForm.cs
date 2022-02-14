using System.Windows.Forms;
using System.Windows.Media.Animation;
using Forms.MetroBase;

namespace Forms
{
    public partial class PersoneelIndelingForm : MetroBaseForm
    {
        public PersoneelIndelingForm()
        {
            InitializeComponent();
        }

        private void personeelIndelingUI1_StatusTextChanged(object sender, System.EventArgs e)
        {
            this.Text = sender as string;
            this.Invalidate();
        }

        private void PersoneelIndelingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            personeelIndelingUI1.CloseUI();
        }

        private void PersoneelIndelingForm_Shown(object sender, System.EventArgs e)
        {
            personeelIndelingUI1.InitUI();
        }
    }
}
