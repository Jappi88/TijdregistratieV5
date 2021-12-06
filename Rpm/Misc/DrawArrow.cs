using ProductieManager.Properties;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controls;

namespace ProductieManager.Rpm.Misc
{
    public enum Direction
    {
        Top,
        Left,
        Right,
        Down
    }

    public class DrawArrow : IDisposable
    {
        public  Direction Direction { get; private set; }
        public bool IsMoving { get; private set; }
        public Point Location { get; set; }
        public Control UserControl { get; set; }
       // public Graphics View { get; set; }
        public int MoveMargin { get; set; } = 10;
        public int AnimationSpeed { get; set; } = 10;
        public Point Position { get; set; }
        public CustomImageBox ImageBox { get; private set; }

        public DrawArrow(Control control, Direction direction, bool move, Point location)
        {
            Direction = direction;
            Location = location;
            IsMoving = move;
            UserControl = control;
            ImageBox = new CustomImageBox();
            ImageBox.SizeMode = PictureBoxSizeMode.CenterImage;
            ImageBox.BackColor = Color.Transparent;
            ImageBox.Name = "PointerImage";
            ImageBox.Size = new Size(32, 32);
            // View = control.CreateGraphics();
        }

        public void Draw()
        {
           // if (UserControl == null || UserControl.IsDisposed) return;
            try
            {
                Task.Factory.StartNew( () =>
                {
                    UserControl.Invoke(new MethodInvoker(() =>
                    {
                        UserControl.Controls.Remove(ImageBox);
                        ImageBox.Image = GetArrowImage();
                        ImageBox.Location = GetLocation();
                        UserControl.Controls.Add(ImageBox);
                        ImageBox.BringToFront();
                    }));
                    while (IsMoving)
                    {
                        if (UserControl == null || UserControl.IsDisposed) break;
                        UserControl.Invoke(new MethodInvoker(() =>
                        {
                            ImageBox.Location = GetLocation();
                            ImageBox.Invalidate();
                            UserControl.Invalidate();
                        }));
                        Task.Delay(AnimationSpeed).Wait();
                    }
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private bool _positive;
        private Point GetLocation()
        {
            var xloc = Position;
            if (IsMoving)
            {
                switch (Direction)
                {
                    case Direction.Top:
                        if (_positive)
                        {
                            xloc.Y++;
                        }
                        else xloc.Y--;
                        break;
                    case Direction.Left:
                        if (_positive)
                        {
                            xloc.X--;
                        }
                        else xloc.X++;
                        break;
                    case Direction.Right:
                        if (_positive)
                        {
                            xloc.X++;
                        }
                        else xloc.X--;
                        break;
                    case Direction.Down:
                        if (_positive)
                        {
                            xloc.Y--;
                        }
                        else xloc.Y++;
                        break;
                }

                if (xloc.Y > MoveMargin || xloc.Y < -MoveMargin ||
                    (xloc.X > MoveMargin || xloc.X < -MoveMargin))
                {
                    _positive = !_positive;
                }

                Position = xloc;
            }

            return new Point(Location.X + xloc.X, Location.Y + xloc.Y);
        }

        public Image GetArrowImage()
        {
            switch (Direction)
            {
                case Direction.Down:
                    return Resources.arrow_down_16740_32x32;
                case Direction.Left:
                    return Resources.arrow_left_15601;
                case Direction.Right:
                    return Resources.arrow_right_15600;
                case Direction.Top:
                    return Resources.arrow_up_16741_32x32;
            }

            return null;
        }

        public void Move(bool move)
        {

        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            IsMoving = false;
            
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
                UserControl?.Controls.Remove(ImageBox);
                ImageBox.Image = null;
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }
    }
}
