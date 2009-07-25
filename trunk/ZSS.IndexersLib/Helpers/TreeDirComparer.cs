using System.Collections.Generic;

namespace ZSS.IndexersLib
{
    public class TreeDirComparer : IComparer<TreeDir>
    {
        public int Compare(TreeDir x, TreeDir y)
        {
            return string.Compare(x.DirectorySize().ToString("0000000000"), y.DirectorySize().ToString("0000000000"));
        }

    }
}
