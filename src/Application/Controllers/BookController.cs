using Domain.AggregationModels.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

[Route("[controller]")]
[ApiController]
public class BookController
{
    private readonly IMediator _mediator;
    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("GetAll")]
    public async Task<IResult> GetAll(CancellationToken token)
    {
        try
        {
            var getAll = new GetAllBooksQuery();
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
    [HttpGet("{ISBN}")]
    public async Task<IResult> GetByISBN(string ISBN, CancellationToken token)
    {
        try
        {

            var getByISBN = new GetByISBNBookQuery(ISBN);
            var result = await _mediator.Send(getByISBN, token);
            if (result is null)
                return Results.NotFound();
            return Results.Ok(result);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    } 
    
    [HttpDelete("{ISBN}")]
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
    [HttpPost]
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
    [HttpPut]
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