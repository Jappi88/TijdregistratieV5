using MetroFramework;
using ProductieManager.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class XMessageBox : Forms.MetroBase.MetroBaseForm
    {
        public DialogResult Result { get; }
        private Color _defaultColor = Color.FromArgb(57, 179, 215);
        private Color _errorColor = Color.FromArgb(210, 50, 45);
        private Color _warningColor = Color.FromArgb(237, 156, 40);
        private Color _success = Color.FromArgb(71, 164, 71);
        private Color _question = Color.FromArgb(71, 164, 71);

        public XMessageBox()
        {
            InitializeComponent();
            SaveLastSize = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
        }

        public string SelectedValue => (string) xchooser.SelectedItem;

        public static DialogResult Show(IWin32Window owner, string message)
        {
            return new XMessageBox().ShowDialog(owner, message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title)
        {
            return new XMessageBox().ShowDialog(owner,message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon,
            string[] chooseitems = null, Dictionary<string, DialogResult> custombuttons = null, Image custImage = null,
            MetroColorStyle style = MetroColorStyle.Default)
        {
            return new XMessageBox().ShowDialog(owner,message, title, buttons, icon, chooseitems, custombuttons, custImage,
                style);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxButtons buttons, Image icon)
        {
            return new XMessageBox().ShowDialog(owner, message, title, buttons, MessageBoxIcon.Information, null, null, icon);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxButtons buttons, Image icon,
            MetroColorStyle style)
        {
            return new XMessageBox().ShowDialog(owner, message, title, buttons, MessageBoxIcon.Information, null, null, icon,
                style);
        }


        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxIcon icon,
            string[] chooseitems = null, Dictionary<string, DialogResult> custombuttons = null)
        {
            return new XMessageBox().ShowDialog(owner, message, title, MessageBoxButtons.OK, icon, chooseitems, custombuttons);
        }

        private void xmessageb1_Click(object sender, EventArgs e)
        {
            DialogResult = (DialogResult) xmessageb1.Tag;
        }

        private void xmessageb2_Click(object sender, EventArgs e)
        {
            DialogResult = (DialogResult) xmessageb2.Tag;
        }

        private void xmessageb3_Click(object sender, EventArgs e)
        {
            DialogResult = (DialogResult) xmessageb3.Tag;
        }

        private void xmessageb4_Click(object sender, EventArgs e)
        {
            DialogResult = (DialogResult) xmessageb4.Tag;
        }

        public DialogResult ShowDialog(IWin32Window owner, string message, string title, MessageBoxButtons buttons,
            MessageBoxIcon icon,
            string[] chooseitems = null, Dictionary<string, DialogResult> custombuttons = null,
            Image customImage = null, MetroColorStyle style = MetroColorStyle.Default)
        {
            Text = title;
            xmessage.Text = message;
            var maxSize = new Size(xmessage.Width, int.MaxValue);
            var textheight = TextRenderer.MeasureText(xmessage.Text, xmessage.Font, maxSize).Height;
            Height = textheight + 200;
            MinimumSize = new Size(this.Width, this.Height);
            if (custombuttons is {Count: > 0})
            {
                var done = 0;
                foreach (var cust in custombuttons)
                {
                    switch (done)
                    {
                        case 0:
                            xmessageb3.Text = cust.Key;
                            //xmessageb3.Size = new Size(cust.Key.MeasureString(xmessageb3.Font).Width, xmessageb3.Size.Height);
                            xmessageb3.Tag = cust.Value;
                            xmessageb3.Visible = true;
                            break;
                        case 1:
                            xmessageb2.Text = cust.Key;
                            xmessageb2.Tag = cust.Value;
                            xmessageb2.Visible = true;
                            break;
                        case 2:
                            xmessageb1.Text = cust.Key;
                            xmessageb1.Tag = cust.Value;
                            xmessageb1.Visible = true;
                            break;
                        case 3:
                            xmessageb4.Text = cust.Key;
                            xmessageb4.Tag = cust.Value;
                            xmessageb4.Visible = true;
                            break;
                    }

                    done++;
                    if (done >= 4)
                        break;
                }
            }
            else
            {
                switch (buttons)
                {
                    case MessageBoxButtons.OK:
                        xmessageb3.Visible = true;
                        xmessageb3.Tag = DialogResult.OK;
                        xmessageb2.Visible = false;
                        xmessageb1.Visible = false;
                        xmessageb3.Text = "OK";
                        break;
                    case MessageBoxButtons.OKCancel:
                        xmessageb3.Visible = true;
                        xmessageb3.Tag = DialogResult.Cancel;
                        xmessageb2.Visible = true;
                        xmessageb2.Tag = DialogResult.OK;
                        xmessageb1.Visible = false;
                        xmessageb3.Text = "Annuleren";
                        xmessageb2.Text = "OK";
                        break;
                    case MessageBoxButtons.AbortRetryIgnore:
                        xmessageb3.Visible = true;
                        xmessageb3.Tag = DialogResult.Abort;
                        xmessageb2.Visible = true;
                        xmessageb2.Tag = DialogResult.Retry;
                        xmessageb1.Visible = true;
                        xmessageb1.Tag = DialogResult.Ignore;
                        xmessageb1.Text = "Afbreken";
                        xmessageb2.Text = "Opnieuw";
                        xmessageb3.Text = "Negeren";
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        xmessageb3.Visible = true;
                        xmessageb3.Tag = DialogResult.Cancel;
                        xmessageb2.Visible = true;
                        xmessageb2.Tag = DialogResult.No;
                        xmessageb1.Visible = true;
                        xmessageb1.Tag = DialogResult.Yes;
                        xmessageb1.Text = "Ja";
                        xmessageb2.Text = "Nee";
                        xmessageb3.Text = "Annuleren";
                        break;
                    case MessageBoxButtons.YesNo:
                        xmessageb3.Visible = true;
                        xmessageb3.Tag = DialogResult.No;
                        xmessageb2.Visible = true;
                        xmessageb2.Tag = DialogResult.Yes;
                        xmessageb1.Visible = false;
                        xmessageb2.Text = "Ja";
                        xmessageb3.Text = "Nee";
                        break;
                    case MessageBoxButtons.RetryCancel:
                        xmessageb1.Visible = true;
                        xmessageb1.Tag = DialogResult.Abort;
                        xmessageb2.Visible = true;
                        xmessageb2.Tag = DialogResult.Retry;
                        xmessageb1.Visible = false;
                        xmessageb3.Text = "Afbreken";
                        xmessageb2.Text = "Opnieuw";
                        break;
                }
            }

            Form form = null;
            try
            {
                while (true)
                {
                    if (owner is Form xform)
                    {
                        form = xform;
                        break;
                    }
                    if (owner is Control xcontrol)
                    {
                        owner = xcontrol.Parent;
                    }

                    if (owner == null)
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            form ??= Application.OpenForms["Mainform"];

            switch (icon)
            {
                case MessageBoxIcon.None:
                    xmessageicon.Image = customImage ?? Resources.ios_8_Message_icon_64_64;
                    //xMessagePanel.BackColor = _defaultColor;
                    break;
                case MessageBoxIcon.Question:
                    xmessageicon.Image = customImage ?? Resources.help_question_1566;
                    this.Style = MetroColorStyle.Purple;
                    //xMessagePanel.BackColor = _question;
                    break;
                case MessageBoxIcon.Exclamation:
                    xmessageicon.Image = customImage ?? Resources.exclamation_warning_15590__1_;
                    this.Style = MetroColorStyle.Yellow;
                   // xMessagePanel.BackColor = _warningColor;
                    break;
                case MessageBoxIcon.Information:
                    xmessageicon.Image = customImage ?? Resources.information_info_1565;
                    this.Style = MetroColorStyle.Blue;
                   // xMessagePanel.BackColor = _success;
                    break;
                case MessageBoxIcon.Error:
                    xmessageicon.Image = customImage ?? Resources.exit_close_error_15565;
                    this.Style = MetroColorStyle.Red;
                   // xMessagePanel.BackColor = _errorColor;
                    break;
            }

            if (style != MetroColorStyle.Default)
                this.Style = style;
            xchooser.Items.Clear();
            xchooserpanel.Visible = chooseitems is {Length: > 0};
            if (chooseitems is {Length: > 0})
            {
                xchooser.Items.AddRange(chooseitems.Select(x => (object) x).ToArray());
                if (xchooser.Items.Count > 0)
                    xchooser.SelectedIndex = 0;
                Height += 50;
            }

            if (form != null)
            {
                Width = form.Width;
                BackColor = form.BackColor;
            }
            TopMost = true;
            ShowInTaskbar = false;
            this.Invalidate();
            BringToFront();
            return ShowDialog();
        }
    }
}