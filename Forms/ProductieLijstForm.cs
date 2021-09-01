using Rpm.Productie;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Forms
{
    public partial class ProductieLijstForm : DockContent
    {
        public string ListName => productieListControl1?.ListName;

        public ProductieLijstForm(string name)
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);
            productieListControl1.ListName = name;
            xlijstname.Text = name;
            this.Text = name;
        }

        public new void Show(DockPanel dock)
        {
            if (!Visible)
                base.Show(dock, DockState.Document);
            else Focus();
        }

        private void StartProductie_Shown(object sender, EventArgs e)
        {
            productieListControl1.InitEvents();
            productieListControl1.InitProductie(true, true, true, true, true, true);
            if (Manager.Opties?._viewbewdata != null)
                productieListControl1.ProductieLijst.RestoreState(Manager.Opties.ViewDataBewerkingenState);
        }

        private void StartProductie_FormClosing(object sender, FormClosingEventArgs e)
        {
            productieListControl1.DetachEvents();
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void productieListControl1_ItemCountChanged(object sender, EventArgs e)
        {
            try
            {
                this.BeginInvoke(new Action(() =>
                {
                    var count = productieListControl1.ProductieLijst.Items.Count;
                    var x1 = count == 1 ? "bewerking" : "bewerkingen";
                    xlijstname.Text = @$"{ListName} met totaal {count} {x1}";
                    this.Invalidate();
                }));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}