using System;
using System.Drawing;
using System.Windows.Forms;

namespace Forms
{
    public partial class TextFieldEditor : MetroFramework.Forms.MetroForm
    {
        public TextFieldEditor()
        {
            InitializeComponent();
        }

        public string SelectedText
        {
            get => xtextfield.Text;
            set => xtextfield.Text = value;
        }

        public string Title
        {
            get => this.Text;
            set => this.Text = value;
        }

        public Image FieldImage { get => ximage.Image; set => ximage.Image = value; }

        public bool MultiLine
        {
            get => xtextfield.Multiline;
            set
            {
                if (value && this.Height < 250)
                    this.Height = 250;
                if (!value && this.Height > 165)
                    this.Height = 165;
                xtextfield.Multiline = value;
            }
        }

        private void xok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void xanuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TextFieldEditor_Shown(object sender, EventArgs e)
        {
            if (xtextfield.TextLength > 0)
            {
                xtextfield.SelectAll();
                            }
            xtextfield.Select();
            xtextfield.Focus();
        }

        private void xtextfield_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && !MultiLine)
                DialogResult = DialogResult.OK;
        }
    }
}