using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZSS.Helpers
{
    [Serializable]
    public class HotkeyInfo
    {
        public string Name { get; set; }
        public Keys Key { get; set; }

        public HotkeyInfo() { }

        public HotkeyInfo(string myName, Keys myHotkey)
        {
            this.Name = myName;
            this.Key = myHotkey;
        }
    }
}
