using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class AanbevolenPersonenForm : MetroBaseForm
    {
        private readonly Bewerking _Bewerking;

        private readonly ProductieFormulier _Form;

        private bool _iswaiting;

        public AanbevolenPersonenForm()
        {
            InitializeComponent();
        }

        public AanbevolenPersonenForm(Bewerking bew) : this()
        {
            _Bewerking = bew;
        }

        public AanbevolenPersonenForm(ProductieFormulier form) : this()
        {
            _Form = form;
        }

        public async void InitBewerking(Bewerking bew)
        {
            if (bew == null) return;
            try
            {
                SetWaitUI();
                var aantalpers = 0;
                var aantalwp = 0;
                var pershtml = await bew.GetAanbevolenPersoneelHtml(true, aantalpers);
                var wphtml = await bew.GetAanbevolenWerkplekHtml(true, aantalwp);
                aantalpers = pershtml.Value;
                aantalwp = wphtml.Value;
                var aantal = aantalpers + aantalwp;
                if (aantal == 0)
                    ThrowNoAanbevelingen();
                xpersHtmlPanel.Text = pershtml.Key;
                xwerkplekkenHtmlPanel.Text = wphtml.Key;
                var x1 = aantalpers == 1 ? "persoon" : "personen";
                var x2 = aantalwp == 1 ? "werkplek" : "werkplekken";
                metroTabPage1.Text = $"{aantalwp} Aanbevolen {x2}";
                metroTabPage2.Text = $"{aantalpers} Aanbevolen {x1}";
                UpdateStatus(bew, aantal);
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Geen Aanbevelingen");
                Close();
            }

            _iswaiting = false;
            Invalidate();
        }

        public async void InitFormulier(ProductieFormulier form)
        {
            if (form?.Bewerkingen == null) return;
            try
            {
                SetWaitUI();
                var pershtml = await form.GetAanbevolenPersoneelHtml();
                var wphtml = await form.GetAanbevolenWerkplekkenHtml();
                var aantal = pershtml.Value + wphtml.Value;
                if (aantal == 0)
                    ThrowNoAanbevelingen();
                xpersHtmlPanel.Text = pershtml.Key;
                xwerkplekkenHtmlPanel.Text = wphtml.Key;
                var x1 = pershtml.Value == 1 ? "persoon" : "personen";
                var x2 = wphtml.Value == 1 ? "werkplek" : "werkplekken";
                metroTabPage1.Text = $"{wphtml.Value} Aanbevolen {x2}";
                metroTabPage2.Text = $"{pershtml.Value} Aanbevolen {x1}";
                UpdateStatus(form, aantal);
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Geen Aanbevelingen");
                Close();
            }

            _iswaiting = false;
            Invalidate();
        }

        private void UpdateStatus(IProductieBase prod, int count)
        {
            if (prod == null)
            {
                Text = "";
            }
            else
            {
                var x1 = count == 0 ? "Geen aanbevelingen" : "Aanbevelingen";
                Text = $"{x1} voor {prod.ProductieNr}-{prod.ArtikelNr} {prod.Omschrijving}";
            }

            Invalidate();
        }

        private void SetWaitUI()
        {
            if (_iswaiting) return;
            _iswaiting = true;
            Task.Run(async () =>
            {
                try
                {
                    var valid = false;
                    Invoke(new MethodInvoker(() => valid = !IsDisposed));
                    if (!valid) return;
                    Invoke(new MethodInvoker(() => { xloadinglabel.Visible = true; }));
                    var cur = 0;
                    var xwv = "Aanbevelingen Zoeken.";
                    //var xcurvalue = xwv;
                    var tries = 0;
                    while (_iswaiting && tries < 200)
                    {
                        if (!Visible) continue;
                        if (cur > 5) cur = 0;
                        var curvalue = xwv.PadRight(xwv.Length + cur, '.');
                        //xcurvalue = curvalue;
                        BeginInvoke(new MethodInvoker(() =>
                        {
                            xloadinglabel.Text = curvalue;
                            xloadinglabel.Invalidate();
                        }));
                        Application.DoEvents();

                        await Task.Delay(350);
                        Application.DoEvents();
                        tries++;
                        cur++;
                        if (IsDisposed || Disposing) break;
                        if (!Visible) continue;
                        Invoke(new MethodInvoker(() => valid = !IsDisposed));
                        if (!valid) break;
                    }

                    if (!IsDisposed && !Disposing && Visible)
                        Invoke(new MethodInvoker(() => { xloadinglabel.Visible = false; }));
                }
                catch (Exception e)
                {
                }
            });
        }

        private void ThrowNoAanbevelingen()
        {
            throw new Exception("Er zijn geen aanbevelingen");
        }

        private void AanbevolenPersonenForm_Shown(object sender, EventArgs e)
        {
            if (_Bewerking != null)
                InitBewerking(_Bewerking);
            else if (_Form != null)
                InitFormulier(_Form);
        }
    }
}