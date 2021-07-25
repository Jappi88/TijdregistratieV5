namespace Rpm.Connection
{
    public enum RespondType
    {
        LoggedIn,
        LoggedOut,
        Bericht,
        Update,
        Delete,
        Replace,
        Add,
        None
    }

    public enum DatabaseName
    {
        Producties,
        GereedProducties,
        Settings,
        Accounts,
        Personeel,
        Alles,
        None
    }
}