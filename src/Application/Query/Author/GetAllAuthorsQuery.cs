using Domain.AggregationModels.Book;
using MediatR;

namespace TemplateASP.NET.CORE.Query;

public record GetAllAuthorsQuery() : IRequest<IEnumerable<Author>>;