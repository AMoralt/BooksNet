using Dapper;
using Domain.AggregationModels.Book;
using Npgsql;

namespace EmptyProjectASPNETCORE;

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
            Name = itemToCreate.Name
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
        
        var publishers =  await connection.QueryAsync(sql);

        if (!publishers.Any())
        {
            throw new System.Exception("No BookFormat found");
        }
        
        return publishers as IEnumerable<BookFormat>;
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
            Name = itemToUpdate.Name
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
        
        var bookFormat = await connection.QueryFirstOrDefaultAsync(sql, parameters);
        
        if (bookFormat is null)
        {
            throw new System.Exception($"BookFormat with id {id} not found");
        }

        _changeTracker.Track(bookFormat);
        return bookFormat;
    }
}