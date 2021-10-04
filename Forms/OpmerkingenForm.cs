using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Opmerking;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Rpm.Mailing;
using Rpm.Various;

namespace Forms
{
    public partial class OpmerkingenForm : MetroFramework.Forms.MetroForm
    {
        public List<OpmerkingEntry> Opmerkingen { get; set; }

        public OpmerkingenForm()
        {
            InitializeComponent();
            xopmerkingimages.Images.Add(Resources.notes_office_page_papers_32x32);
            xopmerkingimages.Images.Add(
                Resources.note_office_page_paper_32x32);
            xopmerkingimages.Images.Add(
                Resources.note_office_page_paper_32x32.CombineImage(Resources.new_25355, 1.5));
            xopmerkingimages.Images.Add(Resources.noteuser_general_office_tie_32x32);
            xopmerkingimages.Images.Add(Resources.notetime_general_office_management_32x32);
            xopmerkingimages.Images.Add(Resources.notedescription_office_page_paper_107752);
            xopmerkingimages.Images.Add(Resources.noterespond_general_32x32);
            xopmerkingimages.Images.Add(Resources.noteuser_general_office_tie_32x32);
            xopmerkingimages.Images.Add(
                Resources.noteuser_general_office_tie_32x32.CombineImage(Resources.new_25355, 1.5));
            xopmerkingimages.Images.Add(Resources.notekey_office_32x32);
            xOpmerkingenTree.SelectedImageIndex = xopmerkingimages.Images.Count - 1;
        }

