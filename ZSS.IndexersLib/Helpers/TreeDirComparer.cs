using System.Collections.Generic;

namespace ZSS.IndexersLib
{
    public class TreeDirComparer : IComparer<cDir>
    {
        public int Compare(cDir x, cDir y)
        {
            return string.Compare(x.DirectorySize().ToString("0000000000"), y.DirectorySize().ToString("0000000000"));
        }

    }
}
