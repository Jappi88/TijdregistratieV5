using Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace Controls
{
    public delegate string FieldTextGetterHandler(PersoonIndeling indeling);
    public partial class PersoonIndeling : UserControl
    {

        public Personeel Persoon { get;  set; }

        public Bewerking SelectedBewerking { get; set; }
        public bool IsSelected { get; set; }

        public FieldTextGetterHandler FieldTextGetter { get; set; }

        public PersoonIndeling()
        {
            InitializeComponent();
        }

        public void InitPersoneel(Personeel persoon)
        {
            try
            {
                Persoon = persoon;
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
                    this.BeginInvoke(new MethodInvoker(UpdateGebruikerInfo));
                else UpdateGebruikerInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateGebruikerInfo()
        {
            //xpersoonInfo.AutoScroll = false;
            //xpersoonInfo.HorizontalScroll.Maximum = 0;
            //xpersoonInfo.VerticalScroll.Visible = false;
            //xpersoonInfo.VerticalScroll.Enabled = false;
            //xpersoonInfo.VerticalScroll.Maximum = 0;
            //xpersoonInfo.HorizontalScroll.Visible = false;
            //xpersoonInfo.HorizontalScroll.Enabled = false;
            if (Persoon == null)
            {
               // ximage.Image = Resources.operation;
               xVerwijderPersoneel.Visible = false;
                if (FieldTextGetter != null)
                    xpersoonInfo.Text = FieldTextGetter.Invoke(this);
                else
                    xpersoonInfo.Text = $"Beheer hier alle bewerkingen.<br>" +
                                        $"Voeg personen toe om producties daarvoor in te delen.<br>" +
                                        $"Sleep een bewerking naar de gewenste personen om ze in te delen.<br>";
                xknoppenpanel.Visible = false;
            }
            else
            {
                //ximage.Image = Resources.user_customer_person_13976;
                xknoppenpanel.Visible = SelectedBewerking != null && Persoon != null && IsSelected;
                xVerwijderPersoneel.Visible = true;
                if (Parent is GroupBox group)
                {
                    if (SelectedBewerking != null && IsSelected)
                    {
                        var bw = SelectedBewerking;
                        group.Text = $"{Persoon.PersoneelNaam} {bw.Naam} van {bw.ArtikelNr} | {bw.ProductieNr}";
                    }
                    else group.Text = $"{Persoon.PersoneelNaam}";
                }
                if (SelectedBewerking != null && Persoon != null)
                {
                    if (Persoon.IngezetAanKlus(SelectedBewerking, true, out var xklusjes))
                    {
                        if (xklusjes.Any(x => x.Status == ProductieState.Gestart))
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
                    var klusjes = Persoon.Klusjes.Where(x =>
                        x.Status != ProductieState.Gereed && x.Status != ProductieState.Verwijderd).ToList();
                    xpersoonInfo.Text = $"{Persoon.PersoneelNaam} Indeling.<br>" +
                                        $"Ingezet op {klusjes.Count} klusje(s).<br>" +
                                        $"Werkt aan {klusjes.Count(x => x.Status == ProductieState.Gestart)} klusje(s) ({Persoon.TijdGewerkt} uur).";
                }
            }

        }

        public void SetBewerking(Bewerking bew)
        {
            if (Persoon == null) return;
            SelectedBewerking = bew;
            UpdateGebruikerInfo();
        }

        public async void InitPersoneel(string persoonnaam)
        {
            try
            {
                if (Manager.Database?.PersoneelLijst == null || Manager.Database.IsDisposed)
                    throw new Exception("Database is niet beschikbaar!");
                var xpers = await Manager.Database.GetPersoneel(persoonnaam);
                if (xpers == null) throw new Exception($"{persoonnaam} niet gevonden!");
                InitPersoneel(xpers);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
                        if (Persoon != null)
                        {
                            if (!Persoon.IngezetAanKlus(bew, false, out var klusjes))
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
                            if (Persoon != null && Persoon.IngezetAanKlus(bew, false, out var klusjes))
                            {
                                if (klusjes.Count > 0)
                                    continue;
                                var wps = Manager.BewerkingenLijst.GetWerkplekken(bew.Naam.Split('[')[0]);
                                if (wps == null || wps.Count == 0)
                                    throw new Exception($"Geen werkplaatsen beschikbaar voor {bew.Naam}!");
                                var wpchooser = new WerkPlekChooser(wps.ToArray(), null);
                                if (wpchooser.ShowDialog() == DialogResult.OK)
                                {
                                    var xwp = wpchooser.SelectedName;
                                    if (!bew.AddPersoneel(Persoon, xwp))
                                        throw new Exception(
                                            $"Het is niet gelukt om {Persoon.PersoneelNaam} toe te voegen!");
                                }
                            }
                            else
                            {
                                var xklus = CreateKlus(bew);
                                if (xklus == null) continue;
                                var xperss = Persoon.CreateCopy();
                                xperss.Werkplek = xklus.WerkPlek;
                                xperss.WerktAan = bew.Path;
                                xperss.Klusjes.Clear();
                                xperss.Klusjes.Add(xklus);
                                bew.AddPersoneel(xperss, xklus.WerkPlek);
                                var xdb = Manager.Database.GetPersoneel(xperss.PersoneelNaam).Result;
                                if (xdb != null)
                                {
                                    xdb.ReplaceKlus(xklus);
                                    Manager.Database.UpSert(xdb, $"{xdb.PersoneelNaam} op {bew.Path} gezet").Wait(2000);;
                                }
                            }

                            bew.UpdateBewerking(null,
                                $"{Persoon} Toegevoegd op [{bew.Naam}] van {bew.Omschrijving}").Wait(2000);;
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
            if (SelectedBewerking == null || Persoon == null) return;
            if (Persoon.IngezetAanKlus(SelectedBewerking, false, out var klusjes))
            {
                var xdb = Manager.Database.GetPersoneel(Persoon.PersoneelNaam).Result;
                if (SelectedBewerking == null) return;
                if (xdb != null)
                {
                    foreach (var klus in klusjes)
                        xdb.Klusjes.Remove(klus);
                    Manager.Database.UpSert(xdb,
                        $"Klusjes van {Persoon.PersoneelNaam} [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}] verwijderd!").Wait(2000);;
                }

                foreach (var wp in SelectedBewerking.WerkPlekken)
                {
                    wp.RemovePersoon(Persoon.PersoneelNaam);
                }

                SelectedBewerking.UpdateBewerking(null,
                    $"{Persoon.PersoneelNaam} verwijderd uit [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!").Wait(2000);
                
            }
        }

        private void xStopKlus_Click(object sender, EventArgs e)
        {
            if (SelectedBewerking == null || Persoon == null) return;
            //var xdb = Manager.Database.GetPersoneel(Persoon.PersoneelNaam).Result;
            //if (xdb != null)
            //{
            //    if (xdb.IngezetAanKlus(SelectedBewerking, false, out var klusjes))
            //    {
            //        foreach (var klus in klusjes)
            //        {
            //            klus.Stop();
            //        }

            //        Manager.Database.UpSert(xdb,
            //            $"Klusjes van {Persoon.PersoneelNaam} [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}] gestopt!").Wait(2000);;
            //    }
            //}

            string change =
                $"{Persoon.PersoneelNaam} gestopt met {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!";
            bool changed = false;
            foreach (var wp in SelectedBewerking.WerkPlekken)
            {
                foreach (var xp in wp.Personen)
                {
                    if (string.Equals(xp.PersoneelNaam, Persoon.PersoneelNaam,
                            StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (xp.IngezetAanKlus(SelectedBewerking, false, out var klusjes))
                        {
                            foreach (var klus in klusjes)
                            {
                                klus.Stop();
                                klus.IsActief = false;
                                Persoon.ReplaceKlus(klus);
                                changed = true;
                            }
                          
                        }
                    }
                }
            }

            if (changed)
                Manager.Database.UpSert(Persoon, change);
            xStartKlus.Enabled = true;
            xStopKlus.Enabled = false;
            _ = SelectedBewerking.UpdateBewerking(null,
                $"{Persoon.PersoneelNaam} UPDATE: {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!");
        }

        private Klus CreateKlus(Bewerking bewerking)
        {
            bewerking ??= SelectedBewerking;
            if (bewerking == null || Persoon == null) return null;
            var wpchooser = new WerkPlekChooser(Manager.GetWerkplekken(bewerking.Naam.Split('[')[0]), null);
            wpchooser.Title =
                $"Kies een werkplek voor {Persoon.PersoneelNaam} op {bewerking.Naam} van {bewerking.ArtikelNr}|{bewerking.ProductieNr}";
            if (wpchooser.ShowDialog() == DialogResult.OK)
            {
                var xklus = new Klus(Persoon, bewerking, wpchooser.SelectedName);
                xklus.IsActief = true;
                return xklus;
            }

            return null;
        }

        private void xStartKlus_Click(object sender, EventArgs e)
        {
            if (SelectedBewerking == null || Persoon == null ||
                SelectedBewerking.State is ProductieState.Verwijderd or ProductieState.Gereed) return;
            bool xstarted = false;
            foreach (var wp in SelectedBewerking.WerkPlekken)
            {
                var xpers = wp.Personen.FirstOrDefault(x =>
                    string.Equals(Persoon.PersoneelNaam, x.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase));
                if (xpers != null)
                {
                    if (xpers.IngezetAanKlus(SelectedBewerking, false, out var klusjes))
                    {
                        foreach (var klus in klusjes)
                        {
                            if (klus.Status != ProductieState.Gestart)
                            {
                                klus.IsActief = true;
                                if (SelectedBewerking.State == ProductieState.Gestart)
                                {
                                    klus.Start();
                                }

                                Persoon.ReplaceKlus(klus);
                            }
                        }

                        xstarted = true;
                    }
                }
            }

            if (!xstarted)
            {
                var xklus = CreateKlus(SelectedBewerking);
                if (xklus == null) return;
                Persoon.Werkplek = xklus.WerkPlek;
                Persoon.WerktAan = SelectedBewerking.Path;
                var xperss = Persoon.CreateCopy();
                xperss.Klusjes.Clear();
                xperss.Klusjes.Add(xklus);
                Persoon.ReplaceKlus(xklus);
                SelectedBewerking.AddPersoneel(xperss, xklus.WerkPlek);
                if (SelectedBewerking.State == ProductieState.Gestart)
                    xklus.Start();
            }
            xStartKlus.Enabled = false;
            xStopKlus.Enabled = true;
            if (SelectedBewerking.State == ProductieState.Gestopt)
            {
                _ = SelectedBewerking.StartProductie(true, true,true);
            }
            else
            {
                string change =
                    $"{Persoon.PersoneelNaam} gestart met {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!";
                Manager.Database?.UpSert(Persoon, change);
                _ = SelectedBewerking.UpdateBewerking(null,
                    $"{Persoon.PersoneelNaam} UPDATE: {SelectedBewerking.Naam} van [{SelectedBewerking.ArtikelNr} | {SelectedBewerking.ProductieNr}]!");
            }
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
                    e.Callback(Resources.user_customer_person_13976);
                    break;
            }
            e.Handled = true;
        }

        private void xVerwijderPersoneel_Click(object sender, EventArgs e)
        {
            OnVerwijderPersoneel(EventArgs.Empty);
        }

        public event EventHandler VerwijderPersoneel;

        protected void OnVerwijderPersoneel(EventArgs e)
        {
            VerwijderPersoneel?.Invoke(this, e);
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
                if (e.Button == MouseButtons.Left && _DDradius > 0 && Persoon != null)
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
