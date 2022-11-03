using Domain.AggregationModels.Book;
using MediatR;

namespace TemplateASP.NET.CORE.Query;

public record GetAllBooksQuery() : IRequest<IQueryable<Book>>;