using MediatR;

namespace EmptyProjectASPNETCORE;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteGenreCommandHandler(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
    {
        _genreRepository = genreRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genreToDelete = await _genreRepository.GetByIdAsync(request.id, cancellationToken);
        if (genreToDelete is null)
            throw new System.Exception("Genre not found");

        await _unitOfWork.StartTransaction(cancellationToken);
        await _genreRepository.DeleteAsync(request.id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}