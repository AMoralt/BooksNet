using Application.Exception;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectASPNETCORE;

[Route("[controller]")]
[ApiController]
public class GenreController
{
    private readonly IMediator _mediator;
    public GenreController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Gets the list of all Genres
    /// </summary>
    /// <returns>The list of Genres</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetAll(CancellationToken token)
    {
        try
        {
            var getAll = new GetAllGenresQuery();
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
    /// Gets Genre by id
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetById(int id, CancellationToken token)
    {
        try
        {
            var getById = new GetByIdGenreQuery(id);
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
    /// Deletes Genre by id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [Produces("application/json")]
    public async Task<IResult> DeleteById(int id, CancellationToken token)
    {
        try
        {
            var deleteCommand = new DeleteGenreCommand(id);
            await _mediator.Send(deleteCommand, token);
            return Results.NoContent();
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Creates Genre
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Genre
    ///     {
    ///         "name": "Genre description",
    ///     }
    /// </remarks>
    [HttpPost("{name}")]
    [ProducesResponseType(201)]
    [Produces("application/json")]
    public async Task<IResult> Create(string name, CancellationToken token)
    {
        try
        {
            var createCommand = new CreateGenreCommand(name);
            await _mediator.Send(createCommand, token);
            return Results.StatusCode(201);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Updates Genre
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /Genre/1
    ///     {
    ///         "id": 1,
    ///         "name": "Genre description"
    ///    }
    /// </remarks>
    [HttpPut("{id:int}/{name}")]
    [ProducesResponseType(202)]
    [Produces("application/json")]
    public async Task<IResult> Update(int id, string name, CancellationToken token)
    {
        try
        {
            var updateCommand = new UpdateGenreCommand(id,name);
            await _mediator.Send(updateCommand, token);
            return Results.StatusCode(202);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}