using System;

namespace Rpm.Various
{

    [Serializable]
    public enum FilterType
    {
        None,
        BegintMet,
        EindigtMet,
        GelijkAan,
        NietGelijkAan,
        Bevat,
        BevatNiet,
        KleinerDan,
        KleinerOfGelijkAan,
        GroterDan,
        GroterOfGelijkAan,
        FilterOpslaan
    }

    [Serializable]
    public enum Operand
    {
        None,
        ALS,
        OF,
        EN
    }


    [Serializable]
    public enum RefreshRate
    {
        PerUur,
        PerDag,
        PerWeek,
        PerMaand
    }

    [Serializable]
    public enum SecondaryManageType
    {
        Read,
        Write,
        None
    }

    [Serializable]
    public enum AccesType
    {
        AlleenKijken,
        ProductieBasis,
        ProductieAdvance,
        Manager
    }

    [Serializable]
    public enum NotitieType
    {
        Algemeen,
        BewerkingGereed,
        ProductieGereed,
        DeelsGereed,
        Werkplek,
        Bewerking,
        Productie
    }

    [Serializable]
    public enum MessageAction
    {
        NieweProductie,
        ProductieNotitie,
        ProductieWijziging,
        ProductieVerwijderen,
        AlgemeneMelding,
        GebruikerUpdate,
        BijlageUpdate,
        None
    }

    [Serializable]
    public enum AktieType
    {
        ControleCheck,
        Beginnen,
        GereedMelden,
        BewerkingGereed,
        KlaarZetten,
        Stoppen,
        PersoneelChange,
        PersoneelVrij,
        Telaat,
        Onderbreking,
        None
    }

    [Serializable]
    public enum MainAktie
    {
        OpenProductie,
        OpenIndeling,
        OpenProductieWijziging,
        OpenInstellingen,
        OpenRangeSearcher,
        OpenPersoneel,
        OpenStoringen,
        OpenAlleStoringen,
        OpenVaardigheden,
        OpenAlleVaardigheden,
        OpenAantalGemaaktProducties,
        MeldBewerkingGereed,
        StartBewerking,
        StopBewerking,
        OpenBijlage,
        OpenAlleBijlages,
    }

    [Serializable]
    public enum TaakUrgentie
    {
        ZodraMogelijk,
        ZSM,
        PerDirect,
        Geen_Prioriteit,
        None
    }

    [Serializable]
    public enum ProductieState
    {
        Gestopt,
        Gestart,
        Gereed,
        Verwijderd
    }

    [Serializable]
    public enum MsgType
    {
        Success,
        Waarschuwing,
        Fout,
        Info,
        Bericht,
        Gebruiker
    }

    public enum ViewState
    {
        Gestopt,
        Gestart,
        Gereed,
        Verwijderd,
        Telaat,
        Nieuw,
        Alles,
        None
    }

    [Serializable]
    public enum ProgressType
    {
        ReadBussy,
        ReadCompleet,
        WriteBussy,
        WriteCompleet
    }
}