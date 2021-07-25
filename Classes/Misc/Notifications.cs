using ProductieManager.Mailing;
using ProductieManager.Productie;

namespace ProductieManager.Misc
{
    public static class Notifications
    {
        public static void Notify(this RemoteMessage message)
        {
            string value = "";
            MsgType type = message.MessageType;
            switch (message.Action)
            {
                case MessageAction.NieweProductie:
                    if (message.Value is ProductieFormulier)
                    {
                        var pr = message.Value as ProductieFormulier;
                        value = $"Nieuwe Productie Toegevoegd: [{pr.ArtikelNr}][{pr.ProductieNr}]";
                    }
                    else if (message.Value is ProductieFormulier[])
                    {
                        var prs = message.Value as ProductieFormulier[];
                        if (prs.Length == 1)
                        {
                            value = $"Nieuwe Productie Toegevoegd: [{prs[0].ArtikelNr}][{prs[0].ProductieNr}]";
                        }
                        else
                            value = $"{prs.Length} Producties Toegevoegd!";
                    }
                    type = MsgType.Success;
                    break;

                case MessageAction.ProductieNotitie:
                case MessageAction.ProductieWijziging:
                    value = $"[{message.ProductieNummer}]\n{message.Message}";
                    type = MsgType.Info;
                    break;

                case MessageAction.ProductieVerwijderen:
                    value = $"Productie nr: {message.ProductieNummer} is zojuist verwijdert";
                    type = MsgType.Warning;
                    break;

                case MessageAction.AlgemeneMelding:
                    value = $"{message.Message}";
                    type = MsgType.Info;
                    break;

                case MessageAction.GebruikerUpdate:
                    value = $"[UPDATE]\n{message.Message}";
                    type = MsgType.Success;
                    break;

                case MessageAction.None:
                    value = $"{message.Message}";
                    type = message.MessageType;
                    break;
            }
            //Manager.Logbook.AddLog(new LogFile(value, type));
            Functions.Alert(value, type);
        }
    }
}