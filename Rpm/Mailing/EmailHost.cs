using Rpm.Connection;
using Rpm.Misc;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

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
        public string EmailAdres { get; set; }
        public string Password { get; set; }


        public int Port {
            get => UseSsl ? 587 : 25;
            set => _Port = value;
        }

        private int _Port;
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

        public string GetImapHost()
        {
            var xtype = GetEmailType();
            switch (xtype)
            {
                case EmailType.None:
                    return null;
                case EmailType.Gmail:
                    return "imap.gmail.com";
                case EmailType.Outlook:
                    return "imap-mail.outlook.com";
                case EmailType.Yahoo:
                    return "imap.mail.yahoo.com";
                case EmailType.Office365:
                    return "outlook.office365.com";
                case EmailType.Sendgrid:
                    return "imap.sendgrid.net";
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
                    EnableSsl = UseSsl,
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

       // public async Task<ImapClient> CreateImapClient()
        //{
        //    var client = new ImapClient();
        //    try
        //    {
        //        var host = GetImapHost();
        //        if (string.IsNullOrEmpty(host)) return null;
        //        //var clientSecrets = new ClientSecrets
        //        //{
        //        //    ClientId = "709410244077-bog56s165bgck4p3tprkh17jcr99tiup.apps.googleusercontent.com",
        //        //    ClientSecret = "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"
        //        //};
        //        //var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        //        //{
        //        //    DataStore = new FileDataStore("CredentialCacheFolder", false),
        //        //    Scopes = new[] { "https://mail.google.com/" },
        //        //    ClientSecrets = clientSecrets
        //        //});
        //        //var codeReceiver = new LocalServerCodeReceiver();
        //        //var authCode = new AuthorizationCodeInstalledApp(codeFlow, codeReceiver);

        //        //var credential = await authCode.AuthorizeAsync(EmailAdres, CancellationToken.None);

        //        //if (credential.Token.IsExpired(SystemClock.Default))
        //        //    await credential.RefreshTokenAsync(CancellationToken.None);

        //        //var oauth2 = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);
        //        client.ServerCertificateValidationCallback = (sender, certificate, certChainType, errors) => true;
        //        client.Connect(host, 993, SecureSocketOptions.Auto);
        //        //client.Authenticate(oauth2);
        //        client.Authenticate(EmailAdres, Password);
        //        return client;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        client.Disconnect(true);
        //        return null;
        //    }
        //}

        public bool TextConnection()
        {
            return SmtpHelper.ValidateCredentials(EmailAdres, Password, Host, 465, true);
        }
    }
}
