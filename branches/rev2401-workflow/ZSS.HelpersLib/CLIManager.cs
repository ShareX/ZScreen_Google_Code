using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HelpersLib
{
    public enum ActionInput { None, Text, Number }

    public class CLIManager
    {
        public List<Command> Commands { get; set; }
        public Action<string> FilePathAction { get; set; }

        public bool Parse(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Trim();

                foreach (Command command in Commands)
                {
                    if (command.Parse(text)) return true;
                }

                if (FilePathAction != null && File.Exists(text))
                {
                    FilePathAction(text);
                }
            }

            return false;
        }
    }

    public class Command
    {
        public string Commands;
        public ActionInput InputType;
        public Action DefaultAction;
        public Action<string> TextAction;
        public Action<int> NumberAction;

        private Command(string commands)
        {
            Commands = commands;
        }

        public Command(string commands, Action action)
            : this(commands)
        {
            InputType = ActionInput.None;
            DefaultAction = action;
        }

        public Command(string commands, Action<int> action)
            : this(commands)
        {
            InputType = ActionInput.Number;
            NumberAction = action;
        }

        public Command(string commands, Action<string> action)
            : this(commands)
        {
            InputType = ActionInput.Text;
            TextAction = action;
        }

        public bool Parse(string text)
        {
            string input;

            switch (InputType)
            {
                default:
                case ActionInput.None:
                    if (Parse(text, out input))
                    {
                        DefaultAction();
                        return true;
                    }
                    break;
                case ActionInput.Number:
                    if (Parse(text, out input))
                    {
                        if (!string.IsNullOrEmpty(input))
                        {
                            int num;
                            if (int.TryParse(input, out num))
                            {
                                NumberAction(num);
                                return true;
                            }
                        }
                    }
                    break;
                case ActionInput.Text:
                    if (Parse(text, out input))
                    {
                        if (!string.IsNullOrEmpty(input))
                        {
                            TextAction(input);
                            return true;
                        }
                    }
                    break;
            }

            return false;
        }

        private bool Parse(string text, out string input)
        {
            input = null;

            string inputPattern = @"(?<Input>\w+|\x22.+?\x22)";
            string pattern = string.Format(@"-+(?:{0})(?:\s+{1}|\s*=+\s*{1}|\s+|\z)", Commands, inputPattern);

            Match match = Regex.Match(text, pattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            if (match.Success)
            {
                Group group = match.Groups["Input"];

                if (group.Success)
                {
                    input = group.Value;
                }
            }

            return match.Success;
        }
    }
}