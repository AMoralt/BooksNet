
using Domain.AggregationModels.Book;

namespace EmptyProjectASPNETCORE;

public interface IGenreRepository
{
    Task CreateAsync(Genre itemToCreate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Genre>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Genre itemToUpdate, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Genre> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}