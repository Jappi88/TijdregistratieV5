﻿using System;
using Rpm.Productie;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProductieManager.Forms.Aantal.Controls;
using Rpm.Misc;
using Rpm.Various;

namespace Forms.Aantal
{
    public partial class AantalGemaaktProducties : MetroFramework.Forms.MetroForm
    {
        public List<Bewerking> Bewerkingen { get; private set; } 
        public string Title
        {
            get => base.Text;
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        public int LastchangedMinutes { get; set; }

        public AantalGemaaktProducties(List<Bewerking> producties, int lastchangedminutes = -1)
        {
            InitializeComponent();
            LastchangedMinutes = lastchangedminutes;
            Bewerkingen = producties;
            LoadProducties();
        }

        private bool xloading = false;
        private void LoadProducties()
        {
            try
            {
                if (xloading) return;
                xloading = true;
                var bws = GetBewerkingen();
                if (bws.Count == 0)
                {
                    Title = "Geen Actieve Producties";
                }
                else
                {
                    var x1 = bws.Count == 1 ? "Productie" : "Producties";
                    Title = $"Wijzig AantalGemaakt Van {bws.Count} Actieve {x1}";

                    xcontainer.Controls.Clear();
                    foreach (var bw in bws)
                    {
                        AddGroup(bw);
                    }

                    UpdateHeight();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            xloading = false;
        }

        private GroupBox AddGroup(Bewerking bw)
        {
            try
            {
                var group = new GroupBox();
                group.ForeColor = Color.DarkRed;
                group.Text = $"{bw.ArtikelNr}, {bw.ProductieNr} | {bw.Naam} Van {bw.Omschrijving}";
                group.Font = new Font(this.Font.FontFamily, 12, FontStyle.Bold);
                group.Dock = DockStyle.Top;

                var xaantal = new AantalChangerUI();
                xaantal.LoadAantalGemaakt(bw);
                group.Size = new Size(xaantal.Width + 25, xaantal.Height + 25);
                xaantal.Dock = DockStyle.Fill;
                group.Controls.Add(xaantal);
                xcontainer.Controls.Add(group);
                return group;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private List<Bewerking> GetBewerkingen()
        {
            return Bewerkingen.Where(x => x.IsAllowed() && string.Equals(
                x.GestartDoor, Manager.Opties.Username,
                StringComparison.CurrentCultureIgnoreCase)).OrderBy(x => x.WerkPlekken.FirstOrDefault()?.Naam ?? x.Naam).Reverse().ToList();
        }

        private void UpdateProducties()
        {
            try
            {
                if (xloading) return;
                xloading = true;
                var bws = GetBewerkingen();
                if (bws.Count == 0)
                {
                    Title = "Geen Actieve Producties";
                    xcontainer.Controls.Clear();
                }
                else
                {
                    var x1 = bws.Count == 1 ? "Productie" : "Producties";
                    Title = $"Wijzig AantalGemaakt Van {bws.Count} Actieve {x1}";
                    xcontainer.SuspendLayout();
                    var groups = xcontainer.Controls.Cast<GroupBox>().ToList();
                    
                    var xremove = groups.Where(x =>
                        x.Controls.Count > 0 && x.Controls[0] is AantalChangerUI ui &&
                        !Bewerkingen.Any(b => b.Equals(ui.Productie) || (LastchangedMinutes > -1 && ui.Productie.LaatstAantalUpdate.AddMinutes(LastchangedMinutes) >= DateTime.Now))).ToList();
                    xremove.ForEach(r => xcontainer.Controls.Remove(r));
                    var aantallen = groups.Select(x => x.Controls[0] as AantalChangerUI).ToList();
                    foreach (var bw in bws)
                    {
                        var ui = aantallen.FirstOrDefault(x => x.Productie.Equals(bw));
                        GroupBox group = null;
                        if (ui == null)
                        {
                            group = AddGroup(bw);
                        }
                        else
                        {
                            group = ui.Parent as GroupBox;
                            ui.LoadAantalGemaakt(bw);

                        }

                        if (group == null)
                            continue;
                        group.Text = $"{bw.ArtikelNr}, {bw.ProductieNr} | {bw.Naam} Van {bw.Omschrijving}";

                    }

                    xcontainer.ResumeLayout(true);
                    UpdateHeight();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            xloading = false;
        }

        public void UpdateProductie(ProductieFormulier form)
        {
            bool valid = form is {State: ProductieState.Gestart};
            bool init = false;
            if (Bewerkingen != null && Bewerkingen.Any(x => string.Equals(x.ProductieNr, form.ProductieNr, StringComparison.CurrentCultureIgnoreCase)))
            {
                if (!valid)
                    init = Bewerkingen.RemoveAll(x =>
                        string.Equals(x.ProductieNr, form.ProductieNr, StringComparison.CurrentCultureIgnoreCase)) > 0;
                else
                {
                    foreach (var bw in form.Bewerkingen)
                    {
                        var index = Bewerkingen.IndexOf(bw);
                        bool bwvalid = bw.State == ProductieState.Gestart && bw.IsAllowed() && string.Equals(
                            bw.GestartDoor, Manager.Opties?.Username,
                            StringComparison.CurrentCultureIgnoreCase);
                        if (index == -1)
                        {
                            if (bwvalid)
                            {
                                if (LastchangedMinutes > -1)
                                {
                                    if (bw.LaatstAantalUpdate.AddMinutes(LastchangedMinutes) < DateTime.Now)
                                    {
                                        Bewerkingen.Add(bw);
                                        init = true;
                                    }
                                }
                                else
                                {
                                    Bewerkingen.Add(bw);
                                    init = true;
                                }
                                
                            }
                        }
                        else
                        {
                            if (bwvalid)
                                Bewerkingen[index] = bw;
                            else
                            {
                                Bewerkingen.RemoveAt(index);
                                init = true;
                            }
                        }
                    }
                }
            }
            else
            {
                Bewerkingen ??= new List<Bewerking>();
                foreach (var bw in form.Bewerkingen)
                {
                    if (bw.State == ProductieState.Gestart && (LastchangedMinutes < 0 ||
                                                               bw.LaatstAantalUpdate.AddMinutes(
                                                                   LastchangedMinutes) < DateTime.Now))
                    {
                        bool bwvalid = bw.State == ProductieState.Gestart && bw.IsAllowed() && string.Equals(
                            bw.GestartDoor, Manager.Opties?.Username,
                            StringComparison.CurrentCultureIgnoreCase);
                        if (bwvalid)
                        {
                            Bewerkingen.Add(bw);
                            init = true;
                        }
                    }
                }
            }

            if (init)
                UpdateProducties();
        }

        private void UpdateHeight()
        {
            var height = 120;
            if (xcontainer.Controls.Count > 0)
            {
                for (int i = 0; i < xcontainer.Controls.Count; i++)
                {
                    if (xcontainer.Controls[i] is GroupBox group)
                        height += group.Height;
                }
            }

            if (height < 750)
            {
                this.Height = height;
                this.Invalidate();
            }
        }

        private void AantalGemaaktProducties_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            try
            {
                if (this.Disposing || this.IsDisposed) return;
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(() => UpdateProductie(changedform)));
                else
                    UpdateProductie(changedform);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AantalGemaaktProducties_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
        }
    }
}
