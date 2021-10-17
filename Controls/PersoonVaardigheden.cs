using System;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Rpm.Productie;

namespace Controls
{
    public partial class PersoonVaardigheden : UserControl
    {
        public PersoonVaardigheden()
        {
            InitializeComponent();
            (xskillview.Columns[0] as OLVColumn).ImageGetter = sender => 0;
        }

        public Personeel Persoon { get; private set; }

        public bool IsCloseAble
        {
            get => xclosepanel.Visible;
            set => xclosepanel.Visible = value;
        }

        public void InitPersoneel(Personeel persoon)
        {
            Persoon = persoon;
            ListItems();
        }

        public void RefreshItems(Personeel persoon)
        {
            if (InvokeRequired)
                Invoke(new Action(() => xRefreshItems(persoon)));
            else
                xRefreshItems(persoon);
        }

        private void xRefreshItems(Personeel persoon)
        {
            if (Persoon == null && persoon != null)
            {
                Persoon = persoon;
                ListItems();
            }
            else if (persoon != null && persoon.Equals(Persoon))
            {
                Persoon = persoon;
                var skills = persoon.GetSkills();
                if (skills == null || skills.SkillEntries.Count == 0 && xskillview.Items.Count > 0)
                {
                    xskillview.SetObjects(new SkillEntry[] { });
                }
                else
                {
                    foreach (var skill in skills.SkillEntries)
                    {
                        var allowed = IsAllowed(skill) && skill.TijdGewerkt > 0;
                        SkillEntry item = null;
                        if (xskillview.Objects != null)
                            item = xskillview.Objects.Cast<SkillEntry>().FirstOrDefault(x => x.Equals(skill));
                        if (item == null && allowed)
                            xskillview.AddObject(skill);
                        else if (item != null && !allowed)
                            xskillview.RemoveObject(item);
                        else xskillview.RefreshObject(skill);
                    }

                    if (xskillview.Objects != null && xskillview.Objects.Cast<SkillEntry>().Count() >
                        skills.SkillEntries.Count)
                        xskillview.RemoveObjects(xskillview.Objects.Cast<SkillEntry>().Where(x =>
                                x.TijdGewerkt == 0 || !IsAllowed(x) ||
                                !skills.SkillEntries.Any(t => t.Equals(x)))
                            .ToArray());
                }
            }

            UpdateHeaderLabel();
            UpdateStatusLabel();
        }

        public void ListItems()
        {
            if (Persoon != null)
            {
                var skills = Persoon.GetSkills();
                if (skills?.SkillEntries != null)
                {
                    var entries = skills.SkillEntries.Where(x => x.TijdGewerkt > 0 && IsAllowed(x)).ToList();
                    xskillview.SetObjects(entries);
                }
                else
                {
                    xskillview.SetObjects(new SkillEntry[] { });
                }
            }
            else
            {
                xskillview.SetObjects(new SkillEntry[] { });
            }

            UpdateHeaderLabel();
            UpdateStatusLabel();
        }

        public bool IsAllowed(SkillEntry entry)
        {
            var filter = xsearchbox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(filter) || filter == "zoeken...")
                return true;

            if (entry.ArtikelNr != null && entry.ArtikelNr.ToLower().Contains(filter))
                return true;
            if (entry.Path != null && entry.Path.ToLower().Contains(filter))
                return true;
            if (entry.Omschrijving != null && entry.Omschrijving.ToLower().Contains(filter))
                return true;
            if (entry.WerkPlek != null && entry.WerkPlek.ToLower().Contains(filter))
                return true;
            return false;
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim().ToLower() != "zoeken...")
                ListItems();
        }

        private void xskillview_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatusLabel();
        }

        private void UpdateHeaderLabel()
        {
            if (Persoon != null)
            {
                var skills = Persoon.GetSkills();
                if (skills is {SkillEntries: { }})
                {
                    var entries = skills.SkillEntries.Where(x => x.TijdGewerkt > 0).ToList();
                    var title =
                        $"Vaardigheden van {Persoon.PersoneelNaam} vanaf {skills.Vanaf.ToString("dd MMMM yyyy")}.\n";
                    if (entries.Count > 0)
                    {
                        var xarg = entries.Count == 1 ? "vaardigheid" : "verschillende vaardigheden";
                        title += $"{entries.Count} {xarg} van totaal {entries.Sum(x => x.TijdGewerkt)} uur.";
                        xomschrijving.Text = title;
                    }
                    else
                    {
                        xomschrijving.Text = $"{title}Momenteel geen vaardigheden.";
                    }
                }
                else
                {
                    xomschrijving.Text = $"{Persoon.PersoneelNaam} heeft nog geen vaardigheden.";
                }
            }
            else
            {
                xomschrijving.Text = "Niemand Geselecteerd";
            }
        }

        private void UpdateStatusLabel()
        {
            if (xskillview.SelectedItems.Count > 0)
            {
                SkillEntry[] selected = { };
                if (xskillview.SelectedObjects != null)
                    selected = xskillview.SelectedObjects.Cast<SkillEntry>().ToArray();
                if (selected.Length == 1)
                    xstatuslabel.Text = $"1 vaardigheid geselecteerd van {selected[0].TijdGewerkt} uur";
                else
                    xstatuslabel.Text =
                        $"{selected.Length} vaardigheden geselecteerd van totaal {selected.Sum(s => s.TijdGewerkt)} uur";
            }
            else
            {
                if (xskillview.Objects != null)
                {
                    var selected = xskillview.Objects.Cast<SkillEntry>().ToArray();
                    if (selected.Length == 1)
                        xstatuslabel.Text = $"1 vaardigheid van {selected[0].TijdGewerkt} uur";
                    else
                        xstatuslabel.Text =
                            $"{selected.Length} Vaardigheden van totaal {selected.Sum(s => s.TijdGewerkt)} uur";
                }
                else
                {
                    xstatuslabel.Text = "Geen Vaardigheden";
                }
            }
        }

        private void xsearch_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb is {Text: "Zoeken..."}) tb.Text = "";
        }

        private void xsearch_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.Text = "Zoeken...";
        }

        public event EventHandler OnCloseButtonPressed;

        public void CloseButtonPressed()
        {
            OnCloseButtonPressed?.Invoke(xsluiten, EventArgs.Empty);
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            CloseButtonPressed();
        }

        private void xskillview_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            var wp = e.Model as SkillEntry;
            if (wp != null)
            {
                e.Title = $"[{wp.Path}]";
                e.Text = wp.Omschrijving;
            }
        }
    }
}