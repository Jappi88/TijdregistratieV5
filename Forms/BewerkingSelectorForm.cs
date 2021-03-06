using Forms.MetroBase;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class BewerkingSelectorForm : MetroBaseForm
    {

        public BewerkingSelectorForm()
        {
            InitializeComponent();
            this.Shown += BewerkingSelectorForm_Shown;
            this.FormClosing += BewerkingSelectorForm_FormClosing;
            productieListControl1.ValidHandler = IsAllowed;
        }

        public IsValidHandler IsValidHandler { get => productieListControl1.ValidHandler; set => productieListControl1.ValidHandler = value; }

        private void BewerkingSelectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            productieListControl1.CloseUI();
        }

        private void BewerkingSelectorForm_Shown(object sender, EventArgs e)
        {
            productieListControl1.InitEvents();
            ListBewerkingen(Bewerkingen);
        }

        public BewerkingSelectorForm(ViewState[] bewerkingstates, bool filter) : this()
        {
            Bewerkingen = Manager.xGetBewerkingen(bewerkingstates, filter, true);
        }

        public BewerkingSelectorForm(List<Bewerking> bws):this()
        {
            Bewerkingen = bws;
        }

        public bool IsAllowed(object sender, object item, string filter, bool tempfilter = false)
        {
            if(item is Bewerking bewerking)
            {
                return Bewerkingen.IndexOf(bewerking) > -1;
            }
            return false;
        }

        public List<Bewerking> SelectedBewerkingen { get; private set; } = new List<Bewerking>();
        public List<Bewerking> Bewerkingen { get; private set; } = new List<Bewerking>();

        private void ListBewerkingen(List<Bewerking> bws)
        {
            try
            {
                if (bws != null && bws.Count > 0)
                {
                    productieListControl1.InitProductie(bws, true, true, false);
                }
                else
                {
                    productieListControl1.InitProductie(true, true, true, true,true,true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xok_Click(object sender, System.EventArgs e)
        {
            try
            {
                SelectedBewerkingen = productieListControl1.ProductieLijst.CheckedObjects.OfType<Bewerking>().ToList();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xannuleren_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
