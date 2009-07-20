using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZSS
{
    [Serializable]
    public class DekiWikiHistory
    {
        public string Path;
        public DateTime Time;

        public DekiWikiHistory()
        {
            this.Path = "";
            this.Time = DateTime.Now;
        }

        public DekiWikiHistory(string Path, DateTime Time)
        {
            this.Path = Path;
            this.Time = Time;
        }
    }
}