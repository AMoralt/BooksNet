using MediatR;

namespace EmptyProjectASPNETCORE;

public record CreatePublisherCommand(string name) : IRequest;