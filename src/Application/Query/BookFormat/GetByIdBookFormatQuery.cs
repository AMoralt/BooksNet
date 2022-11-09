using Domain.AggregationModels.Book;
using MediatR;

namespace TemplateASP.NET.CORE.Query;

public record GetByIdBookFormatQuery(int id) : IRequest<BookFormat>;