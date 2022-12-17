using Dapper;
using Domain.Root;
using Infrastructure.Contracts;
using Infrastructure.Root;
using Microsoft.Extensions.Options;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;
using Npgsql;

namespace Infrastructure.Repository;

public class EventRepository : IEventRepository
{
    private readonly string _connectionString;
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    public EventRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IOptions<DatabaseConnectionOptions> options)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _connectionString = options.Value.ConnectionString;
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

    public async Task<ForecastOut> GetData(string ISBN, DateTime time, CancellationToken cancellationToken = default)
    {
        string sql = $@"
                SELECT quantity AS Quantity, created_at AS Created_at
                FROM events
                WHERE isbn = '{ISBN}' AND created_at >= '{time}'";
        
        string sql2 = @"SELECT MAX(created_at) AS max
                FROM events
                WHERE isbn = @ISBN1 AND created_at >= @time1";
        //isbn AS ISBN, name AS Name, , price AS Price, created_at AS Created_at
        var mlContext = new MLContext();
        
        DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<EventData>();
        
        var dbSource = new DatabaseSource(NpgsqlFactory.Instance, _connectionString, sql);

        IDataView data = loader.Load(dbSource);

        var pipeline = mlContext.Forecasting.ForecastBySsa(
            "Forecast",
            nameof(EventData.Quantity),
            windowSize: 5,
            seriesLength:10,
            trainSize:100,
            horizon:4
        );
        
        var model = pipeline.Fit(data);
        
        var forecastEngine = model.CreateTimeSeriesEngine<EventData, Forecast>(mlContext);
        
        var forecast = forecastEngine.Predict();
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var temp = await connection.QueryAsync<dynamic>(sql2, param: new { ISBN1 = ISBN, time1 = time });
        DateTime lastdate = temp.FirstOrDefault().max;
        var fore = new ForecastOut();
        fore.Date = lastdate;
        fore.ForecastedValues = forecast.ForecastedValues;
        
        return fore;
    }
}

public class EventData
{
    public string ISBN { get; set; }
    public string Name { get; set; }
    [LoadColumn(0)]
    public Single Quantity { get; set; }
    public int Price { get; set; }
    [LoadColumn(1)]
    public DateTime Created_at { get; set; }
}

public class Forecast
{
    [ColumnName("Forecast")]
    public float[] ForecastedValues { get; set; }
}

public class ForecastOut
{
    public DateTime Date { get; set; }
    [ColumnName("Forecast")]
    public float[] ForecastedValues { get; set; }
}