using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Rpm.Opmerking;

namespace Forms
{
    public partial class OpmerkingEditor : MetroFramework.Forms.MetroForm
    {
        private OpmerkingEntry _Selected;
        public OpmerkingEntry SelectedEntry
        {
            get => _Selected;
            set => InitEntry(value);
        }

        private void InitEntry(OpmerkingEntry entry)
        {
            _Selected = entry ?? new OpmerkingEntry();
            xtitle.Text = string.IsNullOrEmpty(_Selected.Title?.Trim()) ? "Vul in een onderwerp..." : _Selected.Title.Trim();
            if (_Selected.Ontvangers == null || _Selected.Ontvangers.Count == 0)
                xontvangers.SelectedIndex = 1;
            else
                xontvangers.Text = string.Join(";", _Selected.Ontvangers);
            xopmerking.Text = string.IsNullOrEmpty(_Selected.Opmerking?.Trim()) ? "Vul in een vraag, opmerking of een verzoek..." : _Selected.Opmerking.Trim();
            xgeplaatstOpLabel.Text = _Selected.GeplaatstOp.ToString(CultureInfo.CurrentCulture);
        }

        public OpmerkingEditor(OpmerkingEntry entry = null)
        {
            InitializeComponent();
            InitEntry(entry);
        }

        private void xOpslaan_Click(object sender, System.EventArgs e)
        {
            var ont = xontvangers.Text.Trim().Split(';').Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (xtitle.Text.Trim().Length < 4 || string.Equals(xtitle.Text.Trim(), "vul in een onderwerp...", StringComparison.CurrentCultureIgnoreCase))
                XMessageBox.Show("Vul in een geldige Title a.u.b.", "Ongeldige Title", MessageBoxIcon.Warning);
            else if(string.Equals(xontvangers.Text, xontvangers.Items[1].ToString(),
                StringComparison.CurrentCultureIgnoreCase) || ont.Count == 0)
                XMessageBox.Show("Vul in minimaal 1 ontvanger a.u.b.", "Ongeldige Ontvanger(s)", MessageBoxIcon.Warning);
            else if (xopmerking.Text.Trim().Length < 8 || string.Equals(xopmerking.Text, "Vul in een vraag, opmerking of een verzoek...",
                StringComparison.CurrentCultureIgnoreCase))
                XMessageBox.Show("Vul in een geldige Opmerking a.u.b.", "Ongeldige Opmerking", MessageBoxIcon.Warning);
            else
            {
                _Selected.SetOpmerking(xtitle.Text.Trim(), xopmerking.Text.Trim(), ont);
                DialogResult = DialogResult.OK;
            }
        }

        private void xannuleren_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xtitle_Enter(object sender, System.EventArgs e)
        {
            if (string.Equals(xtitle.Text.Trim(), "vul in een onderwerp...", StringComparison.CurrentCultureIgnoreCase))
                xtitle.Text = "";
        }

        private void xtitle_Leave(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(xtitle.Text.Trim()))
                xtitle.Text = @"Vul in een onderwerp...";
        }

        private void xontvangers_Enter(object sender, System.EventArgs e)
        {
            if (string.Equals(xontvangers.Text, xontvangers.Items[1].ToString(),
                StringComparison.CurrentCultureIgnoreCase))
            {
                xontvangers.Text = "";
            }
        }

        private void xontvangers_Leave(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(xontvangers.Text.Trim()))
            {
                xontvangers.SelectedIndex = 1;
                xontvangers.Text = xontvangers.Items[1].ToString();
            }
        }

        private void xopmerking_Enter(object sender, System.EventArgs e)
        {
            if (string.Equals(xopmerking.Text, "Vul in een vraag, opmerking of een verzoek...",
                StringComparison.CurrentCultureIgnoreCase))
            {
                xopmerking.Text = "";
            }
        }

        private void xopmerking_Leave(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(xopmerking.Text.Trim()))
                xopmerking.Text = @"Vul in een vraag, opmerking of een verzoek...";
        }
    }
}
