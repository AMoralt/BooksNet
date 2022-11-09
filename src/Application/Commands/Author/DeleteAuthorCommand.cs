using MediatR;

namespace EmptyProjectASPNETCORE;

public record DeleteAuthorCommand(int id) : IRequest;