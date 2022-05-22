using Forms.MetroBase;
using Rpm.Misc;
using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class DatumChanger : MetroBaseForm
    {
        public DatumChanger()
        {
            InitializeComponent();
            xfieldpanel.Height = 40;
        }

        private void InitSize()
        {
            try
            {
                var size = DisplayText.MeasureString(this.Font);
                size.Height += 200;
                this.MinimumSize = new System.Drawing.Size(this.Width, size.Height);
                this.Height = size.Height;
                xaddtimepanel.Visible = xextratijdcheckbox.Checked;
                xdatepicker.Visible = xwijzigdatumcheckbox.Checked;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DateTime SelectedValue
        {
            get => xdatepicker.Value;
            set => xdatepicker.Value = value;
        }

        public string DateFormat
        {
            get => xdatepicker.CustomFormat;
            set => xdatepicker.CustomFormat = value;
        }

        public string DisplayText
        {
            get => xmessage.Text;
            set => xmessage.Text = value;
        }

        public bool AddTime => xextratijdcheckbox.Checked;

        public TimeSpan TimeToAdd => new TimeSpan((int)xdagen.Value, (int)xuren.Value, (int)xmin.Value, 0, 0);

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
            InitSize();
            xdatepicker.Select();
            xdatepicker.Focus();
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                DialogResult = DialogResult.OK;
            }
        }

        private void xwijzigdatumcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            InitSize();
        }
    }
}