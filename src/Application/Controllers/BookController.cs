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
        return Results.Ok(result);
    }
    [HttpGet("{ISBN}")]
    public async Task<IResult> GetByISBN(string ISBN, CancellationToken token)
    {
        var getByISBN = new GetByISBNBookQuery(ISBN);
        
        var result = await _mediator.Send(getByISBN, token);
        return Results.Ok(result);
    } 
    
    [HttpDelete("{ISBN}")]
    public async Task<IResult> DeleteByISBN(string ISBN, CancellationToken token)
    {
        var deleteBook = new DeleteBookCommand(ISBN);
        
        var result = await _mediator.Send(deleteBook, token);
        return Results.Ok(result);
    }
}