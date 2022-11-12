﻿using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<GetGenreResponse>>
{
    private readonly IRepository<Genre> _genreRepository;

    public GetAllGenresQueryHandler(IRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<GetGenreResponse>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var genres= await _genreRepository.GetAllAsync(cancellationToken);
        var result = genres.Select(g => new GetGenreResponse(g.Id.Value, g.Name));
        return result;
    }
}