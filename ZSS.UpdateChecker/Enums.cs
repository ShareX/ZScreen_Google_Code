using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ZSS.UpdateCheckerLib
{
    public enum UpdateCheckType
    {
        [Description("*-setup (Installer)")]
        SETUP,
        [Description("*-bin (Executable)")]
        BIN,
    }

    public static class UpdateCheckTypeExtensions
    {
        public static string ToDescriptionString(this UpdateCheckType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}