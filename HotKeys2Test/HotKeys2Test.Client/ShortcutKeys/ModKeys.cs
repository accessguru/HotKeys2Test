﻿namespace HotKeys2Test.Client.ShortcutKeys
{
    [Flags]
    public enum ModKeys
    {
        None = 0,
        Shift = 0x01,
        Ctrl = 0x02,
        Alt = 0x04,
        Meta = 0x08
    }
}