using System;
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenTesterGUI
{
    public class Tester
    {
        private static string TestFilePicture = @"..\..\..\test.jpg";
        private static string TestFileText = @"..\..\..\test.txt";
        private static string TestFileBinary = @"..\..\..\test.jpg";//pdf";

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Engine.TurnOn();
            Engine.LoadSettingsLatest();

            TesterGUI testerGUI = new TesterGUI
            {
                TestFileBinaryPath = TestFileBinary,
                TestFilePicturePath = TestFilePicture,
                TestFileTextPath = TestFileText
            };

            Application.Run(testerGUI);

            Engine.TurnOff();
        }
    }
}