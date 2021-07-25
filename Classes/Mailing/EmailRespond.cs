using EASendMail;
using ProductieManager.Productie;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductieManager.Mailing
{
    public class EmailRespond
    {
        public ProductieFormulier Formulier { get; private set; }
        public Bewerking Werk { get; set; }

        public EmailRespond(ProductieFormulier formulier)
        {
            Formulier = formulier;
        }

        public EmailRespond(Bewerking bew)
        {
            Werk = bew;
        }

        public SmtpMail[] GetRespondMails(string title)
        {
            try
            {
                List<SmtpMail> mails = new List<SmtpMail> { };
                if (Manager.Opties == null || Manager.LogedInGebruiker == null)
                    return null;
                foreach (UitgaandAdres adres in Manager.Opties.VerzendAdres)
                {
                    if (string.IsNullOrEmpty(adres.Adres) || !adres.States.Any(t => t == Formulier.State))
                        continue;
                    SmtpMail mail = new SmtpMail("TryIt");
                    mail.Sender = new MailAddress("Valk.Rpm@gmail.com");
                    mail.From = "Valk.Rpm@gmail.com";
                    mail.To = adres.Adres.Trim();
                    mail.Subject = $"Feedback Vanuit [{Manager.LogedInGebruiker.Username}]. ProductieNr [{Formulier.ProductieNr.ToUpper()}]";
                    mail.TextBody = GetMessageBody(title);
                    //mail.HtmlBody = GetHtmlBody(title);
                    mails.Add(mail);
                }
                return mails.ToArray();
            }
            catch { return null; }
        }

        public string GetMessageBody(string title)
        {
            string xreturn = $"FeedBack Vanuit De [{Manager.LogedInGebruiker.Username}] Afdeling\n\n" +
                   $"{title}\n\n";
            if (Formulier != null)
                xreturn +=
                   $"Omschrijving: {Formulier.Omschrijving}\n\n" +
                   $"ArtikelNr: {Formulier.ArtikelNr}\n" +
                   $"Aantal: {Formulier.Aantal}\n" +
                   $"Productie Toegevoegd op: {Formulier.DatumToegevoegd.ToString("dd-MM-yyyy H:mm")}\n" +
                   $"Leverdatum: {Formulier.LeverDatum.ToString("dd-MM-yyyy")}\n" +
                   $"Verwacht Leverdatum: {Formulier.VerwachtLeverDatum.ToString("dd-MM-yyyy H:mm")}\n\n" +

                   $"Notitie: {Formulier.Notitie}\n\n";
            if (Formulier != null)
            {
                if (Formulier.Bewerkingen == null || Formulier.Bewerkingen.Length == 0)
                    xreturn += "Er zijn geen bewerkingen bij dit productie!";
                foreach (Bewerking b in Formulier.Bewerkingen)
                {
                    xreturn += CreateBodyFromBewerking(b);
                }
            }
            else if (Werk != null)
                xreturn += CreateBodyFromBewerking(Werk);
            xreturn += $"\n\nMet vriendelijke groet, kind regards,\n" +
                               $"Valk RPM Team\n";

            return xreturn;
        }

        public string GetHtmlBody(string title)
        {
            return $"<title>{title}</title>" +
                $"<tr>" +
                $"<td>" +
                $"</tr>" +
                $"</td>" +
                $"{title}";
        }

        public string CreateBodyFromBewerking(Bewerking bew)
        {
            string datuminfo = "";
            switch (bew.State)
            {
                case ProductieState.Gestopt:
                    if (Formulier.TijdGestopt >= Formulier.DatumToegevoegd)
                        datuminfo = $"Gestopt op: {bew.TijdGestopt.ToString("dd-MM-yyyy H:mm")}";
                    break;

                case ProductieState.Gestart:
                    datuminfo = $"Gestart op: {bew.TijdGestart.ToString("dd-MM-yyyy H:mm")}";
                    break;

                case ProductieState.Gereed:
                    datuminfo = $"Gereed op: {bew.TijdGestopt.ToString("dd-MM-yyyy H:mm")}";
                    break;

                case ProductieState.Verwijderd:
                    datuminfo = $"Verwijderd op: {Formulier.DatumVerwijderd.ToString("dd-MM-yyyy H:mm")}";
                    break;
            }
            int personeel = bew.AantalPersonen;
            string xreturn = $"Bewerking :[{bew.ProductieNr}] {bew.Naam}\n" +
                             $"Status: {Enum.GetName(typeof(ProductieState), bew.State).ToUpper()}\n\n" +

                             $"{datuminfo}\n" +
                             $"Tijd Gewerkt: {Math.Round(bew.TijdAanGewerkt(), 2)} Uur\n" +
                             $"Aantal Gemaakt: {bew.AantalGemaakt}\n" +
                             $"Aantal Personen: {personeel}\n" +
                             $"Gemiddeld P/U: {bew.ProductenPerUur()}\n" +
                             $"Verwacht Leverdatum: {bew.VerwachtDatumGereed().ToString("dd-MM-yyyy H:mm")}\n";

            return xreturn;
        }
    }
}