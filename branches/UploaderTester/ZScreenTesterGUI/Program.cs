using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenTesterGUI
{
    class Tester
    {
        public static string TestFile = @"..\..\..\test.jpg";

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Engine.TurnOn();
            Engine.LoadSettingsLatest();

            Application.Run(new TesterGUI());

            Engine.TurnOff();
        }
    }
}