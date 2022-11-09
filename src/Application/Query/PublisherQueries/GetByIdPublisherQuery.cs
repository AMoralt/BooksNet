using Domain.AggregationModels.Book;
using MediatR;

namespace TemplateASP.NET.CORE.Query;

public record GetByIdPublisherQuery(int id) : IRequest<Publisher>;