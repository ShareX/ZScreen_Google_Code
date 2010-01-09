using System;
using System.Windows.Forms;
using ZScreenLib;
using System.IO;

namespace ZScreenCLI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if Release
            try
            {
#endif
            Engine.TurnOn(new ZScreenLib.Engine.EngineOptions { KeyboardHook = false, ShowConfigWizard = false });
            Engine.LoadSettingsLatest();
            Application.Run(new MainWindow());
#if Release
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex);
            }
            finally
            {
#endif
            Engine.TurnOff();
            Application.Exit();
#if Release
            }
#endif
        }
    }
}