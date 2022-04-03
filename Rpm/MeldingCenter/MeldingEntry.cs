using MetroFramework;
using ProductieManager.Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Rpm.MeldingCenter
{
    public class MeldingEntry
    {
        public int ID => CreateHashCode();
        public byte[] ImageData { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Recievers { get; set; } = new List<string>();
        public List<string> ReadUsers { get; set; } = new List<string>();
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string Action { get; set; }
        public string ActionID { get; set; }
        public int ActionViewIndex { get; set; }
        public string MessageID { get; set; }
        public string MessageType { get; set; }
        public bool ShowMelding()
        {
            try
            {
                if (string.IsNullOrEmpty(Body)) return false;
                var ximage = ImageData.ImageFromBytes();
                if (Manager.OnRequestRespondDialog(Body, Subject, MessageBoxButtons.OK, MessageBoxIcon.Information,
                        null,
                        null, ximage, MetroColorStyle.Red) == DialogResult.OK)
                {
                    UpdateRead(true);
                    if (!string.IsNullOrEmpty(Action))
                    {
                        switch (Action.ToLower())
                        {
                            case "openproductie":
                                ShowProductie();
                                break;
                            case "openbijlage":
                                ShowBijlage();
                                break;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public void ShowProductie()
        {
            try
            {
                if(string.IsNullOrEmpty(Action) || string.IsNullOrEmpty(ActionID)) return;
                if (Manager.Database != null)
                {
                    string[] ids = ActionID.Split(';');
                    if (ids.Length > 0)
                    {
                        var bws = Manager.Database.GetBewerkingen(ViewState.Gestart, true, null, null,false).Result;
                        foreach (string id in ids)
                        {
                            var werk = Werk.FromPath(id.Trim());
                            if (werk.IsValid)
                            {
                                Manager.FormulierActie(
                                    new object[] { werk.Formulier, werk.Bewerking, true, ActionViewIndex },
                                    MainAktie.OpenProductie);
                                continue;
                            }

                            var bw = bws.FirstOrDefault(x =>
                                string.Equals(x.ArtikelNr, id, StringComparison.CurrentCultureIgnoreCase));
                            if (bw != null)
                            {
                                Manager.FormulierActie(new object[] { bw.Parent, bw, true, ActionViewIndex },
                                    MainAktie.OpenProductie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowBijlage()
        {
            try
            {
                if (string.IsNullOrEmpty(Action) || string.IsNullOrEmpty(ActionID)) return;
                Manager.FormulierActie(new object[] {ActionID}, MainAktie.OpenBijlage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool ForMe()
        {
            return Recievers.Count == 0 || Recievers.Any(x =>
                string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool HasRead()
        {
            return ReadUsers.Any(x =>
                string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool IsAllowed()
        {
            bool flag = Manager.Opties != null && Manager.LogedInGebruiker is
                { AccesLevel: >= AccesType.ProductieBasis };
            if (flag && !string.IsNullOrEmpty(MessageType))
            {
                switch (MessageType.ToLower().Trim())
                {
                    case "toevoegen":
                        break;
                    case "wijzig":
                        break;
                    case "bijlage":
                        flag &= Manager.Opties.ToonNieweBijlageMelding;
                        break;
                    case "melding":
                        break;
                }
            }

            return flag;
        }

        public bool ShouldShow()
        {
            return IsAllowed() && DateAdded.AddDays(2) > DateTime.Now && ForMe() && !HasRead();
        }

        public void UpdateRead(bool read)
        {
            ReadUsers.RemoveAll(x =>
                string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
            if (read && !string.IsNullOrEmpty(Manager.Opties?.Username))
                ReadUsers.Add(Manager.Opties.Username);
        }

        public override bool Equals(object obj)
        {
            if (obj is MeldingEntry melding)
            {
                string xrec = string.Join(", ", Recievers);
                string xxrec = string.Join(", ", melding.Recievers);
                var xret = string.Equals(melding.Body, Body, StringComparison.CurrentCultureIgnoreCase);
                xret &= string.Equals(melding.Subject, Subject, StringComparison.CurrentCultureIgnoreCase);
                xret &= string.Equals(xxrec, xrec, StringComparison.CurrentCultureIgnoreCase);
                xret &= string.Equals(Action, melding.Action, StringComparison.CurrentCultureIgnoreCase);
                xret &= string.Equals(ActionID, melding.ActionID, StringComparison.CurrentCultureIgnoreCase);
                xret &= string.Equals(MessageID, melding.MessageID, StringComparison.CurrentCultureIgnoreCase);
                return xret;
            }
            return false;
        }

        private int CreateHashCode()
        {
            var xhash = 0;
            xhash ^= Body?.GetHashCode() ?? 0;
            xhash ^= Subject?.GetHashCode() ?? 0;
            string xrec = string.Join(", ", Recievers);
            xhash ^= xrec.GetHashCode();
            xhash ^= Action?.GetHashCode() ?? 0;
            xhash ^= ActionID?.GetHashCode() ?? 0;
            xhash ^= MessageID?.GetHashCode()??0;
            return xhash;
        }

        public override int GetHashCode()
        {
            return CreateHashCode();
        }
    }
}
