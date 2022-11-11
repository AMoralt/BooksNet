using MediatR;

namespace EmptyProjectASPNETCORE;

public record DeleteBookFormatCommand(int id) : IRequest;