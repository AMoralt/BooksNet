
using Domain.AggregationModels.Book;

namespace EmptyProjectASPNETCORE;

public interface IPublisherRepository
{
    Task CreateAsync(Publisher itemToCreate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Publisher>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Publisher itemToUpdate, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Publisher> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}