using Dapper;
using Domain.AggregationModels.Book;
using Infrastructure.Contracts;
using Npgsql;

namespace Infrastructure.Repository;

public class BookFormatRepository : IRepository<BookFormat>
{
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    private readonly IChangeTracker _changeTracker;
    public BookFormatRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
        IChangeTracker changeTracker)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _changeTracker = changeTracker;
    }
    public async Task CreateAsync(BookFormat itemToCreate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                INSERT INTO book_formats 
                    (name)
                VALUES 
                    (@Name)";
        
        var parameters = new
        {
            Name = itemToCreate.Name.ToLower()
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sql, param: parameters);
        _changeTracker.Track(itemToCreate);
    }

    public async Task<IEnumerable<BookFormat>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                book_formats.id,
                book_formats.name
            FROM book_formats";
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var bookFormat =  await connection.QueryAsync<BookFormat>(sql);
        
        return bookFormat;
    }

    public async Task UpdateAsync(BookFormat itemToUpdate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE book_formats
            SET 
                name = @Name
            WHERE id = @Id";
        
        var parameters = new
        {
            Id = itemToUpdate.Id,
            Name = itemToUpdate.Name.ToLower()
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sql, param: parameters);
        _changeTracker.Track(itemToUpdate);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            DELETE
            FROM book_formats
            WHERE id = @Id";
        
        var parameters = new { Id = id };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sql, param: parameters);
    }

    public async Task<BookFormat> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                book_formats.id,
                book_formats.name
            FROM book_formats
            WHERE 
                book_formats.id = @Id";
        
        var parameters = new { Id = id };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var bookFormat = await connection.QueryFirstOrDefaultAsync<BookFormat>(sql, parameters);

        _changeTracker.Track(bookFormat);
        return bookFormat;
    }
}