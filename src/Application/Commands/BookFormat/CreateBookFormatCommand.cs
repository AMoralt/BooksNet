using MediatR;

namespace EmptyProjectASPNETCORE;

public record CreateBookFormatCommand(string name) : IRequest;