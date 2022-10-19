using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Author : Entity
{
    public string FirstName { get; }
    public string LastName { get; }
    public IReadOnlyCollection<Book>? Books { get; }
    public Author(int id, string lastName, string firstName, IReadOnlyCollection<Book>? books)
    {
        Id = id;
        LastName = lastName;
        FirstName = firstName;
        Books = books;
    }
}