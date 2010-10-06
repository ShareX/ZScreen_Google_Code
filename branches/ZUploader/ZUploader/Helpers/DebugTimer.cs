using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ZUploader
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

            timer = new Stopwatch();
            timer.Start();
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
            DateTime now = DateTime.Now;
            Debug.WriteLine(string.Format("{0} - {1}.{2} - {3}", now.ToShortDateString(), now.ToLongTimeString(), now.Millisecond, text));
        }

        public static void WriteLine(string text, string args)
        {
            WriteLine(string.Format(text, args));
        }
    }
}