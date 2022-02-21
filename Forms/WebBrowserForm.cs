using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms;
using HtmlAgilityPack;
using ProductieManager.Rpm.Connection;
using ProductieManager.Rpm.ExcelHelper;
using Rpm.Various;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ProductieManager.Forms
{
    public partial class WebBrowserForm : Form
    {
        private bool _hasFound;

        protected string NavigatingArtNr;

        public WebBrowserForm()
        {
            InitializeComponent();
        }

        public string[] FilesFormatToOpen { get; set; }
        public List<string> FilesToOpen { get; set; } = new();
        public bool CloseIfNotFound { get; set; }
        public bool OpenIfFound { get; set; }
        public List<string> FilesNotFound { get; } = new();
        public bool ShowNotFoundList { get; set; }
        public ProgressArg Arg { get; set; } = new();
        public bool ShowErrorMessage { get; set; } = true;
        public bool StopNavigatingAfterError { get; set; } = true;

        public WebBrowser Browser { get; private set; }

        private void InitNewBrowser()
        {
            try
            {
                SuspendLayout();
                //Browser?.Stop();
                Controls.Clear();
                Browser = new WebBrowser();
                Browser.ScriptErrorsSuppressed = true;
                Browser.IsWebBrowserContextMenuEnabled = false;
                // Browser.AllowWebBrowserDrop = false;
                Browser.Navigated += xBrowser_Navigated;
                Browser.Dock = DockStyle.Fill;
                Controls.Add(Browser);
                ResumeLayout(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Navigate(string artnr)
        {
            Arg.Message = $"Navigeren naar {artnr}...";
            Arg.Type = ProgressType.ReadBussy;
            Arg.OnChanged(this);
            if (Browser == null)
            {
                if (InvokeRequired)
                    Invoke(new MethodInvoker(InitNewBrowser));
                else
                    InitNewBrowser();
            }

            Browser.Document?.OpenNew(true);
            NavigatingArtNr = artnr;
            var xlink = AutoDeskHelper.GetTekeningPdfLink(artnr);
            if (InvokeRequired)
                Invoke(new MethodInvoker(() => Browser?.Navigate(xlink, false)));
            else
                Browser?.Navigate(xlink, false);
        }

        public void Navigate()
        {
            var xfirst = FilesToOpen?.FirstOrDefault();
            if (xfirst == null) return;
            Navigate(xfirst);
        }


        private HtmlNodeCollection GetNodeCollection(ref bool valid)
        {
            try
            {
                if (Disposing || IsDisposed) return null;
                if (Arg.IsCanceled) throw new Exception();
                if (string.Equals(Browser.DocumentTitle, "Navigatie is geannuleerd",
                        StringComparison.CurrentCultureIgnoreCase) || Browser.DocumentTitle.Contains("niet bereikbaar"))
                {
                    valid = false;
                    return null;
                }

                var html = Browser.DocumentText;
                var xdoc = new HtmlDocument();
                xdoc.LoadHtml(html);
                var xelements =
                    xdoc.DocumentNode.SelectNodes(
                        "/html[1]/body[1]/div[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[1]/div[1]/table[1]/tbody[1]");
                return xelements;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                valid = false;
                return null;
            }
        }

        private async void xBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (_hasFound || FilesToOpen == null || FilesToOpen.Count == 0 ||
                FilesFormatToOpen == null || FilesFormatToOpen.Length == 0) return;
            if (Disposing || IsDisposed) return;
            Arg.Type = ProgressType.ReadBussy;
            Arg.Current++;
            Arg.OnChanged(this);
            if (Arg.IsCanceled)
            {
                Close();
                return;
            }

            HtmlNodeCollection xelements = null;
            var isvalid = true;
            await Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 40; i++)
                {
                    Arg.Message = $"Gegevens laden van {NavigatingArtNr}...";
                    Arg.OnChanged(this);
                    if (Arg.IsCanceled) break;
                    if (InvokeRequired)
                        Invoke(new MethodInvoker(() =>
                        {
                            var xvalid = true;
                            xelements = GetNodeCollection(ref xvalid);
                            isvalid = xvalid;
                        }));
                    else xelements = GetNodeCollection(ref isvalid);

                    if (xelements != null || !isvalid) break;
                    Thread.Sleep(250);
                }
            });
            Arg.OnChanged(this);
            if (Arg.IsCanceled)
            {
                Close();
                return;
            }

            try
            {
                FilesToOpen.Remove(NavigatingArtNr);
                var xnext = FilesToOpen.FirstOrDefault();
                var xflag = (StopNavigatingAfterError || xnext == null) && !isvalid;
                if (xflag)
                    throw new Exception(
                        "Verbinding kan niet tot stand worden gebracht.\n\nControlleer je netwerkinstellingen voor of je toegang hebt tot de server.");


                if (!isvalid || xelements == null)
                {
                    if (!string.IsNullOrEmpty(xnext))
                        Navigate(xnext);
                    return;
                }

                var xdefname = "";
                foreach (var xf in FilesFormatToOpen)
                {
                    var xtitle = string.Format(xf, NavigatingArtNr);
                    Arg.Message = $"laden van {xtitle} ...";
                    Arg.OnChanged(this);
                    if (Arg.IsCanceled)
                    {
                        Close();
                        return;
                    }

                    var xfiles = xelements.FirstOrDefault()?.ChildNodes.FirstOrDefault(x =>
                        string.Equals(x.GetAttributeValue("data-name", xdefname), xtitle,
                            StringComparison.CurrentCultureIgnoreCase));
                    if (xfiles == null)
                    {
                        if (FilesNotFound.IndexOf(NavigatingArtNr) < 0)
                            FilesNotFound.Add(NavigatingArtNr);
                        if (CloseIfNotFound)
                        {
                            XMessageBox.Show(this, $"Geen 'FBR' tekening gevonden voor {NavigatingArtNr}",
                                "Geen Tekening Gevonden!", MessageBoxIcon.Exclamation);
                            if (InvokeRequired)
                                Invoke(new MethodInvoker(Close));
                            else Close();
                            return;
                        }
                    }
                    else if (OpenIfFound)
                    {
                        var xid = "";
                        var id = xfiles.GetAttributeValue("data-id", xid);
                        if (!string.IsNullOrEmpty(id))
                        {
                            _hasFound = true;
                            var xbbtn = FindElement("a", new[] {id});
                            xbbtn?.InvokeMember("click", id);
                            return;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(xnext))
                    Navigate(xnext);
                else Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //Arg.IsCanceled = true;
                Arg.Current = 0;
                Arg.Message = exception.Message;
                Arg.Type = ProgressType.ReadCompleet;
                Arg.OnChanged(this);
                if (ShowErrorMessage)
                {
                    XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Warning);
                    Close();
                }
                //this.Close();
            }
        }

        private void ShowNotFountList()
        {
            if (FilesNotFound is {Count: > 0})
            {
                var xtmpfile = Path.Combine(Path.GetTempPath(), "Ont_FBR_tekeningen.xlsx");
                try
                {
                    var xfp = ExcelWorkbook.CreateArtikelNr(FilesNotFound.ToArray(), xtmpfile, "Ontbr. FBR");

                    if (!string.IsNullOrEmpty(xfp))
                        Process.Start(xfp);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private HtmlElement FindElement(string tagname, string[] criterias)
        {
            var xelements = Browser.Document?.GetElementsByTagName(tagname);
            if (xelements == null || xelements.Count == 0) return null;
            var element = xelements.Cast<HtmlElement>()
                .FirstOrDefault(x => criterias.Any(c =>
                    x.OuterHtml.ToLower().Contains(c.ToLower()) && x.OuterHtml.ToLower().Contains("file_link")));
            return element;
        }


        private void WebBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Arg != null)
            {
                Arg.Current = 0;
                Arg.IsCanceled = true;
                Arg.OnChanged(this);
            }

            if (ShowNotFoundList) ShowNotFountList();
            if (Browser is {Disposing: false, IsDisposed: false})
                Browser.Dispose();
            if (!Disposing && !IsDisposed)
                Dispose();
        }
    }
}