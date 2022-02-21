using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Forms.MetroBase;
using ProductieManager.Properties;
using Rpm.Productie;

namespace Forms
{
    public partial class VerpakkingInstructieForm : MetroBaseForm
    {
        private IProductieBase _productie;

        public VerpakkingInstructieForm(IProductieBase productie)
        {
            InitializeComponent();
            _productie = productie;
            InitUI(false);
            var xmsg = $"Verpakking Instructie voor [{_productie.ArtikelNr}]{_productie.Omschrijving}";
            Text = xmsg;
            Invalidate();
            verpakkingInstructieUI1.AllowEditMode = false;
            verpakkingInstructieUI1.InitFields(_productie?.VerpakkingsInstructies, false, xmsg, Color.SaddleBrown,
                Color.White, _productie);
        }

        private void InitUI(bool editmode)
        {
            if (editmode)
            {
                xsluiten.Text = "Annuleren";
                xwijzig.Text = "Opslaan";
                xwijzig.Image = Resources.diskette_save_saveas_1514;
            }
            else
            {
                xwijzig.Text = "Wijzig";
                xwijzig.Image = Resources.edit__52382;
                xsluiten.Text = "Sluiten";
            }

            verpakkingInstructieUI1.UpdateFields(editmode, _productie);
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            if (verpakkingInstructieUI1.IsEditmode)
                InitUI(false);
            else
                DialogResult = DialogResult.Cancel;
        }

        private void xwijzig_Click(object sender, EventArgs e)
        {
            if (_productie == null) return;
            if (verpakkingInstructieUI1.IsEditmode)
            {
                if (verpakkingInstructieUI1.SaveChanges())
                    InitUI(false);
            }
            else
            {
                InitUI(true);
            }
        }

        private void VerpakkingInstructieForm_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            if (_productie?.ProductieNr == null ||
                !string.Equals(id, _productie.ProductieNr, StringComparison.CurrentCultureIgnoreCase))
                return;
            if (Disposing || IsDisposed) return;
            if (InvokeRequired)
                Invoke(new MethodInvoker(Close));
            else Close();
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            if (changedform != null && _productie != null && !IsDisposed && !Disposing &&
                string.Equals(changedform.ProductieNr, _productie.ProductieNr,
                    StringComparison.CurrentCultureIgnoreCase))
                try
                {
                    BeginInvoke(new Action(() =>
                    {
                        if (_productie is Bewerking bew)
                        {
                            var xbw = changedform.Bewerkingen?.FirstOrDefault(x => x.Equals(bew));
                            if (xbw == null) Close();
                            _productie = xbw;
                        }
                        else
                        {
                            _productie = changedform;
                        }

                        if (!verpakkingInstructieUI1.IsEditmode)
                            InitUI(false);
                        else verpakkingInstructieUI1.Productie = _productie;
                    }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
        }

        private void VerpakkingInstructieForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
        }
    }
}