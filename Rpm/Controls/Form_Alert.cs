using ProductieManager.Properties;
using Rpm.Various;
using System;
using System.Drawing;
using System.Windows.Forms;
using Rpm.Misc;
using Rpm.Productie;
using Forms.MetroBase;

namespace Rpm.Controls
{
    public partial class Form_Alert: BaseForm
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
            this.Size = new Size(450, 125);
            AllowActivation = false;
            SaveLastInfo = false;
            _Timer = new Timer();
            _Timer.Tick += timer1_Tick;
            _Timer.Interval = 10;
            _Timer.Enabled = false;
        }

        protected override bool ShowWithoutActivation => true;
        private Timer _Timer;

        private void TimerTick()
        {
            try
            {
               
                if (this.Disposing || this.IsDisposed)
                {
                    _Timer.Stop();
                    return;
                }
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(TimerTick));
                    return;
                }
                if (Manager.ActiveForm != null && !Manager.ActiveForm.IsDisposed && Manager.ActiveForm.Visible)
                    OwnerForm = Manager.ActiveForm;
                switch (action)
                {
                    case enmAction.wait:
                        _Timer.Interval = 5000;
                        action = enmAction.close;
                        break;

                    case enmAction.start:
                        _Timer.Interval = 10;
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
                        _Timer.Interval = 10;
                        Opacity -= 0.1;

                        Left -= 3;
                        if (Opacity == 0.0)
                        {
                            // this.Invoke(new Action(Close));
                            if (this.Disposing || this.IsDisposed)
                            {
                                _Timer.Stop();
                                return;
                            }
                            this.Close();
                            //if (typeof(Form_Alert) == this.GetType())
                            //    this.Invoke(new Action(Close));
                        }
                        break;
                }
                _Timer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _Timer.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _Timer.Stop();
            if (this.Disposing || this.IsDisposed)
            {
                return;
            }
            try
            {
                //if (InvokeRequired)
                //    this.Invoke(new Action(TimerTick));
                //else
                TimerTick();
            }
            catch(Exception ex)
            {
                _Timer.Stop();
            }
            
        }

        private void close_Click(object sender, EventArgs e)
        {
            _Timer.Interval = 10;
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

        private void xFocusParent()
        {
            try
            {
                var xform = ((Form)OwnerForm)??this.GetParentForm();
                if (xform != null)
                {
                    xFocusForm(xform);
                }
            }
            catch { }
        }

        private void xFocusForm(Form form)
        {
            if (form == null) return;
            try
            {
                if (form.InvokeRequired)
                    form.Invoke(new MethodInvoker(() => xFocusForm(form)));
                else
                {
                    //form.BringToFront();
                    form.Focus();
                    form.Select();
                }
            }
            catch { }
        }

        private void Form_Alert_FormClosed(object sender, FormClosedEventArgs e)
        {
            xFocusParent();
        }

        public new void Show()
        {
            base.Show();
            action = enmAction.start;
            _Timer.Interval = 10;
            _Timer.Start();
            //Invalidate();
           //xFocusParent();
        }

    }
}