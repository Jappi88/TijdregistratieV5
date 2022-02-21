using System;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class DbPathChooser : MetroBaseForm
    {
        public DbPathChooser()
        {
            InitializeComponent();
        }

        public string SelectedPath
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fb = new FolderBrowserDialog();
            fb.Description = "Kies een folder die de database bevat, of die je wilt gebruiken als standaart folder";
            fb.SelectedPath = string.IsNullOrWhiteSpace(SelectedPath) ? Application.StartupPath : SelectedPath;
            if (fb.ShowDialog() == DialogResult.OK) textBox1.Text = fb.SelectedPath;
        }

        private void xok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}