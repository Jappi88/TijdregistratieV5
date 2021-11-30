using Forms;
using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private HtmlNodeCollection GetNodeCollection()
        {
            var html = Browser.DocumentText;
            var xdoc = new HtmlAgilityPack.HtmlDocument();
            xdoc.LoadHtml(html);
            var xelements =
                xdoc.DocumentNode.SelectNodes(
                    "/html[1]/body[1]/div[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[1]/div[1]/table[1]/tbody[1]");
            return xelements;
        }

        private bool _hasFound;
        private async void xBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (_hasFound || FilesToOpen == null || FilesToOpen.Length == 0 || string.IsNullOrEmpty(FileDownloadUrl)) return;
            
            HtmlNodeCollection xelements = null;
            await Task.Factory.StartNew(new Action(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    if (Browser.InvokeRequired)
                    {
                        Browser.Invoke(new MethodInvoker(() => xelements = GetNodeCollection()));
                    }
                    else xelements = GetNodeCollection();

                    if (xelements != null) break;

                    Thread.Sleep(250);
                }
            }));
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
                    //Browser.Navigate(xlink, true);
                    //WebClient client = new WebClient();
                    //var tmp = Path.GetTempPath() + "temp_tekening.pdf";
                    //client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    //client.DownloadFileAsync(new Uri(xlink),tmp);
                }
            }
        }

        private HtmlElement FindElement(string tagname, string[] criterias)
        {
            var xelements = Browser.Document?.GetElementsByTagName("a");
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
    }
}
