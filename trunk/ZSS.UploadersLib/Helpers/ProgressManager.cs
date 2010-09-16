#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2010 ZScreen Developers

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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ZUploader
{
    public class ProgressManager
    {
        public long Position { get; set; }
        public long Length { get; set; }
        public double Percentage { get; set; }
        public double Speed { get; set; }
        public DateTime StartTime { get; private set; }
        public TimeSpan Elapsed { get; set; }
        public TimeSpan Remaining { get; set; }

        private Stopwatch timer = new Stopwatch();
        private int smoothTime;
        private long speedTest;
        private List<double> averageSpeed = new List<double>(10);

        public ProgressManager(long length) : this(length, 500) { }

        public ProgressManager(long length, int smoothTime)
        {
            StartTime = DateTime.Now;
            Length = length;
            this.smoothTime = smoothTime;
            timer.Start();
        }

        public void ChangeProgress(int bytesRead)
        {
            Position += bytesRead;
            Percentage = (double)Position / Length * 100;
            speedTest += bytesRead;

            if (timer.ElapsedMilliseconds > smoothTime)
            {
                if (averageSpeed.Count == 10)
                {
                    averageSpeed.RemoveAt(0);
                }

                averageSpeed.Add((double)speedTest / timer.ElapsedMilliseconds);
                Speed = averageSpeed.Average();
                Elapsed = DateTime.Now - StartTime;
                Remaining = TimeSpan.FromMilliseconds((Length - Position) / Speed);

                speedTest = 0;
                timer.Reset();
                timer.Start();
            }
        }
    }
}