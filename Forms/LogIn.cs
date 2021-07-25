using System;
using System.Windows.Forms;
using Rpm.Productie;

namespace Forms
{
    public partial class LogIn : MetroFramework.Forms.MetroForm
    {
        public LogIn()
        {
            InitializeComponent();
            if (Manager.Opties != null && !Manager.Opties.Username.ToLower().Contains("default"))
                xgebruikersname.Text = Manager.Opties.Username;
        }

        private void xgebruikersname_MouseEnter(object sender, EventArgs e)
        {
            if (xgebruikersname.Text == "Vul in je gebruikersnaam...")
                xgebruikersname.Text = "";
        }

        private void xgebruikersname_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(xgebruikersname.Text) || xgebruikersname.Text.Trim().Length == 0)
                xgebruikersname.Text = "Vul in je gebruikersnaam...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private async void Login()
        {
            try
            {
                if (await Manager.Login(xgebruikersname.Text.Replace("Vul in je gebruikersnaam...", ""),
                    xwachtwoord1.Text, xautologin.Checked,this))
                    DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                XMessageBox.Show(ex.Message, "Inlog Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xwachtwoord1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                Login();
        }
    }
}