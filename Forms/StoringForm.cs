using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class StoringForm : MetroBaseForm
    {
        public StoringForm(WerkPlek plek)
        {
            InitializeComponent();
            Plekken = new List<WerkPlek>();
            Plekken.Add(plek);
            werkPlekStoringen1.OnCloseButtonPressed += xcloseButton_Click;
            Text = $"Onderbrekeningen van {plek.Path}";
            werkPlekStoringen1.InitStoringen(new KeyValuePair<string, List<WerkPlek>>(plek.Naam, Plekken));

            try
            {
                if (Manager.Opties != null)
                    werkPlekStoringen1.xskillview.RestoreState(Manager.Opties.ViewDataStoringenState);
            }
            catch
            {
            }
        }

        public StoringForm(string naam, List<WerkPlek> plekken)
        {
            InitializeComponent();
            Plekken = plekken;
            werkPlekStoringen1.OnCloseButtonPressed += xcloseButton_Click;
            Text = $"Onderbrekeningen van {naam}";
            werkPlekStoringen1.InitStoringen(new KeyValuePair<string, List<WerkPlek>>(naam, Plekken));

            try
            {
                if (Manager.Opties != null)
                    werkPlekStoringen1.xskillview.RestoreState(Manager.Opties.ViewDataStoringenState);
            }
            catch
            {
            }
        }

        public List<WerkPlek> Plekken { get; }

        private void xcloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void StoringenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            werkPlekStoringen1.OnDisableEvents -= werkPlekStoringen1_OnDisableEvents;
            werkPlekStoringen1.OnEnableEvents -= werkPlekStoringen1_OnEnableEvents;
            if (Manager.Opties != null)
                Manager.Opties.ViewDataStoringenState = werkPlekStoringen1.xskillview.SaveState();
        }

        private void StoringForm_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            werkPlekStoringen1.OnDisableEvents += werkPlekStoringen1_OnDisableEvents;
            werkPlekStoringen1.OnEnableEvents += werkPlekStoringen1_OnEnableEvents;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            if (IsDisposed) return;
            if (changedform != null &&
                Plekken.Any(x => string.Equals(x.Werk.Parent.ProductieNr, changedform.ProductieNr,
                    StringComparison.CurrentCultureIgnoreCase)) &&
                changedform.Bewerkingen != null)
                foreach (var bew in changedform.Bewerkingen)
                {
                    var wp = bew.WerkPlekken.FirstOrDefault(x =>
                        Plekken.Any(s => string.Equals(x.Path, s.Path, StringComparison.CurrentCultureIgnoreCase)));
                    if (wp != null)
                    {
                        UpdateWerkplek(wp);
                        werkPlekStoringen1.RefreshItems(wp);
                    }
                }
        }

        private void UpdateWerkplek(WerkPlek wp)
        {
            if (Plekken is {Count: > 0})
                for (var i = 0; i < Plekken.Count; i++)
                    if (Plekken[i].Path.ToLower() == wp.Path.ToLower())
                    {
                        Plekken[i] = wp;
                        break;
                    }
        }

        private void werkPlekStoringen1_OnDisableEvents(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
        }

        private void werkPlekStoringen1_OnEnableEvents(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }
    }
}