using Domain.AggregationModels.Book;
using MediatR;

namespace TemplateASP.NET.CORE.Query;

public record GetByISBNBookQuery(string ISBN) : IRequest<Book>;