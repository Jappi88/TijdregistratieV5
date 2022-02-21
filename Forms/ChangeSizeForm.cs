using System;
using System.Drawing;
using Forms.MetroBase;
using Rpm.Misc;

namespace Forms
{
    public partial class ChangeSizeForm : MetroBaseForm
    {
        public ChangeSizeForm()
        {
            InitializeComponent();
        }

        public ChangeSizeForm(Size size) : this()
        {
            SelectedSize = new Size(size.Width, size.Height);
        }

        public Size SelectedSize { get; set; } = new(256, 128);

        public void ChangeMinimumSize(Size size)
        {
            numericUpDown1.Minimum = size.Width;
            numericUpDown2.Minimum = size.Height;
        }

        public void ChangeMaximumSize(Size size)
        {
            numericUpDown1.Maximum = size.Width;
            numericUpDown2.Maximum = size.Height;
        }

        public void InitInfo()
        {
            numericUpDown1.SetValue(SelectedSize.Width);
            numericUpDown2.SetValue(SelectedSize.Height);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            SelectedSize = new Size((int) numericUpDown1.Value, (int) numericUpDown2.Value);
        }
    }
}