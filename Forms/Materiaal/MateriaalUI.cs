using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms
{
    public partial class MateriaalUI : UserControl
    {
        public MateriaalUI()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.bolts_screws_32x32);
            imageList1.Images.Add(Resources.bolts_screws_32x32.CombineImage(Resources.check_1582,1.5));
            ((OLVColumn) xmateriaallijst.Columns[0]).ImageGetter = GetImage;
        }

        public ProductieFormulier Formulier { get; set; }

        public object GetImage(object sender)
        {
            if (sender is Materiaal {IsKlaarGezet: true}) return 1;

            return 0;
        }

        public void InitMaterialen(ProductieFormulier form)
        {
            Formulier = form;
            xmateriaallijst.SetObjects(form.Materialen);
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
                    }
                    else
                    {
                        xtmp = xmateriaallijst.Items.Count == 1 ? "Materiaal" : "Materialen";
                        xstatus.Text = $"{xmateriaallijst.Items.Count} {xtmp}";
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

            if (mat?.ArtikelNr != null)
            {
                xklaarzetimage.Image = mat.IsKlaarGezet ? Resources.check_1582 : null;
                xklaarzetlabel.Text = $"Klaar Gezet: {mat.Omschrijving}";
            }
            else
            {
                xklaarzetimage.Image = null;
                xklaarzetlabel.Text = "";
            }

            xverwijderb.Enabled = xmateriaallijst.SelectedObjects.Count > 0;
            xwijzigmatb.Enabled = xmateriaallijst.SelectedObjects.Count == 1;
            UpdateStatus();
        }

        private Materiaal CreateMaterial()
        {
            var xnew = new NewMateriaalForm(Formulier);
            Materiaal sel = null;
            var mats = xmateriaallijst.Objects.OfType<Materiaal>().ToList();
            while (true)
            {
                if (xnew.ShowDialog(this) == DialogResult.OK)
                {
                    sel = xnew.SelectedMateriaal;
                }
                else
                {
                    sel = null;
                    break;
                }
                if (sel != null && mats.Any(x => string.Equals(x.ArtikelNr, sel.ArtikelNr, StringComparison.CurrentCultureIgnoreCase)))
                {
                    XMessageBox.Show(this, $"ArtikelNr '{sel.ArtikelNr}' bestaat al...\n\n" +
                        $"Kies een andere ArtikelNr a.u.b.", "ArtikelNr Bestaat Al!", MessageBoxIcon.Warning);
                    continue;
                }
                break;
            }
            xnew.Dispose();
            return sel;
        }

        private void WijzigMaterial(Materiaal mat)
        {
            var xnew = new NewMateriaalForm(mat);
            if (xnew.ShowDialog(this) == DialogResult.OK)
            {
                xmateriaallijst.RefreshObject(xnew.SelectedMateriaal);
            }
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
            if (xmateriaallijst.SelectedObjects.Count > 0)
            {
                var mats = xmateriaallijst.SelectedObjects.OfType<Materiaal>().ToList();
                foreach (var mat in mats)
                    mat.IsKlaarGezet = xchecked;
                xmateriaallijst.RefreshObjects(mats);
            }
        }

        private void xwijzigmatb_Click(object sender, EventArgs e)
        {
            if (xmateriaallijst.SelectedObject is Materiaal mat)
            {
                WijzigMaterial(mat);
                xmateriaallijst.RefreshObject(mat);
                UpdateInfoFields(mat);
                UpdateStatus();
            }
        }

        private void xniewmatb_Click(object sender, EventArgs e)
        {
            var mat = CreateMaterial();
            if (mat != null)
            {
                xmateriaallijst.AddObject(mat);
                xmateriaallijst.SelectedObject = mat;
                xmateriaallijst.SelectedItem?.EnsureVisible();
            }
        }

        private void xverwijderb_Click(object sender, EventArgs e)
        {
            if (xmateriaallijst.SelectedObjects.Count > 0)
            {
                var mats = xmateriaallijst.SelectedObjects.OfType<Materiaal>().ToList();
                string txt;
                if (mats.Count == 1)
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

                if (XMessageBox.Show(this, txt, "Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) ==
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
    }
}