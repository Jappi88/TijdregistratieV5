using ProductieManager.Classes.Productie;
using ProductieManager.Mailing;
using ProductieManager.Misc;
using ProductieManager.Productie;
using ProductieManager.Settings;
using System;
using System.Windows.Forms;

public delegate void RemoteMessageHandler(RemoteMessage message, Manager instance);

public delegate void ProductiesLoadedHandler(object sender, ProductieFormulier[] forms);

public delegate void FormulierChangedHandler(object sender, ProductieFormulier changedform);

public delegate void FormulierActieHandler(ProductieFormulier formulier, Bewerking bew, AktieType type);

public delegate void BewerkingChangedHandler(object sender, Bewerking bewerking);

public delegate void AccountChangedHandler(object sender, UserAccount account);

public delegate void PersoneelChangedHandler(object sender, Personeel user);

public delegate void UserSettingsChangingHandler(object instance, UserSettings settings, ref bool cancel);

public delegate void UserSettingsChangedHandler(object instance, UserSettings settings);

public delegate void LogInChangedHandler(UserAccount user, object instance);

public delegate void RunInstanceCompleteHandler(IQueue instance);

public delegate void RunComplete();

public delegate void ManagerLoadingHandler(Manager instance, ref bool cancel);

public delegate void ManagerLoadedHandler(Manager instance);

public delegate DialogResult ShutdownHandler(Manager instance, ref TimeSpan verlengtijd);

public delegate void NewLogHandler(LogFile log, object sender);

public delegate void TaakHandler(TaakBeheer manager, Taak taak);

public delegate void TakenHandler(TaakBeheer manager, Taak[] taken);

public delegate void PersoneelLijstHandler(PersoneelsLijst lijst);

public delegate void ProgressChangedHandler(object sender, ProgressArg arg);

public class ProgressArg
{
    public string Message;
    public int Pogress;
    public ProgressType Type;
    public object Value;
}