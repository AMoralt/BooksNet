using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;

public class BookFormat : Entity
{
    public string Name { get; }
    public BookFormat(int id, string name)
    {
        Id = id;
        Name = name;
    }
}