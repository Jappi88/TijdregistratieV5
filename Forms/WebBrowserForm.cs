using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProductieManager.Forms
{
    public partial class WebBrowserForm : Form
    {
        public string FileToOpen { get; set; }
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

        private void xBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (string.IsNullOrEmpty(FileToOpen) || string.IsNullOrEmpty(FileDownloadUrl)) return;
            var html = Browser.DocumentText;
            if (html == null) return;
            var xdoc = new HtmlAgilityPack.HtmlDocument();
            xdoc.LoadHtml(html);
            var xelements =
                xdoc.DocumentNode.SelectNodes(
                    "/html[1]/body[1]/div[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[1]/div[1]/table[1]/tbody[1]");
            if (xelements == null) return;
            string xdefname = "";
            var xfiles = xelements.FirstOrDefault()?.ChildNodes.FirstOrDefault( x=> x.OuterHtml.Contains("storage_item") && 
                                                                          string.Equals(x.GetAttributeValue("data-name",xdefname),FileToOpen));
            if (!string.IsNullOrEmpty(FileDownloadUrl) && xfiles != null)
            {
                string xid = "";
                var id = xfiles.GetAttributeValue("data-id",xid);
                if (!string.IsNullOrEmpty(id))
                {
                    var xlink = string.Format(FileDownloadUrl, id);
                    Browser.Navigate(xlink);
                }
            }
        }
    }
}
