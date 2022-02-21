using System;
using System.Drawing;
using Forms.MetroBase;
using Rpm.DailyUpdate;
using Rpm.Productie;
using Rpm.Various;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace Forms
{
    public partial class DailyMessageForm : MetroBaseForm
    {
        public DailyMessageForm(Daily daily)
        {
            InitializeComponent();
            Daily = daily;
            Daily.ImageList.ImageSize = new Size(128, 128);
            Text = $"{Daily.GetDailyGroet()}!";
            Invalidate();
        }

        public Daily Daily { get; set; }

        public string HtmlText
        {
            get => htmlPanel1.Text;
            set => htmlPanel1.Text = value;
        }

        private void htmlPanel1_ImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            e.Callback(Daily.ImageList.Images[e.Src]);
        }

        private void htmlPanel1_LinkClicked(object sender, HtmlLinkClickedEventArgs e)
        {
            try
            {
                if (Manager.Database?.ProductieFormulieren == null) return;
                var xprod = Werk.FromPath(e.Link);
                if (xprod?.Bewerking == null) return;
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