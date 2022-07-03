using System;
using System.Windows.Forms;
using Various;

namespace Forms
{
    public partial class ChatForm : MetroBase.MetroBaseForm
    {
        public ChatForm()
        {
            InitializeComponent();
        }

        public void Show(IWin32Window owner, string username = null)
        {
            productieChatUI1.SetSelected(username);
            base.Show(owner);
        }

        private void ChatForm_Shown(object sender, EventArgs e)
        {
            //this.InitLastInfo();
            Invalidate();
            Application.DoEvents();
            if (!productieChatUI1.InitUI())
                this.Close();
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.SetLastInfo();
            productieChatUI1.CloseUI();
        }
    }
}
