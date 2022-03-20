using System.IO;
using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class BijlageForm : MetroBaseForm
    {
        public BijlageForm()
        {
            InitializeComponent();
        }

        public BijlageForm(IProductieBase productie):this()
        {
            string xpath = Path.Combine(Manager.DbPath, "Bijlages", productie.ArtikelNr);
            SetPath(xpath);
        }

        private void xclose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        public void SetPath(string path)
        {
            bijlageUI1.SetPath(path);
        }
    }
}
