using System;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class ArtikelRecordsForm : MetroBaseForm
    {
        public ArtikelRecordsForm()
        {
            InitializeComponent();
        }

        private void artikelRecordsUI1_StatusTextChanged(object sender, EventArgs e)
        {
            Text = sender as string;
            Invalidate();
        }

        private void artikelRecordsUI1_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void ArtikelRecordsForm_Shown(object sender, EventArgs e)
        {
            artikelRecordsUI1.InitUI();
        }

        private void ArtikelRecordsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            artikelRecordsUI1.CloseUI();
        }
    }
}