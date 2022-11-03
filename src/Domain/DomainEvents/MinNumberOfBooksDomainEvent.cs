using Domain.AggregationModels.Book;
using EmptyProjectASPNETCORE;
using MediatR;

namespace Domain.DomainEvents;

public class MinNumberOfBooksDomainEvent : INotification
{
    public string ISBN { get; }
    public MinNumberOfBooksDomainEvent(string isbn)
    {
        ISBN = isbn;
    }
}