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
using System.Windows.Forms;

namespace HelpersLib
{
    public class DebugTimer : IDisposable
    {
        private Stopwatch timer;
        private string text = "Elapsed time";
        private bool isMsgBox;
        private bool isStartTime;

        public DebugTimer()
        {
            Start();
        }

        public DebugTimer(string message, bool showStartTime = false, bool showMessageBox = false)
        {
            text = message;
            isStartTime = showStartTime;
            isMsgBox = showMessageBox;
            Start();
        }

        public void Start()
        {
            if (isStartTime)
            {
                WriteLine("{0} started.", text);
            }

            timer = Stopwatch.StartNew();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void ShowElapsedTime()
        {
            Stop();

            string msg = string.Format("{0}: {1}ms", text, timer.ElapsedMilliseconds);

            if (isMsgBox)
            {
                MessageBox.Show(msg);
            }
            else
            {
                WriteLine(msg);
            }
        }

        public void Dispose()
        {
            ShowElapsedTime();
        }

        public static void WriteLine(string text)
        {
            Debug.WriteLine(string.Format("{0:dd/MM/yyyy HH:mm:ss.fff} - {1}", FastDateTime.Now, text));
        }

        public static void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }
    }
}