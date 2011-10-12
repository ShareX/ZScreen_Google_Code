﻿using System.Drawing;
using System.Drawing.Drawing2D;
using Plugins;
using GraphicsMgrLib;

namespace ImageManipulation
{
    public class Border : IPluginItem
    {
        public override string Name { get { return "Border"; } }

        public override string Description { get { return "Border"; } }

        private GraphicsMgrImageEffects.BorderStyle borderStyle;

        public GraphicsMgrImageEffects.BorderStyle BorderStyle
        {
            get
            {
                return borderStyle;
            }
            set
            {
                borderStyle = value;
                ChangePreviewText();
            }
        }

        private Color color;

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                ChangePreviewText();
            }
        }

        private int size;

        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                ChangePreviewText();
            }
        }

        private void ChangePreviewText()
        {
            OnPreviewTextChanged(string.Format("{0} {1}", color.ToString(), size));
        }

        public Border()
        {
            borderStyle = GraphicsMgrImageEffects.BorderStyle.Outside;
            color = Color.Black;
            size = 1;
        }

        public override Image ApplyEffect(Image img)
        {
            return GraphicsMgrImageEffects.DrawBorder(borderStyle, img, color, size);
        }
    }
}