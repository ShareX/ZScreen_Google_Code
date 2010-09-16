using System;
using System.Drawing;
using System.Windows.Forms;
using DavuxLib.Win32API;

namespace DavuxLib.Controls
{
    public partial class GlowLabel : Label
    {
        public GlowLabel()
        {
            InitializeComponent();
        }

        protected override void OnRegionChanged(EventArgs e)
        {
            base.OnRegionChanged(e);
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DwmAPI.DwmIsCompositionEnabled() && !DesignMode)
            {
                UXTheme.DrawText(e.Graphics, Text, this.Font, ClientRectangle, ForeColor, TextAlignToFormatFlags(TextAlign) | TextFormatFlags.NoClipping, UXTheme.TextStyle.Glowing);
            }
            else
            {
                base.OnPaint(e);
            }
        }

        private TextFormatFlags TextAlignToFormatFlags(ContentAlignment align)
        {
            switch (align)
            {
                case ContentAlignment.MiddleCenter:
                    return TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                case ContentAlignment.BottomCenter:
                    return TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom;
                case ContentAlignment.BottomLeft:
                    return TextFormatFlags.Left | TextFormatFlags.Bottom;
                case ContentAlignment.BottomRight:
                    return TextFormatFlags.Right | TextFormatFlags.Bottom;
                case ContentAlignment.MiddleLeft:
                    return TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                case ContentAlignment.MiddleRight:
                    return TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                case ContentAlignment.TopCenter:
                    return TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                case ContentAlignment.TopLeft:
                    return TextFormatFlags.Top | TextFormatFlags.Left;
                case ContentAlignment.TopRight:
                    return TextFormatFlags.Top | TextFormatFlags.Right;
            }
            return TextFormatFlags.HorizontalCenter | TextFormatFlags.Top;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (!DwmAPI.DwmIsCompositionEnabled() || DesignMode)
            {
                base.OnPaintBackground(e);
            }
        }
    }
}