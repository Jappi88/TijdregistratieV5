using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Controls;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;

namespace Forms
{
    public partial class UserAccounts : Forms.MetroBase.MetroBaseForm
    {
        public UserAccounts()
        {
            InitializeComponent();
            ListUsers();
            foreach (var x in Enum.GetNames(typeof(AccesType)))
                xlevelselector.Items.Add(x);
        }

        private void xadduser_Click(object sender, EventArgs e)
        {
            var cr = new CreateAccount();
            if (cr.ShowDialog() == DialogResult.OK)
            {
                var ac = cr.Account;
                Manager.CreateAccount(ac).Wait();
                ListUsers();
            }
        }

        private async void ListUsers()
        {
            if (Manager.Database?.UserAccounts == null) return;
            listView1.BeginUpdate();
            listView1.Items.Clear();
            var acc = await Manager.Database.GetAllAccounts();
            foreach (var x in acc)
            {
                var lv = new ListViewItem(x.Username) {Tag = x};
                lv.SubItems.Add(Enum.GetName(typeof(AccesType), x.AccesLevel));
                if (Manager.LogedInGebruiker != null &&
                    string.Equals(Manager.LogedInGebruiker.Username, x.Username, StringComparison.CurrentCultureIgnoreCase))
                    lv.SubItems.Add("Ja");
                else lv.SubItems.Add("Nee");
                listView1.Items.Add(lv);
            }

            listView1.EndUpdate();
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            xinfo.Text = $"Totaal {listView1.Items.Count} {(listView1.Items.Count == 1 ? "Gebruiker" : "Gebruikers")}";
        }

        private async void xremoveuser_Click(object sender, EventArgs e)
        {
            var count = listView1.SelectedItems.Count;
            if (count > 0)
            {
                var del = count > 1
                    ? XMessageBox.Show(this, $"Weetje zeker dat je alle geselecteerde gebruikers wilt verwijderen?",
                        "Verwijderen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes
                    : XMessageBox.Show(this, $"Weetje zeker dat je {listView1.SelectedItems[0].Text} wilt verwijderen?",
                        "Verwijderen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes;
                if (del)
                {
                    foreach (ListViewItem lv in listView1.SelectedItems)
                    {
                        if (lv.Tag is UserAccount us)
                        {
                            if (Manager.LogedInGebruiker != null &&
                                string.Equals(Manager.LogedInGebruiker.Username, us.Username, StringComparison.CurrentCultureIgnoreCase))
                                Manager.LogOut(this,true);
                            if (await Manager.Database.DeleteAccount(us.Username))
                                lv.Remove();
                        }
                    }

                    UpdateStatus();
                }
            }
        }

        private UserAccount[] GetListedAccount()
        {
            var accs = new List<UserAccount>();
            foreach (ListViewItem item in listView1.Items)
            {
                var acc = item.Tag as UserAccount;
                if (acc != null)
                    accs.Add(acc);
            }

            return accs.ToArray();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enable();
        }

        private void Enable()
        {
            xremoveuser.Visible = listView1.SelectedItems.Count > 0;
            xchangepass.Visible = listView1.SelectedItems.Count == 1;
            if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Tag is UserAccount account)
            {
                xlevelpanel.Visible = true;
                xeditb.Visible = false;
                xlevelselector.SelectedItem = Enum.GetName(typeof(AccesType), account.AccesLevel);
                return;
            }

            xlevelpanel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private async void xchangepass_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var acc = listView1.SelectedItems[0].Tag as UserAccount;
                if (acc != null)
                {
                    var changer = new PasswordChanger();
                    if (changer.ShowDialog(acc) == DialogResult.OK)
                        try
                        {
                            await Manager.Database.UpSert(acc);
                        }
                        catch (Exception ex)
                        {
                            XMessageBox.Show(this, ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                }
            }
        }

        private void xeditb_Click(object sender, EventArgs e)
        {
            if (xlevelselector.SelectedItem == null)
                return;
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Tag is UserAccount account)
            {
                if (Enum.TryParse(xlevelselector.SelectedItem.ToString(), out AccesType current))
                    if (current != account.AccesLevel)
                    {
                        account.AccesLevel = current;
                        Manager.Database.UpSert(account);
                        xeditb.Visible = false;
                        listView1.SelectedItems[0].SubItems[1].Text = xlevelselector.SelectedItem.ToString();
                        if (Manager.LogedInGebruiker != null &&
                            string.Equals(Manager.LogedInGebruiker.Username, account.Username, StringComparison.CurrentCultureIgnoreCase))
                        {
                            Manager.LogedInGebruiker.AccesLevel = current;
                            Manager.LoginChanged(this,false,false);
                        }
                    }
            }
        }

        private void xlevelselector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xlevelselector.SelectedItem == null)
                return;
            if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Tag is UserAccount account)
                if (Enum.TryParse(xlevelselector.SelectedItem.ToString(), out AccesType current))
                    if (current != account.AccesLevel)
                    {
                        xeditb.Visible = true;
                        return;
                    }

            xeditb.Visible = false;
        }
    }
}