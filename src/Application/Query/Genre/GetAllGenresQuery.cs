using Domain.AggregationModels.Book;
using MediatR;

namespace TemplateASP.NET.CORE.Query;

public record GetAllGenresQuery() : IRequest<IEnumerable<Genre>>;