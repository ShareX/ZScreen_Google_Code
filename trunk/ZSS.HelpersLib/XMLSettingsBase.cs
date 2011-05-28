namespace HelpersLib
{
    public abstract class XMLSettingsBase<T> where T : new()
    {
        public bool Save(string filePath)
        {
            return SettingsHelper.Save(this, filePath, SerializationType.Xml);
        }

        public static T Load(string filePath)
        {
            return SettingsHelper.Load<T>(filePath, SerializationType.Xml);
        }
    }
}