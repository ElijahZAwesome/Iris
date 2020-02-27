using System.Runtime.InteropServices;

namespace Iris.Internal
{
    internal static class WinApi
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool SetDllDirectory(string path);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool LoadLibrary(string path);
    }
}
