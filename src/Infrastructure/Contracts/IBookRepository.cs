using Domain.AggregationModels.Book;

namespace EmptyProjectASPNETCORE;

public interface IBookRepository
{
    Task CreateAsync(Book itemToCreate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Book ISBN, CancellationToken cancellationToken = default);
    Task DeleteAsync(string ISBN, CancellationToken cancellationToken = default);
    Task<Book> GetByISBNAsync(string ISBN, CancellationToken cancellationToken = default);
}