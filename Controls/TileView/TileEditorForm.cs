using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Misc;

namespace Controls
{
    public partial class TileEditorForm : MetroBaseForm
    {
        public TileInfoEntry SelectedEntry
        {
            get => tileEditorUI1.InfoEntry;
            set => tileEditorUI1.InitEntry(value);
        }

        public TileEditorForm()
        {
            InitializeComponent();
        }

        public TileEditorForm(TileInfoEntry entry) : this()
        {
           tileEditorUI1.InitEntry(entry);
        }
    }
}
