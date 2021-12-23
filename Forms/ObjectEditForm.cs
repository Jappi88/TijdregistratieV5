using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;

namespace Forms
{
    public partial class ObjectEditForm : MetroFramework.Forms.MetroForm
    {
        public ObjectEditForm()
        {
            InitializeComponent();
        }

        public void Init(object instance)
        {
            objectEditorUI1.InitInstance(instance);
        }

        private void xOpslaan_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void xsluiten_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public string Title
        {
            get => this.Text;
            set
            {
                this.Text = value;
                this.Invalidate();
            }
        }

        public object Instance
        {
            get => objectEditorUI1.Instance;
            set => objectEditorUI1.Instance = value;
        }

        public List<string> ExcludeItems
        {
            get => objectEditorUI1.ExcludeItems;
            set => objectEditorUI1.ExcludeItems = value;
        }

        public List<Type> AllowedDataTypes
        {
            get => objectEditorUI1.DisplayTypes;
            set => objectEditorUI1.DisplayTypes = value;
        }
    }
}
