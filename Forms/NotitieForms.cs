using Rpm.Productie;
using Rpm.Various;
using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class NotitieForms : Forms.MetroBase.MetroBaseForm
    {
        public NotitieEntry Notitie { get; set; }
        public NotitieForms()
        {
            InitializeComponent();
            Notitie = new NotitieEntry("", NotitieType.Algemeen);
            SetTitle();
        }

        private void SetTitle()
        {
            if (Notitie == null)
                this.Text = "Nieuwe Notitie";
            else
            {
                this.Text = $"{Enum.GetName(typeof(NotitieType), Notitie.Type)} Notitie voor {Notitie.Path}";
            }
        }

        public NotitieForms(NotitieEntry note) : this()
        {
            Notitie = note ?? new NotitieEntry("", NotitieType.Algemeen);
            xnaam.Text = Notitie.Naam;
            xnotitie.Text = Notitie.Notitie;
            SetTitle();
        }

        public NotitieForms(NotitieEntry note, ProductieFormulier prod) : this()
        {
            Notitie = note ?? new NotitieEntry("", prod);
            xnaam.Text = Notitie.Naam;
            xnotitie.Text = Notitie.Notitie;
            SetTitle();
        }
        public NotitieForms(NotitieEntry note, Bewerking bew) : this()
        {
            Notitie = note ?? new NotitieEntry("", bew);
            xnaam.Text = Notitie.Naam;
            xnotitie.Text = Notitie.Notitie;
            SetTitle();
        }

        public NotitieForms(NotitieEntry note, WerkPlek plek) : this()
        {
            Notitie = note ?? new NotitieEntry("", plek);
            xnaam.Text = Notitie.Naam;
            xnotitie.Text = Notitie.Notitie;
            SetTitle();
        }


        public string Title
        {
            get => this.Text;
            set => this.Text = value;
        }

        private void xokb_Click(object sender, EventArgs e)
        {
            Notitie.Naam = xnaam.Text.Trim();
            Notitie.Notitie = xnotitie.Text.Trim();
            DialogResult = DialogResult.OK;
        }

        private void xcancelb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
