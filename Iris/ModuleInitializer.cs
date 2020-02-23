using System;
using System.IO;
using System.Reflection;

namespace Iris
{
    public static class ModuleInitializer
    {
        public static void Initialize()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dllPath = Path.Combine(localPath, "SdlBinaries");

                if (Environment.Is64BitOperatingSystem)
                {
                    dllPath = Path.Combine(dllPath, "Win64");
                }
                else
                {
                    dllPath = Path.Combine(dllPath, "Win32");
                }

                var env = Environment.GetEnvironmentVariable("PATH");
                env = $"{dllPath};{env}";

                Environment.SetEnvironmentVariable("PATH", env);
            }
        }
    }
}
