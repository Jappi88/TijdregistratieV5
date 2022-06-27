using ProductieManager.Properties;
using ProductieManager.Rpm.ExcelHelper;
using ProductieManager.Rpm.Settings;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class CreateExcelForm : Forms.MetroBase.MetroBaseForm
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
            var selected = Manager.ListLayouts?.GetAlleLayouts()?.FirstOrDefault(x => x.IsUsed("ExcelColumns") && x.IsExcelSettings);
            if (selected != null)
            {
                xcolumnsStatusLabel.Text = $@"Opties Geselecteerd: {selected.Name}";
                xcolumnsStatusLabel.ForeColor = Color.DarkGreen;
            }
            else
            {
                xcolumnsStatusLabel.Text = $@"Geen Opties Geselecteerd!";
                xcolumnsStatusLabel.ForeColor = Color.DarkRed;
            }
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private async void xOpslaan_Click(object sender, EventArgs e)
        {
            if (IsRunning(null))
            {
                StopWait();
                return;
            }

            var xlists = Manager.ListLayouts?.GetAlleLayouts()??new List<ExcelSettings>();
            var selected = xlists.FirstOrDefault(x => x.IsUsed("ExcelColumns"));
            if (selected == null)
            {
                var xf = new ExcelOptiesForm();
                xf.LoadOpties("ExcelColumns",true);
                xf.IsSelectDialog = true;
                if (xf.ShowDialog(this) == DialogResult.OK)
                {
                    if (Manager.Opties != null)
                    {
                        Manager.UpdateExcelColumns(xf.Settings,true,true,true);
                        SetFieldInfo();
                        selected = xlists.FirstOrDefault(x => x.IsUsed("ExcelColumns"));
                    }
                   
                }
            }
            if (selected == null) return;
            var ofd = new SaveFileDialog
            {
                Title = "creëer Productie Overzicht",
                Filter = "Xlsx Bestand|*.Xlsx",
                FileName = "ProductieOverzicht_" + DateTime.Now.ToString("g").Replace(" ","_").Replace("/","").Replace("-","_").Replace(":","")
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StartWait();
                try
                {
                    var producties = Bewerkingen?? await Manager.Database.GetAllBewerkingen(true,true, true);
                    TijdEntry te = Bewerkingen == null ? new TijdEntry(xvanafdate.Value, xtotdate.Value, null) : null;
                    var file = await ExcelWorkbook.CreateWeekOverzicht(te, producties, xcreeroverzicht.Checked, ofd.FileName,$"Overzicht vanaf {xvanafdate.Value} t/m {xtotdate.Value}",IsRunning);
                    if (file != null && File.Exists(file) && xopenexcel.Checked)
                        Process.Start(file);
                }
                catch (Exception exception)
                {
                    XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
                }
                StopWait();
            }
        }

        private bool _isbusy = false;
        private void StartWait()
        {
            if (_isbusy) return;
            _isbusy = true;
            xOpslaan.Text = "Stoppen";
            xOpslaan.Image = Resources.stop_red256_24890;
            button1.Enabled = false;
            xbezig.Visible = true;
            Task.Run(async () =>
            {
                int count = 0;
                while (_isbusy)
                {
                    var xvalue = ("Overzicht Aanmaken").PadRight(count + 19, '.');
                    //xbezig.Invoke(new Action(() => xbezig.Text = xvalue));
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
            {
                xOpslaan.Text = "Opslaan";
                xOpslaan.Image = Resources.diskette_save_saveas_1514;
                button1.Enabled = true;
            }
        }

        public bool IsRunning(ProgressArg arg)
        {
            if (arg?.Message != null)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    xbezig.Text = arg.Message;
                    xbezig.Invalidate();
                }));
            }

            return _isbusy;
        }

        private void CreateExcelForm_Shown(object sender, EventArgs e)
        {
            SetFieldInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var xf = new ExcelOptiesForm();
            xf.LoadOpties( "ExcelColumns",true);
            if (xf.ShowDialog(this) == DialogResult.OK)
            {
                Manager.UpdateExcelColumns(xf.Settings,true,true,true);
                SetFieldInfo();
            }
        }

        private void CreateExcelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopWait();
        }

        private void xcolumnsStatusLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
