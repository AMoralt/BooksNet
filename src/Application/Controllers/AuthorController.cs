using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

[Route("[controller]")]
[ApiController]
public class AuthorController
{
    private readonly IMediator _mediator;
    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Gets the list of all Authors
    /// </summary>
    /// <returns>The list of Authors</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IResult> GetAll(CancellationToken token)
    {
        try
        {
            var getAll = new GetAllAuthorsQuery();
            var result = await _mediator.Send(getAll, token);
            if (result is null)
                return Results.NotFound();
            return Results.Ok(result);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Gets Author by Id
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IResult> GetById(int id, CancellationToken token)
    {
        try
        {
            var getById = new GetByIdAuthorQuery(id);
            var result = await _mediator.Send(getById, token);
            if (result is null)
                return Results.NotFound();
            return Results.Ok(result);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    } 
    
    /// <summary>
    /// Deletes Author by Id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IResult> DeleteById(int id, CancellationToken token)
    {
        try
        {
            var deleteCommand = new DeleteAuthorCommand(id);
            await _mediator.Send(deleteCommand, token);
            return Results.NoContent();
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Creates Author
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Author
    ///     {
    ///       "firstName": "Mike",
    ///       "lastName": "Andrew",       
    ///     }
    /// </remarks>
    [HttpPost("{firstName}/{lastName}")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IResult> Create(string firstName,string lastName, CancellationToken token)
    {
        try
        {
            var createCommand = new CreateAuthorCommand(firstName,lastName);
            await _mediator.Send(createCommand, token);
            return Results.StatusCode(201);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    /// <summary>
    /// Updates Author
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /Author
    ///     {
    ///       "id": "1"
    ///       "firstName": "Mike",
    ///       "lastName": "Andrew",       
    ///     }
    /// </remarks>
    [HttpPut]
    [ProducesResponseType(202)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IResult> Update([FromBody] UpdateAuthorCommand authorToUpdate, CancellationToken token)
    {
        try
        {
            await _mediator.Send(authorToUpdate, token);
            return Results.StatusCode(202);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}