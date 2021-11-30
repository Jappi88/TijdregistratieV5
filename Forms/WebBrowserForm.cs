using Forms;
using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Various;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ProductieManager.Forms
{
    public partial class WebBrowserForm : Form
    {
        public string[] FilesToOpen { get; set; }
        public string FileDownloadUrl { get; set; }
        public WebBrowserForm()
        {
            InitializeComponent();
            Browser.ScriptErrorsSuppressed = true;
        }

        public WebBrowser Browser => this.xBrowser;

        public void Navigate(string url)
        {
            Browser.Navigate(url);
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
            if (_hasFound || FilesToOpen == null || FilesToOpen.Length == 0 || string.IsNullOrEmpty(FileDownloadUrl)) return;
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
                    throw new Exception("Verbinding kan niet tot stand worden gebracht.\n\nControlleer je netwerkinstellingen voor of je toegang hebt tot de server.");
                }
                if (xelements == null)
                    return;
                string xdefname = "";
                var xfiles = xelements.FirstOrDefault()?.ChildNodes.FirstOrDefault( x=> FilesToOpen.Any(f=> 
                    string.Equals(x.GetAttributeValue("data-name",xdefname),f, StringComparison.CurrentCultureIgnoreCase)));
                if (xfiles == null)
                {
                    XMessageBox.Show($"Geen FBR tekening gevonden voor {string.Join(", ", FilesToOpen)}",
                        "Geen Tekening Gevonden!", MessageBoxIcon.Exclamation);
                    if (this.InvokeRequired)
                        this.Invoke(new MethodInvoker(this.Close));
                    else this.Close();
                    return;
                }
                if (!string.IsNullOrEmpty(FileDownloadUrl))
                {
                    string xid = "";
                    var id = xfiles.GetAttributeValue("data-id",xid);
                    if (!string.IsNullOrEmpty(id))
                    {
                        _hasFound = true;
                        var xlink = string.Format(FileDownloadUrl, id);
                        var xbbtn = FindElement("a", new string[] {id });
                        xbbtn?.InvokeMember("click",id);
                    }
                }
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
