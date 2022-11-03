using Domain.AggregationModels.Book;
using MediatR;
using TemplateASP.NET.CORE.Query;

namespace EmptyProjectASPNETCORE;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IQueryable<Book>>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IQueryable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var result= await _bookRepository.GetAllAsync();
        return result;
    }
}