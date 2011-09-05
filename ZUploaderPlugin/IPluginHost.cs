using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZUploaderPluginBase
{
    public interface IPluginHost
    {
        void AddPluginButton(ToolStripMenuItem tsmi);

        void UploadFile(string path);

        void UploadFile(string[] files);

        void UploadImage(Image img);

        void UploadText(string text);

        void Hide();

        void Show();

        void RegisterPluginHotkey(Keys key, Action action);

        void UnregisterPluginHotkey(Keys key, Action action);
    }
}