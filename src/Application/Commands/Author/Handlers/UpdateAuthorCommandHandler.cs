using Domain.AggregationModels.Book;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Commands;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateAuthorCommandHandler(IRepository<Author> authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorExist = await _authorRepository.GetByIdAsync(request.id,cancellationToken);
        if (authorExist is null)
            throw new System.Exception("Author not found");

        var bookFormatToUpdate = Author.Create(
            request.id, 
            request.lastname ?? authorExist.LastName,
            request.firstname ?? authorExist.FirstName);

        await _unitOfWork.StartTransaction(cancellationToken);
        await _authorRepository.UpdateAsync(bookFormatToUpdate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public record UpdateAuthorCommand(int id, string? firstname, string? lastname) : IRequest;