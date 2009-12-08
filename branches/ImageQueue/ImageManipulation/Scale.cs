using System.Drawing;
using System.Drawing.Drawing2D;
using Plugins;
using GraphicsManager;

namespace ImageManipulation
{
    public class Scale : IPluginItem
    {
        public override string Name { get { return "Scale"; } }

        public override string Description { get { return "Scale"; } }

        private float percentage;

        public float Percentage
        {
            get
            {
                return percentage;
            }
            set
            {
                percentage = value.Between(1, 1000);
                OnPreviewTextChanged(percentage + "%");
            }
        }

        public Scale()
        {
            percentage = 100;
        }

        public override Image ApplyEffect(Image img)
        {
            return GraphicsMgr.ChangeImageSize(img, percentage);
        }
    }
}