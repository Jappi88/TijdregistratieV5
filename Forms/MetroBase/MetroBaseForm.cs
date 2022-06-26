using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Interfaces;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Rpm.Misc;
using Various;

namespace Forms.MetroBase
{
    public class MetroBaseForm : MetroForm
    {
        public Form OwnerForm { get; set; }

        public bool InitLastLocation { get; set; }

        public MetroBaseForm()
        {
            InitializeComponent();
            SetStyle(
    ControlStyles.UserPaint |
    ControlStyles.AllPaintingInWmPaint |
    ControlStyles.OptimizedDoubleBuffer |
    ControlStyles.SupportsTransparentBackColor,
    true);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MetroBaseForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(100, 75);
            this.Name = "MetroBaseForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MetroBaseForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MetroBaseForm_FormClosed);
            this.Shown += new System.EventHandler(this.MetroBaseForm_Load);
            this.ResumeLayout(false);

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var e = new KeyEventArgs(keyData);
            if (e.KeyCode == Keys.Escape)
            {
                try
                {
                    this.Close();
                    return true;
                }
                catch { }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public virtual string Title
        {
            get => this.Text;
            set
            {
                this.Text = value;
                this.Invalidate();
            }
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
            if (IsDisposed || Disposing) return;
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(InitInfo));
            else InitInfo();
        }

        private void InitInfo()
        {
            try
            {
                if (SaveLastSize)
                    this.InitLastInfo(InitLastLocation);
                if(!InitLastLocation)
                {
                    var par =  OwnerForm??this.GetParentForm();
                    if (par != null)
                    {
                        par = par?.FindForm() ?? par;
                        var loc = par.Location;
                        var x = (loc.X + (par.Width / 2)) - ((this.Width / 2));
                        var y = (loc.Y + (par.Height / 2)) - ((this.Height / 2));
                        this.Location = new Point(x, y);
                        this.Invalidate();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void MetroBaseForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (SaveLastSize)
                this.SetLastInfo();
        }

        private void MetroBaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Form xform = OwnerForm ?? this.GetParentForm();
                if (xform != null && !xform.IsDisposed)
                {
                    if (xform.InvokeRequired)
                    {
                        xform.Invoke(new MethodInvoker(() =>
                        {
                            xform.BringToFront();
                            xform.Focus();
                            xform.Select();
                        }));

                    }
                    else
                    {
                        xform.BringToFront();
                        xform.Focus();
                        xform.Select();
                        xform.Activate();
                    }
                }
            }
            catch { }
        }
    }
}
