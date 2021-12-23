using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using Rpm.Misc;
using Rpm.Productie;

namespace Controls
{
    public partial class ObjectEditorUI : UserControl
    {
        public ObjectEditorUI()
        {
            InitializeComponent();
        }

        public object Instance { get; set; }

        public List<string> ExcludeItems { get; set; } = new List<string>() { "versie", "verwachtleverdatum", "viewimageindex" };

        public List<Type> DisplayTypes { get; set; } = new List<Type>()
            { typeof(string), typeof(int), typeof(decimal), typeof(double), typeof(DateTime), typeof(Enum)};

        private Control GetControlByProperty(PropertyInfo property)
        {
            var value = property.GetValue(Instance);
            switch (value)
            {
                case string xstring:
                    var xtb = new MetroTextBox();
                    xtb.ShowClearButton = true;
                    xtb.Text = xstring.Trim();
                    xtb.Name = property.Name;
                    xtb.Tag = property;
                    xtb.TextChanged += ValueChanged;
                    xtb.Width = 250;
                    xtb.Multiline = true;
                    xtb.FontSize = MetroTextBoxSize.Medium;
                    xtb.Height = 100;
                    xtb.Width = 200;
                    return xtb;
                case int xint:
                    var xnr = new NumericUpDown();
                    xnr.Maximum = int.MaxValue;
                    xnr.Minimum = int.MinValue;
                    xnr.Tag = property;
                    xnr.Name = property.Name;
                    xnr.Value = xint;
                    xnr.Width = 125;
                    xnr.ValueChanged += ValueChanged;
                    return xnr;
                case double xdouble:
                    var xdbl = new NumericUpDown();
                    xdbl.Maximum = decimal.MaxValue;
                    xdbl.Minimum = decimal.MinValue;
                    xdbl.DecimalPlaces = 2;
                    xdbl.ThousandsSeparator = true;
                    xdbl.Tag = property;
                    xdbl.Name = property.Name;
                    xdbl.SetValue((decimal)xdouble);
                    xdbl.ValueChanged += ValueChanged;
                    xdbl.Width = 125;
                    return xdbl;
                case decimal xdecimal:
                    var xdm = new NumericUpDown();
                    xdm.Maximum = decimal.MaxValue;
                    xdm.Minimum = decimal.MinValue;
                    xdm.DecimalPlaces = 2;
                    xdm.ThousandsSeparator = true;
                    xdm.Tag = property;
                    xdm.Name = property.Name;
                    xdm.SetValue(xdecimal);
                    xdm.ValueChanged += ValueChanged;
                    xdm.Width = 125;
                    return xdm;
                case DateTime xdate:
                    var xdt = new DateTimePicker();
                    xdt.CustomFormat = "dddd dd MMMM yyyy HH:mm 'uur'";
                    xdt.Format = DateTimePickerFormat.Custom;
                    xdt.Tag = property;
                    xdt.Name = property.Name;
                    xdt.SetValue(xdate);
                    xdt.ValueChanged += ValueChanged;
                    xdt.Width = 275;
                    return xdt;
                case Enum xenum:
                    var xnums = Enum.GetNames(xenum.GetType());
                    var xcmb = new MetroComboBox();
                    xcmb.Tag = property;
                    xcmb.Name = property.Name;
                    xcmb.Items.AddRange(xnums.Select(x => (object)x).ToArray());
                    xcmb.SelectedItem = value.ToString();
                    if (xcmb.SelectedIndex == -1 && xcmb.Items.Count > 0)
                        xcmb.SelectedIndex = 0;
                    xcmb.SelectedIndexChanged += ValueChanged;
                    xcmb.Width = 200;
                    xcmb.FontSize = MetroComboBoxSize.Medium;
                    return xcmb;
            }
            return null;
        }

        private void ValueChanged(object sender, EventArgs arg)
        {
            PropertyInfo info = null;
            object value = null;

            switch (sender)
            {
                case MetroTextBox tb:
                    value = tb.Text.Trim();
                    info = tb.Tag as PropertyInfo;
                    break;
                case NumericUpDown nr:

                    info = nr.Tag as PropertyInfo;
                    if (info != null)
                    {
                        if (info.PropertyType == typeof(int))
                            value = (int)nr.Value;
                        else if (info.PropertyType == typeof(double))
                            value = (double)nr.Value;
                        else
                            value = nr.Value;
                    }

                    break;
                case DateTimePicker dt:
                    value = dt.Value;
                    info = dt.Tag as PropertyInfo;
                    break;
                case MetroComboBox cb:
                    info = cb.Tag as PropertyInfo;
                    if (info != null)
                    {
                        value = Enum.Parse(info.PropertyType, cb.SelectedItem.ToString(), true);
                    }

                    break;
            }

            if (value == null || info == null) return;
            info.SetValue(Instance, value);
        }

        public void InitInstance(object instance)
        {
            Instance = instance;
            xFlowPanel.SuspendLayout();
            xFlowPanel.Controls.Clear();
            if (instance != null)
            {
                var props = instance.GetType().GetProperties().Where(x => x.CanRead && x.CanWrite).ToList();
                foreach (var allow in DisplayTypes)
                {
                    var xprops = props.Where(x => ((allow == typeof(Enum) && x.PropertyType.IsEnum) ||
                                                   x.PropertyType.IsAssignableFrom(allow)) &&
                                                  !ExcludeItems.Any(s => string.Equals(s, x.Name,
                                                      StringComparison.CurrentCultureIgnoreCase))).ToList();
                    if (xprops.Count > 0)
                    {
                        foreach (var xprop in xprops)
                        {
                            var xcontrol = GetControlByProperty(xprop);
                            if (xcontrol == null) continue;
                            var desc = typeof(IProductieBase).GetPropertyDescription(xprop.Name);
                            toolTip1.SetToolTip(xcontrol, desc);
                            var xpanel = new Panel();

                            var xlabel = new Label();
                            xlabel.AutoSize = true;
                            xlabel.Text = xprop.Name;
                            xlabel.Font = new Font(xFlowPanel.Font.FontFamily, 10, FontStyle.Bold);
                            toolTip1.SetToolTip(xlabel, desc);
                            xpanel.Controls.Add(xlabel);
                            xlabel.Dock = DockStyle.Top;
                            var xwidth = xlabel.Width > xcontrol.Width ? xlabel.Width : xcontrol.Width;
                            xpanel.Size = new Size(xwidth + 5, xcontrol.Height + xlabel.Height + 10);
                            xpanel.Controls.Add(xcontrol);
                            xcontrol.Dock = DockStyle.Fill;
                            xcontrol.BringToFront();
                            xFlowPanel.Controls.Add(xpanel);
                        }
                    }
                }
            }
            xFlowPanel.ResumeLayout(true);
        }
    }
}
