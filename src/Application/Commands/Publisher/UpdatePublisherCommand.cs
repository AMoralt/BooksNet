using MediatR;

namespace EmptyProjectASPNETCORE;

public record UpdatePublisherCommand(int id, string name) : IRequest;