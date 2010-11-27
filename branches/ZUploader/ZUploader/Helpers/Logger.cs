using System;
using System.Diagnostics;
using System.Text;

namespace ZUploader
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
            MessageFormat = "{0:dd/MM/yyyy HH:mm:ss.fff} - {1}";
            ExceptionFormat = "{0}:\r\n{1}";
        }

        public void WriteLine(string message)
        {
            string msg = string.Format(MessageFormat, FastDateTime.Now, message);
            Messages.AppendLine(msg);
            Debug.WriteLine(msg);
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
    }
}