using Application.Exception;
using MediatR;
using Infrastructure.Contracts;

namespace Application.Query;

public class GetByAuthorIdBooksQueryHandler : IRequestHandler<GetByAuthorIdBooksQuery, IEnumerable<GetBookResponse>>
{
    private readonly IBookRepository _bookRepository;

    public GetByAuthorIdBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<GetBookResponse>> Handle(GetByAuthorIdBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetByAuthorIdBooksAsync(request.id,cancellationToken);

        if (!books.Any())
            throw new NotFoundException($"No books found with author's id: {request.id}");
        
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

public record GetByAuthorIdBooksQuery(int id) : IRequest<IEnumerable<GetBookResponse>>;