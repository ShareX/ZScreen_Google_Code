using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZSS
{
    public class HKcombo
    {
        public Keys[] Mods;
        public Keys Key;

        public HKcombo()
        {

        }

        public HKcombo(Keys key)
        {
            Mods = null;

            Key = key;
        }

        public HKcombo(Keys mod1, Keys key)
        {
            Mods = new Keys[1];
            Mods[0] = mod1;

            Key = key;
        }

        public HKcombo(Keys mod1, Keys mod2, Keys key)
        {
            Mods = new Keys[2];
            Mods[0] = mod1;
            Mods[1] = mod2;

            Key = key;
        }

        public HKcombo(Keys[] mods, Keys key)
        {
            Mods = mods;

            Key = key;
        }
    }
}
