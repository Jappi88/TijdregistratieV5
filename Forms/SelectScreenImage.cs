using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Forms
{
    public partial class SelectScreenImage : Form
    {
        //Moving window by click-drag on a control https://stackoverflow.com/a/13477624/5260872
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        private const int _ = 10; // you can rename this variable if you like

        //How to resize a form without a border? https://stackoverflow.com/a/32261547/5260872
        public SelectScreenImage()
        {
            InitializeComponent();
            Opacity = .5D; //Make trasparent
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true); // this is to avoid visual artifacts
        }

        private new Rectangle Top => new(0, 0, ClientSize.Width, _);
        private new Rectangle Left => new(0, 0, _, ClientSize.Height);
        private new Rectangle Bottom => new(0, ClientSize.Height - _, ClientSize.Width, _);
        private new Rectangle Right => new(ClientSize.Width - _, 0, _, ClientSize.Height);
        private static Rectangle TopLeft => new(0, 0, _, _);

        private Rectangle TopRight => new(ClientSize.Width - _, 0, _, _);
        private Rectangle BottomLeft => new(0, ClientSize.Height - _, _, _);
        private Rectangle BottomRight => new(ClientSize.Width - _, ClientSize.Height - _, _, _);

        public string SelectedImagePath { get; set; }

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
            e.Graphics.FillRectangle(Brushes.Green, Top);
            e.Graphics.FillRectangle(Brushes.Green, Left);
            e.Graphics.FillRectangle(Brushes.Green, Right);
            e.Graphics.FillRectangle(Brushes.Green, Bottom);
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }


        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr) HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr) HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr) HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr) HTBOTTOMRIGHT;

                else if (Top.Contains(cursor)) message.Result = (IntPtr) HTTOP;
                else if (Left.Contains(cursor)) message.Result = (IntPtr) HTLEFT;
                else if (Right.Contains(cursor)) message.Result = (IntPtr) HTRIGHT;
                else if (Bottom.Contains(cursor)) message.Result = (IntPtr) HTBOTTOM;
            }
        }

        private void panelDrag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btnCaptureThis_Click(object sender, EventArgs e)
        {
            Hide();
            var save = new ScreenImageForm(Location.X, Location.Y, Width, Height, Size);

            var result = save.ShowDialog();
            SelectedImagePath = save.SavedImagePath;
            DialogResult = result;
        }
    }
}