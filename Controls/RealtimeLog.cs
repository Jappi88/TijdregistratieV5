using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Controls
{
    public partial class RealtimeLog : UserControl
    {
        public RealtimeLog()
        {
            InitializeComponent();
            xstartdate.Value = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            LogSyncer = new BackgroundWorker();
            LogSyncer.WorkerSupportsCancellation = true;
            LogSyncer.DoWork += LogSyncer_DoWork;
            LogSyncer.WorkerReportsProgress = true;
            LogSyncer.ProgressChanged += LogSyncer_ProgressChanged;
            imageList1.Images.Add(Resources.infolog);
            imageList1.Images.Add(Resources.succeslog);
            imageList1.Images.Add(Resources.exclamation_warning_15590);
            imageList1.Images.Add(Resources.errorlog);
            ((OLVColumn) xlogview.Columns[0]).ImageGetter = ImageGetter;
            xlogview.RowFormatter = RowFormatter;
        }

        public List<LogEntry> Logs { get; private set; }
        public BackgroundWorker LogSyncer { get; }
        public DateTime LastRead { get; private set; }
        public DateTime ReadTo { get; private set; } = DateTime.MaxValue;
        public int ReadInterval { get; set; } = 1500;

        public bool IsCloseAble
        {
            get => xclosepanel.Visible;
            set => xclosepanel.Visible = value;
        }

        private void RowFormatter(OLVListItem olvitem)
        {
            if (olvitem.RowObject is LogEntry st)
                switch (st.Type)
                {
                    case MsgType.Success:
                        olvitem.BackColor = Color.Green;
                        break;
                    case MsgType.Waarschuwing:
                        olvitem.BackColor = Color.Orange;
                        break;
                    case MsgType.Fout:
                        olvitem.BackColor = Color.Red;
                        break;
                    default:
                        olvitem.BackColor = Color.AliceBlue;
                        break;
                }
        }

        public object ImageGetter(object sender)
        {
            if (sender is LogEntry st)
                switch (st.Type)
                {
                    case MsgType.Success:
                        return 1;
                    case MsgType.Waarschuwing:
                        return 2;
                    case MsgType.Fout:
                        return 3;
                    default:
                        return 0;
                }

            return 0;
        }

        private void LogSyncer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is LogEntry entry)
            {
                Logs.Add(entry);
                xlogview.AddObject(entry);
                xlogview.SelectedObject = entry;
                xlogview.SelectedItem?.EnsureVisible();
            }
            else if (e.UserState is List<LogEntry> {Count: > 0} entries)
            {
                Logs.AddRange(entries);
                xlogview.BeginUpdate();
                xlogview.AddObjects(entries);
                xlogview.SelectedObject = entries[entries.Count - 1];
                xlogview.SelectedItem?.EnsureVisible();
                xlogview.EndUpdate();
            }

            LastRead = DateTime.Now;
            UpdateHeaderLabel();
        }

        private void LogSyncer_DoWork(object sender, DoWorkEventArgs e)
        {
            DoWork().Wait();
        }

        private Task DoWork()
        {
            return Task.Run(async () =>
            {
                while (!IsDisposed && Manager.Database != null && LogSyncer is {CancellationPending: false})
                {
                    try
                    {
                        var items = await Manager.Database.GetLogs(LastRead, ReadTo);
                        if (items != null && items.Any())
                            LogSyncer.ReportProgress(0, items);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    await Task.Delay(ReadInterval);
                    //Thread.Sleep(ReadInterval);
                }
            });
        }

        public void Start()
        {
            if (LogSyncer is {IsBusy: false})
            {
                Logs = new List<LogEntry>();
                LogSyncer.RunWorkerAsync();
            }
        }

        public void Stop()
        {
            LogSyncer.CancelAsync();
        }

        public void ListItems()
        {
            if (Logs != null)
                xlogview.SetObjects(Logs.Where(IsAllowed));
            else
                xlogview.SetObjects(new LogEntry[] { });
        }

        public bool IsAllowed(LogEntry entry)
        {
            var filter = xsearchbox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(filter) || filter == "zoeken...")
                return true;
            var xreturn = false;
            if (entry.MachineId != null)
                xreturn |= entry.MachineId.ToLower().Contains(filter);
            if (entry.Username != null)
                xreturn |= entry.Username.ToLower().Contains(filter);
            if (entry.Message != null)
                xreturn |= entry.Message.ToLower().Contains(filter);
            return xreturn;
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim().ToLower() != "zoeken...")
                ListItems();
        }

        private void xskillview_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatusLabel();
        }

        private void UpdateHeaderLabel()
        {
            var xline = $"vanaf {xstartdate.Value}";
            if (xenddate.Checked)
                xline += $" tot {ReadTo}";
            xline += ".";
            if (Logs != null)
            {
                if (Logs.Any())
                {
                    var xarg = Logs.Count == 1 ? "Log" : "Logs";
                    var title = $"Totaal {Logs.Count} {xarg} {xline}";
                    xomschrijving.Text = title;
                }
                else
                {
                    xomschrijving.Text = $"Geen logs beschikbaar {xline}";
                }
            }
            else
            {
                xomschrijving.Text = $"Geen logs beschikbaar {xline}";
            }
        }

        private void UpdateStatusLabel()
        {
            if (xlogview.SelectedObjects.Count > 0)
            {
                var selected = xlogview.SelectedObjects.Cast<LogEntry>().ToArray();
                if (selected.Length == 1)
                {
                    var sel = selected[0];
                    xstatuslabel.Text = $"[{Enum.GetName(typeof(MsgType), sel.Type)}][{sel.Added}] {sel.Message}";
                }
                else
                {
                    xstatuslabel.Text =
                        $"{selected.Length} Logs geselecteerd.";
                }
            }
            else
            {
                if (xlogview.Objects != null)
                {
                    var items = xlogview.Objects.Cast<LogEntry>().ToArray();
                    var selected = items;
                    if (selected.Length == 1)
                    {
                        var sel = selected[0];
                        xstatuslabel.Text = $"[{Enum.GetName(typeof(MsgType), sel.Type)}][{sel.Added}] {sel.Message}";
                    }
                    else
                    {
                        xstatuslabel.Text =
                            $"Totaal {selected.Length} Logs";
                    }
                }
                else
                {
                    xstatuslabel.Text = "Geen logs";
                }
            }
        }

        private void xsearch_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb is {Text: "Zoeken..."}) tb.Text = "";
        }

        private void xsearch_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.Text = "Zoeken...";
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            CloseButtonPressed();
        }

        public event EventHandler OnCloseButtonPressed;

        public void CloseButtonPressed()
        {
            OnCloseButtonPressed?.Invoke(xsluiten, EventArgs.Empty);
        }

        private void xlogview_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            if (!(e.Model is LogEntry wp)) return;
            e.Title = $"[{Enum.GetName(typeof(MsgType), wp.Type)}]{wp.Username}";
            e.Text = wp.Message;
        }

        private void xclearbutton_Click(object sender, EventArgs e)
        {
            foreach (var log in Logs)
                Manager.Database.Logger.Delete(log.Id.ToString());
            Logs?.Clear();
            xlogview.SetObjects(new LogEntry[] { });
            UpdateHeaderLabel();
            UpdateStatusLabel();
        }

        private void xstartdate_ValueChanged(object sender, EventArgs e)
        {
            if (xstartdate.Checked)
            {
                if (LastRead != xstartdate.Value)
                {
                    LastRead = xstartdate.Value;
                    Logs?.Clear();
                    xlogview.SetObjects(new LogEntry[] { });
                }

                UpdateHeaderLabel();
                UpdateStatusLabel();
            }
            else if (!xstartdate.Checked)
            {
                LastRead = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                xstartdate.Value = LastRead;
                xstartdate.Checked = false;
                Logs?.Clear();
                xlogview.SetObjects(new LogEntry[] { });

                UpdateHeaderLabel();
                UpdateStatusLabel();
            }
        }

        private void xenddate_ValueChanged(object sender, EventArgs e)
        {
            if (xenddate.Checked)
            {
                if (ReadTo != xenddate.Value)
                {
                    LastRead = xstartdate.Checked ? xstartdate.Value : DateTime.Now.Subtract(TimeSpan.FromDays(1));
                    ReadTo = xenddate.Value;
                    Logs?.Clear();
                    xlogview.SetObjects(new LogEntry[] { });
                }

                UpdateHeaderLabel();
                UpdateStatusLabel();
            }
            else if (!xenddate.Checked)
            {
                ReadTo = DateTime.MaxValue;
                LastRead = xstartdate.Checked ? xstartdate.Value : DateTime.Now.Subtract(TimeSpan.FromDays(1));
                Logs?.Clear();
                xlogview.SetObjects(new LogEntry[] { });

                UpdateHeaderLabel();
                UpdateStatusLabel();
            }
        }
    }
}