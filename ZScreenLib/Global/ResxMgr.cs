using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ZScreenLib.Properties;

namespace ZScreenLib
{
    public static class ResxMgr
    {
        public static Icon BusyIcon
        {
            get
            {
                return Resources.zss_busy;
            }
        }

        public static Icon ReadyIcon
        {
            get
            {
                return Resources.zss_green;
            }
        }

        public static Icon UploadedIcon
        {
            get
            {
                return Resources.zss_uploaded;
            }
        }

    }
}
