using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Rpm.Productie;
using Rpm.SqlLite;

namespace Forms
{
    public partial class DbBewerkingChanger : MetroFramework.Forms.MetroForm
    {
       // private readonly List<BewerkingEntry> _deletedentries = new();
        private List<BewerkingEntry> _entries = new();

        public DbBewerkingChanger()
        {
            InitializeComponent();
            ((OLVColumn) xpleklist.Columns[0]).AspectGetter = item => (string) item;
            ((OLVColumn) xbewlist.Columns[0]).AspectGetter = NameGetter;
            ((OLVColumn) xbewlist.Columns[1]).AspectGetter = item => ((BewerkingEntry) item).IsBemand ? "Ja" : "Nee";
            LoadBewerkingen();
        }

        private object NameGetter(object item)
        {
            var xitem = (BewerkingEntry) item;
            if (xitem == null) return "N.V.T";
            if (!string.IsNullOrEmpty(xitem.NewName))
                return xitem.NewName;
            return xitem.Naam;
        }

        private void LoadBewerkingen()
        {
            if (Manager.BewerkingenLijst == null)
                return;
            _entries = Manager.BewerkingenLijst.GetAllEntries();
            xbewlist.SetObjects(_entries);
        }

        private void SaveBewerkingen()
        {
            if (Manager.BewerkingenLijst == null)
                return;
            //foreach (var ent in _entries) Manager.BewerkingenLijst.UpdateEntry(ent);
            //foreach (var ent in _deletedentries) Manager.BewerkingenLijst.DeleteEntry(ent.Naam);
            Manager.BewerkingenLijst.WriteEntries();
            _ = Manager.BewerkingenLijst.UpdateDatabase();
        }

