using Domain.Root;
using MediatR;

namespace Domain.DomainEvents;

public class DecreaseQuantityDomainEvent : INotification, IEvent
{
    public string Name => GetType().Name;
    public string ISBN { get; }
    public int Quantity { get; }
    public int Price { get; }
    public DecreaseQuantityDomainEvent(string isbn, int quantity, int price)
    {
        ISBN = isbn;
        Quantity = quantity;
        Price = price;
    }
}