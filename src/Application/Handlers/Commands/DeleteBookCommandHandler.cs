using MediatR;

namespace EmptyProjectASPNETCORE;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.StartTransaction(cancellationToken);
        await _bookRepository.DeleteAsync(request.ISBN, cancellationToken);
        _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}