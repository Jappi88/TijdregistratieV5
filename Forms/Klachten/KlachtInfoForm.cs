using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Forms.MetroBase;
using ProductieManager.Properties;
using Rpm.Klachten;
using Rpm.Productie;
using Rpm.Various;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace Forms.Klachten
{
    public partial class KlachtInfoForm : MetroBaseForm
    {
        public KlachtInfoForm(KlachtEntry entry)
        {
            InitializeComponent();
            Klacht = entry;
            xtextfield.Text = entry.ToHtml();
        }

        public KlachtEntry Klacht { get; private set; }

        private void xtextfield_ImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            if (e.Src == "ProductieWarningicon")
            {
                e.Callback(Resources.file_warning_40447);
                e.Handled = true;
                return;
            }

            if (File.Exists(e.Src))
                try
                {
                    e.Callback(e.Src);
                    e.Handled = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
        }

        private void xtextfield_LinkClicked(object sender, HtmlLinkClickedEventArgs e)
        {
            var link = e.Link.Trim().ToLower();
            if (link.StartsWith("http") || link.StartsWith("www"))
            {
                Process.Start(link);
            }
            else
            {
                var prod = Manager.Database.GetProductie(link);
                if (prod != null) Manager.FormulierActie(new object[] {prod}, MainAktie.OpenProductie);
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
                if (Klacht.ID.ToString().ToLower().Trim() == xvalue.ToLower().Trim())
                {
                    if (InvokeRequired)
                        Invoke(new MethodInvoker(Close));
                    else Close();
                }
        }

        private void Manager_KlachtChanged(object sender, EventArgs e)
        {
            if (Disposing || IsDisposed) return;
            if (sender is KlachtEntry xvalue && Klacht != null)
                if (Klacht.Equals(xvalue))
                {
                    Klacht = xvalue;
                    if (InvokeRequired)
                        Invoke(new MethodInvoker(() => xtextfield.Text = Klacht.ToHtml()));
                    else xtextfield.Text = Klacht.ToHtml();
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