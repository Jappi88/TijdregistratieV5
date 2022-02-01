using System;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace Forms
{
    public partial class LijstWeergaveForm : MetroFramework.Forms.MetroForm
    {
        public LijstWeergaveForm()
        {
            InitializeComponent();
            ((OLVColumn) xlistview.Columns[0]).AspectGetter = item => item.ToString();
        }

        public string[] ViewedData
        {
            get => xlistview.Objects?.Cast<string>().ToArray();
            set => xlistview.SetObjects(value);
        }

        public bool AllowAddItem
        {
            get => xAddPanel.Visible;
            set=> xAddPanel.Visible = value;
        }

        public bool AllowOpslaan
        {
            get => xOpslaan.Visible;
            set => xOpslaan.Visible = value;
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xOpslaan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void xlistview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xlistview.SelectedObjects?.Count > 0)
            {
                if (xlistview.SelectedObjects[0] is string value)
                    xviewtext.Text = value;
                xremove.Enabled = true;
            }
            else
            {
                xviewtext.Text = "";
                xremove.Enabled = false;
            }
        }

        private void xadd_Click(object sender, EventArgs e)
        {
            var txt = xviewtext.Text;
            if (string.IsNullOrEmpty(txt))
            {
                XMessageBox.Show(
                    this, "Je hebt geen waarde ingevuld om toe te voegen!\nVul in een waarde dat je wilt toevoegen.",
                    "Ongeldig", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                var values = xlistview.Objects?.Cast<string>();
                if (values == null)
                {
                    xlistview.SetObjects(new[] {txt});
                }
                else if (values.Any(x => string.Equals(txt, x, StringComparison.CurrentCultureIgnoreCase)))
                {
                    XMessageBox.Show(this, $"'{txt}' Bestaat al en kan niet nogmaals worden toegevoegd.", "Bestaat Al",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    xlistview.AddObject(txt);
                    xlistview.SelectedObject = txt;
                    xlistview.SelectedItem?.EnsureVisible();
                    OnItemAdded(txt);
                }
            }
        }

        private void xremove_Click(object sender, EventArgs e)
        {
            var values = xlistview.SelectedObjects?.Cast<string>().ToArray();
            if (values == null || values.Length == 0) return;
            var xkey = values.Length == 1 ? values[0] : $"{values.Length} selecties";
            if (XMessageBox.Show(this, $"Weetje zeker dat je {xkey} wilt verwijderen?", "Verwijderen",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                xlistview.RemoveObjects(values);
                OnItemsRemoved(values);
            }
        }

        public void OnItemsChanged(object items, EventArgs e)
        {
            try
            {
                if (items is string[] xitems)
                {
                    if (this.InvokeRequired)
                        this.Invoke(new MethodInvoker(() => ViewedData = xitems));
                    else ViewedData = xitems;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public event EventHandler ItemAdded;

        protected virtual void OnItemAdded(string item)
        {
            ItemAdded?.Invoke(item, EventArgs.Empty);
        }

        public event EventHandler ItemRemoved;

        protected virtual void OnItemsRemoved(string[] items)
        {
            ItemRemoved?.Invoke(items, EventArgs.Empty);
        }
    }
}