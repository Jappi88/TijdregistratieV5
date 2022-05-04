using ProductieManager.Properties;
using Rpm.Klachten;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Various;

namespace Forms.Klachten
{
    public partial class KlachtInfoForm : Forms.MetroBase.MetroBaseForm
    {
        public KlachtEntry Klacht { get; private set; }
        public KlachtInfoForm(KlachtEntry entry)
        {
            InitializeComponent();
            Klacht = entry;
            xtextfield.Text = entry.ToHtml();
        }

        private void xtextfield_ImageLoad(object sender, TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs e)
        {
            if (e.Src == "ProductieWarningicon")
            {
                e.Callback(Resources.file_warning_40447);
                e.Handled = true;
                return;
            }
            if (File.Exists(e.Src))
            {
                try
                {
                    if (e.Src.IsImageFile())
                    {
                        var image = Image.FromFile(e.Src);
                        var maxsize = new Size(1200, 720);
                        if (image.Width > maxsize.Width || image.Height > maxsize.Height)
                            image = image.ResizeImage(maxsize);
                        e.Callback(image);
                        e.Handled = true;
                    }
                   
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void xtextfield_LinkClicked(object sender, TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs e)
        {
            var link = e.Link.Trim().ToLower();
            if (link.StartsWith("http") || link.StartsWith("www"))
            {
                Process.Start(link);
            }
            else
            {
                var prod = Manager.Database.GetProductie(link, false);
                if (prod != null)
                {
                    Manager.FormulierActie(new object[] {prod}, MainAktie.OpenProductie);
                }
            }
        }

        private void KlachtInfoForm_Shown(object sender, EventArgs e)
        {
            Manager.KlachtChanged += Manager_KlachtChanged;
            Manager.KlachtDeleted += Manager_KlachtDeleted;
            timer1.Start();
        }

        private void Manager_KlachtDeleted(object sender, EventArgs e)
        {
            if (sender is string xvalue && Klacht != null)
            {
                if (Klacht.ID.ToString().ToLower().Trim() == xvalue.ToLower().Trim())
                {
                    if (InvokeRequired)
                        this.Invoke(new MethodInvoker(this.Close));
                    else this.Close();
                }
            }
           
        }

        private void Manager_KlachtChanged(object sender, EventArgs e)
        {
            if (this.Disposing || this.IsDisposed) return;
            if (sender is KlachtEntry xvalue && Klacht != null)
            {
                if (Klacht.Equals(xvalue))
                {
                    Klacht = xvalue;
                    if (InvokeRequired)
                        this.Invoke(new MethodInvoker(() => xtextfield.Text = Klacht.ToHtml()));
                    else xtextfield.Text = Klacht.ToHtml();
                }
            }
        }

        private void KlachtInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1?.Stop();
            Manager.KlachtChanged -= Manager_KlachtChanged;
            Manager.KlachtDeleted -= Manager_KlachtDeleted;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1?.Stop();
            try
            {
                if (Klacht is {IsGelezen: false})
                {
                    Klacht.IsGelezen = true;
                    Manager.Klachten?.SaveKlacht(Klacht, false);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
