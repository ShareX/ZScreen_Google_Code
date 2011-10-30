using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZScreenLib
{
    public static class SyntaxParser
    {
        public const string FilePath = "%filepath%";
        public const string LogsDir = "%logsdir%";

        public static string Parse(string text)
        {
            text = text.Replace(LogsDir, Engine.LogsDir);
            return text;
        }
    }
}
