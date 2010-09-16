using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DavuxLib.Win32API
{
    public class Shared
    {
        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left_, int top_, int right_, int bottom_)
            {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }

            public int Height { get { return Bottom - Top; } }
            public int Width { get { return Right - Left; } }
            public Size Size { get { return new Size(Width, Height); } }

            public Point Location { get { return new Point(Left, Top); } }

            // Handy method for converting to a System.Drawing.Rectangle
            public Rectangle ToRectangle()
            { return Rectangle.FromLTRB(Left, Top, Right, Bottom); }

            public static RECT FromRectangle(Rectangle rectangle)
            {
                return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }

            public override int GetHashCode()
            {
                return Left ^ ((Top << 13) | (Top >> 0x13))
                  ^ ((Width << 0x1a) | (Width >> 6))
                  ^ ((Height << 7) | (Height >> 0x19));
            }

            #region Operator overloads

            public static implicit operator Rectangle(RECT rect)
            {
                return rect.ToRectangle();
            }

            public static implicit operator RECT(Rectangle rect)
            {
                return FromRectangle(rect);
            }

            #endregion Operator overloads
        }

        /// <summary>
        /// The NCCALCSIZE_PARAMS structure contains information that an application can use
        /// while processing the WM_NCCALCSIZE message to calculate the size, position, and
        /// valid contents of the client area of a window.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]                   // This is the default layout for a structure
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rect0, rect1, rect2;                    // Can't use an array here so simulate one
            public IntPtr lppos;
        }

        /// <summary>
        /// Equivalent to the LoWord C Macro
        /// </summary>
        /// <param name="dwValue"></param>
        /// <returns></returns>
        public static int LoWord(int dwValue)
        {
            return dwValue & 0xFFFF;
        }

        /// <summary>
        /// Equivalent to the HiWord C Macro
        /// </summary>
        /// <param name="dwValue"></param>
        /// <returns></returns>
        public static int HiWord(int dwValue)
        {
            return (dwValue >> 16) & 0xFFFF;
        }
    }
}