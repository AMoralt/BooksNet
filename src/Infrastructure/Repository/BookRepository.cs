using Dapper;
using Domain.AggregationModels.Book;
using Npgsql;

namespace EmptyProjectASPNETCORE;

public class BookRepository : IBookRepository
{
    public IUnitOfWork UnitOfWork { get; }
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    public BookRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    public Task CreateAsync(Book itemToCreate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        // const string sql = @"SELECT * FROM tickets";
        // var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        // var result = await connection.ExecuteAsync(sql);
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Book itemToUpdate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Book itemToDelete, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}