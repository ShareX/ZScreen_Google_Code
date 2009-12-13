using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DavuxLib.Win32API;
using System.Runtime.InteropServices;

namespace DavuxLib.Controls
{
    public partial class GlassForm : Form
    {
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Aero")]
        [Description("Enable Aero Glass")]
        public bool GlassEnabled
        {
            get
            {
                return _GlassEnabled;
            }
            set
            {
                _GlassEnabled = value;
                Invalidate();
                if (value == false)
                {
                    NCAPainting = false;
                    SheetEffect = false;
                }
            }
        }
        private bool _GlassEnabled = true;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Aero")]
        [Description("Enable Painting in the NonClient area of the Window.")]
        public bool NCAPainting
        {
            get
            {
                return _NCAPainting;
            }
            set
            {
                _NCAPainting = value;
                Invalidate();
                if (value == true)
                {
                    GlassEnabled = true;
                    SheetEffect = false;
                }
            }
        }
        private bool _NCAPainting = false;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Aero")]
        [Description("Turn on Aero Glass for the entire surface.")]
        public bool SheetEffect
        {
            get
            {
                return _SheetEffect;
            }
            set
            {
                _SheetEffect = value;
                Invalidate();
            }
        }
        private bool _SheetEffect = false;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Aero")]
        [Description("Aero Glass Region")]
        public Padding GlassArea
        {
            get
            {
                return _GlassArea;
            }
            set
            {
                _GlassArea = value;
                if (DesignMode) Invalidate();
            }
        }
        private Padding _GlassArea = new Padding();


        private bool MarginsAdjusted = false;

