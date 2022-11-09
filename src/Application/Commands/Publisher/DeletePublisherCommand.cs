using MediatR;

namespace EmptyProjectASPNETCORE;

public record DeletePublisherCommand(int id) : IRequest;