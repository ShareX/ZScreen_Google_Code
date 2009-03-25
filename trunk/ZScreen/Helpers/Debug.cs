using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace ZSS
{
    public class Debug
    {
        //private PerformanceCounter memoryPerfCounter;
        private Process currProc = Process.GetCurrentProcess();
        private Timer debugTimer;
        private TimerCallback timerDelegate;
        public Memory StartMemoryUsage { get; private set; }
        public Memory MemoryUsage { get; private set; }
        public Memory MinMemoryUsage { get; private set; }
        public Memory MaxMemoryUsage { get; private set; }
        public DateTime StartTime { get; private set; }
        public bool IsReady = false;

        public Debug()
        {
            StartTime = DateTime.Now;
            timerDelegate = new TimerCallback(TimerTick);
            debugTimer = new Timer(timerDelegate);
            debugTimer.Change(3000, 1000);
        }

        public string DebugInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Running since: " + StartTime.ToString("yyyy-MM-dd") + " " + StartTime.ToString("HH:mm:ss"));
            sb.AppendLine();
            sb.AppendLine("Up Time: " + TimeElapsed);
            sb.AppendLine();
            sb.AppendLine("  Start memory usage: " + StartMemoryUsage.ToString());
            sb.AppendLine("Current memory usage: " + MemoryUsage.ToString());
            sb.AppendLine("Minimum memory usage: " + MinMemoryUsage.ToString());
            sb.AppendLine("Maximum memory usage: " + MaxMemoryUsage.ToString());
            sb.AppendLine();
            sb.AppendLine("Running from:");
            sb.AppendLine(System.Windows.Forms.Application.ExecutablePath);
            sb.AppendLine();
            sb.AppendLine("Settings file:");
            sb.AppendLine(Program.DefaultXMLFilePath);
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

        public int GetMemoryUsage()
        {
            currProc.Refresh();
            return (int)currProc.PrivateMemorySize64 / 1024; // (int)(memoryPerfCounter.NextValue() / 1024);
        }

        private void TimerTick(Object stateInfo)
        {
            if (StartMemoryUsage == 0)
            {
                //memoryPerfCounter = new PerformanceCounter("Process", "Private Bytes", Process.GetCurrentProcess().ProcessName);                               
                MemoryUsage = GetMemoryUsage();
                StartMemoryUsage = MemoryUsage;
                MinMemoryUsage = MemoryUsage;
                MaxMemoryUsage = MemoryUsage;
                IsReady = true;
            }
            else
            {
                MemoryUsage = GetMemoryUsage();
                MinMemoryUsage = Math.Min(MinMemoryUsage, MemoryUsage);
                MaxMemoryUsage = Math.Max(MaxMemoryUsage, MemoryUsage);
            }
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
        /// <param name="dura"></param>
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