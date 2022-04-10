using ProductieManager.Rpm.Misc;
using Rpm.Productie;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class NiewProductieForm : Forms.MetroBase.MetroBaseForm
    {
        public ProductieFormulier CreatedFormulier { get; private set; }

        public NiewProductieForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            xbewerking.Items.Clear();
            xbewerking.Items.AddRange(Manager.BewerkingenLijst.GetAllEntries().Select(x => (object) x.Naam).ToArray());
            pictureBox1.Image =
                ProductieManager.Properties.Resources.page_document_16748_128_128.CombineImage(
                    ProductieManager.Properties.Resources.lightning_weather_storm_2781, 1.75);
            xleverdatum.Value = DateTime.Now.Date.Add(Rooster.StandaartRooster().EindWerkdag);
        }

        private async void xstarten_Click(object sender, EventArgs e)
        {
            if (await Save()) DialogResult = DialogResult.Yes;
        }

        private async void xopslaan_Click(object sender, EventArgs e)
        {
            if (await Save()) DialogResult = DialogResult.OK;
        }

        private async Task<bool> Save()
        {
            bool xreturn = false;
            try
            {
                if (string.IsNullOrEmpty(xartikelnr.Text))
                    throw new Exception("Artikel nummer kan niet leeg zijn!\nVul in een geldige artikel nummer a.u.b.");
                if (xbewerking.SelectedIndex < 0)
                    throw new Exception("Kies een geldidge bewerking a.u.b");
                if (string.IsNullOrEmpty(xomschrijving.Text))
                    throw new Exception("Omschrijving kan niet leeg zijn!\nVul in een omschrijving a.u.b.");
                var bewent = Manager.BewerkingenLijst.GetEntry(xbewerking.SelectedItem.ToString());
                if (bewent == null)
                    throw new Exception($"Bewerking '{xbewerking.SelectedItem.ToString()}' bestaat niet!");
                var prod = await ProductieFormulier.CreateNewProductie(xartikelnr.Text.Replace(" ", ""),
                    xomschrijving.Text.Trim(),
                    (int) xaantal.Value, xleverdatum.Value, (int) xperuur.Value, bewent,
                    xprodnrchecked.Checked ? xprodnr.Text.Trim() : null);
                if (prod != null)
                {
                    CreatedFormulier = prod;
                    xreturn = true;
                }
                else
                    throw new Exception(
                        "Het is niet  gelukt om een nieuwe productieformulier aan te maken!\nRaadpleeg Ihab a.u.b.");
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return xreturn;
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xprodnrchecked_CheckedChanged(object sender, EventArgs e)
        {
            xprodnr.Enabled = xprodnrchecked.Checked;
        }
    }
}
