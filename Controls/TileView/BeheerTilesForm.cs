using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Forms.MetroBase;
using NPOI.SS.Formula.Functions;
using Rpm.Productie;

namespace Controls
{
    public partial class BeheerTilesForm : MetroBaseForm
    {
        string _groupname;
        public BeheerTilesForm(string groupname)
        {
            InitializeComponent();
            _groupname = groupname;
            ((OLVColumn) xmainlist.Columns[0]).ImageGetter = (x) => 0;
            ((OLVColumn)xlist.Columns[0]).ImageGetter = (x) => 1;
            xmainlist.SetObjects(Manager.Opties.GetAllDefaultEntries(true, default, default));
            xlist.SetObjects(Manager.Opties.TileLayout?.Where(x=> string.Equals(x.Group, groupname, StringComparison.CurrentCultureIgnoreCase)).OrderBy(x=> x.TileIndex).ToList()??new List<TileInfoEntry>());
        }

        private void xopslaan_Click(object sender, System.EventArgs e)
        {
            if (Manager.Opties != null)
            {
                
                var items = xlist.Objects.Cast<TileInfoEntry>().ToList();
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].TileIndex = i;
                    items[i].Group = _groupname;
                }
                Manager.Opties.TileLayout.RemoveAll(x => string.Equals(x.Group, _groupname, StringComparison.CurrentCultureIgnoreCase));
                Manager.Opties.TileLayout.AddRange(items);
                Manager.Opties.Save(null, false, false, false);
            }

            DialogResult = DialogResult.OK;
        }

        private void Enable()
        {
            try
            {
                xmoveup.Enabled = xlist.SelectedItem is {Index: > 0};
                xmovedown.Enabled = xlist.SelectedItem != null && xlist.SelectedItem.Index < xlist.Items.Count -1;
                xdeletetile.Enabled = xlist.SelectedObjects.Count > 0;
                xedittile.Enabled = xlist.SelectedItem != null;
                xaddtile.Enabled = xmainlist.SelectedObjects.Count > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xmainlist_DoubleClick(object sender, System.EventArgs e)
        {
            if (xmainlist.SelectedObject is TileInfoEntry entry)
            {
                AddTile(entry);
            }
        }

        private void AddTile(TileInfoEntry entry)
        {
            try
            {
                var xtiles = xlist.Objects.Cast<TileInfoEntry>().ToList();
                var xtile = xtiles.FirstOrDefault(x => x.Equals(entry));
                if (xtile == null)
                {
                    var xname = entry.Name.Trim().Replace(" ", "_");
                    int count = xtiles.Count(x =>
                        string.Equals(xname, x.Name, StringComparison.CurrentCultureIgnoreCase));
                    if(count > 0)
                        xname += "_" + count;
                    entry.Name = xname;
                    xlist.AddObject(entry);
                    xtile = entry;
                }
                entry.Group = _groupname;
                xlist.SelectedObject = xtile;
                xlist.SelectedItem?.EnsureVisible();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xlist_DoubleClick(object sender, System.EventArgs e)
        {
            EditTile();
        }

        private void EditTile()
        {
            if (xlist.SelectedObject is TileInfoEntry entry)
            {
                var xform = new TileEditorForm(entry);
                if (xform.ShowDialog(this) == DialogResult.OK)
                {
                    xlist.RefreshObject(xform.SelectedEntry);
                    xlist.SelectedObject = xform.SelectedEntry;
                    xlist.SelectedItem?.EnsureVisible();
                    Enable();
                }
            }
        }

        private void xmainlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enable();
        }

        private void xmoveup_Click(object sender, EventArgs e)
        {
            if (xlist.SelectedItem != null && xlist.SelectedIndex > 0 && xlist.SelectedObject is TileInfoEntry entry)
            {
                var index = xlist.SelectedIndex - 1;
                xlist.RemoveObject(entry);
                xlist.InsertObjects(index, new TileInfoEntry[] {entry});
                xlist.SelectedObject = entry;
                xlist.SelectedItem?.EnsureVisible();
            }
        }

        private void xmovedown_Click(object sender, EventArgs e)
        {
            if (xlist.SelectedItem != null && xlist.SelectedIndex < xlist.Items.Count -1 && xlist.SelectedObject is TileInfoEntry entry)
            {
                var index = xlist.SelectedIndex + 1;
                xlist.RemoveObject(entry);
                xlist.InsertObjects(index, new TileInfoEntry[] { entry });
                xlist.SelectedObject = entry;
                xlist.SelectedItem?.EnsureVisible();
            }
        }

        private void xdeletetile_Click(object sender, EventArgs e)
        {
            if (xlist.SelectedObjects.Count > 0)
            {
                var xitems = xlist.SelectedObjects.Cast<TileInfoEntry>().ToList();
                xlist.RemoveObjects(xitems);
                Enable();
            }
        }

        private void xedittile_Click(object sender, EventArgs e)
        {
            EditTile();
        }

        private void xaddtile_Click(object sender, EventArgs e)
        {
            if (xmainlist.SelectedObjects.Count > 0)
            {
                var xitems = xmainlist.SelectedObjects.Cast<TileInfoEntry>().ToList();
                foreach (var item in xitems)
                {
                    AddTile(item);
                }

                xlist.Select();
                xlist.Focus();
                xlist.SelectedObjects = xitems;
            }
        }
    }
}
