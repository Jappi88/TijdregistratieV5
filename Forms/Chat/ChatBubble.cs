using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Various;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace ProductieManager.Forms.Chat
{
    public partial class ChatBubble : HtmlLabel

    {
        public ChatBubble()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
            this.TabStop = true;
            //this.Dock = DockStyle.Top;
        }

        public ProductieChatEntry Message { get; private set; }
        public Image ProfileImage { get; set; }

        public void SetMessage(ProductieChatEntry entry, string message, Image image)
        {
            try
            {
                Message = entry;
                ProfileImage = image;
                this.Text = message;
                var size = Message.Bericht.MeasureString(this.Font, new Size(350, 500));
                this.Size = size;
                this.Invalidate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetMessage(ChatBubble message, bool update)
        {
            try
            {
                if (message == null) return;
                bool flag = Message == null || update;
                Message = message.Message;
                if(ProfileImage.Tag is bool online)
                {
                    if (message.ProfileImage.Tag is bool xonline)
                        flag |= xonline != online;
                }
                ProfileImage = message.ProfileImage;
                if (flag || !string.Equals(this.Text, message.Text, StringComparison.CurrentCultureIgnoreCase))
                {
                    this.Text = message.Text;
                    this.Size = Message.Bericht.MeasureString(this.Font, new Size(350, 500));
                    this.Invalidate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xchatview_ImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            string[] values = e.Src.Split(';');
            if (values.Length == 0) return;
            var xmg = ProfileImage;
            switch (values[0])
            {
                case "gelezen":
                    xmg = Properties.Resources.MsgRead_32;
                    break;
                case "verzonden":
                    xmg = Properties.Resources.message_send_32x32;
                    break;
            }

            if (xmg != null)
            {
                string width = values.FirstOrDefault(x => x.ToLower().Contains("width"));
                string height = values.FirstOrDefault(x => x.ToLower().Contains("height"));
                int xwidth = -1;
                int xheight = -1;
                if (width != null)
                {
                    string[] xw = width.Split(':');
                    if (xw.Length > 1)
                        int.TryParse(xw[1], out xwidth);
                }

                if (height != null)
                {
                    string[] xw = height.Split(':');
                    if (xw.Length > 1)
                        int.TryParse(xw[1], out xheight);
                }

                if (xwidth > 0 && xheight > 0)
                    xmg = xmg.ResizeImage(xwidth, xheight);
                e.Callback(xmg);
            }
        }

        private void xchatpanel_LinkClicked(object sender, HtmlLinkClickedEventArgs e)
        {
            if (Manager.Database == null || Manager.Database.IsDisposed) return;
            try
            {
                var prod = Manager.Database.GetProductie(e.Link, false);
                if (prod == null) return;
                var bew = prod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                Manager.FormulierActie(new object[] { prod, bew }, MainAktie.OpenProductie);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ChatBubble chat)
                return chat.Message.Equals(Message);
            if (obj is ProductieChatEntry entry)
                return Message.Equals(entry);
            return false;
        }

        public override int GetHashCode()
        {
            return this.Message?.ID.GetHashCode() ?? 0;
        }
    }
}
