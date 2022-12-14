using Domain.AggregationModels.Book;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Commands;

public class DeleteBookFormatCommandHandler : IRequestHandler<DeleteBookFormatCommand>
{
    private readonly IRepository<BookFormat> _bookFormatRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteBookFormatCommandHandler(IRepository<BookFormat> bookFormatRepository, IUnitOfWork unitOfWork)
    {
        _bookFormatRepository = bookFormatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteBookFormatCommand request, CancellationToken cancellationToken)
    {
        var bookFormatToDelete = await _bookFormatRepository.GetByIdAsync(request.id, cancellationToken);
        if (bookFormatToDelete is null)
            throw new System.Exception("BookFormat not found");

        await _unitOfWork.StartTransaction(cancellationToken);
        await _bookFormatRepository.DeleteAsync(request.id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}

public record DeleteBookFormatCommand(int id) : IRequest;