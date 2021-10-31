using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class DatumChanger : MetroForm
    {
        public DatumChanger()
        {
            InitializeComponent();
        }

        public DateTime SelectedValue
        {
            get => dateTimePicker1.Value;
            set => dateTimePicker1.Value = value;
        }

        public string DateFormat
        {
            get => dateTimePicker1.CustomFormat;
            set => dateTimePicker1.CustomFormat = value;
        }

        public string DisplayText
        {
            get => xmessage.Text;
            set => xmessage.Text = value;
        }

        public DialogResult ShowDialog(DateTime value, string title)
        {
            SelectedValue = value;
            DisplayText = title;
            return ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void DatumChanger_Shown(object sender, EventArgs e)
        {
            dateTimePicker1.Select();
            dateTimePicker1.Focus();
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                DialogResult = DialogResult.OK;
            }
        }
    }
}