﻿using Dapper;
using Domain.AggregationModels.Book;
using Npgsql;

namespace EmptyProjectASPNETCORE;

public class BookRepository : IBookRepository
{
    public IUnitOfWork UnitOfWork { get; }
    private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
    public BookRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    public Task CreateAsync(Book itemToCreate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IQueryable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"SELECT 
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
        //var result = await connection.ExecuteAsync(sql);
        //sql query to get all books with authors and publishers
        
        // var result = await connection.QueryAsync<Book, Title, ISBN, BookDetails,Publisher, Genre, Author, Book>(sql, (book, title,isbn, bookdetails,publisher,genre, author ) =>
        // {
        //     book.Title = title;
        //     book.ISBN = isbn;
        //     book.Details = bookdetails;
        //     book.Publisher = publisher;
        //     book.Genre = genre;
        //     book.Authors.Add(author);
        //     return book;
        // }, splitOn: "title,isbn,quantity,id,id,id");

        var books = await connection.QueryAsync<Book, Title, BookDetails, Publisher, Genre, Author,BookFormat, Book>(sql,
            (book, title, bookdetails, publisher, genre, author, format) =>
            {
                return new Book(
                    book.Id,
                    new BookDetails(bookdetails.Quantity, bookdetails.Price, bookdetails.PublicationDate,
                        bookdetails.ISBN),
                    new Title(title.Value),
                    new Genre(genre.Id, genre.Name),
                    new List<Author>{ new Author(author.Id, author.FirstName, author.LastName)},
                    new Publisher(publisher.Id, publisher.Name),
                    new BookFormat(format.Id, format.Name)
                );
            }, splitOn:"title,isbn,id,id,id,id");
        
        var result = books.GroupBy(p => p.Id).Select(g =>
        {
            var groupedPost = g.First();
            groupedPost.Authors = g.Select(p => p.Authors.Single()).ToList();
            return groupedPost;
        });
        return result.AsQueryable();
    }

    public Task UpdateAsync(Book ISBN, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public Task DeleteAsync(string ISBN, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetByISBNAsync(string ISBN, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}