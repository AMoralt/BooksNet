using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class Publisher : Entity
{
    public string Name { get; }
    public Publisher(int id, string name)
    {
        Id = id;
        Name = name;
    }
}