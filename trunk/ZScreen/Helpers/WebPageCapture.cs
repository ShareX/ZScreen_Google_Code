using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZScreenLib
{
    public class WebPageCapture : IDisposable
    {
        public event ImageEventHandler DownloadCompleted;
        public delegate void ImageEventHandler(Image image);
        public WebBrowser webBrowser = new WebBrowser();

        public WebPageCapture()
        {
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            webBrowser.ScrollBarsEnabled = false;
            webBrowser.Size = Screen.PrimaryScreen.Bounds.Size;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Rectangle rect = webBrowser.Document.ActiveElement.ScrollRectangle;
            webBrowser.Size = new Size(rect.Width, rect.Height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            webBrowser.DrawToBitmap(bmp, rect);

            if (DownloadCompleted != null)
            {
                DownloadCompleted(bmp);
            }
        }

        public void DownloadPage(string url)
        {
            webBrowser.Navigate(url);
        }

        public void Dispose()
        {
            webBrowser.Dispose();
        }
    }
}