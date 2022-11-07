using System.Globalization;
using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var bookExist = await _bookRepository.GetByISBNAsync(request.isbn,cancellationToken);
        if (bookExist is null)
            throw new System.Exception("Book not found");
        
        await _unitOfWork.StartTransaction(cancellationToken);
        if(!DateTime.TryParseExact(request.publicationdate,"yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            if (request.publicationdate is null)
                date = bookExist.Details.PublicationDate;
            else
                throw new System.Exception("Invalid date format");
        }

        var newBook = new Book(
            bookExist.Id,
            BookDetails.Create(
                request.quantity ?? bookExist.Details.Quantity,
                request.price ?? bookExist.Details.Price,
                date,
                request.isbn
            ),
            Title.Create(
                request.title ?? bookExist.Title.Value
            ),
            new Genre(request.genreId ?? (int)bookExist.Genre.Id, null),
            new List<Author>(request.authors.Select(authorsIds => new Author(authorsIds, null, null)))
            ?? bookExist.Authors,
            new Publisher(request.publisherId ?? (int)bookExist.Publisher.Id, null),
            new BookFormat(request.formatId ?? (int)bookExist.Format.Id, null));

         
         await _bookRepository.UpdateAsync(newBook, cancellationToken);
         await _unitOfWork.SaveChangesAsync(cancellationToken);
         return Unit.Value;
    }
}