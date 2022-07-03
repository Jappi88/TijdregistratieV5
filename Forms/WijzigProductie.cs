using BrightIdeasSoftware;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class WijzigProductie : Forms.MetroBase.MetroBaseForm
    {
        private readonly bool editmode;

        internal string[] prods = { };

        public WijzigProductie()
        {
            InitializeComponent();
            Formulier = new ProductieFormulier();
            objectEditorUI1.ExcludeItems.AddRange(new[] { "state", "naam" });
            ((OLVColumn)xbewerkinglijst.Columns[0]).ImageGetter = sender => 0;
            metroTabControl1.SelectedIndex = 0;
        }



        public WijzigProductie(ProductieFormulier form)
        {
            InitializeComponent();
            Formulier = form;
            editmode = true;
            objectEditorUI1.ExcludeItems.AddRange(new[] { "productienr", "artikelnr", "state", "naam" });
            ((OLVColumn)xbewerkinglijst.Columns[0]).ImageGetter = sender => 0;
            metroTabControl1.SelectedIndex = 0;
        }

        public ProductieFormulier Formulier { get; private set; }

        private void Form_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            var thesame = Formulier.Equals(changedform);
            if (!thesame) return;
            if (changedform.State == ProductieState.Verwijderd)
                DialogResult = DialogResult.Cancel;
            else
            {
                {
                    Formulier = changedform.CreateCopy();
                    // dynamicObjectEditor1.Instance = Formulier;
                }
            }
        }


        private void AddBewerkingen(ProductieFormulier form)
        {
            var p = form;
            if (p.Bewerkingen?.Length > 0)
            {
                foreach (var b in p.Bewerkingen)
                    b.xUpdateBewerking(null, "", false, false, false, false);
                xbewerkinglijst.SetObjects(p.Bewerkingen);
                if (p.Bewerkingen.Length <= 0) return;
                xbewerkinglijst.SelectedObject = p.Bewerkingen.First();
                xbewerkinglijst.SelectedItem?.EnsureVisible();
            }
        }

        private Bewerking[] GetBewerkingen()
        {
            var bws = new List<Bewerking>();
            if (xbewerkinglijst.Objects != null && xbewerkinglijst.Items.Count > 0)
            {
                var xitems = xbewerkinglijst.Objects.Cast<Bewerking>().ToArray();
                foreach (var item in xitems)
                {
                    item.Parent = Formulier;
                    bws.Add(item);
                }
            }

            return bws.ToArray();
        }

        public bool SetFormInfo()
        {
            Formulier = ((ProductieFormulier)objectEditorUI1.Instance) ?? new ProductieFormulier();
            materiaalUI1.SaveMaterials();
            return true;
        }

        public void SetFormFields(ProductieFormulier p)
        {
            if (p == null)
                return;
            materiaalUI1.InitMaterialen(p);
            AddBewerkingen(p);
            Text = $"Productie : [{p.ProductieNr}]-[{p.ArtikelNr}]";
            objectEditorUI1.InitInstance(p);
            this.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void InitForm(ProductieFormulier p)
        {
            Formulier = p?.CreateCopy();
            Text = $"Productie : [{p.ProductieNr}]-[{p.ArtikelNr}]";
            this.Invalidate();
            //SetTextboxAutofill();
            SetFormFields(p);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Formulier?.ArtikelNr))
            {
                XMessageBox.Show(this, $"Vul een geldige artikel nummer in a.u.b", "ArtikelNr", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(Formulier?.ProductieNr))
            {
                XMessageBox.Show(this, $"Vul een geldige productie nummer in a.u.b", "ProductieNr", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else if (!editmode && prods.Any(t => string.Equals(t, Formulier?.ProductieNr, StringComparison.CurrentCultureIgnoreCase)))
            {
                XMessageBox.Show(
                    this, $"Productie {Formulier?.ProductieNr} bestaat al en kan daarom niet nogmaals gebruikt worden!\nProbeer een andere nummer a.u.b",
                    "ProductieNr", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Formulier.Aantal <= 0)
            {
                XMessageBox.Show(this, $"Aantal moet meer zijn dan 0!\n Vul eerst een geldige aantal in a.u.b", "Aantal",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (await Save())
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void xpersoneel_Click(object sender, EventArgs e)
        {
            switch (xbewerkinglijst.SelectedObject)
            {
                case null:
                    return;
                case Bewerking b:
                    {
                        var x = new Indeling(Formulier, b) { StartPosition = FormStartPosition.CenterParent };
                        if (x.ShowDialog(this) == DialogResult.OK)
                        {
                            xbewerkinglijst.RefreshObject(b);
                        }

                        break;
                    }
            }
        }

        private async Task<bool> Save()
        {
            SetFormInfo();
            Formulier.Bewerkingen = GetBewerkingen();
            await Formulier.UpdateForm(true, false, null, $"[{Formulier.ProductieNr}] Bewerkingen Gewijzigd");
            return true;
        }

        private async void xvoegbewtoe_Click(object sender, EventArgs e)
        {
            string naam = null;
            var count = 0;
            var bewerkingen = Manager.BewerkingenLijst.GetAllEntries().Select(x => x.Naam).ToArray();
            var xbwform = new BewerkingChooser(bewerkingen);
            xbwform.Title = "Voeg een nieuwe bewerking toe";
            if (xbwform.ShowDialog() != DialogResult.OK) return;
            naam = xbwform.SelectedItem;
            if (naam == null || naam.Trim().Length < 5 ||
                naam.Trim() == "")
            {
                XMessageBox.Show(this, $"Kies een een geldige bewerking a.u.b.", "Ongeldige Bewerking",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (xbewerkinglijst.Objects != null && (count += xbewerkinglijst.Objects.Cast<Bewerking>()
                    .Sum(x => x.Naam.ToLower().Split('[')[0] == naam.ToLower() ? 1 : 0)) >
                0) naam = naam + $"[{count}]";
            var bw = await new Bewerking((double)Formulier.DoorloopTijd).CreateNewInstance(Formulier);
            bw.Naam = naam;
            bw.DatumToegevoegd = DateTime.Now;
            xbewerkinglijst.AddObject(bw);
            xbewerkinglijst.SelectedObject = bw;
            xbewerkinglijst.SelectedItem?.EnsureVisible();
            UpdateButtons();
        }

        private void AddProduction_FormClosed(object sender, FormClosedEventArgs e)
        {
            Manager.OnFormulierChanged -= Form_OnFormulierChanged;
        }

        private void WijzigProductie_Shown(object sender, EventArgs e)
        {
            InitForm(Formulier);
            Manager.OnFormulierChanged += Form_OnFormulierChanged;
        }

        private void xverwijderbewerking_Click(object sender, EventArgs e)
        {
            var count = 0;
            if ((count = xbewerkinglijst.SelectedObjects.Count) == 0)
                return;

            var xvalue = count == 1 ? "bewerking" : "bewerkingen";
            if (XMessageBox.Show(
                    this, $"Je staat op het punt {count} {xvalue} te verwijderen.\n\nWeet je zeker dat je door wilt gaan?\nClick 'Nee' om te annuleren.",
                "Bewerking Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                xbewerkinglijst.RemoveObjects(xbewerkinglijst.SelectedObjects);
        }

        private void xbewerkinglijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            xverwijderbewerking.Enabled = xbewerkinglijst.SelectedObjects.Count > 0;
            xwerkplekken.Enabled = xbewerkinglijst.SelectedObject != null;
            xbeheeronderbrekeningen.Enabled = xbewerkinglijst.SelectedObject != null;
            xsetbewerkingnaam.Enabled = xbewerkinglijst.SelectedObject != null;
            xedit.Enabled = xbewerkinglijst.SelectedObject != null;
            xupbutton.Enabled = xbewerkinglijst.SelectedIndex > 0;
            xdownbutton.Enabled = xbewerkinglijst.SelectedIndex > -1 && xbewerkinglijst.SelectedIndex < xbewerkinglijst.Items.Count - 1;
        }

        private void xbeheeronderbrekeningen_Click(object sender, EventArgs e)
        {
            if (xbewerkinglijst.SelectedObject == null)
                return;
            if (xbewerkinglijst.SelectedObject is Bewerking b)
            {
                var st = new StoringForm(b.Path, b.WerkPlekken);
                st.ShowDialog(this);
            }
        }

        private void xedit_Click(object sender, EventArgs e)
        {
            if (xbewerkinglijst.SelectedObject is Bewerking bew)
            {
                var xform = new ObjectEditForm();
                xform.Title = $"Wijzig {bew.Path}";
                xform.ExcludeItems.AddRange(new[] { "productienr", "artikelnr", "verwachtleverdatum" });
                xform.Init(bew);
                if (xform.ShowDialog(this) == DialogResult.OK)
                {
                    xbewerkinglijst.RefreshObject(xform.Instance);
                }
            }
        }

        private void xbewerkinglijst_DoubleClick(object sender, EventArgs e)
        {
            xedit_Click(sender, e);
        }

        private void xsetbewerkingnaam_Click(object sender, EventArgs e)
        {

            switch (xbewerkinglijst.SelectedObject)
            {
                case null:
                    return;
                case Bewerking b:
                    {
                        var bewerkingen = Manager.BewerkingenLijst.GetAllEntries().Select(x => x.Naam).ToArray();
                        var xbwform = new BewerkingChooser(bewerkingen);
                        xbwform.Title = $"Wijzig '{b.Naam}'";
                        if (xbwform.ShowDialog() != DialogResult.OK) return;
                        var naam = xbwform.SelectedItem;
                        var xselected = naam?.Trim();
                        if (string.IsNullOrEmpty(xselected))
                            return;
                        if (string.Equals(b.Naam, xselected, StringComparison.CurrentCultureIgnoreCase))
                            return;
                        if (xbewerkinglijst.Objects.OfType<Bewerking>().Any(x =>
                                string.Equals(x.Naam, xselected, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            XMessageBox.Show(this, $"Bewerking '{xselected}' bestaat al!", "Bestaat Al!",
                                MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (XMessageBox.Show(this, $"Wil je de bewerkingnaam '{b.Naam}' wijzigen naar '{xselected}'?",
                                "Bewerking Naam Wijzigen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            var xindex = xbewerkinglijst.IndexOf(b);
                            xbewerkinglijst.RemoveObject(b);
                            b.Naam = xselected;
                            xbewerkinglijst.InsertObjects(xindex, new Bewerking[] { b });
                            xbewerkinglijst.SelectedObject = b;
                            xbewerkinglijst.SelectedItem?.EnsureVisible();
                        }

                        break;
                    }
            }
        }

        private void xupbutton_Click(object sender, EventArgs e)
        {
            if (xbewerkinglijst.SelectedObject is Bewerking b)
            {
                var xindex = xbewerkinglijst.IndexOf(b);
                if (xindex > 0)
                {
                    xindex--;
                    xbewerkinglijst.RemoveObject(b);
                    xbewerkinglijst.InsertObjects(xindex, new Bewerking[] { b });
                    xbewerkinglijst.SelectedObject = b;
                    xbewerkinglijst.SelectedItem?.EnsureVisible();
                }
            }
        }

        private void xdownbutton_Click(object sender, EventArgs e)
        {
            if (xbewerkinglijst.SelectedObject is Bewerking b)
            {
                var xindex = xbewerkinglijst.IndexOf(b);
                if (xindex < xbewerkinglijst.Items.Count -1)
                {
                    xindex++;
                    xbewerkinglijst.RemoveObject(b);
                    xbewerkinglijst.InsertObjects(xindex, new Bewerking[] { b });
                    xbewerkinglijst.SelectedObject = b;
                    xbewerkinglijst.SelectedItem?.EnsureVisible();
                }
            }
        }
    }
}