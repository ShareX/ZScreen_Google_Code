using System;
using System.Text;
using System.Diagnostics;

namespace ZSS
{
    public class Debug
    {
        public event StringEventHandler GetDebugInfo;
        private Process currProc = Process.GetCurrentProcess();
        public System.Windows.Forms.Timer DebugTimer;
        public Memory StartMemoryUsage { get; private set; }
        public Memory MemoryUsage { get; private set; }
        public Memory MinMemoryUsage { get; private set; }
        public Memory MaxMemoryUsage { get; private set; }
        public DateTime StartTime { get; private set; }

        public Debug()
        {
            StartTime = DateTime.Now;
            DebugTimer = new System.Windows.Forms.Timer { Interval = 500 };
            DebugTimer.Tick += new EventHandler(TimerTick);
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