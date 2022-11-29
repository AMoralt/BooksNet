using Domain.AggregationModels.Book;

namespace Infrastructure.Contracts;

public interface IBookRepository
{
    Task CreateAsync(Book itemToCreate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetByGenreIdBooksAsync(int genreId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetByAuthorIdBooksAsync(int genreId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Book itemToUpdate, CancellationToken cancellationToken = default);
    Task DeleteAsync(string ISBN, CancellationToken cancellationToken = default);
    Task<Book> GetByISBNAsync(string ISBN, CancellationToken cancellationToken = default);
}