namespace EmptyProjectASPNETCORE;

public record GetBookResponse(
    string title,
    string ISBN,
    string genre,
    string publisherName,
    DateTime publicationDate,
    List<AuthorResponse> authors,
    string bookFormat,
    int price,
    int quantity);
public record AuthorResponse(string last, string first);