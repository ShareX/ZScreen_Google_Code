using HelpersLib;

namespace UploadersLib
{
    internal static class StaticHelper
    {
        public const string EXT_FTP_ACCOUNTS = "zfa";
        public static readonly string FilterFTPAccounts = string.Format("ZScreen FTP Accounts(*.{0})|*.{0}", EXT_FTP_ACCOUNTS);
        public static Logger MyLogger { get; private set; }
    }
}