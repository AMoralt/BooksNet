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
    public async Task<IResult> GetAll(CancellationToken token)
    {
        try
        {
            var getAll = new GetAllBooksQuery();
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
    /// Gets Books by title
    /// </summary>
    /// <param name="title"></param>
    [HttpGet("title/{title}")]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetByTitle(string title, CancellationToken token)
    {
        try
        {
            var getByTitle = new GetByTitleBooksQuery(title);
            var result = await _mediator.Send(getByTitle, token);
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
    [HttpPut]
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
}