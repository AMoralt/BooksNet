using Domain.Root;
using Infrastructure.Repository;

namespace Infrastructure.Contracts;

public interface IEventRepository
{
    Task SaveAsync(IEvent Event, CancellationToken cancellationToken = default);
    Task<List<ForecastOut>> GetData(string ISBN, DateTime time, CancellationToken cancellationToken = default);
}