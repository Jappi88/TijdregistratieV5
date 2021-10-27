using System;
using System.Linq;
using System.Windows.Forms;
using Controls;
using Rpm.Productie;
using WeifenLuo.WinFormsUI.Docking;

namespace Forms
{
    public partial class ProductieLijstForm : DockContent
    {
        public string ListName => productieListControl1?.ListName;

        private readonly int _ListIndex;

        public ProductieLijstForm(int index)
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);
            _ListIndex = index;
            productieListControl1.ListName = $"[{_ListIndex}]ProductieLijst";
            UpdateListName();
        }

        private void UpdateListName()
        {
            var xname = productieListControl1.ListName;
            var xitemcount = productieListControl1.ProductieLijst.Items.Count;
            if (Manager.Opties?.Filters != null)
            {
                var name = xname;
                var items = Manager.Opties.Filters.Where(x =>
                    x.ListNames.Any(s => string.Equals(name, s, StringComparison.CurrentCultureIgnoreCase))).ToArray();
                xname = items.Length > 0 ? string.Join(", ", items.Select(x => x.Name)) : name;
            }

            Text = xname + @$"[{xitemcount}]";
            var x1 = xitemcount == 1 ? "bewerking" : "bewerkingen";
            xlijstname.Text = @$"{xname} met totaal {xitemcount} {x1}";
            Invalidate();
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
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
            Manager.FilterChanged += Manager_FilterChanged;
            productieListControl1.InitProductie(true, true, true, true, true, true);
            //if (Manager.Opties?._viewbewdata != null)
            //    productieListControl1.ProductieLijst.RestoreState(Manager.Opties.ViewDataBewerkingenState);
        }

        private void Manager_FilterChanged(object sender, EventArgs e)
        {
            if (sender is ProductieListControl control && string.Equals(productieListControl1.ListName,
                control.ListName, StringComparison.CurrentCultureIgnoreCase))
                try
                {
                    BeginInvoke(new Action(UpdateListName));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
        }

        private void StartProductie_FormClosing(object sender, FormClosingEventArgs e)
        {
            productieListControl1.SaveColumns(false, Manager.Opties,false);
            productieListControl1.DetachEvents();
            Manager.FilterChanged -= Manager_FilterChanged;
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void productieListControl1_ItemCountChanged(object sender, EventArgs e)
        {
            try
            {
                BeginInvoke(new Action(UpdateListName));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}