#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

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
        private static string ShellExtPath = string.Format("\"{0}\" \"%1\"", Application.ExecutablePath);

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