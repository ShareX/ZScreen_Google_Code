using System.Windows.Forms;
using Microsoft.Win32;

namespace ZUploader
{
    public static class ShellContextMenu
    {
        private static string ShellExtMenuFiles = @"Software\Classes\*\shell\" + Application.ProductName;
        private static string ShellExtMenuFilesCmd = ShellExtMenuFiles + @"\command";
        private static string ShellExtMenuFolders = @"Software\Classes\Folder\shell\" + Application.ProductName;
        private static string ShellExtMenuFoldersCmd = ShellExtMenuFolders + @"\command";
        private static string ShellExtDesc = "Upload using " + Application.ProductName;
        private static string ShellExtPath = Application.ExecutablePath + " \"%1\"";

        public static void Register()
        {
            CreateRegistryKey(ShellExtMenuFiles, ShellExtDesc);
            CreateRegistryKey(ShellExtMenuFilesCmd, ShellExtPath);
            CreateRegistryKey(ShellExtMenuFolders, ShellExtDesc);
            CreateRegistryKey(ShellExtMenuFoldersCmd, ShellExtPath);
        }

        public static void Unregister()
        {
            RemoveRegistryKey(ShellExtMenuFilesCmd);
            RemoveRegistryKey(ShellExtMenuFiles);
            RemoveRegistryKey(ShellExtMenuFoldersCmd);
            RemoveRegistryKey(ShellExtMenuFolders);
        }

        public static bool Check()
        {
            return CheckRegistry(ShellExtMenuFilesCmd) && CheckRegistry(ShellExtMenuFoldersCmd);
        }

        private static void CreateRegistryKey(string path, string value)
        {
            using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(path))
            {
                if (rk != null)
                {
                    rk.SetValue(string.Empty, value);
                }
            }
        }

        private static void RemoveRegistryKey(string path)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(path))
            {
                if (rk != null)
                {
                    Registry.CurrentUser.DeleteSubKey(path);
                }
            }
        }

        private static bool CheckRegistry(string path)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(path))
            {
                if (rk != null && (string)rk.GetValue(string.Empty, "null") != "null")
                {
                    return true;
                }
            }

            return false;
        }
    }
}