using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms
{
    public partial class AlleVaardigheden : MetroFramework.Forms.MetroForm
    {
        public readonly StickyWindow _stickyWindow;

        public AlleVaardigheden()
        {
            InitializeComponent();
            _stickyWindow = new StickyWindow(this);
            ((OLVColumn) xuserlist.Columns[0]).ImageGetter = sender => 0;
            if (Manager.Opties != null)
                persoonVaardigheden1.xskillview.RestoreState(Manager.Opties.ViewDataVaardighedenState);
            InitPersoneel();
        }

        public Personeel Selected { get; set; }

        private void persoonVaardigheden1_OnCloseButtonPressed(object sender, EventArgs e)
        {
            Close();
        }

        private void xuserlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xuserlist.SelectedObject != null)
            {
                if (Selected == null || !xuserlist.SelectedObject.Equals(Selected))
                    Selected = xuserlist.SelectedObject as Personeel;
            }
            else
            {
                Selected = null;
            }

            persoonVaardigheden1.InitPersoneel(Selected);
        }

        public async void InitPersoneel()
        {
            var personen = new List<Personeel>();
            if (Manager.Database != null && !Manager.Database.IsDisposed)
                personen = await Manager.Database.GetAllPersoneel();
            xuserlist.SetObjects(personen);
            if (xuserlist.Items.Count > 0 && xuserlist.SelectedObject == null)
            {
                xuserlist.SelectedObject = personen[0];
                if (xuserlist.SelectedItem != null)
                    xuserlist.SelectedItem.EnsureVisible();
            }

            UpdateStatusText();
        }

        private void UpdateStatusText()
        {
            var text = "Geen Personeel";
            if (xuserlist.Items.Count > 0)
            {
                var pers = xuserlist.Objects.Cast<Personeel>().ToArray();
                double uren = 0;
                foreach (var per in pers)
                {
                    var skills = per.GetSkills();
                    if (skills.SkillEntries == null) continue;
                    uren += skills.SkillEntries.Sum(x => x.TijdGewerkt);
                }

                if (pers.Length == 1)
                    text = $"Alleen {pers[0].PersoneelNaam} met totaal {uren} uren aan ervaring.";
                else
                    text = $"{pers.Length} Personen.\nTotaal {uren} uren aan ervaring.";
            }

            xstatuslabel.Text = text;
        }

        private void AlleVaardigheden_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnPersoneelChanged -= Manager_OnPersoneelChanged;
            if (Manager.Opties != null)
                Manager.Opties.ViewDataVaardighedenState = persoonVaardigheden1.xskillview.SaveState();
        }

        private void AlleVaardigheden_Shown(object sender, EventArgs e)
        {
            Manager.OnPersoneelChanged += Manager_OnPersoneelChanged;
        }

        private void Manager_OnPersoneelChanged(object sender, Personeel user)
        {
            if (IsDisposed || user == null) return;
            if (InvokeRequired)
                Invoke(new Action(() => RefreshPersoneel(user)));
            else RefreshPersoneel(user);
        }

        private void RefreshPersoneel(Personeel persoon)
        {
            if (persoon != null)
            {
                var pers = xuserlist.Objects.Cast<Personeel>().FirstOrDefault(x => x.Equals(persoon));
                if (pers != null)
                {
                    xuserlist.RefreshObject(persoon);
                    if (persoonVaardigheden1.Persoon != null && persoonVaardigheden1.Persoon.Equals(persoon))
                        persoonVaardigheden1.RefreshItems(persoon);
                }
            }
        }
    }
}