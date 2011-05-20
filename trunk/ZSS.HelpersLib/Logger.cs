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

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace HelpersLib
{
    public class Logger
    {
        public StringBuilder Messages { get; private set; }

        /// <summary>{0} = DateTime, {1} = Message</summary>
        public string MessageFormat { get; set; }

        /// <summary>{0} = Message, {1} = Exception</summary>
        public string ExceptionFormat { get; set; }

        public Logger()
        {
            Messages = new StringBuilder();
            MessageFormat = "{0:yyyy-MM-dd HH:mm:ss.fff} - {1}";
            ExceptionFormat = "{0}:\r\n{1}";
        }

        public void WriteLine(string message)
        {
            lock (this)
            {
                string msg = string.Format(MessageFormat, FastDateTime.Now, message);
                Messages.AppendLine(msg);
                Debug.WriteLine(msg);
            }
        }

        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        public void WriteException(string message, Exception exception)
        {
            WriteLine(string.Format(ExceptionFormat, message, exception));
        }

        public void WriteException(Exception exception)
        {
            WriteException("Exception", exception);
        }

        public void SaveLog(string filepath)
        {
            lock (this)
            {
                File.AppendAllText(filepath, Messages.ToString());
                Messages = new StringBuilder();
            }
        }
    }
}