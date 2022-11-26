using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand>
{
    private readonly IRepository<Publisher> _publisherRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdatePublisherCommandHandler(IRepository<Publisher> publisherRepository, IUnitOfWork unitOfWork)
    {
        _publisherRepository = publisherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisherExist = await _publisherRepository.GetByIdAsync(request.id,cancellationToken);
        if (publisherExist is null)
            throw new System.Exception("Publisher not found");

        var publisherToUpdate = Publisher.Create(request.id, request.name);

        await _unitOfWork.StartTransaction(cancellationToken);
        await _publisherRepository.UpdateAsync(publisherToUpdate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public record UpdatePublisherCommand(int id, string name) : IRequest;