using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZSS
{
    public delegate void StringsEventHandler(object sender, string[] strings);
    public delegate void IntegersEventHandler(object sender, int[] integers);
    public delegate void JobsEventHandler(object sender, Tasks.MainAppTask.Jobs jobs);
}