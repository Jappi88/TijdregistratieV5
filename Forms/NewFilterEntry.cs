using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Forms;
using MetroFramework.Forms;
using Org.BouncyCastle.Asn1.X509.Qualified;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace ProductieManager.Forms
{
    public partial class NewFilterEntry : MetroForm
    {
        public PropertyInfo Property { get; private set; }

        public Type PropertyType { get; private set; }

        public FilterEntry SelectedFilter { get; private set; }

        public string Title
        {
            get => Text;
            set
            {
                Text = value;
                this.Invalidate();
            }
        }

        public NewFilterEntry(Type type, string propertyname, bool useoperand)
        {
            InitializeComponent();
            xoperandtype.Enabled = useoperand;
            xoperandtype.Visible = true;
            xvaluepanel.Height = 70;
            Size = base.MinimumSize;
            SelectedFilter = new FilterEntry
            {
                FilterType = FilterType.None,
                OperandType = Operand.ALS,
                PropertyName = propertyname,
                ChildEntries = new List<FilterEntry>()
            };
            InitOperand();
            InitFields(type, propertyname);
        }

        public NewFilterEntry(Type type, FilterEntry entry)
        {
            InitializeComponent();
            xoperandtype.Enabled = entry.OperandType != Operand.ALS;
            xoperandtype.Visible = true;
            numericUpDown1.Value = entry.RangeValue;
            if (entry.CompareWithProperty)
                xVergelijkVariableCheck.Checked = true;
            SelectedFilter = entry.CreateCopy();
            xvaluepanel.Height = 70;
            Size = base.MinimumSize;
            InitOperand();
            InitFields(type, entry.PropertyName);
            SetValue(entry.Value);
            xRangeDevider.SelectedItem = Enum.GetName(typeof(RangeDeviderType), entry.DeviderType);
        }

        private void InitOperand()
        {
            if (xoperandtype.Enabled)
                xoperandtype.Items.AddRange(new object[] {"EN", "OF"});
            else xoperandtype.Items.Add("ALS");
        }

        //GelijkAan,
        //Bevat,
        //Lager,
        //Hoger,
        //NietGelijkAan,
        //BevatNiet
        private void InitFields(Type type, string propertyname)
        {
            PropertyType = type;
            Property = type.GetPropertyInfo(propertyname);
            if (Property == null)
                throw new Exception($"'{propertyname}' bestaat niet, of is ongeldig!");
            xvariablenamelabel.Text = propertyname;
            xvaluetypes.Items.Clear();

            bool xflag = xVergelijkWaardeCheck.Checked;
            
            xtextvalue.Enabled = Property.PropertyType == typeof(string) && xflag;
            xdecimalvalue.Enabled = Property.PropertyType.IsNumericType() && xflag;
            xdatepanel.Enabled = Property.PropertyType == typeof(DateTime) && xflag;
            xComboPanel.Enabled = Property.PropertyType.IsEnum || !xflag;
            xcheckvalue.Enabled = Property.PropertyType == typeof(bool) && xflag;
            xEditValueRangePanel.Visible = !xflag && Property.PropertyType.IsNumericType();
            xtextvalue.Visible = xtextvalue.Enabled;
            xdecimalvalue.Visible = xdecimalvalue.Enabled;
            xdatepanel.Visible = xdatepanel.Enabled;
            xComboPanel.Visible = xComboPanel.Enabled;
            xcheckvalue.Visible = xcheckvalue.Enabled;
            var xvaltypes = FilterEntry.GetFilterStringTypesByType(Property.PropertyType);
            foreach (var xval in xvaltypes)
            {
                xvaluetypes.Items.Add(xval);
            }

            xRangeDevider.Items.Clear();
            xRangeDevider.Items.AddRange(Enum.GetNames(typeof(RangeDeviderType)).Select(x => (object) x).ToArray());
            xRangeDevider.SelectedIndex = 0;
            if (xComboPanel.Enabled)
            {
                xcombovalue.Items.Clear();
                if (xflag)
                {
                    var xnames = Enum.GetNames(Property.PropertyType);
                    foreach (var name in xnames)
                        xcombovalue.Items.Add(name);
                }
                else
                {
                    var xnames = PropertyType.GetProperties()
                        .Where(x => x.CanRead && x.PropertyType == Property.PropertyType).Select(x => x.Name);
                    foreach (var name in xnames)
                        xcombovalue.Items.Add(name);
                }

                if (xcombovalue.Items.Count > 0)
                    xcombovalue.SelectedIndex = 0;
            }

            if (SelectedFilter != null)
            {
                xvaluetypes.SelectedItem = Enum.GetName(typeof(FilterType), SelectedFilter.FilterType);
                SetValue(SelectedFilter.Value);
                if (xoperandtype.Enabled)
                {
                    xoperandtype.SelectedItem = Enum.GetName(typeof(Operand), SelectedFilter.OperandType);
                    if (xoperandtype.SelectedItem == null && xoperandtype.Items.Count > 0)
                        xoperandtype.SelectedIndex = 0;
                }
            }

            if (xvaluetypes.Items.Count > 0 && xvaluetypes.SelectedIndex < 0)
                xvaluetypes.SelectedIndex = 0;
            if (xoperandtype.Items.Count > 0 && xoperandtype.SelectedIndex < 0)
                xoperandtype.SelectedIndex = 0;
        }

        private void SetValue(object value)
        {
            try
            {
                if (value == null) return;
                switch (value)
                {
                    case string xvalue:
                        xtextvalue.Text = xvalue;
                        xcombovalue.SelectedItem = xvalue;
                        break;
                    case decimal xdecimal:
                        xdecimalvalue.SetValue(xdecimal);
                        break;
                    case double xdouble:
                        xdecimalvalue.SetValue((decimal) xdouble);
                        break;
                    case int xvalue:
                        xdecimalvalue.SetValue(xvalue);
                        break;
                    case DateTime xvalue:
                        var xval = xvalue;
                        if (xvalue.Year == 9999 && xvalue.Month == 1 && xvalue.Day == 1)
                        {
                            xval = DateTime.Now;
                            xhuidigeDatum.Checked = true;
                        }
                        else xhuidigeDatum.Checked = false;
                        if (xvalue.TimeOfDay > new TimeSpan())
                        {
                            xhuidigeTijd.Checked = false;
                            xval = xval.ChangeTime(xvalue.TimeOfDay);
                        }
                        else
                            xhuidigeTijd.Checked = true;

                        xdatevalue.Value = xval;
                        break;
                    case Enum xvalue:
                        var xvalueenum = Enum.GetName(xvalue.GetType(), xvalue);
                        xcombovalue.SelectedItem = xvalueenum;
                        break;
                    case bool xvalue:
                        xcheckvalue.Checked = xvalue;
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private object GetValue()
        {
            try
            {
                if (xtextvalue.Enabled)
                {
                    var xtxt = xtextvalue.Text.Trim();
                    if (xtxt.ToLower().StartsWith("vul in een criteria..."))
                        return null;
                    return xtxt;
                }

                if (xdecimalvalue.Enabled)
                    return xdecimalvalue.Value;
                if (xdatepanel.Enabled)
                {
                    var dt = xdatevalue.Value;
                    if (xhuidigeDatum.Checked)
                        dt = new DateTime(9999, 1, 1, dt.Hour, dt.Minute, 0,0);

                    if (xhuidigeTijd.Checked)
                        dt = dt.ChangeTime(new TimeSpan());

                    return dt;
                }


                if (xComboPanel.Enabled)
                    try
                    {
                        if (xVergelijkWaardeCheck.Checked)
                        {
                            var xenum = Enum.Parse(Property.PropertyType, xcombovalue.SelectedItem.ToString().Trim(),
                                true);
                            return xenum;
                        }
                        return xcombovalue.SelectedItem.ToString().Trim();
                    }
                    catch (Exception e)
                    {
                        return null;
                    }

                if (xcheckvalue.Enabled) return xcheckvalue.Checked;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        private void xok_Click(object sender, EventArgs e)
        {
            if (SaveChanges(true))
                DialogResult = DialogResult.OK;
        }

        private bool SaveChanges(bool showmessage)
        {
            if (!DoCheck(showmessage)) return false;
            if (Enum.TryParse<FilterType>(xvaluetypes.SelectedItem.ToString(), true, out var xfiltertype))
            {
                var value = GetValue();
                var operand = Operand.ALS;
                if (xoperandtype.Enabled && xoperandtype.SelectedItem != null)
                    try
                    {
                        operand = (Operand) Enum.Parse(typeof(Operand), xoperandtype.SelectedItem.ToString(), true);
                    }
                    catch (Exception exception)
                    {
                        if (showmessage)
                            XMessageBox.Show("Ongeldige operand type!", "Ongeldig", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        return false;
                    }

                if (value == null)
                {
                    if (showmessage)
                        XMessageBox.Show("Ongeldige Criteria!", "Ongeldig", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                var x = new FilterEntry
                {
                    FilterType = xfiltertype,
                    OperandType = xoperandtype.Enabled ? operand : Operand.ALS,
                    PropertyName = Property.Name,
                    RangeValue = (int)numericUpDown1.Value,
                    Value = value,
                    CompareWithProperty = xVergelijkVariableCheck.Checked
                };
                if (Enum.TryParse<RangeDeviderType>(xRangeDevider.SelectedItem.ToString(), out var xtype))
                    x.DeviderType = xtype;
                else x.DeviderType = RangeDeviderType.None;
                if (SelectedFilter != null)
                    x.ChildEntries = SelectedFilter.ChildEntries;
                SelectedFilter = x;
                UpdateHtmlText();
                return true;
            }

            return false;
        }

        private bool DoCheck(bool showmessage)
        {
            try
            {
                if (xoperandtype.Enabled)
                    if (xoperandtype.SelectedIndex < 0)
                        throw new Exception(
                            "Criteria operand is niet gekozen!\n\nKies of je criteria 'IS','OF' of 'EN' met de voorgaande criteria is");

                if (xvaluetypes.SelectedIndex < 0) throw new Exception("Kies eerst waar je criteria aan moet voldoen");

                if (xtextvalue.Enabled && string.IsNullOrEmpty(xtextvalue.Text.Trim()))
                    throw new Exception($"Vul in een criteria text waar '{xvariablenamelabel.Text}' aan moet voldoen");
                if (xComboPanel.Enabled && xcombovalue.SelectedItem == null)
                    throw new Exception($"Kies eem criteria waarde waar {xvariablenamelabel.Text} aan moet voldoen");
                return true;
            }
            catch (Exception e)
            {
                if (showmessage)
                    XMessageBox.Show(e.Message, "Ongeldige Criteria", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xtextvalue_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xtextvalue.Text.Trim()))
            {
                xtextvalue.Text = "Vul in een criteria...";
                xtextvalue.ForeColor = Color.Gray;
            }
        }

        private void xtextvalue_Enter(object sender, EventArgs e)
        {
            if (xtextvalue.Text.ToLower().Trim().StartsWith("vul in een criteria..."))
            {
                xtextvalue.Text = "";
                xtextvalue.ForeColor = Color.Black;
            }
        }

        private void xcheckvalue_CheckedChanged(object sender, EventArgs e)
        {
            xcheckvalue.Text = xcheckvalue.Checked ? "WAAR" : "NIET WAAR";
            if (xcheckvalue.Enabled)
                SaveChanges(false);
        }

        private void UpdateHtmlText()
        {
            xcriteriahtml.Text = SelectedFilter?.ToHtmlString() ?? "";
        }

        private void xvoorwaardenb_Click(object sender, EventArgs e)
        {
            if (SelectedFilter == null) return;
            var fs = new EditCriteriaForm(PropertyType,SelectedFilter.ChildEntries);
            if (fs.ShowDialog() == DialogResult.OK)
            {
                SelectedFilter.ChildEntries = fs.SelectedFilter;
                UpdateHtmlText();
            }
        }

        private void xoperandtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xoperandtype.Enabled)
                SaveChanges(false);
        }

        private void xvaluetypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xvaluetypes.Enabled)
                SaveChanges(false);
        }

        private void xcombovalue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xComboPanel.Enabled)
                SaveChanges(false);
        }

        private void xtextvalue_TextChanged(object sender, EventArgs e)
        {
            if (xtextvalue.Enabled && !string.IsNullOrEmpty(xtextvalue.Text.Trim()) &&
                !xtextvalue.Text.Trim().ToLower().Contains("vul in een criteria"))
                SaveChanges(false);
        }

        private void xdatevalue_ValueChanged(object sender, EventArgs e)
        {
            if (xdatepanel.Enabled)
                SaveChanges(false);
        }

        private void xdecimalvalue_ValueChanged(object sender, EventArgs e)
        {
            if (xdecimalvalue.Enabled)
                SaveChanges(false);
        }

        private void xVergelijkVariableCheck_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InitFields(PropertyType, Property?.Name);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void xVergelijkWaardeCheck_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                InitFields(PropertyType, Property?.Name);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (xEditValueRangePanel.Visible)
            {
                SaveChanges(false);
            }
        }

        private void xhuidigeDatum_CheckStateChanged(object sender, EventArgs e)
        {
            if (xdatepanel.Enabled)
            {
                if (xhuidigeDatum.Checked)
                {
                    if (xhuidigeTijd.Checked)
                        xdatevalue.Enabled = false;
                    else
                    {
                        xdatevalue.CustomFormat = "HH:mm";
                        xdatevalue.Enabled = true;
                    }
                }
                else
                {
                    xdatevalue.Enabled = true;
                    if (xhuidigeTijd.Checked)
                        xdatevalue.CustomFormat = "dddd dd MMMM yyyy";
                    else xdatevalue.CustomFormat = "dddd dd MMMM yyyy HH:mm";
                }

                SaveChanges(false);
            }
        }

        private void xhuidigeTijd_CheckedChanged(object sender, EventArgs e)
        {
            if (xdatepanel.Enabled)
            {
                if (xhuidigeTijd.Checked)
                {
                    if (xhuidigeDatum.Checked)
                    {
                        xdatevalue.Enabled = false;
                    }
                    else
                    {
                        xdatevalue.Enabled = true;
                        xdatevalue.CustomFormat = "dddd dd MMMM yyyy";
                    }
                }
                else
                {
                    xdatevalue.Enabled = true;
                    if (xhuidigeDatum.Checked)
                        xdatevalue.CustomFormat = "HH:mm";
                    else xdatevalue.CustomFormat = "dddd dd MMMM yyyy HH:mm";
                }

                SaveChanges(false);
            }
        }

        private void xRangeDevider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xEditValueRangePanel.Enabled)
            {
                SaveChanges(false);
            }
        }
    }
}