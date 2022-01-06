using System;
using System.Windows.Forms;
using Rpm.Productie.ArtikelRecords;

namespace Forms.ArtikelRecords
{
    public partial class NewArtikelRecord : MetroFramework.Forms.MetroForm
    {
        public ArtikelRecord SelectedRecord { get; set; } = new ArtikelRecord();
        public NewArtikelRecord()
        {
            InitializeComponent();
        }

        public NewArtikelRecord(ArtikelRecord record) : this()
        {
            SelectedRecord = record ?? new ArtikelRecord();
            xartikelnr.Text = SelectedRecord.ArtikelNr;
            xomschrijving.Text = SelectedRecord.Omschrijving;
            xok.Text = "Wijzigen";
            this.Text = "Wijzig Artikel";
            this.Invalidate();
        }

        private void xok_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (xartikelnr.Text.Trim().Length < 4)
                    throw new Exception("Vul in een geldige ArtikelNr a.u.b.");
                if (xomschrijving.Text.Trim().Length < 12)
                    throw new Exception("Vul in een geldige omschrijving a.u.b.");
                SelectedRecord.ArtikelNr = xartikelnr.Text.Trim();
                SelectedRecord.Omschrijving = xomschrijving.Text.Trim();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                XMessageBox.Show(ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xsluiten_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
