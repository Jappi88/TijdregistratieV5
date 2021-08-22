using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace FolderSync
{
    public class SingleInstance
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        public static void ActivateProcess()
        {
            Process current = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            {
                if (process.Id != current.Id)
                {
                    SetForegroundWindow(process.MainWindowHandle);
                    ShowWindowAsync(process.MainWindowHandle, 9);
                    break;
                }
            }
        }

        public static bool CheckInstanceExist(string uniqueApplicationName)
        {
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, uniqueApplicationName, out createdNew))
            {
                if (createdNew == false)
                {
                    ActivateProcess();
                    return false;
                }
                return true;
            }
        }
    }
}
