using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Forms;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;

namespace Controls
{
    public partial class MateriaalUI : UserControl
    {
        public MateriaalUI()
        {
            InitializeComponent();
            ((OLVColumn) xmateriaallijst.Columns[0]).ImageGetter = GetImage;
        }

        public ProductieFormulier Formulier { get; set; }

        public object GetImage(object sender)
        {
            if (sender is Materiaal mat)
                if (mat.IsKlaarGezet)
                    return 1;

            return 0;
        }

        public void InitMaterialen(ProductieFormulier form)
        {
            Formulier = form;
            xmateriaallijst.SetObjects(form.Materialen.CreateCopy());
            if (xmateriaallijst.Items.Count > 0)
            {
                xmateriaallijst.SelectedObject = xmateriaallijst.Objects.Cast<Materiaal>().ToArray()[0];
                if (xmateriaallijst.SelectedObject != null)
                    xmateriaallijst.SelectedItem.EnsureVisible();
            }

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            string xtmp;
            if (xmateriaallijst.SelectedObjects.Count > 0)
            {
                if (xmateriaallijst.SelectedObjects.Count == 1)
                {
                    var mat = xmateriaallijst.SelectedObjects[0] as Materiaal;
                    if (mat != null)
                    {
                        xstatus.Text = $"{mat.Aantal} {mat.Eenheid} van {mat.Omschrijving}";
                        label9.Text = $"Afkeur ({mat.Eenheid})";
                        xverbruiktlabel.Text = $"Verbuikt({mat.Eenheid})";
                        xafkeurpercent.Text = mat.AfKeurProcent();
                    }
                    else
                    {
                        xtmp = xmateriaallijst.Items.Count == 1 ? "Materiaal" : "Materialen";
                        xstatus.Text = $"{xmateriaallijst.Items.Count} {xtmp}";
                        label9.Text = "Afkeur";
                        xverbruiktlabel.Text = "Verbruikt";
                        xafkeurpercent.Text = "0.00%";
                    }
                }
                else
                {
                    xtmp = xmateriaallijst.SelectedObjects.Count == 1 ? "Materiaal" : "Materialen";
                    xstatus.Text = $"{xmateriaallijst.SelectedObjects.Count} {xtmp} Geselecteerd";
                }
            }
            else
            {
                xtmp = xmateriaallijst.Items.Count == 1 ? "Materiaal" : "Materialen";
                xstatus.Text = $"{xmateriaallijst.Items.Count} {xtmp}";
            }

            Invalidate();
        }

        private void UpdateInfoFields(object matobject)
        {
            Materiaal mat = null;

            if (matobject is Materiaal materiaal)
                mat = materiaal;

            if (mat != null)
            {
                xomschrijving.Text = mat.Omschrijving;
                xartikelnr.Text = mat.ArtikelNr;
                xlocatie.Text = mat.Locatie;
                xaantal.SetValue((decimal) mat.Aantal);
                xafkeurvalue.Value = (decimal) mat.AantalAfkeur;
                xeenheid.SelectedIndex = mat.Eenheid.ToLower() == "m" ? 0 : 1;
                xafkeurpercent.Text = mat.AfKeurProcent();
                xverbruiktlabel.Text = $"Verbruik({mat.Eenheid})";
                xaantalperstuk.SetValue((decimal) mat.AantalPerStuk);
                xklaarzetimage.Image = mat.IsKlaarGezet ? Resources.check_1582 : null;
                xklaarzetlabel.Text = $"Klaar Gezet: {mat.Omschrijving}";
                xlocatielabel.Text = $"Op Locatie: {mat.Locatie}";
                xaantalklaarzetlabel.Text = $"Aantal: {mat.Aantal} {mat.Eenheid}";
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
                xklaarzetlabel.Text = "";
                xlocatielabel.Text = "";
                xaantalklaarzetlabel.Text = "";
            }

            xverwijderb.Enabled = xmateriaallijst.SelectedObjects.Count > 0;
            xwijzigmatb.Enabled = xmateriaallijst.SelectedObjects.Count == 1;
            UpdateStatus();
        }

        private bool UpdateMateriaal(Materiaal mat)
        {
            if (mat == null)
                return false;
            mat.Omschrijving = xomschrijving.Text;
            mat.ArtikelNr = xartikelnr.Text;
            mat.Locatie = xlocatie.Text;
            mat.Eenheid = xeenheid.SelectedIndex == 1 ? "Stuks" : "m";
            mat.AantalPerStuk = (double) xaantalperstuk.Value;
            mat.AantalAfkeur = (double) xafkeurvalue.Value;
            mat.IsKlaarGezet = xklaarzetimage.Image != null;
            return true;
        }

        private Materiaal CreateMaterial()
        {
            var mat = new Materiaal(Formulier);
            UpdateMateriaal(mat);
            return mat;
        }

