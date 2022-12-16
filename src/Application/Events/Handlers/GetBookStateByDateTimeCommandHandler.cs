using Application.Exception;
using MediatR;
using Infrastructure.Contracts;

namespace Application.Commands;

public class GetBookStateByDateTimeCommandHandler : IRequestHandler<GetBookStateByDateTimeCommand>
{
    private readonly IEventRepository _eventRepository;
    public GetBookStateByDateTimeCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Unit> Handle(GetBookStateByDateTimeCommand request, CancellationToken cancellationToken)
    {
        var datetime = DateTime.Parse(request.date);
        await _eventRepository.GetData(request.ISBN, datetime);
        
        //if (!events.Any())
            throw new NotFoundException($"There is no data in repository");
        
        return Unit.Value;
    }
}

public record GetBookStateByDateTimeCommand(string ISBN, string date) : IRequest;