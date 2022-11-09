using MediatR;

namespace EmptyProjectASPNETCORE;

public record CreateAuthorCommand(string firstname, string lastname) : IRequest;