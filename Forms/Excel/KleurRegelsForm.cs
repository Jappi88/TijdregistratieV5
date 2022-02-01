using BrightIdeasSoftware;
using NPOI.SS.UserModel;
using ProductieManager.Forms;
using ProductieManager.Rpm.ExcelHelper;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms.Excel
{
    public partial class KleurRegelsForm : MetroFramework.Forms.MetroForm
    {
        public KleurRegelsForm()
        {
            InitializeComponent();
            ((OLVColumn) xRegelView.Columns[0]).AspectGetter = GetRegelText;
            ((OLVColumn)xRegelView.Columns[0]).ImageGetter = GetRegelImage;
        }

        private object GetRegelText(object model)
        {
            if (model is ExcelRegelEntry regel)
            {
                return $"[{(regel.IsFontColor ? "TextKleur" : "ColumnKleur")}]{regel.Filter.ToString()}";
            }

            return "N.V.T.";
        }

        private object GetRegelImage(object model)
        {
            if (model is ExcelRegelEntry regel)
            {
                if (regel.ColorIndex > -1)
                    return imageList1.Images.IndexOfKey(regel.ColorIndex.ToString());
                if(regel.ColorRGB != 0)
                    return imageList1.Images.IndexOfKey(regel.ColorRGB.ToString());
            }

            return 0;
        }

        public string Variable { get; private set; }
        public bool UseExcelColors { get; private set; }

        public string Title
        {
            get => this.Text;
            set
            {
                this.Text = value;
                this.Invalidate();
            }
        }

        public List<ExcelRegelEntry> KleurRegels { get; private set; } = new List<ExcelRegelEntry>();

        public void InitColorRules(List<ExcelRegelEntry> regels, string variable, bool useExcelColors)
        {
            Variable = variable;
            UseExcelColors = useExcelColors;
            var selected = xRegelView.SelectedObject;
            KleurRegels = regels.CreateCopy();
            UpdateImages();
            xRegelView.SetObjects(KleurRegels);
            xRegelView.SelectedObject = selected;
            xRegelView.SelectedItem?.EnsureVisible();
            UpdateFields();
        }

        private void UpdateImages()
        {
            imageList1.Images.Clear();
            foreach (var regel in KleurRegels)
            {
                if (regel.ColorIndex > -1)
                {
                    imageList1.Images.Add(regel.ColorIndex.ToString(),
                        CreateImage(ExcelColumnEntry.GetColorFromIndex(regel.ColorIndex), imageList1.ImageSize.Width,
                            imageList1.ImageSize.Height));
                }
                else if(regel.ColorRGB != 0)
                {
                    imageList1.Images.Add(regel.ColorRGB.ToString(),
                        CreateImage(Color.FromArgb(regel.ColorRGB), imageList1.ImageSize.Width,
                            imageList1.ImageSize.Height));
                }
            }
        }

        private Image CreateImage(Color color, int width, int height)
        {
            Bitmap Bmp = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(Bmp))
            using (SolidBrush brush = new SolidBrush(color))
            {
                gfx.FillRectangle(brush, 0, 0, width, height);
            }

            return (Image)Bmp;
        }



        private void AddCriteria()
        {
            short colorindex = -1;
            int rgb = 0;
            if (UseExcelColors)
            {
                colorindex = ExcelColumnEntry.GetColorIndex(xcolorPanel.BackColor);
                if (colorindex == -1)
                {
                    XMessageBox.Show(this, $"Kies een kleur waarvoor je een regel wilt.", "Kies Kleur",
                        MessageBoxIcon.Information);
                    return;
                }

                ExcelRegelEntry regel = null;
                if ((regel = xRegelView.Objects.Cast<ExcelRegelEntry>()
                        .FirstOrDefault(x => x.ColorIndex == colorindex)) != null)
                {
                    XMessageBox.Show(this, $"Er is al een regel gemaakt voor de huidig gekozen kleur.", "Kleur Bestaat Al",
                        MessageBoxIcon.Information);
                    xRegelView.SelectedObject = regel;
                    xRegelView.SelectedItem?.EnsureVisible();
                    return;
                }
            }
            else
            {
                rgb = xcolorPanel.BackColor.ToArgb();
                if (rgb is 0)
                {
                    XMessageBox.Show(this, $"Kies een kleur waarvoor je een regel wilt.", "Kies Kleur",
                        MessageBoxIcon.Information);
                    return;
                }

                ExcelRegelEntry regel = null;
                if ((regel = xRegelView.Objects.Cast<ExcelRegelEntry>()
                        .FirstOrDefault(x => x.ColorRGB == rgb && x.IsFontColor == xTextKleur.Checked)) != null)
                {
                    xRegelView.SelectedObject = regel;
                    xRegelView.SelectedItem?.EnsureVisible();
                    XMessageBox.Show(this, $"Er is al een regel gemaakt voor de huidig gekozen kleur.", "Kleur Bestaat Al",
                        MessageBoxIcon.Information);
                    return;
                }
            }
            var xvar = Variable;
            var xnewcrit = new NewFilterEntry(typeof(IProductieBase), xvar, false)
                { Title = $"Nieuwe KleurRegel voor {xvar}" };

            if (xnewcrit.ShowDialog() == DialogResult.OK)
            {
                var xregel = new ExcelRegelEntry()
                {
                    Filter = xnewcrit.SelectedFilter,
                    IsFontColor = xTextKleur.Checked
                };
                if (colorindex > -1)
                    xregel.ColorIndex = colorindex;
                else xregel.ColorRGB = rgb;
                KleurRegels.Add(xregel);
                UpdateImages();
                xRegelView.AddObject(xregel);
                xRegelView.SelectedObject = xregel;
                xRegelView.SelectedItem?.EnsureVisible();
            }

            UpdateFields();
        }

        private void WijzigSelectedCriteria()
        {
            if (xRegelView.SelectedObject is ExcelRegelEntry regel)
            {
                var xnewcrit = new NewFilterEntry(typeof(IProductieBase), regel.Filter)
                    {Title = $"Wijzig KleurRegel voor {regel.Filter.Criteria}"};

                if (xnewcrit.ShowDialog() == DialogResult.OK)
                {
                    regel.Filter = xnewcrit.SelectedFilter;
                    xRegelView.RefreshObject(regel);
                    xRegelView.SelectedObject = regel;
                    xRegelView.SelectedItem?.EnsureVisible();
                }

                UpdateFields();
            }
        }

        private void DeleteSelectedCriteria()
        {
            if (xRegelView.SelectedObjects.Count > 0)
            {

                if (XMessageBox.Show(this, $"Weetje zeker dat je alle geselecteerde regels wilt verwijderen?",
                        "Regel(s) Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                    DialogResult.No) return;
                var selected = xRegelView.SelectedObjects.Cast<ExcelRegelEntry>().ToList();
                foreach (var sel in selected)
                {
                    KleurRegels.Remove(sel);
                    xRegelView.RemoveObject(sel);
                }

                UpdateImages();
                UpdateFields();
            }
        }

        private void xKiesKleur_Click(object sender, System.EventArgs e)
        {
            if (UseExcelColors)
            {
                var colordialog = new ColorPickerForm();
                colordialog.Title = Title;
                colordialog.SetKleuren(ExcelColumnEntry.GetExcelCollors());
                colordialog.SelectedColor = xcolorPanel.BackColor;
                if (colordialog.ShowDialog() == DialogResult.OK)
                {
                    var xindex = ExcelColumnEntry.GetColorIndex(colordialog.SelectedColor);
                    if (xindex == -1) return;
                    xcolorPanel.BackColor = colordialog.SelectedColor;
                }

                return;
            }

            var xpicker = new ColorDialog();
            xpicker.AllowFullOpen = true;
            xpicker.AnyColor = true;
            xpicker.Color = xcolorPanel.BackColor;
            if (xpicker.ShowDialog() == DialogResult.OK)
            {
                xcolorPanel.BackColor = xpicker.Color;
            }
        }

        private void xAddOptieButton_Click(object sender, System.EventArgs e)
        {
            AddCriteria();
        }

        private void xEditOpties_Click(object sender, System.EventArgs e)
        {
            WijzigSelectedCriteria();
        }

        private void xDeleteOptieButton_Click(object sender, System.EventArgs e)
        {
            DeleteSelectedCriteria();
        }

        private void xok_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void xanuleren_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xRegelView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFields();
        }

        private void UpdateFields()
        {
            xDeleteOptieButton.Enabled = xRegelView.SelectedObjects.Count > 0;
            xEditOpties.Enabled = xRegelView.SelectedObject is ExcelRegelEntry;
            xWijzigKleur.Enabled = xRegelView.SelectedObject is ExcelRegelEntry;
            if (xRegelView.SelectedObject is ExcelRegelEntry entry)
            {
                xRegelTextPanel.Text = entry.Filter?.ToHtmlString();
                xTextKleur.Checked = entry.IsFontColor;
                xcolorPanel.BackColor = entry.ColorIndex > -1
                    ? ExcelColumnEntry.GetColorFromIndex(entry.ColorIndex)
                    : Color.FromArgb(entry.ColorRGB);
            }
            else
            {
                xRegelTextPanel.Text = "";
                xTextKleur.Checked = false;
            }
        }

        private void EditColor_Click(object sender, EventArgs e)
        {
            if (xRegelView.SelectedObject is ExcelRegelEntry regel)
            {
                try
                {
                    if(UseExcelColors)
                    {
                        var colorindex = ExcelColumnEntry.GetColorIndex(xcolorPanel.BackColor);
                        if (colorindex == -1)
                        {
                            throw new Exception("Kies een kleur waarvoor je een regel wilt wijzigen.");
                        }

                        if (colorindex == regel.ColorIndex) return;
                        if (xRegelView.Objects.Cast<ExcelRegelEntry>()
                            .Any(x => x.ColorIndex == colorindex))
                        {
                            throw new Exception("Er is al een regel gemaakt voor de huidig gekozen kleur.");
                        }
                        regel.ColorIndex = colorindex;
                        regel.ColorRGB = 0;
                    }
                    else
                    {
                        var rgb = xcolorPanel.BackColor.ToArgb();
                        if(rgb == 0)
                            throw new Exception("Kies een kleur waarvoor je een regel wilt wijzigen.");
                        if (rgb == regel.ColorRGB) return;
                        if (xRegelView.Objects.Cast<ExcelRegelEntry>()
                            .Any(x => rgb == x.ColorRGB && x.IsFontColor == regel.IsFontColor))
                        {
                            throw new Exception("Er is al een regel gemaakt voor de huidig gekozen kleur.");
                        }

                        regel.ColorIndex = -1;
                        regel.ColorRGB = rgb;
                    }
                    UpdateImages();
                    xRegelView.RefreshObject(regel);
                    UpdateFields();
                }
                catch (Exception exception)
                {
                    XMessageBox.Show(this, exception.Message, "Waarschuwing!", MessageBoxIcon.Exclamation);
                }
            }
        }

        private void xTextKleur_CheckedChanged(object sender, EventArgs e)
        {
            if (xRegelView.SelectedObject is ExcelRegelEntry regel)
            {
                regel.IsFontColor = xTextKleur.Checked;
                xRegelView.RefreshObject(regel);
            }
        }

        private void xRegelView_DoubleClick(object sender, EventArgs e)
        {
            WijzigSelectedCriteria();
        }
    }
}
