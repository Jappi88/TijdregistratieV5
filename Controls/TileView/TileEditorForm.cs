using System.Collections.Generic;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Controls
{
    public partial class TileEditorForm : MetroBaseForm
    {
        public TileEditorForm()
        {
            InitializeComponent();
        }

        public TileEditorForm(TileInfoEntry entry) : this()
        {
            tileEditorUI1.InitEntry(entry);
        }

        public TileEditorForm(List<TileInfoEntry> tiles, FlowDirection direction, TileInfoEntry entry) : this()
        {
            tileEditorUI1.InitEntries(tiles, direction, entry);
        }

        public TileInfoEntry SelectedEntry
        {
            get => tileEditorUI1.InfoEntry;
            set => tileEditorUI1.InitEntry(value);
        }

        public FlowDirection Direction
        {
            get => tileEditorUI1.Direction;
            set => tileEditorUI1.Direction = value;
        }

        public List<TileInfoEntry> SelectedEntries => tileEditorUI1.Tiles;
    }
}