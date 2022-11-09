using System.Globalization;
using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
         if(!DateTime.TryParseExact(request.publicationdate,"yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var publicationdate))
         {
             throw new System.Exception("Invalid date format");
         }

         var bookToCreate = new Book(
             null,
             BookDetails.Create(request.quantity, request.price, publicationdate, request.isbn),
             Title.Create(request.title), 
             Genre.Create(request.genreId,null),
             new List<Author>(request.authors.Select(authorsIds => new Author(authorsIds, null,null))),
             new Publisher(request.publisherId, null),
             new BookFormat(request.formatId, null)
         );
         await _unitOfWork.StartTransaction(cancellationToken);
         await _bookRepository.CreateAsync(bookToCreate, cancellationToken);
         await _unitOfWork.SaveChangesAsync(cancellationToken);
         return Unit.Value;
    }
}