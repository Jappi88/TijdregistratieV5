using System;
using System.Windows.Forms;
using Rpm.Productie;
using Various;

namespace Forms
{
    public partial class VaardighedenForm : MetroFramework.Forms.MetroForm
    {
        public VaardighedenForm(Personeel persoon)
        {
            InitializeComponent();
            persoonVaardigheden1.OnCloseButtonPressed += xcloseButton_Click;
            Persoon = persoon;
            if (persoon != null)
            {
                Text = $"Vaardigheden van {persoon.PersoneelNaam}";

                try
                {
                    if (Manager.Opties != null)
                        persoonVaardigheden1.xskillview.RestoreState(Manager.Opties.ViewDataVaardighedenState);
                }
                catch
                {
                }

                persoonVaardigheden1.InitPersoneel(persoon);
            }
        }

        public Personeel Persoon { get; private set; }

        private void xcloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void VaardighedenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SetLastInfo();
            Manager.OnPersoneelChanged -= Manager_OnPersoneelChanged;
            if (Manager.Opties != null)
                Manager.Opties.ViewDataVaardighedenState = persoonVaardigheden1.xskillview.SaveState();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.InitLastInfo();
        }

        private void VaardighedenForm_Shown(object sender, EventArgs e)
        {
            Manager.OnPersoneelChanged += Manager_OnPersoneelChanged;
        }

        private void Manager_OnPersoneelChanged(object sender, Personeel user)
        {
            if (IsDisposed) return;
            if (user != null && Persoon != null && user.Equals(Persoon))
            {
                Persoon = user;
                persoonVaardigheden1.RefreshItems(user);
            }
        }
    }
}