using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Plugins
{
    public delegate void TextChangedEventHandler(string text);

    public abstract class IPluginItem
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        public event TextChangedEventHandler PreviewTextChanged;

        protected void OnPreviewTextChanged(string text)
        {
            if (PreviewTextChanged != null)
            {
                PreviewTextChanged(text);
            }
        }

        public abstract bool ApplyEffect(Image img);
    }
}