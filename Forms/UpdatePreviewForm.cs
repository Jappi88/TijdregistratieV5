using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms
{
    public partial class UpdatePreviewForm : MetroFramework.Forms.MetroForm
    {
        private readonly bool _Isnew;
        private readonly bool _IsHelp;
        public bool IsValid { get; private set; }
        public string Title
        {
            get => this.Text;
            set
            {
                this.Text = value;
                Invalidate();
            }
        }

        public UpdatePreviewForm(string link, bool isnew, bool ishelp)
        {
            InitializeComponent();
            _IsHelp = ishelp;
            _Isnew = isnew;
            if (ishelp) Style = MetroColorStyle.Blue;
            else Style = MetroColorStyle.Orange;
            LoadUrl(link);
        }

        public UpdatePreviewForm(string[] links)
        {
            InitializeComponent();
            _Isnew = false;
            LoadUrls(links);
        }

        public void LoadUrl(string Url)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(() =>
                    {
                        xLoadUrl(Url);
                    }));
                else xLoadUrl(Url);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                IsValid = false;
            }
        }

        public void LoadUrls(string[] Urls)
        {
            try
            {
                if (_IsHelp) return;
                if (this.InvokeRequired)
                    this.Invoke(new Action(() =>
                    {
                        xLoadUrls(Urls);
                    }));
                else xLoadUrls(Urls);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                IsValid = false;
            }
        }

        private void xLoadUrl(string Url)
        {
            try
            {
                var html = Functions.GetStringFromUrl(Url);
                IsValid = !string.IsNullOrEmpty(html);
                htmlPanel1.Text = html;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                IsValid = false;
            }
        }

        private void xLoadUrls(string[] Urls)
        {
            try
            {
                foreach (var url in Urls)
                {
                    var html = Functions.GetStringFromUrl(url);
                    htmlPanel1.Text += html;
                   
                }
                IsValid = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Manager.DefaultSettings != null)
            {
                Manager.DefaultSettings.LastPreviewVersion = this.ProductVersion.ToString();
                timer1.Stop();
            }
        }

        private void UpdatePreviewForm_Shown(object sender, EventArgs e)
        {
            var xsplash = Application.OpenForms["SplashScreen"];
            xsplash?.Close();
            if (_Isnew)
                timer1.Start();
        }

        private void htmlPanel1_StylesheetLoad(object sender, HtmlRenderer.Entities.HtmlStylesheetLoadEventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                var xstyle = Functions.GetStringFromUrl(e.Src);
                e.SetStyleSheet = xstyle;
            }));
            
        }

        private void htmlPanel1_ImageLoad(object sender, HtmlRenderer.Entities.HtmlImageLoadEventArgs e)
        {
            Task.Factory.StartNew(new Action(() =>
            {
                var img = Functions.ImageFromUrl(e.Src);
                if (img != null)
                {
                    e.Callback(img);
                }

                e.Handled = true;

            }));
           
        }

        private void htmlPanel1_LinkClicked(object sender, HtmlRenderer.Entities.HtmlLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Link))
            {

                if (e.Link.ToLower().StartsWith("move"))
                {
                    string[] xvalues = e.Link.Split(':');
                    if (xvalues.Length > 0)
                    {
                        var xvalue = xvalues[xvalues.Length - 1];
                        xvalues = xvalue.Split(',');
                        if (xvalue.Length > 1)
                        {
                            if (int.TryParse(xvalues[0], out var x) && int.TryParse(xvalues[1], out var y))
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    if (htmlPanel1.HorizontalScroll.Value == x) break;
                                    htmlPanel1.HorizontalScroll.Value = x;
                                    htmlPanel1.Invalidate();
                                }
                                for (int i = 0; i < 10; i++)
                                {
                                    if (htmlPanel1.VerticalScroll.Value == y) break;
                                    htmlPanel1.VerticalScroll.Value = y;
                                    htmlPanel1.Invalidate();
                                }
                            }
                        }
                    }
                    e.Handled = true;
                }
                if (e.Link.ToLower().StartsWith("find"))
                {
                    string[] xvalues = e.Link.Split(':');
                    if (xvalues.Length > 0)
                    {
                        var xvalue = xvalues[xvalues.Length - 1];
                        htmlPanel1.ScrollToElement(xvalue);
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
