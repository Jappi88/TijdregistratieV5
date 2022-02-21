using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class WerkPlekChooser : MetroBaseForm
    {
        public WerkPlekChooser(List<WerkPlek> plekken, string selected)
        {
            InitializeComponent();
            if (plekken is {Count: > 0})
            {
                Plekken = plekken;
                comboBox1.Items.AddRange(plekken.Select(x => x.Path).ToArray());
                if (!string.IsNullOrEmpty(selected))
                    selected = plekken.FirstOrDefault(x => x.Path.ToLower().Contains(selected.ToLower()))?.Path;
                if (selected == null)
                    comboBox1.SelectedIndex = 0;
                else comboBox1.SelectedItem = selected;
                comboBox1.Select();
                comboBox1.Focus();
            }
        }

        public WerkPlekChooser(string[] plekken, string selected)
        {
            InitializeComponent();
            if (plekken is {Length: > 0})
            {
                comboBox1.Items.AddRange(plekken.Cast<object>().ToArray());
                if (selected == null)
                    comboBox1.SelectedIndex = 0;
                else comboBox1.SelectedItem = selected;
                comboBox1.Select();
                comboBox1.Focus();
            }
        }

        public List<WerkPlek> Plekken { get; }

        public WerkPlek Selected { get; private set; }

        public string SelectedName { get; private set; }

        public string Title
        {
            get => Text;
            set
            {
                Text = value;
                Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1 && Plekken != null && comboBox1.SelectedIndex < Plekken.Count)
            {
                Selected = Plekken[comboBox1.SelectedIndex];
                DialogResult = DialogResult.OK;
            }
            else if (comboBox1.SelectedItem != null)
            {
                SelectedName = comboBox1.SelectedItem.ToString();
                DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}