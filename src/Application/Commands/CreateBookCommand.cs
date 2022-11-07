using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public record CreateBookCommand(
    string title,
    int genreId,
    int formatId,
    IEnumerable<int> authors,
    int publisherId,
    string isbn,
    string publicationdate,
    int price,
    int quantity) : IRequest;