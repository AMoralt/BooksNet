using Domain.AggregationModels.Book;
using MediatR;

namespace TemplateASP.NET.CORE.Query;

public record GetByIdAuthorQuery(int id) : IRequest<Author>;