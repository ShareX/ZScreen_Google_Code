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
using System.Diagnostics;
using System.Windows.Forms;

namespace ZScreenLib.Helpers
{
    public class MyTimer : IDisposable
    {
        public string TimerText { get; set; }
        public bool ShowMessageBox { get; set; }
        public Stopwatch Timer { get; set; }

        public MyTimer() : this("Elapsed time", true) { }

        public MyTimer(string timerText) : this(timerText, true) { }

        public MyTimer(string timerText, bool showMessageBox)
        {
            TimerText = timerText;
            ShowMessageBox = showMessageBox;
            Timer = new Stopwatch();
            Timer.Start();
        }

        public string Stop()
        {
            string text = string.Format("{0}: {1}ms", TimerText, Timer.ElapsedMilliseconds.ToString());

            if (ShowMessageBox)
            {
                MessageBox.Show(text, "Timer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Console.WriteLine(text);
            }

            return text;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}