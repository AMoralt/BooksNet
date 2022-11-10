using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetByIdBookFormatQueryHandler : IRequestHandler<GetByIdBookFormatQuery, BookFormat>
{
    private readonly IRepository<BookFormat> _bookFormatRepository;

    public GetByIdBookFormatQueryHandler(IRepository<BookFormat> bookFormatRepository)
    {
        _bookFormatRepository = bookFormatRepository;
    }

    public async Task<BookFormat> Handle(GetByIdBookFormatQuery request, CancellationToken cancellationToken)
    {
        var result= await _bookFormatRepository.GetByIdAsync(request.id,cancellationToken);
        return result;
    }
}