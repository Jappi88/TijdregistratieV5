using Forms;
using Forms.MetroBase;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProductieManager.Forms
{
    public partial class FilterEditor : MetroBaseForm
    {
        public List<Filter> Filters { get; set; } = new List<Filter>();

        public FilterEditor()
        {
            InitializeComponent();
            if (Manager.Opties?.Filters != null && Manager.Opties.Filters.Count > 0)
            {
                Filters = Manager.Opties.Filters.CreateCopy();
            }
            xfilternaam.CustomButton.Click += CustomButton_Click;
            InitFilters();
        }

        private void xfilternaam_Enter(object sender, EventArgs e)
        {
            if (xfilternaam.Text.Trim().ToLower() == "filter naam...")
                xfilternaam.Text = "";
        }

        private void xfilternaam_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xfilternaam.Text.Trim()))
                xfilternaam.Text = "Filter naam...";
        }

        private void CustomButton_Click(object sender, System.EventArgs e)
        {
            AddFilter(xfilternaam.Text.Trim());
        }

        private void AddFilter(string name)
        {
            if (CheckForFilter(name))
            {
                Filter xfilter = new Filter() { Name = xfilternaam.Text.Trim() };
                Filters.Add(xfilter);
                xfilterlijst.AddObject(xfilter);
                xfilterlijst.SelectedObject = xfilter;
                xfilterlijst.SelectedItem?.EnsureVisible();
            }
        }

        private bool CheckForFilter(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                XMessageBox.Show(this, $"Filter naam kan niet leeg zijn!\n\nVul in een geldige filternaam en probeer het opnieuw.", "Bestaat Al", MessageBoxIcon.Exclamation);
                return false;
            }
            if (Filters.Any(x =>
                string.Equals(x.Name, value, StringComparison.CurrentCultureIgnoreCase)))
            {
                XMessageBox.Show(this, $"{value} bestaat al!", "Bestaat Al", MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void xfilterlijst_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            xwijzigfilter.Enabled = xfilterlijst.SelectedObjects.Count > 0;
            xdeletefilter.Enabled = xfilterlijst.SelectedObjects.Count > 0;
            var xfilter = xfilterlijst.SelectedObjects.Count > 0 ? (Filter)xfilterlijst.SelectedObjects[0] : null;
            if(xfilter == null)
                InitCriterias(null);
            else InitCriterias(xfilter);
        }

        private void xwijzigfilter_Click(object sender, System.EventArgs e)
        {
            if (xfilterlijst.SelectedItems.Count == 0) return;
            var xitem = (Filter)xfilterlijst.SelectedObjects[0];
            if (xitem == null) return;
            var tf = new TextFieldEditor {Title = "Wijzig filter naam", MultiLine = false, SelectedText = xitem.Name};
            if (tf.ShowDialog(this) == DialogResult.OK)
            {
                if (CheckForFilter(tf.SelectedText.Trim()))
                {
                    xitem.Name = tf.SelectedText.Trim();
                    xfilterlijst.RefreshObject(xitem);
                    xfilterlijst.SelectedObject = xitem;
                    xfilterlijst.SelectedItem?.EnsureVisible();
                    xfilterlijst.Invalidate();
                }
            }
        }

        private void xdeletefilter_Click(object sender, System.EventArgs e)
        {
            DeleteSelectedFilters();
        }

        private void DeleteSelectedFilters()
        {
            if (xfilterlijst.SelectedItems.Count == 0) return;
            if (XMessageBox.Show(this, $"Weetje zeker dat je alle geselecteerde filters wilt verwijderen?!",
                "Filters Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                var xselected = xfilterlijst.SelectedObjects.Cast<Filter>().ToArray();
                xfilterlijst.RemoveObjects(xselected);
                foreach (var xfilter in xselected)
                        Filters.Remove(xfilter);

                xfilterlijst.Invalidate();
            }
        }

        private void InitFilters()
        {
            try
            {
                xfilterlijst.SetObjects(Filters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private Filter SelectedFilter()
        {
            if (xfilterlijst.SelectedItems.Count == 0) return null;
            return xfilterlijst.SelectedObject as Filter;
        }

        private void InitCriterias(Filter filter)
        {
            try
            {
                filterEntryEditorUI1.LoadFilterEntries(typeof(Bewerking), filter,false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

       
        private void xfilterlijst_DoubleClick(object sender, EventArgs e)
        {
            xwijzigfilter_Click(sender, e);
        }

        private void xopslaan_Click(object sender, EventArgs e)
        {
            if (Manager.Opties != null)
            {
                Manager.Opties.Filters = Filters;
                Manager.Opties.Save("Filters aangepast", false, true, true);
            }

            DialogResult = DialogResult.OK;
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void filterEntryEditorUI1_CriteriasChanged(object sender, EventArgs e)
        {
            if (Filters == null) return;
            var xindex = Filters.IndexOf(filterEntryEditorUI1.SelectedFilter);
            if (xindex > -1)
                Filters[xindex].Filters = filterEntryEditorUI1.Criterias;
        }
    }
}
