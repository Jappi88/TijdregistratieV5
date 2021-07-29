﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Rpm.Productie;

namespace Forms
{
    public partial class WerkPlekChooser : MetroFramework.Forms.MetroForm
    {
        public WerkPlekChooser(List<WerkPlek> plekken)
        {
            InitializeComponent();
            if (plekken != null && plekken.Count > 0)
            {
                Plekken = plekken;
                comboBox1.Items.AddRange(plekken.Select(x => x.Path).ToArray());
                comboBox1.SelectedIndex = 0;
            }
        }

        public WerkPlekChooser(string[] plekken)
        {
            InitializeComponent();
            if (plekken != null && plekken.Length > 0)
            {
                comboBox1.Items.AddRange(plekken.Cast<object>().ToArray());
                comboBox1.SelectedIndex = 0;
            }
        }

        public List<WerkPlek> Plekken { get; }

        public WerkPlek Selected { get; private set; }

        public string SelectedName { get; private set; }

        public string Title
        {
            get => this.Text;
            set
            {
                this.Text = value;
                this.Invalidate();
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