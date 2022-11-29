using Application.Exception;
using MediatR;
using Infrastructure.Contracts;

namespace Application.Query;

public class GetByGenreIdBookQueryHandler : IRequestHandler<GetByGenreIdBooksQuery, IEnumerable<GetBookResponse>>
{
    private readonly IBookRepository _bookRepository;

    public GetByGenreIdBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<GetBookResponse>> Handle(GetByGenreIdBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetByGenreIdBooksAsync(request.id,cancellationToken);

        if (!books.Any())
            throw new NotFoundException($"No books found with genre's id: {request.id}");
        
        var result = books.Select(x =>
            new GetBookResponse(
                x.Title.Value,
                x.Details.ISBN,
                x.Genre.Name,
                x.Publisher.Name,
                x.Details.PublicationDate.Date,
                x.Authors.Select(a => new AuthorResponse(a.LastName, a.FirstName)).ToList(),
                x.Format.Name,
                x.Details.Price,
                x.Details.Quantity));
        return result;
    }
}

public record GetByGenreIdBooksQuery(int id) : IRequest<IEnumerable<GetBookResponse>>;