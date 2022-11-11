using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateASP.NET.CORE.Query;

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
    
    [HttpGet("GetAll")]
    public async Task<IResult> GetAll(CancellationToken token)
    {
        try
        {
            var getAll = new GetAllGenresQuery();
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
            var getById = new GetByIdGenreQuery(id);
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
            var deleteCommand = new DeleteGenreCommand(id);
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
            var createCommand = new CreateGenreCommand(name);
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