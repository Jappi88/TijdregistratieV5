using ProductieManager.Properties;
using ProductieManager.Rpm.ExcelHelper;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductieManager.Forms;
using Rpm.Various;

namespace Forms.Excel
{
    public partial class CreateWeekExcelForm : MetroFramework.Forms.MetroForm
    {
        private string ListName = "ExcelWeekOverzicht";
        public CreateWeekExcelForm()
        {
            InitializeComponent();
            InitFilterStrips();
            xweeknr.Value = DateTime.Now.GetWeekNr();
            xjaar.Value = DateTime.Now.Year;
        }

        private async void xOpslaan_Click(object sender, System.EventArgs e)
        {
            if (IsRunning())
            {
                StopWait();
                return;
            }
            var ofd = new SaveFileDialog
            {
                Title = "creëer Productie WeekOverzicht",
                Filter = "Xlsx Bestand|*.Xlsx",
                FileName = $"WeekOverzicht {xweeknr.Value}_{xjaar.Value}"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StartWait();
                try
                {
                    if (await ExcelWorkbook.CreateDagelijksProductieOverzicht((int) xweeknr.Value, (int) xjaar.Value,
                            ofd.FileName, IsRunning, GetActiveFilters()) && File.Exists(ofd.FileName) && xopenexcel.Checked)
                        Process.Start(ofd.FileName);
                }
                catch (Exception exception)
                {
                    XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
                }
                StopWait();
            }
        }

        private List<Filter> GetActiveFilters()
        {
            var xreturn = new List<Filter>();
            foreach (var item in xfiltersStrip.Items.Cast<ToolStripMenuItem>())
            {
                if(item.Tag is Filter filter)
                    xreturn.Add(filter);
            }

            return xreturn;
        }

        public void InitFilterStrips()
        {
            //verwijder alle gekozen filters.
            var items = xfiltersStrip.Items.Cast<ToolStripMenuItem>().ToList();
            ToolStripMenuItem menuitem = null;
            for (var i = 0; i < items.Count; i++)
                if (items[i].Tag != null)
                {
                    xfiltersStrip.Items.Remove(items[i]);
                }
                else if (items[i].DropDownItems.Count > 0)
                {
                    menuitem = items[i];
                    var dropitems = items[i].DropDownItems.Cast<ToolStripItem>().Where(x => x.Tag != null).ToList();
                    for (var j = 0; j < dropitems.Count; j++)
                        menuitem.DropDownItems.Remove(dropitems[j]);
                }
            
            //verwijder alle toegevoegde filters
            //items = xfiltersStripItem.DropDownItems.Cast<ToolStripItem>().Where(x => x.Tag != null).ToList();
            //for (int i = 0; i < items.Count; i++)
            //    xfiltersStripItem.DropDownItems.Remove(items[i]);
            //Voeg toe alle filters indien mogelijk
            if (Manager.Opties?.Filters == null || menuitem == null) return;
            foreach (var f in Manager.Opties.Filters)
            {
                var xitem = new ToolStripMenuItem(f.Name) { Image = Resources.add_1588, Tag = f };
                xitem.ToolTipText = f.ToString();
                menuitem.DropDownItems.Add(xitem);
                if (f.ListNames.Any(x =>
                        string.Equals(ListName, x, StringComparison.CurrentCultureIgnoreCase)))
                    AddFilterToolstripItem(f, false);
            }
        }

        private void xfiltersStripItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag == null)
            {
                var xf = new FilterEditor();
                if (xf.ShowDialog() == DialogResult.OK)
                {
                    InitFilterStrips();
                }
                //InitFilterStrips();
                //UpdateProductieList();
                //OnFilterChanged();
                return;
            }

            if (e.ClickedItem.Tag is Filter filter)
            {
                if (filter.ListNames.Any(x =>
                    string.Equals(ListName, x, StringComparison.CurrentCultureIgnoreCase)))
                    return;
                filter.ListNames.Add(ListName);
                AddFilterToolstripItem(filter, false);
            }
        }

        private bool AddFilterToolstripItem(Filter filter, bool docheck)
        {
            if (filter.ListNames.Any(x =>
                    string.Equals(ListName, x, StringComparison.CurrentCultureIgnoreCase)) &&
                docheck) return false;
            var ts = new ToolStripMenuItem(filter.Name) { Image = Resources.delete_1577, Tag = filter };
            ts.ToolTipText = filter.ToString();
            ts.Click += Ts_Click;
            xfiltersStrip.Items.Add(ts);
            return true;
        }

        private void Ts_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem { Tag: Filter filter } ts)
            {
                filter.ListNames.RemoveAll(x => string.Equals(x, ListName, StringComparison.CurrentCultureIgnoreCase));
                if (filter.IsTempFilter && filter.ListNames.Count == 0)
                    Manager.Opties?.Filters?.Remove(filter);
                //Manager.OnFilterChanged(this);
                ts = xfiltersStrip.Items.Cast<ToolStripItem>().FirstOrDefault(x =>
                    string.Equals(x.Text, filter.Name, StringComparison.CurrentCultureIgnoreCase));
                if (ts != null)
                    xfiltersStrip.Items.Remove(ts);
            }
        }

        private void xsluiten_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool _isbusy = false;
        private void StartWait()
        {
            if (_isbusy) return;
            _isbusy = true;
            xOpslaan.Text = "Stoppen";
            xOpslaan.Image = Resources.stop_red256_24890;
            xbezig.Visible = true;
            //Task.Run(async () =>
            //{
            //    int count = 0;
            //    while (_isbusy)
            //    {
            //        //var xvalue = ("Overzicht Aanmaken").PadRight(count + 19, '.');
            //        //xbezig.Invoke(new Action(() => xbezig.Text = xvalue));
            //        //count++;
            //        //if (count > 4)
            //        //    count = 0;
            //        await Task.Delay(1000);
            //    }

            //    //if (!this.IsDisposed)
            //    //{
            //    //    xbezig.Invoke(new Action(() => xbezig.Text = "Overzicht Aangemaakt!"));
            //    //    await Task.Delay(2000);
            //    //    xbezig.Invoke(new Action(() => xbezig.Visible = false));
            //    //}
            //});
        }

        public bool IsRunning(ProgressArg arg)
        {
            if (arg?.Message != null)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    xbezig.Text = arg.Message;
                    xbezig.Invalidate();
                }));
            }

            return _isbusy;
        }

        private void StopWait()
        {
            _isbusy = false;
            if (!this.IsDisposed)
            {
                xOpslaan.Text = "Opslaan";
                xOpslaan.Image = Resources.diskette_save_saveas_1514;
                xbezig.Text = "";
            }
        }

        public bool IsRunning()
        {
            return _isbusy;
        }
    }
}
