using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetByISBNBookQueryHandler : IRequestHandler<GetByISBNBookQuery, Book>
{
    private readonly IBookRepository _bookRepository;

    public GetByISBNBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book> Handle(GetByISBNBookQuery request, CancellationToken cancellationToken)
    {
        var result= await _bookRepository.GetByISBNAsync(request.ISBN,cancellationToken);
        return result;
    }
}