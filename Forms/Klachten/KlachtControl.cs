using Forms.Klachten;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Klachten;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Forms;
using Rpm.Productie;
using Rpm.Various;

namespace ProductieManager.Forms.Klachten
{
    public partial class KlachtControl : UserControl
    {
        public KlachtEntry Klacht { get; private set; }


        private bool _selected;
        public bool IsSelected
        {
            get => _selected;
            set
            {
                _selected = value;
                if (IsSelected)
                {
                    this.BackColor = Color.LightBlue;
                }
                else this.BackColor = Color.Transparent;
                Invalidate();
            }
        }

        public KlachtControl()
        {
            InitializeComponent();
            this.Height = 127;
        }


        public void InitKlacht(KlachtEntry klacht)
        {
            klacht ??= new KlachtEntry();
            Klacht = klacht;
            var ximg = Resources.page_document_16748_128_128.CombineImage(Resources.dialog_error_36230, 2.5);
            xKlachtImage.Image = Klacht.IsGelezen
                ? ximg
                : ximg.CombineImage(Resources.new_25355,ContentAlignment.TopLeft, 2);
            xklachtinfo.Text = klacht.GetKlachtInfoHtml();
            xdelete.Visible = klacht.AllowEdit;
            xedit.Visible = klacht.AllowEdit;
            verwijderenToolStripMenuItem.Enabled = klacht.AllowEdit;
            wijzigToolStripMenuItem.Enabled = klacht.AllowEdit;
        }

        private void xexpand_Click(object sender, EventArgs e)
        {
            if (Klacht != null)
                new KlachtInfoForm(Klacht).ShowDialog(this);
        }

        private void KlachtControl_Click(object sender, EventArgs e)
        {
            OnKlacktClicked(sender);
        }

        private void KlachtControl_DoubleClick(object sender, EventArgs e)
        {
            if (Klacht != null)
                new KlachtInfoForm(Klacht).ShowDialog(this);
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
            {
                this.BackColor = Color.LightBlue;
            }
            else this.BackColor = Color.AliceBlue;
        }

        private void KlachtControl_MouseLeave(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                this.BackColor = Color.LightBlue;
            }
            else this.BackColor = Color.Transparent;
        }

        private void xedit_Click(object sender, EventArgs e)
        {
            if (Klacht == null) return;
            var xklacht = new NewKlachtForm(false, Klacht);
            if (xklacht.ShowDialog(this) == DialogResult.OK)
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

        private void xklachtinfo_LinkClicked(object sender, TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs e)
        {
            var link = e.Link.Trim().ToLower();
            if (link.StartsWith("http") || link.StartsWith("www"))
            {
                Process.Start(link);
            }
            else
            {
                var prod = Manager.Database.GetProductie(link, false);
                if (prod != null)
                {
                    Manager.FormulierActie(new object[] { prod }, MainAktie.OpenProductie);
                }
            }
        }
    }
}
