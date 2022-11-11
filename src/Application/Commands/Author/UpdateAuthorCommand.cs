using MediatR;

namespace EmptyProjectASPNETCORE;

public record UpdateAuthorCommand(int id, string? firstname, string? lastname) : IRequest;