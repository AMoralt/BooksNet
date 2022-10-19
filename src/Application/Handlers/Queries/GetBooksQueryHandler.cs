using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IQueryable<Book>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IQueryable<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var result= await _bookRepository.GetAllAsync();
        return result;
    }
}