using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Author : Entity
{
    private Author(){}
    public string FirstName { get; }
    public string LastName { get; }
    public Author(int id, string lastName, string firstName)
    {
        Id = id;
        LastName = lastName;
        FirstName = firstName;
    }
}