using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;
using MetroFramework.Controls;
using ProductieManager.Rpm.SqlLite;
using Rpm.Various;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Controls
{
    public partial class ProductieInfoUI : UserControl
    {
        public IProductieBase Productie { get; private set; }
        public string Title { get; private set; }
        public Color HtmlBackColor { get; private set; }
        public Color BackColorGradient { get; private set; }
        public Color TextColor { get; private set; }

        public bool ShowAantal
        {
            get => aantalChangerUI1.Visible;
            set => aantalChangerUI1.Visible = value;
        }

        public bool AllowVerpakkingEdit
        {
            get => verpakkingInstructieUI1.AllowEditMode;
            set => verpakkingInstructieUI1.AllowEditMode = value;
        }

        public MetroTabControl TabControl
        {
            get => xTabControl;
            set => xTabControl = value;
        }

        public ProductieInfoUI()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.infolog);
            productieVerbruikUI1.ShowMateriaalSelector = true;
            productieVerbruikUI1.ShowOpslaan = true;
            xTabControl.SelectedIndex = 0;
            ((OLVColumn) xLogDataList.Columns[0]).ImageGetter = (x) => 0;
            //webBrowser1.Navigated += webBrowser1_Navigated;
        }

        private void UpdateLogData(List<LogEntry> entries)
        {
            try
            {
                var xcurents = xLogDataList.Objects?.Cast<LogEntry>().ToList() ?? new List<LogEntry>();
                var xremove = xcurents.Where(xc => entries.All(e => e.Id != xc.Id)).ToList();
                bool selected = xremove.Count > 0;
                if (xremove.Count > 0)
                {
                    xLogDataList.BeginUpdate();
                    xremove.ForEach(x =>
                    {
                        xcurents.Remove(x);
                        xLogDataList.RemoveObject(x);
                    });
                    xLogDataList.EndUpdate();
                }


                for (int i = 0; i < entries.Count; i++)
                {
                    var xent = entries[i];
                    var xold = xcurents.FirstOrDefault(x => x.Id == xent.Id);
                    if (xold != null)
                    {
                        xLogDataList.RefreshObject(xold);
                    }
                    else
                    {
                        selected = true;
                        xLogDataList.BeginUpdate();
                        xLogDataList.AddObject(xent);
                        xLogDataList.EndUpdate();

                    }
                }

                if (selected)
                {
                    xLogDataList.SelectedIndex = xLogDataList.Items.Count - 1;
                    xLogDataList.SelectedItem?.EnsureVisible();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateView()
        {
            if (Productie == null) return;
            try
            {
                if (ShowAantal)
                {
                    aantalChangerUI1.Visible = true;
                    aantalChangerUI1.LoadAantalGemaakt(Productie);
                }

                string txt = String.Empty;
                int index = -1;
                if (Productie is Bewerking bew && bew.Combies.Count > 0)
                {
                    xTabControl.TabPages[2].Text = $"Combinaties[{bew.Combies.Count}]";
                }
                else xTabControl.TabPages[2].Text = "Combineren";

                switch (xTabControl.SelectedIndex)
                {
                    case 0:
                        //Header Html
                        index = 0;
                        txt = Productie.GetHeaderHtmlBody(Title,
                            Productie.GetImageFromResources(),
                            new Size(64, 64), BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 1:
                        productieVerbruikUI1.InitFields((Productie as Bewerking)?.Parent);
                        break;
                    case 2:
                        combineerUI1.UpdateBewerking(Productie as Bewerking);
                        break;
                    case 3:
                        //ProductieInfo
                        index = 3;
                        txt = Productie.GetProductieInfoHtml("Productie Info",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 4:
                        //Notities
                        index = 4;
                        txt = Productie.GetNotitiesHtml("Notities",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 5:
                        //ProductieDatums
                        index = 5;
                        txt = Productie.GetDatumsHtml("Productie Datums",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 6:
                        verpakkingInstructieUI1.AllowEditMode = true;
                        if (!verpakkingInstructieUI1.IsEditmode)
                            verpakkingInstructieUI1.InitFields(Productie.VerpakkingsInstructies,
                                verpakkingInstructieUI1.IsEditmode,
                                "VerpakkingsInstructies", Color.White, Color.Black, Productie);
                        else verpakkingInstructieUI1.Productie = Productie;
                        break;
                    case 7:
                        //Materialen
                        index = 7;
                        txt = Productie.GetMaterialenHtml("Materialen",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 8:
                        //WerkPlaatsen
                        index = 8;
                        txt = Productie.GetWerkplekkenHtml("Werk Plaatsen",
                            BackColor, BackColorGradient, TextColor, true);
                        break;
                    case 9:
                        //Aantal Geschiedenis
                        alleWerkPlekAantalHistoryUI1.UpdateBewerking(Productie as Bewerking);
                        break;
                    case 10:
                        //Productie Logs
                        if (Productie is Bewerking {Parent: IDbLogging entry})
                        {
                            var xlogs = entry?.Logs ?? new List<LogEntry>();
                            UpdateLogData(xlogs);
                        }

                        break;
                    case 11:
                        string xpath = Path.Combine(Manager.DbPath, "Bijlages", Productie.ArtikelNr);
                        bijlageUI1.SetPath(xpath);
                        break;
                }

                if (index > -1)
                {
                    var xpanel = xTabControl.TabPages[index].Controls.Find($"htmlpanel_{index}", false)
                        .FirstOrDefault() as HtmlPanel;
                    bool xinit = xpanel == null;
                    xpanel ??= new HtmlPanel()
                    {
                        IsContextMenuEnabled = false,
                        IsSelectionEnabled = false,
                        Name = $"htmlpanel_{index}"
                    };
                    if (xinit)
                    {
                        xpanel.ImageLoad += xVerpakkingHtmlPanel_ImageLoad;
                    }

                    xpanel.Dock = DockStyle.Fill;
                    if (!string.Equals(xpanel.Text, txt, StringComparison.CurrentCultureIgnoreCase))
                    {
                        xTabControl.TabPages[index].SuspendLayout();
                        xTabControl.TabPages[index].Controls.Remove(xpanel);
                        var curpos = xpanel.VerticalScroll.Value;
                        xpanel.Text = txt;
                        if (curpos > 0)
                        {
                            for (var i = 0; i < 5; i++)
                            {
                                xpanel.VerticalScroll.Value = curpos;
                            }
                        }

                        xTabControl.TabPages[index].Controls.Add(xpanel);
                        xTabControl.TabPages[index].ResumeLayout(true);
                        //panel.AutoScrollPosition.Offset(curpos);
                        //panel.Invalidate();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CopyControl(Control sourceControl, Control targetControl)
        {
            // make sure these are the same
            if (sourceControl.GetType() != targetControl.GetType())
            {
                throw new Exception("Incorrect control types");
            }

            foreach (PropertyInfo sourceProperty in sourceControl.GetType().GetProperties())
            {
                object newValue = sourceProperty.GetValue(sourceControl, null);

                MethodInfo mi = sourceProperty.GetSetMethod(true);
                if (mi != null)
                {
                    sourceProperty.SetValue(targetControl, newValue, null);
                }
            }
        }

        public void SetInfo(IProductieBase productie, string title, Color backColor, Color backGroundGradient,
            Color textColor)
        {
            if (productie == null) return;
            try
            {
                Productie = productie;
                Title = title;
                HtmlBackColor = backColor;
                BackColorGradient = backGroundGradient;
                TextColor = textColor;
                if (InvokeRequired)
                    this.BeginInvoke(new MethodInvoker(UpdateView));
                else
                    UpdateView();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



        private void xVerpakkingHtmlPanel_ImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            var xkey = e.Src;
            switch (xkey.ToLower().Trim())
            {
                case "verpakkingicon":
                    e.Callback(Resources.package_box_128x128);
                    break;
                case "productieinfoicon":
                    e.Callback(Resources.microsoft_info_22732);
                    break;
                case "datumsicon":
                    e.Callback(Resources.systemtime_778);
                    break;
                case "materialenicon":
                    e.Callback(Resources.bolts_construction_rivet_screw_screws_128x128);
                    break;
                case "notitiesicon":
                    e.Callback(Resources.memo_pad_notes_reminder_task_icon_128x128);
                    break;
                case "werkplaatsenicon":
                    e.Callback(Resources.iconfinder_technology);
                    break;
            }
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private async void xLogDataList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && xLogDataList.SelectedObjects.Count > 0)
            {
                if (Manager.LogedInGebruiker == null || Manager.LogedInGebruiker.AccesLevel < AccesType.Manager)
                    return;
                var xents = xLogDataList.SelectedObjects.Cast<LogEntry>().ToList();

                var xprod = (Productie as Bewerking)?.Parent ?? (Productie as ProductieFormulier);
                if (xprod != null)
                {
                    var xlist = xprod.Logs ?? new List<LogEntry>();
                    xents.ForEach(x => { xlist.Remove(x); });
                    if (Manager.Database?.ProductieFormulieren != null)
                    {
                        await Manager.Database.UpSert(xprod, false, false, "");
                    }
                }

            }
        }

        //private delegate int EnumChildProc(IntPtr hwnd, IntPtr lParam);

        //[DllImport("user32.dll", SetLastError = true)]
        //private static extern IntPtr SendMessage(IntPtr hWnd, int Msg,
        //    IntPtr wParam, IntPtr lParam);

        //[DllImport("user32.dll", SetLastError = true)]
        //private static extern int EnumChildWindows(IntPtr hWndParent,
        //    EnumChildProc lpEnumFunc, IntPtr lParam);

        //[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        //private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName,
        //    int nMaxCount);

        //private const int WM_COMMAND = 0x108E;
        //private const int SHVIEW_REPORT = 0x702C;
        //private const string SHELLVIEW_CLASS = "SHELLDLL_DefView";

        //private IntPtr m_ShellView;

        //private int EnumChildren(IntPtr hwnd, IntPtr lParam)
        //{
        //    int retval = 1;

        //    StringBuilder sb = new StringBuilder(SHELLVIEW_CLASS.Length -1);
        //    int numChars = GetClassName(hwnd, sb, sb.Capacity);
        //    if (numChars == SHELLVIEW_CLASS.Length)
        //    {
        //        if (sb.ToString(0, numChars) == SHELLVIEW_CLASS)
        //        {
        //            m_ShellView = hwnd;
        //            retval = 0;
        //        }
        //    }

        //    return retval;
        //}

        //#region WebrowserViewStyle

        //private const int LV_VIEW_ICON = 0x0;

        //private const int LV_VIEW_DETAILS = 0x1;

        //private const int LV_VIEW_SMALLICON = 0x2;

        //private const int LV_VIEW_LIST = 0x3;

        //private const int LV_VIEW_TILE = 0x4;

        //private const int EM_HIDEBALLOONTIP = 0x1504;

        //private const int LVM_SETVIEW = 0x108E;

        //private const string ListViewClassName = "SHELLDLL_DefView";

        //private readonly System.Runtime.InteropServices.HandleRef NullHandleRef =
        //    new System.Runtime.InteropServices.HandleRef(null, System.IntPtr.Zero);



        //[DllImport("user32.dll", ExactSpelling = true)]

        //private static extern bool EnumChildWindows(HandleRef hwndParent,
        //    EnumChildrenCallback lpEnumFunc, HandleRef lParam);

        //[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        //static extern int SendMessage(HandleRef hWnd, uint Msg, int wParam, int lParam);


        //[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]

        //private static extern uint RealGetWindowClass(IntPtr hwnd, [Out] StringBuilder pszType, uint cchType);


        //private delegate bool EnumChildrenCallback(IntPtr hwnd, IntPtr lParam);

        //private HandleRef listViewHandle;
        //private void FindListViewHandle()
        //{
        //    this.listViewHandle = NullHandleRef;
        //    var lpEnumFunc = new EnumChildrenCallback(EnumChildren);

        //    EnumChildWindows(new HandleRef(this.webBrowser1, this.webBrowser1.Handle),
        //        lpEnumFunc, NullHandleRef);
        //}




        //private bool EnumChildren(IntPtr hwnd, IntPtr lparam)
        //{
        //    var sb = new StringBuilder(100);

        //    RealGetWindowClass(hwnd, sb, 100);

        //    if (sb.ToString() == ListViewClassName)
        //        this.listViewHandle = new System.Runtime.InteropServices.HandleRef(null, hwnd);
        //    return true;

        //}

        //#endregion

        //private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        //{
        //    FindListViewHandle();
        //    var view = 2;

        //    SendMessage(this.listViewHandle, LVM_SETVIEW, view, 0);
        //}
    }
}
