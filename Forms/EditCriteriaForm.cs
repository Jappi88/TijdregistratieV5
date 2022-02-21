using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Misc;

namespace Forms
{
    public partial class EditCriteriaForm : MetroBaseForm
    {
        public EditCriteriaForm(Type type, List<FilterEntry> filters)
        {
            InitializeComponent();
            filterEntryEditorUI1.LoadFilterEntries(type, filters, true);
            SelectedFilter = new List<FilterEntry>();
        }

        public List<FilterEntry> SelectedFilter { get; private set; }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xok_Click(object sender, EventArgs e)
        {
            SelectedFilter = filterEntryEditorUI1.Criterias;
            DialogResult = DialogResult.OK;
        }
    }
}