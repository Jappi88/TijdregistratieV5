using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace FolderSync.Security
{
    public class RestartProcessAsAdmin
    {
        public static bool IsAdministrator
        {
            get
            {
                var wi = WindowsIdentity.GetCurrent();
                var wp = new WindowsPrincipal(wi);

                return wp.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static bool RestartAsAdmin(string arguments)
        {
            if (IsAdministrator == false && arguments != "1")
            {
                var oProcess = new Process();
                var startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Assembly.GetEntryAssembly().GetName().Name;
                startInfo.Verb = "runas";
                startInfo.Arguments = "\"1\"";
                var p = Process.Start(startInfo);
                return true;
            }

            return false;
        }
    }
}
/* In Windows Form Application Program.cs write code as like this :
 * The argument is the argument of the application, if it is 1, it means that the application 
 * is running before previous instance is restarted
            
            string argument = string.Empty;
            if (args.Length >= 1) argument = args[0];
            if (RestartProcessAsAdmin.RestartAsAdmin(argument) == false)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
*/