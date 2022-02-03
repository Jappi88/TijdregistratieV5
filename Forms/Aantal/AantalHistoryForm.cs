using Rpm.Productie;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Forms.Aantal
{
    public partial class AantalHistoryForm : Forms.MetroBase.MetroBaseForm
    {
        public WerkPlek Plek { get; private set; }
        public AantalHistoryForm(WerkPlek plek)
        {
            InitializeComponent();
            Plek = plek;
            this.Text = $"Aantal Geschiedenis Van {Plek.Path}";
            this.Invalidate();
            LoadWerkplekken();
        }

        public void LoadWerkplekken()
        {
            try
            {
                if (Plek == null) return;
                aantalGemaaktHistoryUI1.UpdateList(Plek);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AantalHistoryForm_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            if (Plek?.ProductieNr != null &&
                string.Equals(Plek.ProductieNr, id, StringComparison.CurrentCultureIgnoreCase))
            {
                this.Close();
            }
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            if (Plek?.ProductieNr == null || changedform == null ||
                !string.Equals(Plek.ProductieNr, changedform.ProductieNr, StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            var xplek = changedform.GetAlleWerkplekken().FirstOrDefault(x =>
                string.Equals(Plek.Path, x.Path, StringComparison.CurrentCultureIgnoreCase));
            if (string.IsNullOrEmpty(xplek?.ProductieNr))
                this.Close();
            else
            {
                Plek = xplek;
                aantalGemaaktHistoryUI1.UpdateList(Plek);
            }
        }

        private void AantalHistoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
        }
    }
}
