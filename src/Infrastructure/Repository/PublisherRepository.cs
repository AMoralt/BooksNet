using Dapper;
using Domain.AggregationModels.Book;
using Npgsql;

namespace EmptyProjectASPNETCORE;

public class PublisherRepository : IRepository<Publisher>
{
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    private readonly IChangeTracker _changeTracker;
    public PublisherRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
        IChangeTracker changeTracker)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _changeTracker = changeTracker;
    }

    public async Task CreateAsync(Publisher itemToCreate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                INSERT INTO publishers 
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

    public async Task<IEnumerable<Publisher>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                publishers.id,
                publishers.name
            FROM publishers";
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var publishers =  await connection.QueryAsync<Publisher>(sql);

        if (!publishers.Any())
        {
            throw new System.Exception("No publishers found");
        }
        
        return publishers;
    }

    public async Task UpdateAsync(Publisher itemToUpdate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE publishers
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
            FROM publishers
            WHERE id = @Id";
        
        var parameters = new { Id = id };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sql, param: parameters);
    }

    public async Task<Publisher> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                publishers.id,
                publishers.name
            FROM publishers
            WHERE 
                publishers.id = @Id";
        
        var parameters = new { Id = id };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var publisher = await connection.QueryFirstOrDefaultAsync<Publisher>(sql, parameters);
        
        if (publisher is null)
        {
            throw new System.Exception($"Publisher with id {id} not found");
        }

        _changeTracker.Track(publisher);
        return publisher;
    }
}