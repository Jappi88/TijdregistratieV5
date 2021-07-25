using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;

namespace ProductieManager.Forms
{
    public partial class CreateExcelForm : MetroFramework.Forms.MetroForm
    {
        public List<Bewerking> Bewerkingen { get; private set; }
        public CreateExcelForm()
        {
            InitializeComponent();
            xvanafdate.Value = DateTime.Now.Subtract(TimeSpan.FromDays(365));
        }

        public CreateExcelForm(List<Bewerking> bewerkingen) :this()
        {
            Bewerkingen = bewerkingen;
        }

        public CreateExcelForm(List<ProductieFormulier> producties):this()
        {
            if (producties == null || producties.Count == 0) return;
            Bewerkingen = new List<Bewerking>();
            for (int i = 0; i < producties.Count; i++)
            {
                var prod = producties[i];
                if (prod?.Bewerkingen == null || prod.Bewerkingen.Length == 0) continue;
                foreach(var bw in prod.Bewerkingen)
                    if (bw != null && bw.IsAllowed())
                        Bewerkingen.Add(bw);
            }
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

        private void SetFieldInfo()
        {
            if (Bewerkingen != null)
            {
                var x1 = Bewerkingen.Count == 1 ? "bewerking" : "bewerkingen";
                var x2 = Bewerkingen.Count == 1 ? "is" : "zijn";
                xinfolabel.Text = $"{Bewerkingen.Count} {x1} {x2} geselecteerd.\n\n" +
                                  $"Creeër een excel overzicht van de bewerkingen.";
                xinfolabel.Visible = true;
            }
            else xinfolabel.Visible = false;
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private async void xOpslaan_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog
            {
                Title = "creëer Productie Overzicht",
                Filter = "Xlsx Bestand|*.Xlsx",
                FileName = DateTime.Now.ToString("g").Replace(" ","_").Replace("/","").Replace("-","_").Replace(":","")
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StartWait();
                var producties = Bewerkingen?? await Manager.Database.GetAllBewerkingen(true,true);
                TijdEntry te = Bewerkingen == null ? new TijdEntry(xvanafdate.Value, xtotdate.Value, null) : null;
                var file = await ExcelWorkbook.CreateWeekOverzicht(te, producties, xcreeroverzicht.Checked, ofd.FileName,$"Overzicht vanaf {xvanafdate.Value} t/m {xtotdate.Value}");
                if (file != null && File.Exists(file) && xopenexcel.Checked)
                    Process.Start(file);
                StopWait();
            }
        }

        private bool _isbusy = false;
        private void StartWait()
        {
            if (_isbusy) return;
            _isbusy = true;
            xOpslaan.Enabled = false;
            xbezig.Visible = true;
            Task.Run(async () =>
            {
                int count = 0;
                while (_isbusy)
                {
                    var xvalue = ("Overzicht Aanmaken").PadRight(count + 19, '.');
                    xbezig.Invoke(new Action(() => xbezig.Text = xvalue));
                    count++;
                    if (count > 4)
                        count = 0;
                    await Task.Delay(1000);
                }

                if (!this.IsDisposed)
                {
                    xbezig.Invoke(new Action(() => xbezig.Text = "Overzicht Aangemaakt!"));
                    await Task.Delay(2000);
                    xbezig.Invoke(new Action(() => xbezig.Visible = false));
                }
            });
        }

        private void StopWait()
        {
            _isbusy = false;
            if (!this.IsDisposed)
                xOpslaan.Enabled = true;
        }

        private void CreateExcelForm_Shown(object sender, EventArgs e)
        {
            SetFieldInfo();
        }
    }
}
