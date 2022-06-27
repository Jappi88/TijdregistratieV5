using Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;
using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace Controls
{
    public delegate string WerkplekTextGetterHandler(WerkplekIndeling indeling);
    
    public partial class WerkplekIndeling : UserControl
    {
        public bool IsCompact
        {
            get => Settings?.IsCompact ?? false;
            private set
            {
                Settings ??= new WerkplaatsSettings();
                Settings.IsCompact = value;
            }
        }

        public string Werkplek
        {
            get => Settings?.Name; private set
            {
                Settings ??= new WerkplaatsSettings();
                Settings.Name = value;
            }
        }

        public bool IsDefault()
        {
            return string.IsNullOrEmpty(Werkplek) ||
                   string.Equals(Werkplek, "default", StringComparison.CurrentCultureIgnoreCase);
        }
        public WerkplaatsSettings Settings { get; set; } = new WerkplaatsSettings();
        public string Criteria { get; set; }
        public bool ToonNietIngedeeld = true;
        public Bewerking SelectedBewerking { get; set; }
        public bool IsSelected { get; set; }
        public DateTime GereedOp { get; set; }
        public WerkplekTextGetterHandler FieldTextGetter { get; set; }

        public WerkplekIndeling()
        {
            InitializeComponent();
        }

        public void InitWerkplek(WerkplaatsSettings werkplek)
        {
            try
            {
                Settings = werkplek;
                UpdateWerkplekInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateWerkplekInfo()
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(UpdateWerkplekInfo));
            }
            else
            {
                xcompact.Image = IsCompact ? Resources.icons8_expand_32 : Resources.icons8_collapse_32;
                xcompactinfo.Visible = IsCompact && (SelectedBewerking == null || !IsSelected);
                if (IsDefault())
                {
                    // ximage.Image = Resources.operation;
                    xVerwijderPersoneel.Visible = false;
                    xresetindeling.Visible = false;
                    xnietingedeeld.Visible = true;
                    xcompact.Visible = true;
                    xnietingedeeld.Checked = ToonNietIngedeeld;
                    if (FieldTextGetter != null)
                    {
                        if (IsCompact)
                        {
                            xpersoonInfo.Text = "";
                            xcompactinfo.Text = FieldTextGetter?.Invoke(this);
                        }
                        else
                        {
                            xpersoonInfo.Text = FieldTextGetter?.Invoke(this);
                            xcompactinfo.Text = "";
                        }

                    }
                    xknoppenpanel.Visible = false;
                    xpanel.Visible = false;
                }
                else
                {
                    //ximage.Image = Resources.user_customer_person_13976;
                    xknoppenpanel.Visible = SelectedBewerking != null && !IsDefault() && IsSelected;
                    xcompact.Visible = true;
                    xVerwijderPersoneel.Visible = true;
                    xVerwijderKlus.Enabled = SelectedBewerking != null && SelectedBewerking.State != ProductieState.Gereed;
                    xresetindeling.Visible = true;
                    xpanel.Visible = true;
                 
                    
                    if (Parent is GroupBox group)
                    {
                        if (SelectedBewerking != null && IsSelected)
                        {
                            var bw = SelectedBewerking;
                            group.Text = $"{Werkplek} {bw.Naam}[{bw.ProductieNr}, {bw.ArtikelNr}]";
                        }
                        else group.Text = $"{Werkplek}";
                    }
                    if (SelectedBewerking != null)
                    {
                        var wp = SelectedBewerking.GetWerkPlek(Werkplek, false);
                        xStartKlus.Enabled = SelectedBewerking.State == ProductieState.Gestopt && wp != null;
                        xStopKlus.Enabled = SelectedBewerking.State == ProductieState.Gestart && wp != null;
                    }
                    else
                    {
                        xknoppenpanel.Visible = false;
                    }
                }
                if (FieldTextGetter != null)
                {
                    if (IsCompact)
                    {
                        xpersoonInfo.Text = "";
                        xcompactinfo.Text = FieldTextGetter.Invoke(this);
                    }
                    else
                    {
                        xpersoonInfo.Text = FieldTextGetter.Invoke(this);
                        xcompactinfo.Text = "";
                    }

                }
                UpdateHeight();
            }
        }

        public void UpdateHeight()
        {
            try
            {
                xpanel.Height = IsCompact && (SelectedBewerking == null || !IsSelected) ? 44 : 40;
                Control c = this.Parent ?? this;
                if (c != null)
                {

                    c.Height = IsCompact ? 60 : IsDefault()? 122 : 140;
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SetBewerking(Bewerking bew)
        {
            SelectedBewerking = bew;
            if (IsDefault()) return;
            UpdateWerkplekInfo();
        }

        private void PersoonIndeling_DragEnter(object sender, DragEventArgs e)
        {
            var data = (GroupBox)e.Data.GetData(typeof(GroupBox));
            var falsecolor = Color.MistyRose;
            var truecolor = Color.LightGreen;
            if (data != null && data.Controls.Count > 0 && data.Controls[0] is WerkplekIndeling indeling && !indeling.IsDefault())
            {
                this.BackColor = truecolor;
                e.Effect = DragDropEffects.Move;
                return;
            }
            if (e.Data.GetData("Producties") is ArrayList xdata)
            {
                foreach (var x in xdata)
                {
                    if (x is Bewerking bew)
                    {
                        if (bew.State is ProductieState.Gereed or ProductieState.Verwijderd) continue;
                        if (!IsDefault())
                        {
                            if (Settings?.Voorwaardes != null && !Settings.Voorwaardes.IsAllowed(bew, null, false))
                                continue;
                            var wps = Manager.BewerkingenLijst?.GetWerkplekken(bew.Naam);
                            if (wps == null || !wps.Any(w =>
                                    string.Equals(w, Werkplek, StringComparison.CurrentCultureIgnoreCase)))
                                continue;
                            var wp = bew.GetWerkPlek(Werkplek, false);
                            if (wp == null)
                            {
                                this.BackColor = truecolor;
                                e.Effect = DragDropEffects.Link;
                                return;
                            }
                        }
                    }
                }
            }

            this.BackColor = falsecolor;
            e.Effect = DragDropEffects.None;
        }

        private void PersoonIndeling_DragDrop(object sender, DragEventArgs e)
        {

            try
            {
                if (e.Data.GetData("Producties") is ArrayList xdata)
                {
                    foreach (var x in xdata)
                    {
                        if (x is Bewerking bew)
                        {
                            if (bew.State is ProductieState.Gereed or ProductieState.Verwijderd) continue;
                            if (!IsDefault())
                            {
                                if (Settings?.Voorwaardes != null && !Settings.Voorwaardes.IsAllowed(bew, null, false))
                                    continue;
                                var wps = Manager.BewerkingenLijst?.GetWerkplekken(bew.Naam);
                                if (wps == null || !wps.Any(w =>
                                        string.Equals(w, Werkplek, StringComparison.CurrentCultureIgnoreCase)))
                                    continue;
                                var werk = Werk.FromPath(bew.Path);
                                if (!werk.IsValid) continue;
                                var per = werk.Formulier;
                                bew = per.Bewerkingen.FirstOrDefault(x => x.Equals(bew));
                                bew.WerkPlekken.RemoveAll(b =>
                                    b.TijdGewerkt == 0 && b.TotaalGemaakt == 0);
                                var wp = bew.GetWerkPlek(Werkplek, true);
                                wp.Tijden.WerkRooster = Settings?.WerkRooster;
                                wp.Tijden.SpecialeRoosters = Settings?.SpecialeRoosters;
                                _ = per.UpdateForm(true,false,null,
                                    $"[{wp.Naam}] Ingedeeld op {bew.Path}");
                            }
                        }
                    }
                }
                BitMapCursor?.Dispose();
                BitMapCursor = null;
                _isDragging = false;
                this.OnMouseLeave(EventArgs.Empty);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                XMessageBox.Show(this,exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void PersoonIndeling_DragLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(EventArgs.Empty);
        }

        private void xPersoonImage_Click(object sender, EventArgs e)
        {
            this.OnClick(EventArgs.Empty);
        }

        private void xPersoonImage_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(EventArgs.Empty);
        }

        private void xPersoonImage_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(EventArgs.Empty);
        }

        private void xVerwijderKlus_Click(object sender, EventArgs e)
        {
            if (SelectedBewerking == null || IsDefault()) return;
            if (!IsDefault() && SelectedBewerking != null)
            {
                if (SelectedBewerking == null) return;
                var xremove = SelectedBewerking.WerkPlekken.Where(x =>
                    string.Equals(Werkplek.Replace(" ",""), x.Naam.Replace(" ",""), StringComparison.CurrentCultureIgnoreCase)).ToList();
                var xtijd = xremove.Sum(x => x.TijdAanGewerkt());
                if (xtijd > 0)
                {
                    if (XMessageBox.Show(this.Parent?.Parent?.Parent?.Parent?.Parent,
                            $"Er is {xtijd} uur gewerkt aan {SelectedBewerking.Naam} op {Werkplek}!\n\n" +
                            $"Weetje zeker dat je {SelectedBewerking.Naam}({SelectedBewerking.ProductieNr}) uit {Werkplek} wilt verwijderen?",
                            $"{Werkplek} Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) ==
                        DialogResult.No) return;
                }

                if (xremove.Count > 0)
                {
                    foreach (var xr in xremove)
                        SelectedBewerking.WerkPlekken.Remove(xr);
                    _ = SelectedBewerking.UpdateBewerking(null,
                        $"{Werkplek} verwijderd uit [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!");
                }

            }
        }

        private void xStopKlus_Click(object sender, EventArgs e)
        {
            if (SelectedBewerking == null || IsDefault()) return;

            string change =
                $"{Werkplek} gestopt met {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!";
            bool changed = false;
            var wp = SelectedBewerking.GetWerkPlek(Werkplek, false);
            if (wp == null) return;
            foreach (var xp in wp.Personen)
            {
                if (xp.IngezetAanKlus(SelectedBewerking, false, out var klusjes))
                {
                    var xdp = Manager.Database.xGetPersoneel(xp.PersoneelNaam);
                    foreach (var klus in klusjes)
                    {
                        klus.Stop();
                        klus.IsActief = false;
                        changed = true;
                        xdp?.ReplaceKlus(klus);
                    }

                    if (xdp != null)
                        Manager.Database.xUpSert(xdp.PersoneelNaam, xdp, change);
                }
            }

            if (changed)
            {
                xStartKlus.Enabled = true;
                xStopKlus.Enabled = false;
                _ = SelectedBewerking.xUpdateBewerking(null,
                    $"{Werkplek} UPDATE: {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!");

            }
        }

        private void xStartKlus_Click(object sender, EventArgs e)
        {
            if (SelectedBewerking == null || IsDefault() ||
                SelectedBewerking.State is ProductieState.Verwijderd or ProductieState.Gereed) return;
            var wp = SelectedBewerking.GetWerkPlek(Werkplek, true);
            if (!wp.IsActief())
            {
                if (wp.Personen.Count > 0)
                {
                    foreach (var per in wp.Personen)
                    {
                        SelectedBewerking.ZetPersoneelActief(per.PersoneelNaam, wp.Naam, true);
                        var xdb = Manager.Database.xGetPersoneel(per.PersoneelNaam);
                        if (xdb != null)
                        {
                            if (per.IngezetAanKlus(wp.Path, true, out var klusjes))
                            {
                                klusjes.ForEach(x => xdb.ReplaceKlus(x));
                                string change =
                                    $"{xdb.PersoneelNaam} gestart met {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!";
                                Manager.Database?.xUpSert(xdb.PersoneelNaam, xdb, change);
                            }
                        }
                    }
                }
                else
                {
                    var xbw = SelectedBewerking;
                    if (!ProductieListControl.AddPersoneel(this, ref xbw, wp.Naam))
                        return;
                }
            }

            if (SelectedBewerking.State == ProductieState.Gestopt)
            {
                if (!ProductieListControl.StartBewerkingen(this.Parent, new Bewerking[] {SelectedBewerking}))
                    return;
            }
            else
            {
                _ = SelectedBewerking.UpdateBewerking(null,
                    $"{Werkplek} UPDATE: {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!");
            }
            xStartKlus.Enabled = false;
            xStopKlus.Enabled = true;
        }

        private void xPersoonImage_DoubleClick(object sender, EventArgs e)
        {
            if(!IsDefault())
            {
                TogleCompactMode();
            }
            this.OnDoubleClick(EventArgs.Empty);
        }

        private void xpersoonInfo_ImageLoad(object sender, HtmlImageLoadEventArgs e)
        {
            switch (e.Src?.ToLower())
            {
                case "bewerkingen":
                case null:
                    e.Callback(Resources.operation);
                    break;
                default:
                    e.Callback(Resources.iconfinder_technology);
                    break;
            }
            e.Handled = true;
        }

        private void xVerwijderPersoneel_Click(object sender, EventArgs e)
        {
            OnVerwijderWerkplaats(EventArgs.Empty);
        }

        public event EventHandler VerwijderWerkplaats;

        protected void OnVerwijderWerkplaats(EventArgs e)
        {
            VerwijderWerkplaats?.Invoke(this, e);
        }

        public event EventHandler ViewStateChanged;

        protected override void OnClick(EventArgs e)
        {
            Focus();
            base.OnClick(e);
        }

        public event EventHandler ResetIndeling;
        protected virtual void OnResetIndeling()
        {
            ResetIndeling?.Invoke(this, EventArgs.Empty);
        }

        private bool _isDragging;
        private readonly int _DDradius = 40;
        private int _mX;
        private int _mY;
       
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            base.OnMouseDown(e);
            _mX = e.X;
            _mY = e.Y;
            _isDragging = false;
            //Cast the sender to control type youre using
            //Copy the control in a bitmap
            Bitmap bmp = new Bitmap(this.Parent.Width, this.Parent.Height);
            this.Parent.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
            bmp = bmp.ChangeOpacity(0.75f);
            //In a variable save the cursor with the image of your controler
            this.BitMapCursor = new Cursor(bmp.GetHicon());
            //this.DoDragDrop(this.Parent, DragDropEffects.Move);

        }

        private Cursor BitMapCursor;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!_isDragging)
            {
                // This is a check to see if the mouse is moving while pressed.
                // Without this, the DragDrop is fired directly when the control is clicked, now you have to drag a few pixels first.
                if (e.Button == MouseButtons.Left && _DDradius > 0 && !IsDefault())
                {
                    var num1 = _mX - e.X;
                    var num2 = _mY - e.Y;
                    if (num1 * num1 + num2 * num2 > _DDradius)
                    {
                        DoDragDrop(this.Parent, DragDropEffects.All);
                        _isDragging = true;
                        return;
                    }
                }

                _isDragging = false;
                base.OnMouseMove(e);
            }
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            //Deactivate the default cursor
            e.UseDefaultCursors = false;
            //Use the cursor created from the bitmap
            Cursor.Current = this.BitMapCursor;
            base.OnGiveFeedback(e);
        }

        public void IndelingMouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        public void IndelingMouseMove(object sender, MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }

        private void xpersoonInfo_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            this.OnGiveFeedback(e);
        }

        private void xnietingedeeld_CheckedChanged(object sender, EventArgs e)
        {
            ToonNietIngedeeld = xnietingedeeld.Checked;
            OnViewStateChanged();
        }

        protected virtual void OnViewStateChanged()
        {
            ViewStateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void xresetindeling_Click(object sender, EventArgs e)
        {
            this.OnResetIndeling();
        }

        private void xcompact_Click(object sender, EventArgs e)
        {
            TogleCompactMode();
        }

        public void TogleCompactMode()
        {
            IsCompact = !IsCompact;
            UpdateWerkplekInfo();
        }

        private void xsettings_Click(object sender, EventArgs e)
        {
            var set = new WerkplaatsSettingsForm(Settings);
            if (set.ShowDialog(this) != DialogResult.OK) return;
            Settings = set.Settings;
            if (Manager.Opties != null)
                Manager.Opties.NationaleFeestdagen = set.roosterUI1.NationaleFeestdagen().ToArray();
            UpdateWerkplekInfo();
            OnSettingsChanged();
        }

        public event EventHandler SettingsChanged;
        protected void OnSettingsChanged()
        {
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
