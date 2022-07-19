using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Drawing;
using System.Windows.Forms;
using Various;

namespace Forms.MetroBase
{
    public partial class BaseForm : Form
    {
        IWin32Window _owner;

        public bool SaveLastInfo { get; set; }
        public IWin32Window OwnerForm { get => _owner; set => _owner = value; }

        public bool AllowActivation { get; set; } = true;

        public BaseForm(IWin32Window owner)
        {
            _owner = owner;
            InitializeComponent();
        }

        public BaseForm()
        {
            InitializeComponent();
        }

        private void Producties_Load(object sender, EventArgs e)
        {
            if (SaveLastInfo && Visible)
                InitInfo();
            //
            //if (this.Parent == null)
            //{
            //    var xparent = this.GetParentForm();
            //    if (xparent != null)
            //    {
            //        var y = (xparent.Location.Y + xparent.Height / 2) - this.Height / 2;
            //        var x = (xparent.Location.X + xparent.Width / 2) - this.Width / 2;
            //        if (Screen.GetWorkingArea(xparent).Contains(new Point(x, y)))
            //            this.Location = new Point(x, y);
            //        else this.StartPosition = FormStartPosition.CenterScreen;
            //    }
            //    else
            //        this.StartPosition = FormStartPosition.CenterScreen;
            //}
            //else this.StartPosition = FormStartPosition.CenterParent;
        }

        private void InitInfo()
        {
            try
            {
                var par = ((Control)_owner) ?? this.GetParentForm();
                if (par != null)
                {
                    par = par?.Parent?.FindForm() ?? par;
                    var loc = par.Location;
                    var x = (loc.X + (par.Width / 2)) - ((this.Width / 2));
                    var y = (loc.Y + (par.Height / 2)) - ((this.Height / 2));
                    this.Location = new Point(x, y);
                    this.Invalidate();
                }
                this.InitLastInfo(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Producties_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SaveLastInfo)
                this.SetLastInfo();            
            if(Manager.ActiveForm != null && Manager.ActiveForm.Equals(this))
            {
                Manager.ActiveForm = null;
            }
        }

        private void BaseForm_Activated(object sender, EventArgs e)
        {
            if (this.FormBorderStyle != FormBorderStyle.None && AllowActivation)
            {
                Manager.ActiveForm = this;
            }
        }

        private void BaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //(((Control)_owner)??Manager.ActiveForm)?.BringToFront();
            try
            {
                if (Manager.ActiveForm?.Equals(this?.Parent ?? this) ?? false)
                    Manager.ActiveForm = null;
                Form xform = ((Form)_owner)?? this.GetParentForm();
                if (xform != null && !xform.IsDisposed)
                {
                    if (xform.InvokeRequired)
                    {
                        xform.Invoke(new MethodInvoker(() =>
                        {

                            xform.Focus();
                            xform.Select();
                            xform.BringToFront();
                        }));

                    }
                    else
                    {

                        xform.Focus();
                        xform.Select();
                        xform.BringToFront();
                    }
                }
            }
            catch { }
        }
    }
}
