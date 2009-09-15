using System;
using System.IO;

namespace ZScreenTesterGUI
{
    public class MyConsole : StringWriter
    {
        public delegate void ConsoleEventHandler(string value);

        public event ConsoleEventHandler ConsoleWriteLine;

        public override void Write(string value)
        {
            throw new Exception(value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            OnConsoleWriteLine(new string(buffer));
            base.Write(buffer, index, count);
        }

        protected void OnConsoleWriteLine(string value)
        {
            if (ConsoleWriteLine != null)
            {
                ConsoleWriteLine(value);
            }
        }
    }
}