        private BewerkingEntry CurrentBew()
        {
            return (BewerkingEntry) xbewlist.SelectedObject;
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xisbemand_CheckedChanged(object sender, EventArgs e)
        {
            var cur = CurrentBew();
            if (cur == null)
            {
                xisbemand.Checked = false;
            }
            else
            {
                cur.IsBemand = xisbemand.Checked;
                xbewlist.RefreshObject(cur);
            }
        }

        private void xaddbew_Click(object sender, EventArgs e)
        {
            ChangeBew(true);
        }

        private void ChangeBew(bool addnew)
        {
            if (xbewerkingnaam.Text.Trim().Length < 4)
            {
                XMessageBox.Show(
                    this, $"'{xbewerkingnaam.Text}' is ongeldig!\nGebruik een geldige naam dat bestaat uit minimaal 4 characters.",
                    "Ongeldig", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (_entries.Any(x =>
                string.Equals(x.Naam, xbewerkingnaam.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(x.NewName, xbewerkingnaam.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)))
            {
                XMessageBox.Show(
                    this, $"'{xbewerkingnaam.Text}' Bestaat al!\nGebruik een andere naam dat nog niet is toegevoegd.",
                    "Bestaat al", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                BewerkingEntry ent;
                if (addnew)
                {
                    ent = new BewerkingEntry(xbewerkingnaam.Text.Trim(), xisbemand.Checked);
                    _entries.Add(ent);
                    xbewlist.AddObject(ent);
                }
                else
                {
                    ent = CurrentBew();
                    if (ent != null)
                    {
                        ent.NewName = xbewerkingnaam.Text.Trim();
                        xbewlist.RefreshObject(ent);
                    }
                }

                xbewlist.SelectedObject = ent;
                xbewlist.SelectedItem?.EnsureVisible();
            }
        }

        private void xdelbew_Click(object sender, EventArgs e)
        {
            if (xbewlist.SelectedObjects.Count > 0)
                if (XMessageBox.Show(this, $"Je staat op het punt alle geselecteerde bewerkingen te verwijderen...\n" +
                                     "Weetje zeker dat je door wilt gaan?!", "Verwijderen", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (var x in xbewlist.SelectedObjects.Cast<BewerkingEntry>())
                    {
                        _entries.Remove(x);
                        //_deletedentries.Add(x);
                        xbewlist.RemoveObject(x);
                    }

                    SetBewEnable();
                }
        }

        private void SetBewEnable()
        {
            bool empty = xbewlist.SelectedObjects.Count == 0;
            xdelbew.Enabled = !empty;
            if (empty)
                xpleklist.SetObjects(new string[] { });
            SetWPEnable();
        }

        private void SetWPEnable()
        {
            xdelwerkplek.Enabled = xdelbew.Enabled && xpleklist.SelectedObjects.Count > 0;
        }

        private void xaddwerkplek_Click(object sender, EventArgs e)
        {
            var cur = CurrentBew();
            if (cur == null) return;
            if (xwerkpleknaam.Text.Trim().Length < 4)
            {
                XMessageBox.Show(
                    this, $"'{xwerkpleknaam.Text}' is ongeldig!\nGebruik een geldige naam dat bestaat uit minimaal 4 characters.",
                    "Ongeldig Werkplek", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (cur.WerkPlekken.Any(wp =>
                string.Equals(wp, xwerkpleknaam.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)))
            {
                XMessageBox.Show(
                    this, $"'{xwerkpleknaam.Text}' Bestaat al!\nGebruik een andere naam dat nog niet is toegevoegd.",
                    "Werkplek Bestaat al", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                cur.WerkPlekken.Add(xwerkpleknaam.Text.Trim());
                xpleklist.AddObject(xwerkpleknaam.Text.Trim());
                xpleklist.SelectedObject = xwerkpleknaam.Text.Trim();
                xpleklist.SelectedItem?.EnsureVisible();
            }
        }

        private void xdelwerkplek_Click(object sender, EventArgs e)
        {
            var cur = CurrentBew();
            if (cur == null) return;
            if (xpleklist.SelectedObjects.Count > 0)
                if (XMessageBox.Show(this, $"Je staat op het punt alle geselecteerde werkplekken te verwijderen...\n" +
                                     "Weetje zeker dat je door wilt gaan?!", "Verwijderen", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (var x in xpleklist.SelectedObjects.Cast<string>())
                    {
                        cur.WerkPlekken.Remove(x);
                        xpleklist.RemoveObject(x);
                    }

                    SetBewEnable();
                }
        }

        private void EditWerkplek()
        {
            var cur = CurrentBew();
            if (cur == null) return;
            var curwp = (string) xpleklist.SelectedObject;
            if (curwp == null) return;
            if (xwerkpleknaam.Text.Trim().Length < 4)
            {
                XMessageBox.Show(
                    this, $"'{xwerkpleknaam.Text}' is ongeldig!\nGebruik een geldige naam dat bestaat uit minimaal 4 characters.",
                    "Ongeldig Werkplek", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (cur.WerkPlekken.Any(wp =>
                string.Equals(wp, xwerkpleknaam.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)))
            {
                XMessageBox.Show(
                    this, $"'{xwerkpleknaam.Text}' Bestaat al!\nGebruik een andere naam dat nog niet is toegevoegd.",
                    "Werkplek Bestaat al", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                cur.WerkPlekken.Remove(curwp);
                cur.WerkPlekken.Add(xwerkpleknaam.Text);
                xpleklist.RemoveObject(curwp);
                xpleklist.AddObject(xwerkpleknaam.Text);
                xpleklist.SelectedObject = xwerkpleknaam.Text;
                xpleklist.SelectedItem?.EnsureVisible();
            }
        }

        private void xbewlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cur = CurrentBew();
            if (cur == null) return;
            xisbemand.Checked = cur.IsBemand;
            xbewerkingnaam.Text = cur.NewName ?? cur.Naam;
            xpleklist.SetObjects(cur.WerkPlekken);
            if (cur.WerkPlekken.Count > 0)
                xpleklist.SelectedObject = cur.WerkPlekken[0];
            SetBewEnable();
        }

        private void xpleklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cur = CurrentBew();
            if (cur == null) return;
            var curwp = (string) xpleklist.SelectedObject;
            if (curwp == null) return;
            xwerkpleknaam.Text = curwp;
            SetWPEnable();
        }

        private void xOpslaan_Click(object sender, EventArgs e)
        {
            SaveBewerkingen();
            DialogResult = DialogResult.OK;
        }

        private void xeditbew_Click(object sender, EventArgs e)
        {
            ChangeBew(false);
        }

        private void xeditplek_Click(object sender, EventArgs e)
        {
            EditWerkplek();
        }
    }
}