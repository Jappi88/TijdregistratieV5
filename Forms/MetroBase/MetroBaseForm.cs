using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;
using Various;

namespace Forms.MetroBase
{
    public class MetroBaseForm : MetroForm
    {

        public MetroBaseForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MetroBaseForm
            // 
            this.ClientSize = new System.Drawing.Size(970, 669);
            this.Name = "MetroBaseForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MetroBaseForm_FormClosing);
            this.Load += new System.EventHandler(this.MetroBaseForm_Load);
            this.ResumeLayout(false);

        }

        public bool SameChildControlStyle { get; set; } = true;
        public bool SaveLastSize { get; set; } = true;

        public new MetroColorStyle Style
        {
            get => base.Style;
            set => SetStyle(this,value, SameChildControlStyle);
        }

        public void SetStyle(MetroForm form, MetroColorStyle style, bool childcontrols)
        {
            try
            {
                form.Style = style;
                if (childcontrols)
                {
                    var xcontrols = form.Controls.Cast<Control>().ToList();
                    foreach (var xcon in xcontrols)
                    {
                        SetStyle(xcon, style, true);
                    }
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
                    foreach (var xcon in xcontrols)
                    {
                        SetStyle(xcon, style, true);
                    }
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

        private void MetroBaseForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (SaveLastSize)
                this.SetLastInfo();
        }
    }
}
