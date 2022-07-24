using ProductieManager.Rpm.Various;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms.Chat
{
    public partial class NewMessageForm : Form
    {
        public ProductieChatEntry[] Messages { get; private set; }

        public NewMessageForm()
        {
            InitializeComponent();
        }


        public Label TitleLabel => xtitle;

        public void InitMessages(ProductieChatEntry[] messages)
        {
            if (messages == null || messages.Length == 0)
            {
                if (this.Visible)
                    this.Close();
            }
            else
            {
                Messages = messages.OrderBy(x => x.Tijd).ToArray();
                var msg = string.Join("<br>", Messages.Select(x => $"<div><b>{x.Afzender.UserName} Zegt:</b></div>" +
                                                                  $"<div>{x.Bericht}</div>"));
                xmessage.Text = $"<span color='White'>{msg}</span>";
                var x1 = messages.Length == 1 ? "bericht" : "berichten";
                TitleLabel.Text = $"{messages.Length} Nieuwe {x1}";
                xmessage.VerticalScroll.Value = xmessage.VerticalScroll.Maximum;
                xmessage.PerformLayout();
                xmessage.VerticalScroll.Value = xmessage.VerticalScroll.Maximum;
                Application.DoEvents();
                this.Invalidate();
            }
        }

        private void xclose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


        private void xMain_Click(object sender, EventArgs e)
        {
            OnMessageClicked(this);
        }

        private void xMain_MouseEnter(object sender, EventArgs e)
        {
            xMainPanel.BackColor = Color.LightBlue;
        }

        private void xMain_MouseLeave(object sender, EventArgs e)
        {
            xMainPanel.BackColor = Color.LightSteelBlue;
        }

        private void xmessage_LinkClicked(object sender, TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs e)
        {
            if (Manager.Database == null || Manager.Database.IsDisposed) return;
            try
            {
                var prod = Manager.Database.GetProductie(e.Link, false);
                if (prod == null) return;
                var bew = prod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                Manager.FormulierActie(new object[] { prod, bew }, MainAktie.OpenProductie);
                e.Handled = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public event EventHandler MessageClicked;
        protected virtual void OnMessageClicked(object sender)
        {
            this.MessageClicked?.Invoke(sender, EventArgs.Empty);
        }
    }
}
