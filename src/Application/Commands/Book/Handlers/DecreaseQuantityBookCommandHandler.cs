using Infrastructure.Contracts;
using MediatR;

namespace Application.Commands;

public class DecreaseQuantityBookCommandHandler : IRequestHandler<DecreaseQuantityBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DecreaseQuantityBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DecreaseQuantityBookCommand request, CancellationToken cancellationToken)
    {
        var bookExist = await _bookRepository.GetByISBNAsync(request.isbn,cancellationToken);
        if (bookExist is null)
            throw new System.Exception("Book not found");

        await _unitOfWork.StartTransaction(cancellationToken);
        bookExist.DecreaseQuantity(request.quantity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public record DecreaseQuantityBookCommand(
    string isbn,
    int quantity) : IRequest;