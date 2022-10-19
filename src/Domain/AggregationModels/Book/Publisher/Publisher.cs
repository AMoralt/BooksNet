using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Publisher : Entity
{
    public string FirstName { get; }
    public string LastName { get; }
    public Publisher(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
}