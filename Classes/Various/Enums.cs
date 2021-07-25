using System;

[Serializable]
public enum AccesType : int
{
    AlleenKijken,
    ProductieBasis,
    ProductieAdvance,
    Manager
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
    None
}

[Serializable]
public enum AktieType : int
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
    None
}

[Serializable]
public enum TaakUrgentie : int
{
    ZodraMogelijk,
    ZSM,
    PerDirect,
    Geen_Prioriteit,
    None
}

[Serializable]
public enum ProductieState : byte
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
    Warning,
    Error,
    Info
}

public enum ViewState : int
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
public enum ProgressType : int
{
    ReadBussy,
    ReadCompleet,
    WriteBussy,
    WriteCompleet
}