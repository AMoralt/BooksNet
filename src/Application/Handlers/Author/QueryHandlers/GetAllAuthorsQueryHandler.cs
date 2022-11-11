using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<GetAuthorResponse>>
{
    private readonly IRepository<Author> _authorRepository;

    public GetAllAuthorsQueryHandler(IRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<GetAuthorResponse>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors= await _authorRepository.GetAllAsync(cancellationToken);
        var result = authors.Select(a => new GetAuthorResponse(a.Id.Value, a.LastName, a.FirstName));
        return result;
    }
}