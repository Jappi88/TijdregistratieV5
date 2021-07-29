﻿using System;
using System.Windows.Forms;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;

namespace Rpm.Various
{
    public delegate void RemoteMessageHandler(RemoteMessage message, Manager instance);

    public delegate void ProductiesChangedHandler(object sender);

    public delegate void FormulierChangedHandler(object sender, ProductieFormulier changedform);
    public delegate void FormulierDeletedHandler(object sender, string id);

    public delegate void FormulierActieHandler(object[] values, MainAktie type);

    public delegate void BewerkingChangedHandler(object sender, Bewerking bewerking, string change);

    public delegate void AccountChangedHandler(object sender, UserAccount account);

    public delegate void PersoneelChangedHandler(object sender, Personeel user);
    public delegate void PersoneelDeletedHandler(object sender, string id);

    public delegate void UserSettingsChangingHandler(object instance, ref UserSettings settings, ref bool cancel);

    public delegate void UserSettingsChangedHandler(object instance, UserSettings settings, bool reinit);

    public delegate void LogInChangedHandler(UserAccount user, object instance);

    public delegate void RunInstanceCompleteHandler(IQueue instance);

    public delegate void RunComplete();

    public delegate void ManagerLoadingHandler(ref bool cancel);

    public delegate void ManagerLoadedHandler();

    public delegate DialogResult ShutdownHandler(Manager instance, ref TimeSpan verlengtijd);

    public delegate void NewLogHandler(LogEntry log, object sender);

    public delegate void TaakHandler(Taak taak);

    public delegate void ProgressChangedHandler(object sender, ProgressArg arg);

    public delegate void InstanceChangedHandler(object sender, object value);
    public delegate bool IsValidHandler(object value, string filter);

    public class ProgressArg
    {
        public string Message;
        public int Pogress;
        public ProgressType Type;
        public object Value;
    }
}