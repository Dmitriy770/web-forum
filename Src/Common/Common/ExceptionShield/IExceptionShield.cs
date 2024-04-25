using System.Diagnostics;

namespace Common.ExceptionShield;

public interface IExceptionShield
{
    public T Process<T>(Action action);
}