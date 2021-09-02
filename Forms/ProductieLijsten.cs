using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Various;
using WeifenLuo.WinFormsUI.Docking;

namespace Forms
{
    public partial class ProductieLijsten : Form
    {

        public ProductieLijsten()
        {
            InitializeComponent();
            InitDock();
        }

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
            dockPanel.DefaultFloatWindowSize = this.MinimumSize;
            dockPanel.Dock = DockStyle.Fill;
            dockPanel.DockBackColor = Color.White;
            dockPanel.ShowDocumentIcon = true;
            dockPanel.Location = new Point(0, 0);
            dockPanel.Margin = new Padding(2);
            dockPanel.Name = "dockPanel";
            dockPanel.Size = this.Size;
            dockPanel.TabIndex = 4;
            dockPanel.ContentAdded += new EventHandler<DockContentEventArgs>(dockPanel1_ContentAdded);
            dockPanel.ContentRemoved += new EventHandler<DockContentEventArgs>(dockPanel1_ContentRemoved);
            return dockPanel;
        }


        public DockPanel DockPanel { get; private set; }

        public string Catagory { get; set; }
        public bool IsReordered { get; set; }

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
            {
                //dockPanel.SaveAsXml(Manager.DbPath + "\\dockstyleinfo.xml");
                UpdateStatusText(dockPanel);
            }
            //else ReArangeDocks();
        }

        private void UpdateStatusText(DockPanel dockPanel)
        {
            try
            {
                var count = dockPanel.Contents.Count;
                var xv = count == 1 ? "Productielijst" : "Productielijsten";
                this.Text = $"{count} {xv} Geopend";
                this.Invalidate();

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
            UpdateStatusText(dockPanel);
            this.BringToFront();
            this.Focus();
            this.Select();
        }

        private void Producties_Shown(object sender, EventArgs e)
        {
            //BringToFront();
            //Focus();
        }

        private void Producties_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SetLastInfo();
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
            List<DockPane> panes = new List<DockPane>();
            DockPane pane = null;
            int curindex = 0;
            for (int i = 0; i < DockPanel.Contents.Count; i++)
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
    }
}