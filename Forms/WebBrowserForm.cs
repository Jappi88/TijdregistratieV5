using Forms;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using ProductieManager.Rpm.Connection;
using Various;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ProductieManager.Forms
{
    public partial class WebBrowserForm : Form
    {
        public string[] FilesFormatToOpen { get; set; }
        public List<string> FilesToOpen { get; set; } = new List<string>();
        public bool CloseIfNotFound { get; set; }
        public bool OpenIfFound { get; set; }
        public List<string> FilesNotFound { get; } = new List<string>();
        public WebBrowserForm()
        {
            InitializeComponent();
        }

        private void InitNewBrowser()
        {
            this.SuspendLayout();
            Browser?.Stop();
            Browser?.Dispose();
            this.Controls.Clear();
            Browser = new WebBrowser();
            Browser.ScriptErrorsSuppressed = true;
            Browser.IsWebBrowserContextMenuEnabled = false;
            Browser.Navigated += xBrowser_Navigated;
            Browser.Dock = DockStyle.Fill;
            this.Controls.Add(Browser);
            this.ResumeLayout(true);
        }

        public WebBrowser Browser { get; private set; }

        protected string NavigatingArtNr;

        public void Navigate(string artnr)
        {
            InitNewBrowser();
            NavigatingArtNr = artnr;
            var xlink = AutoDeskHelper.GetTekeningPdfLink(artnr);
            Browser?.Navigate(xlink);
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
                if (string.Equals(Browser.DocumentTitle, "Navigatie is geannuleerd",
                        StringComparison.CurrentCultureIgnoreCase))
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

        private bool _hasFound;

        private async void xBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (_hasFound || FilesToOpen == null || FilesToOpen.Count == 0 ||
                FilesFormatToOpen == null || FilesFormatToOpen.Length == 0) return;
            if (Disposing || IsDisposed) return;
            HtmlNodeCollection xelements = null;
            bool isvalid = true;
            await Task.Factory.StartNew(new Action(() =>
            {
                for (int i = 0; i < 20; i++)
                {

                    if (this.InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(() => xelements = GetNodeCollection(ref isvalid)));
                    }
                    else xelements = GetNodeCollection(ref isvalid);

                    if (xelements != null || !isvalid) break;

                    Thread.Sleep(250);
                }
            }));

            try
            {
                if (!isvalid)
                {
                    throw new Exception(
                        "Verbinding kan niet tot stand worden gebracht.\n\nControlleer je netwerkinstellingen voor of je toegang hebt tot de server.");
                }

                FilesToOpen.Remove(NavigatingArtNr);
                var xnext = FilesToOpen.FirstOrDefault();
                if (xelements == null)
                {
                    if (!string.IsNullOrEmpty(xnext))
                        Navigate(xnext);
                    return;
                }

                string xdefname = "";
                foreach (var xf in FilesFormatToOpen)
                {
                    var xtitle = string.Format(xf, NavigatingArtNr);
                    var xfiles = xelements.FirstOrDefault()?.ChildNodes.FirstOrDefault(x => 
                        string.Equals(x.GetAttributeValue("data-name", xdefname), xtitle, StringComparison.CurrentCultureIgnoreCase));
                    if (xfiles == null)
                    {
                        if (FilesNotFound.IndexOf(NavigatingArtNr) < 0)
                            FilesNotFound.Add(NavigatingArtNr);
                        if (CloseIfNotFound)
                        {
                            XMessageBox.Show($"Geen FBR tekening gevonden voor {string.Join(", ", FilesToOpen)}",
                                "Geen Tekening Gevonden!", MessageBoxIcon.Exclamation);
                            if (this.InvokeRequired)
                                this.Invoke(new MethodInvoker(this.Close));
                            else this.Close();
                            return;
                        }
                    }
                    else if (OpenIfFound)
                    {
                        string xid = "";
                        var id = xfiles.GetAttributeValue("data-id", xid);
                        if (!string.IsNullOrEmpty(id))
                        {
                            _hasFound = true;
                            var xbbtn = FindElement("a", new string[] {id});
                            xbbtn?.InvokeMember("click", id);
                            return;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(xnext))
                        Navigate(xnext);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                XMessageBox.Show(exception.Message, "Fout", MessageBoxIcon.Warning);
                this.Dispose();
            }
        }

        private HtmlElement FindElement(string tagname, string[] criterias)
        {
            var xelements = Browser.Document?.GetElementsByTagName(tagname);
            if (xelements == null || xelements.Count == 0) return null;
            var element = xelements.Cast<HtmlElement>()
                .FirstOrDefault(x => criterias.Any(c => x.OuterHtml.ToLower().Contains(c.ToLower()) && x.OuterHtml.ToLower().Contains("file_link")));
            return element;
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                if (!e.Cancelled && e.UserState is string file)
                {
                    Process.Start(file);
                    if (this.InvokeRequired)
                        this.Invoke(new MethodInvoker(this.Close));
                    else this.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void WebBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SetLastInfo();
            if (!Browser.Disposing && !Browser.IsDisposed)
                Browser.Dispose();
            if (!this.Disposing && !this.IsDisposed)
                this.Dispose();
        }

        private void WebBrowserForm_Shown(object sender, EventArgs e)
        {
            this.InitLastInfo();
        }
    }
}
