using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace web2Excel
{
    class Shutdown
    {
        [DllImport("user32.dll", EntryPoint = "ExitWindowsEx", CharSet = CharSet.Ansi)]
        private static extern int ExitWindowsEx(int uFlags, int dwReserved); 

        public static void ShutDown()
        {
            Process myProcess = new Process();
            myProcess.StartInfo.FileName = "cmd.exe";
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardInput = true;
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.RedirectStandardError = true;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
            myProcess.StandardInput.WriteLine("shutdown -s -t 0"); 
        }
    }
}
