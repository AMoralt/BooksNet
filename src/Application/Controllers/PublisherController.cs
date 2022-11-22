using Application.Exception;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

[Route("[controller]")]
[ApiController]
public class PublisherController
{
    private readonly IMediator _mediator;
    public PublisherController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Gets the list of all Publishers
    /// </summary>
    /// <returns>The list of Publishers</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetAll(CancellationToken token)
    {
        try
        {
            var getAll = new GetAllPublishersQuery();
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
    /// Gets Publisher by id
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetById(int id, CancellationToken token)
    {
        try
        {
            var getById = new GetByIdPublisherQuery(id);
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
    /// Deletes Publisher by id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [Produces("application/json")]
    public async Task<IResult> DeleteById(int id, CancellationToken token)
    {
        try
        {
            var deleteCommand = new DeletePublisherCommand(id);
            await _mediator.Send(deleteCommand, token);
            return Results.NoContent();
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Creates Publisher
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Publisher
    ///     {
    ///         "name": "Publisher name",
    ///     }
    /// </remarks>
    [HttpPost("{name}")]
    [ProducesResponseType(201)]
    [Produces("application/json")]
    public async Task<IResult> Create(string name, CancellationToken token)
    {
        try
        {
            var createCommand = new CreatePublisherCommand(name);
            await _mediator.Send(createCommand, token);
            return Results.StatusCode(201);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Updates Publisher
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /Publisher/1
    ///     {
    ///         "id": 1,
    ///         "name": "Publisher name"
    ///    }
    /// </remarks>
    [HttpPut("{id:int}/{name}")]
    [ProducesResponseType(202)]
    [Produces("application/json")]
    public async Task<IResult> Update(int id, string name, CancellationToken token)
    {
        try
        {
            var updateCommand = new UpdatePublisherCommand(id,name);
            await _mediator.Send(updateCommand, token);
            return Results.StatusCode(202);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}