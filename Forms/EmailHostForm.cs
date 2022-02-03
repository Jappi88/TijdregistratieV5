using ProductieManager.Properties;
using Rpm.Mailing;
using Rpm.Misc;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Forms
{
    public partial class EmailHostForm : Forms.MetroBase.MetroBaseForm
    {
        public EmailHost SelectedHost { get; private set; } = new();
        public EmailHostForm()
        {
            InitializeComponent();
        }

        public EmailHostForm(EmailHost host) : this()
        {
            SelectedHost = host ?? new EmailHost();
            if (SelectedHost != null)
            {
                xemailtextbox.Text = SelectedHost.EmailAdres?.Trim();
                xpasstextbox.Text = SelectedHost.Password?.Trim();
                xusessl.Checked = SelectedHost.UseSsl;
            }

        }
        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox box)
            {
                var t = xpasstextbox;
                if (t != null)
                {
                    if (box.Tag.ToString() == "1")
                    {
                        box.Image = Resources._3844443_disable_eye_inactive_see_show_view_watch_110296;
                        box.Tag = "0";
                        t.PasswordChar = '*';
                    }
                    else
                    {
                        box.Image = Resources._3844441_eye_see_show_view_watch_110305;
                        box.Tag = "1";
                        t.PasswordChar = new char();
                    }
                }
            }
        }

        public EmailHost GetMailHost()
        {
            return new EmailHost()
            {
                EmailAdres = xemailtextbox.Text.Trim(),
                Password = xpasstextbox.Text.Trim(),
                UseSsl = xusessl.Checked
            };
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox box) box.BackColor = Color.LightBlue;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox box) box.BackColor = Color.Transparent;
        }

        private void xemailtextbox_TextChanged(object sender, EventArgs e)
        {
            if (!xemailtextbox.Text.EmailIsValid() &&
                !xemailtextbox.Text.ToLower().Contains("vul in een geldige email adres"))
                xemailtextbox.ForeColor = Color.Red;
            else xemailtextbox.ForeColor = Color.Black;
        }

        private void xemailtextbox_Enter(object sender, EventArgs e)
        {
            if (xemailtextbox.Text == @"Vul in een geldige email adres...")
                xemailtextbox.Text = "";
        }

        private void xemailtextbox_Leave(object sender, EventArgs e)
        {
            if (xemailtextbox.Text.Trim().Length == 0)
                xemailtextbox.Text = @"Vul in een geldige email adres...";
        }

        private void xcancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xsave_Click(object sender, EventArgs e)
        {
            try
            {
                var host = GetMailHost();
                if (host.TextConnection())
                {
                    SelectedHost.EmailAdres = xemailtextbox.Text.Trim();
                    SelectedHost.Password = xpasstextbox.Text.Trim();
                    SelectedHost.UseSsl = xusessl.Checked;
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

       
    }
}
