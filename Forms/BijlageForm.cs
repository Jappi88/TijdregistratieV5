using System.IO;
using Forms.FileBrowser;
using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class BijlageForm : MetroBaseForm
    {
        public BijlageForm()
        {
            InitializeComponent();
            this.Shown += BijlageForm_Shown;
        }

        private void BijlageForm_Shown(object sender, System.EventArgs e)
        {
            if(!string.IsNullOrEmpty(NavigationPath))
                SetPath(NavigationPath);
            else if(!string.IsNullOrEmpty(RootPath))
                SetPath(RootPath);
        }

        public string RootPath
        {
            get => fileBrowserUI1.RootPath;
            set=> fileBrowserUI1.RootPath = value;
        }

        public string NavigationPath { get; set; }

        public FileBrowserUI Browser => fileBrowserUI1;

        public BijlageForm(IProductieBase productie):this(productie.ArtikelNr)
        {
        }

        public BijlageForm(string id) : this()
        {
            string xpath = Path.Combine(Manager.DbPath, "Bijlages", id);
            RootPath = xpath;
            //SetPath(xpath);
        }

        public BijlageForm(string id, string root) : this()
        {
            string xpath = Path.Combine(Manager.DbPath, "Bijlages", id);
            RootPath = root;
           // SetPath(xpath);
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
            if (obj is BijlageForm form)
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
