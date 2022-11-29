using Application.Exception;
using MediatR;
using Infrastructure.Contracts;

namespace Application.Query;

public class GetByTitleBooksQueryHandler : IRequestHandler<GetByTitleBooksQuery, IEnumerable<GetBookResponse>>
{
    private readonly IBookRepository _bookRepository;

    public GetByTitleBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<GetBookResponse>> Handle(GetByTitleBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetByTitleBooksAsync(request.title,cancellationToken);

        if (!books.Any())
            throw new NotFoundException($"No books found with title: {request.title}");
        
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

public record GetByTitleBooksQuery(string title) :  IRequest<IEnumerable<GetBookResponse>>;