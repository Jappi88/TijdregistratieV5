﻿using Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace Controls
{
    public delegate string WerkplekTextGetterHandler(WerkplekIndeling indeling);
    public partial class WerkplekIndeling : UserControl
    {

        public KeyValuePair<string, List<Bewerking>> Werkplek { get;  set; }

        public Bewerking SelectedBewerking { get; set; }
        public bool IsSelected { get; set; }

        public WerkplekTextGetterHandler FieldTextGetter { get; set; }

        public WerkplekIndeling()
        {
            InitializeComponent();
        }

        public void InitWerkplek(KeyValuePair<string, List<Bewerking>> werkplek)
        {
            try
            {
                Werkplek = werkplek;
                UpdateLabelText();
            }
            catch (Exception e)
            {

            }
        }

        public void UpdateLabelText()
        {
            try
            {
                if (this.InvokeRequired)
                    this.BeginInvoke(new MethodInvoker(UpdateWerkplekInfo));
                else UpdateWerkplekInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateWerkplekInfo()
        {
            //xpersoonInfo.AutoScroll = false;
            //xpersoonInfo.HorizontalScroll.Maximum = 0;
            //xpersoonInfo.VerticalScroll.Visible = false;
            //xpersoonInfo.VerticalScroll.Enabled = false;
            //xpersoonInfo.VerticalScroll.Maximum = 0;
            //xpersoonInfo.HorizontalScroll.Visible = false;
            //xpersoonInfo.HorizontalScroll.Enabled = false;
            if (Werkplek.IsDefault())
            {
               // ximage.Image = Resources.operation;
               xVerwijderPersoneel.Visible = false;
                if (FieldTextGetter != null)
                    xpersoonInfo.Text = FieldTextGetter.Invoke(this);
                else
                    xpersoonInfo.Text = $"Beheer hier alle bewerkingen.<br>" +
                                        $"Voeg werkplaatsen toe om producties daarvoor in te delen.<br>" +
                                        $"Sleep een bewerking naar de gewenste werkplek om ze in te delen.<br>";
                xknoppenpanel.Visible = false;
            }
            else
            {
                //ximage.Image = Resources.user_customer_person_13976;
                xknoppenpanel.Visible = SelectedBewerking != null && !Werkplek.IsDefault() && IsSelected;
                xVerwijderPersoneel.Visible = true;
                if (Parent is GroupBox group)
                {
                    if (SelectedBewerking != null && IsSelected)
                    {
                        var bw = SelectedBewerking;
                        group.Text = $"{Werkplek.Key} {bw.Naam} van {bw.ArtikelNr} | {bw.ProductieNr}";
                    }
                    else group.Text = $"{Werkplek.Key}";
                }
                if (SelectedBewerking != null && !Werkplek.IsDefault())
                {
                    var wp = SelectedBewerking.GetWerkPlek(Werkplek.Key, false);
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
                else
                {
                    var klusjes = Werkplek.Value.Where(x =>
                        x.State != ProductieState.Gereed && x.State != ProductieState.Verwijderd).ToList();
                    xpersoonInfo.Text = $"{Werkplek.Key} Indeling.<br>" +
                                        $"Ingedeeld met {klusjes.Count} productie(s).<br>" +
                                        $"Bezig met {klusjes.Count(x => x.State == ProductieState.Gestart)} producties(s) ({klusjes.Sum(x=> x.TijdAanGewerkt())} uur).";
                }
            }

        }

        public void SetBewerking(Bewerking bew)
        {
            if (Werkplek.IsDefault()) return;
            SelectedBewerking = bew;
            UpdateWerkplekInfo();
        }

        private void PersoonIndeling_DragEnter(object sender, DragEventArgs e)
        {
            var data = (GroupBox)e.Data.GetData(typeof(GroupBox));
            var falsecolor = Color.MistyRose;
            var truecolor = Color.LightGreen;
            if (data != null && data.Controls.Count > 0 && data.Controls[0] is PersoonIndeling indeling && indeling.Persoon != null)
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
                        if (!Werkplek.IsDefault())
                        {
                            var wps = Manager.BewerkingenLijst?.GetWerkplekken(bew.Naam);
                            if (wps == null || !wps.Any(w =>
                                    string.Equals(w, Werkplek.Key, StringComparison.CurrentCultureIgnoreCase)))
                                continue;
                            var wp = bew.GetWerkPlek(Werkplek.Key, false);
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
                            if (!Werkplek.IsDefault())
                            {
                                var wp = bew.GetWerkPlek(Werkplek.Key, true); 
                                bew.UpdateBewerking(null,
                                $"{wp.Naam} Ingedeeld voor [{bew.Naam}] van {bew.Omschrijving}").Wait(2000);;
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
            if (SelectedBewerking == null || Werkplek.IsDefault()) return;
            if (!Werkplek.IsDefault() && SelectedBewerking != null)
            {
                if (SelectedBewerking == null) return;
                if (SelectedBewerking.WerkPlekken.RemoveAll(x =>
                        string.Equals(Werkplek.Key, x.Naam, StringComparison.CurrentCultureIgnoreCase)) > 0)
                    SelectedBewerking.UpdateBewerking(null,
                            $"{Werkplek.Key} verwijderd uit [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!")
                        .Wait(2000);

            }
        }

        private void xStopKlus_Click(object sender, EventArgs e)
        {
            if (SelectedBewerking == null || Werkplek.IsDefault()) return;

            string change =
                $"{Werkplek.Key} gestopt met {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!";
            bool changed = false;
            var wp = SelectedBewerking.GetWerkPlek(Werkplek.Key, false);
            if (wp == null) return;
            foreach (var xp in wp.Personen)
            {
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
            }

            if (changed)
            {
                xStartKlus.Enabled = true;
                xStopKlus.Enabled = false;
                _ = SelectedBewerking.UpdateBewerking(null,
                    $"{Werkplek.Key} UPDATE: {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!");
            }
        }

        private void xStartKlus_Click(object sender, EventArgs e)
        {
            if (SelectedBewerking == null || Werkplek.IsDefault() ||
                SelectedBewerking.State is ProductieState.Verwijderd or ProductieState.Gereed) return;
            bool xstarted = false;
            var wp = SelectedBewerking.GetWerkPlek(Werkplek.Key, true);
            if (!wp.IsActief() && wp.Personen.Count > 0)
            {
                foreach (var per in wp.Personen)
                {
                    SelectedBewerking.ZetPersoneelActief(per.PersoneelNaam, wp.Naam, true);
                    var xdb = Manager.Database.GetPersoneel(per.PersoneelNaam).Result;
                    if (xdb != null)
                    {
                        if (per.IngezetAanKlus(wp.Path, true, out var klusjes))
                        {
                            klusjes.ForEach(x => xdb.ReplaceKlus(x));
                            string change =
                                $"{xdb.PersoneelNaam} gestart met {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!";
                            Manager.Database?.UpSert(xdb, change);
                        }
                    }
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
                    $"{Werkplek.Key} UPDATE: {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!");
            }
            xStartKlus.Enabled = false;
            xStopKlus.Enabled = true;
        }

        private void xPersoonImage_DoubleClick(object sender, EventArgs e)
        {
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

        protected override void OnClick(EventArgs e)
        {
            Focus();
            base.OnClick(e);
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
                if (e.Button == MouseButtons.Left && _DDradius > 0 && !Werkplek.IsDefault())
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
    }
}