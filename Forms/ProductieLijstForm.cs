using Controls;
using ProductieManager.Forms.MetroDock;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Forms
{
    public partial class ProductieLijstForm : DockInstance
    {
        public string ListName => productieListControl1?.ListName;

        public IsValidHandler ValidHandler
        {
            get => productieListControl1?.ValidHandler;
            set
            {
                if (productieListControl1 != null)
                    productieListControl1.ValidHandler = value;
            }
        }

        private readonly int _ListIndex;
        private List<Bewerking> _bewerkingen;

        public ProductieLijstForm():base()
        {
            InitializeComponent();
            DisplayHeader = false;
            ShadowType = MetroFormShadowType.AeroShadow;
            Style = MetroColorStyle.Purple;
        }

        public ProductieLijstForm(int index):this()
        {
            _ListIndex = index;
            productieListControl1.ListName = $"[{_ListIndex}]ProductieLijst";
            UpdateListName();
        }

        public bool IsValidHandler(object value, string filter)
        {
            if (value is Bewerking bew)
            {
                return _bewerkingen.IndexOf(bew) > -1;
            }

            return false;
        }

        public ProductieLijstForm(List<Bewerking> bewerkingen, string name):this()
        {
            productieListControl1.ListName = name;
            _bewerkingen = bewerkingen;
            productieListControl1.EnableFiltering = false;
            productieListControl1.ValidHandler = IsValidHandler;
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
            xstatuslabel.Text = @$"{xname} met totaal {xitemcount} {x1}";
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
            Manager.FilterChanged += Manager_FilterChanged;
            if(_bewerkingen == null)
                productieListControl1.InitProductie(true, true, true, true, true, true);
            else
                productieListControl1.InitProductie(_bewerkingen, true, true, false);
            UpdateListName();
            productieListControl1.InitEvents();
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
            productieListControl1.SaveColumns(true);
            productieListControl1.DetachEvents();
            Manager.FilterChanged -= Manager_FilterChanged;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}