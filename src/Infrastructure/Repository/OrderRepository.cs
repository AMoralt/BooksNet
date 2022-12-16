using Dapper;
using Domain.AggregationModels.Book;
using Domain.AggregationModels.Order;
using Domain.Root;
using Infrastructure.Contracts;
using MediatR;
using Npgsql;

namespace Infrastructure.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    private readonly IChangeTracker _changeTracker;
    public OrderRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _changeTracker = changeTracker;
    }

    public async Task<IEnumerable<Order>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken = default)
    {
        string sql = @"
            SELECT
                orders.id, orders.date,
                orderItem.book_id, orderItem.quantity, orderItem.price,
                book.id,
                book.title, 
                book.isbn, book.quantity AS bookq, book.price AS bookp, book.publicationdate,
                publisher.id AS publisherid, publisher.name AS publishername,
                genre.id AS genreid, genre.name AS genrename,
                author.id AS authorid, author.firstname,author.lastname,
                bformat.id AS bformatid, bformat.name AS formatname
            FROM orders
            JOIN order_items AS orderItem ON orderItem.order_id = orders.id
            JOIN books AS book ON book.id = orderItem.book_id 
            JOIN publishers AS publisher ON book.publisher_id = publisher.id
            JOIN genres AS genre ON book.genre_id = genre.id
            JOIN author_book AS abook ON abook.book_id = book.id
            JOIN authors AS author ON author.id = abook.author_id
            JOIN book_formats AS bformat ON bformat.id = book.format_id
            OFFSET @Offset";
        
        if (limit != 0) //if limit is entered by user
            sql += "\rLIMIT @Limit";

        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

        var queryResult = await connection.QueryAsync<dynamic>(sql,
            param: new {Offset = offset, Limit = limit});

        var orders = queryResult.GroupBy(x1 => x1.id).Select(x2 =>
            {
                var orderItems = x2.GroupBy(x3 => x3.isbn).Select(x4 =>
                {
                    var first = x4.First();
                    var orderItem = new OrderItem(first.id, first.quantity, first.price,
                        new Book(
                            first.book_id,
                            new BookDetails(first.bookq, first.bookp, first.publicationdate, first.isbn),
                            new Title(first.title),
                            new Genre(first.genreid, first.genrename),
                            x4.Select(x6 => new Author(x6.authorid, x6.lastname, x6.firstname)).ToList(),
                            new Publisher(first.publisherid, first.publishername),
                            new BookFormat(first.bformatid, first.formatname)
                        )); 
                    return orderItem;
                }).ToList();
                var first = x2.First();
                var order = new Order(first.id, new PurchaseDate(first.date), orderItems);
                return order;
            }).ToList();
        
        return orders;
    }

    public async Task CreateAsync(Order itemToCreate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                INSERT INTO orders 
                    (date) 
                VALUES 
                    (@Date)
                RETURNING id
            ";

        const string sql2 = @"
            INSERT INTO order_items 
                (order_id, book_id, quantity,price)
            VALUES
                (@OrderId, @BookId, @Quantity, @Price)";

        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var orderId = await connection.ExecuteScalarAsync(sql, param: new {Date = itemToCreate.DateTime.Value});

        foreach (var order in itemToCreate.OrderItems)
        {
            await connection.ExecuteAsync(sql2, new {OrderId = orderId, BookId = order.Book.Id, order.Quantity, order.Price});
        }
        _changeTracker.Track(itemToCreate);
    }
}