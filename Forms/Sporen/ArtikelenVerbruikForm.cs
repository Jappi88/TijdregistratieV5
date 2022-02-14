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

        private void artikelenVerbruikUI1_CloseClicked(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ArtikelenVerbruikForm_Shown(object sender, System.EventArgs e)
        {
            artikelenVerbruikUI1.InitUI();
        }

        private void ArtikelenVerbruikForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            artikelenVerbruikUI1.CloseUI();
        }

        private void artikelenVerbruikUI1_StatusTextChanged(object sender, System.EventArgs e)
        {
            this.Text = sender as string;
            this.Invalidate();
        }
    }
}
