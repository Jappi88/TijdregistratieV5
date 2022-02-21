using System;
using System.Net;
using System.Net.Mail;
using Rpm.Connection;
using Rpm.Misc;

namespace Rpm.Mailing
{
    public class EmailHost
    {
        public enum EmailType
        {
            None,
            Gmail,
            Outlook,
            Yahoo,
            Office365,
            Sendgrid,
            Custom
        }

        private string _host;

        private int _Port;
        public string EmailAdres { get; set; }
        public string Password { get; set; }

        public int Port
        {
            get => UseSsl ? 587 : 25;
            set => _Port = value;
        }

        public bool UseSsl { get; set; }

        public string Host
        {
            get => GetHost();
            set => _host = value;
        }

        public EmailType GetEmailType()
        {
            if (string.IsNullOrEmpty(EmailAdres)) return EmailType.None;
            if (!EmailAdres.EmailIsValid()) return EmailType.None;
            if (EmailAdres.ToLower().Contains("gmail"))
                return EmailType.Gmail;
            if (EmailAdres.ToLower().Contains("outlook"))
                return EmailType.Outlook;
            if (EmailAdres.ToLower().Contains("yahoo"))
                return EmailType.Yahoo;
            if (EmailAdres.ToLower().Contains("office365"))
                return EmailType.Office365;
            if (EmailAdres.ToLower().Contains("sendgrid"))
                return EmailType.Sendgrid;
            return EmailType.Custom;
        }

        public string GetHost()
        {
            var xtype = GetEmailType();
            switch (xtype)
            {
                case EmailType.None:
                    return null;
                case EmailType.Gmail:
                    return "smtp.gmail.com";
                case EmailType.Outlook:
                    return "smtp-mail.outlook.com";
                case EmailType.Yahoo:
                    return "smtp.mail.yahoo.com";
                case EmailType.Office365:
                    return "smtp.office365.com";
                case EmailType.Sendgrid:
                    return "smtp.sendgrid.net";
                case EmailType.Custom:
                    return _host;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public SmtpClient CreateClient()
        {
            try
            {
                var host = GetHost();
                if (string.IsNullOrEmpty(host)) return null;
                var smtp = new SmtpClient(host)
                {
                    Credentials = new NetworkCredential(EmailAdres, Password),
                    EnableSsl = UseSsl
                };
                smtp.Port = Port;
                return smtp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool TextConnection()
        {
            return SmtpHelper.ValidateCredentials(EmailAdres, Password, Host, 465, true);
        }
    }
}