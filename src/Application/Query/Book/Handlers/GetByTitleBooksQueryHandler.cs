using Application.Exception;
using MediatR;
using Infrastructure.Contracts;

namespace Application.Query;

public class GetByFiltersBooksQueryHandler : IRequestHandler<GetByFiltersBooksQuery, IEnumerable<GetBookResponse>>
{
    private readonly IBookRepository _bookRepository;

    public GetByFiltersBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<GetBookResponse>> Handle(GetByFiltersBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetByFiltersBooksAsync(request.Filter, cancellationToken);

        if (!books.Any())
            throw new NotFoundException($"No books found with that filters");
        
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

public record GetByFiltersBooksQuery(string Filter) :  IRequest<IEnumerable<GetBookResponse>>;