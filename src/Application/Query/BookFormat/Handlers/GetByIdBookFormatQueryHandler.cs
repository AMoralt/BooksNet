using Application.Exception;
using Infrastructure.Contracts;
using Domain.AggregationModels.Book;
using MediatR;

namespace Application.Query;

public class GetByIdBookFormatQueryHandler : IRequestHandler<GetByIdBookFormatQuery, GetBookFormatResponse>
{
    private readonly IRepository<BookFormat> _bookFormatRepository;

    public GetByIdBookFormatQueryHandler(IRepository<BookFormat> bookFormatRepository)
    {
        _bookFormatRepository = bookFormatRepository;
    }

    public async Task<GetBookFormatResponse> Handle(GetByIdBookFormatQuery request, CancellationToken cancellationToken)
    {
        var bookFormat = await _bookFormatRepository.GetByIdAsync(request.id,cancellationToken);
        if (bookFormat is null)
            throw new NotFoundException($"There is no Book Format with id: {request.id}");
        var result = new GetBookFormatResponse(bookFormat.Id.Value, bookFormat.Name);
        return result;
    }
}

public record GetByIdBookFormatQuery(int id) : IRequest<GetBookFormatResponse>;