using Forms;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Controls.TileView
{
    public partial class GroupedTileView : UserControl
    {
        public GroupInfoEntry GroupEntry { get; private set; } = new GroupInfoEntry();
        public GroupedTileView()
        {
            InitializeComponent();
        }

        public TileViewer Viewer => tileViewer1;

        public string GroupName { get => Viewer.GroupName;
            set
            {
                var old = GroupName;
                bool flag = !string.Equals(value, old, StringComparison.CurrentCultureIgnoreCase);
                Viewer.GroupName = value;
                xMainGroup.Text = value;
                if (flag)
                    Viewer.UpdateTiles(null, old);
            }
        }

        private void InitToolStripMenu()
        {
            tileViewer1.FlowDirection = GroupEntry.TileFlowDirection;
            int xindex = (int)tileViewer1.FlowDirection;
            var xitems = toolStripMenuItem1.DropDownItems.Cast<ToolStripItem>();
            int i = 0;
            foreach (var xitem in xitems)
            {
                if (xitem is ToolStripMenuItem item)
                {
                    item.Checked = i.ToString() == xindex.ToString();
                }
                i++;
            }
        }

        public void UpdateFilterTiles()
        {
            var xtiles = Manager.Opties.TileLayout.Where(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(x.GroupName, "filter", StringComparison.CurrentCultureIgnoreCase)).ToList();
            var xremove = xtiles.Where(x => Manager.Opties.Filters.All(f => f.ID != x.LinkID)).ToList();
            if (xremove.Count > 0)
                xremove.ForEach(f => Manager.Opties.TileLayout.Remove(f));
            xtiles = Manager.Opties.TileLayout.Where(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase)).ToList();
            for (int i = 0; i < xtiles.Count; i++)
                xtiles[i].TileIndex = i;
            xtiles = xtiles.Where(x => string.Equals(x.GroupName, "filter", StringComparison.CurrentCultureIgnoreCase)).ToList();
            xtiles.ForEach(f =>
            {
                var xent = Manager.Opties.Filters.FirstOrDefault(x => x.ID == f.LinkID);
                if (xent != null)
                {
                    f.Name = xent.Name;
                }
            });
        }

        private void ChooseBackgroundColor()
        {
            try
            {
                if (Manager.Opties == null) return;
                var xcolorpicker = new ColorDialog();
                xcolorpicker.AllowFullOpen = true;
                xcolorpicker.Color = Color.FromArgb(Manager.Opties.TileViewBackgroundColorRGB);
                xcolorpicker.AnyColor = true;
                if (xcolorpicker.ShowDialog() == DialogResult.OK)
                {
                    var gr = GetGroupEntry(true);
                    gr.TileViewBackgroundColorRGB = xcolorpicker.Color.ToArgb();
                    Manager.Opties.Save(null, false, false, false);
                    LoadTileViewer();
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void ShowTileLayoutEditor()
        {
            try
            {
                if (Manager.Opties?.TileLayout == null) return;
                var xtiles = Manager.Opties.TileLayout.Where(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                var xeditor = new TileEditorForm(xtiles.CreateCopy(), GroupEntry.TileFlowDirection, null);
                xeditor.Size = new Size(1200, 750);
                if (xeditor.ShowDialog() == DialogResult.OK)
                {
                    GroupEntry.TileFlowDirection = xeditor.Direction;
                    Manager.Opties.TileLayout.RemoveAll(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase));
                    Manager.Opties.TileLayout.AddRange(xeditor.SelectedEntries);
                    Manager.Opties.Save(null, false, false, false);
                    LoadTileViewer();
                }

            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xBeheerweergavetoolstrip_Click(object sender, System.EventArgs e)
        {
            xBeheerweergavetoolstrip.ShowDropDown();
        }

        private void wijzigGroepNaamToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void kiesAchtergrondKleurToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ChooseBackgroundColor();
        }

        private void beheerTileLayoutToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            ShowTileLayoutEditor();
        }

        private void xBeheerLijstenToolstripItem_Click(object sender, System.EventArgs e)
        {
            var xform = new BeheerTilesForm(GroupName);
            if (xform.ShowDialog() == DialogResult.OK)
            {
                LoadTileViewer();
            }
        }

        private void reserLayoutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var result = XMessageBox.Show(this, "Weetje zeker dat je de TileLayout wilt resetten?!\n\n" +
                                                "Alle gewijzigde tiles zullen ongedaan worden, toch doorgaan?",
                "TileLayout Resetten", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.No) return;
            if (Manager.Opties != null)
            {
                Manager.Opties.TileLayout = Manager.Opties.GetAllDefaultEntries(false, default, default);
                Manager.Opties.Save("Tiles gereset!");
                LoadTileViewer();
            }
        }

        public void LoadTileViewer()
        {
            if (Manager.Opties != null)
            {
                var gr = GetGroupEntry(true);
                if (gr != null)
                {
                    xMainGroup.Text = gr.Name;
                    tileViewer1.BackColor = Color.FromArgb(gr.TileViewBackgroundColorRGB);
                    InitToolStripMenu();
                    UpdateFilterTiles();
                }

                tileViewer1.LoadTiles(Manager.Opties.TileLayout);
            }
        }

        public void SaveTileLayout(bool save)
        {
            if (Manager.Opties != null)
            {
                var gr = GetGroupEntry(true);
                if (gr != null)
                {
                    gr.TileViewBackgroundColorRGB = this.BackColor.ToArgb();
                    gr.TileFlowDirection = Viewer.FlowDirection;
                }
                tileViewer1.SaveTiles(save);
            }
        }

        public GroupInfoEntry GetGroupEntry(bool addnew)
        {
            if (Manager.Opties?.GroupEntries != null)
            {
                var gr = Manager.Opties.GroupEntries.FirstOrDefault(x => string.Equals(x.Name, GroupName, StringComparison.CurrentCultureIgnoreCase));
                if (gr == null && addnew)
                {
                    gr = new GroupInfoEntry();
                    gr.Name = GroupName;
                    Manager.Opties.GroupEntries.Add(gr);
                }
                if (gr == null) return null;
                gr.Location = this.Location;
                gr.Size = this.Size;
                GroupEntry = gr;
            }
            return GroupEntry;
        }

        private void toolStripMenuItem1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var xindex = toolStripMenuItem1.DropDownItems.IndexOf(e.ClickedItem);
            if (xindex > -1)
            {
                var gr = GetGroupEntry(true);
                if (gr == null) return;
                gr.TileFlowDirection = (FlowDirection)xindex;
                Manager.Opties.Save(null, false, false, false);
                LoadTileViewer();
            }
        }

        private void naamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Manager.Opties?.TileLayout != null)
            {
                var items = Manager.Opties.TileLayout.Where(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase)).OrderBy(x => x.Text).ToList();
                Manager.Opties.TileLayout.RemoveAll(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase));
                Manager.Opties.TileLayout.AddRange(items);
                var index = 0;
                items.ForEach(x => x.TileIndex = index++);
                Manager.Opties.Save(null, false, false, false);
                LoadTileViewer();
            }
        }

        private void typeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Manager.Opties?.TileLayout != null)
            {
                var items = Manager.Opties.TileLayout.Where(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase)).OrderBy(x => x.GroupName).ToList();
                Manager.Opties.TileLayout.RemoveAll(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase));
                Manager.Opties.TileLayout.AddRange(items);
                var index = 0;
                items.ForEach(x => x.TileIndex = index++);
                Manager.Opties.Save(null, false, false, false);
                LoadTileViewer();
            }
        }

        private void kleurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Manager.Opties?.TileLayout != null)
            {
                var items = Manager.Opties.TileLayout.Where(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase)).OrderBy(x => x.TileColor.GetHue()).ToList();
                Manager.Opties.TileLayout.RemoveAll(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase));
                Manager.Opties.TileLayout.AddRange(items);
                var index = 0;
                items.ForEach(x => x.TileIndex = index++);
                Manager.Opties.Save(null, false, false, false);
                LoadTileViewer();
            }
        }

        private void wijzigGroepGrootteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void OnTileClicked(object sender, EventArgs args)
        {
            TileClicked?.Invoke(sender, args);
        }

        public void OnTilesLoaded(object sender, EventArgs args)
        {
            TilesLoaded?.Invoke(sender, args);
        }

        public event EventHandler TileClicked;
        public event EventHandler TilesLoaded;
    }
}
