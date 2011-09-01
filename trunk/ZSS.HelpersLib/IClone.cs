namespace HelpersLib
{
    public interface IClone
    {
        T Clone<T>(T instance) where T : class;
    }
}
