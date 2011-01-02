using System.Windows.Forms;

namespace HelpersLib
{
    public class HotkeyInfo
    {
        public ushort ID { get; private set; }
        public Keys Key { get; private set; }

        public HotkeyInfo(ushort id, Keys key)
        {
            ID = id;
            Key = key;
        }
    }
}