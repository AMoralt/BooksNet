using Application.Exception;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class GetByISBNBookQueryHandler : IRequestHandler<GetByISBNBookQuery, GetBookResponse>
{
    private readonly IBookRepository _bookRepository;

    public GetByISBNBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<GetBookResponse> Handle(GetByISBNBookQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetByISBNAsync(request.ISBN,cancellationToken);

        if (books is null)
            throw new NotFoundException($"No book found with ISBN: {request.ISBN}");
        
        var result = new GetBookResponse(
            books.Title.Value,
            books.Details.ISBN,
            books.Genre.Name,
            books.Publisher.Name,
            books.Details.PublicationDate.Date,
            books.Authors.Select(a=> new AuthorResponse(a.LastName, a.FirstName)).ToList(),
            books.Format.Name,
            books.Details.Price,
            books.Details.Quantity);
        return result;
    }
}

public record GetByISBNBookQuery(string ISBN) : IRequest<GetBookResponse>;