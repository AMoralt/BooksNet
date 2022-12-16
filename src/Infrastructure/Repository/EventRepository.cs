using Dapper;
using Domain.AggregationModels.Book;
using Domain.Root;
using Infrastructure.Contracts;
using MediatR;
using Npgsql;

namespace Infrastructure.Repository;

public class EventRepository : IEventRepository
{
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    public EventRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task SaveAsync(IEvent Event, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                INSERT INTO events 
                    (isbn, name, quantity, price, created_at)
                VALUES 
                    (@Isbn, @Name, @Quantity, @Price, @Created_at)";
        
        var parameters = new
        {
            ISBN = Event.ISBN,
            Name = Event.Name,
            Quantity = Event.Quantity,
            Price = Event.Price,
            Created_at = DateTime.Now
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sql, param: parameters);
    }

    public Task GetData(string ISBN, DateTime time, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}