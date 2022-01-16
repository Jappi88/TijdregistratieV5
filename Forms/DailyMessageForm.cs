using System;
using System.Drawing;
using System.Windows.Forms;
using Rpm.DailyUpdate;
using Rpm.Productie;
using Rpm.Various;

namespace Forms
{
    public partial class DailyMessageForm : MetroFramework.Forms.MetroForm
    {
        public Daily Daily { get; set; }
        public DailyMessageForm(Daily daily)
        {
            InitializeComponent();
            Daily = daily;
            Daily.ImageList.ImageSize = new Size(128, 128);
            this.Text = $"{Daily.GetDailyGroet()}!";
            this.Invalidate();
        }

        public string HtmlText
        {
            get => htmlPanel1.Text;
            set => htmlPanel1.Text = value;
        }

        private void htmlPanel1_ImageLoad(object sender, TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs e)
        {
            e.Callback(Daily.ImageList.Images[e.Src]);
        }

        private void htmlPanel1_LinkClicked(object sender, TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs e)
        {
            try
            {
                if (Manager.Database?.ProductieFormulieren == null) return;
                var xprod = Werk.FromPath(e.Link);
                if(xprod?.Bewerking == null) return;
                Manager.FormulierActie(new object[] {xprod.Formulier, xprod.Bewerking}, MainAktie.OpenProductie);
                e.Handled = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
