using Domain.AggregationModels.Book;
using EmptyProjectASPNETCORE;
using MediatR;

namespace Domain.DomainEvents;

public class MinNumberOfBooksDomainEvent : INotification
{
    public ISBN ISBN { get; }
    public MinNumberOfBooksDomainEvent(ISBN isbn)
    {
        ISBN = isbn;
    }
}