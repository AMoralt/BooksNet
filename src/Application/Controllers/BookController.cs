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
    
    [HttpGet]
    public async Task<IResult> GetAll(CancellationToken token)
    {
        var getBooks = new GetBooksQuery();
        
        var result = await _mediator.Send(getBooks, token);
        return Results.Ok(result);
    } 
    
    [HttpDelete("{isbn}")]
    public async Task<IResult> DeleteBookByISBN(string isbn, CancellationToken token)
    {
        var deleteBook = new DeleteBookCommand(isbn);
        
        var result = await _mediator.Send(deleteBook, token);
        return Results.Ok(result);
    }
}