using System;
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenTesterGUI
{
    class Tester
    {
        public static string TestFilePicture = @"..\..\..\test.jpg";
        public static string TestFileText = @"..\..\..\test.txt";
        public static string TestFileBinary = @"..\..\..\test.pdf";

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