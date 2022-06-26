using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class NewMateriaalForm : MetroBase.MetroBaseForm
    {
        public Materiaal SelectedMateriaal { get; private set; }
        public bool IsEditMode { get;private set; }

        public NewMateriaalForm(ProductieFormulier parent)
        {
            InitializeComponent();
            SelectedMateriaal = new Materiaal();
            SelectedMateriaal.Parent = parent;
            this.Title = $"Materiaal Aanmaken";
        }

        public NewMateriaalForm(Materiaal mat):this(mat?.Parent)
        {
            this.Title = $"Wijzig {mat.Omschrijving}({mat.ArtikelNr})";
            SelectedMateriaal = mat?.CreateCopy()??new Materiaal();
            IsEditMode = true;
            UpdateInfoFields(mat);
        }


        public void UpdateInfoFields(Materiaal mat)
        {
            if (mat?.ArtikelNr != null)
            {
                SelectedMateriaal = mat;
                xomschrijving.Text = mat.Omschrijving;
                xartikelnr.Text = mat.ArtikelNr;
                xlocatie.Text = mat.Locatie;
                xaantal.SetValue((decimal)mat.Aantal);
                xafkeurvalue.Value = (decimal)mat.AantalAfkeur;
                xeenheid.SelectedIndex = mat.Eenheid.ToLower() == "m" ? 0 : 1;
                xafkeurpercent.Text = mat.AfKeurProcent((decimal)mat.AantalAfkeur);
                xverbruiktlabel.Text = $"Verbruik({mat.Eenheid})";
                xaantalperstuk.SetValue((decimal)mat.AantalPerStuk);
                xklaarzetimage.Image = mat.IsKlaarGezet ? Resources.check_1582 : null;
            }
            else
            {
                xomschrijving.Text = "";
                xartikelnr.Text = "";
                xlocatie.Text = "";
                xeenheid.SelectedIndex = -1;
                xverbruiktlabel.Text = "Verbruik";
                xaantalperstuk.Value = 0;
                xafkeurvalue.Value = 0;
                xaantal.Value = 0;
                xafkeurpercent.Text = "0.00%";
                xklaarzetimage.Image = null;
            }
        }

        private void UpdateMateriaal()
        {
            SelectedMateriaal ??= new Materiaal();
            SelectedMateriaal.Omschrijving = xomschrijving.Text;
            SelectedMateriaal.ArtikelNr = xartikelnr.Text;
            SelectedMateriaal.Locatie = xlocatie.Text;
            SelectedMateriaal.Eenheid = xeenheid.SelectedIndex == 1 ? "Stuks" : "m";
            SelectedMateriaal.AantalPerStuk = (double)xaantalperstuk.Value;
            SelectedMateriaal.AantalAfkeur = (double)xafkeurvalue.Value;
            SelectedMateriaal.IsKlaarGezet = xklaarzetimage.Image != null;
        }

        private void xklaarzetimage_Click(object sender, EventArgs e)
        {
            if (xklaarzetimage.Image == null)
                xklaarzetimage.Image = Resources.check_1582;
            else xklaarzetimage.Image = null;
        }

        private void xklaarzetimage_MouseEnter(object sender, EventArgs e)
        {
            xklaarzetpanel.BackColor = Color.AliceBlue;
        }

        private void xklaarzetimage_MouseLeave(object sender, EventArgs e)
        {
            xklaarzetpanel.BackColor = Color.Transparent;
        }

        private void xOK_Click(object sender, System.EventArgs e)
        {
            if (xomschrijving.Text.Trim().Length < 4)
                XMessageBox.Show(this, $"Ongeldige Omschrijving!", "Ongeldig", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (xartikelnr.Text.Trim().Length < 4)
                XMessageBox.Show(this, $"Ongeldige Artikel Nr!", "Ongeldig", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (xeenheid.SelectedIndex < 0)
                XMessageBox.Show(this, $"Ongeldige Eenheid! Kies een eenheid voor de aantallen.", "Ongeldig",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                UpdateMateriaal();
                DialogResult = DialogResult.OK;
            }
        }

        private void xSluiten_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xafkeurvalue_ValueChanged(object sender, EventArgs e)
        {
            xafkeurpercent.Text = SelectedMateriaal.AfKeurProcent(xafkeurvalue.Value);
        }
    }
}
