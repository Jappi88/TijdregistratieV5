using System;
using System.ComponentModel;
using System.Windows.Shapes;

namespace Rpm.Misc
{
    [TypeConverter(typeof(Rectangle))]
    public class Radius
    {
        private int u0003;
    private int u0005;
    private int u0008;
    private int u0006;

    public Radius()
        {
        }

        public Radius(int topLeft, int topRight, int bottomLeft, int bottomRight)
        {
            if (topLeft > -1)
                this.TopLeft = topLeft;
            if (topRight > -1)
                this.TopRight = topRight;
            if (bottomLeft > -1)
                this.BottomLeft = bottomLeft;
            if (bottomRight <= -1)
                return;
            this.BottomRight = bottomRight;
        }
        
        [DefaultValue(0)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [NotifyParentProperty(true)]
        public int TopLeft
        {
            get => this.u0003;
            set
            {
                this.u0003 = value > -1 ? value : throw new Exception();
                OnChanged();
            }
        }

        [DefaultValue(0)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [NotifyParentProperty(true)]
        [Browsable(true)]
        public int TopRight
        {
            get => this.u0005;
            set
            {
                this.u0005 = value > -1 ? value : throw new Exception();
                OnChanged();
            }
        }

        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public int BottomRight
        {
            get => this.u0008;
            set
            {
                this.u0008 = value > -1 ? value : throw new Exception();
                OnChanged();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        public int BottomLeft
        {
            get => this.u0006;
            set
            {
                this.u0006 = value > -1 ? value : throw new Exception();
                OnChanged();
            }
        }

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
