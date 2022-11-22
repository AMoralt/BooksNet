using Application.Exception;
using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetByIdPublisherQueryHandler : IRequestHandler<GetByIdPublisherQuery, GetPublisherResponse>
{
    private readonly IRepository<Publisher> _publisherRepository;

    public GetByIdPublisherQueryHandler(IRepository<Publisher> publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<GetPublisherResponse> Handle(GetByIdPublisherQuery request, CancellationToken cancellationToken)
    {
        var publisher = await _publisherRepository.GetByIdAsync(request.id,cancellationToken);
        if (publisher is null)
            throw new NotFoundException($"There is no Publisher with id: {request.id}");
        var result = new GetPublisherResponse(publisher.Id.Value, publisher.Name);
        return result;
    }
}