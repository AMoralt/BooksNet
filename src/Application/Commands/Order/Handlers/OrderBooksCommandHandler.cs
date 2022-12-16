using System.Globalization;
using Domain.AggregationModels.Book;
using Domain.AggregationModels.Order;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Commands;

public class OrderBooksCommandHandler : IRequestHandler<OrderBooksCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    public OrderBooksCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork, IOrderRepository orderRepository)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(OrderBooksCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.StartTransaction(cancellationToken);
        var list = new List<OrderItem>();
        var time = new PurchaseDate(DateTime.Now);
        foreach (var e in request.Orders)
        {
            var bookExist = await _bookRepository.GetByISBNAsync(e.ISBN,cancellationToken);
            if (bookExist is null)
                throw new System.Exception("Book not found");

            bookExist.DecreaseQuantity(e.Quantity);
            var item = new OrderItem(null, e.Quantity, bookExist.Details.Price,bookExist);
            list.Add(item);
        }
        var order = new Order(null, time, list);
        await _orderRepository.CreateAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public record OrderBooksCommand(
    IEnumerable<Items> Orders) : IRequest;
    
public record Items(string ISBN, int Quantity);