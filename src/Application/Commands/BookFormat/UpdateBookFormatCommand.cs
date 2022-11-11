using MediatR;

namespace EmptyProjectASPNETCORE;

public record UpdateBookFormatCommand(int id, string name) : IRequest;