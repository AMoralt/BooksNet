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
    
    [HttpGet("GetAll")]
    public async Task<IResult> GetAll(CancellationToken token)
    {
        try
        {
            var getAll = new GetAllPublishersQuery();
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
            var getById = new GetByIdPublisherQuery(id);
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
            var deleteCommand = new DeletePublisherCommand(id);
            await _mediator.Send(deleteCommand, token);
            return Results.NoContent();
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    [HttpPost("{name}")]
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
    [HttpPut("{id:int}/{name}")]
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