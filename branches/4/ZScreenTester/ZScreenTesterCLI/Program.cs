using System;
using ZScreenLib;

namespace ZScreenTesterCLI
{
    class Tester
    {
        public static string TestFile = @"C:\Users\PC\Desktop\test.jpg";

        private static void Main(string[] args)
        {
            Engine.TurnOn();
            Engine.LoadSettings();
            TesterCLI.Test();
            Console.ReadLine();
            Engine.TurnOff();
        }
    }
}