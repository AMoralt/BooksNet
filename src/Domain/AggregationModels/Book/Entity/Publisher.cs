using Domain.Root;

namespace Domain.AggregationModels.Book;

public class Publisher : Entity
{
    public string Name { get; }
    public Publisher(int? id, string name)
    {
        Id = id;
        Name = name;
    }
    public static Publisher Create(int? id, string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new Exception("Publisher name is required");
        if(!string.Concat(name.Where(c=>!char.IsWhiteSpace(c))).All(char.IsLetter) )
            throw new Exception("Publisher name must contain only letters");
        if(name.Length > 50) 
            throw new Exception("Publisher name must be less than 50 characters");
        
        return new Publisher(id, name);
    }
}