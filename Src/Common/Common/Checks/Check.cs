namespace Common.Checks;

public static class Check
{
    public static T NotNull<T>(T obj)
    {
        if (obj == null)
        {
            throw new ArgumentException("Must be not null", nameof(obj));
        }
        return obj;
    }
}