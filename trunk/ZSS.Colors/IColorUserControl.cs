using System;
using System.Collections.Generic;
using System.Text;

namespace ZSS.Colors
{
    public interface IColorUserControl
    {
        void DrawSaturation();
        void DrawBrightness();
        void DrawRed();
        void DrawGreen();
        void DrawBlue();
    }
}
