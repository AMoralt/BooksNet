using Domain.AggregationModels.Book;

namespace Infrastructure.Contracts;

public interface IBookRepository
{
    Task CreateAsync(Book itemToCreate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetByGenreIdBooksAsync(int genreId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetByAuthorIdBooksAsync(int genreId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetByTitleBooksAsync(string title, CancellationToken cancellationToken = default);
    Task UpdateAsync(Book itemToUpdate, CancellationToken cancellationToken = default);
    Task DeleteAsync(string ISBN, CancellationToken cancellationToken = default);
    Task<Book> GetByISBNAsync(string ISBN, CancellationToken cancellationToken = default);
    Task UpdateQuantityAsync(string ISBN, int quantity, CancellationToken cancellationToken = default);
}