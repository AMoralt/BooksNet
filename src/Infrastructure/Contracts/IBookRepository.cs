using Domain.AggregationModels.Book;

namespace EmptyProjectASPNETCORE;

public interface IBookRepository : IRepository<Book>
{
    Task CreateAsync(Book itemToCreate, CancellationToken cancellationToken = default);
    Task<IQueryable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Book ISBN, CancellationToken cancellationToken = default);
    Task DeleteAsync(string ISBN, CancellationToken cancellationToken = default);
    Task<Book> GetByISBNAsync(string ISBN, CancellationToken cancellationToken = default);
}