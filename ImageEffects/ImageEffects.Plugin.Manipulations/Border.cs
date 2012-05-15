using System.Drawing;
using HelpersLib.GraphicsHelper;
using ImageEffects.IPlugin;

namespace ImageManipulation
{
    public class Border : IPluginItem
    {
        public override string Name { get { return "Border"; } }

        public override string Description { get { return "Border"; } }

        private ImageEffectsHelper.BorderStyle borderStyle;

        public ImageEffectsHelper.BorderStyle BorderStyle
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
            borderStyle = ImageEffectsHelper.BorderStyle.Outside;
            color = Color.Black;
            size = 1;
        }

        public override Image ApplyEffect(Image img)
        {
            return ImageEffectsHelper.DrawBorder(borderStyle, img, color, size);
        }
    }
}