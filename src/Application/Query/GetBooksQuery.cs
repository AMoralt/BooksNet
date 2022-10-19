using Domain.AggregationModels.Book;
using MediatR;

namespace TemplateASP.NET.CORE.Query;

public record GetBooksQuery() : IRequest<IQueryable<Book>>;