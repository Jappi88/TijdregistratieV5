using BrightIdeasSoftware;
using ProductieManager.Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class Indeling : Forms.MetroBase.MetroBaseForm
    {
        private Bewerking _bew;

        public Indeling()
        {
            InitializeComponent();

            xwerkplekimages.Images.Add(Resources.iconfinder_technology);
            xwerkplekimages.Images.Add(Resources.iconfinder_technology.CombineImage(Resources.check_1582, 2));

            imageList1.Images.Add(Resources.user_customer_person_13976);
            imageList1.Images.Add(Resources.user_customer_person_13976.CombineImage(Resources.check_1582, 2));
           
            ((OLVColumn) xwerkplekken.Columns[0]).ImageGetter = WerkplekImageGet;
            ((OLVColumn)xwerkplekken.Columns[1]).AspectGetter = WerkplekActiefGet;
            ((OLVColumn)xwerkplekken.Columns[2]).AspectGetter = WerkplekRoosterGet;
            ((OLVColumn)xwerkplekken.Columns[4]).AspectGetter = WerkplekTijdGewerktGet;

            ((OLVColumn)xshiftlist.Columns[0]).ImageGetter = ImageGet;
            ((OLVColumn)xshiftlist.Columns[2]).AspectGetter = TijdGewerktGet;
            ((OLVColumn)xshiftlist.Columns[4]).AspectGetter = TijdGestartGet;
            ((OLVColumn)xshiftlist.Columns[5]).AspectGetter = TijdGestoptGet;
            ((OLVColumn)xshiftlist.Columns[6]).AspectGetter = RoosterGet;

            InitAutoInfilTextbox();
            //xnaampersoneel.TextChanged += xnaampersoneel_TextChanged;
            //xtijdgestart.ValueChanged += xtijdgestart_ValueChanged;
            //xtijdgestopt.ValueChanged += xtijdgestopt_ValueChanged;
        }

        public Indeling(ProductieFormulier parent, Bewerking bew) : this()
        {
            Formulier = parent?.CreateCopy();
            LoadIndeling(Formulier, bew);
        }

        public Indeling(ProductieFormulier form) : this()
        {
            Formulier = form?.CreateCopy();
            LoadIndeling(Formulier, null);
        }

        public Indeling(WerkPlek plek) : this()
        {
            Formulier = plek.Werk.Parent?.CreateCopy();
            LoadIndeling(Formulier, plek.Werk, plek.Naam);
        }

        public ProductieFormulier Formulier { get; set; }

        public Bewerking Bewerking
        {
            get => GetCurrentBewerking();
            set => _bew = value;
        }

        private async void InitAutoInfilTextbox()
        {
            var pers = await Manager.Database.GetAllPersoneel();
            xnaampersoneel.AutoCompleteSource = AutoCompleteSource.CustomSource;
            xnaampersoneel.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            xnaampersoneel.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            xnaampersoneel.AutoCompleteCustomSource.AddRange(pers.Select(x => x.PersoneelNaam).ToArray());
        }

        public object WerkplekImageGet(object sender)
        {
            if (sender is WerkPlek wp)
            {
                if (wp.IsActief())
                    return 1;
                return 0;
            }

            return 0;
        }

        public object WerkplekActiefGet(object sender)
        {
            if (sender is WerkPlek wp)
            {
                if (wp.IsActief())
                    return "Ja";
                return "Nee";
            }

            return "N.V.T.";
        }

        public object WerkplekRoosterGet(object sender)
        {
            if (sender is WerkPlek wp)
            {
                if (wp.Tijden?.WerkRooster != null && wp.Tijden.WerkRooster.IsCustom())
                    return "Eigen";
                return "Standaard";
            }

            return "Geen Idee";
        }
        public object WerkplekTijdGewerktGet(object sender)
        {
            if (sender is WerkPlek wp)
            {
                return wp.TijdAanGewerkt();
            }

            return "Geen Idee";
        }

        public object RoosterGet(object sender)
        {
            if (sender is Personeel per)
            {
                if (per.WerkRooster != null && per.WerkRooster.IsCustom())
                    return "Eigen";
                return "Standaard";
            }

            return "Geen Idee";
        }

        public object ImageGet(object sender)
        {
            if (sender is Personeel per)
            {
                var lv = xshiftlist.SelectedItems.Cast<OLVListItem>().FirstOrDefault(x => x.RowObject.Equals(per));

                var klus = GetCurrentKlus(per, false);
                if (klus != null)
                {
                    if (lv != null)
                        lv.Checked = klus.IsActief;
                    return klus.IsActief ? 1 : 0;
                }

                if (lv != null)
                    lv.Checked = false;
            }

            return 0;
        }

        public object TijdGestartGet(object sender)
        {
            if (sender is Personeel per)
            {
                var klus = GetCurrentKlus(per, false);
                var xent = klus?.GetAvailibleTijdEntry();
                if (xent != null) return xent.Start;
            }

            return "N.V.T";
        }

        public object TijdGestoptGet(object sender)
        {
            if (sender is Personeel per)
            {
                var klus = GetCurrentKlus(per, false);
                var xent = klus?.GetAvailibleTijdEntry();
                if (xent != null) return xent.Stop;
            }

            return "N.V.T";
        }

        public object TijdGewerktGet(object sender)
        {
            if (sender is Personeel per)
            {
                var klus = GetCurrentKlus(per, false);
                var wp = GetWerkPlek(per, false);
                if (klus != null && wp != null)
                    return Math.Round(
                        klus.TijdGewerkt(wp.GetStoringen(), klus.Tijden?.WerkRooster).TotalHours,
                        2) + " uur";
            }

            return "N.V.T";
        }

        public void LoadIndeling(ProductieFormulier form, Bewerking bewerking, string werkplek = null)
        {
            var selectedwp = werkplek;
            if (form == null && bewerking != null)
            {
                xbewerking.Items.Add(bewerking.Naam);
                xbewerking.SelectedIndex = 0;
            }
            else if (form != null && form.Bewerkingen.Length > 0)
            {
                string selected = null;
                foreach (var bew in form.Bewerkingen)
                {
                    if (!bew.IsAllowed()) continue;
                    xbewerking.Items.Add(bew.Naam);
                    if (bewerking != null && string.Equals(bew.Naam, bewerking.Naam, StringComparison.CurrentCultureIgnoreCase))
                        selected = bew.Naam;
                }

                if (xbewerking.Items.Count > 0)
                    if (selected != null)
                        xbewerking.SelectedItem = selected;
                    else
                        xbewerking.SelectedIndex = 0;
            }

            if (selectedwp != null && xwerkplekken.Objects != null)
                xwerkplekken.SelectedObject = xwerkplekken.Objects.Cast<WerkPlek>()
                    .FirstOrDefault(x => string.Equals(x.Naam, selectedwp, StringComparison.CurrentCultureIgnoreCase));
            xwerkplekken.SelectedItem?.EnsureVisible();
        }

        private void LoadAfdelingen()
        {
            if (Bewerking != null)
            {
                xindelinggroup.Text = $@"{Bewerking.Naam}";
                Text =
                    $@"{Bewerking.Omschrijving} | ArtikelNr: {Bewerking.ArtikelNr} | ProductieNr: {Bewerking.ProductieNr}";
                LoadWerkPlekken(null);
                LoadShifts();
                xnaampersoneel.Select();
                xnaampersoneel.Focus();
            }
        }

        private void LoadShifts()
        {
            var werkplek = GetCurrentWerkPlek();
            if (werkplek != null)
            {
                var selected = xshiftlist.SelectedObject;
                xshiftlist.SetObjects(werkplek.Personen);
                if (selected == null && werkplek.Personen.Count > 0)
                    xshiftlist.SelectedObject = werkplek.Personen[0];
                else
                    xshiftlist.SelectedObject = selected;
                xshiftlist.SelectedItem?.EnsureVisible();
                string x1 = werkplek.Personen.Count == 1 ? "persoon" : "personen";
                xpersoneelgroep.Text = $@"{werkplek.Naam} Geselecteerd met {werkplek.Personen.Count} {x1}";
                xwerktijdnaarwerkplek.Visible = werkplek.Personen.Count > 0;
            }
            else
            {
                xshiftlist.SetObjects(new List<Personeel>());
                xpersoneelgroep.Text = $@"Geen werkplek geselecteerd";
                xwerktijdnaarwerkplek.Visible = false;
            }

            UpdateUsersButtons();
        }

        private void LoadWerkPlekken(WerkPlek selected)
        {
            var bew = Bewerking;
            if (bew == null)
                return;
            var xselected = selected??xwerkplekken.SelectedObject?? (bew.WerkPlekken.Count > 0? bew.WerkPlekken[0] : null);
            xwerkplekken.SetObjects(bew.WerkPlekken);
            xwerkplekken.SelectedObject = xselected;
            xwerkplekken.SelectedItem?.EnsureVisible();
            LoadShifts();
        }

        private void ClearFields()
        {
            //xnaampersoneel.TextChanged -= xnaampersoneel_TextChanged;
            //xnaampersoneel.Text = "";
            xnaampersoneel.Tag = null;
            xactiefimage.Image = null;
            //xnaampersoneel.TextChanged += xnaampersoneel_TextChanged;
        }

        private void SetFields(Personeel shift)
        {
            var s = shift;
            if (s == null) return;
            if (s.Werkplek != null)
            {
                var xwp = xwerkplekken.Objects.Cast<WerkPlek>()
                    .FirstOrDefault(t => t.Naam.ToLower().StartsWith(s.Werkplek.ToLower()));
                if (xwp != null)
                    xwerkplekken.SelectedObject = xwp;
            }

            if (xwerkplekken.SelectedItem == null && xwerkplekken.Items.Count > 0)
                xwerkplekken.SelectedIndex = 0;
            else xwerkplekken.SelectedItem?.EnsureVisible();
            var klus = GetCurrentKlus(s, false);
            //if (initname)
            //    xnaampersoneel.Text = s.PersoneelNaam.Trim();
            xnaampersoneel.Tag = s;
            if (s.Efficientie == 0)
                s.Efficientie = 100;
            if (klus != null)
            {
                xactiefimage.Image = !klus.IsActief
                    ? null
                    : Resources.check_1582;
            }
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xvoegindeling_Click(object sender, EventArgs e)
        {
            if (xbewerking.SelectedItem == null)
            {
                XMessageBox.Show(this, $"Kies een bewerking eerst.", "Kies Bewerking", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            UpdatePersoneelNaamTextbox();
            var personen = xshiftlist.Objects?.Cast<Personeel>().ToArray();
            if (personen == null || string.IsNullOrEmpty(xnaampersoneel.Text.Trim()) ||
                !(xnaampersoneel.Tag is Personeel xpers && string.Equals(xpers.PersoneelNaam, xnaampersoneel.Text,
                    StringComparison.CurrentCultureIgnoreCase)))
            {
                var pers = KiesPersoneel();
                if (pers?.Length > 0)
                {
                    foreach (var per in pers) per.Klusjes?.Clear();
                    var xwp = GetCurrentWerkPlek();
                    var klusui = new NieuwKlusForm(Formulier, pers, false, false, GetCurrentBewerking(), xwp?.Naam);
                    if (klusui.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var pr in klusui.Persoon)
                        {
                            //klusui.Persoon[0].CopyTo(ref pers[i]);
                            AddShift(pr);
                        }
                    }

                }
            }
            else
            {
                AddNewPersoneel();
            }
        }

        private bool AddNewPersoneel()
        {
            var bew = GetCurrentBewerking();
            var wp = GetCurrentWerkPlek();
           // var wpnaam = ChooseWerkplek(wp?.Naam);
            //if (wpnaam == null || bew == null)
             //   return false;

            var shift = new Personeel
            {
                TijdIngezet = DateTime.Now,
                PersoneelNaam = xnaampersoneel.Text.Trim(),
                WerktAan = bew.Path
            };

            if (xnaampersoneel.Tag is Personeel xshift && string.Equals(xnaampersoneel.Text.Trim(), xshift.PersoneelNaam.Trim(),
                StringComparison.CurrentCultureIgnoreCase))
            {
                shift.Efficientie = xshift.Efficientie;
                shift.PersoneelNaam = xshift.PersoneelNaam;
                shift.WerkRooster = xshift.WerkRooster;
                shift.VrijeDagen = xshift.VrijeDagen;
                shift.IsUitzendKracht = xshift.IsUitzendKracht;
            }
            else
            {
                return false;
            }

            var klusui = new NieuwKlusForm(Formulier, shift, false, false, bew, wp?.Naam);
            if (klusui.ShowDialog() == DialogResult.OK)
            {
                foreach (var pr in klusui.Persoon)
                {
                    //klusui.Persoon[0].CopyTo(ref pers[i]);
                    AddShift(pr);
                }

                return true;
            }

            return false;
        }

        private Bewerking GetCurrentBewerking()
        {
            var bew = _bew;
            if (Formulier?.Bewerkingen != null && xbewerking.SelectedItem != null)
                bew = Formulier.Bewerkingen.FirstOrDefault(x =>
                    string.Equals(x.Naam, xbewerking.SelectedItem.ToString(),
                        StringComparison.CurrentCultureIgnoreCase));
            return bew;
        }

        private Klus GetCurrentKlus(Personeel pers, bool createnew)
        {
            if (pers == null)
                return null;
            var klus = pers.Klusjes.GetKlus(pers.WerktAan + "\\" + pers.Werkplek);
            if (klus == null && createnew)
            {
                var wp = GetWerkPlek(pers, true);
                if (wp == null) return null;
                klus = new Klus(pers, wp);
                pers.ReplaceKlus(klus);
            }

            return klus;
        }

        private WerkPlek GetCurrentWerkPlek()
        {
            return xwerkplekken.SelectedObject as WerkPlek;
        }

        private Personeel GetCurrentPersoon()
        {
            return xshiftlist.SelectedObject as Personeel;
        }

        private WerkPlek GetWerkPlek(Personeel pers, bool createnew)
        {
            if (pers?.Werkplek == null && pers?.WerktAan == null)
                return null;
            var bew = Formulier.Bewerkingen.FirstOrDefault(x => string.Equals(x.Path, pers.WerktAan, StringComparison.CurrentCultureIgnoreCase));
            if (bew == null)
                return null;
            var wp = bew.WerkPlekken.FirstOrDefault(x => string.Equals(x.Naam, pers.Werkplek, StringComparison.CurrentCultureIgnoreCase));
            if (wp == null && createnew)
            {
                wp = new WerkPlek(pers, pers.Werkplek, bew);
                bew.WerkPlekken.Add(wp);
            }

            return wp;
        }

        private bool AddShift(Personeel shift)
        {
            if (shift?.WerktAan == null)
                return false;
            var werk = Formulier.Bewerkingen.FirstOrDefault(x =>
                string.Equals(x.Path, shift.WerktAan, StringComparison.CurrentCultureIgnoreCase));
            if (werk != null)
            {
                var wp = werk.WerkPlekken.FirstOrDefault(x =>
                    string.Equals(x.Naam, shift.Werkplek, StringComparison.CurrentCultureIgnoreCase));
                if (wp == null)
                {
                    wp = new WerkPlek(shift, shift.Werkplek, werk);
                    werk.WerkPlekken.Add(wp);
                }
                else
                {
                    if (!wp.Personen.Any(x => string.Equals(x.PersoneelNaam, shift.PersoneelNaam,
                        StringComparison.CurrentCultureIgnoreCase)))
                        wp.AddPersoon(shift, werk);
                }

                xbewerking.SelectedItem = shift.WerktAan.Split('\\').Last();
                if (xbewerking.SelectedItem == null && xbewerking.Items.Count > 0)
                    xbewerking.SelectedIndex = 0;
                LoadWerkPlekken(wp);
                LoadShifts();
                xshiftlist.SelectedObject = shift;
                xshiftlist.SelectedItem?.EnsureVisible();
                return true;
            }
            return false;
        }

        private void xverwijder_Click(object sender, EventArgs e)
        {
            if (xshiftlist.SelectedObjects.Count > 0)
            {
                var pers = xshiftlist.SelectedObjects.Cast<Personeel>().ToArray();
                var naam = pers.Length > 1 ? $"alle {pers.Length} geselecteerde personen" : pers[0].PersoneelNaam;
                if (XMessageBox.Show(this, $"Weet je zeker dat je {naam} wilt verwijderen?", "Verwijderen",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                    for (int i = 0; i < pers.Length; i++)
                    {
                        var per = pers[i];
                        var wp = GetWerkPlek(per, false);
                        if (wp != null)
                        {
                            wp.Personen.Remove(per);
                            xshiftlist.RemoveObject(per);
                            xwerkplekken.RefreshObject(wp);
                            UpdateUsersButtons();
                        }
                    }
            }
        }

        private void xshiftlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUsersButtons();
            if (xshiftlist.SelectedObjects.Count > 0)
                SetFields(xshiftlist.SelectedObjects[0] as Personeel);
            else ClearFields();
        }

        private void UpdateUsersButtons()
        {
            xverwijder.Visible = xshiftlist.SelectedObjects.Count > 0;
            xedituser.Visible = xshiftlist.SelectedObjects.Count == 1;
            xtijdengewerkt.Visible = xshiftlist.SelectedObjects.Count == 1;
            xroosterb.Visible = xshiftlist.SelectedObjects.Count == 1;
            var wp = GetCurrentWerkPlek();
            xwerktijdnaarwerkplek.Visible = wp != null && xshiftlist.Items.Count > 0;
            if (wp != null)
            {
                if (xshiftlist.SelectedObjects.Count == 0)
                {
                    var x1 = xshiftlist.Items.Count == 1
                        ? xshiftlist.Objects.Cast<Personeel>().FirstOrDefault()?.PersoneelNaam
                        : $"{xshiftlist.Items.Count} Medewerkers";
                    xwerktijdnaarwerkplek.Text = $"{x1} Tijden Naar {wp.Naam}";
                    ClearFields();
                }
                else
                {
                    var x1 = xshiftlist.SelectedObjects.Count == 1
                        ? xshiftlist.SelectedObjects.Cast<Personeel>().FirstOrDefault()?.PersoneelNaam
                        : $"{xshiftlist.SelectedObjects.Count} Medewerkers";
                    xwerktijdnaarwerkplek.Text = $"{x1} Tijden Naar {wp.Naam}";
                }
            }
            else ClearFields();
        }

        private Personeel[] KiesPersoneel()
        {
            var pers = new PersoneelsForm(true);
            if (pers.ShowDialog(Bewerking) == DialogResult.OK)
                if (pers.SelectedPersoneel is {Length: > 0})
                {
                    foreach (var per in pers.SelectedPersoneel)
                        per.Klusjes.Clear();
                    return pers.SelectedPersoneel;
                }

            return Array.Empty<Personeel>();
        }

        private readonly Timer _textTimer = new Timer() { Interval = 2000 };

        private void UpdatePersoneelNaamTextbox()
        {
            var pers = Manager.Database.GetPersoneel(xnaampersoneel.Text.Trim()).Result;
            if (pers != null)
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(() => SetFields(pers)));
                else
                    SetFields(pers);
            }
        }

        //private void xnaampersoneel_TextChanged(object sender, EventArgs e)
        //{
        //    _textTimer.Stop();
        //    _textTimer.Start();
        //}

        private void xactiefimage_MouseEnter(object sender, EventArgs e)
        {
            xactiefimage.BackColor = Color.AliceBlue;
            xactieflabel.BackColor = Color.AliceBlue;
        }

        private void xactiefimage_MouseLeave(object sender, EventArgs e)
        {
            xactiefimage.BackColor = Color.Transparent;
            xactieflabel.BackColor = Color.Transparent;
        }

        private void xactiefimage_Click(object sender, EventArgs e)
        {
            SetPersoneelActief();
        }

        private void SetPersoneelActief(Personeel per, bool actief)
        {
            if (per == null) return;
            //var klus = GetCurrentKlus(per, true);
            //if (klus == null) return;
            //klus.ZetActief(actief, Bewerking.State == ProductieState.Gestart);
            //if(actief)
            //{
            var bew = GetCurrentBewerking();
            bew?.ZetPersoneelActief(per.PersoneelNaam, per.Werkplek, actief);
            //}
            xshiftlist.RefreshObject(per);
            xshiftlist.SelectedObject = per;
            xshiftlist.SelectedItem?.EnsureVisible();
            SetFields(per);
        }

        private void SetPersoneelActief()
        {
            if (xshiftlist.SelectedObjects == null) return;
            var selected = xshiftlist.SelectedObjects;
            var actief = ClickImage();
            foreach (var per in xshiftlist.SelectedObjects.Cast<Personeel>()) SetPersoneelActief(per, actief);
            xshiftlist.SelectedObjects = selected;
            xshiftlist.SelectedItem?.EnsureVisible();
        }

        private bool ClickImage()
        {
            xactiefimage.Image = xactiefimage.Image != null ? null : Resources.check_1582;
            return xactiefimage.Image != null;
        }

        private async void xokb_Click(object sender, EventArgs e)
        {
            try
            {
                Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
                Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
                if (await Save())
                    DialogResult = DialogResult.OK;
                else DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xbewerking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Formulier?.Bewerkingen != null)
                if (xbewerking.SelectedItem != null)
                {
                    var b = Formulier.Bewerkingen.FirstOrDefault(x =>
                        x.Naam.ToLower() == (xbewerking.SelectedItem as string)?.ToLower());
                    if (_bew == null || b != null && !b.Equals(_bew))
                    {
                        _bew = b;
                        LoadAfdelingen();
                    }
                }
        }

        public async Task<bool> Save()
        {
            if (Formulier == null)
                return false;
            try
            {
                if (Formulier.Bewerkingen.Length <= 0) return false;
                var xdbs = await Manager.Database.GetAllPersoneel();
                foreach (var bew in Formulier.Bewerkingen)
                {
                    if (!bew.IsAllowed()) continue;
                    var personen = bew.GetPersoneel();
                    foreach (var pers in personen)
                    {
                        pers.Klusjes.RemoveAll(x =>
                            string.Equals(bew.Path, x.Werk, StringComparison.CurrentCultureIgnoreCase) &&
                            !bew.WerkPlekken.Any(w =>
                                string.Equals(x.WerkPlek, w.Naam, StringComparison.CurrentCultureIgnoreCase) &&
                                w.Personen.Any(p => string.Equals(p.PersoneelNaam, pers.PersoneelNaam,
                                    StringComparison.CurrentCultureIgnoreCase))));
                        var xdbpers = xdbs.FirstOrDefault(x =>
                            string.Equals(x.PersoneelNaam, pers.PersoneelNaam,
                                StringComparison.CurrentCultureIgnoreCase));
                        if (pers.IngezetAanKlus(bew, false, out var klusjes))
                        {
                            klusjes.ForEach(x =>
                            {
                                if (bew.State == ProductieState.Gestart)
                                {
                                    if (x.IsActief)
                                        x.Start();
                                    else if(x.Status == ProductieState.Gestart)
                                        x.Stop();
                                }
                                else
                                {
                                    if (x.Status == ProductieState.Gestart)
                                        x.Stop();
                                    x.Status = bew.State;
                                }
                                xdbpers?.ReplaceKlus(x);
                            });
                        }
                        if (xdbpers != null)
                        {
                            //pers.Klusjes.RemoveAll(x => bew.WerkPlekken.All(b => b.Naam.ToLower() != x.WerkPlek.ToLower()));
                            //klusjes.RemoveAll(x => bew.WerkPlekken.All(b => b.Naam.ToLower() != x.WerkPlek.ToLower()));
                            var save = xdbpers.Klusjes.RemoveAll(x =>
                                string.Equals(bew.Path, x.Werk, StringComparison.CurrentCultureIgnoreCase) &&
                                !bew.WerkPlekken.Any(w =>
                                    string.Equals(x.WerkPlek, w.Naam, StringComparison.CurrentCultureIgnoreCase) &&
                                    w.Personen.Any(p => string.Equals(p.PersoneelNaam, xdbpers.PersoneelNaam,
                                        StringComparison.CurrentCultureIgnoreCase)))) > 0;
                            var msg = $"{xdbpers.PersoneelNaam} klusjes aangepast.";

                            await Manager.Database.UpSert(xdbpers, msg);
                        }
                    }
                }


                foreach (var pers in xdbs)
                {
                    var changed = false;
                    foreach (var bew in Formulier.Bewerkingen)
                        if (pers.IngezetAanKlus(bew, false, out var klusjes))
                        {
                            if (!bew.Personen.Any(x => x.Equals(pers)))
                            {
                                klusjes.ForEach(x => pers.Klusjes.Remove(x));
                                changed = true;
                            }


                        }

                    if (changed)
                        await Manager.Database.UpSert(pers,
                            $"Klusjes van '{Formulier.ProductieNr}' zijn verwijderd");
                }

                await Formulier.UpdateForm(true, false, null, "Indeling Aangepast");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void xdeletewerkplek_Click(object sender, EventArgs e)
        {
            if (Bewerking != null && xwerkplekken.SelectedObjects.Count > 0)
            {
                var plekken = xwerkplekken.SelectedObjects.Cast<WerkPlek>().ToArray();
                var naam = plekken.Length > 1 ? $"alle {plekken.Length} geselecteerde werkplekken" : plekken[0].Naam;
                if (XMessageBox.Show(this, $"Weet je zeker dat je {naam} wilt verwijderen?\n\n" +
                                     "Alle medewerkers met gewerkte uren zullen verloren gaan!", "Verwijderen",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < plekken.Length; i++)
                    {
                        var wp = plekken[i];
                        if (Bewerking.WerkPlekken.Remove(wp))
                            xwerkplekken.RemoveObject(wp);
                    }

                    xwpaantal.Value = 0;
                    LoadShifts();
                }
            }
        }

        private void Indeling_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
        }

        private void Indeling_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            var prodnr = Formulier?.ProductieNr;
            if (this.IsDisposed || Formulier == null || id == null || !string.Equals(id, prodnr)) return;
            this.BeginInvoke(new MethodInvoker(this.Close));
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            var prodnr = Formulier?.ProductieNr;
            if (changedform == null || !string.Equals(changedform.ProductieNr, prodnr)) return;
            this.BeginInvoke(new MethodInvoker(() =>
            {
                try
                {
                    var bw = GetCurrentBewerking();
                    Formulier = changedform.CreateCopy();
                    for (int i = 0; i < Formulier.Bewerkingen.Length; i++)
                    {
                        if (string.Equals(bw.Naam, Formulier.Bewerkingen[i].Naam,
                            StringComparison.CurrentCultureIgnoreCase))
                        {
                            Formulier.Bewerkingen[i] = bw;
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }));
        }

        private void xwpaantal_ValueChanged(object sender, EventArgs e)
        {
            var wp = GetCurrentWerkPlek();
            if (wp != null && wp.AantalGemaakt != xwpaantal.Value)
            {
                wp.AantalGemaakt = (int) xwpaantal.Value;
                xwerkplekken.RefreshObject(wp);
            }
        }

        private string ChooseWerkplek(string name)
        {
            var wp = new WerkPlekChooser(Manager.GetWerkplekken(Bewerking.Naam),name);
            if (wp.ShowDialog() == DialogResult.OK) return wp.SelectedName;

            return null;
        }

        private void xaddwerkplek_Click(object sender, EventArgs e)
        {
            var werkplek = ChooseWerkplek(null);

            if (!string.IsNullOrWhiteSpace(werkplek) && werkplek.ToLower().Trim() != "n.v.t")
            {
                if (Bewerking.WerkPlekken.Any(x => x.Naam.ToLower() == werkplek.ToLower()))
                {
                    XMessageBox.Show(
                        this, $"{werkplek} bestaat al...",
                        "Bestaat Al", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    var wp = new WerkPlek(werkplek, Bewerking);
                    Bewerking.WerkPlekken.Add(wp);
                    xwerkplekken.AddObject(wp);
                    xwerkplekken.SelectedObject = wp;
                    xwerkplekken.SelectedItem?.EnsureVisible();
                }
            }
        }

        private void xwerkplekken_SelectedIndexChanged(object sender, EventArgs e)
        {
            var wp = GetCurrentWerkPlek();
            xwpaantal.SetValue(wp?.AantalGemaakt ?? 0);
            if (wp != null)
            {
                xwerkplekgroup.Text = wp.Naam;
                if (wp.Tijden?.WerkRooster != null && wp.Tijden.WerkRooster.IsCustom())
                    xwerkplekken.Text += "[EIGEN ROOSTER]";
            }
            else xwerkplekgroup.Text = "Werkplek / Machine";

            xwerkplektijden.Enabled = xwerkplekken.SelectedObjects.Count == 1;
            xdeletewerkplek.Enabled = xwerkplekken.SelectedObjects.Count > 0;
            xeditwerkplek.Enabled = xwerkplekken.SelectedObjects.Count == 1;
            xplekroosterb.Enabled = xwerkplekken.SelectedObjects.Count > 0;
            xwpnotitie.Enabled = xwerkplekken.SelectedObjects.Count > 0;
            LoadShifts();
        }

        private void xedituser_Click(object sender, EventArgs e)
        {
            var per = GetCurrentPersoon();
            var bew = Bewerking;
            if (per != null && bew != null)
            {
                var xklus = new NieuwKlusForm(Formulier, per,false,true, bew, per.Werkplek);
                if (xklus.ShowDialog() == DialogResult.OK)
                {
                    WerkPlek wp = bew.WerkPlekken.FirstOrDefault(x => 
                    string.Equals(x.Naam, xklus.SelectedKlus.WerkPlek, StringComparison.CurrentCultureIgnoreCase));
                    if (wp?.Werk != null && wp.Werk.IsBemand)
                    {
                        wp.Tijden.UpdateLijst(xklus.SelectedKlus.Tijden,false);
                    }
                    LoadWerkPlekken(wp);
                    //LoadShifts();
                    xshiftlist.SelectedObject = per;
                    xshiftlist.SelectedItem?.EnsureVisible();
                }
            }
        }

        private void xtijdengewerkt_Click(object sender, EventArgs e)
        {
            var per = GetCurrentPersoon();
            if (per != null)
            {
                var klus = GetCurrentKlus(per, true);
                var wc = new WerktijdChanger(klus);
                if (wc.ShowDialog() == DialogResult.OK)
                {
                    if (per.WerkRooster == null || !per.WerkRooster.IsCustom())
                        per.WerkRooster = klus.Tijden.WerkRooster;

                    var wp = Bewerking?.WerkPlekken.FirstOrDefault(x =>
                        string.Equals(klus.Path, x.Path, StringComparison.CurrentCultureIgnoreCase));
                    if (wp != null)
                    {
                        wp.Tijden.UpdateLijst(klus.Tijden,false);
                        //wp.Tijden.WerkRooster = klus.Tijden.WerkRooster.CreateCopy();
                        LoadWerkPlekken(wp);
                    }
                    xshiftlist.RefreshObject(per);
                }
            }
        }

        private void xroosterb_Click(object sender, EventArgs e)
        {
            if (xshiftlist.SelectedObjects.Count > 0)
            {
                var personen = xshiftlist.SelectedObjects.Cast<Personeel>().ToArray();
                string usersfield = string.Join(", ", personen.Select(x => x.PersoneelNaam));
                var xr = personen.FirstOrDefault(x => x.WerkRooster != null)?.WerkRooster??Manager.Opties.GetWerkRooster();
                var rs = new RoosterForm(xr,
                    $"Rooster voor {usersfield}");
                rs.ViewPeriode = false;
                if (rs.ShowDialog() == DialogResult.Cancel) return;
                foreach (var pers in personen)
                {
                    var klus = GetCurrentKlus(pers, false);
                    pers.WerkRooster = rs.WerkRooster.CreateCopy();
                    if (klus?.Tijden != null)
                    {
                        klus.Tijden.WerkRooster = pers.WerkRooster;
                        var ent = klus.Tijden.GetInUseEntry(false);
                        if (ent != null)
                            ent.WerkRooster = pers.WerkRooster;
                    }
                    xshiftlist.RefreshObject(pers);
                }

                if (Bewerking != null)
                {
                    foreach (var wp in Bewerking.WerkPlekken)
                        wp.UpdateWerkRooster(null,true,false,false,false,false,false,false);
                    LoadWerkPlekken(null);
                }
            }
        }

        private void xshiftlist_DoubleClick(object sender, EventArgs e)
        {
            if (xshiftlist.SelectedObject is Personeel selected)
            {
                var actief = ClickImage();
                SetPersoneelActief(selected, actief);
                xshiftlist.SelectedObject = selected;
                xshiftlist.SelectedItem?.EnsureVisible();
                xwerkplekken.RefreshObjects(xwerkplekken.Objects.Cast<WerkPlek>().ToArray());
            }
        }

        private void xeditwerkplek_Click(object sender, EventArgs e)
        {
            if (xwerkplekken.SelectedObject is WerkPlek plek)
            {
                var werkplek = ChooseWerkplek(plek.Naam);
                if (string.IsNullOrEmpty(werkplek)) return;
                var plekpath = plek.Path;
                bool thesame = string.Equals(plekpath, werkplek, StringComparison.CurrentCultureIgnoreCase);
                if (thesame) return;

                var xnewwp = Bewerking.WerkPlekken.FirstOrDefault(x =>
                    string.Equals(x.Naam, werkplek, StringComparison.CurrentCultureIgnoreCase));
                if (xnewwp != null)
                {
                    if (plek.Storingen.Count > 0)
                        xnewwp.Storingen.AddRange(plek.Storingen);
                    if (plek.Personen.Count > 0)
                    {
                        for(int i = 0; i < plek.Personen.Count; i++)
                        {
                            var per = plek.Personen[i];
                            xnewwp.Personen.Remove(per);
                            xnewwp.Personen.Add(per);
                            per.WerktAanKlus(plekpath, out var klus);
                            if (klus != null) klus.WerkPlek = werkplek;
                            per.Werkplek = werkplek;
                        }
                    }

                    xnewwp.Tijden = plek.Tijden;
                    if (plek.AantalGemaakt > xnewwp.AantalGemaakt)
                        xnewwp.AantalGemaakt = plek.AantalGemaakt;
                    Bewerking.WerkPlekken.Remove(plek);
                    plek = xnewwp;
                }
                else if (plek.Personen.Count > 0)
                {
                    for (int i = 0; i < plek.Personen.Count; i++)
                    {
                        var per = plek.Personen[i];
                        per.WerktAanKlus(plekpath, out var klus);
                        if (klus != null) klus.WerkPlek = werkplek;
                        per.Werkplek = werkplek;
                    }

                    plek.Naam = werkplek;
                }

                LoadWerkPlekken(plek);
                //LoadShifts();
                //xwerkplekken.SelectedObject = plek;
                //xwerkplekken.SelectedItem?.EnsureVisible();
            }
        }

        private void xwerkplektijden_Click(object sender, EventArgs e)
        {
            if (xwerkplekken.SelectedObject is WerkPlek wp)
            {
                var wc = new WerktijdChanger(wp);
                if (wc.ShowDialog() == DialogResult.OK)
                {
                    xwerkplekken.RefreshObject(wp);
                    xwerkplekken.SelectedObject = wp;
                    xwerkplekken.SelectedItem?.EnsureVisible();
                    LoadShifts();
                }
            }
        }

        private void xwerkplekken_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            if (e.Model is WerkPlek wp)
            {
                e.Title = $"{wp.Path}";
                e.Text = $"{wp.AantalGemaakt} gemaakt op {wp.Naam}.\n" +
                         $"Totaal {wp.TijdAanGewerkt()} uur aan gewerkt met {wp.Personen.Count} personen.\n" +
                         $"Gewerkt vanaf:\n" +
                         $"{wp.Tijden.GetFirstStart()}\n" +
                         $"Tot: {wp.Tijden.GetLastStop()}.";
                if (wp.Tijden.WerkRooster != null && wp.Tijden.WerkRooster.IsCustom())
                    e.Text += "Werkt momenteel met een aangepaste rooster.";
            }
        }

        private void xplekroosterb_Click(object sender, EventArgs e)
        {
            if (xwerkplekken.SelectedObjects.Count > 0)
            {
                var plekken = xwerkplekken.SelectedObjects.Cast<WerkPlek>().ToArray();
                string usersfield = string.Join(", ", plekken.Select(x => x.Naam));
                var xr = plekken.FirstOrDefault(x => x.Tijden.WerkRooster != null)?.Tijden.WerkRooster ?? Manager.Opties.GetWerkRooster();
                var rs = new RoosterForm(xr,
                    $"Rooster voor {usersfield}");
                rs.ViewPeriode = false;
                if (rs.ShowDialog() == DialogResult.Cancel) return;
                for (int i = 0; i < plekken.Length; i++)
                {
                    var plek = plekken[i];
                    var flag = plek.Personen.Any(x =>
                        x.WerkRooster == null || !x.WerkRooster.SameTijden(rs.WerkRooster));
                    var xrs = rs.WerkRooster.CreateCopy();
                    if (flag)
                    {
                        var result = XMessageBox.Show(
                            this, $"Er zijn personeel leden op {plek.Naam} die niet hetzelfde rooster hebben als wat jij hebt gekozen...\n" +
                                  $"De personeel bepaald natuurlijk hoe en wanneer er op {plek.Naam} wordt gewerkt.\n\n" +
                                  $"Wil je dit rooster door geven aan alle personeel leden op {plek.Naam}?",
                            "Personeel Werkrooster Wijzigen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (result == DialogResult.Cancel) return;
                        if (result == DialogResult.Yes)
                        {
                            for (int j = 0; j < plek.Personen.Count; j++)
                            {
                                var per = plek.Personen[j];
                                per.WerkRooster = xrs;
                                var klus = per.Klusjes.GetKlus(plek.Path);
                                if (klus != null)
                                {
                                    klus.Tijden.WerkRooster = per.WerkRooster;
                                    var ent = klus.Tijden.GetInUseEntry(false);
                                    if (ent != null)
                                        ent.WerkRooster = per.WerkRooster;
                                }
                                xshiftlist.RefreshObject(per);
                            }
                        }
                    }
                    plek.Tijden.WerkRooster = xrs;
                    xwerkplekken.RefreshObject(plek);
                    LoadShifts();
                }
            }
        }

        private void xwpnotitie_Click(object sender, EventArgs e)
        {
            if (xwerkplekken.SelectedObject is WerkPlek wp)
            {
                var wpnode = new NotitieForms(wp.Note, wp);
                if (wpnode.ShowDialog() == DialogResult.OK)
                    wp.Note = wpnode.Notitie;
            }
        }

        private void xwerkplekken_ModelCanDrop(object sender, ModelDropEventArgs e)
        {
            if (e.TargetModel is WerkPlek wp)
            {
                var pers = e.SourceModels.Cast<Personeel>().ToArray();
                foreach (var per in pers)
                    if (string.Equals(per.Werkplek, wp.Naam, StringComparison.CurrentCultureIgnoreCase))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }

                e.Effect = DragDropEffects.Move;
            }
            else e.Effect = DragDropEffects.None;
        }

        private void xwerkplekken_ModelDropped(object sender, ModelDropEventArgs e)
        {
            if (e.TargetModel is WerkPlek wp)
            {
                var pers = e.SourceModels.Cast<Personeel>().ToArray();
                foreach (var per in pers)
                {
                    if (string.Equals(per.Werkplek, wp.Naam, StringComparison.CurrentCultureIgnoreCase))
                        continue;
                   
                    
                    if (wp.Personen.Any(x =>
                        string.Equals(per.PersoneelNaam, x.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        var result = XMessageBox.Show(this, $"'{per.PersoneelNaam}' bestaat al op '{wp.Naam}'!\n\n" + 
                                                      $@"Zou je {per.PersoneelNaam} willen overschrijven?",$"{per.PersoneelNaam} vervangen op {wp.Naam}",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Cancel) return;
                        if (result == DialogResult.No)
                            continue;
                    }
                    var oldwp = GetWerkPlek(per, false);
                    Klus klus = null;
                    if (oldwp != null)
                    {
                        oldwp.RemovePersoon(per.PersoneelNaam);
                        wp.RemovePersoon(per.PersoneelNaam);
                        per.WerktAanKlus(oldwp.Path, out klus);
                    }
                    per.Werkplek = wp.Naam;
                    klus ??= new Klus(per, wp);
                    per.ReplaceKlus(klus);
                    klus.WerkPlek = wp.Naam;
                    wp.AddPersoon(per, wp.Werk);
                }

                e.RefreshObjects();
                xwerkplekken.SelectedObject = wp;
                xwerkplekken.SelectedItem?.EnsureVisible();
            }
        }

        private void xwerktijdnaarwerkplek_Click(object sender, EventArgs e)
        {
            if (xwerkplekken.SelectedObject is WerkPlek wp)
            {
                List<Personeel> pers = new List<Personeel>();
                if (xshiftlist.SelectedObjects.Count > 0)
                {
                    pers = xshiftlist.SelectedObjects.Cast<Personeel>().ToList();
                }
                else
                    pers = wp.Personen;

                if (pers.Count > 0)
                {
                    var x1 = pers.Count == 1 ? pers[0].PersoneelNaam : $"{pers.Count} medewerkers";
                    if (XMessageBox.Show(this, $"Wil je alle gewerkte tijden van {x1} naar {wp.Naam} verplaatsen?",
                            $"Tijden Naar {wp.Naam}", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
                        DialogResult.Yes)
                    {
                        foreach (var user in pers)
                        {
                            var klus = GetCurrentKlus(user, false);
                            if (klus == null) continue;
                            wp.Tijden.UpdateLijst(klus.Tijden,false);
                        }
                    }

                    xwerkplekken.RefreshObject(wp);
                }
            }
        }

        private void xwerkplekken_DoubleClick(object sender, EventArgs e)
        {
            if (xwerkplekken.SelectedObject is WerkPlek wp)
            {
                var checkall = !wp.IsActief();
                foreach (var per in wp.Personen) 
                    SetPersoneelActief(per, checkall);
                wp.UpdateWerkplek(false);
                xwerkplekken.RefreshObject(wp);
            }
        }

        private void xnaampersoneel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && xnaampersoneel.Text.Trim().Length > 2)
            {
                UpdatePersoneelNaamTextbox();
                if (xnaampersoneel.Tag is Personeel)
                    xvoegindeling_Click(sender, EventArgs.Empty);
            }
        }

        private void xnaampersoneel_TextChanged(object sender, EventArgs e)
        {
            //if (xnaampersoneel.Text.Trim().Length > 2)
            //    UpdatePersoneelNaamTextbox();
        }
    }
}