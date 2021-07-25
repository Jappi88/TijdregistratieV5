using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Controls;
using MetroFramework.Forms;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms
{
    public partial class AfkeurForm : MetroForm
    {
        public ProductieFormulier Productie { get; private set; }
        public AfkeurForm()
        {
            InitializeComponent();
        }

        public AfkeurForm(ProductieFormulier productie) : this()
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

        public void InitProductie(ProductieFormulier productie)
        {
            Productie = productie.CreateCopy();
            Title = $"[Afkeur][{Productie.ProductieNr} | {Productie.ArtikelNr}] {Productie.Omschrijving}";
            xmaterialpanel.SuspendLayout();
            xmaterialpanel.Controls.Clear();
            if (Productie.Materialen != null)
            {
                int index = 0;
                foreach (var mat in Productie.Materialen)
                {
                    if (mat == null) continue;
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
                    Productie.Materialen = xmaterialpanel.Controls.Cast<AfkeurEntryUI>().Select(x => x.Materiaal).ToList();
                    await Productie.UpdateForm(false, false, null,
                        $"[{Productie.ProductieNr} | {Productie.ArtikelNr}] Afkeur aangepast");
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
