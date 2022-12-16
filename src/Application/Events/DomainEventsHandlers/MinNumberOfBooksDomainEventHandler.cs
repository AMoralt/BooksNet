using Domain.DomainEvents;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Events;

public class MinNumberOfBooksDomainEventHandler : INotificationHandler<MinNumberOfBooksDomainEvent>
{
    private readonly IEventRepository _eventRepository;
    private readonly IBookRepository _bookRepository;
    public MinNumberOfBooksDomainEventHandler(IEventRepository eventRepository, IBookRepository bookRepository)
    {
        _eventRepository = eventRepository;
        _bookRepository = bookRepository;
    }
    
    public async Task Handle(MinNumberOfBooksDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventRepository.SaveAsync(notification);
        await _bookRepository.UpdateQuantityAsync(notification.ISBN, 10); //TODO: 10 is a magic number
    }
}