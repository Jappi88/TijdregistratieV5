using Controls;
using Forms.MetroBase;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class AfkeurForm : MetroBaseForm
    {
        public IProductieBase Productie { get; private set; }
        public AfkeurForm()
        {
            InitializeComponent();
        }

        public AfkeurForm(IProductieBase productie) : this()
        {
            InitProductie(productie);
        }

        public string Title
        {
            get => this.Text;
            set
            {
                this.Text = value;
                this.Invalidate();
            }
        }

        public void InitProductie(IProductieBase productie)
        {
            Productie = productie.CreateCopy();
            Title = $"[Afkeur][{Productie.ProductieNr} | {Productie.ArtikelNr}] {Productie.Omschrijving}";
            xmaterialpanel.SuspendLayout();
            xmaterialpanel.Controls.Clear();
            var mats = productie.GetMaterialen();
            if (mats != null)
            {
                int index = 0;
                var afk = new List<Materiaal>();
                bool isbw = false;
                if(productie is Bewerking bewerking)
                {
                    afk = bewerking.Afkeur;
                    isbw = true;
                }
                foreach (var mat in mats)
                {
                    if (mat == null) continue;
                    if (isbw)
                    {
                        var af = afk.FirstOrDefault(x => string.Equals(x.ArtikelNr, mat.ArtikelNr, StringComparison.CurrentCultureIgnoreCase));
                        if (af != null)
                            mat.AantalAfkeur = af.AantalAfkeur;
                        else mat.AantalAfkeur = 0;
                    }
                    var xui = new AfkeurEntryUI();
                    xui.InitMateriaal(mat);
                    //xui.Dock = DockStyle.Top;
                    xui.Location = new Point(5, (index * 85) + 5);
                    xui.Size = new Size(xmaterialpanel.Width - 10, 85);
                    xui.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                    xmaterialpanel.Controls.Add(xui);
                    index++;
                }
            }

            xmaterialpanel.ResumeLayout(true);
        }

        private async void xok_Click(object sender, EventArgs e)
        {
            if (Productie == null) DialogResult = DialogResult.Cancel;
            else
            {
                try
                {
                    ProductieFormulier form = null;
                    var mats = xmaterialpanel.Controls.Cast<AfkeurEntryUI>().Select(x => x.Materiaal).ToList();
                    if (Productie is ProductieFormulier formulier)
                    {
                        form = formulier;
                    }
                    else if (Productie is Bewerking bew && bew.Parent != null)
                    {
                        form = bew.Parent;
                        bew.Afkeur = mats.Where(x => x.AantalAfkeur > 0).ToList().CreateCopy();
                    }
                    if (form != null)
                    {
                        mats.ForEach(x => x.AantalAfkeur = form.Bewerkingen.Sum(b =>
                                                                                b.Afkeur.Where(a => string.Equals(x.ArtikelNr, a.ArtikelNr, StringComparison.CurrentCultureIgnoreCase)).Sum(a => a.AantalAfkeur)));
                        form.Materialen = mats;
                        await form.UpdateForm(false, false, null,
                      $"[{Productie.ProductieNr}, {Productie.ArtikelNr}] Afkeur gewijzigd");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void AfkeurForm_Shown(object sender, System.EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }

        private void AfkeurForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            try
            {
                if (this.Disposing || this.IsDisposed || changedform == null || Productie == null ||
                    !Productie.Equals(changedform))
                    return;
                Productie = changedform.CreateCopy();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
