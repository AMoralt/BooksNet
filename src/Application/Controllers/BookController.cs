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
        var getAll = new GetAllBooksQuery();
        
        var result = await _mediator.Send(getAll, token);
        
        if(result is null)
            return Results.NotFound();
        
        return Results.Ok(result);
    }
    [HttpGet("{ISBN}")]
    public async Task<IResult> GetByISBN(string ISBN, CancellationToken token)
    {
        var getByISBN = new GetByISBNBookQuery(ISBN);
        
        var result = await _mediator.Send(getByISBN, token);
        
        if(result is null)
            return Results.NotFound();
        
        return Results.Ok(result);
    } 
    
    [HttpDelete("{ISBN}")]
    public async Task<IResult> DeleteByISBN(string ISBN, CancellationToken token)
    {
        try
        {
            var getByISBN = new GetByISBNBookQuery(ISBN);
            var bookExist = await _mediator.Send(getByISBN, token);
            if (bookExist is null)
                return Results.NotFound();
            
            var deleteBook = new DeleteBookCommand(ISBN);

            await _mediator.Send(deleteBook, token);
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
        var book = await _mediator.Send(bookToCreate, token);
        return Results.Created("...",book);
    }
}