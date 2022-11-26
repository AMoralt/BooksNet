using Application.Exception;
using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class GetAllPublishersQueryHandler : IRequestHandler<GetAllPublishersQuery, IEnumerable<GetPublisherResponse>>
{
    private readonly IRepository<Publisher> _publisherRepository;

    public GetAllPublishersQueryHandler(IRepository<Publisher> publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<IEnumerable<GetPublisherResponse>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
    {
        var publishers= await _publisherRepository.GetAllAsync(cancellationToken);
        if (!publishers.Any())
            throw new NotFoundException($"There is no Publishers in repository");
        var result = publishers.Select(p => new GetPublisherResponse(p.Id.Value, p.Name));
        return result;
    }
}

public record GetAllPublishersQuery() : IRequest<IEnumerable<GetPublisherResponse>>;