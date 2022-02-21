using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms.MetroBase;
using MetroFramework;
using PdfSharp;
using PdfSharp.Drawing;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Forms
{
    public partial class UpdatePreviewForm : MetroBaseForm
    {
        private readonly bool _IsHelp;
        private readonly bool _Isnew;

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

        public bool IsValid { get; private set; }

        public string Title
        {
            get => Text;
            set
            {
                Text = value;
                Invalidate();
            }
        }

        public void LoadUrl(string Url)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new Action(() => { xLoadUrl(Url); }));
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
                if (InvokeRequired)
                    Invoke(new Action(() => { xLoadUrls(Urls); }));
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
                Manager.DefaultSettings.LastPreviewVersion = ProductVersion;
                Manager.DefaultSettings.PreviewShown = true;
                Manager.DefaultSettings.SaveAsDefault();
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

        private void htmlPanel1_StylesheetLoad(object sender, HtmlStylesheetLoadEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                var xstyle = Functions.GetStringFromUrl(e.Src);
                e.SetStyleSheet = xstyle;
            });
        }

        private void htmlPanel1_ImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                var img = GraphicsExtensions.ImageFromUrl(e.Src);
                if (img != null) e.Callback(img);

                e.Handled = true;
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //var pages = GetHtmlPages();
                //if (pages.Length == 0) return;
                var config = new PdfGenerateConfig();
                config.PageSize = PageSize.A4;
                config.SetMargins(5);
                var xpage = htmlPanel1.Text.Replace("<img width = \"900\" height=\"500\"",
                    "<img width =\"475\" height=\"250\"");
                var pdf = PdfGenerator.GeneratePdf(xpage, config, null, OnStylesheetLoad,
                    OnImageLoadPdfSharp); //new PdfDocument();
                //foreach (var page in pages)
                //{
                //    PdfGenerator.AddPdfPages(pdf, page, config, null, OnStylesheetLoad, OnImageLoadPdfSharp);
                //}

                var ofd = new SaveFileDialog();
                ofd.Filter = "Pdf |*.pdf";
                ofd.Title = "Sla alles op als een PDF";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pdf.Save(ofd.FileName);
                    Process.Start(ofd.FileName);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private string[] GetHtmlPages()
        {
            var xreturn = new List<string>();
            try
            {
                var html = htmlPanel1.Text;
                var body = ReplaceInnerHtml(html, "<blockquote class=\"whitehole\">", "</blockquote>", "");
                var content = GetInnerHtml(html, "<blockquote class=\"whitehole\">", "</blockquote>");
                var page = 1;
                var xstart = 0;
                while (true)
                {
                    var xvar = $"<div id=\"page{page}\">";
                    xstart = content.IndexOf(xvar, StringComparison.CurrentCultureIgnoreCase);
                    if (xstart < 0) break;
                    xstart += xvar.Length;
                    var xend = content.IndexOf($"<div id=\"page{page + 1}\">",
                        StringComparison.CurrentCultureIgnoreCase);
                    if (xend < 0) xend = content.Length;
                    var length = xend - xstart;
                    var xpage = content.Substring(xstart, length);
                    xend = xpage.LastIndexOf("</div>", StringComparison.CurrentCultureIgnoreCase);
                    if (xend > -1) xpage = xpage.Substring(0, xend);

                    xpage = xpage.Replace("<img width = \"900\" height=\"500\"", "<img width = \"475\" height=\"250\"");
                    var xtitle = GetInnerHtml(xpage, "<h1", "</h1>");
                    xpage = RemoveHtmlBlock(xpage, "<h1", "</h1>");
                    var xbodyinfo = $"{xtitle}";
                    var xbody = ReplaceInnerHtml(body, "<div id=\"header\">", "</div>", xbodyinfo);
                    xbody = ReplaceInnerHtml(xbody, "<blockquote class=\"whitehole\">", "</blockquote>", xpage);
                    xreturn.Add(xbody);
                    page++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn.ToArray();
        }

        private string RemoveHtmlBlock(string basehtml, string xstartvalue, string xendvalue)
        {
            var xhtml = basehtml;
            var xvar = xstartvalue;
            var xstart = xhtml.FindIndex(xvar);
            if (xstart < 0) return xhtml;
            var xend = xhtml.FindIndex(xendvalue);

            if (xend < 0)
                xend = xhtml.Length;
            else xend += xendvalue.Length;
            xend -= xstart;
            xhtml = xhtml.Remove(xstart, xend);

            return xhtml;
        }

        private string ReplaceInnerHtml(string basehtml, string xstartvalue, string xendvalue, string newvalue)
        {
            var xhtml = basehtml;
            var xvar = xstartvalue;
            var xstart = xhtml.FindIndex(xvar);
            if (xstart < 0) return xhtml;
            xstart += xvar.Length;
            var xend = xhtml.FindIndex(xendvalue);
            if (xend < 0)
                xend = xhtml.Length;
            xend -= xstart;
            xhtml = xhtml.Remove(xstart, xend).Insert(xstart, newvalue);

            return xhtml;
        }

        private string GetInnerHtml(string basehtml, string xstartvalue, string xendvalue)
        {
            var xhtml = basehtml;
            var xvar = xstartvalue;
            var xstart = xhtml.FindIndex(xvar);
            if (xstart < 0) return xhtml;
            if (!xvar.EndsWith(">"))
            {
                xstart = xhtml.IndexOf('>', xstart);
                xstart++;
            }

            xstart += xvar.Length;
            var xend = xhtml.FindIndex(xendvalue);
            if (xend < 0)
                xend = xhtml.Length;
            xend -= xstart;
            xhtml = xhtml.Substring(xstart, xend);

            return xhtml;
        }

        private string GetEmptyBody(string basehtml, string title, string description)
        {
            var xhtml = basehtml;
            title = string.Empty;
            var xvar = "<blockquote class=\"whitehole\">";
            var xstart = xhtml.IndexOf(xvar, StringComparison.CurrentCultureIgnoreCase);
            if (xstart < 0) return xhtml;
            xstart += xvar.Length;
            var xend = xhtml.IndexOf("<\blockquote>", StringComparison.CurrentCultureIgnoreCase);
            if (xend < 0)
                xend = xhtml.Length;
            xend -= xstart;
            xhtml = xhtml.Remove(xstart, xend);
            return xhtml;
        }

        public static void OnImageLoadPdfSharp(object sender, HtmlImageLoadEventArgs e)
        {
            var url = e.Src;
            if (e.Src.StartsWith("http:") || e.Src.StartsWith("https:"))
                e.Callback(XImage.FromStream(GraphicsExtensions.ImageStreamFromUrl(url)));
        }

        public static void OnStylesheetLoad(object sender, HtmlStylesheetLoadEventArgs e)
        {
            var url = e.Src;
            if (e.Src.StartsWith("http:") || e.Src.StartsWith("https:"))
            {
                var xstyle = Functions.GetStringFromUrl(e.Src);
                e.SetStyleSheet = xstyle;
            }
        }
    }
}