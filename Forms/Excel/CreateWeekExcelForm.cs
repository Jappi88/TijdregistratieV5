using ProductieManager.Properties;
using ProductieManager.Rpm.ExcelHelper;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Excel
{
    public partial class CreateWeekExcelForm : MetroFramework.Forms.MetroForm 
    {
        public CreateWeekExcelForm()
        {
            InitializeComponent();
            xweeknr.Value = DateTime.Now.GetWeekNr();
            xjaar.Value = DateTime.Now.Year;
            SetFieldInfo();
        }

        private void SetFieldInfo()
        {
            var selected = Manager.Opties?.ExcelColumns?.FirstOrDefault(x => x.IsUsed("ExcelColumns") && x.IsExcelSettings);
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

        private void button1_Click(object sender, System.EventArgs e)
        {
            var xf = new ExcelOptiesForm();
            xf.LoadOpties(Manager.Opties, "ExcelColumns", true);
            if (xf.ShowDialog() == DialogResult.OK)
            {
                Manager.UpdateExcelColumns(xf.Settings, true);
                Manager.Opties.Save("ExcelColumns Aangepast!", false, false, true);
                SetFieldInfo();
            }
        }

        private async void xOpslaan_Click(object sender, System.EventArgs e)
        {
            if (IsRunning())
            {
                StopWait();
                return;
            }
            var selected = Manager.Opties?.ExcelColumns?.FirstOrDefault(x => x.IsUsed("ExcelColumns"));
            if (selected == null)
            {
                var xf = new ExcelOptiesForm();
                xf.LoadOpties(Manager.Opties, "ExcelColumns", true);
                xf.IsSelectDialog = true;
                if (xf.ShowDialog() == DialogResult.OK)
                {
                    if (Manager.Opties != null)
                    {
                        Manager.UpdateExcelColumns(xf.Settings, true);
                        await Manager.Opties.Save("ExcelColumns Aangepast!");
                        SetFieldInfo();
                        selected = Manager.Opties?.ExcelColumns?.FirstOrDefault(x => x.IsUsed("ExcelColumns"));
                    }

                }
            }
            if (selected == null) return;
            var ofd = new SaveFileDialog
            {
                Title = "creëer Productie Overzicht",
                Filter = "Xlsx Bestand|*.Xlsx",
                FileName = $"WeekOverzicht {xweeknr.Value}_{xjaar.Value}"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StartWait();
                if (await ExcelWorkbook.CreateDagelijksProductieOverzicht((int) xweeknr.Value, (int) xjaar.Value,
                        ofd.FileName) && File.Exists(ofd.FileName) && xopenexcel.Checked)
                    Process.Start(ofd.FileName);
                StopWait();
            }
        }

        private void xsluiten_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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
            {
                xOpslaan.Text = "Opslaan";
                xOpslaan.Image = Resources.diskette_save_saveas_1514;
                button1.Enabled = true;
            }
        }

        public bool IsRunning()
        {
            return _isbusy;
        }
    }
}
