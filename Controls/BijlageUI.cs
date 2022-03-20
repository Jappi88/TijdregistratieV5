using System;
using System.IO;
using System.Windows.Forms;

namespace Controls
{
    public partial class BijlageUI : UserControl
    {
        public BijlageUI()
        {
            InitializeComponent();
        }

        public string BasePath { get; private set; }

        public void SetPath(string path)
        {
         
            if (xbijlagebrowser.Url == null || !xbijlagebrowser.Url.LocalPath.StartsWith(path))
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                BasePath = path;
                var xlink = new Uri(path);
                xbijlagebrowser.Url = xlink;
            }
        }

        private void xbijlagebrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            UpdateButtons(e.Url);
        }

        private void UpdateButtons(Uri uri)
        {
            var xvorigepath = Path.GetDirectoryName(uri.LocalPath);
            xvorige.Enabled = xvorigepath != null && xvorigepath.ToLower().Contains(BasePath.ToLower()) && Directory.Exists(xvorigepath) &&
                              xbijlagebrowser.CanGoBack;
            xvolgende.Enabled = xbijlagebrowser.CanGoForward;
            xstatus.Text = "Bijlages" + uri.LocalPath.Replace(Path.GetDirectoryName(BasePath) ?? "", "");
        }

        private void xvorige_Click(object sender, EventArgs e)
        {
            try
            {
                xbijlagebrowser.GoBack();
            }
            catch (Exception exception)
            {
                xvorige.Enabled = false;
            }
        }

        private void xvolgende_Click(object sender, EventArgs e)
        {
            try
            {
                xbijlagebrowser.GoForward();
            }
            catch (Exception exception)
            {
                xvolgende.Enabled = false;
            }
        }

        private void xbijlagebrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            try
            {
                if (!Directory.Exists(e.Url.LocalPath))
                {
                    UpdateButtons(xbijlagebrowser.Url);
                    e.Cancel = true;
                }
            }
            catch (Exception exception)
            {
                xvorige.Enabled = false;
            }
        }
    }
}
