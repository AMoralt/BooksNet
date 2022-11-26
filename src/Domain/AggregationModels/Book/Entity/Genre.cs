using Domain.Root;

namespace Domain.AggregationModels.Book;


public class Genre : Entity
{
    public string Name { get; }
    public Genre(int? id, string name)
    {
        Id = id;
        Name = name;
    }
    public static Genre Create(int? id, string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new Exception("Genre name is required");
        if(!string.Concat(name.Where(c=>!char.IsWhiteSpace(c))).All(char.IsLetter) )
            throw new Exception("Genre name must contain only letters");
        if(name.Length > 50) 
            throw new Exception("Publisher name must be less than 50 characters");
        
        return new Genre(id, name);
    }
}