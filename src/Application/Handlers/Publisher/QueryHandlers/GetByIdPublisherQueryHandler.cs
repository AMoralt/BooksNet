﻿using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetByIdPublisherQueryHandler : IRequestHandler<GetByIdPublisherQuery, Publisher>
{
    private readonly IPublisherRepository _publisherRepository;

    public GetByIdPublisherQueryHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<Publisher> Handle(GetByIdPublisherQuery request, CancellationToken cancellationToken)
    {
        var result= await _publisherRepository.GetByIdAsync(request.id,cancellationToken);
        return result;
    }
}