using System.IO;
using System.Reflection;

namespace Iris.Internal
{
    internal static class EmbeddedResources
    {
        public const string IconResourceString = "Iris.Resources.Logo.png";

        private static readonly Assembly ThisAssembly = Assembly.GetExecutingAssembly();

        public static string[] GetResourceNames()
            => ThisAssembly.GetManifestResourceNames();
        
        public static Stream GetResourceStream(string fullyQualifiedName)
            => ThisAssembly.GetManifestResourceStream(fullyQualifiedName);

    }
}