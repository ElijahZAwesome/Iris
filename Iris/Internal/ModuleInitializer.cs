using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Iris.Internal
{
    internal class ModuleInitializer
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool SetDllDirectory(string path);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool LoadLibrary(string path);

        private static readonly string Architecture = Environment.Is64BitOperatingSystem ? "Win64" : "Win32";
        private static readonly Assembly ThisAssembly = Assembly.GetExecutingAssembly();

        public static void Initialize()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var tempDirPath = CreateTemporaryDirectory();
                SetDllDirectory(tempDirPath);

                var dependencies = ThisAssembly.GetManifestResourceNames().Where(x => x.Contains(Architecture));
                foreach (var dep in dependencies)
                {
                    ExtractAndLoadEmbeddedDependency(tempDirPath, dep);
                }
            }
        }

        private static string CreateTemporaryDirectory()
        {
            var path = Path.GetTempPath();
            var completePath = Path.Combine(path, "Iris2D");

            if (Directory.Exists(completePath))
                Directory.Delete(completePath, true);

            Directory.CreateDirectory(completePath);

            return completePath;
        }

        private static string ResourceNameToFileName(string resourceName)
        {
            return resourceName.Replace($"Iris.Binaries.{Architecture}.", "");
        }

        private static void ExtractAndLoadEmbeddedDependency(string targetDir, string fqn)
        {
            var actualFileName = ResourceNameToFileName(fqn);

            using var fs = new FileStream(Path.Combine(targetDir, actualFileName), FileMode.Create);
            using var ms = ThisAssembly.GetManifestResourceStream(fqn);
            ms.CopyTo(fs);

            Console.WriteLine(actualFileName);
            LoadLibrary(actualFileName);
        }
    }
}
