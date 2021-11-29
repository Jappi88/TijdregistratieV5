using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MetroFramework.Drawing.Html;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Productie;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace AutoUpdaterDotNET
{
    internal partial class UpdateForm : Form
    {
        private readonly UpdateInfoEventArgs _args;

        public UpdateForm(UpdateInfoEventArgs args)
        {
            _args = args;
            InitializeComponent();
            buttonSkip.Visible = AutoUpdater.ShowSkipButton;
            buttonRemindLater.Visible = AutoUpdater.ShowRemindLaterButton;
           // var resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));

            var curversion = new Version(_args.CurrentVersion);
            bool isnew = curversion > _args.InstalledVersion;
            labelUpdate.Text = isnew
                ? $"Een nieuwe versie van {AutoUpdater.AppTitle} is beschikbaar!"
                : $"Er is geen nieuwe update beschikbaar voor {AutoUpdater.AppTitle}.";
            this.Text = labelUpdate.Text;
           xdescription.Text = isnew? $"{AutoUpdater.AppTitle} {_args.CurrentVersion} is nu beschikbaar." +
                $" Jij hebt versie {_args.InstalledVersion} geinstalleerd. Wil je nu downloaden?" : 
                $"Huidige versie van {AutoUpdater.AppTitle} is {_args.CurrentVersion}.";
           buttonUpdate.Text = isnew ? "Update" : "Download";

           if (string.IsNullOrEmpty(_args.ChangelogURL))
           {
               var reduceHeight = xchangelog.Height;
               xchangelog.Hide();
               Height -= reduceHeight;
           }
           else
           {
               xchangelog.Text = ChangeLogTxtToHtml(_args.ChangelogURL, labelUpdate.Text, curversion.ToString());
               //if (null != AutoUpdater.BasicAuthChangeLog)
               //{
               //    webBrowser.Navigate(_args.ChangelogURL, "", null,
               //        $"Authorization: {AutoUpdater.BasicAuthChangeLog}");
               //}
               //else
               //{

               //}
           }

            if (AutoUpdater.Mandatory && AutoUpdater.UpdateMode == Mode.Forced)
            {
                ControlBox = false;
            }
        }

        //private void UseLatestIE()
        //{
        //    int ieValue = 0;
        //    switch (webBrowser.Version.Major)
        //    {
        //        case 11:
        //            ieValue = 11001;
        //            break;
        //        case 10:
        //            ieValue = 10001;
        //            break;
        //        case 9:
        //            ieValue = 9999;
        //            break;
        //        case 8:
        //            ieValue = 8888;
        //            break;
        //        case 7:
        //            ieValue = 7000;
        //            break;
        //    }

        //    if (ieValue != 0)
        //    {
        //        try
        //        {
        //            using (RegistryKey registryKey =
        //                Registry.CurrentUser.OpenSubKey(
        //                    @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
        //                    true))
        //            {
        //                registryKey?.SetValue(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName),
        //                    ieValue,
        //                    RegistryValueKind.DWord);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            // ignored
        //        }
        //    }
        //}

        private void UpdateFormLoad(object sender, EventArgs e)
        {
           
        }

        private string ChangeLogTxtToHtml(string text, string title, string versie)
        {
            if (string.IsNullOrEmpty(text)) return text; 
            var xbody = text.Replace("[", "</ul>\n<h2>").Replace("]", "</h2>\n<ul>");
            xbody = xbody.Substring(7) + "\n</ul>\n";
            using var streamreader = new StringReader(xbody);
            string xline = null;
            while (streamreader.Peek() > -1)
            {
                xline ??= streamreader.ReadLine();
                if (!string.IsNullOrEmpty(xline) && xline.StartsWith("*"))
                {
                    string xnextline = null;
                    string xtoadd = "";
                    while (streamreader.Peek() > -1)
                    {
                        xnextline = streamreader.ReadLine();
                        if (!string.IsNullOrEmpty(xnextline) && !xnextline.StartsWith("*") &&
                            !xnextline.StartsWith("<"))
                        {
                            xtoadd += "\n" + xnextline.TrimStart();
                            if (!string.IsNullOrWhiteSpace(xnextline))
                                xbody = xbody.Replace(xnextline, "");
                        }
                        else
                            break;
                    }

                    string ximagekey = xline.ToLower().Contains("opgelost") ? "opgelost" :
                        xline.ToLower().Contains("toegevoegd") ? "toegevoegd" : "geen idee";
                    var xlineimage = $"<img src=\"{ximagekey}\" />";
                    string newline = xline.Replace("* ", "<li>{0}") + xtoadd + "</li>";
                    newline = string.Format(newline, xlineimage);
                    
                    xbody = xbody.Replace(xline, newline);
                    if (!string.IsNullOrEmpty(xnextline))
                        xline = xnextline;
                    else xline = null;

                }
                else xline = null;
            }
            string xreturn = $"<html>\n" +
                             $"<head>\n" +
                             $"<title>Changelogs</title>\n" +
                             $"<link rel=\"Stylesheet\" href=\"StyleSheet\" />\n" +
                             $"</head>\n" +
                             $"<body style=\"background - color: #333; background-gradient: #707; background-gradient-angle: 60; margin: 0;\">\n" +
                             $"<blockquote class=\"whitehole\">\n" +
                             $"{xbody}\n" +
                             $"</blockquote>\n" +
                             $"</body>\n" +
                             $"</html>";
            return xreturn;
        }

        private void ButtonUpdateClick(object sender, EventArgs e)
        {
            if (AutoUpdater.OpenDownloadPage)
            {
                var processStartInfo = new ProcessStartInfo(_args.DownloadURL);

                Process.Start(processStartInfo);

                DialogResult = DialogResult.OK;
            }
            else
            {
                if (AutoUpdater.DownloadUpdate(_args))
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void ButtonSkipClick(object sender, EventArgs e)
        {
            AutoUpdater.PersistenceProvider.SetSkippedVersion(new Version(_args.CurrentVersion));
        }

        private void ButtonRemindLaterClick(object sender, EventArgs e)
        {
            if (AutoUpdater.LetUserSelectRemindLater)
            {
                using (var remindLaterForm = new RemindLaterForm())
                {
                    var dialogResult = remindLaterForm.ShowDialog();

                    if (dialogResult.Equals(DialogResult.OK))
                    {
                        AutoUpdater.RemindLaterTimeSpan = remindLaterForm.RemindLaterFormat;
                        AutoUpdater.RemindLaterAt = remindLaterForm.RemindLaterAt;
                    }
                    else if (dialogResult.Equals(DialogResult.Abort))
                    {
                        ButtonUpdateClick(sender, e);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            AutoUpdater.PersistenceProvider.SetSkippedVersion(null);

            DateTime remindLaterDateTime = DateTime.Now;
            switch (AutoUpdater.RemindLaterTimeSpan)
            {
                case RemindLaterFormat.Days:
                    remindLaterDateTime = DateTime.Now + TimeSpan.FromDays(AutoUpdater.RemindLaterAt);
                    break;
                case RemindLaterFormat.Hours:
                    remindLaterDateTime = DateTime.Now + TimeSpan.FromHours(AutoUpdater.RemindLaterAt);
                    break;
                case RemindLaterFormat.Minutes:
                    remindLaterDateTime = DateTime.Now + TimeSpan.FromMinutes(AutoUpdater.RemindLaterAt);
                    break;
            }

            AutoUpdater.PersistenceProvider.SetRemindLater(remindLaterDateTime);
            AutoUpdater.SetTimer(remindLaterDateTime);

            DialogResult = DialogResult.Cancel;
        }

        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AutoUpdater.Running = false;
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AutoUpdater.Mandatory && AutoUpdater.UpdateMode == Mode.Forced)
            {
                e.Cancel = e.CloseReason == CloseReason.UserClosing;
            }
        }

        private void xchangelog_StylesheetLoad(object sender, HtmlStylesheetLoadEventArgs e)
        {
            var stylesheet = IProductieBase.GetStylesheet(e.Src);
            if (stylesheet != null)
                e.SetStyleSheet = stylesheet;
        }

        private void xchangelog_ImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            var ximage = GetImage(e.Src);
            if (ximage != null)
                e.Callback(ximage);
        }


        private Image GetImage(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            switch (name.ToLower())
            {
                case "opgelost":
                    return Resources.fixed_Bug_32x32.ResizeImage(16,16);
                case "toegevoegd":
                    return Resources.add_Blue_circle_32x32.ResizeImage(16, 16);
                default:
                    return Resources.verbeteringen_32x32.ResizeImage(16, 16);
            }
        }
    }
}