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

using System;
using System.Diagnostics;
using System.Text;

namespace ZScreenLib
{
    public class DebugHelper
    {
        public event StringEventHandler GetDebugInfo;
        private Process currProc = Process.GetCurrentProcess();
        private PerformanceCounter mCpuCounter = new PerformanceCounter();
        public System.Windows.Forms.Timer DebugTimer;
        public Memory StartMemoryUsage { get; private set; }
        public Memory MemoryUsage { get; private set; }
        public Memory MinMemoryUsage { get; private set; }
        public Memory MaxMemoryUsage { get; private set; }
        public DateTime StartTime { get; private set; }

        public DebugHelper()
        {
            StartTime = DateTime.Now;
            DebugTimer = new System.Windows.Forms.Timer { Interval = 500 };
            DebugTimer.Tick += new EventHandler(TimerTick);
            mCpuCounter.CategoryName = "Processor";
            mCpuCounter.CounterName = "% Processor Time";
            mCpuCounter.InstanceName = "_Total";
        }

        public string DebugInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Running since: " + StartTime.ToString("yyyy-MM-dd") + " " + StartTime.ToString("HH:mm:ss"));
            sb.AppendLine();
            sb.AppendLine("Up Time: " + TimeElapsed);
            sb.AppendLine();
            sb.AppendLine("  Start memory usage: " + StartMemoryUsage);
            sb.AppendLine("Current memory usage: " + MemoryUsage);
            sb.AppendLine("Minimum memory usage: " + MinMemoryUsage);
            sb.AppendLine("Maximum memory usage: " + MaxMemoryUsage);
            sb.AppendLine();
            sb.AppendLine("Running from:");
            sb.AppendLine(System.Windows.Forms.Application.ExecutablePath);
            sb.AppendLine();
            sb.AppendLine("Settings file:");
            sb.AppendLine(Engine.AppConf.XMLSettingsFile);
            return sb.ToString();
        }

        public string TimeElapsed
        {
            get
            {
                TimeSpan ts = DateTime.Now - StartTime;
                return GetDurationString(ts);
                // return ts.ToString().Substring(0, ts.ToString().LastIndexOf("."));
            }
        }

        public float GetProcessorUsage()
        {
            return mCpuCounter.NextValue();
        }

        public int GetMemoryUsage()
        {
            currProc.Refresh();
            return (int)currProc.PrivateMemorySize64 / 1024; // (int)(memoryPerfCounter.NextValue() / 1024);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (StartMemoryUsage == 0)
            {
                //memoryPerfCounter = new PerformanceCounter("Process", "Private Bytes", Process.GetCurrentProcess().ProcessName);
                MemoryUsage = GetMemoryUsage();
                StartMemoryUsage = MemoryUsage;
                MinMemoryUsage = MemoryUsage;
                MaxMemoryUsage = MemoryUsage;
            }
            else
            {
                MemoryUsage = GetMemoryUsage();
                MinMemoryUsage = Math.Min(MinMemoryUsage, MemoryUsage);
                MaxMemoryUsage = Math.Max(MaxMemoryUsage, MemoryUsage);
            }
            GetDebugInfo(this, DebugInfo());
        }

        public struct Memory
        {
            private int Value;

            public Memory(int value)
            {
                Value = value;
            }

            static public implicit operator Memory(int value) //Int -> Memory
            {
                return new Memory(value);
            }

            static public implicit operator int(Memory value) //Memory -> Int
            {
                return value.Value;
            }

            public override string ToString()
            {
                return Value == 0 ? "" : Value.ToString("#,#") + " KiB";
            }
        }

        /// <summary>
        /// Get DuratingString in HH:mm:ss
        /// </summary>
        /// <returns>DuratingString in HH:mm:ss</returns>
        public static string GetDurationString(TimeSpan ts)
        {
            double duraSec = ts.TotalSeconds;

            long hours = (long)duraSec / 3600;
            long secLeft = (long)duraSec - hours * 3600;
            long mins = secLeft / 60;
            long sec = secLeft - mins * 60;

            return string.Format("{0}:{1}:{2}", hours.ToString("00"), mins.ToString("00"), sec.ToString("00"));
        }
    }
}