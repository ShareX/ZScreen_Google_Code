using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ZUploader
{
    public class ProgressManager
    {
        public long Position { get; set; }
        public long Length { get; set; }
        public double Percentage { get; set; }
        public double Speed { get; set; }
        public TimeSpan EstimatedCompleteTime { get; set; }

        private Stopwatch timer = new Stopwatch();
        private int smoothTime;
        private long speedTest;
        private List<double> averageSpeed = new List<double>();

        public ProgressManager(long length, int smoothTime)
        {
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
                averageSpeed.Add((double)speedTest / timer.ElapsedMilliseconds);
                if (averageSpeed.Count > 10)
                {
                    averageSpeed.RemoveAt(0);
                    Speed = averageSpeed.Average();
                }
                else
                {
                    Speed = averageSpeed.Last();
                }
                EstimatedCompleteTime = TimeSpan.FromMilliseconds((Length - Position) / Speed);
                speedTest = 0;
                timer.Reset();
                timer.Start();
            }

            Console.WriteLine(string.Format("{0} - {1}/{2} - {3}% - {4}kb/s - {5}",
                DateTime.Now.ToLongTimeString(), Position, Length, Percentage, Speed, EstimatedCompleteTime.TotalSeconds));
        }
    }
}