        public void LoadOpmerkingen(object selected = null)
        {
            try
            {
                Opmerkingen ??= Manager.Opmerkingen?.GetAvailibleNotes()?.CreateCopy();
                Opmerkingen ??= new List<OpmerkingEntry>();
                selected ??= xOpmerkingenTree.SelectedNode?.Tag;
                xOpmerkingenTree.BeginUpdate();
                xOpmerkingenTree.Nodes.Clear();
                var xroot = new TreeNode($"Opmerkingen[{Opmerkingen.Count}]");
                xroot.Tag = Opmerkingen;
                xroot.ImageIndex = 0;
                xroot.SelectedImageIndex = 0;
                foreach (var op in Opmerkingen)
                {
                    var tn = new TreeNode(op.Title)
                    {
                        Tag = op,
                        ImageIndex = op.IsGelezen ? 1 : 2,
                        SelectedImageIndex = op.IsGelezen ? 1 : 2
                    };

                    //voeg de tijd waarop is geplaatst
                    AddNode(tn, "Geplaatst door " + op.Afzender, 3, op);
                    AddNode(tn, "Geplaatst op " + op.GeplaatstOp.ToString(CultureInfo.CurrentCulture), 4, op);
                    AddNode(tn, op.Opmerking, 5, op);

                    var tn1 = new TreeNode($"Reacties[{op.Reacties.Count}]")
                    {
                        Tag = op.Reacties,
                        ImageIndex = 6,
                        SelectedImageIndex = 6
                    };
                    foreach (var rea in op.Reacties)
                    {
                        var reatn = new TreeNode(rea.ReactieVan)
                        {
                            Tag = rea,
                            ImageIndex = rea.IsGelezen ? 7 : 8,
                            SelectedImageIndex = rea.IsGelezen ? 7 : 8
                        };
                        AddNode(reatn,
                            "Gelezen op " + rea.GelezenOp.ToString(CultureInfo.CurrentCulture), 4, rea);
                        if (!string.IsNullOrEmpty(rea.Reactie?.Trim()))
                        {
                            AddNode(reatn,
                                "Reactie op " + rea.ReactieOp.ToString(CultureInfo.CurrentCulture), 4, rea);
                            AddNode(reatn,
                                rea.Reactie, 5, rea);
                        }
                        tn1.Nodes.Add(reatn);
                    }

                    tn.Nodes.Add(tn1);
                    xroot.Nodes.Add(tn);
                }

                xOpmerkingenTree.Nodes.Add(xroot);
                xOpmerkingenTree.TopNode = xroot;
                if (selected != null)
                {
                    TreeNode xtn = null;

                    if (selected is OpmerkingEntry xent)
                    {
                        xtn =
                            xOpmerkingenTree.Nodes[0].Nodes.Cast<TreeNode>().FirstOrDefault(x =>
                                string.Equals(xent.Title, x.Text, StringComparison.CurrentCultureIgnoreCase));
                    }
                    else if (selected is TreeNode tn)
                    {
                        xtn = tn;
                    }
                    else if (selected is ReactieEntry reactie)
                    {
                        if (xselectedopmerkingpanel.Tag is OpmerkingEntry xentry)
                        {
                            var xnode =
                                xOpmerkingenTree.Nodes[0].Nodes.Cast<TreeNode>().FirstOrDefault(x =>
                                    string.Equals(xentry.Title, x.Text, StringComparison.CurrentCultureIgnoreCase));
                            xtn = xnode?.Nodes.Cast<TreeNode>().FirstOrDefault(x => x.Tag is List<ReactieEntry>);
                            xtn = xtn?.Nodes.Cast<TreeNode>().FirstOrDefault(x =>
                                x.Tag is ReactieEntry re && string.Equals(re.ReactieVan, reactie.ReactieVan,
                                    StringComparison.CurrentCultureIgnoreCase));
                        }
                    }
                    else if(selected is List<ReactieEntry>)
                    {
                        if (xselectedopmerkingpanel.Tag is OpmerkingEntry xentry)
                        {
                            var xnode =
                                xOpmerkingenTree.Nodes[0].Nodes.Cast<TreeNode>().FirstOrDefault(x =>
                                    string.Equals(xentry.Title, x.Text, StringComparison.CurrentCultureIgnoreCase));
                            xtn = xnode?.Nodes.Cast<TreeNode>().FirstOrDefault(x => x.Tag is List<ReactieEntry>);
                        }
                    }

                    if (xtn == null && xselectedopmerkingpanel.Tag is OpmerkingEntry xselected)
                        xtn = xOpmerkingenTree.Nodes[0].Nodes.Cast<TreeNode>().FirstOrDefault(x =>
                            string.Equals(xselected.Title, x.Text, StringComparison.CurrentCultureIgnoreCase));

                    xOpmerkingenTree.SelectedNode = xtn;
                    xOpmerkingenTree.SelectedNode?.EnsureVisible();
                    xOpmerkingenTree.SelectedNode?.Expand();

                }

                xOpmerkingenTree.TopNode?.Expand();
                xOpmerkingenTree.EndUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void AddNode(TreeNode parent, string text, int imageindex, object tag)
        {
            var tn = new TreeNode(text)
            {
                Tag = tag,
                ImageIndex = imageindex,
                SelectedImageIndex = imageindex,
            };

            parent?.Nodes.Add(tn);
        }

        private void xopmerkinglijst_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void xOpmerkingenTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OpmerkingEntry xopmerking = null;
            if (e.Node.Tag is OpmerkingEntry entry)
            {
                xopmerking = entry;
            }
            else if (e.Node.Tag is ReactieEntry)
            {
                if (e.Node.Parent?.Parent?.Parent.Tag is OpmerkingEntry entry1)
                    xopmerking = entry1;
                else if (e.Node.Parent?.Parent?.Tag is OpmerkingEntry entry2)
                    xopmerking = entry2;
            }
            else if (e.Node.Tag is List<ReactieEntry>)
            {
                if (e.Node.Parent.Tag is OpmerkingEntry entry1)
                    xopmerking = entry1;
            }

            bool isvalid = xopmerking == null || xselectedopmerkingpanel.Tag == null ||
                           (xselectedopmerkingpanel.Tag is OpmerkingEntry xentry &&
                            !string.Equals(xopmerking.Title, xentry.Title, StringComparison.CurrentCultureIgnoreCase));
            if (isvalid)
                SetOpmerkingFields(xopmerking);
        }

        private void SetOpmerkingFields(OpmerkingEntry entry)
        {
            xselectedopmerkingpanel.Tag = entry;
            if (entry == null)
            {
                xselectedopmerkinglabel.Text = "";
                xdeletetoolstripbutton.Enabled = false;
                xselectedopmerkingpanel.Visible = false;
                xopmerkingtextbox.Text = "";
                xreactietextbox.Text = "";
                xaansluitencheckbox.Checked = false;
                timer1.Stop();
            }
            else
            {
                if (!entry.IsGelezen || entry.Reacties.Any(x => !x.IsGelezen))
                    timer1.Start();
                xselectedopmerkinglabel.Text = @$"Opmerking van {entry.Afzender}";
                xselectedopmerkingpanel.Visible = true;
                xdeletetoolstripbutton.Enabled = string.Equals(entry.Afzender, Manager.Opties?.Username,
                    StringComparison.CurrentCultureIgnoreCase);
                xopmerkingtextbox.ReadOnly = !string.Equals(entry.Afzender, Manager.Opties?.Username,
                    StringComparison.CurrentCultureIgnoreCase);
                xopmerkingtextbox.Text = entry.Opmerking?.Trim() ?? "";
                var req = entry.GetReactie();
                xreactietextbox.Text = req == null ? "" : req.Reactie;
                xaansluitencheckbox.Checked = req?.Reactie != null && req.Reactie.ToLower().StartsWith("ik sta achter deze opmerking, want: ");
            }
        }

        private void xOpslaan_Click(object sender, EventArgs e)
        {
            Manager.Opmerkingen.SetNotes(Opmerkingen);
            foreach (var r in _removed)
                Manager.Opmerkingen?.Remove(r);
            _removed.Clear();
            Manager.Opmerkingen.Save().Wait();
            Manager.RemoteMessage(new RemoteMessage("Opmerkingen Opgeslagen!", MessageAction.AlgemeneMelding, MsgType.Info));
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void xaddtoolstripbutton_Click(object sender, EventArgs e)
        {
            var xnewop = new OpmerkingEditor();
            if (xnewop.ShowDialog() == DialogResult.OK)
            {
                var op = xnewop.SelectedEntry;
                var xcur = xOpmerkingenTree.Nodes[0].Nodes.Find(op.Title, false);
                if (xcur.Length > 0)
                {
                    XMessageBox.Show($"Er bestaal al een opmerking  met de title '{op.Title}'...\n\n" +
                                     $"Maak een opmerking met een andere Title, of sluit je aan bij de bestaande versie.",
                        "Title Bestaal Al", MessageBoxIcon.Warning);
                    xOpmerkingenTree.SelectedNode = xcur[0];
                    xOpmerkingenTree.SelectedNode?.EnsureVisible();
                    return;
                }

                Opmerkingen.Add(op);
                LoadOpmerkingen(op);
            }
        }

        private void xwijzigbutton_Click(object sender, EventArgs e)
        {
            if (xselectedopmerkingpanel.Tag is OpmerkingEntry opmerking)
            {
                opmerking.SetReactie(xreactietextbox.Text);
                opmerking.SetOpmerking(opmerking.Title, xopmerkingtextbox.Text, opmerking.Ontvangers);
                LoadOpmerkingen(opmerking.GetReactie());
                UpdateWijzigButton();
            }

        }

        private void xopmerkingtextbox_TextChanged(object sender, EventArgs e)
        {
            UpdateWijzigButton();
        }

        private void xreactietextbox_TextChanged(object sender, EventArgs e)
        {
            UpdateWijzigButton();
        }

        private void UpdateWijzigButton()
        {
            if (xselectedopmerkingpanel.Tag is OpmerkingEntry opmerking)
            {
                var reactie = opmerking.GetReactie() ?? new ReactieEntry();
                xwijzigbutton.Visible = (!string.Equals(xreactietextbox.Text, reactie.Reactie,
                                            StringComparison.CurrentCultureIgnoreCase)) ||
                                        (!string.Equals(xopmerkingtextbox.Text, opmerking.Opmerking,
                                            StringComparison.CurrentCultureIgnoreCase));
            }
        }

        private void xaansluitencheckbox_CheckedChanged(object sender, EventArgs e)
        {
            string xdeelname = "Ik sta achter deze opmerking, want: ";
            var xvalue = xreactietextbox.Text.Replace(xdeelname, "").Trim();
            if (xaansluitencheckbox.Checked)
            {
                xreactietextbox.Text = $@"{xdeelname}{xvalue}";
            }
            else xreactietextbox.Text = xvalue;
        }

        private void xOpmerkingenTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            xOpmerkingenTree.SelectedNode = e.Node;
        }

        private List<OpmerkingEntry> _removed = new List<OpmerkingEntry>();

        private void xdeletetoolstripbutton_Click(object sender, EventArgs e)
        {
            if (xselectedopmerkingpanel.Tag is OpmerkingEntry xent)
            {
                if (XMessageBox.Show($"Weetje zeker dat je '{xent.Title}' wilt verwijderen?", "Opmerking verwijderen",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    timer1.Stop();
                    Opmerkingen.Remove(xent);
                    _removed.Add(xent);
                    SetOpmerkingFields(null);
                    LoadOpmerkingen();
                }
            }
        }

        private void wijzigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xselectedopmerkingpanel.Tag is OpmerkingEntry xent)
            {
                var xnewop = new OpmerkingEditor(xent);
                if (xnewop.ShowDialog() == DialogResult.OK)
                {
                    var op = xnewop.SelectedEntry;
                    var xcur = xOpmerkingenTree.Nodes[0].Nodes.Find(op.Title, false);
                    if (xcur.Length > 0)
                    {
                        XMessageBox.Show($"Er bestaal al een opmerking  met de title '{op.Title}'...\n\n" +
                                         $"Maak een opmerking met een andere Title, of sluit je aan bij de bestaande versie.",
                            "Title Bestaal Al", MessageBoxIcon.Warning);
                        xOpmerkingenTree.SelectedNode = xcur[0];
                        xOpmerkingenTree.SelectedNode?.EnsureVisible();
                        return;
                    }

                    SetOpmerkingFields(null);
                    LoadOpmerkingen(op);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (xselectedopmerkingpanel.Tag is OpmerkingEntry entry && string.Equals(entry.Afzender, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase))
            {
                wijzigToolStripMenuItem.Enabled = true;
                verwijderToolStripMenuItem.Enabled = true;
            }
            else
            {
                wijzigToolStripMenuItem.Enabled = false;
                verwijderToolStripMenuItem.Enabled = false;
            }
        }

        private void vouwAllesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xOpmerkingenTree.TopNode?.Collapse();
        }

        private void ontvouwAllesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xOpmerkingenTree.TopNode?.ExpandAll();
        }

        private void OpmerkingenForm_Shown(object sender, EventArgs e)
        {
            Manager.Opmerkingen.OnOpmerkingenChanged += Opmerkingen_OnOpmerkingenChanged;
        }

        private void Opmerkingen_OnOpmerkingenChanged(object sender, EventArgs e)
        {
            Opmerkingen = null;
            this.BeginInvoke(new MethodInvoker(() => LoadOpmerkingen(xselectedopmerkingpanel.Tag as OpmerkingEntry)));
        }

        private void OpmerkingenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.Opmerkingen.OnOpmerkingenChanged -= Opmerkingen_OnOpmerkingenChanged;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (xselectedopmerkingpanel.Tag is OpmerkingEntry entry)
            {
                entry.SetIsGelezen();
                Manager.Opmerkingen.SetNotes(Opmerkingen);
                LoadOpmerkingen(entry);
                Manager.Opmerkingen.Save();
            }
        }
    }
}
