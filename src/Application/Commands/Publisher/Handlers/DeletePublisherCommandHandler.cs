using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand>
{
    private readonly IRepository<Publisher> _publisherRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeletePublisherCommandHandler(IRepository<Publisher> publisherRepository, IUnitOfWork unitOfWork)
    {
        _publisherRepository = publisherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisherToDelete = await _publisherRepository.GetByIdAsync(request.id, cancellationToken);
        if (publisherToDelete is null)
            throw new System.Exception("Publisher not found");

        await _unitOfWork.StartTransaction(cancellationToken);
        await _publisherRepository.DeleteAsync(request.id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}

public record DeletePublisherCommand(int id) : IRequest;