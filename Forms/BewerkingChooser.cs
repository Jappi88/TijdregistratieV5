using System;
using System.Linq;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class BewerkingChooser : MetroBaseForm
    {
        public BewerkingChooser(string[] items)
        {
            InitializeComponent();
            xbewerkingen.Items.AddRange(items.Union(items).Select(x => (object) x).ToArray());
            if (xbewerkingen.Items.Count > 0)
                xbewerkingen.SelectedItem = xbewerkingen.Items[0];
        }

        public string SelectedItem => xbewerkingen.SelectedItem?.ToString();

        public string Title
        {
            get => Text;
            set => Text = value;
        }

        private void xok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void xanuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}