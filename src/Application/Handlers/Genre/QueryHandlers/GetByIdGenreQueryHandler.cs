using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetByIdGenreQueryHandler : IRequestHandler<GetByIdGenreQuery, Genre>
{
    private readonly IRepository<Genre> _genreRepository;

    public GetByIdGenreQueryHandler(IRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<Genre> Handle(GetByIdGenreQuery request, CancellationToken cancellationToken)
    {
        var result= await _genreRepository.GetByIdAsync(request.id,cancellationToken);
        return result;
    }
}