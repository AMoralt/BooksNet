using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetByIdGenreQueryHandler : IRequestHandler<GetByIdGenreQuery, GetGenreResponse>
{
    private readonly IRepository<Genre> _genreRepository;

    public GetByIdGenreQueryHandler(IRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<GetGenreResponse> Handle(GetByIdGenreQuery request, CancellationToken cancellationToken)
    {
        var genre= await _genreRepository.GetByIdAsync(request.id,cancellationToken);
        var result = new GetGenreResponse(genre.Id.Value, genre.Name);
        return result;
    }
}