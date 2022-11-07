using EmptyProjectASPNETCORE;

namespace Domain.AggregationModels.Book;


public class Genre : Entity
{
    public string Name { get; }
    public Genre(int id, string name)
    {
        Id = id;
        Name = name;
    }
}