using Application.Exception;
using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<GetBookResponse>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<GetBookResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books= await _bookRepository.GetAllAsync(cancellationToken);
        
        if (!books.Any())
            throw new NotFoundException($"There is no Books in repository");
        
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