        private void EnableGlass()
        {
            if (GlassEnabled)
            {
                DavuxLib.Win32API.DwmAPI.MARGINS GlassMargins = new DavuxLib.Win32API.DwmAPI.MARGINS();
                if (SheetEffect)
                {
                    GlassMargins.topHeight = GlassMargins.leftWidth = GlassMargins.rightWidth = GlassMargins.bottomHeight = -1;
                }
                else
                {
                    GlassMargins.leftWidth = GlassArea.Left;
                    GlassMargins.rightWidth = GlassArea.Right;
                    GlassMargins.bottomHeight = GlassArea.Bottom;
                    GlassMargins.topHeight = GlassArea.Top;
                }
                DavuxLib.Win32API.DwmAPI.DwmExtendFrameIntoClientArea(this, GlassMargins);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (DavuxLib.Win32API.DwmAPI.DwmIsCompositionEnabled() && GlassEnabled)
            {
                if (DesignMode)
                {
                    e.Graphics.Clear(Color.Silver);
                }
                else
                {
                    e.Graphics.Clear(Color.Transparent);
                }
                if (!SheetEffect)
                {
                    Rectangle clientArea = new Rectangle(GlassArea.Left, GlassArea.Top,
                        ClientRectangle.Width - GlassArea.Left - GlassArea.Right,
                        ClientRectangle.Height - GlassArea.Top - GlassArea.Bottom);
                    e.Graphics.FillRectangle(new SolidBrush(BackColor), clientArea);
                }
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            EnableGlass();
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            int WM_NCCALCSIZE = 0x83;
            int WM_NCHITTEST = 0x84;

            

            if (m.Msg == 0x84 // if this is a click
                && m.Result.ToInt32() == 1 // ...and it is on the client
                && this.IsOnGlass(m.LParam.ToInt32())) // ...and specifically in the glass area
            {
                //m.Result = HitTestNCA(m.HWnd, m.WParam, m.LParam);
                m.Result = new IntPtr(2); // lie and say they clicked on the title bar
                return;
            }
            if (m.Msg == 0x031E) // WM_DWMCOMPOSITIONCHANGED
            {
                EnableGlass();
                return;
            }
            else
            {
                if (NCAPainting)
                {
                    if (Win32API.DwmAPI.DwmDefWindowProc(ref m))
                    {
                        return;
                    }
                    else
                    {
                        if (m.Msg == WM_NCCALCSIZE && (int)m.WParam == 1)
                        {
                            Shared.NCCALCSIZE_PARAMS nccsp = (Shared.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(Shared.NCCALCSIZE_PARAMS));

                            // Adjust (shrink) the client rectangle to accommodate the border:
                            nccsp.rect0.Top += 0;
                            nccsp.rect0.Bottom += 0;
                            nccsp.rect0.Left += 0;
                            nccsp.rect0.Right += 0;

                            if (!MarginsAdjusted && !DesignMode)
                            {
                                _GlassArea.Top += (nccsp.rect2.Top - nccsp.rect1.Top);
                                _GlassArea.Left += (nccsp.rect2.Left - nccsp.rect1.Left);
                                _GlassArea.Right += (nccsp.rect1.Right - nccsp.rect2.Right);
                                _GlassArea.Bottom += (nccsp.rect1.Bottom - nccsp.rect2.Bottom);
                                MarginsAdjusted = true;
                            }
                            Marshal.StructureToPtr(nccsp, m.LParam, false);

                            m.Result = IntPtr.Zero;
                            return;
                        }
                        else if (m.Msg == WM_NCHITTEST && (int)m.Result == 0)
                        {
                            m.Result = HitTestNCA(m.HWnd, m.WParam, m.LParam);
                            return;
                        }
                    }
                }
            }
            base.WndProc(ref m);
        }

        private bool IsOnGlass(int lParam)
        {
            if (DavuxLib.Win32API.DwmAPI.DwmIsCompositionEnabled() && GlassEnabled)
            {
                int x = (lParam << 16) >> 16; // lo order word
                int y = lParam >> 16; // hi order word
                Point p = this.PointToClient(new Point(x, y));

                if (SheetEffect || GlassArea.Top < 0 || GlassArea.Bottom < 0 || GlassArea.Left < 0 || GlassArea.Right < 0)
                {
                    return true;
                }
                else if (p.Y > ClientRectangle.Height - GlassArea.Bottom)
                {
                    return true;
                }
                else if (p.Y < GlassArea.Top)
                {
                    return true;
                }
                else if (p.X < GlassArea.Left)
                {
                    return true;
                }
                else if (p.X > ClientRectangle.Width - GlassArea.Right)
                {
                    return true;
                }
            }
            return false;
        }


        private IntPtr HitTestNCA(IntPtr hwnd, IntPtr wparam, IntPtr lparam)
        {
            int HTNOWHERE = 0;
            int HTCLIENT = 1;
            int HTCAPTION = 2;
            int HTGROWBOX = 4;
            int HTSIZE = HTGROWBOX;
            int HTMINBUTTON = 8;
            int HTMAXBUTTON = 9;
            int HTLEFT = 10;
            int HTRIGHT = 11;
            int HTTOP = 12;
            int HTTOPLEFT = 13;
            int HTTOPRIGHT = 14;
            int HTBOTTOM = 15;
            int HTBOTTOMLEFT = 16;
            int HTBOTTOMRIGHT = 17;
            int HTREDUCE = HTMINBUTTON;
            int HTZOOM = HTMAXBUTTON;
            int HTSIZEFIRST = HTLEFT;
            int HTSIZELAST = HTBOTTOMRIGHT;

            Point p = new Point(Shared.LoWord((int)lparam), Shared.HiWord((int)lparam));

            Rectangle topleft = RectangleToScreen(new Rectangle(0, 0, GlassArea.Left, GlassArea.Left));

            if (topleft.Contains(p))
                return new IntPtr(HTTOPLEFT);

            Rectangle topright = RectangleToScreen(new Rectangle(Width - GlassArea.Right, 0, GlassArea.Right, GlassArea.Right));

            if (topright.Contains(p))
                return new IntPtr(HTTOPRIGHT);

            Rectangle botleft = RectangleToScreen(new Rectangle(0, Height - GlassArea.Bottom, GlassArea.Left, GlassArea.Bottom));

            if (botleft.Contains(p))
                return new IntPtr(HTBOTTOMLEFT);

            Rectangle botright = RectangleToScreen(new Rectangle(Width - GlassArea.Right, Height - GlassArea.Bottom, GlassArea.Right, GlassArea.Bottom));

            if (botright.Contains(p))
                return new IntPtr(HTBOTTOMRIGHT);

            Rectangle top = RectangleToScreen(new Rectangle(0, 0, Width, GlassArea.Left));

            if (top.Contains(p))
                return new IntPtr(HTTOP);

            Rectangle cap = RectangleToScreen(new Rectangle(0, GlassArea.Left, Width, GlassArea.Top - GlassArea.Left));

            if (cap.Contains(p) || SheetEffect)
                return new IntPtr(HTCAPTION);

            Rectangle left = RectangleToScreen(new Rectangle(0, 0, GlassArea.Left, Height));

            if (left.Contains(p))
                return new IntPtr(HTLEFT);

            Rectangle right = RectangleToScreen(new Rectangle(Width - GlassArea.Right, 0, GlassArea.Right, Height));

            if (right.Contains(p))
                return new IntPtr(HTRIGHT);

            Rectangle bottom = RectangleToScreen(new Rectangle(0, Height - GlassArea.Bottom, Width, GlassArea.Bottom));

            if (bottom.Contains(p))
                return new IntPtr(HTBOTTOM);

            return new IntPtr(HTCLIENT);
        }
        
    }
}
