using System;
using System.Net;
using System.Text;

namespace Rpm.Connection
{
    public class SmtpHelper
    {
        public static bool ValidateCredentials(string login, string password, string server, int port, bool enableSsl)
        {
            SmtpConnectorBase connector;
            if (enableSsl)
            {
                connector = new SmtpConnectorWithSsl(server, port);
            }
            else
            {
                connector = new SmtpConnectorWithoutSsl(server, port);
            }

            if (!connector.CheckResponse(220))
            {
                throw new Exception("Server is onbereikbaar!");
            }

            connector.SendData($"HELO {Dns.GetHostName()}{SmtpConnectorBase.EOF}");
            if (!connector.CheckResponse(250))
            {
                throw new Exception("Ongeldige host adres!");
            }

            connector.SendData($"AUTH LOGIN{SmtpConnectorBase.EOF}");
            if (!connector.CheckResponse(334))
            {
                throw new Exception("Server is niet toegankelijk!");
            }

            connector.SendData(Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}")) + SmtpConnectorBase.EOF);
            if (!connector.CheckResponse(334))
            {
                throw new Exception("Ongeldige email adres!");
            }

            connector.SendData(Convert.ToBase64String(Encoding.UTF8.GetBytes($"{password}")) + SmtpConnectorBase.EOF);
            if (!connector.CheckResponse(235))
            {
                throw new Exception("Ongeldige email/wachtwoord combinatie!");
            }

            return true;
        }
    }
}
