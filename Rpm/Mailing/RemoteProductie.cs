using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using ProductieManager.Rpm.Mailing;
using ProductieManager.Rpm.Productie;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailMessage = System.Net.Mail.MailMessage;

namespace Rpm.Mailing
{
    public class RemoteProductie
    {
        private static bool _IsProcessing;

        public static bool ProcessInkomingMail(MimeMessage message)
        {
            try
            {
                if (message == null) return false;
                var body = "";
                var mail = message;
                if(Manager.Opties.InkomendMail.OnlyAllowedSenders)
                {
                    if (Manager.Opties?.InkomendMail?.AllowedSenders == null) return false;
                    if (Manager.Opties.InkomendMail.AllowedSenders.Count == 0) return false;
                    if (!Manager.Opties.InkomendMail.AllowedSenders.Any(x => string.Equals(x, ((MailboxAddress)message.From?.First())?.Address, StringComparison.CurrentCultureIgnoreCase)))
                        return false;
                }
                if (Manager.Opties?.InkomendMail?.AllowedActions == null)
                    return false;
                body += mail.TextBody ?? HtmlUtilities.ConvertToPlainText(mail.HtmlBody);

                var xendindex = body.IndexOf("Met vriendelijke groet", StringComparison.CurrentCultureIgnoreCase);
                if(xendindex == -1)
                    xendindex = body.IndexOf("Verzonden vanuit", StringComparison.CurrentCultureIgnoreCase);
                if (xendindex == -1)
                    xendindex = body.IndexOf("Verstuurd vanaf mijn", StringComparison.CurrentCultureIgnoreCase);
                if (xendindex > -1)
                {
                    body = body.Substring(0, xendindex);
                }
                var xonderwerp = ClearSubject(mail.Subject?.Trim() ?? "");
                var x1 = xonderwerp.GetBlockBySeperator(':', false).Trim();
                var x2 = xonderwerp.GetBlockBySeperator(':', true).Trim();
                var ret = true;
                switch (x1.ToLower())
                {
                    case "wijzig":
                        if (Manager.Opties.InkomendMail.AllowedActions.All(x => x != MessageAction.ProductieWijziging))
                            return false;
                        if (!string.IsNullOrEmpty(x1) && !string.IsNullOrEmpty(body))
                        {
                            var logs = new List<string>();
                            if (WijzigData(body, logs))
                            {
                                var att = mail.Attachments?.FirstOrDefault(x => x.ContentDisposition != null && x.ContentDisposition.Disposition != "inline");
                                byte[] xmg = GetMessageData((MimePart)att);
                                var xprods = logs.Where(x => x.StartsWith("["));
                                var xactionids = new List<string>();
                                foreach(var log in xprods)
                                {
                                    var xend = log.IndexOf("]");
                                    if(xend > -1)
                                        xactionids.Add(log.Substring(1, xend - 1));
                                }
                                string action = "openproductie";
                                string actionid = string.Join(";", xactionids);
                                Manager.Meldingen.CreateMelding(string.Join("\n", logs),
                                    $"Productie Wijzigingen van {mail.From?.First()?.Name}",mail.MessageId,x1, new List<string>(), xmg,
                                    true,false, xactionids.Count > 0? action: null, xactionids.Count > 0 ? actionid : null);
                            }
                        }
                        break;
                    case "bijlage":
                        if (Manager.Opties.InkomendMail.AllowedActions.All(x => x != MessageAction.BijlageUpdate))
                            return false;
                        if (!string.IsNullOrEmpty(x2))
                        {
                            var att = mail.Attachments?.Cast<MimePart>().ToList() ?? new List<MimePart>();
                            var log = new StringBuilder();
                            int done = 0;
                            var id = x2;
                            if (id.Contains("\\") || id.Contains("/"))
                            {
                                var xindex = id.IndexOf("\\", StringComparison.CurrentCultureIgnoreCase);
                                if (xindex == -1)
                                    xindex = id.IndexOf("/", StringComparison.CurrentCultureIgnoreCase);
                                if (xindex > -1)
                                {
                                    id = id.Substring(0, xindex);
                                }
                            }
                            foreach (var x in att)
                            {
                                var xname = x?.FileName ?? x.ContentDisposition?.FileName;
                                if (string.IsNullOrEmpty(xname))
                                    continue;
                                if (BijlageBeheer.UpdateBijlage(x2, GetMessageData(x), xname))
                                {
                                    done++;
                                    log.AppendLine($"'{xname}' toegevoegd als bijlage in \\{x2}");
                                }
                            }
                            if(done > 0 && Manager.Meldingen?.Database != null && !Manager.Meldingen.IsDisposed)
                            {
                                var melding = Manager.Meldingen.CreateMelding(log.ToString(), "Bijlages toegevoegd door " + mail.From?.First()?.Name,mail.MessageId,x1,
                                    new List<string>(), null, true,false, "openbijlage", id,11);
                            }
                        }
                        break;
                    case "melding":
                        if (Manager.Opties.InkomendMail.AllowedActions.All(x => x != MessageAction.AlgemeneMelding))
                            return false;
                        if (Manager.Meldingen == null || Manager.Meldingen.IsDisposed)
                        {
                            ret = false;
                            break;
                        }
                        if (!string.IsNullOrEmpty(body))
                        {
                            var att = mail.Attachments?.FirstOrDefault(x=> x.ContentDisposition != null && x.ContentDisposition.Disposition != "inline");

                            byte[] xmg = GetMessageData((MimePart) att);
                            var xaction = GetActionFromBody(ref body, out var actionid);
                            var melding = Manager.Meldingen.CreateMelding(body, "Melding van " + mail.From?.First()?.Name,mail.MessageId,x1,
                                    x2.Split(';').ToList(), xmg, true, false, xaction, actionid);
                            ret = melding != null;
                        }
                        break;
                    case "toevoegen":
                        var xatt = mail.Attachments?.Cast<MimePart>().ToList() ?? new List<MimePart>();
                        var xlog = new StringBuilder();
                        var ids = new List<string>();
                        foreach (var x in xatt)
                        {
                            var xname = x?.FileName ?? x.ContentDisposition?.FileName;
                            if (string.IsNullOrEmpty(xname))
                                continue;
                            var ext = Path.GetExtension(xname);
                            if (string.IsNullOrEmpty(ext) || ext.ToLower() != ".pdf")
                                continue;
                            var filename = Path.Combine(Path.GetTempPath(), "tempproductie" + ext);
                            int count = 0;
                           
                            if(SaveMessageData(x,filename))
                            {
                                var msg = Manager.AddProductie(filename, true, true).Result;
                                if(msg.Count > 0)
                                {
                                    foreach(var m in msg)
                                    {
                                        if ((m.MessageType == MsgType.Success || m.MessageType == MsgType.Info) && !string.IsNullOrEmpty(m.ProductieNummer))
                                        {
                                            count++;
                                            xlog.AppendLine(m.Message);
                                            ids.Add(m.ProductieNummer);
                                        }
                                    }
                                }
                            }
                        }
                        if (ids.Count > 0 && Manager.Meldingen?.Database != null && !Manager.Meldingen.IsDisposed)
                        {
                            var melding = Manager.Meldingen.CreateMelding(xlog.ToString(), "Producties toegevoegd door " + mail.From?.First()?.Name, mail.MessageId,x1,
                                new List<string>(), null, true, false, "openproductie", string.Join(";",ids), 0);
                        }
                        break;
                }
                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static string ClearSubject(string value)
        {
            var charsToRemove = new string[] { "fw:", "fwd:", "re:", "rep:" };
            string xret = value;
            foreach (var c in charsToRemove)
            {
                var xindex = xret.IndexOf(c, 0, xret.Length, StringComparison.CurrentCultureIgnoreCase);
                if (xindex == -1) continue;
                xret = xret.Remove(xindex, c.Length).Trim();
            }
            return xret;
        }

        public static string GetActionFromBody(ref string body, out string actionid)
        {
           var xlines = body.Split('\n');
            string xret = null;
            actionid = null;
            foreach(var line in xlines)
            {
                var xl = line.Trim();
                if (xl.ToLower().Replace(" ", "").StartsWith("action=") ||
                    xl.ToLower().Replace(" ", "").StartsWith("actie="))
                {
                    xret = xl.Split('=').LastOrDefault();
                    body = body.Replace(xl, "");
                }
                if (xl.ToLower().Replace(" ", "").StartsWith("actionid=") ||
                    xl.ToLower().Replace(" ", "").StartsWith("actieid="))
                {
                    actionid = xl.Split('=').LastOrDefault();
                    body = body.Replace(xl, "");
                }
                body = body.Trim();
                if (!string.IsNullOrEmpty(actionid) && !string.IsNullOrEmpty(xret))
                    break;
            }
            return xret;
        }

        private static bool WijzigValue(string value,Werk werk, List<string> logs)
        {
            if (string.IsNullOrEmpty(value) || werk == null || !werk.IsValid) return false;
            var type = werk.Plek?.GetType() ?? werk.Bewerking?.GetType() ?? werk.Formulier?.GetType();
            object data = (object)werk.Plek ?? (object)werk.Bewerking ?? (object)werk.Formulier;
            if (type == null) return false;
            var properties = type.GetProperties();
            var changed = false;
            if (value.Length > 4 && value.Contains("="))
            {
                logs ??= new List<string>();
                var xs = value.Split('=');
                foreach (var p in properties)
                    if (p.CanWrite && string.Equals(p.Name, xs[0].Trim(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        var xset = xs[1].Trim().ToObjectValue(p.PropertyType);
                        if (xset != null)
                        {
                            try
                            {
                                var xold = p.GetValue(data);
                                p.SetValue(data, xset);
                                logs.Add($"{p.Name} Gewijzigd van {xold.ToString()} naar {xs[1].Trim()}");
                                changed = true;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }

                        break;
                    }
            }

            return changed;
        }

        private static bool WijzigData(string data, List<string> logs)
        {
            try
            {
                if (string.IsNullOrEmpty(data)) return false;
                logs ??= new List<string>();
                using var sr = new StringReader(data);
                string line = null;
                while (sr.Peek() > -1)
                {
                    line ??= sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        line = null;
                        continue;
                    }
                    if (line.StartsWith("*"))
                    {
                        string id = line.Replace("*", "").Trim();
                        line = null;
                        if (string.IsNullOrEmpty(id)) continue;
                        var werk = Werk.FromPath(id);
                        if (!werk.IsValid) continue;
                        var xlogs = new List<string>();
                        while (sr.Peek() > -1)
                        {
                            var info = sr.ReadLine();
                            if (string.IsNullOrEmpty(info)) continue;
                            if (info.StartsWith("*"))
                            {
                                line = info;
                                break;
                            }

                            if (info.Contains("="))
                                WijzigValue(info, werk, xlogs);
                        }

                        if (xlogs.Count > 0)
                        {
                            xlogs.Insert(0, $"[{werk.Path}]Gewijzigd: ");
                            logs.AddRange(xlogs);
                            werk.Formulier.UpdateForm(true, false, null, string.Join(", ", xlogs));
                        }
                    }
                    else line = null;
                }

                return logs.Count > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private static byte[] GetMessageData(MimePart entity)
        {
            try
            {
                if (entity == null) return null;
                using var ms = new MemoryStream();
                entity.Content.DecodeTo(ms);
                return ms.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private static bool SaveMessageData(MimePart entity, string filename)
        {
            try
            {
                if (entity == null) return false;
                using var ms = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                entity.Content.DecodeTo(ms);
                ms.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static Task ControlleerOpMessages()
        {
            return Task.Factory.StartNew(() =>
            {
                if (_IsProcessing) return;
                _IsProcessing = true;
                try
                {
                    // Gmail IMAP4 server is "imap.gmail.com"
                    var oServer = CreateImapClient(out var email);
                    if (oServer != null)
                    {
                        var inbox = oServer.Inbox;
                        inbox.Open(FolderAccess.ReadWrite);
                        var ids = inbox.Search(SearchQuery.All);
                        for (var i = 0; i < ids.Count; i++)
                        {
                            var id = ids[i];
                            var info = inbox.GetMessage(id);
                            if (ProcessInkomingMail(info))
                            {
                                inbox.AddFlags(new UniqueId[] {id}, MessageFlags.Deleted, false);
                                inbox.Expunge();
                            }
                        }
                        oServer.Disconnect(true);
                    }
                }
                catch (Exception ep)
                {
                    Console.WriteLine(ep.Message);
                }

                _IsProcessing = false;
            });
        }

        public static bool RespondByEmail(ProductieFormulier formulier, string title = "")
        {
            return RespondByEmail(new EmailRespond(formulier), title,false);
        }

        public static bool RespondByEmail(Bewerking bew, string title = "")
        {
            return RespondByEmail(new EmailRespond(bew), title,false);
        }

        public static bool SendEmail(string adress, string title, string msg, List<string> attachments, bool shownotification, bool ishtml)
        {
            try
            {
                var es = EmailRespond.CreateMail(adress, "ProductieManager", title, msg, attachments,ishtml);
                return RespondByEmail(new[] {es},shownotification);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SendEmails(List<EmailClient> adresses, string title, string afzender, string msg, List<string> attachments, bool shownotification, bool ishtml)
        {
            try
            {
                if (adresses == null || adresses.Count == 0)
                    return false;
                var oSmtp = CreateSmtpClient(out var email);
                if (oSmtp == null) return false;
                //AsyncCallback callback = shownotification ? new AsyncCallback(RecievedRespond) : null;
                foreach (var adres in adresses)
                {
                    var es = EmailRespond.CreateMail(adres.Email, afzender, title, msg, attachments,ishtml);
                    if (es == null) continue;
                    es.From = new MailAddress(email, "ProductieManager");
                    es.Sender = new MailAddress(email, "ProductieManager");
                    oSmtp.SendAsync(es, shownotification ? es : null);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static SmtpClient CreateSmtpClient(out string email)
        {
            email = string.Empty;
            try
            {
                
                if (Manager.Opties == null || string.IsNullOrEmpty(Manager.Opties.BoundUsername)) return null;
                var acc = Manager.Database?.GetAccount(Manager.Opties.BoundUsername).Result;
                var host = acc?.MailingHost;
                if (host == null) return null;
                email = host.EmailAdres;
                var smtp = host.CreateClient();
                smtp.SendCompleted += Smtp_SendCompleted;
                return smtp;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ImapClient CreateImapClient(out string email)
        {
            email = string.Empty;
            try
            {

                if (Manager.Opties == null || string.IsNullOrEmpty(Manager.Opties.BoundUsername)) return null;
                var acc = Manager.Database?.GetAccount(Manager.Opties.BoundUsername).Result;
                var host = acc?.MailingHost;
                if (host == null) return null;
                email = host.EmailAdres;
                var smtp = host.CreateImapClient();
               // smtp.SendCompleted += Smtp_SendCompleted;
                return smtp;

            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void Smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.UserState is MailMessage mail)
            {
                string xreciepts = string.Join(", ", mail.To.Select(x => x.Address));
                Manager.RemoteMessage(new RemoteMessage($"Email verzonden naar {xreciepts}",
                    MessageAction.AlgemeneMelding, MsgType.Bericht));
            }
        }

        public static bool RespondByEmail(EmailRespond respond, string title, bool shownotification)
        {
            try
            {
                var mails = respond.GetRespondMails(title);
                return RespondByEmail(mails,shownotification);
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public static bool RespondByEmail(MailMessage[] mails, bool shownotification)
        {
            try
            {
                if (mails == null || mails.Length == 0)
                    return false;
                var oSmtp = CreateSmtpClient(out var email);
                if (oSmtp == null) return false;
                //AsyncCallback callback = shownotification ? new AsyncCallback(RecievedRespond) : null;
                foreach (var mail in mails)
                {
                    if (mail == null) continue;
                    mail.From = new MailAddress(email, "ProductieManager");
                    mail.Sender = new MailAddress(email, "ProductieManager");
                    oSmtp.SendAsync(mail, shownotification ? email : null);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static bool SendStoringMail(Storing storing, IProductieBase productie)
        {
            try
            {
                var mails = EmailRespond.GetRespondMails(storing, productie);
                return RespondByEmail(mails,false);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static RemoteMessage[] GetRemoteMessages(string onderwerp, string body)
        {
            var messages = new List<RemoteMessage>();
            using (var sr = new StringReader(body))
            {
                var isread = false;
                var line = "";
                while (sr.Peek() > -1)
                {
                    var message = new RemoteMessage();

                    line = isread ? line : sr.ReadLine();
                    isread = false;
                    if (string.IsNullOrEmpty(line) || line.Length == 0)
                        continue;
                    if ((line.StartsWith("[") || line.StartsWith("(")) && (line.EndsWith("]") || line.EndsWith(")")))
                    {
                        //is productienr
                        line = line.Replace(" ", "");
                        var prodnr = line.TrimStart('[', '(').TrimEnd(']', ')');
                        var args = prodnr.Split(',');
                        if (args.Length > 0 && args[0].Length > 4)
                        {
                            if (args.Length > 1)
                                message.ProductieNummer = args[1];
                            switch (args[0].ToLower())
                            {
                                case "melding":
                                    message.Action = MessageAction.AlgemeneMelding;
                                    break;

                                case "wijzig":
                                    message.Action = MessageAction.ProductieWijziging;
                                    break;

                                case "productiemelding":
                                    message.Action = MessageAction.ProductieNotitie;
                                    break;

                                case "verwijder":
                                    message.Action = MessageAction.ProductieVerwijderen;
                                    break;

                                case "gebruiker":
                                    message.Action = MessageAction.GebruikerUpdate;
                                    break;
                            }
                        }

                        var note = "";
                        while (sr.Peek() > -1)
                        {
                            line = isread ? line : sr.ReadLine();
                            if (line.Length < 2)
                            {
                                isread = false;
                                continue;
                            }

                            if ((line.StartsWith("[") || line.StartsWith("(")) &&
                                (line.EndsWith("]") || line.EndsWith(")")))
                            {
                                isread = true;
                                break;
                            }

                            note += "\n" + line;
                        }

                        message.Message = note.TrimStart('\n').TrimEnd('\n');
                        messages.Add(message);
                    }
                }
            }

            return messages.ToArray();
        }
    }
}