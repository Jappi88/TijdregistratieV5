using NPOI.SS.UserModel;
using ProductieManager.Rpm.ExcelHelper;
using ProductieManager.Rpm.Settings;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Forms.Excel;
using ProductieManager.Properties;

namespace Forms
{
    public partial class ExcelOptiesForm : MetroFramework.Forms.MetroForm
    {
        public ExcelOptiesForm()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.mimetypes_excel_32x32);
            imageList1.Images.Add(Resources.check_1582);
            ((OLVColumn) xOptiesView.Columns[0]).ImageGetter = GetImageIndex;
        }

        public bool IsSelectDialog { get; set; }

        public object GetImageIndex(object model)
        {
            if (model is ExcelSettings settings)
            {
                if (settings.IsSelected) return 1;
            }

            return 0;
        }

        public List<ExcelSettings> Settings { get; set; }

        public void LoadSettings(List<ExcelSettings> settings)
        {
            xOptiesView.SetObjects(settings);
            var properties = typeof(IProductieBase).GetProperties().Where(x => x.CanRead).ToArray();
            xBeschikbareColumns.SetObjects(properties);
            if (xOptiesView.Items.Count > 0)
            {
                xOptiesView.Items[0].Selected = true;
                xOptiesView.SelectedItem?.EnsureVisible();
            }
        }

        public void LoadOpties(UserSettings opties)
        {
            try
            {
                Settings = opties.ExcelColumns.CreateCopy();
                LoadSettings(Settings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private List<Color> GetCollors()
        {
            var xreturn = new List<Color>();
            var props = typeof(IndexedColors).GetFields().Select(x => (IndexedColors) x.GetValue("Index")).ToArray();
            if (props.Length > 0)
            {
                xreturn.AddRange(props.Select(prop => Color.FromArgb(prop.RGB[0], prop.RGB[1], prop.RGB[2])).ToArray());
            }

            return xreturn;
        }

        private short GetColorIndex(Color color)
        {
            var props = typeof(IndexedColors).GetFields().Select(x=> ((IndexedColors)x.GetValue("Index"))).ToArray();
            if (props.Length > 0)
            {
                //var rgb = $"{color.R:X2}{color.G:X2}{color.B:X2}";
                var prop = props.FirstOrDefault(x =>
                    string.Equals( ColorFromRGB(x.RGB).Name, color.Name, StringComparison.CurrentCultureIgnoreCase));
                if (prop == null) return -1;
                var xindex = ((IndexedColors) prop).Index;
                return xindex;
            }

            return -1;
        }

        public Color GetColorFromIndex(short index)
        {
            try
            {
                var color = IndexedColors.ValueOf(index);
                if (color == null) return Color.Empty;
                return ColorFromRGB(color.RGB);
            }
            catch (Exception e)
            {
                return Color.Empty;
            }
           
        }

        public Color ColorFromRGB(byte[] rgb)
        {
            return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
        }

        private void xColumnKleurB_Click(object sender, System.EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry colentry)
            {
                var colordialog = new ColorPickerForm();
                colordialog.Title = $"Kies achtergrond kleur voor '{colentry.Naam}'";
                colordialog.SetKleuren(GetCollors());
                colordialog.SelectedColor = xColumnKleurB.BackColor;
                if (colordialog.ShowDialog() == DialogResult.OK)
                {
                    var xindex = GetColorIndex(colordialog.SelectedColor);
                    if (xindex == -1) return;
                    xColumnKleurB.BackColor = colordialog.SelectedColor;
                    xTextKleurB.BackColor = colordialog.SelectedColor;
                    colentry.ColumnColorIndex = xindex;
                    xZichtbareColumnsView.RefreshObject(colentry);
                }
            }
        }

        private void xTextKleurB_Click(object sender, System.EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry colentry)
            {
                var colordialog = new ColorPickerForm();
                colordialog.Title = $"Kies textkleur voor '{colentry.Naam}'";
                colordialog.SetKleuren(GetCollors());
                colordialog.SelectedColor = xTextKleurB.ForeColor;
                if (colordialog.ShowDialog() == DialogResult.OK)
                {
                    var xindex = GetColorIndex(colordialog.SelectedColor);
                    if (xindex == -1) return;
                    xColumnKleurB.ForeColor = colordialog.SelectedColor;
                    xTextKleurB.ForeColor = colordialog.SelectedColor;
                    colentry.ColumnTextColorIndex = xindex;
                    xZichtbareColumnsView.RefreshObject(colentry);
                }
            }
        }

        private void xInstellingenView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xOptiesView.SelectedObject is ExcelSettings settings)
            {
                xZichtbareColumnsView.SetObjects(settings.Columns);
                if (xZichtbareColumnsView.Items.Count > 0)
                {
                    xZichtbareColumnsView.Items[0].Selected = true;
                    xZichtbareColumnsView.SelectedItem?.EnsureVisible();
                }
                else UpdateSelectedFields();
            }
            else
            {
                xZichtbareColumnsView.SetObjects(new List<ExcelColumnEntry>());
                UpdateSelectedFields();
            }
        }

        private void xZichtbareColumnsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedFields();
        }

        private void xItemUp_Click(object sender, EventArgs e)
        {
            if (xOptiesView.SelectedObject is ExcelSettings settings &&
                xZichtbareColumnsView.SelectedObject is ExcelColumnEntry ent)
            {
                var index = settings.Columns.IndexOf(ent);
                if (index <  1) return;
                settings.Columns.Remove(ent);
                settings.Columns.Insert(index -1 , ent);
                xZichtbareColumnsView.RemoveObject(ent);
                xZichtbareColumnsView.InsertObjects(index -1 , new ExcelColumnEntry[] { ent });
                xZichtbareColumnsView.SelectedObject = ent;
                xZichtbareColumnsView.SelectedItem?.EnsureVisible();
            }
        }

        private void xItemDown_Click(object sender, EventArgs e)
        {
            if (xOptiesView.SelectedObject is ExcelSettings settings &&
                xZichtbareColumnsView.SelectedObject is ExcelColumnEntry ent)
            {
                var index = settings.Columns.IndexOf(ent);
                if (index > settings.Columns.Count - 1) return;
                settings.Columns.Remove(ent);
                settings.Columns.Insert(index + 1, ent);
                xZichtbareColumnsView.RemoveObject(ent);
                xZichtbareColumnsView.InsertObjects(index + 1, new ExcelColumnEntry[] {ent});
                xZichtbareColumnsView.SelectedObject = ent;
                xZichtbareColumnsView.SelectedItem?.EnsureVisible();
            }
        }

        private void xItemRight_Click(object sender, EventArgs e)
        {
            AddSelectedProperty();
        }

        private void AddSelectedProperty()
        {
            if (xOptiesView.SelectedObject is ExcelSettings settings &&
                xBeschikbareColumns.SelectedObject is PropertyInfo prop)
            {
                if (settings.Columns.Any(x =>
                    string.Equals(x.Naam, prop.Name, StringComparison.CurrentCultureIgnoreCase)))
                    return;
                var xnew = new ExcelColumnEntry(prop.Name);
                settings.Columns.Add(xnew);
                xZichtbareColumnsView.AddObject(xnew);
                xZichtbareColumnsView.SelectedObject = xnew;
                xZichtbareColumnsView.SelectedItem?.EnsureVisible();
            }
        }

        private void xItemDelete_Click(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObjects.Count > 0 && xOptiesView.SelectedObject is ExcelSettings settings)
            {
                var xcount = xZichtbareColumnsView.SelectedObjects.Count;
                var x1 = xcount == 1 ? $"'{(xZichtbareColumnsView.SelectedObject as ExcelColumnEntry)?.Naam}'" : $"{xcount} columns";
                if (XMessageBox.Show($"Weetje zeker dat je {x1} wilt verwijderen?", "Columns Verwijderen",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.None)
                    return;
                var xitems = xZichtbareColumnsView.SelectedObjects.Cast<ExcelColumnEntry>().ToArray();
                int xc = 0;
                foreach (var item in xitems)
                    if (settings.Columns.Remove(item))
                        xc++;
                if (xc > 0)
                {
                    xZichtbareColumnsView.SetObjects(settings.Columns);
                    UpdateSelectedFields();
                }
            }
        }

        private void xAddOptieButton_Click(object sender, EventArgs e)
        {
            var xtb = new TextFieldEditor();
            xtb.Title = "Vul in een optie naam";
            if (xtb.ShowDialog() == DialogResult.OK)
            {
                var txt = xtb.SelectedText;
                if (Settings.Any(x => string.Equals(x.Name, txt, StringComparison.CurrentCultureIgnoreCase)))
                {
                    XMessageBox.Show($"'{txt}' bestaat al!", "Bestaat Al", MessageBoxIcon.Exclamation);
                }
                else
                {
                    var xs = new ExcelSettings(txt);
                    xs.IsSelected = !Settings.Any(x => x.IsSelected);
                    Settings.Add(xs);
                    LoadSettings(Settings);
                }
            }
        }

        private void xDeleteOptieButton_Click(object sender, EventArgs e)
        {
            if (xOptiesView.SelectedObjects.Count > 0)
            {
                var xcount = xOptiesView.SelectedObjects.Count;
                var x1 = xcount == 1 ? $"'{(xOptiesView.SelectedObject as ExcelSettings)?.Name}'" : $"{xcount} opties";
                if (XMessageBox.Show($"Weetje zeker dat je {x1} wilt verwijderen?", "Opties Verwijderen",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.None)
                    return;
                var xitems = xOptiesView.SelectedObjects.Cast<ExcelSettings>().ToArray();
                int xc = 0;
                foreach (var item in xitems)
                    if (Settings.Remove(item))
                        xc++;
                if (xc > 0)
                {
                    xOptiesView.SetObjects(Settings);
                }
            }
        }

        private void UpdateSelectedFields()
        {
            if (xOptiesView.SelectedObject is ExcelSettings settings && xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                xColumnTextBox.Text = entry.ColumnText?.Trim();
                xColumnFormatTextbox.Text = entry.ColumnFormat?.Trim();
                xColumnBreedte.SetValue(entry.ColumnBreedte);
                xWijzigColumnBreedte.Visible = false;
                xColumnBreedte.Enabled = !entry.AutoSize;
                xautoculmncheckbox.Checked = entry.AutoSize;
                xverborgencheckbox.Checked = entry.IsVerborgen;
                switch (entry.Type)
                {
                    case CalculationType.None:
                        xGeenBerekeningRadio.Checked = true;
                        break;
                    case CalculationType.SOM:
                        xSomAllesRadio.Checked = true;
                        break;
                    case CalculationType.Gemiddeld:
                        xBerekenGemiddeldRadio.Checked = true;
                        break;
                }
                switch (entry.ColorType)
                {
                    case ColorRuleType.None:
                        xGeenKleurRadio.Checked = true;
                        xColumnKleurB.BackColor = Color.White;
                        xColumnKleurB.ForeColor = Color.Black;
                        xTextKleurB.ForeColor = Color.Black;
                        xTextKleurB.BackColor = Color.White;
                        break;
                    case ColorRuleType.Static:
                        xVasteKleurRadio.Checked = true;
                        var color = GetColorFromIndex(entry.ColumnColorIndex);
                        xColumnKleurB.BackColor = color.IsEmpty?Color.White : color;
                        xTextKleurB.BackColor = color.IsEmpty ? Color.White : color;
                        color = GetColorFromIndex(entry.ColumnTextColorIndex);
                        xTextKleurB.ForeColor = color.IsEmpty ? Color.Black : color;
                        xColumnKleurB.ForeColor = color.IsEmpty ? Color.Black : color;
                        break;
                    case ColorRuleType.Dynamic:
                        xVariableColorRadio.Checked = true;
                        var xitems = entry.KleurRegels;
                        var x1 = xitems.Count == 1 ? "Regel" : "Regels";
                        xColorRegelStatusLabel.Text = $@"{xitems.Count} {x1} aangemaakt";
                        break;
                }

                xItemUp.Enabled = settings.Columns.IndexOf(entry) > 0;
                xItemDown.Enabled = settings.Columns.IndexOf(entry) < settings.Columns.Count -1;
            }
            else
            {
                xColumnTextBox.Text = "";
                xColumnFormatTextbox.Text = "";
                xColumnBreedte.Enabled = false;
                xautoculmncheckbox.Checked = false;
                xverborgencheckbox.Checked = false;
                xGeenBerekeningRadio.Checked = true;
                xGeenKleurRadio.Checked = true;
                xItemUp.Enabled = false;
                xItemDown.Enabled = false;
                xItemDelete.Enabled = false;
            }
            xDeleteOptieButton.Enabled = xOptiesView.SelectedObjects.Count > 0;
            xEditOpties.Enabled = xOptiesView.SelectedObject is ExcelSettings;
            xItemDelete.Enabled = xZichtbareColumnsView.SelectedObjects.Count > 0;
            if (xOptiesView.SelectedObject is ExcelSettings &&
                xZichtbareColumnsView.SelectedObject is ExcelColumnEntry &&
                xBeschikbareColumns.SelectedObject is PropertyInfo prop)
            {
                xItemRight.Enabled = !xZichtbareColumnsView.Objects?.Cast<ExcelColumnEntry>().Any(x =>
                    string.Equals(prop.Name, x.Naam, StringComparison.CurrentCultureIgnoreCase)) ?? false;
            }
            else xItemRight.Enabled = false;
        }

        private void xGeenKleurRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColorRadios();
        }

        private void UpdateColorRadios()
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                if (xGeenKleurRadio.Checked)
                {
                    entry.ColorType = ColorRuleType.None;
                    xStaticColorPanel.Visible = false;
                    xKleurRegelPanel.Visible = false;
                }
                else if (xVasteKleurRadio.Checked)
                {
                    entry.ColorType = ColorRuleType.Static;
                    xStaticColorPanel.Visible = true;
                    xKleurRegelPanel.Visible = false;
                }
                else if (xVariableColorRadio.Checked)
                {
                    entry.ColorType = ColorRuleType.Dynamic;
                    xStaticColorPanel.Visible = false;
                    xKleurRegelPanel.Visible = true;
                }
            }
            else
            {
                xGeenKleurRadio.Checked = true;
                xStaticColorPanel.Visible = false;
                xKleurRegelPanel.Visible = false;
            }
        }

        private void xVasteKleurRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColorRadios();
        }

        private void xVariableColorRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColorRadios();
        }

        private void xColumnTextBox_TextChanged(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                xWijzigColumnText.Visible = !string.Equals(entry.ColumnText?.Trim(), xColumnTextBox.Text.Trim(),
                    StringComparison.CurrentCultureIgnoreCase);
            }
            else
                xWijzigColumnText.Visible = false;
        }

        private void xWijzigColumnText_Click(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                entry.ColumnText = xColumnTextBox.Text.Trim();
                xWijzigColumnText.Visible = false;
            }
        }

        private void xBeschikbareColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xOptiesView.SelectedObject is ExcelSettings &&
                xBeschikbareColumns.SelectedObject is PropertyInfo prop)
            {
                xItemRight.Enabled = !xZichtbareColumnsView.Objects?.Cast<ExcelColumnEntry>().Any(x =>
                    string.Equals(prop.Name, x.Naam, StringComparison.CurrentCultureIgnoreCase)) ?? false;
            }
            else xItemRight.Enabled = false;
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xOpslaan_Click(object sender, EventArgs e)
        {
            if (IsSelectDialog && !Settings.Any(x => x.IsSelected))
            {
                XMessageBox.Show("Selecteer een optie om te gebruiken", "Selecteer Optie", MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void xGeenBerekeningRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                if (xGeenBerekeningRadio.Checked)
                    entry.Type = CalculationType.None;
            }
        }

        private void xSomAllesRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                if (xSomAllesRadio.Checked)
                    entry.Type = CalculationType.SOM;
            }
        }

        private void xBerekenGemiddeldRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                if (xBerekenGemiddeldRadio.Checked)
                    entry.Type = CalculationType.Gemiddeld;
            }
        }

        private void xautoculmncheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                entry.AutoSize = xautoculmncheckbox.Checked;
                xColumnBreedte.Enabled = !entry.AutoSize;
            }
        }

        private void xverborgencheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                entry.IsVerborgen = xverborgencheckbox.Checked;
            }
        }

        private void xOptiesView_DoubleClick(object sender, EventArgs e)
        {
            if (xOptiesView.SelectedObject is ExcelSettings setting)
            {
                var enabled = !setting.IsSelected;
                if (enabled)
                    Settings.ForEach(x => x.IsSelected = false);
                setting.IsSelected = enabled;
                xOptiesView.RefreshObjects(Settings);
                if (enabled && IsSelectDialog)
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void xBeschikbareColumns_DoubleClick(object sender, EventArgs e)
        {
            AddSelectedProperty();
        }

        private void xEditOpties_Click(object sender, EventArgs e)
        {
            if (xOptiesView.SelectedObject is ExcelSettings setting)
            {
                var xtb = new TextFieldEditor();
                xtb.Title = $"Wijzig naam voor '{setting.Name}'";
                xtb.SelectedText = setting.Name;
                if (xtb.ShowDialog() == DialogResult.OK)
                {
                    var txt = xtb.SelectedText;
                    if (string.Equals(setting.Name, txt, StringComparison.CurrentCultureIgnoreCase)) return;
                    if (Settings.Any(x => string.Equals(x.Name, txt, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        XMessageBox.Show($"'{txt}' bestaat al!", "Bestaat Al", MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        setting.Name = txt;
                        xOptiesView.RefreshObject(setting);
                    }
                }
            }
        }

        private void xKiesKleurRegels_Click(object sender, EventArgs e)
        {
            if (xOptiesView.SelectedObject is ExcelSettings setting &&
                xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                var regelform = new KleurRegelsForm();
                regelform.Title = $"Kies een kleur regel voor '{entry.Naam}'";
                regelform.InitColorRules(entry.KleurRegels, entry.Naam);
                if (regelform.ShowDialog() == DialogResult.OK)
                {
                    entry.KleurRegels = regelform.KleurRegels;
                    xZichtbareColumnsView.RefreshObject(entry);
                    UpdateSelectedFields();
                }
            }
        }

        private void xColumnFormatTextbox_TextChanged(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                xWijzigColumnFormat.Visible = !string.Equals(entry.ColumnFormat?.Trim(), xColumnFormatTextbox.Text.Trim(),
                    StringComparison.CurrentCultureIgnoreCase);
            }
            else
                xWijzigColumnFormat.Visible = false;
        }

        private void xWijzigColumnFormat_Click(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                entry.ColumnFormat = xColumnFormatTextbox.Text.Trim();
                xWijzigColumnFormat.Visible = false;
            }
        }

        private void xColumnBreedte_ValueChanged(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                xWijzigColumnBreedte.Visible = entry.ColumnBreedte != xColumnBreedte.Value;
            }
            else
                xWijzigColumnBreedte.Visible = false;
        }

        private void xWijzigColumnBreedte_Click(object sender, EventArgs e)
        {
            if (xZichtbareColumnsView.SelectedObject is ExcelColumnEntry entry)
            {
                entry.ColumnBreedte = (int) xColumnBreedte.Value;
                xWijzigColumnBreedte.Visible = false;
            }
        }
    }
}
