using ProductieManager.Properties;
using Rpm.Various;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Rpm.Controls
{
    public partial class Form_Alert: Form
    {
        public enum enmAction
        {
            wait,
            start,
            close
        }

        private enmAction action;

        private int x, y;

        public Form_Alert()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (action)
            {
                case enmAction.wait:
                    timer1.Interval = 5000;
                    action = enmAction.close;
                    break;

                case enmAction.start:
                    timer1.Interval = 10;
                    Opacity += 0.1;
                    if (x < Location.X)
                    {
                        Left--;
                    }
                    else
                    {
                        if (Opacity == 1.0) action = enmAction.wait;
                    }

                    break;

                case enmAction.close:
                    timer1.Interval = 10;
                    Opacity -= 0.1;

                    Left -= 3;
                    if (Opacity == 0.0)
                    {
                        this.Invoke(new Action(Close));
                        //this.Close();
                    }
                    break;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            action = enmAction.close;
        }

        private void lblMsg_MouseEnter(object sender, EventArgs e)
        {
        }

        private void lblMsg_MouseLeave(object sender, EventArgs e)
        {
        }

        public void showAlert(string msg, string title, MsgType type)
        {
            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;
            for (var i = 1; i < 13; i++)
            {
                var fname = "alert" + i;
                var frm = (Form_Alert)Application.OpenForms[fname];

                if (frm == null)
                {
                    Name = fname;
                    x = Screen.PrimaryScreen.WorkingArea.Width - Width + 15;
                    y = Screen.PrimaryScreen.WorkingArea.Height - Height * i - 5 * i;

                    Location = new Point(x, y);
                    break;
                }
            }

            x = Screen.PrimaryScreen.WorkingArea.Width - Width - 5;

            switch (type)
            {
                case MsgType.Success:
                    pictureBox1.Image = Resources.success;
                    BackColor = Color.MediumSeaGreen;
                    break;

                case MsgType.Fout:
                    pictureBox1.Image = Resources.error;
                    BackColor = Color.DarkRed;
                    break;

                case MsgType.Info:
                    pictureBox1.Image = Resources.info;
                    BackColor = Color.RoyalBlue;
                    break;

                case MsgType.Waarschuwing:
                    pictureBox1.Image = Resources.warning;
                    BackColor = Color.DarkOrange;
                    break;
                case MsgType.Gebruiker:
                    pictureBox1.Image = Resources.user_64_64;
                    BackColor = Color.LightSkyBlue;

                    xmsgTitle.ForeColor = Color.Black;
                    lblMsg.ForeColor = Color.Black;
                    break;
                case MsgType.Bericht:
                    pictureBox1.Image = Resources.ios_8_Message_icon_64_64;
                    BackColor = Color.CornflowerBlue;
                    break;
            }

            xmsgTitle.Text = title;
            lblMsg.Text = msg;
        }

        public new void Show()
        {
            base.Show();
            action = enmAction.start;
            timer1.Interval = 100;
            timer1.Start();
            Invalidate();
        }

    }
}