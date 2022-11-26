using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateAuthorCommandHandler(IRepository<Author> authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorToCreate = Author.Create(null, request.lastname, request.firstname);
        
        await _unitOfWork.StartTransaction(cancellationToken);
        await _authorRepository.CreateAsync(authorToCreate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public record CreateAuthorCommand(string firstname, string lastname) : IRequest;