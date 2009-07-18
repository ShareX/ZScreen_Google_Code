using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZSS
{
    public class UploadInfo
    {
        public int UniqueID;
        public int UploadPercentage;

        public UploadInfo(int id)
        {
            this.UniqueID = id;
        }
    }
}