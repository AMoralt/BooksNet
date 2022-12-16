using Domain.DomainEvents;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Events;

public class IncreaseQuantityDomainEventHandler : INotificationHandler<IncreaseQuantityDomainEvent>
{
    private readonly IEventRepository _eventRepository;
    private readonly IBookRepository _bookRepository;
    public IncreaseQuantityDomainEventHandler(IBookRepository bookRepository, IEventRepository eventRepository)
    {
        _bookRepository = bookRepository;
        _eventRepository = eventRepository;
    }
    
    public async Task Handle(IncreaseQuantityDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventRepository.SaveAsync(notification);
        await _bookRepository.UpdateQuantityAsync(notification.ISBN, notification.Quantity);//TODO: insert into order table from here
    }
}