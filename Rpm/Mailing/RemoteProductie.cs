using ProductieManager.Rpm.Mailing;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using MailMessage = System.Net.Mail.MailMessage;

namespace Rpm.Mailing
{
    public class RemoteProductie
    {
        //public static RemoteMessage[] ControlleerOpMessages()
        //{
        //    try
        //    {
        //        var xreturn = new List<RemoteMessage>();
        //        // Gmail IMAP4 server is "imap.gmail.com"
        //        var oServer = new MailServer("imap.gmail.com",
        //            "valk.rpm@gmail.com",
        //            "XXXXXXXXXX",
        //            ServerProtocol.Imap4);

        //        // Enable SSL connection.
        //        oServer.SSLConnection = true;

        //        // Set 993 SSL port
        //        oServer.Port = 993;
        //        oServer.SSLType = SSLConnectType.ConnectSSLAuto;
        //        var oClient = new MailClient("TryIt");
        //        oClient.Connect(oServer);
        //        // retrieve unread/new email only
        //        oClient.GetMailInfosParam.Reset();
        //        oClient.GetMailInfosParam.GetMailInfosOptions = GetMailInfosOptionType.NewOnly;

        //        var infos = oClient.GetMailInfos();
        //        for (var i = 0; i < infos.Length; i++)
        //        {
        //            var info = infos[i];
        //            Console.WriteLine("Index: {0}; Size: {1}; UIDL: {2}",
        //                info.Index, info.Size, info.UIDL);
        //            var oMail = oClient.GetMail(info);

        //            var fromadmin = oMail.From.Address.ToLower() == "jappi88@gmail.com" ||
        //                            oMail.From.Address.ToLower() == "iabed@valksystemen.nl" ||
        //                            oMail.From.Address.ToLower() == "irjappi88@hotmail.com";

        //            // Receive email from IMAP4 server

        //            if (Manager.Opties != null && Manager.Opties.OntvangAdres != null &&
        //                Manager.Opties.OntvangAdres.Count > 0 || fromadmin)
        //            {
        //                var deletemail = false;
        //                var ink = new InkomendAdres();
        //                if (fromadmin)
        //                    ink.Actions = new[]
        //                    {
        //                        MessageAction.AlgemeneMelding,
        //                        MessageAction.NieweProductie, MessageAction.ProductieNotitie,
        //                        MessageAction.ProductieVerwijderen, MessageAction.ProductieWijziging,
        //                        MessageAction.GebruikerUpdate
        //                    };
        //                else
        //                    ink = Manager.Opties.OntvangAdres.FirstOrDefault(x =>
        //                        x.Adres.Trim().ToLower() == oMail.From.Address.ToLower());

        //                if (ink != null)
        //                {
        //                    var body = oMail.TextBody;
        //                    var end = body.ToLower().IndexOf("met vriendelijke groet", StringComparison.Ordinal);
        //                    if (end > -1 || (end = body.ToLower().IndexOf("\r\n\r\n", StringComparison.Ordinal)) > -1)
        //                        body = body.Substring(0, end).Trim();
        //                    var messages = GetRemoteMessages(body);
        //                    if (messages != null && messages.Length > 0)
        //                    {
        //                        messages = messages.Where(t => ink.Actions.Any(x => x == t.Action)).ToArray();
        //                        if (messages.Length > 0)
        //                        {
        //                            xreturn.AddRange(messages);
        //                            deletemail = true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        deletemail = true;
        //                    }

        //                    var x = oMail.Attachments;
        //                    if (x != null && ink.Actions.Any(x => x == MessageAction.NieweProductie))
        //                    {
        //                        foreach (var a in x)
        //                        {
        //                            if (a.Content == null || a.Content.Length < 10 || a.ContentType.Contains("png"))
        //                                continue;
        //                            xreturn.Add(new RemoteMessage("Nieuwe Productiebon", MessageAction.NieweProductie,
        //                                MsgType.Success, a.Content));
        //                        }

        //                        deletemail = true;
        //                    }
        //                    else
        //                    {
        //                        deletemail &= true;
        //                    }
        //                }

        //                Console.WriteLine("From: {0}", oMail.From);
        //                Console.WriteLine("Subject: {0}\r\n", oMail.Subject);
        //                // verwijder de verwerkte email
        //                if (!info.Read && deletemail)
        //                    oClient.Delete(info);
        //                else
        //                    oClient.MarkAsRead(info, true);
        //            }
        //        }

        //        try
        //        {
        //            oClient.Quit();
        //        }
        //        catch
        //        {
        //        }

        //        return xreturn.ToArray();
        //    }
        //    catch (Exception ep)
        //    {
        //        Console.WriteLine(ep.Message);
        //        return null;
        //    }
        //}

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

        public static RemoteMessage[] GetRemoteMessages(string body)
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