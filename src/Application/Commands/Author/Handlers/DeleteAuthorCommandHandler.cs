using Domain.AggregationModels.Book;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Commands;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteAuthorCommandHandler(IRepository<Author> authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorToDelete = await _authorRepository.GetByIdAsync(request.id, cancellationToken);
        if (authorToDelete is null)
            throw new System.Exception("Author not found");

        await _unitOfWork.StartTransaction(cancellationToken);
        await _authorRepository.DeleteAsync(request.id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}

public record DeleteAuthorCommand(int id) : IRequest;