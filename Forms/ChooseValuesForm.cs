using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Forms.MetroBase;
using MetroFramework;
using MetroFramework.Controls;

namespace Forms
{
    public partial class ChooseValuesForm : MetroBaseForm
    {
        public ChooseValuesForm()
        {
            InitializeComponent();
        }

        public void SetChooseItems(List<string> items)
        {
            try
            {
                xlayoutpanel.SuspendLayout();
                xlayoutpanel.Controls.Clear();
                foreach (var item in items)
                {
                    var check = CreateCheckbox(item, false);
                    if (check != null)
                    {
                        xlayoutpanel.Controls.Add(check);
                    }
                }

                xlayoutpanel.ResumeLayout(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public List<string> SelectedValues
        {
            get => GetValues();
            set => SetValues(value);
        }

        public void SetValues(List<string> values)
        {
            try
            {
                try
                {
                    var xvalues = xlayoutpanel.Controls.Cast<Control>().ToList();
                    foreach (var v in xvalues)
                    {
                        if (v is MetroCheckBox xcheck)
                            xcheck.Checked = values.Any(x =>
                                string.Equals(x, v.Text, StringComparison.CurrentCultureIgnoreCase));
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private MetroCheckBox CreateCheckbox(string name, bool selected)
        {
            var x = new MetroCheckBox();
            x.Text = name;
            x.Checked = selected;
            x.UseStyleColors = true;
            x.AutoSize = true;
            x.Style = this.Style;
            x.BackColor = Color.Transparent;
            x.FontSize = MetroCheckBoxSize.Tall;
            x.FontWeight = MetroCheckBoxWeight.Bold;
            return x;
        }

        public List<string> GetValues()
        {
            var xret = new List<string>();
            try
            {
                var xvalues = xlayoutpanel.Controls.Cast<Control>().ToList();
                foreach (var v in xvalues)
                {
                    if (v is MetroCheckBox {Checked: true} && !xret.Any(x =>
                            string.Equals(x, v.Text, StringComparison.CurrentCultureIgnoreCase)))
                        xret.Add(v.Text);
                }
            }
            catch (Exception e)
            {
                // ignored
            }

            return xret;
        }
    }
}
