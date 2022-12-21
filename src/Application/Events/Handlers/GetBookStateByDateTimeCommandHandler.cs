using MediatR;
using Infrastructure.Contracts;
using Infrastructure.Repository;

namespace Application.Commands;

public class GetBookStateByDateTimeCommandHandler : IRequestHandler<GetBookStateByDateTimeCommand,List<ForecastOut>> 
{
    private readonly IEventRepository _eventRepository;
    public GetBookStateByDateTimeCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<List<ForecastOut>> Handle(GetBookStateByDateTimeCommand request, CancellationToken cancellationToken)
    {
        var datetime = DateTime.Parse(request.date);
        var result = await _eventRepository.GetData(request.ISBN, datetime);
        
        //if (!events.Any())
            //throw new NotFoundException($"There is no data in repository");
            
        return result;
    }
}

public record GetBookStateByDateTimeCommand(string ISBN, string date) : IRequest<List<ForecastOut>>;