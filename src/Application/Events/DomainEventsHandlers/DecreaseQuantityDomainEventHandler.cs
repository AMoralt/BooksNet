using Domain.DomainEvents;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Events;

public class DecreaseQuantityDomainEventHandler : INotificationHandler<DecreaseQuantityDomainEvent>
{
    private readonly IEventRepository _eventRepository;
    private readonly IBookRepository _bookRepository;
    public DecreaseQuantityDomainEventHandler(IEventRepository eventRepository, IBookRepository bookRepository)
    {
        _eventRepository = eventRepository;
        _bookRepository = bookRepository;
    }
    
    public async Task Handle(DecreaseQuantityDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventRepository.SaveAsync(notification);
        await _bookRepository.UpdateQuantityAsync(notification.ISBN, -notification.Quantity); // Decrease quantity, WATCHOUT MINUS
    }
}