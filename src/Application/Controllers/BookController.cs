using Application.Exception;
using Application.Commands;
using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("[controller]")]
[ApiController]
public class BookController
{
    private readonly IMediator _mediator;
    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Gets the list of all Books
    /// </summary>
    /// <returns>The list of Books</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetAll(int limit, int offset, CancellationToken token)
    {
        try
        {
            var getAll = new GetAllBooksQuery(limit, offset);
            var result = await _mediator.Send(getAll, token);
            return Results.Ok(result);
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Gets Book by ISBN
    /// </summary>
    /// <param name="ISBN"></param>
    [HttpGet("{ISBN}")]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetByISBN(string ISBN, CancellationToken token)
    {
        try
        {
            var getByISBN = new GetByISBNBookQuery(ISBN);
            var result = await _mediator.Send(getByISBN, token);
            return Results.Ok(result);
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Gets Books by Genre's Id
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("genre/{id:int}")]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetByGenreId(int id, CancellationToken token)
    {
        try
        {
            var getByGenreId = new GetByGenreIdBooksQuery(id);
            var result = await _mediator.Send(getByGenreId, token);
            return Results.Ok(result);
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    } 
    
    /// <summary>
    /// Get Books by filters
    /// </summary>
    /// <param name="filter"></param>
    [HttpGet("title/{filter}")]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetByFilters(string filter, CancellationToken token)
    {
        try
        {
            var bookToFind = new GetByFiltersBooksQuery(filter);
            var result = await _mediator.Send(bookToFind, token);
            return Results.Ok(result);
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    } 
    /// <summary>
    /// Gets Books by Author's Id
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("author/{id:int}")]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetByAuthorId(int id, CancellationToken token)
    {
        try
        {
            var getByAuthorId = new GetByAuthorIdBooksQuery(id);
            var result = await _mediator.Send(getByAuthorId, token);
            return Results.Ok(result);
        }
        catch (NotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    } 
    
    /// <summary>
    /// Deletes Book by ISBN
    /// </summary>
    /// <param name="ISBN"></param>
    [HttpDelete("{ISBN}")]
    [ProducesResponseType(204)]
    [Produces("application/json")]
    public async Task<IResult> DeleteByISBN(string ISBN, CancellationToken token)
    {
        try
        {
            var deleteBookCommand = new DeleteBookCommand(ISBN);
            await _mediator.Send(deleteBookCommand, token);
            return Results.NoContent();
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Creates Book
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Book
    ///     {
    ///         "title": "Book title",
    ///         "genreId": 1,
    ///         "formatId": 1,
    ///         "authors": [
    ///             1
    ///         ],
    ///         "publisherId": 1,
    ///         "isbn": "2222222222",
    ///         "publicationdate": "2020",
    ///         "price": 200,
    ///         "quantity": 20
    ///    }
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(201)]
    [Produces("application/json")]
    public async Task<IResult> Create([FromBody] CreateBookCommand bookToCreate, CancellationToken token)
    {
        try
        {
            await _mediator.Send(bookToCreate, token);
            return Results.StatusCode(201);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Updates Book
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /Book
    ///     {
    ///         "title": "Book title",
    ///         "genreId": 1,
    ///         "formatId": 1,
    ///         "authors": [
    ///             1
    ///         ],
    ///         "publisherId": 1,
    ///         "isbn": "2222222222",
    ///         "publicationdate": "2020",
    ///         "price": 200,
    ///         "quantity": 20
    ///    }
    /// </remarks>
    [HttpPut("book")]
    [ProducesResponseType(202)]
    [Produces("application/json")]
    public async Task<IResult> Update([FromBody] UpdateBookCommand bookToUpdate, CancellationToken token)
    {
        try
        {
            await _mediator.Send(bookToUpdate, token);
            return Results.StatusCode(202);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// /// <summary>
    /// Change Quantity of books
    /// </summary>
    /// <param name="ISBN"></param>
    /// <param name="Quantity"></param>
    [HttpPut("{ISBN}")]
    [ProducesResponseType(202)]
    [Produces("application/json")]
    public async Task<IResult> DecreaseQuantity(string ISBN, int Quantity, CancellationToken token)
    {
        try
        {
            var DecreaseQuantity = new DecreaseQuantityBookCommand(ISBN, Quantity);
            await _mediator.Send(DecreaseQuantity, token);
            return Results.StatusCode(202);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}