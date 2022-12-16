using Domain.AggregationModels.Order;
using Domain.Root;
using MediatR;

namespace Infrastructure.Contracts;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken = default);
    Task CreateAsync(Order itemToCreate, CancellationToken cancellationToken = default);
}