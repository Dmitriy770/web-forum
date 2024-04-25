using FluentResults;

namespace Common.FluentResult;

public static class FluentResultExtensions
{
    public static TResult Match<T, TResult>(
        this Result<T> result, 
        Func<T, TResult> whenSuccess,
        Func<IList<IError>, TResult> whenError)
    {
        return result.IsSuccess ? whenSuccess(result.Value) : whenError(result.Errors);
    }

    public static void Match<T>(
        this Result<T> result, 
        Action<T> whenSuccess,
        Action<IList<IError>> whenError)
    {
        if (result.IsSuccess)
        {
            whenSuccess(result.Value);
        }
        else
        {
            whenError(result.Errors);
        }
    }
}