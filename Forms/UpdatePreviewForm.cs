using System;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms
{
    public partial class UpdatePreviewForm : MetroFramework.Forms.MetroForm
    {
        private readonly bool _Isnew;
        public UpdatePreviewForm(string link, bool isnew)
        {
            InitializeComponent();
            _Isnew = isnew;
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
            }
        }

        public void LoadUrls(string[] Urls)
        {
            try
            {
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
            }
        }

        private void xLoadUrl(string Url)
        {
            try
            {
                this.Text = $"NIEUW In {this.ProductVersion}!";
                this.Invalidate();
                var html = Functions.GetStringFromUrl(Url);
                htmlPanel1.Text = html;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xLoadUrls(string[] Urls)
        {
            this.Text = $"Alle Aanpassingen";
            this.Invalidate();
            try
            {
                foreach (var url in Urls)
                {
                    var html = Functions.GetStringFromUrl(url);
                    htmlPanel1.Text += html;
                }
             
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
    }
}
