using MediatR;

namespace EmptyProjectASPNETCORE;

public record DeleteGenreCommand(int id) : IRequest;