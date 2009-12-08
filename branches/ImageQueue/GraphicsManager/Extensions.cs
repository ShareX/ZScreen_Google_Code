using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicsManager
{
    public static class Extensions
    {
        public static float Between(this float num, float min, float max)
        {
            if (num < min) return min;
            if (num > max) return max;
            return num;
        }

        public static int Between(this int num, int min, int max)
        {
            if (num < min) return min;
            if (num > max) return max;
            return num;
        }
    }
}