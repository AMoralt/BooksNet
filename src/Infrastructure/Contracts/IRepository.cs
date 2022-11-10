
using Domain.AggregationModels.Book;

namespace EmptyProjectASPNETCORE;

public interface IRepository<T> where T : Entity
{
    Task CreateAsync(T itemToCreate, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(T itemToUpdate, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}