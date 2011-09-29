using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HelpersLib.Hotkey
{
    public class HotkeySetting
    {
        public Keys Hotkey { get; set; }

        [XmlIgnore]
        public int Tag { get; set; }

        [XmlIgnore]
        public ToolStripMenuItem MenuItem { get; set; }

        public HotkeySetting()
        {
        }

        public HotkeySetting(Keys hotkey, int tag = -1, ToolStripMenuItem menuItem = null)
        {
            Hotkey = hotkey;
            Tag = tag;
            MenuItem = menuItem;
        }

        public void UpdateMenuItemShortcut()
        {
            if (MenuItem != null)
            {
                MenuItem.ShortcutKeyDisplayString = new KeyInfo(Hotkey).ToString();
            }
        }
    }
}