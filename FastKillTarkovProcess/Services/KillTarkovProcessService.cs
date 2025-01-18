using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FastKillTarkovProcess.Services
{
    public class KillTarkovProcessService : ObservableObject
    {
        public void KillTarkovProcess()
        {
            foreach (var process in Process.GetProcessesByName("EscapeFromTarkov"))
            {
                process.Kill();
            }
        }
    }
}