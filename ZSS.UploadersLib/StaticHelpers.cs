using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace UploadersLib
{
    internal static class StaticHelpers
    {
        public const string EXT_FTP_ACCOUNTS = "zfa";
        public static readonly string FilterFTPAccounts = string.Format("ZScreen FTP Accounts(*.{0})|*.{0}", EXT_FTP_ACCOUNTS);

        public static bool CheckList<T>(List<T> list, int selected)
        {
            return list.Count > 0 && selected >= 0 && list.Count > selected;
        }

        public static int Between(this int num, int min, int max)
        {
            if (num <= min) return min;
            if (num >= max) return max;
            return num;
        }

        public static float Between(this float num, float min, float max)
        {
            if (num <= min) return min;
            if (num >= max) return max;
            return num;
        }

        public static bool IsBetween(this int num, int min, int max)
        {
            return num >= min && num <= max;
        }


    }
}
