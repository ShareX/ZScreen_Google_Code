using System;

namespace HelpersLib
{
    public static class Native
    {
        [Flags]
        public enum Modifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }

        public const int WM_HOTKEY = 0x312;
    }
}