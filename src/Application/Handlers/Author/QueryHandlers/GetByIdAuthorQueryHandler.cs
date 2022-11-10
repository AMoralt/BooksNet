using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetByIdAuthorQueryHandler : IRequestHandler<GetByIdAuthorQuery, Author>
{
    private readonly IRepository<Author> _authorRepository;

    public GetByIdAuthorQueryHandler(IRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<Author> Handle(GetByIdAuthorQuery request, CancellationToken cancellationToken)
    {
        var result= await _authorRepository.GetByIdAsync(request.id,cancellationToken);
        return result;
    }
}