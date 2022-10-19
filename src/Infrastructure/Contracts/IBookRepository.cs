using Domain.AggregationModels.Book;

namespace EmptyProjectASPNETCORE;

public interface IBookRepository : IRepository<Book>
{
    Task CreateAsync(Book itemToCreate, CancellationToken cancellationToken = default);
    Task<IQueryable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Book itemToUpdate, CancellationToken cancellationToken = default);
    Task DeleteAsync(Book itemToDelete, CancellationToken cancellationToken = default);
}