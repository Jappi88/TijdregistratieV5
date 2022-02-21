using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Forms;
using Forms.Klachten;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Klachten;
using Rpm.Productie;
using Rpm.Various;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace ProductieManager.Forms.Klachten
{
    public partial class KlachtControl : UserControl
    {
        private bool _selected;

        public KlachtControl()
        {
            InitializeComponent();
            Height = 127;
        }

        public KlachtEntry Klacht { get; private set; }

        public bool IsSelected
        {
            get => _selected;
            set
            {
                _selected = value;
                if (IsSelected)
                    BackColor = Color.LightBlue;
                else BackColor = Color.Transparent;
                Invalidate();
            }
        }


        public void InitKlacht(KlachtEntry klacht)
        {
            klacht ??= new KlachtEntry();
            Klacht = klacht;
            var ximg = Resources.page_document_16748_128_128.CombineImage(Resources.dialog_error_36230, 2.5);
            xKlachtImage.Image = Klacht.IsGelezen
                ? ximg
                : ximg.CombineImage(Resources.new_25355, ContentAlignment.TopLeft, 2);
            xklachtinfo.Text = klacht.GetKlachtInfoHtml();
            xdelete.Visible = klacht.AllowEdit;
            xedit.Visible = klacht.AllowEdit;
            verwijderenToolStripMenuItem.Enabled = klacht.AllowEdit;
            wijzigToolStripMenuItem.Enabled = klacht.AllowEdit;
        }

        private void xexpand_Click(object sender, EventArgs e)
        {
            if (Klacht != null)
                new KlachtInfoForm(Klacht).ShowDialog();
        }

        private void KlachtControl_Click(object sender, EventArgs e)
        {
            OnKlacktClicked(sender);
        }

        private void KlachtControl_DoubleClick(object sender, EventArgs e)
        {
            if (Klacht != null)
                new KlachtInfoForm(Klacht).ShowDialog();
        }

        public event EventHandler KlacktClicked;

        protected virtual void OnKlacktClicked(object sender)
        {
            IsSelected = true;

            if (sender is Control c)
            {
                c.Select();
                c.Focus();
            }

            KlacktClicked?.Invoke(this, EventArgs.Empty);
        }

        private void KlachtControl_MouseEnter(object sender, EventArgs e)
        {
            if (IsSelected)
                BackColor = Color.LightBlue;
            else BackColor = Color.AliceBlue;
        }

        private void KlachtControl_MouseLeave(object sender, EventArgs e)
        {
            if (IsSelected)
                BackColor = Color.LightBlue;
            else BackColor = Color.Transparent;
        }

        private void xedit_Click(object sender, EventArgs e)
        {
            if (Klacht == null) return;
            var xklacht = new NewKlachtForm(false, Klacht);
            if (xklacht.ShowDialog() == DialogResult.OK)
            {
                xklacht.Klacht.GelezenDoor.Clear();
                Manager.Klachten.SaveKlacht(xklacht.Klacht, true);
            }
        }

        private void xdelete_Click(object sender, EventArgs e)
        {
            if (Klacht == null) return;
            if (XMessageBox.Show(this, $"Weetje zeker dat je '{Klacht.Onderwerp}' wilt verwijderen?", "Verwijderen",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                Manager.Klachten.RemoveKlacht(Klacht);
        }

        private void xklachtinfo_LinkClicked(object sender, HtmlLinkClickedEventArgs e)
        {
            var link = e.Link.Trim().ToLower();
            if (link.StartsWith("http") || link.StartsWith("www"))
            {
                Process.Start(link);
            }
            else
            {
                var prod = Manager.Database.GetProductie(link);
                if (prod != null) Manager.FormulierActie(new object[] {prod}, MainAktie.OpenProductie);
            }
        }
    }
}