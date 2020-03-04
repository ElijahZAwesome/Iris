using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Iris.Internal.Boot
{
    internal class EmbeddedDllLoader
    {
        private static readonly string ArchitectureString = Environment.Is64BitOperatingSystem ? "Win64" : "Win32";

        internal void InitializeNativeDlls()
        {
            var tempDirPath = CreateDllDirectory();
            WinApi.SetDllDirectory(tempDirPath);

            var dependencies = EmbeddedResources.GetResourceNames()
                                                .Where(x => x.Contains(ArchitectureString) && x.EndsWith(".dll"));
            foreach (var dep in dependencies)
            {
                ExtractAndLoadEmbeddedDependency(tempDirPath, dep);
            }
        }

        private static string ResourceNameToFileName(string resourceName)
            => resourceName.Replace($"Iris.Binaries.{ArchitectureString}.", "");

        private static string CreateDllDirectory()
        {
            var path = Path.GetTempPath();
            var completePath = Path.Combine(path, "Iris2D");

            if (Directory.Exists(completePath))
                Directory.Delete(completePath, true);

            Directory.CreateDirectory(completePath);
            return completePath;
        }

        private static void ExtractAndLoadEmbeddedDependency(string targetDir, string fqn)
        {
            var actualFileName = ResourceNameToFileName(fqn);

            using var fs = new FileStream(Path.Combine(targetDir, actualFileName), FileMode.Create);
            using var ms = EmbeddedResources.GetResourceStream(fqn);
            ms.CopyTo(fs);

            Console.WriteLine(actualFileName);
            WinApi.LoadLibrary(actualFileName);
        }
    }
}