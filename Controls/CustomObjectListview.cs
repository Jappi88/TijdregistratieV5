using BrightIdeasSoftware;
using Rpm.Misc;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Controls
{
    public partial class CustomObjectListview : ObjectListView
    {
        internal bool _isLoading;

        public bool IsLoading => _isLoading;
        public bool AllowCellEdit { get; set; }

        public CustomObjectListview()
        {
            InitializeComponent();
            this.Scrollable = true;
            xloadinglabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            xloadinglabel.Size = this.Size;
            this.Controls.Add(xloadinglabel);
            xloadinglabel.BringToFront();
            CellEditStarting += CustomObjectListview_CellEditStarting;
            CellClick += DoCellClick;
        }

        private void UpdateSelectedEditControl(Control tb, int cellindex)
        {
            var xsel = SelectedItem;
            if (xsel == null) return;
            Rectangle  subrec = CalculateCellTextBounds(xsel, cellindex);
            var xloc = tb.Location;
            var xmaxwidth = tb.Width;
            if (xmaxwidth < 250)
                xmaxwidth = 250;
            var xsize = tb.Text.MeasureString(tb.Font, new Size(xmaxwidth, 100));
            var width = xsize.Width + 10;
            var height = xsize.Height;
            if (width < 100)
                width = 100;
            if (height < 20)
                height = 20;
            height += 5;
            if (View is not View.Details and not View.List)
            {
                var Y = subrec.Y;
                if (View == View.LargeIcon)
                {
                    Y = subrec.Height;
                }
                else if (View == View.Tile)
                    Y += 12;

                xloc = new Point((subrec.Location.X + subrec.Width / 2) - (xsize.Width / 2), Y);
            }

            tb.Bounds = new Rectangle(xloc, new Size(width, height));
        }

        private void CustomObjectListview_CellEditStarting(object sender, CellEditEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                tb.AutoCompleteMode = AutoCompleteMode.Suggest;
                var size = tb.Text.MeasureString(this.Font);
                if (size.Height < 20)
                    size.Height = 20;
                if (size.Width < 100)
                    size.Width = 100;
                size.Width += 5;
                size.Height += 5;
                tb.Bounds = new Rectangle(tb.Bounds.X, tb.Bounds.Y, size.Width, size.Height);
                tb.Multiline = true;
               
                var format = e.Column.AspectToStringFormat;
                if (!string.IsNullOrEmpty(format))
                {
                    var xindex = format.LastIndexOf('}');
                    if (xindex > -1)
                    {
                        xindex++;
                        format = format.Substring(xindex, format.Length - xindex);
                    }

                    tb.Text = tb.Text.Replace(format, "");
                }
            }
        }

        public override void EditModel(object rowModel)
        {
            if (!AllowCellEdit) return;
            base.EditModel(rowModel);
            if (CellEditor != null)
            {
                CellEditor.Tag = rowModel;
                CellEditor.TextChanged -= CellEditor_TextChanged;
                CellEditor.TextChanged += CellEditor_TextChanged;
                UpdateSelectedEditControl(CellEditor,0);
            }
        }

        public override void EditSubItem(OLVListItem item, int subItemIndex)
        {
            if (!AllowCellEdit) return;
            var col = GetColumn(subItemIndex);
            if (col is not { IsEditable: true }) return;
            base.StartCellEdit(item, subItemIndex);
            if (CellEditor != null)
            {
                CellEditor.Tag = subItemIndex;
                CellEditor.TextChanged -= CellEditor_TextChanged;
                CellEditor.TextChanged += CellEditor_TextChanged;
                UpdateSelectedEditControl(CellEditor,subItemIndex);
            }
        }

        object _lastselected = null;
        DateTime _lastclicked = DateTime.Now;

        private void DoCellClick(object sender, CellClickEventArgs e)
        {
            if (e.ClickCount > 1) return;
            var xdt = DateTime.Now;
            var xlast = (xdt - _lastclicked).TotalMilliseconds;
            if (e.Model is not null)
            {
                if (_lastselected != null && _lastselected.Equals(e.Model) && xlast is > 500 and <= 1500)
                {
                    EditSubItem(e.Item, e.ColumnIndex);
                }
            }

            _lastselected = e.Model;
            _lastclicked = DateTime.Now;
        }

        private void CellEditor_TextChanged(object sender, EventArgs e)
        {
            if (sender is Control tb)
            {
                var index = 0;
                if (tb.Tag is int val)
                    index = val;
                UpdateSelectedEditControl(tb,index);
            }
        }

        private string _loadingvalue;
        public void StartWaitUI(string value, double timeout = -1)
        {
            _loadingvalue = value;
            if (_isLoading) return;
            _isLoading = true;
            Task.Run(() =>
            {
                try
                {
                    if (Disposing || IsDisposed) return;
                    this.Invoke(new MethodInvoker(() =>
                    {
                        xloadinglabel.Visible = true;
                    }));

                    var cur = 0;
                    //var xcurvalue = xwv;
                    double tries = 0;
                    try
                    {
                        if (timeout == -1)
                            timeout = 60000;
                        while (_isLoading && tries < timeout)
                        {
                            if (cur > 5) cur = 0;
                            if (Disposing || IsDisposed) return;
                            var curvalue = _loadingvalue.PadRight(_loadingvalue.Length + cur, '.');
                            //xcurvalue = curvalue;
                            this.Invoke(new MethodInvoker(() =>
                            {
                                xloadinglabel.Text = curvalue;
                                xloadinglabel.Invalidate();
                            }));
                            //Application.DoEvents();

                            Thread.Sleep(250);
                            //Application.DoEvents();
                            tries+=250;
                            cur++;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    if (Disposing || IsDisposed) return;
                    this.Invoke(new MethodInvoker(() =>
                    {
                        xloadinglabel.Visible = false;
                    }));
                }
                catch (Exception e)
                {
                }

                _isLoading = false;
            });
        }

        /// <summary>
        ///     verberg het laad scherm
        /// </summary>
        public void StopWait()
        {
            _isLoading = false;
        }
    }
}
