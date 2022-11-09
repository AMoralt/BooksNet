using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetAllPublishersQueryHandler : IRequestHandler<GetAllPublishersQuery, IEnumerable<Publisher>>
{
    private readonly IPublisherRepository _publisherRepository;

    public GetAllPublishersQueryHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<IEnumerable<Publisher>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
    {
        var result= await _publisherRepository.GetAllAsync(cancellationToken);
        return result;
    }
}