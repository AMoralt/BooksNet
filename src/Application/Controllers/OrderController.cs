using Application.Exception;
using Application.Commands;
using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("[controller]")]
[ApiController]
public class OrderController
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Order books
    /// </summary>
    [HttpPut]
    [ProducesResponseType(202)]
    [Produces("application/json")]
    public async Task<IResult> Order([FromBody] OrderBooksCommand orderBooks, CancellationToken token)
    {
        try
        {
            await _mediator.Send(orderBooks, token);
            return Results.StatusCode(202);
        }
        catch (System.Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Get forecast for orders
    /// </summary>
    /// <param name="ISBN"></param>
    [HttpGet("{ISBN}/{date}")]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetForecast(string ISBN, string date, CancellationToken token)
    {
        try
        {
            var getBookStateByDateTimeCommand = new GetBookStateByDateTimeCommand(ISBN, date);
            var result = await _mediator.Send(getBookStateByDateTimeCommand, token);
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
    /// Gets the list of all Orders
    /// </summary>
    /// <returns>The list of Orders</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [Produces("application/json")]
    public async Task<IResult> GetAll(int limit, int offset, CancellationToken token)
    {
        try
        {
            var getAll = new GetAllOrdersQuery(limit, offset);
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
}