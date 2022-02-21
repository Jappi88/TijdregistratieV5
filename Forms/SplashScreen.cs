using System;
using System.Drawing;
using System.Windows.Forms;

namespace Forms
{
    public partial class SplashScreen : Form
    {
        private readonly int _duraction;
        private Timer _timer;

        public SplashScreen(int duraction)
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            //SetImage();
            _duraction = duraction;
            Shown += SplashScreen_Shown;
        }

        public bool CanClose { get; set; }

        private void SetImage()
        {
            const float limit = 0.3f;
            var bmp = (Bitmap) pictureBox1.Image;
            for (var i = 0; i < bmp.Width; i++)
            for (var j = 0; j < bmp.Height; j++)
            {
                var c = bmp.GetPixel(i, j);
                if (c.GetBrightness() > limit) bmp.SetPixel(i, j, Color.Transparent);
            }

            pictureBox1.Image = bmp;
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            if (_duraction > -1)
                LoadSplash(_duraction);
        }

        public void LoadSplash(int duraction)
        {
            _timer = new Timer();
            _timer.Interval = duraction;
            _timer.Tick += _timer_Tick;
            BringToFront();
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            OnFinishedLoading();
            if (CanClose)
                Close();
            else
                CanClose = true;
        }

        public event EventHandler FinishedLoading;

        protected virtual void OnFinishedLoading()
        {
            FinishedLoading?.Invoke(this, EventArgs.Empty);
        }
    }
}