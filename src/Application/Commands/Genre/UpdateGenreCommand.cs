using MediatR;

namespace EmptyProjectASPNETCORE;

public record UpdateGenreCommand(int id, string name) : IRequest;