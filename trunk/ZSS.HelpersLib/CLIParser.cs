using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpersLib
{
    public class CLIParser
    {
        private string input;
        private int i;

        public List<string> Parse(string input)
        {
            this.input = input;

            List<string> commands = new List<string>();

            string command = null;

            for (i = 0; i < input.Length; i++)
            {
                command = ParseUntilWhiteSpace();

                if (!string.IsNullOrEmpty(command))
                {
                    commands.Add(command);
                }
            }

            return commands;
        }

        private string ParseUntilWhiteSpace()
        {
            int start = i;

            for (; i < input.Length; i++)
            {
                if (char.IsWhiteSpace(input[i]))
                {
                    return input.Substring(start, i - start);
                }
                else if (input[i] == '"' && (i + 1) < input.Length)
                {
                    return ParseUntilDoubleQuotes();
                }
            }

            return input.Substring(start, i - start);
        }

        private string ParseUntilDoubleQuotes()
        {
            int start = ++i;

            for (; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    return input.Substring(start, i - start);
                }
            }

            return input.Substring(start, i - start);
        }
    }
}