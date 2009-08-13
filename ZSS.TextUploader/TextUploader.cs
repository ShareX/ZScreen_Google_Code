#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ZSS.TextUploadersLib.Helpers;
using ZSS.TextUploadersLib.URLShorteners;
using ZSS.UploadersLib;

namespace ZSS.TextUploadersLib
{
    [Serializable]
    public abstract class TextUploader : Uploader
    {
        #region ** THIS HAS TO BE UP-TO-DATE OTHERWISE XML SERIALIZING IS GOING TO FUCK UP **
        public static List<Type> Types = new List<Type> { typeof(FTPUploader), typeof(Paste2Uploader), typeof(PastebinCaUploader),
            typeof (PastebinUploader), typeof(SlexyUploader), typeof(SniptUploader), typeof(TinyURLUploader), typeof(ThreelyUploader),
            typeof(KlamUploader), typeof(IsgdUploader), typeof(BitlyUploader), typeof(TextUploader) };

        /// <summary>
        /// String that is uploaded
        /// </summary>
        #endregion

        protected TextUploader() { }

        /// <summary>
        /// Descriptive name for the Text Uploader
        /// </summary>
        public override string ToString()
        {
            return "TextUploader";
        }

        /// <summary>
        /// String used to test the functionality
        /// </summary>
        public virtual string TesterString
        {
            get { return "http://code.google.com/p/zscreen"; }
        }

        public virtual object Settings { get; set; }

        public abstract string UploadText(TextInfo text);

        public string UploadTextFromClipboard()
        {
            if (Clipboard.ContainsText())
            {
                return UploadText(TextInfo.FromClipboard());
            }
            else if (Clipboard.ContainsFileDropList())
            {
                string filePath = Clipboard.GetFileDropList()[0];
                if (filePath.EndsWith(".txt"))
                {
                    return UploadTextFromFile(filePath);
                }
            }
            return "";
        }

        public string UploadTextFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return UploadText(TextInfo.FromFile(filePath));
            }
            return "";
        }
    }

    public abstract class TextUploaderSettings
    {
        public abstract string Name { get; set; }
        public abstract string URL { get; set; }
    }
}