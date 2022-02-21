using System;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class ArtikelsForm : MetroBaseForm
    {
        public ArtikelsForm()
        {
            InitializeComponent();
        }

        public string SelectedArtikelNr
        {
            get => artikelsUI1.SelectedArtikelNr;
            set => artikelsUI1.SelectedArtikelNr = value;
        }

        private void artikelsUI1_StatusTextChanged(object sender, EventArgs e)
        {
            Text = sender as string;
            Invalidate();
        }

        private void ArtikelsForm_Shown(object sender, EventArgs e)
        {
            artikelsUI1.InitUI();
        }

        private void ArtikelsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            artikelsUI1.CloseUI();
        }
    }
}