using MediatR;

namespace EmptyProjectASPNETCORE;

public record CreateGenreCommand(string name) : IRequest;