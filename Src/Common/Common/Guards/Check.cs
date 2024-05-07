using System.Text.RegularExpressions;

namespace Common.Guards;

public static class Check
{
    public static T NotNull<T>(T? obj, string name)
    {
        if (obj is null)
        {
            throw new ArgumentException("Must be not null", name);
        }
        return obj;
    }
    
    public static T NotNull<T>(T? str, string name) where T : struct
    {
        if (!str.HasValue)
        {
            throw new ArgumentException("Must be not null", name);
        }

        return str.Value;
    }

    public static int InRange(int value, int from, int to, string name)
    {
        if(value < from || value > to)
        {
            throw new ArgumentOutOfRangeException(name, $"Must be from {from} to {to} inclusive");
        }

        return value;
    }

    public static string ValidRegex(string value, string pattern, string name)
    {
        if (!Regex.IsMatch(value, pattern, RegexOptions.Multiline))
        {
            throw new ArgumentException("Invalid string", name);
        }

        return value;
    }
}