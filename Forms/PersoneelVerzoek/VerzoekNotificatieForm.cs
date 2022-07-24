using ProductieManager;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using Rpm.Verzoeken;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms.Verzoeken
{
    public partial class VerzoekNotificatieForm : MetroBase.BaseForm
    {
        public VerzoekEntry[] Verzoeken { get; private set; }

        public bool ClickAble { get; set; } = true;

        public VerzoekNotificatieForm()
        {
            InitializeComponent();
        }

        public bool EnableMenuButtons
        {
            get => xMenuPanel.Visible;
            set
            {
                xMenuPanel.Visible = value;
                this.Height = value ? (160 + 34) : 160;
                this.Invalidate();
            }
        }

        public Label TitleLabel => xtitle;
        public string MessageText { get => xmessage.Text;
            set => xmessage.Text = value;
        }

        public void InitVerzoeken(VerzoekEntry[] messages)
        {
            if (messages == null || messages.Length == 0)
            {
                if (this.Visible)
                    this.Close();
            }
            else
            {
                bool flag = Manager.LogedInGebruiker is { AccesLevel: AccesType.Manager };
                EnableMenuButtons = flag && messages.Length == 1;// && messages[0].Status is VerzoekStatus.InAfwachting;
                Verzoeken = messages.OrderBy(x => x.IngediendOp).ToArray();
                string msg = "";

                msg = string.Join("<br>", Verzoeken.Select(x => x.GetVerzoekMessage()));
                xmessage.Text = $"<span color='black'>{msg}</span>";
                var afwachting = messages.Where(x => x.Status == VerzoekStatus.InAfwachting).ToList();
                var gekeurd = messages.Where(x => x.Status == VerzoekStatus.GoedGekeurd).ToList();
                var afgekeurd = messages.Where(x => x.Status == VerzoekStatus.Afgekeurd).ToList();

                var a1 = afwachting.Count == 1 ? "verzoek" : "verzoeken";
                var b1 = gekeurd.Count == 1 ? "verzoek" : "verzoeken";
                var c1 = afgekeurd.Count == 1 ? "verzoek" : "verzoeken";

                var a2 = afwachting.Count > 0? $"{afwachting.Count} nieuwe {a1}": "";
                var b2 = gekeurd.Count > 0 ? $"{gekeurd.Count} {b1} Goedgekeurd" : "";
                var c2 = afgekeurd.Count > 0 ? $"{afgekeurd.Count} {c1} Afgekeurd" : "";

                var title = $"";
                if (!string.IsNullOrEmpty(a2))
                {
                    if (!string.IsNullOrEmpty(b2))
                    {
                        if (string.IsNullOrEmpty(c2))
                            title = $"{a2} en {b2}";
                        else
                        {
                            title = $"{a2}, {b2} en {c2}!";
                        }
                    }
                    else if (!string.IsNullOrEmpty(c2))
                    {
                        title = $"{a2} en {c2}!";
                    }
                    else
                    {
                        title = $"{a2}!";
                    }
                }
                else if (!string.IsNullOrEmpty(b2))
                {
                    if (!string.IsNullOrEmpty(c2))
                    {
                        title = $"{b2} en {c2}!";
                    }
                    else
                    {
                        title = $"{b2}!";
                    }
                }
                else if (!string.IsNullOrEmpty(c2))
                {
                    title = $"{c2}!";
                }

                TitleLabel.Text = title;
                xmessage.VerticalScroll.Value = xmessage.VerticalScroll.Maximum;
                xmessage.PerformLayout();
                xmessage.VerticalScroll.Value = xmessage.VerticalScroll.Maximum;
                Application.DoEvents();
                this.Invalidate();
            }
        }


        private void xclose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


        private void xMain_Click(object sender, EventArgs e)
        {
            OnMessageClicked(this);
        }

        private void xMain_MouseEnter(object sender, EventArgs e)
        {
            if (ClickAble)
            {
                xMainPanel.BackColor = Color.LightBlue;
                Cursor.Current = Cursors.Hand;
            }
        }

        private void xMain_MouseLeave(object sender, EventArgs e)
        {
            xMainPanel.BackColor = Color.Transparent;
            Cursor.Current = Cursors.Default;
        }

        private void xmessage_LinkClicked(object sender, TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs e)
        {
            if (Manager.Database == null || Manager.Database.IsDisposed) return;
            try
            {
                var prod = Manager.Database.GetProductie(e.Link, false);
                if (prod == null) return;
                var bew = prod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                Manager.FormulierActie(new object[] { prod, bew }, MainAktie.OpenProductie);
                e.Handled = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public event EventHandler MessageClicked;
        protected virtual void OnMessageClicked(object sender)
        {
            this.MessageClicked?.Invoke(sender, EventArgs.Empty);
        }

        private void xGoedkeuren_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manager.Verzoeken?.Database == null || !Manager.Verzoeken.Database.CanRead)
                {
                    this.Close();
                    return;
                }
                if (Manager.LogedInGebruiker is { AccesLevel: AccesType.Manager })
                {
                    var verz = Verzoeken.ToList();
                    if (verz.Count > 0)
                    {
                        var note = "";
                        if (verz.Any(x => x.VerzoekSoort != VerzoekType.Verwijderen))
                        {
                            var tf = new TextFieldEditor();
                            tf.Title = "Goedkeuring Bericht";
                            tf.UseSecondary = false;
                            tf.MultiLine = true;
                            tf.MinimalTextLength = 0;
                            tf.Style = MetroFramework.MetroColorStyle.Green;
                            tf.FieldImage = Resources.notification_done_114461;
                            if (tf.ShowDialog(Application.OpenForms.OfType<Mainform>().FirstOrDefault()) != DialogResult.OK) return;
                            note = tf.SelectedText?.Trim();
                        }
                        foreach (var ent in verz)
                        {
                            ent.GelezenDoor = new System.Collections.Generic.List<string>() { Manager.Opties.Username };
                            ent.Status = VerzoekStatus.GoedGekeurd;
                            ent.VerzoekReactie = note;
                            ent.ReactieDoor = Manager.LogedInGebruiker.Username;
                            ent.ReactieOp = DateTime.Now;
                            var pers = Manager.Database.xGetPersoneel(ent.PersoneelNaam);
                            var tijdentry = new TijdEntry(ent.StartDatum, ent.EindDatum, pers?.WerkRooster);
                            switch (ent.VerzoekSoort)
                            {
                                case VerzoekType.Vrij:
                                    if(pers != null)
                                    {
                                        pers.VrijeDagen.Add(tijdentry);
                                        Manager.Database.UpSert(pers,$"Verlof voor {pers.PersoneelNaam} aangepast!");
                                    }
                                    Manager.Verzoeken.UpdateVerzoek(ent);
                                    break;
                                case VerzoekType.OverWerk:
                                    Manager.Verzoeken.UpdateVerzoek(ent);
                                    break;
                                case VerzoekType.Verwijderen:
                                    if (pers != null)
                                    {
                                        if(pers.VrijeDagen.Remove(tijdentry))
                                        {
                                            Manager.Database.UpSert(pers, $"Verlof voor {pers.PersoneelNaam} aangepast!");
                                        }
                                    }
                                    Manager.Verzoeken.Database.Delete(ent.ID.ToString());
                                    break;
                            }
                        }
                    }
                }
                this.Close();
            }
            catch(Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xAfkeuren_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manager.Verzoeken?.Database == null || !Manager.Verzoeken.Database.CanRead)
                {
                    this.Close();
                    return;
                }
                if (Manager.LogedInGebruiker is { AccesLevel: AccesType.Manager })
                {
                    var verz = Verzoeken.ToList();
                    if (verz.Count > 0)
                    {
                        var note = "";
                        if (verz.Any(x => x.VerzoekSoort != VerzoekType.Verwijderen))
                        {
                            var tf = new TextFieldEditor();
                            tf.Title = "Reden voor Afwijzing";
                            tf.UseSecondary = false;
                            tf.MultiLine = true;
                            tf.Style = MetroFramework.MetroColorStyle.Red;
                            tf.MinimalTextLength = 0;
                            tf.FieldImage = Resources.cancel_stop_exit_64x64;
                            if (tf.ShowDialog(Application.OpenForms.OfType<Mainform>().FirstOrDefault()) != DialogResult.OK) return;
                            note = tf.SelectedText?.Trim();
                        }
                        foreach (var ent in verz)
                        {
                            ent.GelezenDoor = new System.Collections.Generic.List<string>() { Manager.Opties.Username };
                            ent.Status = VerzoekStatus.Afgekeurd;
                            ent.VerzoekReactie = note;
                            ent.ReactieOp = DateTime.Now;
                            ent.ReactieDoor = Manager.LogedInGebruiker.Username;
                            Manager.Verzoeken.UpdateVerzoek(ent);
                        }
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }
    }
}
