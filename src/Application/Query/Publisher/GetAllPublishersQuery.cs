using Domain.AggregationModels.Book;
using EmptyProjectASPNETCORE;
using MediatR;

namespace TemplateASP.NET.CORE.Query;

public record GetAllPublishersQuery() : IRequest<IEnumerable<GetPublisherResponse>>;