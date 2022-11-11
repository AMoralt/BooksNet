using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetByIdAuthorQueryHandler : IRequestHandler<GetByIdAuthorQuery, GetAuthorResponse>
{
    private readonly IRepository<Author> _authorRepository;

    public GetByIdAuthorQueryHandler(IRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<GetAuthorResponse> Handle(GetByIdAuthorQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.id,cancellationToken);
        var result = new GetAuthorResponse(author.Id.Value, author.LastName, author.FirstName);
        return result;
    }
}