using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

[Route("[controller]")]
[ApiController]
public class BookFormatController
{
    private readonly IMediator _mediator;
    public BookFormatController(IMediator mediator)
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
            var deleteGenreCommand = new DeleteGenreCommand(id);
            await _mediator.Send(deleteGenreCommand, token);
            return Results.NoContent();
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    [HttpPost("{name:max(50)}")]
    public async Task<IResult> Create(string name, CancellationToken token)
    {
        try
        {
            var createGenreCommand = new CreateGenreCommand(name);
            await _mediator.Send(createGenreCommand, token);
            return Results.StatusCode(201);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    [HttpPut("{id:int}/{name:max(50)}")]
    public async Task<IResult> Update(int id, string name, CancellationToken token)
    {
        try
        {
            var updateGenreCommand = new UpdateGenreCommand(id,name);
            await _mediator.Send(updateGenreCommand, token);
            return Results.StatusCode(202);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}