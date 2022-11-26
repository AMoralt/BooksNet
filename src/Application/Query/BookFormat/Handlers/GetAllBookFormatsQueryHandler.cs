using Application.Exception;
using Infrastructure.Contracts;
using Domain.AggregationModels.Book;
using MediatR;

namespace Application.Query;

public class GetAllBookFormatsQueryHandler : IRequestHandler<GetAllBookFormatsQuery, IEnumerable<GetBookFormatResponse>>
{
    private readonly IRepository<BookFormat> _bookFormatRepository;

    public GetAllBookFormatsQueryHandler(IRepository<BookFormat> bookFormatRepository)
    {
        _bookFormatRepository = bookFormatRepository;
    }

    public async Task<IEnumerable<GetBookFormatResponse>> Handle(GetAllBookFormatsQuery request, CancellationToken cancellationToken)
    {
        var bookFormats= await _bookFormatRepository.GetAllAsync(cancellationToken);
        if (!bookFormats.Any())
            throw new NotFoundException($"There is no Book Formats in repository");
        
        var result = bookFormats.Select(b => new GetBookFormatResponse(b.Id.Value, b.Name));
        return result;
    }
}

public record GetAllBookFormatsQuery() : IRequest<IEnumerable<GetBookFormatResponse>>;