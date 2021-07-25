using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using Forms;
using ProductieManager.Forms;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Controls
{
    public partial class FilterEntryEditorUI : UserControl
    {
        private List<FilterEntry> _Criterias = new List<FilterEntry>();
        public List<FilterEntry> Criterias => _Criterias;
        private bool _UseOperand;

        public FilterEntryEditorUI()
        {
            InitializeComponent();
        }

        public void LoadFilterEntries(List<FilterEntry> entries, bool useOperand)
        {
            try
            {
                _UseOperand = useOperand;
                InitVariablen();
                InitCriterias(entries);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public FilterEntry SelectedFilterEntry()
        {
            return xcriterialijst.SelectedObject as FilterEntry;
        }

        public string CreateHtmlFromList()
        {
            if (_Criterias == null || _Criterias.Count == 0) return string.Empty;
            return string.Join("\n", _Criterias.Select(x => x.ToHtmlString()));
        }

        private void DeleteSelectedCriterias()
        {
            if (xcriterialijst.SelectedItems.Count == 0) return;
            if (XMessageBox.Show("Weetje zeker dat je alle geselecteerde criteria's wilt verwijderen?!",
                "Filters Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                var crits = xcriterialijst.SelectedObjects.Cast<FilterEntry>().ToArray();
                xcriterialijst.RemoveObjects(crits);
                foreach (var xcrit in crits)
                    _Criterias.Remove(xcrit);

                UpdateCriterias();
                xcriterialijst.Invalidate();
            }
        }

        private void InitVariablen()
        {
            try
            {
                xvariablelijst.Items.Clear();
                var properties = typeof(Bewerking).GetProperties().Where(x => x.CanRead && x.PropertyType.IsSupportedType()).ToArray();
                xvariablelijst.SetObjects(properties);
                xvariablelijst.Sort(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void InitCriterias(List<FilterEntry> criterias)
        {
            try
            {
                _Criterias = criterias;
                if (criterias == null)
                {
                    xcriterialijst.SetObjects(new FilterEntry[] { });
                    return;
                }
                xcriterialijst.SetObjects(criterias);
                UpdateHtmlCriteria();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xaddcriteria_Click(object sender, EventArgs e)
        {
            AddCriteria();
        }

        private void AddCriteria()
        {
            if (xvariablelijst.SelectedItems.Count == 0) return;
            var xvar = xvariablelijst.SelectedItems[0].Text;
            var xnewcrit = new NewFilterEntry(xvar, xcriterialijst.Items.Count > 0 || _UseOperand) { Title = $"Nieuwe regel voor {xvar}" };

            if (xnewcrit.ShowDialog() == DialogResult.OK)
            {
                _Criterias.Add(xnewcrit.SelectedFilter);
                xcriterialijst.AddObject(xnewcrit.SelectedFilter);
                xcriterialijst.SelectedObject = xnewcrit.SelectedFilter;
                xcriterialijst.SelectedItem?.EnsureVisible();
            }

            UpdateHtmlCriteria();
        }

        private void xwijzigfilterregel_Click(object sender, EventArgs e)
        {
            WijzigCriteria();
        }

        private void WijzigCriteria()
        {
            var xfilter = SelectedFilterEntry();
            if (xfilter == null) return;
            int index = _Criterias.IndexOf(xfilter);
            if (index > -1)
            {
                var xnewcrit = new NewFilterEntry(xfilter) {Title = $"Wijzig regel voor {xfilter.PropertyName}"};
                if (xnewcrit.ShowDialog() == DialogResult.OK)
                {


                    _Criterias[index] = xnewcrit.SelectedFilter;
                    //xfilter.Filters.Remove(xvar);
                    //xvar = xnewcrit.SelectedFilter;
                    //xvar.OldOperandType = xvar.OperandType;
                    //xfilter.Filters.Insert(index, xnewcrit.SelectedFilter);
                    xcriterialijst.RefreshObject(xnewcrit.SelectedFilter);
                    UpdateCriterias();
                }
            }
        }

        private void xregelup_Click(object sender, EventArgs e)
        {
            var xfilter = SelectedFilterEntry();
            if (xfilter == null) return;
            if (xcriterialijst.SelectedIndex == 0) return;
            var index = xcriterialijst.SelectedIndex - 1;
            _Criterias.Remove(xfilter);
            _Criterias.Insert(index, xfilter);
            xcriterialijst.RemoveObject(xfilter);
            xcriterialijst.InsertObjects(index, new FilterEntry[] { xfilter });
            xcriterialijst.SelectedObject = xfilter;
            xcriterialijst.SelectedItem?.EnsureVisible();
            UpdateCriterias();
        }

        private void UpdateCriterias()
        {
            if (_UseOperand) return;
            bool first = true;
            var xfilters = xcriterialijst.Objects.Cast<FilterEntry>().ToList();
            var filters = new List<FilterEntry>();
            foreach (var entry in xfilters)
            {
                if (first)
                {
                    entry.OperandType = Operand.ALS;
                    first = false;
                }
                else if (entry.OperandType == Operand.ALS)
                {
                    if (entry.OldOperandType == Operand.ALS)
                        entry.OperandType = Operand.OF;
                    else entry.OperandType = entry.OldOperandType;
                }

                xcriterialijst.RefreshObject(entry);
                filters.Add(entry);
            }
            InitCriterias(filters);
        }

        private void UpdateHtmlCriteria()
        {
            xcriteriahtml.Text = CreateHtmlFromList();
        }

        private void xregeeldown_Click(object sender, EventArgs e)
        {
            if (xcriterialijst.SelectedItems.Count == 0) return;
            var xvar = SelectedFilterEntry();
            if (xcriterialijst.SelectedIndex >= xcriterialijst.Items.Count - 1 || xvar == null) return;
            var index = xcriterialijst.SelectedIndex + 1;
            xcriterialijst.RemoveObject(xvar);
            xcriterialijst.InsertObjects(index, new FilterEntry[] { xvar });
            xcriterialijst.SelectedObject = xvar;
            xcriterialijst.SelectedItem?.EnsureVisible();
            UpdateCriterias();
        }

        private void xdeletefilterregel_Click(object sender, EventArgs e)
        {
            DeleteSelectedCriterias();
        }

        private void xvariablelijst_DoubleClick(object sender, EventArgs e)
        {
            AddCriteria();
        }

        private void xcriterialijst_DoubleClick(object sender, EventArgs e)
        {
            WijzigCriteria();
        }

        private void xvariablelijst_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            xaddcriteria.Enabled = xvariablelijst.SelectedItems.Count > 0;
        }

        private void xcriterialijst_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            xwijzigfilterregel.Enabled = xcriterialijst.SelectedItems.Count > 0;
            xdeletefilterregel.Enabled = xcriterialijst.SelectedItems.Count > 0;
            xregelup.Enabled = xcriterialijst.SelectedItems.Count > 0;
            xregeeldown.Enabled = xcriterialijst.SelectedItems.Count > 0;
        }
    }
}
