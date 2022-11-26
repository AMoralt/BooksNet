using Domain.AggregationModels.Book;
using MediatR;
using Infrastructure.Contracts;

namespace Application.Commands;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
{
    private readonly IRepository<Genre> _genreRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateGenreCommandHandler(IRepository<Genre> genreRepository, IUnitOfWork unitOfWork)
    {
        _genreRepository = genreRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genreExist = await _genreRepository.GetByIdAsync(request.id,cancellationToken);
        if (genreExist is null)
            throw new System.Exception("Genre not found");

        var genreToUpdate = Genre.Create(request.id, request.name);

        await _unitOfWork.StartTransaction(cancellationToken);
        await _genreRepository.UpdateAsync(genreToUpdate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public record UpdateGenreCommand(int id, string name) : IRequest;