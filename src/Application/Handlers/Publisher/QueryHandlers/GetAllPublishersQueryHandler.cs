using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetAllPublishersQueryHandler : IRequestHandler<GetAllPublishersQuery, IEnumerable<Publisher>>
{
    private readonly IRepository<Publisher> _publisherRepository;

    public GetAllPublishersQueryHandler(IRepository<Publisher> publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<IEnumerable<Publisher>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
    {
        var result= await _publisherRepository.GetAllAsync(cancellationToken);
        return result;
    }
}