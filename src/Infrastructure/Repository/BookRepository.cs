using Dapper;
using Domain.AggregationModels.Book;
using Npgsql;

namespace EmptyProjectASPNETCORE;

public class BookRepository : IBookRepository
{
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    private readonly IChangeTracker _changeTracker;
    public BookRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
        IChangeTracker changeTracker)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _changeTracker = changeTracker;
    }
    public async Task CreateAsync(Book bookToCreate, CancellationToken cancellationToken = default)
    {
        const string sql = @"
                INSERT INTO books 
                    (title, isbn, quantity, price, publicationdate, publisher_id, genre_id, format_id)
                VALUES 
                    (@Title, @Isbn, @Quantity, @Price, @PublicationDate, @PublisherId, @GenreId, @FormatId)
                RETURNING id
            ";

        const string sql2 = @"
            INSERT INTO author_book 
                (author_id, book_id)
            VALUES
                (@AuthorId, @BookId)";
        
        
        var parameters = new
        {
            Title = bookToCreate.Title.Value,
            Isbn = bookToCreate.Details.ISBN,
            Quantity = bookToCreate.Details.Quantity,
            Price = bookToCreate.Details.Price,
            PublicationDate = bookToCreate.Details.PublicationDate,
            PublisherId = bookToCreate.Publisher.Id,
            GenreId = bookToCreate.Genre.Id,
            FormatId = bookToCreate.Format.Id
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var bookId = await connection.ExecuteScalarAsync(sql, param: parameters);
        
        var author = bookToCreate.Authors.Select(a => (int)a.Id );

        foreach (var id in author)
        {
            await connection.ExecuteAsync(sql2, new {AuthorId = id, BookId = bookId});
        }
        _changeTracker.Track(bookToCreate);
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                book.id,
                book.title, 
                book.isbn, book.quantity, book.price, book.publicationdate,
                publisher.id, publisher.name,
                genre.id, genre.name,
                author.id, author.firstname,author.lastname,
                bformat.id, bformat.name
            FROM books AS book
            JOIN publishers AS publisher ON book.publisher_id = publisher.id
            JOIN genres AS genre ON book.genre_id = genre.id
            JOIN author_book AS abook ON abook.book_id = book.id
            JOIN authors AS author ON author.id = abook.author_id
            JOIN book_formats AS bformat ON bformat.id = book.format_id
            ";
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var books = await connection.QueryAsync<Book, Title, BookDetails, Publisher, Genre, Author,BookFormat, Book>(sql,
            (book, title, bookdetails, publisher, genre, author, format) =>
            {
                return new Book(
                    book.Id,
                    new BookDetails(bookdetails.Quantity, bookdetails.Price, bookdetails.PublicationDate,
                        bookdetails.ISBN),
                    new Title(title.Value),
                    new Genre(genre.Id.Value, genre.Name),
                    new List<Author>{ new Author(author.Id.Value, author.FirstName, author.LastName)},
                    new Publisher(publisher.Id.Value, publisher.Name),
                    new BookFormat(format.Id.Value, format.Name)
                );
            }, splitOn:"title,isbn,id,id,id,id");

        if (books.Any())
        {
            books = books.GroupBy(p => p.Id).Select(g =>
            {
                var groupedPost = g.First();
                groupedPost.Authors = g.Select(p => p.Authors.Single()).ToList();
                return groupedPost;
            });
            return books;
        }
        
        return null;
    }

    public async Task UpdateAsync(Book itemToUpdate, CancellationToken cancellationToken = default)
    {
        const string sqlUpdateBooks = @"
            UPDATE books
            SET 
                title = @Title,
                quantity = @Quantity,
                price = @Price, 
                publicationdate = @PublicationDate,
                publisher_id = @PublisherId, 
                genre_id = @GenreId,
                format_id = @FormatId
            WHERE
                id = @Id;
            DELETE
            FROM author_book 
            WHERE 
                book_id = @BookId";

        const string sqlUpdateABook = @"
            INSERT INTO author_book 
                (author_id, book_id)
            VALUES
                (@AuthorId, @BookId)";
        var parameters = new
        {
            BookId = itemToUpdate.Id,
            Title = itemToUpdate.Title.Value,
            Quantity = itemToUpdate.Details.Quantity,
            Price = itemToUpdate.Details.Price,
            PublicationDate = itemToUpdate.Details.PublicationDate,
            PublisherId = itemToUpdate.Publisher.Id,
            GenreId = itemToUpdate.Genre.Id,
            FormatId = itemToUpdate.Format.Id
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sqlUpdateBooks, param: parameters);
        
        var author = itemToUpdate.Authors.Select(a => (int)a.Id );

        foreach (var id in author)
        {
            await connection.ExecuteAsync(sqlUpdateABook, new {AuthorId = id, BookId = itemToUpdate.Id});
        }
        _changeTracker.Track(itemToUpdate);
    }
    public async Task DeleteAsync(string ISBN, CancellationToken cancellationToken = default)
    {
       const string sql = @"
            DELETE
            FROM books AS book
            WHERE book.isbn = @ISBN";
        
        var parameters = new { ISBN = ISBN };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sql, param: parameters);
    }

    public async Task<Book> GetByISBNAsync(string ISBN, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                book.id,
                book.title, 
                book.isbn, book.quantity, book.price, book.publicationdate,
                publisher.id, publisher.name,
                genre.id, genre.name,
                author.id, author.firstname,author.lastname,
                bformat.id, bformat.name
            FROM (SELECT
                      id, title, isbn, quantity, price, publicationdate,
                      publisher_id, genre_id, format_id
                  FROM books
                  WHERE isbn = @ISBN) 
                AS book
            JOIN publishers AS publisher ON book.publisher_id = publisher.id
            JOIN genres AS genre ON book.genre_id = genre.id
            JOIN author_book AS abook ON abook.book_id = book.id
            JOIN authors AS author ON author.id = abook.author_id
            JOIN book_formats AS bformat ON bformat.id = book.format_id
            ";
        
        var parameters = new { ISBN = ISBN };
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

        
        var books = await connection
            .QueryAsync<Book, Title, BookDetails, Publisher, Genre, Author, BookFormat, Book>(sql,
                (book, title, bookdetails, publisher, genre, author, format) =>
                {
                    return new Book(
                        book.Id,
                        new BookDetails(bookdetails.Quantity, bookdetails.Price, bookdetails.PublicationDate,
                            bookdetails.ISBN),
                        new Title(title.Value),
                        new Genre(genre.Id.Value, genre.Name),
                        new List<Author> { new Author(author.Id.Value, author.FirstName, author.LastName) },
                        new Publisher(publisher.Id.Value, publisher.Name),
                        new BookFormat(format.Id.Value, format.Name)
                    );
                }, splitOn: "title,isbn,id,id,id,id", param: parameters);
        
        if (books.Any())
        {
            var book = books.FirstOrDefault();
            book.Authors = books.Select(p => p.Authors.Single()).ToList();
            _changeTracker.Track(book);
            return book;
        }

        return null;;
    }
}