using System;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Forms;
using ProductieManager.Forms.MetroDock;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;
using WeifenLuo.WinFormsUI.Docking;

namespace Forms
{
    public partial class StartProductie : DockInstance
    {
        private Bewerking _selected;
        public StartProductie(ProductieFormulier formulier, Bewerking bewerking)
        {
            InitializeComponent();
            //SetStyle(
            //    ControlStyles.UserPaint |
            //    ControlStyles.AllPaintingInWmPaint |
            //    ControlStyles.OptimizedDoubleBuffer |
            //    ControlStyles.SupportsTransparentBackColor,
            //    true);
            Formulier = formulier;
            _selected = bewerking;
            DisplayHeader = false;
            ShadowType = MetroFormShadowType.AeroShadow;
        }

        public ProductieFormulier Formulier { get; set; }

        public Bewerking SelectedBewerking
        {
            get => productieForm1.SelectedBewerking;
            set => productieForm1.SelectedBewerking = value;
        }

        private void ProductieForm1_OnCloseButtonPressed(object sender, EventArgs e)
        {
           // DialogResult = DialogResult.Cancel;
           Close();
        }

        private void Formulier_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            if (this.IsDisposed || this.Disposing || !Visible) return;
            if (changedform.Equals(Formulier))
            {
                try
                {
                   
                    if (InvokeRequired)
                        this.BeginInvoke(new Action(()=> UpdateFields(changedform,null)));
                    else UpdateFields(changedform,null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
              
            }
        }

        public new void Show(DockPanel dock)
        {
            if (!Visible)
                base.Show(dock, DockState.Document);
            else Focus();
            BringToFront();
        }

        public void UpdateFields(ProductieFormulier formulier, Bewerking selected)
        {
            if (Disposing || IsDisposed) return;
            if (formulier == null)
                return;
            try
            {
                Formulier = formulier;
                var bws = Formulier.Bewerkingen.Where(x =>
                    x.IsAllowed() && x.State != ProductieState.Verwijderd).ToList();
                if (bws.Count == 0)
                {
                    DetachEvents();
                    Close();
                }
                else
                {
                    Text = $"[{Formulier.ProductieNr}]";
                    productieForm1.SetParent(Formulier);
                    if (selected != null)
                        productieForm1.SelectedBewerking = selected;
                    var plekken = productieForm1.SelectedBewerking?.WerkPlekken;
                    if (plekken is {Count: > 0})
                        TabText = $"{string.Join(",",plekken.Select(x=> x.Naam))} [{Formulier.ProductieNr}, {Formulier.ArtikelNr}]";
                    else TabText = $"[{Formulier.ProductieNr}, {Formulier.ArtikelNr}]";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void StartProductie_Shown(object sender, EventArgs e)
        {
            try
            {
                BringToFront();
                Focus();
                InitEvents();
                UpdateFields(Formulier, _selected);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void InitEvents()
        {
            Manager.OnSettingsChanged += _manager_OnSettingsChanged;
            Manager.OnFormulierChanged += Formulier_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
            productieForm1.OnCloseButtonPressed += ProductieForm1_OnCloseButtonPressed;
        }

        private void DetachEvents()
        {
            Manager.OnSettingsChanged -= _manager_OnSettingsChanged;
            Manager.OnFormulierChanged -= Formulier_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
            productieForm1.OnCloseButtonPressed -= ProductieForm1_OnCloseButtonPressed;
        }

        private void _manager_OnSettingsChanged(object instance, UserSettings settings, bool init)
        {
            if (this.Disposing || this.IsDisposed) return;
            var user = Manager.LogedInGebruiker;
            if (user is not {AccesLevel: > AccesType.AlleenKijken})
            {
                DetachEvents();
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(Close));
                else
                    Close();
            }
            else productieForm1.OnSettingChanged(instance, settings, init);
        }

        private void StartProductie_FormClosing(object sender, FormClosingEventArgs e)
        {
            DetachEvents();
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    if (Disposing || IsDisposed || Formulier?.ProductieNr == null) return;
                    if (!string.Equals(id, Formulier.ProductieNr, StringComparison.CurrentCultureIgnoreCase)) return;
                    DetachEvents();
                    this.Close();
                }));
            }
            catch (Exception e)
            {
            }
        }
    }
}