using Domain.DomainEvents;
using MediatR;

namespace EmptyProjectASPNETCORE;

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