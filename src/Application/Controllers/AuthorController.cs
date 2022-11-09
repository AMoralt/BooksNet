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
    
    [HttpGet("GetAll")]
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
    [HttpGet("{id:int}")]
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
    
    [HttpDelete("{id:int}")]
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
    [HttpPost("{first:max(50)}/{last:max(50)}")]
    public async Task<IResult> Create(string first,string last, CancellationToken token)
    {
        try
        {
            var createCommand = new CreateAuthorCommand(first,last);
            await _mediator.Send(createCommand, token);
            return Results.StatusCode(201);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    [HttpPut]
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