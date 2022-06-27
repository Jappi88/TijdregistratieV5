using MetroFramework;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            StartPosition = Parent == null ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent;
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

        private bool _CustomImage = false;

        public DialogResult ShowDialog(IWin32Window owner, string message, string title, MessageBoxButtons buttons,
            MessageBoxIcon icon,
            string[] chooseitems = null, Dictionary<string, DialogResult> custombuttons = null,
            Image customImage = null, MetroColorStyle style = MetroColorStyle.Default)
        {
            _CustomImage = customImage != null;
            Text = title;
            xmessage.Text = message;
            var maxSize = new Size(xmessage.Width, int.MaxValue);
            var textheight = TextRenderer.MeasureText(xmessage.Text, xmessage.Font, maxSize).Height;
            Height = textheight + 200;
            MinimumSize = new Size(this.Width, this.Height);
            owner = ((Control)owner)?.FindForm()?? owner;
            if (owner is Form f && f.Parent != null)
                owner = f.ParentForm;
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
            try
            {
                while (true)
                {
                    if (owner is Form xform)
                    {
                        OwnerForm = xform;
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

            OwnerForm ??= Application.OpenForms["Mainform"];
            if (icon is MessageBoxIcon.Question)
            {
                xmessageicon.Image = customImage ?? Resources.help_question_1566;
                this.Style = MetroColorStyle.Purple;
            }
            else if (icon is MessageBoxIcon.Error)
            {
                xmessageicon.Image = customImage ?? Resources.exit_close_error_15565;
                this.Style = MetroColorStyle.Red;
            }
            else if (icon is MessageBoxIcon.Warning)
            {
                xmessageicon.Image = customImage ?? Resources.notification_warning_114460;
                this.Style = MetroColorStyle.Orange;
            }
            else if (icon is MessageBoxIcon.Information or MessageBoxIcon.Asterisk)
            {
                xmessageicon.Image = customImage ?? Resources.information_info_1565;
                this.Style = MetroColorStyle.Blue;
            }
            else if (icon is MessageBoxIcon.Exclamation)
            {
                xmessageicon.Image = customImage ?? Resources.exclamation_warning_15590__1_;
                this.Style = MetroColorStyle.Yellow;
            }
            else if (icon is MessageBoxIcon.Stop or MessageBoxIcon.Hand)
            {
                xmessageicon.Image = customImage ?? Resources.Private_80_icon_icons_com_57286;
                this.Style = MetroColorStyle.Red;
            }
            else if (icon is MessageBoxIcon.None)
            {
                xmessageicon.Image = customImage ?? Resources.ios_8_Message_icon_64_64;
            }

            if (customImage != null)
                xmessageicon.SizeMode = PictureBoxSizeMode.StretchImage;
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

            if (OwnerForm != null)
            {
                //Width = form.Width;
               // this.StartPosition = FormStartPosition.Manual;
                //this.Location = new Point((OwnerForm.Location.X + OwnerForm.Width / 2), (OwnerForm.Location.Y + OwnerForm.Height / 2));
                BackColor = OwnerForm.BackColor;
            }
            
            TopMost = true;
            ShowInTaskbar = false;
            this.Invalidate();
            BringToFront();
            return base.ShowDialog(owner);
        }

        private void xmessageicon_DoubleClick(object sender, EventArgs e)
        {
            if(_CustomImage && xmessageicon.Image != null)
            {
                try
                {
                    var xtmp = Path.Combine(Path.GetTempPath(), $"rpm_tmp_img{xmessageicon.Image.GetImageExstension()}");
                    xmessageicon.Image.Save(xtmp);
                    if (File.Exists(xtmp))
                        System.Diagnostics.Process.Start(xtmp);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}