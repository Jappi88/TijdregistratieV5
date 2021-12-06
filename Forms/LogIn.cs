using System;
using System.Windows.Forms;
using System.Windows.Navigation;
using Rpm.Productie;

namespace Forms
{
    public partial class LogIn : MetroFramework.Forms.MetroForm
    {
        public bool DisableLogin { get; set; }
        public bool ShowAutoLoginCheckbox
        {
            get => xautologin.Visible;
            set => xautologin.Visible = value;
        }
        public LogIn()
        {
            InitializeComponent();
            if (Manager.Opties != null && !Manager.Opties.Username.ToLower().Contains("default"))
                xgebruikersname.Text = Manager.Opties.Username;
        }

        private void xgebruikersname_MouseEnter(object sender, EventArgs e)
        {
            if (xgebruikersname.Text == @"Vul in je gebruikersnaam...")
                xgebruikersname.Text = "";
        }

        public string Username => xgebruikersname.Text.Replace("Vul in je gebruikersnaam...", "").Trim();
        public string Password => xwachtwoord1.Text.Trim();
        public bool AutoLogin => xautologin.Checked;

        private void xgebruikersname_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(xgebruikersname.Text) || xgebruikersname.Text.Trim().Length == 0)
                xgebruikersname.Text = @"Vul in je gebruikersnaam...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private async void Login()
        {
            try
            {
                if (DisableLogin || await Manager.Login(xgebruikersname.Text.Replace("Vul in je gebruikersnaam...", ""),
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

        private void xwachtwoord1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                Login();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var cr = new CreateAccount();
            if (cr.ShowDialog() == DialogResult.OK)
            {
                var ac = cr.Account;
                Manager.CreateAccount(ac).Wait();
            }
        }
    }
}