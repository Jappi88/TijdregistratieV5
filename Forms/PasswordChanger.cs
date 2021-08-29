using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using ProductieManager.Properties;
using Rpm.Settings;

namespace Forms
{
    public partial class PasswordChanger : MetroFramework.Forms.MetroForm
    {
        public PasswordChanger()
        {
            InitializeComponent();
        }

        public UserAccount Account { get; set; }

        public DialogResult ShowDialog(UserAccount account)
        {
            if (account == null)
                return DialogResult.Cancel;
            Account = account;
            Text = $"Wijzig Wachtwoord: {account.Username}";
            return base.ShowDialog();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox box)
            {
                var t = GetTextBoxByPicturebox(box);
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

  private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox box) box.BackColor = Color.LightBlue;
        }

  private void pictureBox_MouseLeave(object sender, EventArgs e)
  {
      if (sender is PictureBox box) box.BackColor = Color.Transparent;
  }

  private TextBox GetTextBoxByPicturebox(PictureBox box)
        {
            if (box.Name == "xpic1")
                return xpass1;
            if (box.Name == "xpic2")
                return xpass2;
            if (box.Name == "xpic3")
                return xpass3;
            return null;
        }

      

        private void xok_Click(object sender, EventArgs e)
        {
            if (Account == null)
                return;
            var pass = Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(xpass1.Text)));
            if (pass != Account.Password)
            {
                XMessageBox.Show("Ongeldige Wachtwoord!\n\nWachtwoord komt niet overeen met je account.", "Ongeldig",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (xpass2.Text.Length < 6)
            {
                XMessageBox.Show("Ongeldige Wachtwoord!\n\nWachtwoord moet minimaal 6 karakters hebben.", "Ongeldig",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (xpass2.Text != xpass3.Text)
            {
                XMessageBox.Show("Ongeldige Wachtwoord!\n\nWachtwoorden komen niet overeen met elkaar..", "Ongeldig",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Account.Password = xpass3.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private void xannuleer_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}