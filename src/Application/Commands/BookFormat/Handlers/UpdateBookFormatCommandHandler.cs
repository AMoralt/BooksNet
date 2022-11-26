using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class UpdateBookFormatCommandHandler : IRequestHandler<UpdateBookFormatCommand>
{
    private readonly IRepository<BookFormat> _bookFormatRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateBookFormatCommandHandler(IRepository<BookFormat> bookFormatRepository, IUnitOfWork unitOfWork)
    {
        _bookFormatRepository = bookFormatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateBookFormatCommand request, CancellationToken cancellationToken)
    {
        var bookFormatExist = await _bookFormatRepository.GetByIdAsync(request.id,cancellationToken);
        if (bookFormatExist is null)
            throw new System.Exception("BookFormat not found");

        var bookFormatToUpdate = BookFormat.Create(request.id, request.name);

        await _unitOfWork.StartTransaction(cancellationToken);
        await _bookFormatRepository.UpdateAsync(bookFormatToUpdate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public record UpdateBookFormatCommand(int id, string name) : IRequest;