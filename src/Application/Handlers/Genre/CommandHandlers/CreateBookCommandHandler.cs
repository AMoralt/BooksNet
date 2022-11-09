using Domain.AggregationModels.Book;
using MediatR;

namespace EmptyProjectASPNETCORE;

public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateGenreCommandHandler(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
    {
        _genreRepository = genreRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genreToCreate = Genre.Create(null, request.name);
        
        await _unitOfWork.StartTransaction(cancellationToken);
        await _genreRepository.CreateAsync(genreToCreate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}