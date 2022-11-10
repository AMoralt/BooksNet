using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class CreateBookFormatCommandHandler : IRequestHandler<CreateBookFormatCommand>
{
    private readonly IRepository<BookFormat> _bookFormatRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateBookFormatCommandHandler(IRepository<BookFormat> bookFormatRepository, IUnitOfWork unitOfWork)
    {
        _bookFormatRepository = bookFormatRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(CreateBookFormatCommand request, CancellationToken cancellationToken)
    {
        var bookFormatToCreate = BookFormat.Create(null, request.name);
        
        await _unitOfWork.StartTransaction(cancellationToken);
        await _bookFormatRepository.CreateAsync(bookFormatToCreate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}