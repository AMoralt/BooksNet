using Infrastructure.Contracts;
using Dapper;
using Domain.AggregationModels.Book;
using Npgsql;

namespace Infrastructure.Repository;

public class GenreRepository : IRepository<Genre>
{
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    private readonly IChangeTracker _changeTracker;
    public GenreRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
        IChangeTracker changeTracker)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _changeTracker = changeTracker;
    }
    public async Task CreateAsync(Genre itemToCreate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                INSERT INTO genres 
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

    public async Task<IEnumerable<Genre>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                genres.id,
                genres.name
            FROM genres";
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var genres =  await connection.QueryAsync<Genre>(sql);
        
        return genres; 
    }

    public async Task UpdateAsync(Genre itemToUpdate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE genres
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
            FROM genres
            WHERE id = @Id";
        
        var parameters = new { Id = id };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sql, param: parameters);

    }

    public async Task<Genre> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                genres.id,
                genres.name
            FROM genres
            WHERE 
                genres.id = @Id";
        
        var parameters = new { Id = id };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var genre = await connection.QueryFirstOrDefaultAsync<Genre>(sql, parameters);
        
        _changeTracker.Track(genre);
        return genre;
    }
}