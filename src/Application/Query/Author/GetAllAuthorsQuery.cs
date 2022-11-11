using Domain.AggregationModels.Book;
using EmptyProjectASPNETCORE;
using MediatR;

namespace TemplateASP.NET.CORE.Query;

public record GetAllAuthorsQuery() : IRequest<IEnumerable<GetAuthorResponse>>;