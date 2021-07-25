using EAGetMail;
using EASendMail;
using ProductieManager.Productie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServerProtocol = EAGetMail.ServerProtocol;

namespace ProductieManager.Mailing
{
    public class RemoteProductie
    {
        public static RemoteMessage[] ControlleerOpMessages()
        {
            try
            {
                List<RemoteMessage> xreturn = new List<RemoteMessage>();
                // Gmail IMAP4 server is "imap.gmail.com"
                MailServer oServer = new MailServer("imap.gmail.com",
                                "valk.rpm@gmail.com",
                                "R3iuyie3",
                                ServerProtocol.Imap4);

                // Enable SSL connection.
                oServer.SSLConnection = true;

                // Set 993 SSL port
                oServer.Port = 993;
                MailClient oClient = new MailClient("TryIt");
                oClient.Connect(oServer);
                // retrieve unread/new email only
                oClient.GetMailInfosParam.Reset();
                oClient.GetMailInfosParam.GetMailInfosOptions = GetMailInfosOptionType.NewOnly;

                MailInfo[] infos = oClient.GetMailInfos();
                Console.WriteLine("Total {0} unread email(s)\r\n", infos.Length);
                for (int i = 0; i < infos.Length; i++)
                {
                    MailInfo info = infos[i];
                    Console.WriteLine("Index: {0}; Size: {1}; UIDL: {2}",
                        info.Index, info.Size, info.UIDL);
                    Mail oMail = oClient.GetMail(info);

                    bool fromadmin = oMail.From.Address.ToLower() == "jappi88@gmail.com" ||
                        oMail.From.Address.ToLower() == "iabed@valksystemen.nl" ||
                        oMail.From.Address.ToLower() == "irjappi88@hotmail.com";

                    // Receive email from IMAP4 server

                    if (Manager.Opties != null && Manager.Opties.OntvangAdres != null && Manager.Opties.OntvangAdres.Length > 0 || fromadmin)
                    {
                        bool deletemail = false;
                        InkomendAdres ink = new InkomendAdres();
                        if (fromadmin)
                            ink.Actions = new MessageAction[] { MessageAction.AlgemeneMelding,
                                MessageAction.NieweProductie, MessageAction.ProductieNotitie,
                                MessageAction.ProductieVerwijderen, MessageAction.ProductieWijziging,
                                MessageAction.GebruikerUpdate };
                        else
                            ink = Manager.Opties.OntvangAdres.Where(x => x.Adres.Trim().ToLower() == oMail.From.Address.ToLower()).FirstOrDefault();

                        if (ink != null)
                        {
                            string body = oMail.TextBody;
                            int end = body.ToLower().IndexOf("met vriendelijke groet");
                            if (end > -1 || (end = body.ToLower().IndexOf("\r\n\r\n")) > -1)
                                body = body.Substring(0, end).Trim();
                            RemoteMessage[] messages = GetRemoteMessages(body);
                            if (messages != null && messages.Length > 0)
                            {
                                messages = messages.Where(t => ink.Actions.Any(x => x == t.Action)).ToArray();
                                if (messages.Length > 0)
                                {
                                    xreturn.AddRange(messages);
                                    deletemail = true;
                                }
                            }
                            var x = oMail.Attachments;
                            if (x != null && ink.Actions.Any(x => x == MessageAction.NieweProductie))
                            {
                                foreach (var a in x)
                                {
                                    if (a.Content == null || a.Content.Length < 10 || a.ContentType.Contains("png"))
                                        continue;
                                    xreturn.Add(new RemoteMessage($"Nieuwe Productiebon", MessageAction.NieweProductie, MsgType.Success, a.Content));
                                }
                                deletemail = true;
                            }
                            else deletemail |= false;
                        }
                        Console.WriteLine("From: {0}", oMail.From.ToString());
                        Console.WriteLine("Subject: {0}\r\n", oMail.Subject);
                        // verwijder de verwerkte email
                        if (!info.Read && deletemail)
                        {
                            oClient.Delete(info);
                        }
                    }
                }

                try { oClient.Quit(); } catch { }
                Console.WriteLine("Completed!");
                return xreturn.ToArray();
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
                return null;
            }
        }

        public static bool RespondByEmail(ProductieFormulier formulier, string title = "")
        {
            return RespondByEmail(new EmailRespond(formulier), title);
        }

        public static bool RespondByEmail(Bewerking bew, string title = "")
        {
            return RespondByEmail(new EmailRespond(bew), title);
        }

        public static bool RespondByEmail(EmailRespond respond, string title)
        {
            try
            {
                SmtpMail[] mails = respond.GetRespondMails(title);
                if (mails == null || mails.Length == 0)
                    return false;
                SmtpServer oServer = new SmtpServer("smtp.gmail.com");
                oServer.User = "valk.rpm@gmail.com";
                oServer.Password = "Valk_Productie";

                // Enable SSL connection.
                oServer.ConnectType = SmtpConnectType.ConnectTryTLS;

                // Set 993 SSL port
                oServer.Port = 587;

                SmtpClient oSmtp = new SmtpClient();
                foreach (SmtpMail mail in mails)
                {
                    SmtpClientAsyncResult result = oSmtp.BeginSendMail(oServer, mail, null, null);
                }
                return true;
            }
            catch (Exception) { return false; }
        }

        public static RemoteMessage[] GetRemoteMessages(string body)
        {
            List<RemoteMessage> messages = new List<RemoteMessage> { };
            using (StringReader sr = new StringReader(body))
            {
                bool isread = false;
                string line = "";
                while (sr.Peek() > -1)
                {
                    RemoteMessage message = new RemoteMessage();

                    line = isread ? line : sr.ReadLine();
                    isread = false;
                    if (string.IsNullOrEmpty(line) || line.Length == 0)
                        continue;
                    if ((line.StartsWith("[") || line.StartsWith("(")) && (line.EndsWith("]") || line.EndsWith(")")))
                    {
                        //is productienr
                        line = line.Replace(" ", "");
                        string prodnr = line.TrimStart(new char[] { '[', '(' }).TrimEnd(new char[] { ']', ')' });
                        string[] args = prodnr.Split(',');
                        if (args != null && args.Length > 0 && args[0].Length > 4)
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

                        string note = "";
                        while (sr.Peek() > -1)
                        {
                            line = isread ? line : sr.ReadLine();
                            if (line.Length < 2)
                            {
                                isread = false;
                                continue;
                            }
                            if ((line.StartsWith("[") || line.StartsWith("(")) && (line.EndsWith("]") || line.EndsWith(")")))
                            {
                                isread = true;
                                break;
                            }
                            else
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