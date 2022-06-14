
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Linq;
using System.Windows.Forms;
using Various;

namespace Forms
{
    public partial class ZoekForm : MetroBase.MetroBaseForm
    {
        public ZoekForm()
        {
            InitializeComponent();
            Initgeavanceerd();
            xcriteria.ShowClearButton = true;
            if (Manager.BewerkingenLijst == null) return;
            var bewerkingen = Manager.BewerkingenLijst.GetAllEntries().Select(x => (object)x.Naam).ToArray();
            var afdelingen = Manager.BewerkingenLijst.GetAlleWerkplekken().Select(x => (object)x).ToArray();
            var stats = Enum.GetNames(typeof(ViewState)).ToArray();
            xwerkplekken.Items.AddRange(afdelingen);
            xbewerkingen.Items.AddRange(bewerkingen);
            xstatuscombo.Items.AddRange(stats);
            Invalidate();
            xcriteria.Select();
        }

        private bool DoCheck()
        {
            if (xwerkplekcheck.Checked && string.IsNullOrEmpty(xwerkplekken.Text.Trim()))
                XMessageBox.Show(
                    this, "Werkplekken is aangevinkt, maar het veld is leeg!\nvul in of kies werkplek en probeer het opniew.",
                    "Werkplekken", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (xbewerkingcheck.Checked && string.IsNullOrEmpty(xbewerkingen.Text.Trim()))
                XMessageBox.Show(
                    this, "Bewerking is aangevinkt, maar het veld is leeg!\nvul in of kies een bewerking en probeer het opniew.",
                    "Bewerking", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (xmateriaalcheckbox.Checked && string.IsNullOrWhiteSpace(xmateriaalcriteria.Text.Trim()))
                XMessageBox.Show(
                    this, "Materialen aangevinkt, maar het veld is leeg!\nVul in een criteria waar een materiaal aan moet voldoen",
                    "Criteria", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                return true;

            return false;
        }

        private void Initgeavanceerd()
        {
            if (xgeavanceerd.Checked)
            {
                xgeavanceerdpannel.Visible = true;
                this.MinimumSize = new System.Drawing.Size(this.MinimumSize.Width, 440);
            }
            else
            {
                xgeavanceerdpannel.Visible = false;
                this.MinimumSize = new System.Drawing.Size(this.MinimumSize.Width, 230);
            }
            this.Size = new System.Drawing.Size(this.Width, this.MinimumSize.Height);
        }

        private void xgeavanceerd_CheckedChanged(object sender, System.EventArgs e)
        {
            Initgeavanceerd();
        }

        public RangeFilter GetFilter()
        {
            var f = new RangeFilter();
            f.Enabled = true;
            f.VanafCheck = xvanafcheck.Checked;
            f.VanafTime = xvanafdate.Value;
            f.TotCheck = xtotcheck.Checked;
            f.TotTime = xtotdate.Value;
            f.Bewerking = xbewerkingcheck.Checked ? xbewerkingen.Text.Trim() : null;
            f.werkPlek = xwerkplekcheck.Checked ? xwerkplekken.Text.Trim() : null;
            f.Criteria = string.IsNullOrEmpty(xcriteria.Text.Trim()) ? null:  xcriteria.Text.Trim();
            f.MaterialenCriteria = xmateriaalcheckbox.Checked ? xmateriaalcriteria.Text.Trim() : null;
            f.State = xstatuscheckbox.Checked ? xstatuscombo.Text.Trim() : null;
            return f;
        }

        public void SetFilter(RangeFilter filter)
        {
            xcriteria.Select();
            if (!filter.Enabled) return;
            var x = filter;
            xvanafcheck.Checked = x.VanafCheck;
            xtotcheck.Checked = x.TotCheck;
            bool flag = xgeavanceerd.Checked;
           
            if (!string.IsNullOrEmpty(x.Criteria))
            {
                xcriteria.Text = x.Criteria;
            }

            if (!string.IsNullOrEmpty(x.State))
            {
                xstatuscombo.SelectedItem = x.State;
                xstatuscheckbox.Checked = true;
                flag = true;
                xstatuscombo.Select();
            }
            else xstatuscheckbox.Checked = false;

            if (!string.IsNullOrEmpty(x.werkPlek))
            {
                xwerkplekken.SelectedItem = x.werkPlek;
                xwerkplekcheck.Checked = true;
                flag = true;
                xwerkplekken.Select();
            }
            else xwerkplekcheck.Checked = false;
            if (x.VanafCheck)
            {
                xvanafdate.SetValue(x.VanafTime);
                flag = true;
            }
            if (x.TotCheck)
            {
                xtotdate.SetValue(x.TotTime);
                flag = true;
            }
            if (!string.IsNullOrEmpty(x.Bewerking))
            {
                xbewerkingen.SelectedItem = x.Bewerking;
                xbewerkingcheck.Checked = true;
                xbewerkingen.Select();
                flag = true;
            }
            else xbewerkingcheck.Checked = false;
            if (!string.IsNullOrEmpty(x.MaterialenCriteria))
            {
                xmateriaalcriteria.Text = x.MaterialenCriteria;
                xmateriaalcheckbox.Checked = true;
                xmateriaalcriteria.Select();
                flag = true;
            }
            else xmateriaalcheckbox.Checked = false;
            xgeavanceerd.Checked = flag;
        }

        private void xzoeken_Click(object sender, System.EventArgs e)
        {
            if (DoCheck())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void xartikelnrcheck_CheckedChanged(object sender, EventArgs e)
        {
            xmateriaalcriteria.Enabled = xmateriaalcheckbox.Checked;
        }

        private void xwerkplekcheck_CheckedChanged(object sender, EventArgs e)
        {
            xwerkplekken.Enabled = xwerkplekcheck.Checked;
        }

        private void xvanafcheck_CheckedChanged(object sender, EventArgs e)
        {
            xvanafdate.Enabled = xvanafcheck.Checked;
        }

        private void xbewerkingcheck_CheckedChanged(object sender, EventArgs e)
        {
            xbewerkingen.Enabled = xbewerkingcheck.Checked;
        }

        private void xtotcheck_CheckedChanged(object sender, EventArgs e)
        {
            xtotdate.Enabled = xtotcheck.Checked;
        }

        private void xcriteria_TextChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void xcriteria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                DialogResult = DialogResult.OK;
            }
        }

        private void xstatuscheckbox_CheckedChanged(object sender, EventArgs e)
        {
            xstatuscombo.Enabled = xstatuscheckbox.Checked;
        }
    }
}
