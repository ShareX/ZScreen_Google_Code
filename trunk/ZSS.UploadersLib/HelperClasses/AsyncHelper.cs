using System.ComponentModel;
using System.Windows.Forms;

namespace UploadersLib.HelperClasses
{
    public static class AsyncHelper
    {
        public static void AsyncJob(MethodInvoker thread, MethodInvoker threadCompleted)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (sender, e) => thread.Invoke();
            bw.RunWorkerCompleted += (sender, e) => threadCompleted.Invoke();
            bw.RunWorkerAsync();
        }
    }
}