using Application.Exception;
using Domain.AggregationModels.Order;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Query;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<GetOrderResponse>>
{
    private readonly IOrderRepository _orderRepository;
    public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<GetOrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders= await _orderRepository.GetAllAsync(request.limit, request.offset, cancellationToken);
        
        if (!orders.Any())
            throw new NotFoundException($"There is no Orders in repository");

        var result = orders.Select(x =>
            new GetOrderResponse((int)x.Id, x.DateTime.Value,
                x.OrderItems.Select(y =>
                {
                    return
                        new OrderItemResponse(y.Quantity, y.Price, new GetBookResponse(
                            y.Book.Title.Value,
                            y.Book.Details.ISBN,
                            y.Book.Genre.Name,
                            y.Book.Publisher.Name,
                            y.Book.Details.PublicationDate.Date,
                            y.Book.Authors.Select(a => new AuthorResponse(a.LastName, a.FirstName)).ToList(),
                            y.Book.Format.Name,
                            y.Book.Details.Price,
                            y.Book.Details.Quantity));
                }).ToList()));

        return result;
    }
}
public record GetAllOrdersQuery(int limit, int offset) : IRequest<IEnumerable<GetOrderResponse>>;