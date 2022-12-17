using Infrastructure.Contracts;
using Dapper;
using Domain.AggregationModels.Book;
using Npgsql;

namespace Infrastructure.Repository;

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
    public async Task CreateAsync(Book itemToCreate, CancellationToken cancellationToken = default)
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
            Title = itemToCreate.Title.Value.ToLower(),
            Isbn = itemToCreate.Details.ISBN,
            Quantity = itemToCreate.Details.Quantity,
            Price = itemToCreate.Details.Price,
            PublicationDate = itemToCreate.Details.PublicationDate,
            PublisherId = itemToCreate.Publisher.Id,
            GenreId = itemToCreate.Genre.Id,
            FormatId = itemToCreate.Format.Id
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        var bookId = await connection.ExecuteScalarAsync(sql, param: parameters);
        
        var author = itemToCreate.Authors.Select(a => (int)a.Id );

        foreach (var id in author)
        {
            await connection.ExecuteAsync(sql2, new {AuthorId = id, BookId = bookId});
        }
        _changeTracker.Track(itemToCreate);
    }
    public async Task<IEnumerable<Book>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken = default)
    {
        string sql = @"
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
            OFFSET @Offset";

        if (limit != 0) //if limit is entered by user
            sql += "\rLIMIT @Limit";
        
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
                    new List<Author>{ new Author(author.Id.Value, author.LastName, author.FirstName)},
                    new Publisher(publisher.Id.Value, publisher.Name),
                    new BookFormat(format.Id.Value, format.Name)
                );
            }, splitOn:"title,isbn,id,id,id,id", param: new {Offset = offset, Limit = limit});
        
        books = books.GroupBy(p => p.Details.ISBN).Select(g =>
        {
            var groupedBooks = g.First();
            groupedBooks.Authors = g.Select(p => p.Authors.Single()).ToList();
            return groupedBooks;
        }).ToList();
        
        return books;
    }

    public async Task<IEnumerable<Book>> GetByGenreIdBooksAsync(int genreId, CancellationToken cancellationToken = default)
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
                  WHERE genre_id = @GenreId) 
                AS book
            JOIN publishers AS publisher ON book.publisher_id = publisher.id
            JOIN genres AS genre ON book.genre_id = genre.id
            JOIN author_book AS abook ON abook.book_id = book.id
            JOIN authors AS author ON author.id = abook.author_id
            JOIN book_formats AS bformat ON bformat.id = book.format_id";
        
        var parameters = new { GenreId = genreId };
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
            }, splitOn:"title,isbn,id,id,id,id", param: parameters);
        
        books = books.GroupBy(p => p.Details.ISBN).Select(g =>
        {
            var groupedBooks = g.First();
            groupedBooks.Authors = g.Select(p => p.Authors.Single()).ToList();
            return groupedBooks;
        }).ToList();
        
        return books;
    }

    public async Task<IEnumerable<Book>> GetByAuthorIdBooksAsync(int authorId, CancellationToken cancellationToken = default)
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
                      id,lastName,firstname
                  FROM authors
                  WHERE authors.id = @AuthorId)
                  AS author 
            JOIN author_book AS abook ON abook.author_id = author.id
            JOIN books AS book ON abook.book_id = book.id
            JOIN publishers AS publisher ON book.publisher_id = publisher.id
            JOIN genres AS genre ON book.genre_id = genre.id
            JOIN book_formats AS bformat ON bformat.id = book.format_id";
        
        var parameters = new { AuthorId = authorId };
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
            }, splitOn:"title,isbn,id,id,id,id", param: parameters);
        
        books = books.GroupBy(p => p.Details.ISBN).Select(g =>
        {
            var groupedBooks = g.First();
            groupedBooks.Authors = g.Select(p => p.Authors.Single()).ToList();
            return groupedBooks;
        }).ToList();
        
        return books;
    }

    public async Task<IEnumerable<Book>> GetByFiltersBooksAsync(string filter, CancellationToken cancellationToken = default)
    {
        //carefull, shity code =(
        //you was worried
        
        string sql1 = @"
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
                  WHERE title % @Filter OR ts @@ to_tsquery('russian', @Filter)) 
                AS book
            JOIN publishers AS publisher ON book.publisher_id = publisher.id
            JOIN genres AS genre ON book.genre_id = genre.id
            JOIN author_book AS abook ON abook.book_id = book.id
            JOIN authors AS author ON author.id = abook.author_id
            JOIN book_formats AS bformat ON bformat.id = book.format_id
            ";
        
        string sql2 = @"
            SELECT 
                book.id,
                book.title, 
                book.isbn, book.quantity, book.price, book.publicationdate,
                publisher.id, publisher.name,
                genre.id, genre.name,
                author.id, author.firstname,author.lastname,
                bformat.id, bformat.name
            FROM (SELECT
                    id, name
                FROM publishers
                WHERE name LIKE CONCAT('%',@Filter,'%')) 
                AS publisher
            JOIN books AS book ON book.publisher_id = publisher.id
            JOIN genres AS genre ON book.genre_id = genre.id
            JOIN author_book AS abook ON abook.book_id = book.id
            JOIN authors AS author ON author.id = abook.author_id
            JOIN book_formats AS bformat ON bformat.id = book.format_id
            ";
        
        string sql3 = @"
            SELECT 
                book.id,
                book.title, 
                book.isbn, book.quantity, book.price, book.publicationdate,
                publisher.id, publisher.name,
                genre.id, genre.name,
                author.id, author.firstname,author.lastname,
                bformat.id, bformat.name
            FROM (SELECT
                    id, firstname, lastname
                FROM authors
                WHERE firstname LIKE CONCAT('%',@Filter,'%') OR
                        lastname LIKE CONCAT('%',@Filter,'%')) 
                AS author
            JOIN author_book AS abook ON abook.author_id = author.id
            JOIN books AS book ON abook.book_id = book.id
            JOIN publishers AS publisher ON book.publisher_id = publisher.id
            JOIN genres AS genre ON book.genre_id = genre.id
            JOIN book_formats AS bformat ON bformat.id = book.format_id
            ";
        
        var parameters = new { Filter = filter.ToLower()};
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

        var books1 = await connection.QueryAsync<Book, Title, BookDetails, Publisher, Genre, Author,BookFormat, Book>(sql1,
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
            }, splitOn:"title,isbn,id,id,id,id", param: parameters);
        
        var books2 = await connection.QueryAsync<Book, Title, BookDetails, Publisher, Genre, Author,BookFormat, Book>(sql2,
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
            }, splitOn:"title,isbn,id,id,id,id", param: parameters);
        
        var books3 = await connection.QueryAsync<Book, Title, BookDetails, Publisher, Genre, Author,BookFormat, Book>(sql3,
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
            }, splitOn:"title,isbn,id,id,id,id", param: parameters);
        
        books1 = books1.GroupBy(p => p.Details.ISBN).Select(g =>
        {
            var groupedBooks = g.First();
            groupedBooks.Authors = g.Select(p => p.Authors.Single()).ToList();
            return groupedBooks;
        }).ToList();
        books2 = books2.GroupBy(p => p.Details.ISBN).Select(g =>
        {
            var groupedBooks = g.First();
            groupedBooks.Authors = g.Select(p => p.Authors.Single()).ToList();
            return groupedBooks;
        }).ToList();
        books3 = books3.GroupBy(p => p.Details.ISBN).Select(g =>
        {
            var groupedBooks = g.First();
            groupedBooks.Authors = g.Select(p => p.Authors.Single()).ToList();
            return groupedBooks;
        }).ToList();
        
        var books = books1.Concat(books2).Concat(books3);
        return books;
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
                id = @BookId;
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
            Title = itemToUpdate.Title.Value.ToLower(),
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
            FROM books
            WHERE books.isbn = @ISBN";
        
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

        var book = books.GroupBy(p => p.Id).Select(g =>
        {
            var groupBooks = g.First();
            groupBooks.Authors = g.SelectMany(p => p.Authors).ToList();
            return groupBooks;
        }).FirstOrDefault();

        _changeTracker.Track(book);
        return book;
    }

    public async Task UpdateQuantityAsync(string ISBN, int quantity, CancellationToken cancellationToken = default)
    {
        const string sqlUpdateBooks = @"
            UPDATE books
            SET 
                quantity = quantity + @Quantity
            WHERE
                isbn = @ISBN";
        
        var parameters = new
        {
            ISBN = ISBN,
            Quantity = quantity,
        };
        
        var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
        
        await connection.ExecuteAsync(sqlUpdateBooks, param: parameters);
    }
}