
namespace Application.Query;

public record GetAuthorResponse(
    int id,
    string LastName,
    string FirstName);