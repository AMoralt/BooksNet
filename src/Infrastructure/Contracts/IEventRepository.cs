using Domain.Root;
using MediatR;

namespace Infrastructure.Contracts;

public interface IEventRepository
{
    Task SaveAsync(IEvent Event, CancellationToken cancellationToken = default);
    Task GetData(string ISBN, DateTime time, CancellationToken cancellationToken = default);
}