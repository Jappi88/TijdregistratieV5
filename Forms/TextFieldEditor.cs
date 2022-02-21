﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class TextFieldEditor : MetroBaseForm
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
            get => Text;
            set
            {
                Text = value;
                Invalidate();
            }
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
            set => xextrafieldcheck.Checked = value;
        }

        public bool EnableSecondaryField
        {
            get => xextrafieldcheck.Visible;
            set
            {
                if (UseSecondary && !value)
                    UseSecondary = false;
                xsecondaryPanel.Visible = value;
            }
        }

        public Image FieldImage
        {
            get => ximage.Image;
            set => ximage.Image = value;
        }

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
            if (!xextrafieldcheck.Checked && xtextfield.Text.Trim().Length < MinimalTextLength ||
                xextrafieldcheck.Checked && xsecondarytextbox.Text.Trim().Length < MinimalTextLength)
                XMessageBox.Show(this, "Ongeldige waarde", "Ongeldig", MessageBoxIcon.Warning);
            else
                DialogResult = DialogResult.OK;
        }

        private void xanuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TextFieldEditor_Shown(object sender, EventArgs e)
        {
            if (xtextfield.TextLength > 0) xtextfield.SelectAll();

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
            var xheight = xextrafieldcheck.Checked ? 85 : 22;
            xsecondaryPanel.Height = xheight;
            if (xextrafieldcheck.Checked)
                xsecondarytextbox.Select();
            else xtextfield.Select();
            if (MultiLine)
                xheight += 85;
            var xbase = 200 + xheight;

            MinimumSize = new Size(MinimumSize.Width, xbase);
            Height = xbase;
        }
    }
}