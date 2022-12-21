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

    public async Task<List<ForecastOut>> GetData(string ISBN, DateTime time, CancellationToken cancellationToken = default)
    {
        string sql = $@"
                SELECT e.created_at AS Created_at, SUM(e.quantity) AS Quantity
                FROM (
                        SELECT date_trunc('month', created_at) AS created_at, quantity AS quantity
                        FROM events
                        WHERE isbn = '{ISBN}' AND created_at >= '{time}') 
                    AS e
                GROUP BY created_at";

        string sql2 = @"SELECT date_trunc('month', created_at) AS Date, quantity AS ForecastedValues
                FROM events
                WHERE isbn = @ISBN AND created_at >= @time";
        var mlContext = new MLContext();

        DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<EventData>();

        var dbSource = new DatabaseSource(NpgsqlFactory.Instance, _connectionString, sql);

        IDataView data = loader.Load(dbSource);

        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        var lastdate = await connection.QueryAsync<ForecastOut>(sql2, param: new { ISBN = ISBN, time = time });

        var pipeline = mlContext.Forecasting.ForecastBySsa(
            "Forecast",
            nameof(EventData.Quantity),
            windowSize: 5,
            seriesLength:10,
            trainSize:100,
            horizon: 4
        );
        
        var model = pipeline.Fit(data);
        
        var forecastEngine = model.CreateTimeSeriesEngine<EventData, Forecast>(mlContext);
        
        var forecast = forecastEngine.Predict();

        var list = lastdate.ToList();
        
        for (int i = 1; i < 5; i++)
        {
            var obj = new ForecastOut(lastdate.Max(x => x.Date).AddMonths(i), forecast.ForecastedValues[i - 1]);
            list.Add(obj);
        }
        
        return list;
    }
}

public class EventData
{
    [LoadColumn(1)]
    public Single Quantity { get; set; }
    [LoadColumn(2)]
    public DateTime Created_at { get; set; }
}

public class Forecast
{
    [ColumnName("Forecast")]
    public float[] ForecastedValues { get; set; }
}

public class ForecastOut
{
    private ForecastOut() { }
    public ForecastOut(DateTime date, float forecastedValues)
    {
        Date = date;
        ForecastedValues = forecastedValues;
    }
    public DateTime Date { get; set; }
    [ColumnName("Forecast")]
    public float ForecastedValues { get; set; }
}