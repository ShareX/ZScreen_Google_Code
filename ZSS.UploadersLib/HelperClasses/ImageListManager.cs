using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using UploadersLib.Properties;

namespace UploadersLib.HelperClasses
{
    public class ImageListManager
    {
        private ListView lv;
        private ImageList il;

        public ImageListManager(ListView listView)
        {
            il = new ImageList();
            il.ColorDepth = ColorDepth.Depth32Bit;
            listView.SmallImageList = il;
        }

        public string AddImage(string key)
        {
            if (!il.Images.ContainsKey(key))
            {
                ResourceManager rm = Resources.ResourceManager;
                Image img = rm.GetObject(key) as Image;

                if (img != null)
                {
                    il.Images.Add(key, img);
                }
                else
                {
                    return string.Empty;
                }
            }

            return key;
        }
    }
}