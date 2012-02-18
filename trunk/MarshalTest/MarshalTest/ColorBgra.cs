using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Test
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ColorBgra
    {
        [FieldOffset(0)]
        public byte B;

        [FieldOffset(1)]
        public byte G;

        [FieldOffset(2)]
        public byte R;

        [FieldOffset(3)]
        public byte A;

        [FieldOffset(0)]
        public uint Bgra;
    }
}