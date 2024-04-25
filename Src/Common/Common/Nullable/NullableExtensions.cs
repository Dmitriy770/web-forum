namespace Common.Nullable;

public static class NullableExtensions
{
    public static TResult Match<T, TResult>(
        this T? value, Func<T, TResult> whenValue, Func<TResult> whenNull)
    {
        return value is null ? whenNull() : whenValue(value);
    }
}