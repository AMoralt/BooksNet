using MediatR;

namespace EmptyProjectASPNETCORE;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _bookRepository;

    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        //await _bookRepository.UnitOfWork.StartTransaction();
        
        
        
        //var res = await _bookRepository.DeleteAsync(request);
        
        //if (aviaTicket is null) 
        //    throw new System.Exception("Ticket not found");
        
        //await _aviaTicketRepository.UnitOfWork.SaveChangesAsync();
        
        return Unit.Value;
    }
}