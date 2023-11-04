using BuberBreakfast.Application.Common.Interfaces.Services;

namespace BuberBreakfast.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}