using WebForum.Core.Application.Interfaces;

namespace WebForum.Core.Infrastructure.Utilities;

public sealed class DataTimeProvider : IDataTimeProvider
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}