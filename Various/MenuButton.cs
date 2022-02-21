using System.Drawing;
using System.Windows.Forms;
using ProductieManager.Rpm.Misc;
using Rpm.Various;

namespace Various
{
    public class MenuButton
    {
        private bool _enabled = true;
        private Bitmap _image;
        private string _txt;
        public ContextMenuStrip ContextMenu { get; set; }
        public AccesType AccesLevel { get; set; }
        public int Index { get; set; }
        public Button Base { get; set; }

        public string Text
        {
            get => _txt;
            set
            {
                _txt = value;
                if (Base != null)
                {
                    if (Base.Width < 50)
                        Base.Text = "";
                    else
                        Base.Text = value;
                }
            }
        }

        public string Tooltip { get; set; }
        public string Name { get; set; }

        public Bitmap Image
        {
            get => _image != null
                ? (CombineImage != null ? _image?.CombineImage(CombineImage, CombineScale) : _image).ResizeImage(
                    ImageSize.Width, ImageSize.Height)
                : null;
            set => _image = value;
        }

        public double CombineScale { get; set; } = 1.5;

        public Bitmap CombineImage { get; set; }
        public Size ImageSize { get; set; } = new(32, 32);

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                if (Base != null)
                    Base.Enabled = value;
            }
        }
    }
}