        public bool SaveMaterials()
        {
            if (Formulier == null)
                return false;
            try
            {
                Formulier.Materialen = xmateriaallijst.Objects.Cast<Materiaal>().ToList();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void xmateriaallijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = "Klaar Zetten";
            if (xmateriaallijst.SelectedObjects.Count > 0)
            {
                var count = xmateriaallijst.SelectedObjects.Count;
                xmateriaalpanel.Tag = xmateriaallijst.SelectedObjects.Cast<Materiaal>().ToArray();
                if (count == 1)
                {
                    var mat = xmateriaallijst.SelectedObjects[0] as Materiaal;
                    if (mat != null) value = $"Klaar Zetten: {mat.Omschrijving}";
                }
                else
                {
                    value = $"Klaar Zetten: {count} Materialen";
                }
            }
            else
            {
                xmateriaalpanel.Tag = null;
            }

            UpdateStatus();
            xklaarzetlabel.Text = value;
            UpdateInfoFields(xmateriaallijst.SelectedObject);
        }

        private void xklaarzetimage_MouseEnter(object sender, EventArgs e)
        {
            xklaarzetpanel.BackColor = Color.AliceBlue;
        }

        private void xklaarzetimage_MouseLeave(object sender, EventArgs e)
        {
            xklaarzetpanel.BackColor = Color.Transparent;
        }

        private void xklaarzetimage_Click(object sender, EventArgs e)
        {
            if (xklaarzetimage.Image == null)
                xklaarzetimage.Image = Resources.check_1582;
            else xklaarzetimage.Image = null;
            var xchecked = xklaarzetimage.Image != null;
            var mats = xmateriaalpanel.Tag as Materiaal[];
            if (mats != null && mats.Length > 0)
            {
                foreach (var mat in mats)
                    mat.IsKlaarGezet = xchecked;
                xmateriaallijst.RefreshObjects(mats);
            }
        }

        private void xwijzigmatb_Click(object sender, EventArgs e)
        {
            var mats = xmateriaalpanel.Tag as Materiaal[];
            if (mats != null && mats.Length > 0)
            {
                UpdateMateriaal(mats[0]);
                xmateriaallijst.RefreshObject(mats[0]);
                UpdateInfoFields(mats[0]);
                UpdateStatus();
            }
        }

        private void xniewmatb_Click(object sender, EventArgs e)
        {
            if (xomschrijving.Text.Trim().Length < 4)
                XMessageBox.Show("Ongeldige Omschrijving!", "Ongeldig", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (xartikelnr.Text.Trim().Length < 4)
                XMessageBox.Show("Ongeldige Artikel Nr!", "Ongeldig", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrEmpty(xlocatie.Text.Trim()))
                XMessageBox.Show("Ongeldige Locatie!", "Ongeldig", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (xeenheid.SelectedIndex < 0)
                XMessageBox.Show("Ongeldige Eenheid! Kies een eenheid voor de aantallen.", "Ongeldig",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (xmateriaallijst.Objects.Cast<Materiaal>()
                .Any(x => x.ArtikelNr.ToLower() == xartikelnr.Text.ToLower()))
                XMessageBox.Show("Ongeldige ArtikelNr! ArtikelNr bestaal al, gebruik een andere artikel nr a.u.b. ",
                    "Ongeldig", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                xmateriaallijst.AddObject(CreateMaterial());
        }

        private void xverwijderb_Click(object sender, EventArgs e)
        {
            var mats = xmateriaalpanel.Tag as Materiaal[];
            if (mats != null && mats.Length > 0)
            {
                string txt;
                if (mats.Length == 1)
                {
                    var mat = mats[0];
                    txt = mat != null
                        ? $"Weetje zeker dat je '{mat.Omschrijving}' wilt verwijderen?"
                        : "Weetje zeker dat je het geselecteerde materiaal wilt verwijderen?";
                }
                else
                {
                    var count = xmateriaallijst.SelectedObjects.Count;
                    txt = $"Weetje zeker dat je alle '{count}' geselecteerd materialen wilt verwijderen?";
                }

                if (XMessageBox.Show(txt, "Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) ==
                    DialogResult.Yes) xmateriaallijst.RemoveObjects(mats);
            }
        }

        private void xmateriaallijst_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            var mat = (Materiaal) e.Model;
            if (mat == null) return;
            e.Title = mat.ArtikelNr;
            e.Text = mat.Omschrijving;
        }

        private void xaantalperstuk_ValueChanged(object sender, EventArgs e)
        {
            if (xmateriaallijst.SelectedObject is not Materiaal mat) return;
            mat.AantalPerStuk = (double) xaantalperstuk.Value;
            xmateriaallijst.RefreshObject(mat);
            xaantalperstuk.ValueChanged -= xaantalperstuk_ValueChanged;
            UpdateInfoFields(mat);
            xaantalperstuk.ValueChanged += xaantalperstuk_ValueChanged;
        }

        private void xafkeurvalue_ValueChanged(object sender, EventArgs e)
        {
            if (xmateriaallijst.SelectedObject is not Materiaal mat) return;
            mat.AantalAfkeur = (double) xafkeurvalue.Value;
            xmateriaallijst.RefreshObject(mat);
            UpdateInfoFields(mat);
            xafkeurvalue.ValueChanged -= xafkeurvalue_ValueChanged;
            UpdateInfoFields(mat);
            xafkeurvalue.ValueChanged += xafkeurvalue_ValueChanged;
        }

        private void xeenheid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xmateriaallijst.SelectedObject is not Materiaal mat || xeenheid.SelectedIndex < 0) return;
            mat.Eenheid = xeenheid.SelectedIndex == 0 ? "m" : "Stuks";
            xmateriaallijst.RefreshObject(mat);
            xeenheid.SelectedIndexChanged -= xeenheid_SelectedIndexChanged;
            UpdateInfoFields(mat);
            xeenheid.SelectedIndexChanged += xeenheid_SelectedIndexChanged;
        }

        private void xaantal_ValueChanged(object sender, EventArgs e)
        {
            if (xmateriaallijst.SelectedObject is not Materiaal mat) return;
            mat.Aantal = (double) xaantal.Value;
            xmateriaallijst.RefreshObject(mat);
            xaantal.ValueChanged -= xaantal_ValueChanged;
            UpdateInfoFields(mat);
            xaantal.ValueChanged += xaantal_ValueChanged;
        }
    }
}