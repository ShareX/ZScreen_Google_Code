using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ZSS
{
    public class ColorBox : UserControl
    {
        #region Variables

        public MyColors.MyColor MyColor { get; set; }

        private eDrawStyle mDrawStyle;

        public eDrawStyle DrawStyle
        {
            get
            {
                return mDrawStyle;
            }
            set
            {
                mDrawStyle = value;
                DrawColors();
            }
        }

        public enum eDrawStyle
        {
            Hue, Saturation, Brightness, Red, Green, Blue
        }

        #endregion

        public ColorBox()
        {
            InitializeComponent();
            this.MyColor = Color.Red;
            this.DrawStyle = eDrawStyle.Hue;
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Name = "ColorBox";
            this.Size = new System.Drawing.Size(255, 255);
            this.Paint += new PaintEventHandler(ColorBox_Paint);
        }

        #endregion

        #region Events

        private void ColorBox_Paint(object sender, PaintEventArgs e)
        {
            DrawColors();
        }

        #endregion

        #region Private Methods

        private void DrawColors()
        {
            switch (DrawStyle)
            {
                case eDrawStyle.Hue:
                    DrawHue();
                    break;
                case eDrawStyle.Saturation:
                    DrawSaturation();
                    break;
                case eDrawStyle.Brightness:
                    DrawBrightness();
                    break;
                case eDrawStyle.Red:
                    DrawRed();
                    break;
                case eDrawStyle.Green:
                    DrawGreen();
                    break;
                case eDrawStyle.Blue:
                    DrawBlue();
                    break;
            }
        }

        // Saturation = 0 -> 100
        // Brightness = 100 -> 0
        private void DrawHue()
        {
            Graphics g = this.CreateGraphics();
            MyColors.HSB start = new MyColors.HSB(MyColor.HSB.Hue, 1.0, 0.0);
            MyColors.HSB end = new MyColors.HSB(MyColor.HSB.Hue, 0.0, 0.0);

            for (int i = 0; i < this.Height; i++)
            {
                start.Brightness = end.Brightness = 1.0 - (double)i / this.Height;
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, 1),
                    start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, this.Width, 1));
            }
        }

        // Hue = 0 -> 360
        // Brightness = 100 -> 0
        private void DrawSaturation()
        {
            Graphics g = this.CreateGraphics();
            MyColors.HSB start = new MyColors.HSB(0.0, MyColor.HSB.Saturation, 1.0);
            MyColors.HSB end = new MyColors.HSB(0.0, MyColor.HSB.Saturation, 0.0);

            for (int i = 0; i < this.Width; i++)
            {
                start.Hue = end.Hue = (double)i / this.Height;
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, this.Height),
                    start.ToColor(), end.ToColor(), 90, false);
                g.FillRectangle(brush, new Rectangle(i, 0, 1, this.Height));
            }
        }

        // Hue = 0 -> 360
        // Saturation = 100 -> 0
        private void DrawBrightness()
        {
            Graphics g = this.CreateGraphics();
            MyColors.HSB start = new MyColors.HSB(0.0, 1.0, MyColor.HSB.Brightness);
            MyColors.HSB end = new MyColors.HSB(0.0, 0.0, MyColor.HSB.Brightness);

            for (int i = 0; i < this.Width; i++)
            {
                start.Hue = end.Hue = (double)i / this.Height;
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, this.Height),
                    start.ToColor(), end.ToColor(), 90, false);
                g.FillRectangle(brush, new Rectangle(i, 0, 1, this.Height));
            }
        }

        // Blue = 0 -> 255
        // Green = 255 -> 0
        private void DrawRed()
        {
            Graphics g = this.CreateGraphics();
            MyColors.RGB start = new MyColors.RGB(MyColor.RGB.Red, 0, 0);
            MyColors.RGB end = new MyColors.RGB(MyColor.RGB.Red, 0, 255);

            for (int i = 0; i < this.Height; i++)
            {
                start.Green = end.Green = Round(255 - (255 * (double)i / (this.Height)));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, 1),
                  start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, this.Width, 1));
            }
        }

        // Blue = 0 -> 255
        // Red = 255 -> 0
        private void DrawGreen()
        {
            Graphics g = this.CreateGraphics();
            MyColors.RGB start = new MyColors.RGB(0, MyColor.RGB.Green, 0);
            MyColors.RGB end = new MyColors.RGB(0, MyColor.RGB.Green, 255);

            for (int i = 0; i < this.Height; i++)
            {
                start.Red = end.Red = Round(255 - (255 * (double)i / (this.Height)));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, 1),
                  start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, this.Width, 1));
            }
        }

        // Red = 0 -> 255
        // Green = 255 -> 0
        private void DrawBlue()
        {
            Graphics g = this.CreateGraphics();
            MyColors.RGB start = new MyColors.RGB(0, 0, MyColor.RGB.Blue);
            MyColors.RGB end = new MyColors.RGB(255, 0, MyColor.RGB.Blue);

            for (int i = 0; i < this.Height; i++)
            {
                start.Green = end.Green = Round(255 - (255 * (double)i / (this.Height)));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, 1),
                  start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, this.Width, 1));
            }
        }

        #endregion

        #region Private Static Helpers

        private static int Round(double val)
        {
            int ret_val = (int)val;

            int temp = (int)(val * 100);

            if ((temp % 100) >= 50)
                ret_val += 1;

            return ret_val;
        }

        #endregion
    }
}