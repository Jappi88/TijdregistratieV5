using ProductieManager.Properties;
using Rpm.Productie;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class VerpakkingInstructieForm : MetroFramework.Forms.MetroForm 
    {
        private IProductieBase _productie;
        public VerpakkingInstructieForm(IProductieBase productie)
        {
            InitializeComponent();
            _productie = productie;
            InitUI(false);
        }

        private void InitUI(bool editmode)
        {
            var xmsg = $"Verpakking Instructie voor [{_productie.ArtikelNr}]{_productie.Omschrijving}";
            this.Text = xmsg;
            this.Invalidate();
            verpakkingInstructieUI1.InitFields(_productie?.VerpakkingsInstructies, editmode,xmsg);
            if (verpakkingInstructieUI1.IsEditmode)
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
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            if (verpakkingInstructieUI1.IsEditmode)
            {
                InitUI(false);
            }
            else
                DialogResult = DialogResult.Cancel;
        }

        private async void xwijzig_Click(object sender, EventArgs e)
        {
            if (_productie == null) return;
            if(verpakkingInstructieUI1.IsEditmode)
            {
                _productie.VerpakkingsInstructies = verpakkingInstructieUI1.VerpakkingInstructie;
                InitUI(false);
                await _productie.Update($"VerpakkingsInstructies aangepast voor [{_productie.ArtikelNr}]{_productie.Omschrijving}", true, true);
            }
            else
                InitUI(true);
        }

        private void VerpakkingInstructieForm_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            if (_productie?.ProductieNr == null || !string.Equals(id, _productie.ProductieNr, StringComparison.CurrentCultureIgnoreCase))
                return;
            if (Disposing || IsDisposed) return;
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(Close));
            else this.Close();
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            if (changedform != null && _productie != null && !IsDisposed && !Disposing &&
                  string.Equals(changedform.ProductieNr, _productie.ProductieNr, StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    this.BeginInvoke(new Action(() =>
                                    {
                                        
                                        if (_productie is Bewerking bew)
                                        {
                                            var xbw = changedform.Bewerkingen?.FirstOrDefault(x => x.Equals(bew));
                                            if (xbw == null) this.Close();
                                            _productie = xbw;
                                        }
                                        else
                                        {
                                            _productie = changedform;
                                        }
                                        if (!verpakkingInstructieUI1.IsEditmode)
                                            InitUI(false);
                                    }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void VerpakkingInstructieForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
        }
    }
}
