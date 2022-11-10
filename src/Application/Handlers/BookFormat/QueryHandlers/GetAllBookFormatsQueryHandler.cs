using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetAllBookFormatsQueryHandler : IRequestHandler<GetAllBookFormatsQuery, IEnumerable<BookFormat>>
{
    private readonly IRepository<BookFormat> _bookFormatRepository;

    public GetAllBookFormatsQueryHandler(IRepository<BookFormat> bookFormatRepository)
    {
        _bookFormatRepository = bookFormatRepository;
    }

    public async Task<IEnumerable<BookFormat>> Handle(GetAllBookFormatsQuery request, CancellationToken cancellationToken)
    {
        var result= await _bookFormatRepository.GetAllAsync(cancellationToken);
        return result;
    }
}