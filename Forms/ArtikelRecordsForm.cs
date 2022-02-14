using Forms.MetroBase;

namespace Forms
{
    public partial class ArtikelRecordsForm : MetroBaseForm
    {
        public ArtikelRecordsForm()
        {
            InitializeComponent();
        }

        private void artikelRecordsUI1_StatusTextChanged(object sender, System.EventArgs e)
        {
            this.Text = sender as string;
            this.Invalidate();
        }

        private void artikelRecordsUI1_CloseClicked(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ArtikelRecordsForm_Shown(object sender, System.EventArgs e)
        {
            artikelRecordsUI1.InitUI();
        }

        private void ArtikelRecordsForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            artikelRecordsUI1.CloseUI();
        }
    }
}
