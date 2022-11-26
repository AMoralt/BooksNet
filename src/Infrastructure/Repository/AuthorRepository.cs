using Dapper;
using Domain.AggregationModels.Book;
using Infrastructure.Contracts;
using Npgsql;

namespace Infrastructure.Repository;

public class AuthorRepository : IRepository<Author>
{
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    private readonly IChangeTracker _changeTracker;
    public AuthorRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
        IChangeTracker changeTracker)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _changeTracker = changeTracker;
    }

    public async Task CreateAsync(Author itemToCreate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                INSERT INTO authors 
                    (firstname, lastname)
                VALUES 
                    (@First, @Last)";
        
        var parameters = new
        {
            First = itemToCreate.FirstName,
            Last = itemToCreate.LastName
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sql, param: parameters);
        _changeTracker.Track(itemToCreate);
    }

    public async Task<IEnumerable<Author>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                authors.id,
                authors.lastname,
                authors.firstname
            FROM authors";
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var authors =  await connection.QueryAsync<Author>(sql);
        
        return authors;
    }

    public async Task UpdateAsync(Author itemToUpdate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE authors
            SET 
                firstname = @First,
                lastname = @Last
            WHERE id = @Id";
        
        var parameters = new
        {
            Id = itemToUpdate.Id,
            Last = itemToUpdate.LastName,
            First = itemToUpdate.FirstName
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sql, param: parameters);
        _changeTracker.Track(itemToUpdate);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            DELETE
            FROM authors
            WHERE id = @Id";
        
        var parameters = new { Id = id };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sql, param: parameters);
    }

    public async Task<Author> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                authors.id,
                authors.lastname,
                authors.firstname
            FROM authors
            WHERE 
                authors.id = @Id";
        
        var parameters = new { Id = id };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var author = await connection.QueryFirstOrDefaultAsync<Author>(sql, parameters);

        _changeTracker.Track(author);
        return author;
    }
}