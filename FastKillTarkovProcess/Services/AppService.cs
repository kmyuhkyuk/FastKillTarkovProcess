using System.Reflection;

namespace FastKillTarkovProcess.Services
{
    public class AppService
    {
        public string AppVersion { get; } = GetAssemblyVersion();

        public string AppURL { get; } = "https://github.com/kmyuhkyuk/FastKillTarkovProcess";

        public static string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                   ?? string.Empty;
        }
    }
}