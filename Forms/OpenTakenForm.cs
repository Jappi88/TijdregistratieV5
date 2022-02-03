using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class OpenTakenForm : Forms.MetroBase.MetroBaseForm
    {
        public IProductieBase Productie { get; private set; }

        public OpenTakenForm(IProductieBase productie )
        {
            InitializeComponent();
            Productie = productie;
            xrooster.Visible = productie is Bewerking;
            InitFields();
        }

        private void InitFields()
        {
            try
            {
                //indeling info
                int count = Productie.GetPersonen(true).Length;
                string x1 = count == 1 ? "Medewerker" : "Medewerkers";
                Color c = count == 0 ? Color.Red : Color.Green;
                xindelinglabel.Text = string.Format("{0} " + x1, count);
                xindelinglabel.ForeColor = c;
                //werktijd info
                double tijd = Productie.TijdGewerkt;
                c = tijd <= 0 ? Color.Red : Color.Green;
                xwerktijdlabel.Text = $"{tijd} uur";
                xwerktijdlabel.ForeColor = c;
                //onderbrekeningen
                count = Productie.GetStoringen( true).Length;
                c = count > 0 ? Color.Red : Color.Green;
                xondbrlabel.Text = $"{count} Openstaand";
                xondbrlabel.ForeColor = c;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public bool DoCheck()
        {
            if (Productie == null) return false;
            if (Productie.TijdGewerkt <= 0)
            {
                XMessageBox.Show(this, $"Werktijd is nog niet opgelost!\n\n" +
                                 "Controlleer de tijdlijnen en de werkrooster.\n" +
                                 "Controlleer of je een speciale rooster hebt toegevoegd als dit een uitzonderlijke werkdag is.",
                    "Controlleer Werktijd", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (Productie.GetPersonen(true).Length == 0)
            {
                XMessageBox.Show(this, $"Er zijn geen actieve personen!\n\n" +
                                 "Controlleer de indeling of je minstins 1 persoon op actief hebt staan.\n",
                    "Controlleer Indeling", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (Productie.GetStoringen(true).Length > 0)
            {
                XMessageBox.Show(this, $"Er staat nog 1 of meer onderbrekeningen open.\n\n" +
                                 "Bekijk en lost de onderbrekening op voordat je verder kan.\n",
                    "Controlleer Onderbrekeningen", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            if (DoCheck())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void OpenTakenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
        }

        private void OpenTakenForm_Shown(object sender, System.EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            if (this.Disposing || this.IsDisposed || Productie == null || !string.Equals(Productie.ProductieNr,
                changedform.ProductieNr, StringComparison.CurrentCultureIgnoreCase)) return;
            if (Productie is Bewerking bew)
            {
                var bw = changedform.Bewerkingen?.FirstOrDefault(x =>
                    string.Equals(x.Naam, Productie.Naam, StringComparison.CurrentCultureIgnoreCase));
                if (bw != null)
                {
                    lock (Productie)
                    {
                        Productie = bw;
                    }
                }
            }
            else 
            {
                lock (Productie)
                {
                    Productie = changedform;
                }
            }
            this.BeginInvoke(new Action(InitFields));
        }

        private void xindeling_Click(object sender, EventArgs e)
        {
            if (Productie == null) return;
            if (Productie is Bewerking bew)
            {
                var x = new Indeling(bew.Root, bew);
                x.StartPosition = FormStartPosition.CenterParent;
                x.ShowDialog();
            }
            else if (Productie is ProductieFormulier form)
            {
                var x = new Indeling(form, null);
                x.StartPosition = FormStartPosition.CenterParent;
                x.ShowDialog();
            }
        }

        private void xwerktijd_Click(object sender, EventArgs e)
        {
            if (Productie is Bewerking bew)
                bew.ShowWerktIjden(this);
            else if(Productie is ProductieFormulier  form)
            {
                if (form.Bewerkingen == null) return;
                foreach (var xb in form.Bewerkingen)
                    xb.ShowWerktIjden(this);
            }
        }

        private void xonderbrekeningen_Click(object sender, EventArgs e)
        {
            ProductieFormulier form = null;
            if (Productie is ProductieFormulier prod)
                form = prod;
            else if (Productie is Bewerking bew) form = bew.GetParent();
            if (form == null) return;
            var allst = new AlleStoringen();
            allst.InitStoringen(form);
            allst.ShowDialog();
        }

        private void xrooster_Click(object sender, EventArgs e)
        {
            if (Productie is Bewerking bew)
                bew.DoBewerkingEigenRooster(this);
            
        }
    }
}
