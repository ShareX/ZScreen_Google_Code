namespace ZSS
{
    public delegate void StringEventHandler(object sender, string str);
    public delegate void StringsEventHandler(object sender, string[] strings);
    public delegate void IntegersEventHandler(object sender, int[] integers);
    public delegate void JobsEventHandler(object sender, Tasks.MainAppTask.Jobs jobs);
}