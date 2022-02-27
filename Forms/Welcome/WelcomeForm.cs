using System;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Misc;
using Rpm.Productie;

namespace ProductieManager.Forms.Welcome
{
    public partial class WelcomeForm : MetroBaseForm
    {
        private const string WelcomeLinks = "https://www.dropbox.com/s/ucbcyz8dvzvbpka/Welcome%20Links.txt?dl=1";
        public WelcomeForm()
        {
            InitializeComponent();
            metroTabControl1.SelectedIndex = 0;
            UpdateButtonVisibility();
        }

        private void LoadWelcome()
        {
            var xvalues = Functions.GetVersionPreviews(WelcomeLinks);
        }

        private void UpdateButtonVisibility()
        {
            try
            {
                xvorige.Visible = metroTabControl1.SelectedIndex > 0;
                xvolgende.Visible = metroTabControl1.SelectedIndex < metroTabControl1.TabPages.Count -1;
                xsluiten.Visible = metroTabControl1.SelectedIndex == metroTabControl1.TabPages.Count -1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateButtonVisibility();
        }

        private void xvorige_Click(object sender, EventArgs e)
        {
            if (metroTabControl1.SelectedIndex > 0)
                metroTabControl1.SelectedIndex--;
        }

        private void xvolgende_Click(object sender, EventArgs e)
        {
            if (metroTabControl1.SelectedIndex < metroTabControl1.TabPages.Count -1)
                metroTabControl1.SelectedIndex++;
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            if (metroTabControl1.SelectedIndex == metroTabControl1.TabPages.Count - 1)
                this.Close();
        }

        private void WelcomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Manager.DefaultSettings != null)
            {
                Manager.DefaultSettings.WelcomeShown = true;
                Manager.DefaultSettings.SaveAsDefault();
            }
        }
    }
}
