using Domain.DomainEvents;
using MediatR;

namespace Application.Events;

public class MinNumberOfBooksDomainEventHandler : INotificationHandler<MinNumberOfBooksDomainEvent>
{
    
    public MinNumberOfBooksDomainEventHandler()
    {
        
    }
    
    public async Task Handle(MinNumberOfBooksDomainEvent notification, CancellationToken cancellationToken)
    {
        //Сделать запрос к поставщикам
    }
}