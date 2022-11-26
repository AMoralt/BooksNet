using Application.Exception;
using Infrastructure.Contracts;
using Domain.AggregationModels.Book;
using MediatR;

namespace Application.Query;

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
        if (!authors.Any())
            throw new NotFoundException($"There is no Authors in repository");
        var result = authors.Select(a => new GetAuthorResponse(a.Id.Value, a.LastName, a.FirstName));
        return result;
    }
}

public record GetAllAuthorsQuery() : IRequest<IEnumerable<GetAuthorResponse>>;