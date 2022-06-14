using System;
using System.Drawing;
using System.Windows.Forms;

namespace Forms
{
    public partial class TextFieldEditor : Forms.MetroBase.MetroBaseForm
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

        public int MinimalTextLength { get; set; } = 2;

        public string SecondaryDescription
        {
            get => xdescriptiontext.Text;
            set => xdescriptiontext.Text = value;
        }

        public string SecondaryText
        {
            get => xsecondarytextbox.Text;
            set => xsecondarytextbox.Text = value;
        }

        public string SecondaryCheckBoxText
        {
            get => xextrafieldcheck.Text;
            set => xextrafieldcheck.Text = value;
        }

        public bool UseSecondary
        {
            get => xextrafieldcheck.Checked;
            set=> xextrafieldcheck.Checked = value;
        }

        public bool EnableSecondaryField
        {
            get => xextrafieldcheck.Visible;
            set
            {
                if (UseSecondary && !value)
                    UseSecondary = false;
                xsecondaryPanel.Visible = value;
                SetSize();
            }
        }

        public Image FieldImage { get => ximage.Image; set => ximage.Image = value; }

        public bool MultiLine
        {
            get => xtextfield.Multiline;
            set
            {
                xtextfield.Multiline = value;
                SetSize();
            }
        }

        private void xok_Click(object sender, EventArgs e)
        {
            var x1 = MinimalTextLength == 1 ? "character" : "characters";
            if ((!xextrafieldcheck.Checked && xtextfield.Text.Trim().Length < MinimalTextLength) || (xextrafieldcheck.Checked && xsecondarytextbox.Text.Trim().Length < MinimalTextLength))
                XMessageBox.Show(this, $"Ongeldige waarde\n\n" +
                    $"De waarde moet minimaal {MinimalTextLength} {x1} bevatten", "Ongeldig", MessageBoxIcon.Warning);
            else
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
            if (e.KeyCode == Keys.Enter && !MultiLine)
            {
                e.Handled = true;
                xok_Click(this, EventArgs.Empty);
            }
        }

        private void xextrafieldcheck_CheckedChanged(object sender, EventArgs e)
        {
            SetSize();
        }

        private void SetSize()
        {
            var xheight = xextrafieldcheck.Visible ? 22 : 0;
           xheight += xextrafieldcheck.Checked ? 85 : 0;
            xsecondaryPanel.Height = xheight;
            if (xextrafieldcheck.Checked)
                xsecondarytextbox.Select();
            else xtextfield.Select();
            if (MultiLine)
                xheight += 85;
            var xbase = 200 + xheight;

            this.MinimumSize = new Size(this.MinimumSize.Width, xbase);
            this.Height = xbase;
        }
    }
}