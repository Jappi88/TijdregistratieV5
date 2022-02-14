using System.Windows.Forms;
using System.Windows.Media.Animation;
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

        private void PersoneelIndelingForm_Shown(object sender, System.EventArgs e)
        {
            werkplaatsIndeling1.InitUI();
        }

        private void werkplaatsIndeling1_StatusTextChanged(object sender, System.EventArgs e)
        {
            this.Text = sender as string;
            this.Invalidate();
        }
    }
}
