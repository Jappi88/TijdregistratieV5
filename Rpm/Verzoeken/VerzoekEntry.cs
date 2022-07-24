using Rpm.Productie;
using System;
using System.Collections.Generic;

namespace Rpm.Verzoeken
{
    public class VerzoekEntry
    {
        public int ID { get; private set; } = DateTime.Now.GetHashCode();
        public string Afdeling { get; set; } = Manager.Opties?.Username;
        public string PersoneelNaam { get; set; }
        public string NaamMelder { get; set; }
        public DateTime IngediendOp { get; set; } = DateTime.Now;
        public DateTime ReactieOp { get; set; }
        public VerzoekType VerzoekSoort { get; set; }
        public VerzoekStatus Status { get; set; } = VerzoekStatus.Geen;
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public double TotaalTijd { get; set; }
        public string VerzoekMelding { get; set; }
        public string VerzoekReactie { get; set; }
        public string ReactieDoor { get; set; }
        public List<string> GelezenDoor { get; set; } = new List<string>();

        public VerzoekEntry()
        {
            if (Manager.Opties?.Username != null)
                GelezenDoor.Add(Manager.Opties.Username);
        }

        public bool IsRead()
        {
            GelezenDoor ??= new List<string>();
            return GelezenDoor.IndexOf(Manager.Opties?.Username) > -1;
        }

        public void SetRead(bool isread)
        {
            GelezenDoor ??= new List<string>();
            GelezenDoor.RemoveAll(x => string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
            if (isread && Manager.Opties?.Username != null)
                GelezenDoor.Add(Manager.Opties.Username);
        }

        public string GetVerzoekMessage()
        {
            string xret = "";
            xret = $"<div>Verzoek <b>{VerzoekSoort}</b> voor <b>{PersoneelNaam}</b> is <b>{Status}</b></div>";
            xret += $"<div>Vanaf " +
                $" <b>{StartDatum:f}</b><br>Tot  <b>{EindDatum:f} ({TotaalTijd} uur)</b></div>";
            if (!string.IsNullOrEmpty(VerzoekMelding))
                xret += $"<div>Notitie: {VerzoekMelding}</div>";
            if (Status != VerzoekStatus.InAfwachting)
            {
                if (!string.IsNullOrEmpty(VerzoekReactie) && !string.IsNullOrEmpty(ReactieDoor))
                    xret += $"<div><b>{ReactieDoor} zegt: {VerzoekReactie}</b></div>";
            }
            return xret;
        }

        public override bool Equals(object obj)
        {
            if (obj is VerzoekEntry entry)
                return entry.ID == ID;
            return false;
        }

        public override int GetHashCode() { return ID; }
    }

    public enum VerzoekType
    {
        Vrij,
        OverWerk,
        Verwijderen
    }

    public enum VerzoekStatus
    {
        GoedGekeurd,
        Afgekeurd,
        InAfwachting,
        Geen
    }
}
