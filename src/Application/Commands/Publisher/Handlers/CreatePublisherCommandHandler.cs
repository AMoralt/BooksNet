using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand>
{
    private readonly IRepository<Publisher> _publisherRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreatePublisherCommandHandler(IRepository<Publisher> publisherRepository, IUnitOfWork unitOfWork)
    {
        _publisherRepository = publisherRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisherToCreate = Publisher.Create(null, request.name);
        
        await _unitOfWork.StartTransaction(cancellationToken);
        await _publisherRepository.CreateAsync(publisherToCreate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public record CreatePublisherCommand(string name) : IRequest;