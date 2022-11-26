using Application.Exception;
using Domain.AggregationModels.Book;
using MediatR;

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
        var genre = await _genreRepository.GetByIdAsync(request.id,cancellationToken);
        if (genre is null)
            throw new NotFoundException($"There is no Genre with id: {request.id}");
        var result = new GetGenreResponse(genre.Id.Value, genre.Name);
        return result;
    }
}

public record GetByIdGenreQuery(int id) : IRequest<GetGenreResponse>;