using System;

namespace Iris.Internal.Boot
{
    internal class ModuleInitializer
    {
        private static EmbeddedDllLoader Loader { get; }

        static ModuleInitializer()
        {
            Loader = new EmbeddedDllLoader();
        }

        public static void Initialize()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Loader.InitializeNativeDlls();
            }
        }
    }
}
