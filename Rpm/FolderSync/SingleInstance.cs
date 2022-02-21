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
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        public static void ActivateProcess()
        {
            var current = Process.GetCurrentProcess();
            foreach (var process in Process.GetProcessesByName(current.ProcessName))
                if (process.Id != current.Id)
                {
                    SetForegroundWindow(process.MainWindowHandle);
                    ShowWindowAsync(process.MainWindowHandle, 9);
                    break;
                }
        }

        public static bool CheckInstanceExist(string uniqueApplicationName)
        {
            var createdNew = true;
            using (var mutex = new Mutex(true, uniqueApplicationName, out createdNew))
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