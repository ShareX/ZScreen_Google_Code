using System.IO;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib;
using ZScreenGUI.Properties;
using ZScreenLib;

#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
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

namespace ZScreenGUI
{
    public static class FormsMgr
    {
        private static AboutBox _AboutWindow = null;
        private static TextViewer _LicenseWindow = null;

        private static ZScreenOptionsUI _OptionsUI = null;
        private static ApiKeysUI _ApiKeysUI = null;

        private static ProxyConfigUI _ProxyConfig = null;
        private static TextViewer _VersionHistoryWindow = null;

        private static TextViewer FillTextViewer(TextViewer viewer, string title, string manifestFileName)
        {
            string txt = FileSystem.GetTextFromFile(Path.Combine(Application.StartupPath, manifestFileName));
            if (txt == string.Empty)
            {
                txt = FileSystem.GetText(manifestFileName);
            }

            if (txt != string.Empty)
            {
                viewer = new TextViewer(string.Format("{0} - {1}", Application.ProductName, title), txt) { Icon = Resources.zss_main };
            }

            return viewer;
        }

        public static ZScreenOptionsUI OptionsUI
        {
            get
            {
                if (_OptionsUI == null || _OptionsUI.IsDisposed)
                {
                    _OptionsUI = new ZScreenOptionsUI(Engine.ConfigOptions) { Icon = Resources.zss_tray };
                }
                return _OptionsUI;
            }
            private set
            {
                _OptionsUI = value;
            }
        }

        public static ApiKeysUI ApiKeysUI
        {
            get
            {
                if (_ApiKeysUI == null || _ApiKeysUI.IsDisposed)
                {
                    _ApiKeysUI = new ApiKeysUI(Engine.ConfigUI.ApiKeys) { Icon = Resources.zss_tray };
                }
                return _ApiKeysUI;
            }
            private set
            {
                _ApiKeysUI = value;
            }
        }

        public static ProxyConfigUI ProxyConfig
        {
            get
            {
                if (_ProxyConfig == null || _ProxyConfig.IsDisposed)
                {
                    _ProxyConfig = new ProxyConfigUI(Engine.ConfigUI.ConfigProxy) { Icon = Resources.zss_tray };
                }
                return _ProxyConfig;
            }
            private set
            {
                _ProxyConfig = value;
            }
        }

        public static void ShowAboutWindow()
        {
            if (_AboutWindow == null || _AboutWindow.IsDisposed)
            {
                _AboutWindow = new AboutBox();
            }

            _AboutWindow.Activate();
            _AboutWindow.Show();
        }

        public static void ShowLicense()
        {
            if (_LicenseWindow == null || _LicenseWindow.IsDisposed)
            {
                _LicenseWindow = FillTextViewer(_LicenseWindow, "License", "license.txt");
            }

            if (_LicenseWindow != null)
            {
                _LicenseWindow.Activate();
                _LicenseWindow.Show();
            }
        }

        public static void ShowOptionsUI()
        {
            OptionsUI.Activate();
            OptionsUI.Show();
        }

        public static void ShowApiKeysUI()
        {
            ApiKeysUI.Activate();
            ApiKeysUI.Show();
        }

        public static DialogResult ShowDialogProxyConfig()
        {
            ProxyConfig.Activate();
            return ProxyConfig.ShowDialog();
        }

        public static void ShowVersionHistory()
        {
            if (_VersionHistoryWindow == null || _VersionHistoryWindow.IsDisposed)
            {
                _VersionHistoryWindow = FillTextViewer(_VersionHistoryWindow, "Version History", "VersionHistory.txt");
            }

            if (_VersionHistoryWindow != null)
            {
                _VersionHistoryWindow.Activate();
                _VersionHistoryWindow.Show();
            }
        }
    }
}