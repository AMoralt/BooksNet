using Application.Exception;
using Domain.AggregationModels.Book;
using MediatR;

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
        if (author is null)
            throw new NotFoundException($"There is no author with id: {request.id}");
        var result = new GetAuthorResponse(author.Id.Value, author.LastName, author.FirstName);
        return result;
    }
}

public record GetByIdAuthorQuery(int id) : IRequest<GetAuthorResponse>;