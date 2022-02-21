using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Interfaces;
using Various;

namespace Forms.MetroBase
{
    public class MetroBaseForm : MetroForm
    {
        public MetroBaseForm()
        {
            InitializeComponent();
        }

        public bool SameChildControlStyle { get; set; } = true;
        public bool SaveLastSize { get; set; } = true;

        public new MetroColorStyle Style
        {
            get => base.Style;
            set => SetStyle(this, value, SameChildControlStyle);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // MetroBaseForm
            // 
            ClientSize = new Size(100, 75);
            MinimumSize = new Size(100, 75);
            Name = "MetroBaseForm";
            ShadowType = MetroFormShadowType.AeroShadow;
            StartPosition = FormStartPosition.CenterParent;
            FormClosing += MetroBaseForm_FormClosing;
            Shown += MetroBaseForm_Load;
            ResumeLayout(false);
        }

        public void SetStyle(MetroForm form, MetroColorStyle style, bool childcontrols)
        {
            try
            {
                form.Style = style;
                if (childcontrols)
                {
                    var xcontrols = form.Controls.Cast<Control>().ToList();
                    foreach (var xcon in xcontrols) SetStyle(xcon, style, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetStyle(Control control, MetroColorStyle style, bool childcontrols)
        {
            try
            {
                if (control is IMetroControl xmc)
                {
                    xmc.Style = style;
                    control.Invalidate();
                }

                if (childcontrols)
                {
                    var xcontrols = control.Controls.Cast<Control>().ToList();
                    foreach (var xcon in xcontrols) SetStyle(xcon, style, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        private void MetroBaseForm_Load(object sender, EventArgs e)
        {
            if (SaveLastSize)
                this.InitLastInfo();
        }

        private void MetroBaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SaveLastSize)
                this.SetLastInfo();
        }
    }
}