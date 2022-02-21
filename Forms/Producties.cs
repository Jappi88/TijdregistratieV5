using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Rpm.Productie;
using Various;
using WeifenLuo.WinFormsUI.Docking;

namespace Forms
{
    public partial class Producties : Form
    {
        private List<IDockContent> _producties = new();
        private int lastindex;

        public Producties()
        {
            InitializeComponent();
            InitDock();
        }


        public DockPanel DockPanel { get; private set; }

        public string Catagory { get; set; }
        public bool IsReordered { get; set; }

        public void InitDock()
        {
            SuspendLayout();
            DockPanel = CreateDockPanel();
            Controls.Add(DockPanel);
            ResumeLayout(false);
        }

        private DockPanel CreateDockPanel()
        {
            var dockPanel = new DockPanel();
            // 
            // dockPanel1
            // 
            dockPanel.BackColor = Color.White;
            dockPanel.DefaultFloatWindowSize = MinimumSize;
            dockPanel.Dock = DockStyle.Fill;
            dockPanel.DockBackColor = Color.White;
            dockPanel.ShowDocumentIcon = true;
            dockPanel.Location = new Point(0, 0);
            dockPanel.Margin = new Padding(2);
            dockPanel.Name = "dockPanel";
            dockPanel.Size = Size;
            dockPanel.TabIndex = 4;
            dockPanel.ContentAdded += dockPanel1_ContentAdded;
            dockPanel.ContentRemoved += dockPanel1_ContentRemoved;
            return dockPanel;
        }

        public bool LoadXml(DockPanel dockPanel)
        {
            if (File.Exists(Manager.DbPath + "\\dockstyleinfo.xml"))
            {
                try
                {
                    dockPanel.LoadFromXml(Manager.DbPath + "\\dockstyleinfo.xml", GetPersistDockContent);
                }
                catch (Exception e)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        private void dockPanel1_ContentRemoved(object sender, DockContentEventArgs e)
        {
            var dockPanel = sender as DockPanel;
            if (dockPanel == null) return;
            if (dockPanel.Contents.Count == 0)
                try
                {
                    if (Visible) Close();
                }
                catch
                {
                }
            else
                //dockPanel.SaveAsXml(Manager.DbPath + "\\dockstyleinfo.xml");
                UpdateStatusText(dockPanel);
            //else ReArangeDocks();
        }

        private void UpdateStatusText(DockPanel dockPanel)
        {
            //this.BeginInvoke(new Action(() =>
            //{
            //    try
            //    {
            //        var count = dockPanel.Contents.Count;
            //        var xv = count == 1 ? "Productie" : "Producties";
            //        this.Text = $"{count} {xv} Geopend";
            //        this.Invalidate();

            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //    }
            //}));
            try
            {
                var count = dockPanel.Contents.Count;
                var xv = count == 1 ? "Productie" : "Producties";
                Text = $"{count} {xv} Geopend";
                Invalidate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void dockPanel1_ContentAdded(object sender, DockContentEventArgs e)
        {
            var dockPanel = sender as DockPanel;
            if (dockPanel == null) return;
            _producties.Add(e.Content);
            UpdateStatusText(dockPanel);
            BringToFront();
            Focus();
            Select();
        }

        private void SaveLastState(DockPanel dockPanel)
        {
            for (var i = 0; i < dockPanel.DockWindows.Count; i++)
            {
                var dw = dockPanel.DockWindows[i];
            }
        }

        public IDockContent GetPersistDockContent(string persist)
        {
            if (_producties == null || _producties.Count == 0)
                return new DockContent();
            if (lastindex >= _producties.Count) lastindex = 0;
            return _producties[lastindex++];
        }

        public void LoadProducties(List<StartProductie> producties)
        {
            _producties = producties.Cast<IDockContent>().ToList();
            InitDock();
        }


        //public void ReArangeDocks(DockPanel dockPanel)
        //{
        //    try
        //    {
        //        if (IsDisposed || dockPanel?.Contents == null || dockPanel.Contents.Count == 0) return;
        //        int xcur = 1;
        //        foreach (var dock in dockPanel.Contents)
        //        {
        //            var state = GetDockState(xcur++);
        //            dock.DockHandler.Form.Width = this.MinimumSize.Width;
        //            dock.DockHandler.DockState = state;
        //        }

        //        if (dockPanel.Contents.Count > 4)
        //            this.WindowState = FormWindowState.Maximized;
        //        else
        //            this.Size = GetFormSize(dockPanel.Contents.Count);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        //private DockState GetDockState(int count)
        //{
        //    switch (count)
        //    {
        //        case 0:
        //        case 1:
        //        case 2:
        //        case 5:
        //            return DockState.Document;
        //        case 3:
        //        case 4:
        //        case 6:
        //            return DockState.DockBottom;
        //        default: return DockState.Float;
        //    }
        //}

        private Size GetFormSize(int forms)
        {
            var row = forms > 2 ? 2 : 1;
            var colmns = forms == 3 ? 2 : forms < 3 ? forms : forms > 4 ? 3 : 2;

            var width = MinimumSize.Width * colmns;
            var height = MinimumSize.Height * row;
            return new Size(width, height);
        }

        private DockStyle GetDockStyle(int index)
        {
            switch (index)
            {
                case 0:
                    return DockStyle.Top;
                case 1:
                    return DockStyle.Right;
                case 2:
                    return DockStyle.Bottom;
                case 3:
                    return DockStyle.Left;
                default:
                    return DockStyle.Fill;
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            var panes = new List<DockPane>();
            DockPane pane = null;
            var curindex = 0;
            for (var i = 0; i < DockPanel.Contents.Count; i++)
            {
                var content = DockPanel.Contents[i];
                if (pane == null || pane.Contents.Count > 3)
                {
                    pane = DockPanel.Theme.Extender.DockPaneFactory.CreateDockPane(content, DockState.Document, false);
                    panes.Add(pane);
                    curindex = 0;
                }

                content.DockHandler.PanelPane = pane;
                pane.DockTo(DockPanel, GetDockStyle(curindex));
                curindex++;
                //content.DockHandler.DockTo(pane, DockStyle.Right, i);
            }
        }

        private void Producties_Load(object sender, EventArgs e)
        {
            this.InitLastInfo();
        }

        private void Producties_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SetLastInfo();
        }
    }
}