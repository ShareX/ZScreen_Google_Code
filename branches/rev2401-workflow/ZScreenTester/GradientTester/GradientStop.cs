using System;
using System.Drawing;
using System.Globalization;
using ZSS.ColorsLib;

namespace GradientTester
{
    public class GradientStop
    {
        public Color Color { get; set; }
        public float Offset { get; set; }

        public GradientStop(Color color, float offset)
        {
            Color = color;
            Offset = offset;
        }

        public GradientStop(string color, string offset)
        {
            this.Color = MyColors.ParseColor(color);

            if (this.Color == null)
            {
                throw new Exception("Color is unknown.");
            }

            float offset2;
            if (float.TryParse(offset, NumberStyles.Any, CultureInfo.InvariantCulture, out offset2))
            {
                this.Offset = offset2;
            }
            else
            {
                this.Offset = 0;
            }
        }
    }
}