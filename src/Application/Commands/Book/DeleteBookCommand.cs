using MediatR;

namespace EmptyProjectASPNETCORE;

public record DeleteBookCommand(string ISBN) : IRequest;