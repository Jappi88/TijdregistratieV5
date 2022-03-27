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

        public string RootPath => fileBrowserUI1.RootPath;

        public BijlageForm(IProductieBase productie):this(productie.ArtikelNr)
        {
        }

        public BijlageForm(string id) : this()
        {
            string xpath = Path.Combine(Manager.DbPath, "Bijlages", id);
            SetPath(xpath);
        }

        private void xclose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        public void SetPath(string path)
        {
           fileBrowserUI1.Navigate(path);
        }

        public override bool Equals(object obj)
        {
           if(obj is BijlageForm form)
                return string.Equals(form.RootPath, RootPath, System.StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return RootPath?.GetHashCode() ?? 0;
        }

        private void BijlageForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            fileBrowserUI1?.Close();
        }
    }
}
