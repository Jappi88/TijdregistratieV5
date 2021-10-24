using System;
using System.Drawing;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Forms;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms
{
    public partial class NewStoringForm : MetroFramework.Forms.MetroForm
    {
        private Storing _onderbreking;

        public NewStoringForm(WerkPlek plek) : this(plek, null)
        {
        }

        public NewStoringForm(WerkPlek plek, Storing storing)
        {
            InitializeComponent();
            IsEdit = storing != null;
            //pak all soort storingen
            Text = $@"Alle Onderbrekeningen van {plek.Path}";
            var storingen = Functions.LoadSoortStoringen("SoortStilstanden.txt").Select(x => (object) x).ToArray();
            xsoortstoringen.Items.AddRange(storingen);
            SetOnderbreking(plek, storing);
        }

        public bool IsEdit { get; }

        public WerkPlek Plek { get; private set; }

        public Storing Onderbreking
        {
            get => _onderbreking;
            set => SetOnderbreking(Plek, value);
        }

        public void SetOnderbreking(WerkPlek plek, Storing storing)
        {
            Plek = plek;
            _onderbreking = storing == null ? new Storing() : storing.CreateCopy();
            _onderbreking.WerkRooster = plek.Tijden.WerkRooster;
            _onderbreking.Path = plek.Path;
            _onderbreking.WerkPlek = plek.Naam;
            _onderbreking.Plek = plek;
            var x = _onderbreking;
            xisbeeindigd.Image = Onderbreking.IsVerholpen ? Resources.check_1582 : Resources.delete_1577;
            if (!string.IsNullOrEmpty(x.StoringType))
                xsoortstoringen.Text = x.StoringType;
            else
            {
                var xitem = xsoortstoringen.Items.Cast<string>().FirstOrDefault(s => s.ToLower().Contains("ombouw"));
                xsoortstoringen.SelectedItem = xitem;
                if (xsoortstoringen.SelectedIndex < 0 && xsoortstoringen.Items.Count > 0)
                    xsoortstoringen.SelectedIndex = 0;
            }

            xnaammelder.Text = x.GemeldDoor;
            xnaambeeindiger.Text = x.VerholpenDoor;
            xstartstoring.SetValue(x.Gestart);
            xeindestoring.SetValue(x.Gestopt);
            xeindestoring.Enabled = Onderbreking.IsVerholpen;
            xnaambeeindiger.Enabled = Onderbreking.IsVerholpen;
            xomschrijving.Text = x.Omschrijving;
            xactie.Text = x.Oplossing;
            UpdateStatusLabel();
            SetTextFieldEnable();
        }

        private void UpdateStatusLabel()
        {
            if (IsEdit)
            {
                if (Onderbreking == null)
                    this.Text = $"Wijzig onderbreking van {Plek.Path}";
                else this.Text = $"Wijzig onderbreking van {Plek.Path} [{Onderbreking.GetTotaleTijd()} uur]";
            }
            else
            {
                if (Onderbreking == null)
                    this.Text = $"Voeg nieuwe onderbreking toe aan {Plek.Path}";
                else
                    this.Text = $"Voeg nieuwe onderbreking toe aan {Plek.Path} [{Onderbreking.GetTotaleTijd()} uur]";
            }

            if (Onderbreking != null)
                xtijdlabel.Text = $"{Onderbreking.GetTotaleTijd()} Uur Aan {Onderbreking.StoringType}";
            else xtijdlabel.Text = "";
            this.Invalidate();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            xisbeeindigd.BackColor = Color.AliceBlue;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            xisbeeindigd.BackColor = Color.Transparent;
        }

        private void xnaammelder_TextChanged(object sender, EventArgs e)
        {
            if (Onderbreking != null)
                Onderbreking.GemeldDoor = xnaammelder.Text;
        }

        private void xsoortstoringen_TextChanged(object sender, EventArgs e)
        {
            SetTextFieldEnable();
        }

        private void SetTextFieldEnable()
        {
            if (Onderbreking != null)
            {
                Onderbreking.StoringType = xsoortstoringen.Text;
                xomschrijving.Enabled = MustEditTextFields();
                xactie.Enabled = Onderbreking.IsVerholpen;
                UpdateStatusLabel();
            }
        }

        private bool MustEditTextFields()
        {
            var xvalue = xsoortstoringen.Text.ToLower().Trim();
            var enabled = !string.IsNullOrEmpty(xvalue) && !xvalue.Contains("ombouw") &&
                          !xvalue.Contains("zaagwissel") &&
                          !xvalue.Contains("schoonmaak");
            return enabled;
        }

        private void xomschrijving_TextChanged(object sender, EventArgs e)
        {
            if (Onderbreking != null)
                Onderbreking.Omschrijving = xomschrijving.Text;
        }

        private void xactie_TextChanged(object sender, EventArgs e)
        {
            if (Onderbreking != null)
                Onderbreking.Oplossing = xactie.Text;
        }

        private void xnaambeeindiger_TextChanged(object sender, EventArgs e)
        {
            if (Onderbreking != null)
                Onderbreking.VerholpenDoor = xnaambeeindiger.Text;
        }

        private void xstartstoring_ValueChanged(object sender, EventArgs e)
        {
            if (Onderbreking != null)
            {
                Onderbreking.Gestart = xstartstoring.Value;
                UpdateStatusLabel();
            }
        }

        private void xeindestoring_ValueChanged(object sender, EventArgs e)
        {
            if (Onderbreking != null)
            {
                Onderbreking.Gestopt = xeindestoring.Value;
                UpdateStatusLabel();
            }
        }

        private void xisbeeindigd_Click(object sender, EventArgs e)
        {
            if (Onderbreking != null)
            {
                Onderbreking.IsVerholpen = !Onderbreking.IsVerholpen;
                xisbeeindigd.Image = Onderbreking.IsVerholpen ? Resources.check_1582 : Resources.delete_1577;
                xeindestoring.Enabled = Onderbreking.IsVerholpen;
                xnaambeeindiger.Enabled = Onderbreking.IsVerholpen;
                SetTextFieldEnable();
                if (Onderbreking.IsVerholpen)
                    xeindestoring.Value = DateTime.Now;
            }
        }

        private void xok_Click(object sender, EventArgs e)
        {
            if (xnaammelder.Text.Trim().Replace(" ", "").Length < 3)
                XMessageBox.Show("Ongeldige melder naam!\n\nVul in een geldige naam en probeer het opnieuw.",
                    "Ongeldige Naam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (xsoortstoringen.Text.Trim().Replace(" ", "").Length < 4)
                XMessageBox.Show("Ongeldige onderbreking type!\n\nVul in een geldige type en probeer het opnieuw.",
                    "Ongeldige Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (MustEditTextFields() && xomschrijving.Text.Trim().Replace(" ", "").Length < 4)
                XMessageBox.Show("Ongeldige omschrijving!\n\nVul in een geldige omschrijving en probeer het opnieuw.",
                    "Ongeldige Omschrijving", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (Onderbreking.IsVerholpen && xnaambeeindiger.Text.Trim().Replace(" ", "").Length < 3)
                XMessageBox.Show(
                    "Ongeldige beëindiger naam!\n\nVul in een geldige naam in van diegene die het heeft verholpen en probeer het opnieuw.",
                    "Ongeldige Naam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (Onderbreking.IsVerholpen && MustEditTextFields() && xactie.Text.Trim().Replace(" ", "").Length < 4)
                XMessageBox.Show(
                    "Ongeldige actie omschrijving!\n\nVul in een geldige actie omschrijving en probeer het opnieuw.",
                    "Ongeldige omschrijving", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if(Onderbreking.IsVerholpen && Onderbreking.GetTotaleTijd() <= 0)
                XMessageBox.Show(
                    "Onderbreking is verholpen, maar de totale tijd is 0 uur...\n" +
                    "Vul in een geldige tijd en probeer het opnieuw",
                    "Ongeldige Tijd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                SaveStoringen();
                DialogResult = DialogResult.OK;
            }
        }

        private void SaveStoringen()
        {
            try
            {
                string value = xsoortstoringen.Text.Trim();
                var xcur = xsoortstoringen.Items.Cast<string>().ToList();
                if (value.Length > 2)
                {
                   
                    if (!xcur.Any(x => string.Equals(x, value, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        xcur.Insert(0, value);
                        xsoortstoringen.Items.Insert(0, value);
                    }
                }
                Functions.SaveStoringen(xcur, "SoortStilstanden.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void NewStoringForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
        }

        private void NewStoringForm_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            var prodnr = Plek?.Werk?.ProductieNr;
            if (IsDisposed || Plek == null || id == null || !string.Equals(id, prodnr)) return;
            this.BeginInvoke(new MethodInvoker(this.Close));
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            var prodnr = Plek?.Werk?.ProductieNr;
            if (Plek == null || changedform == null || !string.Equals(changedform.ProductieNr, prodnr)) return;

            var plekken = changedform.GetWerkPlekken();
            var xplek = plekken.FirstOrDefault(x => x.Equals(Plek));
            if (xplek != null)
            {
                Plek = xplek;
            }
            else
            {
                this.BeginInvoke(new MethodInvoker(this.Close));
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (xsoortstoringen.SelectedIndex < 0)
                e.Cancel = true;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xsoortstoringen.SelectedItem != null)
            {
                xsoortstoringen.Items.Remove(xsoortstoringen.SelectedItem);
            }
        }
    }
}