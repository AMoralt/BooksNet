using Application.Exception;
using Application.Commands;
using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("[controller]")]
[ApiController]
public class BookFormatController
{
    private readonly IMediator _mediator;
    public BookFormatController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Gets the list of all Book Formats
    /// </summary>
    /// <returns>The list of Book Formats</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetAll(CancellationToken token)
    {
        try
        {
            var getAll = new GetAllBookFormatsQuery();
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
    /// Gets Book Format by id
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetById(int id, CancellationToken token)
    {
        try
        {
            var getById = new GetByIdBookFormatQuery(id);
            var result = await _mediator.Send(getById, token);
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
    /// Deletes Book Format by id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [Produces("application/json")]
    public async Task<IResult> DeleteById(int id, CancellationToken token)
    {
        try
        {
            var deleteCommand = new DeleteBookFormatCommand(id);
            await _mediator.Send(deleteCommand, token);
            return Results.NoContent();
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Creates Book Format
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /BookFormat
    ///     {
    ///         "name": "Book format description",
    ///     }
    /// </remarks>
    [HttpPost("{name}")]
    [ProducesResponseType(201)]
    [Produces("application/json")]
    public async Task<IResult> Create(string name, CancellationToken token)
    {
        try
        {
            var createCommand = new CreateBookFormatCommand(name);
            await _mediator.Send(createCommand, token);
            return Results.StatusCode(201);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Updates Book Format
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /BookFormat/1
    ///     {
    ///         "id": 1,
    ///         "name": "Book format description"
    ///    }
    /// </remarks>
    [HttpPut("{id:int}/{name}")]
    [ProducesResponseType(202)]
    [Produces("application/json")]
    public async Task<IResult> Update(int id, string name, CancellationToken token)
    {
        try
        {
            var updateCommand = new UpdateBookFormatCommand(id,name);
            await _mediator.Send(updateCommand, token);
            return Results.StatusCode(202);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}