using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class ArtikelsForm : MetroBaseForm
    {
        public string SelectedArtikelNr
        {
            get => artikelsUI1.SelectedArtikelNr;
            set => artikelsUI1.SelectedArtikelNr = value;
        }

        public ArtikelsForm()
        {
            InitializeComponent();
        }

        private void artikelsUI1_StatusTextChanged(object sender, System.EventArgs e)
        {
            this.Text = sender as string;
            this.Invalidate();
        }

        private void ArtikelsForm_Shown(object sender, System.EventArgs e)
        {
            artikelsUI1.InitUI();
        }

        private void ArtikelsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            artikelsUI1.CloseUI();
        }
    }
}
