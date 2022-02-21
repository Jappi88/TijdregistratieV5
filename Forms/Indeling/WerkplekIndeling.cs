using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace Controls
{
    public delegate string WerkplekTextGetterHandler(WerkplekIndeling indeling);

    public partial class WerkplekIndeling : UserControl
    {
        private readonly int _DDradius = 40;

        private bool _isDragging;
        private int _mX;
        private int _mY;

        private Cursor BitMapCursor;
        public bool ToonNietIngedeeld = true;

        public WerkplekIndeling()
        {
            InitializeComponent();
        }

        public string Werkplek { get; set; }

        public string Criteria { get; set; }
        public Bewerking SelectedBewerking { get; set; }
        public bool IsSelected { get; set; }

        public WerkplekTextGetterHandler FieldTextGetter { get; set; }

        public bool IsDefault()
        {
            return string.IsNullOrEmpty(Werkplek) ||
                   string.Equals(Werkplek, "default", StringComparison.CurrentCultureIgnoreCase);
        }

        public void InitWerkplek(string werkplek)
        {
            try
            {
                Werkplek = werkplek;
                UpdateLabelText();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateLabelText()
        {
            try
            {
                if (InvokeRequired)
                    BeginInvoke(new MethodInvoker(UpdateWerkplekInfo));
                else UpdateWerkplekInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateWerkplekInfo()
        {
            if (IsDefault())
            {
                // ximage.Image = Resources.operation;
                xVerwijderPersoneel.Visible = false;
                xnietingedeeld.Visible = true;
                xnietingedeeld.Checked = ToonNietIngedeeld;
                if (FieldTextGetter != null)
                    xpersoonInfo.Text = FieldTextGetter.Invoke(this);
                else
                    xpersoonInfo.Text = "Beheer hier alle bewerkingen.<br>" +
                                        "Voeg werkplaatsen toe om producties daarvoor in te delen.<br>" +
                                        "Sleep een bewerking naar de gewenste werkplek om ze in te delen.<br>";
                xknoppenpanel.Visible = false;
            }
            else
            {
                //ximage.Image = Resources.user_customer_person_13976;
                xknoppenpanel.Visible = SelectedBewerking != null && !IsDefault() && IsSelected;
                xVerwijderPersoneel.Visible = true;
                if (Parent is GroupBox group)
                {
                    if (SelectedBewerking != null && IsSelected)
                    {
                        var bw = SelectedBewerking;
                        group.Text = $"{Werkplek} {bw.Naam} van {bw.ArtikelNr} | {bw.ProductieNr}";
                    }
                    else
                    {
                        @group.Text = $"{Werkplek}";
                    }
                }

                if (SelectedBewerking != null)
                {
                    var wp = SelectedBewerking.GetWerkPlek(Werkplek, false);
                    if (wp != null && wp.IsActief())
                    {
                        if (SelectedBewerking.State == ProductieState.Gestart)
                        {
                            xStartKlus.Enabled = false;
                            xStopKlus.Enabled = true;
                        }
                        else
                        {
                            xStartKlus.Enabled = true;
                            xStopKlus.Enabled = false;
                        }
                    }
                    else
                    {
                        xStartKlus.Enabled = true;
                        xStopKlus.Enabled = false;
                    }
                }
                else
                {
                    xknoppenpanel.Visible = false;
                }

                if (FieldTextGetter != null)
                    xpersoonInfo.Text = FieldTextGetter.Invoke(this);
            }
        }

        public void SetBewerking(Bewerking bew)
        {
            if (IsDefault()) return;
            SelectedBewerking = bew;
            UpdateWerkplekInfo();
        }

        private void PersoonIndeling_DragEnter(object sender, DragEventArgs e)
        {
            var data = (GroupBox) e.Data.GetData(typeof(GroupBox));
            var falsecolor = Color.MistyRose;
            var truecolor = Color.LightGreen;
            if (data != null && data.Controls.Count > 0 && data.Controls[0] is WerkplekIndeling indeling &&
                !indeling.IsDefault())
            {
                BackColor = truecolor;
                e.Effect = DragDropEffects.Move;
                return;
            }

            if (e.Data.GetData("Producties") is ArrayList xdata)
                foreach (var x in xdata)
                    if (x is Bewerking bew)
                        if (!IsDefault())
                        {
                            var wps = Manager.BewerkingenLijst?.GetWerkplekken(bew.Naam);
                            if (wps == null || !wps.Any(w =>
                                    string.Equals(w, Werkplek, StringComparison.CurrentCultureIgnoreCase)))
                                continue;
                            var wp = bew.GetWerkPlek(Werkplek, false);
                            if (wp == null)
                            {
                                BackColor = truecolor;
                                e.Effect = DragDropEffects.Link;
                                return;
                            }
                        }

            BackColor = falsecolor;
            e.Effect = DragDropEffects.None;
        }

        private void PersoonIndeling_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetData("Producties") is ArrayList xdata)
                    foreach (var x in xdata)
                        if (x is Bewerking bew)
                            if (!IsDefault())
                            {
                                var wps = Manager.BewerkingenLijst?.GetWerkplekken(bew.Naam);
                                if (wps == null || !wps.Any(w =>
                                        string.Equals(w, Werkplek, StringComparison.CurrentCultureIgnoreCase)))
                                    continue;
                                var wp = bew.GetWerkPlek(Werkplek, true);
                                _ = bew.UpdateBewerking(null,
                                    $"[{bew.Naam}] Ingedeeld op {wp.Naam}").Result;
                            }

                BitMapCursor?.Dispose();
                BitMapCursor = null;
                _isDragging = false;
                OnMouseLeave(EventArgs.Empty);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void PersoonIndeling_DragLeave(object sender, EventArgs e)
        {
            OnMouseLeave(EventArgs.Empty);
        }

        private void xPersoonImage_Click(object sender, EventArgs e)
        {
            OnClick(EventArgs.Empty);
        }

        private void xPersoonImage_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(EventArgs.Empty);
        }

        private void xPersoonImage_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(EventArgs.Empty);
        }

        private void xVerwijderKlus_Click(object sender, EventArgs e)
        {
            if (SelectedBewerking == null || IsDefault()) return;
            if (!IsDefault() && SelectedBewerking != null)
            {
                if (SelectedBewerking == null) return;
                var xremove = SelectedBewerking.WerkPlekken.Where(x =>
                    string.Equals(Werkplek, x.Naam, StringComparison.CurrentCultureIgnoreCase)).ToList();
                var xtijd = xremove.Sum(x => x.TijdAanGewerkt());
                if (xtijd > 0)
                    if (XMessageBox.Show(Parent?.Parent?.Parent?.Parent?.Parent,
                            $"Er is {xtijd} uur aan {SelectedBewerking.Naam} op {Werkplek} gewerkt!\n\n" +
                            $"Weetje zeker dat je alsnog {Werkplek} uit {SelectedBewerking.Naam}({SelectedBewerking.ProductieNr})  wilt verwijderen?",
                            $"{Werkplek} Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) ==
                        DialogResult.No)
                        return;

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

            var change =
                $"{Werkplek} gestopt met {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!";
            var changed = false;
            var wp = SelectedBewerking.GetWerkPlek(Werkplek, false);
            if (wp == null) return;
            foreach (var xp in wp.Personen)
                if (xp.IngezetAanKlus(SelectedBewerking, false, out var klusjes))
                {
                    var xdp = Manager.Database.GetPersoneel(xp.PersoneelNaam).Result;
                    foreach (var klus in klusjes)
                    {
                        klus.Stop();
                        klus.IsActief = false;
                        changed = true;
                        xdp?.ReplaceKlus(klus);
                    }

                    if (xdp != null)
                        Manager.Database.UpSert(xdp, change);
                }

            if (changed)
            {
                xStartKlus.Enabled = true;
                xStopKlus.Enabled = false;
                _ = SelectedBewerking.UpdateBewerking(null,
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
                        var xdb = Manager.Database.GetPersoneel(per.PersoneelNaam).Result;
                        if (xdb != null)
                            if (per.IngezetAanKlus(wp.Path, true, out var klusjes))
                            {
                                klusjes.ForEach(x => xdb.ReplaceKlus(x));
                                var change =
                                    $"{xdb.PersoneelNaam} gestart met {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!";
                                Manager.Database?.UpSert(xdb, change);
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
                if (!ProductieListControl.StartBewerkingen(Parent, new[] {SelectedBewerking}))
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
            OnDoubleClick(EventArgs.Empty);
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            base.OnMouseDown(e);
            _mX = e.X;
            _mY = e.Y;
            _isDragging = false;
            //Cast the sender to control type youre using
            //Copy the control in a bitmap
            var bmp = new Bitmap(Parent.Width, Parent.Height);
            Parent.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
            bmp = bmp.ChangeOpacity(0.75f);
            //In a variable save the cursor with the image of your controler
            BitMapCursor = new Cursor(bmp.GetHicon());
            //this.DoDragDrop(this.Parent, DragDropEffects.Move);
        }

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
                        DoDragDrop(Parent, DragDropEffects.All);
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
            Cursor.Current = BitMapCursor;
            base.OnGiveFeedback(e);
        }

        public void IndelingMouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        public void IndelingMouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void xpersoonInfo_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            OnGiveFeedback(e);
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
    }
}