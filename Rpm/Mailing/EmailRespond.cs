using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.Mailing
{
    public class EmailRespond
    {
        public EmailRespond(IProductieBase formulier)
        {
            Productie = formulier;
        }

        public IProductieBase Productie { get; }

        public MailMessage[] GetRespondMails(string title)
        {
            try
            {
                var mails = new List<MailMessage>();
                if (Manager.Opties == null || Manager.LogedInGebruiker == null)
                    return null;
                var prodnr = Productie?.ProductieNr ?? "N.V.T.";
                if (Productie != null)
                {
                    var state = Productie.State;
                    foreach (var adres in Manager.Opties.VerzendAdres)
                    {
                        if (string.IsNullOrEmpty(adres.Adres) || adres.States.All(t => t != state))
                            continue;
                        var onderwerp =
                            $"[{Manager.LogedInGebruiker.Username}] {Enum.GetName(typeof(ProductieState), state)} [{prodnr.ToUpper()}, {Productie.ArtikelNr?.ToUpper()}]";
                        var mail = CreateMail(adres.Adres, "ProductieManager", onderwerp, GetMessageBody(title, false),
                            null, true);
                        mails.Add(mail);
                    }
                }

                return mails.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public static MailMessage CreateMail(string adres, string afzender, string onderwerp, string bericht,
            List<string> bijlages, bool ishtml)
        {
            try
            {
                if (Manager.Opties == null || Manager.LogedInGebruiker == null)
                    return null;
                if (string.IsNullOrEmpty(adres))
                    return null;
                var xgroeten = "\n\nMet vriendelijke groet, kind regards,\n" +
                               $"{Manager.LogedInGebruiker.Username} Afdeling\n" +
                               $"{afzender}\n" +
                               $"Versie {Assembly.GetExecutingAssembly().GetName().Version}\n";
                if (ishtml)
                    xgroeten = xgroeten.Replace("\n", "<br>");
                bericht += xgroeten;
                var mail = new MailMessage();
                mail.To.Add(adres);
                mail.Subject = onderwerp;
                if (bijlages?.Count > 0)
                    foreach (var bijlage in bijlages)
                        if (!string.IsNullOrEmpty(bijlage) && File.Exists(bijlage))
                            mail.Attachments.Add(new Attachment(bijlage));

                mail.Body = bericht;
                mail.IsBodyHtml = ishtml;
                return mail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static MailMessage[] GetRespondMails(Storing storing, IProductieBase productie)
        {
            try
            {
                var mails = new List<MailMessage>();
                if (Manager.Opties == null || Manager.LogedInGebruiker == null)
                    return null;
                foreach (var adres in Manager.Opties.VerzendAdres)
                {
                    if (string.IsNullOrEmpty(adres.Adres) || !adres.SendStoringMail)
                        continue;
                    var mail = CreateMail(adres.Adres, "ProductieManager",
                        $"[{Manager.Opties.Username}] {storing.StoringType} op {storing.Path}",
                        CreateStoringBody(storing, productie), null, false);
                    if (mail != null)
                        mails.Add(mail);
                }

                return mails.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public static string CreateStoringBody(Storing storing, IProductieBase productie)
        {
            var xreturn = $"{storing.StoringType} Productie [{productie.ProductieNr}, {productie.ArtikelNr}]\n" +
                          $"{productie.Omschrijving}\n\n" +
                          (storing.IsVerholpen
                              ? $"{storing.VerholpenDoor} heeft zojuist {storing.StoringType} verholpen."
                              : $"Er is heeft zojuist een {storing.StoringType} plaatsgevonden op {storing.WerkPlek}.") +
                          "\n";
            xreturn += $"{storing.StoringType} Gemeld door {storing.GemeldDoor} op {storing.Gestart} uur.\n";
            if (!string.IsNullOrEmpty(storing.Omschrijving?.Trim()))
                xreturn += $"{storing.Omschrijving}\n";
            if (storing.IsVerholpen)
            {
                xreturn +=
                    $"\n{storing.StoringType} is verholpen door {storing.VerholpenDoor} op {storing.Gestopt} uur.\n" +
                    $"{storing.StoringType} heeft {Math.Round(storing.GetTotaleTijd(), 2)} uur geduurd\n";
                if (!string.IsNullOrEmpty(storing.Oplossing?.Trim()))
                    xreturn += $"{storing.Oplossing}\n";
            }
            else
            {
                xreturn += $"\n{storing.StoringType} is nog niet verholpen.";
            }

            return xreturn;
        }

        public string GetMessageBody(string title, bool showimage)
        {
            if (Productie == null) return string.Empty;
            var xreturn = Productie.GetHeaderHtmlBody(title, showimage ? Productie.GetImageFromResources() : null,
                new Size(48, 48), Color.White, Color.White, Color.DarkBlue, false);
            xreturn += Productie.GetProductieInfoHtml("Productie Info", Color.White, Color.White,
                Color.DarkBlue, false);

            xreturn += Productie.GetNotitiesHtml("Notities", Color.White, Color.White,
                Color.DarkBlue, false);

            xreturn += Productie.GetDatumsHtml("Datums", Color.White, Color.White,
                Color.DarkBlue, false);

            xreturn += Productie.GetVerpakkingHtmlText(null, "VerpakkingsInstructies", Color.White, Color.White,
                Color.DarkBlue, false);

            xreturn += Productie.GetMaterialenHtml("Materialen", Color.White, Color.White,
                Color.DarkBlue, false);

            xreturn += Productie.GetWerkplekkenHtml("Werk Plaatsen", Color.White, Color.White,
                Color.DarkBlue, false);

            return xreturn;
            // return Productie.GetHtmlBody(title, showimage?Productie.GetImageFromResources():null, new Size(48, 48), Color.RoyalBlue, Color.DodgerBlue, Color.DarkBlue);
        }
    }
}