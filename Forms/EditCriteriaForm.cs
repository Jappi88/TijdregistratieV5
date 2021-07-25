using System.Collections.Generic;
using System.Windows.Forms;
using Rpm.Misc;
using Rpm.Various;

namespace Forms
{
    public partial class EditCriteriaForm : MetroFramework.Forms.MetroForm
    {
        public List<FilterEntry> SelectedFilter { get; private set; }

        public EditCriteriaForm(List<FilterEntry> filters)
        {
            InitializeComponent();
            filterEntryEditorUI1.LoadFilterEntries(filters,true);
            SelectedFilter = new List<FilterEntry>();
        }

        private void xannuleren_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xok_Click(object sender, System.EventArgs e)
        {
            SelectedFilter = filterEntryEditorUI1.Criterias;
            DialogResult = DialogResult.OK;
        }
    }
}
