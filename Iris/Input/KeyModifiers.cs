using System;

namespace Iris.Input
{
    [Flags]
    public enum KeyModifiers : byte
    {
        None   = 0,
        Ctrl   = 1 << 0,
        Alt    = 1 << 1,
        System = 1 << 2,
        Shift  = 1 << 3
    }
}
