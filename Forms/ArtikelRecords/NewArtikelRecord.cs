using System;
using System.Windows.Forms;
using Rpm.Productie.ArtikelRecords;

namespace Forms.ArtikelRecords
{
    public partial class NewArtikelRecord : MetroFramework.Forms.MetroForm
    {
        public ArtikelRecord SelectedRecord { get; set; } = new ArtikelRecord();

        public bool IsWerkplek
        {
            get => xwerkplekcheck.Checked;
            set => xwerkplekcheck.Checked = value;
        }

        public bool AllowArtikelEdit
        {
            get => xartikelnr.Enabled;
            set => xartikelnr.Enabled = value;
        }

        public bool AllowWerkplekEdit
        {
            get => xwerkplekcheck.Visible;
            set => xwerkplekcheck.Visible = value;
        }

        public NewArtikelRecord()
        {
            InitializeComponent();
        }

        public NewArtikelRecord(ArtikelRecord record) : this()
        {
            SelectedRecord = record ?? new ArtikelRecord();
            xartikelnr.Text = SelectedRecord.ArtikelNr;
            xomschrijving.Text = SelectedRecord.Omschrijving;
            xwerkplekcheck.Checked = record?.IsWerkplek??false;
            xok.Text = "Wijzigen";
            this.Text = "Wijzig Artikel/ Werkplek";
            this.Invalidate();
        }

        private void xok_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (IsWerkplek)
                {
                    if (xartikelnr.Text.Trim().Length < 4)
                        throw new Exception("Vul in een geldige Werkplek naam a.u.b.");
                }
                else
                {
                    if (xartikelnr.Text.Trim().Length < 4)
                        throw new Exception("Vul in een geldige ArtikelNr a.u.b.");
                }
                if (xomschrijving.Text.Trim().Length < 12)
                    throw new Exception("Vul in een geldige omschrijving a.u.b.");
                SelectedRecord.ArtikelNr = xartikelnr.Text.Trim();
                SelectedRecord.IsWerkplek = xwerkplekcheck.Checked;
                SelectedRecord.Omschrijving = xomschrijving.Text.Trim();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xsluiten_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xwerkplekcheck_CheckedChanged(object sender, EventArgs e)
        {
            xartikelnr.WaterMark = IsWerkplek ? "Vul in een Werkplek naam" : "Vul in een ArtikelNr";
        }
    }
}
